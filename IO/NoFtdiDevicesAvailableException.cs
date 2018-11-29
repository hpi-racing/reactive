using FTD2XX_NET;

namespace WindowsApplication.Io
{
	class NoFtdiDevicesAvailableException : FtdiException
	{
		public NoFtdiDevicesAvailableException()
			: base("No FTDI devices connected. Check connection.")
		{
		}
	}
}
