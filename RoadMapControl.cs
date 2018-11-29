using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsApplication
{
	public partial class RoadMapControl : UserControl
	{
		private readonly Dictionary<int,Label> sensorLabels;

		public RoadMapControl()
		{
			InitializeComponent();
			this.sensorLabels = this.Controls.OfType<Label>().ToDictionary(label => Int32.Parse(label.Text),label => label);

			foreach (Label label in this.sensorLabels.Values)
			{
				label.Visible = false;

				Timer timer = new Timer();
				timer.Interval = 500;
				timer.Tick += delegate
				{
					label.Visible = false;
					timer.Enabled = false;
				};
				label.Tag = timer;
			}

		}

		public void ActivateSensor(int sensorID,int carID)
		{
			Label label = this.sensorLabels[sensorID];
			label.Text = (carID+1).ToString();
			label.Visible = true;
			((Timer)label.Tag).Enabled = false;
			((Timer)label.Tag).Enabled = true;
		}
	}
}
