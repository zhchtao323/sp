namespace mjpeg
{
	partial class cameraW
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
            //if (mjpegSource != null)
            //{
            //    Stop();
            //    //mjpegSource.Stop();
            //}
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.cms1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kslx = new System.Windows.Forms.ToolStripMenuItem();
            this.tzlx = new System.Windows.Forms.ToolStripMenuItem();
            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.录像方式实时ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.录像方式运动检测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.录像方式1秒1桢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.浏览器控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.不显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmErr = new System.Windows.Forms.Timer(this.components);
            this.cms1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cms1
            // 
            this.cms1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cms1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kslx,
            this.tzlx,
            this.选择ToolStripMenuItem,
            this.录像方式实时ToolStripMenuItem,
            this.录像方式运动检测ToolStripMenuItem,
            this.录像方式1秒1桢ToolStripMenuItem,
            this.浏览器控制ToolStripMenuItem,
            this.不显示ToolStripMenuItem,
            this.显示ToolStripMenuItem});
            this.cms1.Name = "cms1";
            this.cms1.Size = new System.Drawing.Size(185, 202);
            this.cms1.Text = "曲线";
            // 
            // kslx
            // 
            this.kslx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.kslx.ForeColor = System.Drawing.Color.Red;
            this.kslx.Name = "kslx";
            this.kslx.Size = new System.Drawing.Size(184, 22);
            this.kslx.Text = "开始录像";
            this.kslx.Click += new System.EventHandler(this.kslx_Click);
            // 
            // tzlx
            // 
            this.tzlx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tzlx.ForeColor = System.Drawing.Color.Red;
            this.tzlx.Name = "tzlx";
            this.tzlx.Size = new System.Drawing.Size(184, 22);
            this.tzlx.Text = "停止录像";
            this.tzlx.Click += new System.EventHandler(this.tzlx_Click);
            // 
            // 选择ToolStripMenuItem
            // 
            this.选择ToolStripMenuItem.Name = "选择ToolStripMenuItem";
            this.选择ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.选择ToolStripMenuItem.Text = "取消";
            this.选择ToolStripMenuItem.Click += new System.EventHandler(this.选择ToolStripMenuItem_Click);
            // 
            // 录像方式实时ToolStripMenuItem
            // 
            this.录像方式实时ToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.录像方式实时ToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.录像方式实时ToolStripMenuItem.Name = "录像方式实时ToolStripMenuItem";
            this.录像方式实时ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.录像方式实时ToolStripMenuItem.Text = "录像方式：实时";
            this.录像方式实时ToolStripMenuItem.Click += new System.EventHandler(this.录像方式实时ToolStripMenuItem_Click);
            // 
            // 录像方式运动检测ToolStripMenuItem
            // 
            this.录像方式运动检测ToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.录像方式运动检测ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.录像方式运动检测ToolStripMenuItem.Name = "录像方式运动检测ToolStripMenuItem";
            this.录像方式运动检测ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.录像方式运动检测ToolStripMenuItem.Text = "录像方式：运动检测";
            this.录像方式运动检测ToolStripMenuItem.Click += new System.EventHandler(this.录像方式运动检测ToolStripMenuItem_Click);
            // 
            // 录像方式1秒1桢ToolStripMenuItem
            // 
            this.录像方式1秒1桢ToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.录像方式1秒1桢ToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.录像方式1秒1桢ToolStripMenuItem.Name = "录像方式1秒1桢ToolStripMenuItem";
            this.录像方式1秒1桢ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.录像方式1秒1桢ToolStripMenuItem.Text = "录像方式：1秒1桢";
            this.录像方式1秒1桢ToolStripMenuItem.Click += new System.EventHandler(this.录像方式1秒1桢ToolStripMenuItem_Click);
            // 
            // 浏览器控制ToolStripMenuItem
            // 
            this.浏览器控制ToolStripMenuItem.Name = "浏览器控制ToolStripMenuItem";
            this.浏览器控制ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.浏览器控制ToolStripMenuItem.Text = "浏览器控制";
            this.浏览器控制ToolStripMenuItem.Click += new System.EventHandler(this.浏览器控制ToolStripMenuItem_Click);
            // 
            // 不显示ToolStripMenuItem
            // 
            this.不显示ToolStripMenuItem.ForeColor = System.Drawing.Color.Cyan;
            this.不显示ToolStripMenuItem.Name = "不显示ToolStripMenuItem";
            this.不显示ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.不显示ToolStripMenuItem.Text = "录像中不显示";
            this.不显示ToolStripMenuItem.Click += new System.EventHandler(this.不显示ToolStripMenuItem_Click);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.ForeColor = System.Drawing.Color.Cyan;
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.显示ToolStripMenuItem.Text = "录像中显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 6000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmErr
            // 
            this.tmErr.Enabled = true;
            this.tmErr.Interval = 10000;
            this.tmErr.Tick += new System.EventHandler(this.tmErr_Tick);
            // 
            // cameraW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "cameraW";
            this.Size = new System.Drawing.Size(321, 319);
            this.Load += new System.EventHandler(this.w_Load);
            this.cms1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ContextMenuStrip cms1;
        private System.Windows.Forms.ToolStripMenuItem kslx;
        private System.Windows.Forms.ToolStripMenuItem tzlx;
        private System.Windows.Forms.ToolStripMenuItem 选择ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 录像方式实时ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 录像方式运动检测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 录像方式1秒1桢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 浏览器控制ToolStripMenuItem;
        private System.Windows.Forms.Timer tmErr;
        private System.Windows.Forms.ToolStripMenuItem 不显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;


    }
}
