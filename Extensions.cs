
namespace WindowsApplication
{
	public static class Extensions
	{
		public static bool Bit(this int value,byte position)
		{
			return (value & (1<<position))!=0;
		}

		public static int ToInt(this bool[] values)
		{
			int result = 0;
			for (int i=0;i<values.Length;i++)
				if (values[values.Length-i-1])
					result |= 1<<i;
			return result;
		}
	}
}
