namespace mjpeg
{
	partial class Sp
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
			
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sp));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.顶层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.底层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkXh = new System.Windows.Forms.CheckBox();
            this.btnconfig = new System.Windows.Forms.Button();
            this.txtms = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.顶层ToolStripMenuItem,
            this.底层ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // 顶层ToolStripMenuItem
            // 
            this.顶层ToolStripMenuItem.Name = "顶层ToolStripMenuItem";
            resources.ApplyResources(this.顶层ToolStripMenuItem, "顶层ToolStripMenuItem");
            this.顶层ToolStripMenuItem.Click += new System.EventHandler(this.顶层ToolStripMenuItem_Click);
            // 
            // 底层ToolStripMenuItem
            // 
            this.底层ToolStripMenuItem.Name = "底层ToolStripMenuItem";
            resources.ApplyResources(this.底层ToolStripMenuItem, "底层ToolStripMenuItem");
            this.底层ToolStripMenuItem.Click += new System.EventHandler(this.底层ToolStripMenuItem_Click);
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            resources.ApplyResources(this.cmbGroup, "cmbGroup");
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkXh
            // 
            resources.ApplyResources(this.chkXh, "chkXh");
            this.chkXh.Name = "chkXh";
            this.chkXh.UseVisualStyleBackColor = true;
            this.chkXh.CheckedChanged += new System.EventHandler(this.chkXh_CheckedChanged);
            this.chkXh.CheckStateChanged += new System.EventHandler(this.chkXh_CheckStateChanged);
            // 
            // btnconfig
            // 
            resources.ApplyResources(this.btnconfig, "btnconfig");
            this.btnconfig.Name = "btnconfig";
            this.btnconfig.UseVisualStyleBackColor = true;
            this.btnconfig.Click += new System.EventHandler(this.btnconfig_Click);
            // 
            // txtms
            // 
            resources.ApplyResources(this.txtms, "txtms");
            this.txtms.Name = "txtms";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // Sp
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtms);
            this.Controls.Add(this.btnconfig);
            this.Controls.Add(this.chkXh);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Sp";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.sp_FormClosing);
            this.Load += new System.EventHandler(this.sp_Load);
            this.Resize += new System.EventHandler(this.sp_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 顶层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 底层ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkXh;
        private System.Windows.Forms.Button btnconfig;
        private System.Windows.Forms.TextBox txtms;
        private System.Windows.Forms.Button button2;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
	}
}