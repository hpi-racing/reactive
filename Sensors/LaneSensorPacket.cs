using System;
using System.Collections;

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
            //TAG print packet data
            //Console.WriteLine("new package data");
            //Console.WriteLine(Convert.ToString(data[0], 2));
            //Console.WriteLine(Convert.ToString(data[1], 2));
            this.TankEnabled = (bool) new BitArray(data)[1];
            this.TankEnabled = (bool)Convert.ToBoolean(data[1]);
            var actuatorData = data[0];

            this.ControllerID = actuatorData >> 5;
            this.LaneChangeButtonPressed = !Convert.ToBoolean(actuatorData & 0x10);
            this.Speed = actuatorData & 0x0F;
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

        public override string ToString()
        {
            return base.ToString() + "\ntank: " + this.TankEnabled + "\ncontrollerid: " + this.ControllerID +
                "\nlangecng: " + this.LaneChangeButtonPressed + "\nspeed: " + this.Speed;
        }
    }
}
