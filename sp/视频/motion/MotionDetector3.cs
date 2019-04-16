// Motion Detector
//
// Copyright © Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace motion
{
	using System;
	using System.Drawing;

	using AForge.Imaging;
	using AForge.Imaging.Filters;

	/// <summary>
	/// MotionDetector3
	/// </summary>
	public class MotionDetector3 : IMotionDetector
	{
		private IFilter	grayscaleFilter = new GrayscaleBT709();
		private IFilter	pixellateFilter = new Pixellate();
		private Difference differenceFilter = new Difference();
		private IFilter thresholdFilter = new Threshold(15, 255);
		private Dilatation dilatationFilter = new Dilatation();
		private IFilter edgesFilter = new Edges();
		private Merge mergeFilter = new Merge();

		private IFilter extrachChannel = new ExtractChannel(RGB.R);
		private ReplaceChannel replaceChannel = new ReplaceChannel(RGB.R);
		private MoveTowards moveTowardsFilter = new MoveTowards();

		private FiltersSequence	processingFilter1 = new FiltersSequence();
		private FiltersSequence	processingFilter2 = new FiltersSequence();

		private Bitmap	backgroundFrame;
		private int		counter = 0;

		// Constructor
		public MotionDetector3()
		{
			processingFilter1.Add(grayscaleFilter);
			processingFilter1.Add(pixellateFilter);

			processingFilter2.Add(differenceFilter);
			processingFilter2.Add(thresholdFilter);
			processingFilter2.Add(dilatationFilter);
			processingFilter2.Add(edgesFilter);
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

			// extract red channel from the original image
			Bitmap redChannel = extrachChannel.Apply(image);

			//  merge red channel with moving object borders
			mergeFilter.OverlayImage = tmpImage2;
			Bitmap tmpImage3 = mergeFilter.Apply(redChannel);
			redChannel.Dispose();
			tmpImage2.Dispose();

			// replace red channel in the original image
			replaceChannel.ChannelImage = tmpImage3;
			Bitmap tmpImage4 = replaceChannel.Apply(image);
			tmpImage3.Dispose();

			image.Dispose();
			image = tmpImage4;
		}
	}
}
