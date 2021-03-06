// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace videosource
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Threading;
	using System.Net;
    using System.Net.Sockets;
    using System.Net.NetworkInformation;
	/// <summary>
	/// MJPEGSource - MJPEG stream support
	/// </summary>
	public class MJPEGSource : IVideoSource
	{
		public string	source;
		private string	login = null;
		private string	password = null;
		private object	userData = null;
		private int		framesReceived;
		private int		bytesReceived;
		
		private const int	bufSize = 512 * 1024;	// buffer size
		private const int	readSize = 1024;		// portion size to read

		private Thread	thread = null;		
		// new frame event
		public event CameraEventHandler NewFrame;
        public event CameraErrorEventHandler ErrEvent;

        private string err = "";

        public string Err
        {
            get { return err; }
            //set { err = value; }
        }
		// VideoSource property
		public string VideoSource
		{
			get { return source; }
			set
			{
				source = value;
                Stop();
                Start();
			}
		}
		// Login property
		public string Login
		{
			get { return login; }
			set { login = value; }
		}
		// Password property
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		// FramesReceived property
		public int FramesReceived
		{
			get
			{
				int frames = framesReceived;
				framesReceived = 0;
				return frames;
			}
		}
		// BytesReceived property
		public int BytesReceived
		{
			get
			{
				int bytes = bytesReceived;
				bytesReceived = 0;
				return bytes;
			}
		}
		// UserData property
		public object UserData
		{
			get { return userData; }
			set { userData = value; }
		}
		// Get state of the video source thread
		public bool Running
		{
			get
			{
               if (yx)
                { return true; }
                else
				return false;
			}
		}



		// Constructor
		public MJPEGSource()
		{
           System.Net.ServicePointManager.DefaultConnectionLimit = 1000000;
          // ServicePointManager.DefaultConnectionLimit = ServicePointManager.DefaultPersistentConnectionLimit;

		}

		// Start work
		public void Start()
		{
			if (thread == null)
			{
				framesReceived = 0;
				bytesReceived = 0;
				// create and start new thread
				thread = new Thread(new ThreadStart(WorkerThread));
				thread.Name = source;
                thread.IsBackground = true;
                yx = true;
				thread.Start();
			}
		}

		// Signal thread to stop work
		public void SignalToStop()
		{
			// stop thread
			if (thread != null)
			{
				// signal to stop
                //stopEvent.Set();
			}
		}

		// Wait for thread stop
		public void WaitForStop()
		{
			if (thread != null)
			{
				// wait for thread stop
                thread.Join();
				Free();
			}
		}

		// Abort thread
		public void Stop()
		{
     
            yx = false;
            //Thread.Sleep(100);
            //thread.Abort();
            // wait for thread stop
          //  thread.Join();
   
		}

		// Free resources
		private void Free()
		{
			thread = null;
            GC.Collect();
		}
        private int waitTime =100;

        public int WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; }
        }
        private Boolean IsExistC()
        {
            try
            {
                if (source.Length > 2)
                {
                    string ip = source.Substring(source.IndexOf("//") + 2, source.IndexOf(":80") - 7);
                    Ping p = new Ping();
                    PingReply r = p.Send(ip, 500);
                    if (r.Status == IPStatus.Success)
                    {    return true;      }
                    else
                    {    return false;      }
                }
                else
                {     return false;   }
            }
            catch
            {     return false;      }
        }

        private Boolean yx = true;

        public Boolean Yx
        {
            get { return yx; }
            set { yx = value; }
        }
		// Thread entry point
		public void WorkerThread()
		{
            byte[] delimiter = null;
            byte[] delimiter2 = null;
            byte[] boundary = null;
            int boundaryLen = 0;
            int delimiterLen = 0;
            int delimiter2Len = 0;
            int read = 0;
            int todo = 0;
            int total = 0;
            int pos = 0;
            int align = 1;
            int start = 0;
            int stop = 0;
            ASCIIEncoding encoding;
            byte[] buffer = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
           // MemoryStream
            Stream stream = null;
            //byte[] mmsb = new byte[50000];//1024000
            byte[] mmsb = new byte[100];//1024000
            MemoryStream mms = new MemoryStream(mmsb);
            //Bitmap bmp;
            Image bmp;
            string ct = "";
            while (yx & mjpeg.TC.yx)
            {
                delimiter = null;
                delimiter2 = null;
                boundary = null;
                 boundaryLen=0;
                 delimiterLen = 0;
                 delimiter2Len = 0;
                 read=0;
                 todo = 0;
                 total = 0;
                 pos = 0;
                 align = 1;  // align                //  1 = searching for image start                //  2 = searching for image end
                 start = 0;
                 stop = 0;
                 req = null;
                 resp = null;
                 stream = null;
                 GC.Collect();
                 if (IsExistC())
                 {
                     // get boundary
                     encoding = new ASCIIEncoding();
                     buffer = new byte[bufSize];	// buffer to read stream
                     ct = "";
                     mmsb = new byte[50000];
                     // create request
                     try
                     {
                         req = null;
                         resp = null;
                         lock (this)
                         {
                            // source = source + "&compression=30&resolution=2CIF&date=0";
                             req = (HttpWebRequest)WebRequest.Create(source);
                             req.Timeout = 50000;// 1000;// 50000; //已重写。获取或设置 GetResponse 和 GetRequestStream 方法的超时值。
                             req.ReadWriteTimeout = 20000;//1000
                             mms = new MemoryStream(mmsb);
                             Thread.Sleep(50);
                             //// set login and password
                             //if ((login != null) && (password != null) && (login != ""))
                                 req.Credentials = new NetworkCredential("ttsj", "ttsjfc");                        
                             // get response
                             resp = (HttpWebResponse)req.GetResponse();
                             if (resp.StatusCode != HttpStatusCode.OK)
                             { throw new ApplicationException("服务器响应不正确"); }
                             // check content type
                             ct = resp.ContentType;
                             //ct = req.ContentType;
                             if (ct.IndexOf("multipart/x-mixed-replace") == -1)
                                 throw new ApplicationException("Invalid URL");
                             // get response stream
                             stream = resp.GetResponseStream();
                             if (ct.IndexOf("boundary=", 0) >= 0)
                             {
                                 boundary = encoding.GetBytes(ct.Substring(ct.IndexOf("boundary=", 0) + 9));
                             }
                             boundaryLen = boundary.Length;                           
                         }
                     }
                     catch (Exception ex)
                     {
                         System.Diagnostics.Debug.Write(" mjpeg1:" + ex.ToString() + source);
                         err = ex.ToString();
                         if (ErrEvent != null)
                         { ErrEvent(this, err); }

                         if (resp != null)
                         {       resp.Close();   }
                         if (req != null)
                         {        req.Abort();    }
                         continue;
                     }
                     int cw0 = 0;
                     #region //x  while
                     while (yx)
                     {
                         try
                         {
                             // check total read
                             if (total > bufSize - readSize)
                             {   total = pos = todo = 0;  }
                             Thread.Sleep(waitTime);  //2005-11  //10  2006-4-26                  
                             read = stream.Read(buffer, total, buffer.Length - total);
                             if (read == 0)
                                 cw0 = cw0 + 1;
                             if (cw0 > 10)
                             {
                                 cw0 = 0;
                                 break;
                             }
                             //continue;
                             total += read;
                             todo += read;

                             // increment received bytes counter
                             bytesReceived += read;
                             // does we know the delimiter ?
                             if (delimiter == null)
                             {
                                 // find boundary
                                 pos = ByteArrayUtils.Find(buffer, boundary, pos, todo);
                                 if (pos == -1)
                                 {                                     // was not found
                                     todo = boundaryLen - 1;
                                     pos = total - todo;
                                     continue;
                                 }
                                 todo = total - pos;
                                 if (todo < 2)
                                     continue;
                                 // check new line delimiter type
                                 if (buffer[pos + boundaryLen] == 10)
                                 {
                                     delimiterLen = 2;
                                     delimiter = new byte[2] { 10, 10 };
                                     delimiter2Len = 1;
                                     delimiter2 = new byte[1] { 10 };
                                 }
                                 else
                                 {
                                     delimiterLen = 4;
                                     delimiter = new byte[4] { 13, 10, 13, 10 };
                                     delimiter2Len = 2;
                                     delimiter2 = new byte[2] { 13, 10 };
                                 }

                                 pos += boundaryLen + delimiter2Len;
                                 todo = total - pos;
                             }
                             if (align == 1)
                             {
                                 start = ByteArrayUtils.Find(buffer, delimiter, pos, todo);
                                 if (start != -1)
                                 {
                                     // found delimiter
                                     start += delimiterLen;
                                     pos = start;
                                     todo = total - pos;
                                     align = 2;
                                 }
                                 else
                                 {
                                     // delimiter not found
                                     todo = delimiterLen - 1;
                                     pos = total - todo;
                                 }
                             }
                             //  continue;
                             // search for image end
                             while ((align == 2) && (todo >= boundaryLen))
                             {
                                 stop = ByteArrayUtils.Find(buffer, boundary, pos, todo);
                                 if (stop != -1)
                                 {
                                     pos = stop;
                                     todo = total - pos;
                                     // increment frames counter
                                     framesReceived++;
                                     // continue;
                                     // image at stop
                                     if (NewFrame != null)
                                     {

                                         if (mmsb.Length != stop - start)
                                         {
                                             mmsb = new byte[stop - start];
                                             //Array.Copy(buffer, start, mmsb, 0, stop - start);
                                             Buffer.BlockCopy(buffer, start, mmsb, 0, stop - start);
                                             mms = new MemoryStream(mmsb);
                                         }
                                         else
                                         {   
                                             //Array.Copy(buffer, start, mmsb, 0, stop - start);
                                             Buffer.BlockCopy(buffer, start, mmsb, 0, stop - start);       
                                         }
                                         bmp = Image.FromStream(mms, false, false);
                                         // notify client                                        
                                         NewFrame(this, bmp);                                       
                                     }                                     
                                     // shift array
                                     pos = stop + boundaryLen;
                                     todo = total - pos;
                                     //Array.Copy(buffer, pos, buffer, 0, todo);
                                     Buffer.BlockCopy(buffer, pos, buffer, 0, todo);
                                     total = todo;
                                     pos = 0;
                                     align = 1;
                                 }
                                 else
                                 {
                                     // delimiter not found
                                     todo = boundaryLen - 1;
                                     pos = total - todo;
                                 }
                             }
                         }
                         catch (Exception ex3)
                         {
                             System.Diagnostics.Debug.Write(" mjpeg2::" + ex3.ToString() + source);
                             err = ex3.ToString();
                             if(ErrEvent!=null)
                             { ErrEvent(this, err); }
                             if (resp != null)
                             {
                                 resp.Close();
                             }
                             if (req != null)
                             {
                                 req.Abort();
                             }
                             break;
                         }
                     }
                     #endregion //xwhile

                 }
                 else
                 {
                     err = "IP地址不通";          
                     Thread.Sleep(50);                     
                 }
	         }//while
		}//method
	}  // class
}//namespace
