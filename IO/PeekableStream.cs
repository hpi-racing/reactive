using System;
using System.IO;
using System.Collections.Generic;

namespace WindowsApplication.Io
{
	public class PeekableStream : Stream
	{
		private readonly Stream Stream;
		private readonly List<byte> Buffer;

		public PeekableStream(Stream stream)
		{
			if (!stream.CanRead)
				throw new ArgumentException("PeekableStream must be constructed with a readable stream.", "stream");

			Stream = stream;
			Buffer = new List<byte>();
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			Stream.Dispose();
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public override long Length
		{
			get { throw new NotSupportedException(); }
		}

		public override long Position
		{
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

		public void Unread(byte b)
		{
			Buffer.Insert(0, b);
		}

		/// <summary>
		/// Returns the byte at the given offset without removing it from
		/// the stream, or returns -1 if at the end of the stream.
		/// </summary>
		/// <param name="offset">
		/// The zero-based byte offset from the current position in the
		/// stream.
		/// </param>
		/// <returns>
		/// The unsigned byte cast to Int32, or -1 if at the end of the stream.
		/// </returns>
		public int PeekByte(int offset)
		{
			if (offset >= Buffer.Count)
				FillBuffer(1 + offset - Buffer.Count);

			if (offset >= Buffer.Count)
				return -1;

			return Buffer[offset];
		}

		private void FillBuffer(int count)
		{
            count = 8 * ((count / 8) + 1);
			byte[] buffer = new byte[count];
			int totalBytesRead = 0;

			while (totalBytesRead < count)
			{
				int bytesRemaining = count - totalBytesRead;
				int bytesRead = Stream.Read(buffer, totalBytesRead, bytesRemaining);

				if (bytesRead == 0)
					break;

				totalBytesRead += bytesRead;
			}

			for (int i = 0; i < totalBytesRead; i++)
				Buffer.Add(buffer[i]);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int bytesRead = ReadFromBuffer(buffer, offset, count);
			bytesRead += ReadFromStream(
				buffer, offset + bytesRead, count - bytesRead
			);

			return bytesRead;
		}

		private int ReadFromBuffer(byte[] buffer, int offset, int count)
		{
			int result = Math.Min(count, Buffer.Count);

			for (int i = 0; i < result; i++)
			{
				buffer[i + offset] = Buffer[i];
			}

			Buffer.RemoveRange(0, result);

			return result;
		}

		private int ReadFromStream(byte[] buffer, int offset, int count)
		{
			if (count <= 0)
				return 0;

			return Stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}
	}
}
