using System;
using System.Drawing;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows.Forms;
using WindowsApplication.Io;
using WindowsApplication.Sensors;

namespace WindowsApplication
{
	public partial class Form1 : Form
	{
		private const string FTDI_SENSOR_DESCRIPTION = "Carrera Sensor Control";
		private const string FTDI_ACTUATOR_DESCRIPTION = "Carrera Handset Control";

		private readonly Stream ActuatorStream;


		private readonly ReglerControl[] Controllers;

		public Form1()
		{
			this.Font = SystemFonts.MessageBoxFont;
			InitializeComponent();

			this.Controllers = new ReglerControl[] {
				this.reglerControl1,
				this.reglerControl2,
				this.reglerControl3,
				this.reglerControl4,
				this.reglerControl5,
				this.reglerControl6,
				this.reglerControl7,
				this.reglerControl8,
			};
			for (int i=0;i<this.Controllers.Length;i++)
				this.Controllers[i].Text = "Regler "+(i+1);
			
			this.ActuatorStream = new FtdiStream(FTDI_ACTUATOR_DESCRIPTION);
			var ftdiStream = new FtdiStream(FTDI_SENSOR_DESCRIPTION);
			var packetReader = new SensorPacketReader(ftdiStream);

			Subject<SensorPacket> sensorStream = new Subject<SensorPacket>();
			
			new Thread((ThreadStart)delegate
				{
					while (true)
						sensorStream.OnNext(packetReader.ReadPacket());
				}) { IsBackground = true }.Start();

			
			sensorStream
				.OfType<LaneSensorPacket>()
				.ObserveOn(SynchronizationContext.Current)
				.Subscribe(psp =>
				{
					this.Controllers[psp.ControllerID].Speed = psp.Speed;
					this.Controllers[psp.ControllerID].Pressed = psp.LaneChangeButtonPressed;
				});

			sensorStream
				.OfType<PositionSensorPacket>()
				.GroupBy(psp => psp.CarID)
				.SelectMany(group => group.Throttle(TimeSpan.FromMilliseconds(10)))
				.ObserveOn(SynchronizationContext.Current)
				.Subscribe (psp =>
				{
					this.roadMapControl1.ActivateSensor(psp.SensorID,psp.CarID);										
					if (psp.SensorID==19 || psp.SensorID==21)
						this.Controllers[psp.CarID].LapCompleted(psp.TimeStamp);
				});
		}


		private void OnFormClosed(object sender,FormClosedEventArgs e)
		{
			this.ActuatorStream.Dispose();
		}
	}
}
