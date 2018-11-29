using System;
using FTD2XX_NET;

namespace WindowsApplication.Io
{
	class FtdiDeviceNotFoundException : FtdiException
	{
		public FtdiDeviceNotFoundException(string deviceDescription, FTDI.FT_STATUS status)
			: base(String.Format("Failed to open device \"{0}\"", deviceDescription), status)
		{
		}
	}
}
