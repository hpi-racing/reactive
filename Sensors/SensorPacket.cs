using System;

namespace WindowsApplication.Sensors
{
	public enum SensorPacketType
	{
		Lane = 0x00,
		Race = 0x20,
		Junction = 0x40,
		ActiveControllers = 0x60,
		Position = 0x80,
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
				case SensorPacketType.Race:
				case SensorPacketType.ActiveControllers:
					// TODO
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
