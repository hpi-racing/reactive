using System;

namespace WindowsApplication.Sensors
{
	public enum SensorPacketType
	{
		Lane = 0x00,
		Position = 0x80,
		Junction = 0x40
	}

	public abstract class SensorPacket
	{
		public static SensorPacket Create(SensorPacketType type,ushort timeStamp,byte[] data)
		{
			switch (type)
			{
				case SensorPacketType.Lane:
					return new LaneSensorPacket(timeStamp,data);
				case SensorPacketType.Position:
					return new PositionSensorPacket(timeStamp,data);
				case SensorPacketType.Junction:
					return new JunctionSensorPacket(timeStamp,data);
			}

			throw new ArgumentException("Invalid packet type " + type);
		}

		public ushort TimeStamp { get; private set; }

		public SensorPacket(ushort timeStamp)
		{
			this.TimeStamp = timeStamp;
		}
	}
}
