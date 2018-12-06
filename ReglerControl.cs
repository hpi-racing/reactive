using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsApplication
{
	public partial class ReglerControl : UserControl
	{
		private ushort lastTimeStamp = 0;
		private TimeSpan fastestLap;

		public ReglerControl()
		{
			InitializeComponent();
		}

		public override string Text
		{
			get { return this.groupBox1.Text; }
			set { this.groupBox1.Text = value; }
		}

		public int Speed
		{
			set { this.label2.Text = value.ToString(); }
		}

		public bool Pressed
		{
			set { this.label2.ForeColor = (value) ? Color.Red : Color.Black; }
		}

		public void LapCompleted(ushort timestamp)
		{
			if (timestamp>0)
			{
				var currentLapTime = TimeSpan.FromMilliseconds(timestamp - this.lastTimeStamp);
				this.fastestLap = (this.fastestLap > currentLapTime || this.lastTimeStamp==0) ? currentLapTime : this.fastestLap;

				this.labelCurrentLap.Text = currentLapTime.ToString(@"mm\:ss\.ff");
				this.labelFastestLap.Text = this.fastestLap.ToString(@"mm\:ss\.ff");
			}

			this.lastTimeStamp = timestamp;
		}
	}
}
