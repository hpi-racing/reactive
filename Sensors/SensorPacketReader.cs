using System;
using System.IO;

using WindowsApplication.Io;

namespace WindowsApplication.Sensors
{
	public class SensorPacketReader : IDisposable
	{
		private const byte PACKET_TYPE_BIT_MASK = 0xE0;
		private const byte PAYLOAD_LENGTH_BIT_MASK = 0x07;
		private const int HEADER_LENGTH = 4;

		private const byte PACKET_START_MAGIC = 0xAA;

		private readonly PeekableStream Stream;

		public SensorPacketReader(Stream stream)
		{
			this.Stream = new PeekableStream(stream);
		}

		public void Close()
		{
			this.Dispose();
		}

		public void Dispose()
		{
			Stream.Dispose();
		}

		/// <summary>
		/// Reads the next packet from the underlying stream, or returns
		/// null if at the end of the stream.
		/// </summary>
		public SensorPacket ReadPacket()
		{
			this.ReadUntilStreamIsSynchronized();
			byte[] packet = this.ReadPacketBytes();

			if (packet == null)
				return null;

			return this.Parse(packet);
		}

		private void ReadUntilStreamIsSynchronized()
		{
			while (true)
			{
				if (Stream.PeekByte(0) != PACKET_START_MAGIC)
					this.ReadUntil(PACKET_START_MAGIC);

				int header = Stream.PeekByte(1);
				int payloadLength = header & PAYLOAD_LENGTH_BIT_MASK;
				int nextPacketStart = Stream.PeekByte(HEADER_LENGTH + payloadLength);

				if (nextPacketStart == PACKET_START_MAGIC || nextPacketStart == -1)
					return;
				else
					this.ReadAfter(PACKET_START_MAGIC);
			}
		}

		private void ReadUntil(byte b)
		{
			this.ReadAfter(b);
			Stream.Unread(b);
		}

		private void ReadAfter(byte b)
		{
			while (true)
			{
				int read = Stream.ReadByte();

				if (read == -1 || read == b)
					return;
			}
		}

		private byte[] ReadPacketBytes()
		{
			byte[] header = this.Read(HEADER_LENGTH);

			if (header == null)
				return null;

			int payloadLength = this.GetPayloadLengthFrom(header);
			byte[] payload = this.Read(payloadLength);

			if (payload == null)
				return null;

			byte[] result = new byte[header.Length + payload.Length];
			header.CopyTo(result,0);
			payload.CopyTo(result,header.Length);

			return result;
		}

		private int GetPayloadLengthFrom(byte[] header)
		{
			return header[1] & PAYLOAD_LENGTH_BIT_MASK;
		}

		private byte[] Read(int count)
		{
			byte[] result = new byte[count];
			int totalBytesRead = 0;

			while (totalBytesRead < result.Length)
			{
				int bytesRemaining = result.Length - totalBytesRead;
				int bytesRead = Stream.Read(result,totalBytesRead,bytesRemaining);

				if (bytesRead == 0)
					return null;

				totalBytesRead += bytesRead;
			}

			return result;
		}

		private SensorPacket Parse(byte[] packet)
		{
			return SensorPacket.Create(
				this.GetPacketTypeFrom(packet),
				this.GetTimeStampFrom(packet),
				this.GetPayloadFrom(packet)
			);
		}

		private SensorPacketType GetPacketTypeFrom(byte[] packet)
		{
			byte type = (byte)(packet[1] & PACKET_TYPE_BIT_MASK);

			return (SensorPacketType)Enum.ToObject(
				typeof(SensorPacketType),type
			);
		}

		private ushort GetTimeStampFrom(byte[] packet)
		{
			return (ushort)(packet[2] << 8 | packet[3]);
		}

		private byte[] GetPayloadFrom(byte[] packet)
		{
			int payloadLength = packet.Length - HEADER_LENGTH;
			byte[] result = new byte[payloadLength];

			Array.Copy(
				packet,HEADER_LENGTH,
				result,0,result.Length
			);

			return result;
		}
	}
}
