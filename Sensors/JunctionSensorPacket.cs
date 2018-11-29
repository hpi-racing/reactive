using System;

namespace WindowsApplication.Sensors
{
	public class JunctionSensorPacket : SensorPacket
	{
		public JunctionSensorPacket(ushort timeStamp, byte[] data)
			: base(timeStamp)
		{
		}
	}
}
