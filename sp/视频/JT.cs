using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
namespace mjpeg
{   
    enum VideoType{Axis,Pannic,Sonny}
    public class JT
    {
        const string cgi = @":80/axis-cgi/mjpg/video.cgi?camera=";
        const string http = @"http://169.85.100.";
        public string url
        { get; set; }
        //public  string url
        //{
        //    get
        //    {
        //        if (wd.Trim() == "")
        //        {
        //            return http + iP.Trim() + cgi + hl.Trim();
        //        }
        //        else
        //        {
        //            return @"http://169.85."+wd+"." + iP.Trim() + cgi + hl.Trim();
        //        }

        //    }
        //}

        private int xh = 0;

        public int Xh
        {
            get { return xh; }
            set { xh = value; }
        }
        private string group_name;
        /// <summary>
        /// 组名称
        /// </summary>
        public string Group_name
        {
            get { return group_name; }
            set { group_name = value; }
        }

        private string iP;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get { return iP; }
            set { iP = value; }
        }

        private string hl;
        //回路
        public string Hl
        {
            get { return hl; }
            set { hl = value; }
        }
        private string mc;
        /// <summary>
        /// 名称
        /// </summary>
        public string Mc
        {
            get { return mc; }
            set { mc = value; }
        }
        private VideoType lx;
        /// <summary>
        /// 镜头类型
        /// </summary>
        internal VideoType Lx
        {
            get { return lx; }
            set { lx = value; }
        }
        private string wd = "100";

        public string Wd
        {
            get { return wd; }
            set { wd = value; }
        }
        
    }
   
}
