using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace vltest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Vlc.DotNet.Forms.VlcControl vlc
        void   t()
        {
            Vlc.DotNet.Forms.VlcControl vlc = new Vlc.DotNet.Forms.VlcControl();
            ((System.ComponentModel.ISupportInitialize)(vlc)).BeginInit();
            //this.SuspendLayout();   
           
             this.Controls.Add(vlc);
           
            vlc.VlcLibDirectoryNeeded += vlc_VlcLibDirectoryNeeded;
            ((System.ComponentModel.ISupportInitialize)(vlc)).EndInit();
            //this.ResumeLayout(false); 
            
            //Vlc.DotNet.Forms.VlcControl vlc = new Vlc.DotNet.Forms.VlcControl();
            


            //wz w1 = GetWZ(sm, i, wei, hei, 0, y0);
            vlc.Location = new System.Drawing.Point(0, 0);
            vlc.Size = new System.Drawing.Size(this.Width, this.Height);
            //vlc.Source = jt1.url;
            // vlc.Video.AspectRatio = vlc.Width.ToString() + ":" + vlc.Height.ToString();

           //、、//、 vlc.Play(new Uri("rtsp://169.85.100.36/axis-media/media.amp?camera=1"));
            //this.Controls.Add(vlc);

            vlc.Play(new Uri("rtsp://169.85.100.36/axis-media/media.amp?camera=1"));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            t();
           
        }

        private void vlc_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

        }
    }
}
