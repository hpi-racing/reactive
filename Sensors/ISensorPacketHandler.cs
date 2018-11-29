using System;

namespace WindowsApplication.Sensors
{
	public interface ISensorPacketHandler
	{
		void HandlePacket(LaneSensorPacket packet);
		void HandlePacket(PositionSensorPacket packet);
		void HandlePacket(JunctionSensorPacket packet);
	}
}
