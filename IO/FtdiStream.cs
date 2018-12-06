using System;
using System.IO;

using FTD2XX_NET;

namespace WindowsApplication.Io
{
	class FtdiStream : Stream
	{
		private readonly FTDI device;

		public FtdiStream(string sensorDescription)
		{
			device = new FTDI(); 
			UInt32 deviceCount = 0;
			var status = device.GetNumberOfDevices(ref deviceCount);

			if (status != FTDI.FT_STATUS.FT_OK)
				throw new FtdiException("Failed to get number of devices", status);

			Console.WriteLine("Number of FTDI devices: {0}", deviceCount);

			if (deviceCount == 0)
				throw new NoFtdiDevicesAvailableException();

			status = device.OpenByDescription(sensorDescription);
			//if (status != FTDI.FT_STATUS.FT_OK)
			//	throw new FtdiDeviceNotFoundException(sensorDescription, status);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			device.Close();
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
			get { return true; }
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
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public int BytesAvailable
		{
			get
			{
				uint result = 0;
				var status = device.GetRxBytesAvailable(ref result);

				if (status != FTDI.FT_STATUS.FT_OK)
					throw new FtdiException("Failed to count available bytes", status);

				return Convert.ToInt32(result);
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] readBuffer = new byte[count];
			uint bytesRead = 0;
			var status = device.Read(
				readBuffer, Convert.ToUInt32(count), ref bytesRead
			);

			if (status != FTDI.FT_STATUS.FT_OK)
				throw new FtdiException("Failed to read from device", status);

			Array.Copy(readBuffer, 0, buffer, offset, bytesRead);

			return Convert.ToInt32(bytesRead);
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
			while (count > 0)
			{
				byte[] writeBuffer = new byte[count];
				Array.Copy(buffer, offset, writeBuffer, 0, count);

				uint bytesWritten = 0;
				var status = device.Write(writeBuffer, count, ref bytesWritten);

				if (status != FTDI.FT_STATUS.FT_OK)
					throw new FtdiException("Failed to write to device", status);

				count -= Convert.ToInt32(bytesWritten);
				offset += Convert.ToInt32(bytesWritten);
			}
		}
	}
}
