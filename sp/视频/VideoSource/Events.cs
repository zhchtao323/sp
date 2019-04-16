namespace videosource
{
	using System;
	using System.Drawing.Imaging;

	// NewFrame delegate
	//public delegate void CameraEventHandler(object sender, CameraEventArgs e);
    public delegate void CameraEventHandler(object sender, System.Drawing.Image e);
    public delegate void CameraErrorEventHandler(object sender, string e);
	/// <summary>
	/// Camera event arguments
	/// </summary>
	public class CameraEventArgs : EventArgs
	{
		//private System.Drawing.Bitmap bmp;
        private System.Drawing.Image bmp;

		// Constructor
		public CameraEventArgs(System.Drawing.Bitmap bmp)
		{
			this.bmp = bmp;
		}

		// Bitmap property
		public System.Drawing.Image Bitmap
		{
			get { return bmp; }
		}
	}
}