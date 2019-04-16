using System;
using System.Collections.Generic;
using System.IO;
//using System.Data;
//using System.IO;
using System.Reflection;
using System.Windows.Forms;
//using System.Windows.Forms;
namespace mjpeg
{
    public partial class Sp : Form
    {
        public Sp()
        {
            InitializeComponent();
            TC.yx = true;
        }
        struct CT
        {
            public Control c1;
            public bool rtsp;

        }
        private void sp_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "SteelBlue.txt";
            skinEngine1.Active = true;
            Form.CheckForIllegalCrossThreadCalls = false;
            try
            {
                ZCT.Data.MsData m = new ZCT.Data.MsData(conn);
                string cmd = "select    GROUP_name  from sp_group  order  by xh";
                groups = m.FillList(cmd);
                cmbGroup.DataSource = groups;
                DisplayGroup();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            this.Text = "视频(双击图象、点开始录象) .net 4.0  20160608";
        }

        string conn = "Data Source=169.85.170.18;Initial Catalog=gk;User ID=sj;pwd=sj";
        private List<Vlc.DotNet.Forms.VlcControl> arr = new List<Vlc.DotNet.Forms.VlcControl>();//当前视频
        private List<mjpeg.cameraW> arc = new List<cameraW>();//当前视频
        private List<Control> ar = new List<Control>();//当前视频
        private List<JT> jtss = new List<JT>();//当前画面的地址
        private List<string> groups = new List<string>();//     
        /// <summary>
        /// 显示
        /// </summary>
        private void DisplayGroup()
        {
            ZCT.Data.MsData m = new ZCT.Data.MsData(conn);
            string cmd = "";
            if (cmbGroup.Text.Trim() != "")
            {
                string str = cmbGroup.Text.Trim();
                cmd = "select *  from   sp where group_name='" + str + "' order  by  xh";
                jtss = m.FillCollection<JT>(cmd);
                if (chkXh.Checked)
                {
                    //int intr = 4000;
                    //int.TryParse(txtms.Text, out intr);
                    //if (intr == 0)
                    //{ intr = 4000; }
                    //timer1.Interval = intr;
                    //timer1.Enabled = true;
                    //cur = 0;
                    //spx_jg();
                }
                else
                {
                    timer1.Enabled = false;
                    spxshi();

                }
            }
        }

        private void sp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Resize -= new EventHandler(sp_Resize);
            TC.yx = false;
            foreach (mjpeg.cameraW c in arc)
            {
                c.mjpegSource.Yx = false;
                c.Stop();
                c.Dispose();
            }
            foreach (Vlc.DotNet.Forms.VlcControl c in arr)
            {                
                c.Stop();
                c.Dispose();
            }
            ar.Clear();
            arc.Clear();
            arr.Clear();
            ar = null;
            
            GC.Collect();
        }
        private int getsm(int ccc)
        {
            int sm = 0;
            if (ccc > 9)
            { sm = 12; }
            else
            {
                if (ccc > 8)
                { sm = 9; }
                else
                {
                    if (ccc > 6)
                    { sm = 8; }
                    else
                    {
                        if (ccc > 4)
                        { sm = 6; }
                        else
                        {
                            if (ccc > 2)
                            { sm = 4; }
                            else
                            {
                                if (ccc > 1)
                                { sm = 2; }
                                else
                                { sm = 1; }
                            }
                        }
                    }
                }
            }
            return sm;
        }
        int cur = 0;
        public void spx_jg()
        {
            try
            {
                TC.yx = false;

                //清除现有镜头
                this.Cursor = Cursors.WaitCursor;
                foreach (mjpeg.cameraW c in arc)
                {
                    c.mjpegSource.Yx = false;
                    c.Stop();
                    c.Dispose();
                }
                foreach (Vlc.DotNet.Forms.VlcControl c in arr)
                {
                    c.Stop();
                    c.Dispose();
                }
                ar.Clear();
                arc.Clear();
                arr.Clear();
                GC.Collect();

                TC.yx = true;
                mjpeg.cameraW c2 = new cameraW();
                c2.Dock = DockStyle.Fill;
                c2.Source = jtss[cur].url;

                this.Controls.Add(c2);
                ar.Add(c2);
                c2.Start();

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }

        }
        #region  //xs视频显示
        struct wz
        {
            public int x;
            public int y;
            public int w;
            public int h;
        }
        wz GetWZ(int sm, int i, int wei, int hei, int x0, int y0)
        {
            wz w1 = new wz();
            switch (sm)
            {
                case 1:
                    w1.x = 0;
                    w1.y = y0;
                    w1.h = hei - y0;
                    w1.w = wei;
                    //c.Location = new System.Drawing.Point(0, 0 + y0);
                    //c.Dock = System.Windows.Forms.DockStyle.Fill;
                    break;
                case 2:
                    //c.Location = new System.Drawing.Point(i * wei / 2, 0 + y0);
                    //c.Size = new System.Drawing.Size(wei / 2, hei);
                    w1.x = i * wei / 2;
                    w1.y = y0;
                    w1.h = hei;
                    w1.w = wei / 2;
                    break;
                case 4:
                    int[,] wz = new int[,] { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } };
                    //c.Location = new System.Drawing.Point(wz[i, 0] * wei / 2, wz[i, 1] * hei / 2 + y0);
                    //c.Size = new System.Drawing.Size(wei / 2, hei / 2);
                    w1.x = wz[i, 0] * wei / 2;
                    w1.y = wz[i, 1] * hei / 2 + y0;
                    w1.h = hei / 2;
                    w1.w = wei / 2;
                    break;
                case 6:
                    int[,] wz6 = new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } };
                    //c.Location = new System.Drawing.Point(wz6[i, 0] * wei / 3, wz6[i, 1] * hei / 2 + y0);
                    //c.Size = new System.Drawing.Size(wei / 3, hei / 2);
                    w1.x = wz6[i, 0] * wei / 3;
                    w1.y = wz6[i, 1] * hei / 2 + y0;
                    w1.h = hei / 2;
                    w1.w = wei / 3;
                    break;
                case 8:
                    int[,] wz8 = new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 }, { 3, 1 } };
                    //c.Location = new System.Drawing.Point(wz8[i, 0] * wei / 4, wz8[i, 1] * hei / 2 + y0);
                    //c.Size = new System.Drawing.Size(wei / 4, hei / 2);
                    w1.x = wz8[i, 0] * wei / 4;
                    w1.y = wz8[i, 1] * hei / 2 + y0;
                    w1.h = hei / 2;
                    w1.w = wei / 4;
                    break;
                case 9:
                    int[,] wz9 = new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 }, { 0, 2 }, { 1, 2 }, { 2, 2 } };
                    //c.Location = new System.Drawing.Point(wz9[i, 0] * wei / 3, wz9[i, 1] * hei / 3 + y0);
                    //c.Size = new System.Drawing.Size(wei / 3, hei / 3);
                    w1.x = wz9[i, 0] * wei / 3;
                    w1.y = wz9[i, 1] * hei / 3 + y0;
                    w1.h = hei / 3;
                    w1.w = wei / 3;
                    break;
                case 12:
                    int[,] wz12 = new int[,] { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 }, { 3, 1 }, { 0, 2 }, { 1, 2 }, { 2, 2 }, { 3, 2 } };
                    //c.Location = new System.Drawing.Point(wz12[i, 0] * wei / 4, wz12[i, 1] * hei / 3 + y0);
                    //c.Size = new System.Drawing.Size(wei / 4, hei / 3);
                    w1.x = wz12[i, 0] * wei / 4;
                    w1.y = wz12[i, 1] * hei / 3 + y0;
                    w1.h = hei / 3;
                    w1.w = wei / 4;
                    break;
                default:
                    break;
            }
            return w1;
        }
        public void spxshi()
        {
            try
            {
                TC.yx = false;

                //清除现有镜头
                this.Cursor = Cursors.WaitCursor;
                foreach (mjpeg.cameraW c in arc)
                {
                    c.mjpegSource.Yx = false;
                    c.Stop();
                    c.Dispose();
                }
                foreach (Vlc.DotNet.Forms.VlcControl c in arr)
                {
                    c.Stop();
                    c.Dispose();
                }
                ar.Clear();
                arc.Clear();
                arr.Clear();
                GC.Collect();
                int sm = getsm(jtss.Count);
                TC.yx = true;
                //显示
                int wei = this.Width;
                int hei = this.Height - 60;
                int y0 = 30;
                int i = 0;
                foreach (JT jt1 in jtss)
                {
                    if (i < sm)
                    {

                        bool rtsp = jt1.url.Trim().ToUpper().Substring(0, 4) == "RTSP";
                        if (rtsp)
                        {
                            Vlc.DotNet.Forms.VlcControl vlc = new Vlc.DotNet.Forms.VlcControl();
                            ((System.ComponentModel.ISupportInitialize)(vlc)).BeginInit();
                            this.SuspendLayout();

                            wz w1 = GetWZ(sm, i, wei, hei, 0, y0);
                            vlc.Location = new System.Drawing.Point(w1.x, w1.y);
                            vlc.Size = new System.Drawing.Size(w1.w, w1.h);
                            this.Controls.Add(vlc);
                            try
                            {
                            vlc.VlcLibDirectoryNeeded += vlc_VlcLibDirectoryNeeded;
                            ((System.ComponentModel.ISupportInitialize)(vlc)).EndInit();
                            this.ResumeLayout(false);
                           
                            vlc.Video.AspectRatio = vlc.Width.ToString() + ":" + vlc.Height.ToString();
                           
                                vlc.Play(new Uri(jt1.url));
                            }
                            catch(Exception  ex)
                            {
                                MessageBox.Show("联系计算机人员安装VLC控件:"+ex.ToString());
                            }
                            //vlc.Play(new Uri("rtsp://169.85.100.36/axis-media/media.amp?camera=1"));                           
                            ar.Add(vlc);
                            arr.Add(vlc);
                             vlc.Resize += vlc_Resize;                            
                        }
                        else
                        {
                            c = new cameraW();
                            wz w1 = GetWZ(sm, i, wei, hei, 0, y0);
                            c.Location = new System.Drawing.Point(w1.x, w1.y);
                            c.Size = new System.Drawing.Size(w1.w, w1.h);
                            c.Source = jt1.url;
                            this.Controls.Add(c);
                            ar.Add(c);
                            arc.Add(c);
                            c.Start();
                        }
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        void vlc_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
           e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
           // e.VlcLibDirectory = new DirectoryInfo(Path.Combine(@"c:\", "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
        }

        void vlc_Resize(object sender, EventArgs e)
        {
            Vlc.DotNet.Forms.VlcControl vlc = (Vlc.DotNet.Forms.VlcControl)sender;
            //throw new NotImplementedException();
            vlc.Video.AspectRatio = vlc.Width.ToString() + ":" + vlc.Height.ToString();
        }

        private cameraW c;
        #endregion


        #region sp_Resize
        private void sp_Resize(object sender, EventArgs e)
        {
            if (!chkXh.Checked)
            {
                int wei = this.Width;
                int hei = this.Height - 60;
                int sm = getsm(ar.Count);
                int i = 0;
                int y0 = 30;
                foreach (Control c in ar)
                {
                    wz w1 = GetWZ(sm, i, wei, hei, 0, y0);
                    c.Location = new System.Drawing.Point(w1.x, w1.y);
                    c.Size = new System.Drawing.Size(w1.w, w1.h);
                    i = i + 1;
                }
            }
        }
        #endregion


        /// <summary>
        /// 窗体顶层控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 顶层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void 底层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        //根据group 获取摄像头 private string filename = @"c:\spgroup.xml";
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayGroup();
        }

        /// <summary>
        /// 循环显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkXh_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void btnconfig_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            sp.frmConfig f = new sp.frmConfig();
            f.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cur >= jtss.Count)
                { cur = 0; }

                TC.yx = false;
                //清除现有镜头
                this.Cursor = Cursors.WaitCursor;
                foreach (mjpeg.cameraW c in ar)
                {
                    c.mjpegSource.Yx = false;
                    c.Stop();
                    c.Dispose();
                }
                ar.Clear();
                GC.Collect();

                TC.yx = true;
                mjpeg.cameraW c2 = new cameraW();
                c2.Dock = DockStyle.Fill;
                c2.Source = jtss[cur].url;

                this.Controls.Add(c2);
                ar.Add(c2);
                c2.Start();
                cur = cur + 1;
            }
            catch
            { }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void chkXh_CheckedChanged(object sender, EventArgs e)
        {
            DisplayGroup();
        }

        private void tcom_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ZCT.Data.MsData m = new ZCT.Data.MsData(conn);
                string cmd = "select    GROUP_name  from sp_group  order  by xh";
                groups = m.FillList(cmd);
                cmbGroup.DataSource = groups;
                //xx();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmW w = new frmW();
            w.TopMost = true;
            w.Show();
        }
    }
}