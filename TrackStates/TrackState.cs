using System;
using System.IO;

using WindowsApplication.Sensors;

namespace WindowsApplication.TrackStates
{
	abstract class TrackState : ISensorPacketHandler
	{
		public event Action<TrackState> NextStateChanged;

		protected readonly Stream ActuatorStream;
		private TrackState NextStateField;

		public TrackState(Stream actuator)
		{
			ActuatorStream = actuator;
			NextState = this;
		}

		protected void SendCommand(byte speed, bool button)
		{
			byte sendByte = (byte)((speed & 0x0F) | (((button) ? 1 : 0) << 4) | (1 << 6));
			ActuatorStream.WriteByte(sendByte);
		}

		public TrackState NextState
		{
			get { return NextStateField; }
			protected set
			{
				NextStateField = value;
				NextStateChanged.Fire(NextStateField);
			}
		}

		public virtual void HandlePacket(LaneSensorPacket packet)
		{
		}

		public virtual void HandlePacket(PositionSensorPacket packet)
		{
		}

		public virtual void HandlePacket(JunctionSensorPacket packet)
		{
		}
	}
}
