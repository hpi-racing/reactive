using System;
using FTD2XX_NET;

namespace WindowsApplication.Io
{
	class FtdiException : Exception
	{
		public FTDI.FT_STATUS Status { get; private set; }

		public FtdiException(string message, FTDI.FT_STATUS status)
			: base(String.Format("{0}: {1}", message, status))
		{
			Status = status;
		}

		protected FtdiException(string message)
			: base(message)
		{
			Status = FTDI.FT_STATUS.FT_OTHER_ERROR;
		}
	}
}
