using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using videosource;
namespace mjpeg
{
	public partial class cameraWr : UserControl
	{
		public cameraWr()
		{
			InitializeComponent();
		}
		public  MJPEGSource mjpegSource;
		private Bitmap bmp;
		private string source = "";

		public string Source
		{
			get { return source; }
			set { source = value; }
		}
		public void Start()
		{
			mjpegSource.VideoSource = source;
            mjpegSource.WaitTime = 100;//ms
            mjpegSource.wr = true;
            mjpegSource.fileN = @"e:\sp.mjpeg";
			mjpegSource.Start();
            aw.FrameRate = 15;

            aw.Codec = "Divx Codec 4.12";
            string str = @"E:\sp";
            str = str + System.DateTime.Now.ToString("yyMMdd-hhmm");
            str = str + ".avi";
            aw.Open(str, 704, 576);
            //aw = new Tiger.Video.VFW.AVIWriter();
            ////aw.Codec = "DIB";
            //aw.Open(@"e:/sp.avi", 704, 576);
            //aw.FrameRate = 15;
		}
		public void Stop()
		{
			mjpegSource.Stop();
            aw.Close();
		}
		private void w_Load(object sender, EventArgs e)
		{
			mjpegSource = new MJPEGSource();
			mjpegSource.VideoSource = source;
			mjpegSource.Login = "sjfc";
			mjpegSource.Password = "sjfc"; 
			mjpegSource.NewFrame += new CameraEventHandler(mjpegSource_NewFrame);
		}

        Tiger.Video.VFW.AVIWriter aw = new Tiger.Video.VFW.AVIWriter();
        //int i = 0;
		void mjpegSource_NewFrame(object sender, CameraEventArgs e)
		{
			bmp = (Bitmap)e.Bitmap.Clone();
			Graphics g = this.CreateGraphics();
			Rectangle rc = this.ClientRectangle;
			g.DrawImage(bmp, rc.X + 1, rc.Y + 1, rc.Width - 2, rc.Height - 2);

            //Tiger.Video.VFW.AVIWriter aw = new Tiger.Video.VFW.AVIWriter();
            //aw.Codec = "Divx Codec 4.12";
            //aw.Open(@"e:/sp.avi", 704, 576);
            //aw.FrameRate = 15;
            aw.AddFrame(bmp);
            //aw.Close();
           
            //System.IO.FileStream fs = new System.IO.FileStream(@"E:\sp.mjpeg", System.IO.FileMode.OpenOrCreate);
            //fs.Seek(i * 2000, System.IO.SeekOrigin.Begin);//18932
            //bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            ////bmp.SaveAdd(System.Drawing.Imaging.FrameDimension.
            ////fs.Write(buffer, start, stop - start);
            //fs.Close();
            //i = i + 1;

		}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string fileN=@"e:\sp.mjpeg";
            //System.IO.FileStream fs = new System.IO.FileStream(fileN, System.IO.FileMode.OpenOrCreate);            
            //fs.Write();
            //fs.Close();
            
        }		
	}
}
