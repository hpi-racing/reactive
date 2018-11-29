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
			int payload = LaneSensorPacket.PayloadDataToInt(data);

			this.ControllerID = new[] { payload.Bit(0),payload.Bit(1),payload.Bit(2) }.ToInt();
			this.LaneChangeButtonPressed = !payload.Bit(3);
			this.Speed = new[] { payload.Bit(4),payload.Bit(5),payload.Bit(6),payload.Bit(7) }.ToInt();
			this.TankEnabled = payload.Bit(8);
		}

		private static int PayloadDataToInt(byte[] payload)
		{
			int result = 0;

			for (int i = payload.Length - 1;i >= 0;i--)
			{
				result <<= 8;
				result |= payload[i];
			}

			return result;
		}
	}
}
