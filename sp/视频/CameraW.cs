using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using videosource;
using motion;
using System.Runtime.InteropServices; 
//using MjpegProcessor;
namespace mjpeg
{
    public enum LxWay { Motion4Inter, TimeInter, Motion4, Always };
    /// <summary>
    /// �����ṩ��ƵԴ�����ơ�������ֹͣ��¼��¼��ʽ
    /// </summary>
    public partial class cameraW : UserControl
    {
        private Graphics g;//= this.CreateGraphics();
        private Rectangle rc;// = this.ClientRectangle;
        private bool onlyVide = false;

        public bool OnlyVide
        {
            get { return onlyVide; }
            set { onlyVide = value; }
        }
        public cameraW()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            rc = this.ClientRectangle;
            source = "";
            this.Resize += new EventHandler(cameraW_Resize);
            this.MouseDoubleClick += new MouseEventHandler(cameraW_MouseDoubleClick);
        }
        /// <summary>
        /// ����ͨ�ж�
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private Boolean IsExistC(string ip)
        {
            Ping p = new Ping();
            PingReply r = p.Send(ip, 1000);// 200
            if (r.Status == IPStatus.Success)
            { return true; }
            else
            { return false; }
        }


        void cameraW_Resize(object sender, EventArgs e)
        {
            g.Dispose();
            g = this.CreateGraphics();
            rc = this.ClientRectangle;
            rc = new Rectangle(rc.X + 1, rc.Y + 1, rc.Width - 2, rc.Height - 2);
            //g.Clear(Color.Green);
            //g.DrawString(name, new Font("����", 12), Brushes.Orange, new PointF(0, rc.Height/2));
        }
        public MJPEGSource mjpegSource;
        //MjpegProcessor.MjpegDecoder ms;
        private string source = "";
        /// <summary>
        /// ����Դ
        /// </summary>
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        private string name = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string Name1
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// ������������ͨ���˷�ʽ������Ĭ��״̬����������
        /// </summary>
        public void Start()
        {
            mjpegSource = null;
            mjpegSource = new MJPEGSource();
            mjpegSource.Login = "sjfc";
            mjpegSource.Password = "sjfc";
            mjpegSource.NewFrame += new CameraEventHandler(mjpegSource_NewFrame);
            mjpegSource.ErrEvent += mjpegSource_ErrEvent;
            mjpegSource.VideoSource = source;
            mjpegSource.Start();
            //ms = new MjpegDecoder();
            //ms.FrameReady += new EventHandler<FrameReadyEventArgs>(ms_FrameReady);
            //ms.ParseStream(new Uri(source));
        }
       object obj = new object();
        void mjpegSource_ErrEvent(object sender, string e)
        {
            //throw new NotImplementedException();
            //g.DrawImage(e, rc);
           try
           { 
            lock (obj)
            {
                //this.clientDC
             Graphics   c1 = this.CreateGraphics();
             c1.Clear(SystemColors.WindowFrame);
             c1.DrawString(e, new Font("����", 12), Brushes.Orange, new PointF(0, rc.Height / 2));
             //c1.Dispose();               
            }
           }
           catch(Exception   ex)
           {}
        
        
        }

        //void ms_FrameReady(object sender, FrameReadyEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    drawbmp11(e.Bitmap);
        //}

        /// <summary>
        /// ֹͣͨѶ
        /// </summary>
        public void Stop()
        {
            try
            {
                if (aw != null)
                {
                    aw.Close();
                }

                mjpegSource.NewFrame -= new CameraEventHandler(mjpegSource_NewFrame);
                mjpegSource.Yx = false;
                mjpegSource.Stop();
                mjpegSource = null;
                motionDetecotor = null;
                GC.Collect();
            }
            catch { }
        }
        private int width = 704;//ͼ��Ŀ��
        private int height = 576;
        private int cs = 0;
        //bilblt
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, //Ŀ���豸�ľ��   
            int nXDest,     // Ŀ���������Ͻǵ�X����   
            int nYDest,     // Ŀ���������Ͻǵ�X����   
            int nWidth,     // Ŀ�����ľ��εĿ��   
            int nHeight,    // Ŀ�����ľ��εĳ���   
            IntPtr hdcSrc,  // Դ�豸�ľ��   
            int nXSrc,      // Դ��������Ͻǵ�X����   
            int nYSrc,      // Դ��������Ͻǵ�X����   
            int dwRop       // ��դ�Ĳ���ֵ   
            );
        [DllImport("gdi32.dll")]
        private static extern bool StretchBlt(
            IntPtr hdcDest, //Ŀ���豸�ľ��   
            int nXDest,     // Ŀ���������Ͻǵ�X����   
            int nYDest,     // Ŀ���������Ͻǵ�X����   
            int nWidth,     // Ŀ�����ľ��εĿ��   
            int nHeight,    // Ŀ�����ľ��εĳ���   
            IntPtr hdcSrc,  // Դ�豸�ľ��   
            int nXSrc,      // Դ��������Ͻǵ�X����   
            int nYSrc,      // Դ��������Ͻǵ�X����  
            int nWidth1,     // Ŀ�����ľ��εĿ��   
            int nHeight1,    // Ŀ�����ľ��εĳ���   
            TernaryRasterOperations dwRop       // ��դ�Ĳ���ֵ   
            );
        //, HDC hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, DWORD dwRop)��
        public const int SRCCOPY = 0xCC0020;
        //public const int SRCCOPY = 0x00CC0020;
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdcPtr);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hdcPtr, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool DeleteDC(IntPtr hdcPtr);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        //Bitmap bmp;     // ���ڱ���ͼƬ�Ļ���   
        //int i;          // ͳ�ƻ�ͼ����   
        //end
        public enum TernaryRasterOperations
        {
            SRCCOPY = 0x00CC0020, /* dest = source*/
            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/
            SRCAND = 0x008800C6, /* dest = source AND dest*/
            SRCINVERT = 0x00660046, /* dest = source XOR dest*/
            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/
            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/
            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/
            PATCOPY = 0x00F00021, /* dest = pattern*/
            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/
            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/
            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/
            BLACKNESS = 0x00000042, /* dest = BLACK*/
            WHITENESS = 0x00FF0062, /* dest = WHITE*/
        };
        [DllImport("gdi32.dll")]
        static extern int SetStretchBltMode(IntPtr hdc, StretchBltMode iStretchMode);
        private enum StretchBltMode : int
        {
            STRETCH_ANDSCANS = 1,
            STRETCH_ORSCANS = 2,
            STRETCH_DELETESCANS = 3,
            STRETCH_HALFTONE = 4,
        }


        private delegate void drawbmp(Image e);
        Bitmap bbb;
        Graphics clientDC;
        private void drawbmp1(Image e)
        {
            width = e.Width;
            height = e.Height;
           
            if (!(video && onlyVide))//¼����ʾ
            {
                //if (this.height > 5)
                //{
                    bbb = (Bitmap)e;
                    IntPtr h1 = bbb.GetHbitmap();
                    //g.DrawImage(e, rc);
                   
                        clientDC = this.CreateGraphics();
                        IntPtr hdcPtr = clientDC.GetHdc();//clientDC.GetHdc();//
                        IntPtr memdcPtr = CreateCompatibleDC(hdcPtr);   // ��������DC  
                        IntPtr pOrig = SelectObject(memdcPtr, h1);

                        SetStretchBltMode(hdcPtr, StretchBltMode.STRETCH_DELETESCANS);
                        StretchBlt(hdcPtr, 0, 0, this.Width, this.Height, memdcPtr, 0, 0, width, height, TernaryRasterOperations.SRCCOPY);
                        IntPtr pNew = SelectObject(memdcPtr, pOrig);
                        DeleteObject(pNew);

                        DeleteDC(memdcPtr);             // �ͷ��ڴ�  
                        clientDC.ReleaseHdc(hdcPtr);    // �ͷ��ڴ�   
                        clientDC.Dispose();
                   
                //}

            }
        }
        /// <summary>
        /// ��Ƶ��֡������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mjpegSource_NewFrame(object sender, Image e)
        {
            //this.Invoke(new drawbmp(drawbmp1), e);
            drawbmp1(e);
            cs = cs + 1;
            if (cs > 65536)
            { cs = 0; }
            VideoLx(e);  //¼��                     
        }
        private int cs1 = 0;//����
        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmErr_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (System.Math.Abs(cs - cs1) < 3)
            //    {
            //        g.Dispose();
            //        g = this.CreateGraphics();
            //        rc = this.ClientRectangle;
            //        rc = new Rectangle(rc.X + 1, rc.Y + 1, rc.Width - 2, rc.Height - 2);
            //        g.Clear(Color.Green);
            //        g.DrawString(name + mjpegSource.Err, new Font("����", 12), Brushes.Orange, new PointF(0, rc.Height / 2));
            //        this.Stop();
            //        this.Start();

            //    }
            //    cs1 = cs;
            //}
            //catch { }
        }
        private void w_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ��ʾ¼��˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraW_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cms1.Left = e.X;
            cms1.Top = e.Y;
            cms1.TopLevel = true;
            cms1.Show();
        }
        /// <summary>
        /// //¼��
        /// </summary>
        private Tiger.Video.VFW.AVIWriter aw;
        private string directory = "";
        private int fcc = 0;
        private Boolean video = false;
        private MotionDetector4 motionDetecotor;
        private void AviInit()
        {
            //��ȡ�����ļ���
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "��Ƶ�ļ� (*.avi)|*.*";
            string str = @"E:\sp\sp";
            if (sav.ShowDialog() == DialogResult.OK)
            {
                str = sav.FileName;
                if (name != "")
                {
                    int j = str.LastIndexOf(@"\");
                    directory = str.Substring(0, j) + @"\sp";
                    str = directory + name + "-" + System.DateTime.Now.ToString("yyMMdd-HHmm");
                }
            }
            else
            {
                directory = @"E:\sp\sp";
                str = directory + name + "-" + System.DateTime.Now.ToString("yyMMdd-HHmm");
            }
            str = str + ".avi";
            //��Ƶд��ʼ��
            aw = new Tiger.Video.VFW.AVIWriter();
            aw.FrameRate = 10;
            aw.Codec = "divx"; //"DiVx Codec 4.12";//Micro            
            //aw.Open(str, 704, 576);
            aw.Open(str, width, height);
            fcc = aw.Fcc;
        }

        public Boolean Video  //¼��
        {
            get { return video; }
            set
            {
                motionDetecotor = new motion.MotionDetector4();
                video = value;
                if (video)
                { AviInit(); }
                else
                {
                    if (aw != null)
                    { aw.Close(); }
                }
            }
        }
        private LxWay way = LxWay.Motion4Inter;

        public LxWay Way
        {
            get { return way; }
            set
            {
                way = value;
                switch (way)
                {
                    case LxWay.Motion4Inter:
                    case LxWay.Motion4:
                        motionDetecotor = new MotionDetector4();
                        break;
                }
            }
        }

        private DateTime dt = System.DateTime.Now; //ʱ��
        private int interTime = 1000;//ʱ����
        private Boolean motionStstus = true; //�˶�״̬
        Bitmap bmp;//=new Bitmap(width, height);
        /// <summary>
        /// ¼����
        /// </summary>
        /// <param name="bmp1"></param>
        private void VideoLx(Image bmp1)
        {

            if (video)
            {
                try
                {
                    lock (bmp1)
                    {
                        bmp = (Bitmap)bmp1.Clone();
                    }
                    DateTime dt1;
                    TimeSpan sp;
                    Graphics g;
                    switch (way)
                    {
                        case LxWay.Motion4Inter:  //���ʱ�����˶����˶����¼
                            dt1 = System.DateTime.Now;
                            sp = dt1.Subtract(dt);
                            if (sp.TotalMilliseconds >= interTime)
                            {
                                if (motionDetecotor != null)
                                {
                                    if (motionDetecotor.ProcessFrame1(bmp))
                                    {
                                        aw.AddFrame(bmp);
                                        motionStstus = true;
                                    }
                                }
                                dt = dt1;
                            }
                            else
                            {
                                motionStstus = false;
                            }
                            //�˶����¼
                            if (motionStstus)
                            {
                                aw.AddFrame(bmp);
                                //g = Graphics.FromImage(bmp);
                                //g.DrawString("¼����", new Font("����", 60), Brushes.Red, new PointF(20, 150));
                            }
                            break;
                        case LxWay.Motion4:
                            if (motionDetecotor != null)
                            {
                                if (motionDetecotor.ProcessFrame1(bmp))
                                {
                                    aw.AddFrame(bmp);
                                    // g = Graphics.FromImage(bmp);
                                    // g.DrawString("¼����", new Font("����", 60), Brushes.Red, new PointF(20, 150));                          
                                }
                            }
                            break;
                        case LxWay.TimeInter:
                            dt1 = System.DateTime.Now;
                            sp = dt1.Subtract(dt);
                            if (Math.Abs(sp.TotalMilliseconds) >= interTime)
                            {
                                aw.AddFrame(bmp);
                                //g = Graphics.FromImage(bmp);
                                // g.DrawString("¼����", new Font("����", 60), Brushes.Red, new PointF(20, 150));
                                dt = dt1;
                            }
                            break;
                        case LxWay.Always:
                            aw.AddFrame(bmp);
                            // g = Graphics.FromImage(bmp);
                            // g.DrawString("¼����", new Font("����", 60), Brushes.Red, new PointF(20, 150));                          
                            break;
                    }
                }
                catch { }
            }

        }



        private void ѡ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        string strsj = DateTime.Today.ToString();//DateTime.Now.ToString("yyMMdd-HH");//= 
        /// <summary>
        /// ÿ��һ���ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (video)//¼��
            {
                string strsj1 = DateTime.Today.ToString();
                if (strsj1 != strsj)
                {
                    strsj = strsj1;
                    string str = directory + name + "-" + System.DateTime.Now.ToString("yyMMdd-HHmm");
                    str = str + ".avi";
                    aw.Close();
                    //aw.Open2(str, 704, 576);
                    aw.Open2(str, width, height);
                }
            }
        }

        /// <summary>
        /// �Ƿ�¼�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kslx_Click(object sender, EventArgs e)
        {
            Video = true;
        }

        private void tzlx_Click(object sender, EventArgs e)
        {
            Video = false;
        }
        /// <summary>
        /// ¼��ʽ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ¼��ʽʵʱToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Way = LxWay.Always;
        }
        private void ¼��ʽ�˶����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Way = LxWay.Motion4Inter;
        }
        private void ¼��ʽ1��1��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Way = LxWay.TimeInter;
        }
        /// <summary>
        /// ת���������ʾ�Ϳ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int j = source.IndexOf(@"/axis");
            if (j > 0)
            {
                string str = source.Substring(0, j);
                Process.Start(str);
            }
        }

        private void ����ʾToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyVide = true;
        }

        private void ��ʾToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyVide = false;
        }


    }
}
