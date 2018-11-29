using System;

namespace WindowsApplication.Sensors
{
	public class PositionSensorPacket : SensorPacket
	{
		public int CarID { get;  set; }
		public int SensorID { get;  set; }

		public PositionSensorPacket(ushort timeStamp)
			: base(timeStamp)
		{ }

		public PositionSensorPacket(ushort timeStamp, byte[] data)
			: base(timeStamp)
		{
			this.SensorID = data[0];
			this.CarID = data[1];
		}
	}
}
