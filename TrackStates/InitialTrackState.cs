using System;
using System.IO;

using WindowsApplication.Sensors;

namespace WindowsApplication.TrackStates
{
	class InitialTrackState : TrackState
	{
		public InitialTrackState(Stream actuator)
			: base(actuator)
		{
			SendCommand(speed: 7, button: false);
		}

		public override void HandlePacket(PositionSensorPacket packet)
		{
			SendCommand(speed: 0, button: false);
			NextState = new RunningTrackState(ActuatorStream);
		}
	}
}
