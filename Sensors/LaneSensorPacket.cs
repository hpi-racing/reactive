using System;

namespace WindowsApplication.Sensors
{
	public class LaneSensorPacket : SensorPacket
	{
		public int ControllerID { get; private set; }
		public int Speed { get; private set; }
		public bool LaneChangeButtonPressed { get; private set; }
		public bool TankEnabled { get; private set; }

		public LaneSensorPacket(ushort timeStamp,byte[] data)
			: base(timeStamp)
		{
			this.TankEnabled = (bool) data[1];
			byte actuatorData = data[0];

			this.ControllerID = new[] { actuatorData.Bit(7),actuatorData.Bit(6),actuatorData.Bit(5) }.ToInt();
			this.LaneChangeButtonPressed = !actuatorData.Bit(4);
			this.Speed = new[] { actuatorData.Bit(3),actuatorData.Bit(2),actuatorData.Bit(1),actuatorData.Bit(0) }.ToInt();
		}

		//private static int PayloadDataToInt(byte[] payload)
		//{
			//int result = 0;

			//for (int i = payload.Length - 1;i >= 0;i--)
			//{
				//result <<= 8;
				//result |= payload[i];
			//}

			//return result;
		//}
	}
}
