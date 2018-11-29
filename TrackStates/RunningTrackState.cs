using System;
using System.IO;
using System.Timers;

using WindowsApplication.Sensors;

namespace WindowsApplication.TrackStates
{
	class RunningTrackState : TrackState
	{
		private bool LapStarted;

        private Timer LapTimer;
        private Random RandomGenerator;
        private byte RunningSpeed;

		public RunningTrackState(Stream actuator)
			: base(actuator)
		{
            RunningSpeed = 4;
			LapStarted = false;
            RandomGenerator = new Random();

            LapTimer = new Timer();
            LapTimer.AutoReset = true;
            LapTimer.Interval = 2000;
            LapTimer.Elapsed += OnLapTimerElapsed;

			Timer t = new Timer();
			t.AutoReset = false;
			t.Interval = 1000;
			t.Elapsed += OnWaitTimerElapsed;
			t.Start();
		}

        private void OnWaitTimerElapsed(object sender, ElapsedEventArgs e)
		{
			SendCommand(speed: 7, button: false);
			LapStarted = true;
            LapTimer.Start();
		}

        private void OnLapTimerElapsed(object sender, ElapsedEventArgs e)
        {
            SendCommand(RunningSpeed, false);
            RunningSpeed = (byte)((RunningSpeed == 4) ? 8 : 4);
        }

		public override void HandlePacket(PositionSensorPacket packet)
		{
			if (!LapStarted)
				return;

            LapTimer.Elapsed -= OnLapTimerElapsed;

			SendCommand(speed: 0, button: false);
			NextState = null;
		}
	}
}
