namespace WindowsApplication
{
	partial class Form1
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.roadMapControl1 = new WindowsApplication.RoadMapControl();
			this.reglerControl6 = new WindowsApplication.ReglerControl();
			this.reglerControl5 = new WindowsApplication.ReglerControl();
			this.reglerControl4 = new WindowsApplication.ReglerControl();
			this.reglerControl3 = new WindowsApplication.ReglerControl();
			this.reglerControl2 = new WindowsApplication.ReglerControl();
			this.reglerControl1 = new WindowsApplication.ReglerControl();
			this.reglerControl7 = new WindowsApplication.ReglerControl();
			this.reglerControl8 = new WindowsApplication.ReglerControl();
			this.SuspendLayout();
			// 
			// roadMapControl1
			// 
			this.roadMapControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.roadMapControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("roadMapControl1.BackgroundImage")));
			this.roadMapControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.roadMapControl1.Location = new System.Drawing.Point(40, 213);
			this.roadMapControl1.Name = "roadMapControl1";
			this.roadMapControl1.Size = new System.Drawing.Size(1456, 677);
			this.roadMapControl1.TabIndex = 14;
			// 
			// reglerControl6
			// 
			this.reglerControl6.Location = new System.Drawing.Point(982, 12);
			this.reglerControl6.Name = "reglerControl6";
			this.reglerControl6.Size = new System.Drawing.Size(188, 160);
			this.reglerControl6.TabIndex = 13;
			// 
			// reglerControl5
			// 
			this.reglerControl5.Location = new System.Drawing.Point(788, 12);
			this.reglerControl5.Name = "reglerControl5";
			this.reglerControl5.Size = new System.Drawing.Size(188, 160);
			this.reglerControl5.TabIndex = 12;
			// 
			// reglerControl4
			// 
			this.reglerControl4.Location = new System.Drawing.Point(594, 12);
			this.reglerControl4.Name = "reglerControl4";
			this.reglerControl4.Size = new System.Drawing.Size(188, 160);
			this.reglerControl4.TabIndex = 11;
			// 
			// reglerControl3
			// 
			this.reglerControl3.Location = new System.Drawing.Point(400, 12);
			this.reglerControl3.Name = "reglerControl3";
			this.reglerControl3.Size = new System.Drawing.Size(188, 160);
			this.reglerControl3.TabIndex = 10;
			// 
			// reglerControl2
			// 
			this.reglerControl2.Location = new System.Drawing.Point(206, 12);
			this.reglerControl2.Name = "reglerControl2";
			this.reglerControl2.Size = new System.Drawing.Size(188, 160);
			this.reglerControl2.TabIndex = 9;
			// 
			// reglerControl1
			// 
			this.reglerControl1.Location = new System.Drawing.Point(12, 12);
			this.reglerControl1.Name = "reglerControl1";
			this.reglerControl1.Size = new System.Drawing.Size(188, 160);
			this.reglerControl1.TabIndex = 8;
			// 
			// reglerControl7
			// 
			this.reglerControl7.Location = new System.Drawing.Point(1176, 12);
			this.reglerControl7.Name = "reglerControl7";
			this.reglerControl7.Size = new System.Drawing.Size(188, 160);
			this.reglerControl7.TabIndex = 15;
			// 
			// reglerControl8
			// 
			this.reglerControl8.Location = new System.Drawing.Point(1370, 12);
			this.reglerControl8.Name = "reglerControl8";
			this.reglerControl8.Size = new System.Drawing.Size(188, 160);
			this.reglerControl8.TabIndex = 16;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1567, 902);
			this.Controls.Add(this.reglerControl8);
			this.Controls.Add(this.reglerControl7);
			this.Controls.Add(this.roadMapControl1);
			this.Controls.Add(this.reglerControl6);
			this.Controls.Add(this.reglerControl5);
			this.Controls.Add(this.reglerControl4);
			this.Controls.Add(this.reglerControl3);
			this.Controls.Add(this.reglerControl2);
			this.Controls.Add(this.reglerControl1);
			this.Name = "Form1";
			this.Text = "Carrera Racetrack";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.ResumeLayout(false);

		}

		#endregion

		private ReglerControl reglerControl1;
		private ReglerControl reglerControl2;
		private ReglerControl reglerControl3;
		private ReglerControl reglerControl4;
		private ReglerControl reglerControl5;
		private ReglerControl reglerControl6;
		private RoadMapControl roadMapControl1;
		private ReglerControl reglerControl7;
		private ReglerControl reglerControl8;
	}
}

