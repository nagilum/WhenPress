namespace WhenPressTrayApp {
	partial class fmTray {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmTray));
			this.label1 = new System.Windows.Forms.Label();
			this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miScripts = new System.Windows.Forms.ToolStripMenuItem();
			this.miSeparatorScripts = new System.Windows.Forms.ToolStripSeparator();
			this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.miSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
			this.miReloadConfig = new System.Windows.Forms.ToolStripMenuItem();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmTray.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "WhenPress";
			// 
			// cmTray
			// 
			this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miScripts,
            this.miSeparatorScripts,
            this.miAbout,
            this.miSeparatorAbout,
            this.miReloadConfig,
            this.miExit});
			this.cmTray.Name = "cmTray";
			this.cmTray.Size = new System.Drawing.Size(150, 104);
			// 
			// miScripts
			// 
			this.miScripts.Name = "miScripts";
			this.miScripts.Size = new System.Drawing.Size(149, 22);
			this.miScripts.Text = "Scripts";
			// 
			// miSeparatorScripts
			// 
			this.miSeparatorScripts.Name = "miSeparatorScripts";
			this.miSeparatorScripts.Size = new System.Drawing.Size(146, 6);
			// 
			// miAbout
			// 
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new System.Drawing.Size(149, 22);
			this.miAbout.Text = "About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// miSeparatorAbout
			// 
			this.miSeparatorAbout.Name = "miSeparatorAbout";
			this.miSeparatorAbout.Size = new System.Drawing.Size(146, 6);
			// 
			// miReloadConfig
			// 
			this.miReloadConfig.Name = "miReloadConfig";
			this.miReloadConfig.Size = new System.Drawing.Size(149, 22);
			this.miReloadConfig.Text = "Reload Config";
			this.miReloadConfig.Click += new System.EventHandler(this.miReloadConfig_Click);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.Size = new System.Drawing.Size(149, 22);
			this.miExit.Text = "Exit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// niTray
			// 
			this.niTray.ContextMenuStrip = this.cmTray;
			this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
			this.niTray.Text = "WhenPress";
			this.niTray.Visible = true;
			// 
			// fmTray
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 31);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fmTray";
			this.ShowInTaskbar = false;
			this.Text = "WhenPress";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.fmTray_Load);
			this.cmTray.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenuStrip cmTray;
		private System.Windows.Forms.ToolStripMenuItem miScripts;
		private System.Windows.Forms.ToolStripSeparator miSeparatorScripts;
		private System.Windows.Forms.ToolStripMenuItem miAbout;
		private System.Windows.Forms.ToolStripSeparator miSeparatorAbout;
		private System.Windows.Forms.ToolStripMenuItem miReloadConfig;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.NotifyIcon niTray;
	}
}

