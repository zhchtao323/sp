using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
public enum Fx { left, right, up, down, far, near, autoiris };
namespace videosource
{
	class Ptz
	{
		private string url = @"http://169.85.100.210:80/axis-cgi/mjpg/video.cgi?camera=2";

		public string Url
		{
			get { return url; }
			set { url = value; }
		}
		private string login ="sjfc";

		public string Login
		{
			get { return login; }
			set { login = value; }
		}
		private string password = "sjfc";

		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		private Fx f = Fx.left;

		public Fx F
		{
			get { return f; }
			set { f = value; }
		}
		public void Work()
		{ 
			Thread thr =new Thread(new ThreadStart(PtzFx));
			thr.Start();
		}
		//ÒÆ¶¯ÔÆÌ¨¿ØÖÆ
		public   void PtzFx()
		{
			// http://<servername>/axis-cgi/com/ptz.cgi?<parameter>=<value>
			//  move=<string> home,up,down,left,right,upleft,upright,downleft,downright			
			//zoomrel :tele,wide,telemax,telemin Adjusts the zoom gradually. 
			//focusrel :far,farmore,near,nearmore
			//http://169.85.100.219:80/axis-cgi/mjpg/video.cgi?camera=1
			string urlok = url.Replace("/mjpg/video.cgi?", "/com/ptz.cgi?");
			switch (f)
			{
				case Fx.left:
					urlok = urlok + "&move=left";
					break;
				case Fx.right:
					urlok = urlok + "&move=right";
					break;
				case Fx.up:
					urlok = urlok + "&move=up";
					break;
				case Fx.down:
					urlok = urlok + "&move=down";
					break;
				case Fx.far:
					urlok = urlok + "&rzoom=500";//-2500
					break;
				case Fx.near:
					urlok = urlok + "&rzoom=-500";//2500
					break;
				case Fx.autoiris:
					urlok = urlok + "&autoiris=on";//2500
					break;					
				default:
					break;
			}
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlok);
			// set login and password
			if ((login != null) && (password != null) && (login != ""))
				req.Credentials = new NetworkCredential(login, password);
            req.Timeout = 10000;
			// get response zoomrel3
            WebResponse resp;
            try
            {
                resp = req.GetResponse();
                resp.Close();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                //throw new Exception(ex.Message);
            }
		}
	}
}
