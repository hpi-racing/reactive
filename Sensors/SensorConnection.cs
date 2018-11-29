using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace WindowsApplication.Sensors
{
    public class SensorConnection : IDisposable
	{
		private readonly SensorPacketReader Reader;
		private readonly Thread ReaderThread;
		private readonly ConcurrentQueue<ISensorPacketHandler> PacketHandlers;

        private volatile bool Running = true;

        public SensorConnection(Stream stream)
        {
			Reader = new SensorPacketReader(stream);
			PacketHandlers = new ConcurrentQueue<ISensorPacketHandler>();

            ReaderThread = new Thread(ReceiveData);
            ReaderThread.IsBackground = true;
            ReaderThread.Start();
        }

		public void RegisterPacketHandler(ISensorPacketHandler handler)
		{
			PacketHandlers.Enqueue(handler);
		}

		public void Dispose()
		{
			Running = false;

			if (!ReaderThread.Join(500))
			{
				ReaderThread.Abort();
            }

            Reader.Dispose();
		}

        private void ReceiveData()
        {
			while (Running)
			{
				SensorPacket packet = Reader.ReadPacket();

				if (packet == null)
					return;

				foreach (var packetHandler in PacketHandlers)
					packet.Accept(packetHandler);
			}
        }
	}
}
 
