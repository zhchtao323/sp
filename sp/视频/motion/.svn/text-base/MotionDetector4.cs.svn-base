// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace motion
{
	using System;
	using System.Drawing;
	using System.Reflection;

	using AForge.Imaging;
	using AForge.Imaging.Filters;

	/// <summary>
	/// MotionDetector4
	/// </summary>
	public class MotionDetector4 : IMotionDetector
	{
		private IFilter	grayscaleFilter = new GrayscaleBT709();
		private IFilter	pixellateFilter = new Pixellate();
		private Difference differenceFilter = new Difference();
		private IFilter thresholdFilter = new Threshold(15, 255);
		private MoveTowards moveTowardsFilter = new MoveTowards();

		private FiltersSequence	processingFilter1 = new FiltersSequence();
		private FiltersSequence	processingFilter2 = new FiltersSequence();

		private Bitmap	backgroundFrame;
		private int		counter = 0;

		private Bitmap[]	numbersBitmaps = new Bitmap[9];

		// Constructor
		public MotionDetector4()
		{
			processingFilter1.Add(grayscaleFilter);
			processingFilter1.Add(pixellateFilter);

			processingFilter2.Add(differenceFilter);
			processingFilter2.Add(thresholdFilter);

			// load numbers bitmaps
            //Assembly assembly = this.GetType().Assembly;

            //for (int i = 1; i <= 9; i++)
            //{
            //    numbersBitmaps[i - 1] = new Bitmap(assembly.GetManifestResourceStream(
            //        string.Format("motion.Resources.{0}.gif", i)));
            //}
		}

		// Reset detector to initial state
		public void Reset()
		{
			if (backgroundFrame != null)
			{
				backgroundFrame.Dispose();
				backgroundFrame = null;
			}
			counter = 0;
		}

		// Process new frame
		public void ProcessFrame(ref Bitmap image)
		{
			if (backgroundFrame == null)
			{
				// create initial backgroung image
				backgroundFrame = processingFilter1.Apply(image);

				// just return for the first time
				return;
			}

			Bitmap tmpImage;

			// apply the the first filters sequence
			tmpImage = processingFilter1.Apply(image);
		
			if (++counter == 2)
			{
				counter = 0;

				// move background towards current frame
				moveTowardsFilter.OverlayImage = tmpImage;
				Bitmap tmp = moveTowardsFilter.Apply(backgroundFrame);

				// dispose old background
				backgroundFrame.Dispose();
				backgroundFrame = tmp;
			}

			// set backgroud frame as an overlay for difference filter
			differenceFilter.OverlayImage = backgroundFrame;

			// apply the the second filters sequence
			Bitmap tmpImage2 = processingFilter2.Apply(tmpImage);
			tmpImage.Dispose();

			// get object rectangles
			Rectangle[] rects = BlobCounter.GetObjectRectangles(tmpImage2);
			tmpImage2.Dispose();

			// create graphics object from initial image
			Graphics g = Graphics.FromImage(image);

			using (Pen pen = new Pen(Color.Red, 1))
			{
				int n = 0;

				// draw each rectangle
				foreach (Rectangle rc in rects)
				{
					g.DrawRectangle(pen, rc);

					if ((n < 10) && (rc.Width > 15) && (rc.Height > 15))
					{
						g.DrawImage(numbersBitmaps[n], rc.Left, rc.Top, 7, 9);
						n++;
					}
				}
			}

			g.Dispose();
		}


        public bool ProcessFrame1( Bitmap image)
        {
            if (backgroundFrame == null)
            {
                // create initial backgroung image
                backgroundFrame = processingFilter1.Apply(image);

                // just return for the first time
                return false;
            }

            Bitmap tmpImage;

            // apply the the first filters sequence
            tmpImage = processingFilter1.Apply(image);

            if (++counter == 2)
            {
                counter = 0;

                // move background towards current frame
                moveTowardsFilter.OverlayImage = tmpImage;
                Bitmap tmp = moveTowardsFilter.Apply(backgroundFrame);

                // dispose old background
                backgroundFrame.Dispose();
                backgroundFrame = tmp;
            }

            // set backgroud frame as an overlay for difference filter
            differenceFilter.OverlayImage = backgroundFrame;

            // apply the the second filters sequence
            Bitmap tmpImage2 = processingFilter2.Apply(tmpImage);
            tmpImage.Dispose();

            // get object rectangles
            Rectangle[] rects = BlobCounter.GetObjectRectangles(tmpImage2);
            tmpImage2.Dispose();
         
            if (rects.Length>2)
            {
                return true;
            }
            else
            {
                return false;
            }

            // create graphics object from initial image
           
            //using (Pen pen = new Pen(Color.Red, 1))
            //{
            //    int n = 0;

            //     draw each rectangle
            //    foreach (Rectangle rc in rects)
            //    {
            //        g.DrawRectangle(pen, rc);

            //        if ((n < 10) && (rc.Width > 15) && (rc.Height > 15))
            //        {
            //            g.DrawImage(numbersBitmaps[n], rc.Left, rc.Top, 7, 9);
            //            n++;
            //        }
            //    }
            //}

            
        }
	}
}
