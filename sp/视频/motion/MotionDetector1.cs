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
	/// MotionDetector1
	/// </summary>
	public class MotionDetector1 : IMotionDetector
	{
		private IFilter	grayscaleFilter = new GrayscaleBT709();
		private Difference differenceFilter = new Difference();
		private IFilter thresholdFilter = new Threshold(15, 255);
		private IFilter erosionFilter = new Erosion();
		private Merge mergeFilter = new Merge();

		private IFilter extrachChannel = new ExtractChannel(RGB.R);
		private ReplaceChannel replaceChannel = new ReplaceChannel(RGB.R);

		private FiltersSequence	processingFilter = new FiltersSequence();

		private Bitmap	backgroundFrame;

		// Constructor
		public MotionDetector1()
		{
			processingFilter.Add(differenceFilter);
			processingFilter.Add(thresholdFilter);
			processingFilter.Add(erosionFilter);
		}

		// Reset detector to initial state
		public void Reset()
		{
			if (backgroundFrame != null)
			{
				backgroundFrame.Dispose();
				backgroundFrame = null;
			}
		}

		// Process new frame
		public void ProcessFrame(ref Bitmap image)
		{
			if (backgroundFrame == null)
			{
				// create initial backgroung image
				backgroundFrame = grayscaleFilter.Apply(image);

				// just return for the first time
				return;
			}

			Bitmap tmpImage;

			// apply the grayscale file
			tmpImage = grayscaleFilter.Apply(image);

			// set backgroud frame as an overlay for difference filter
			differenceFilter.OverlayImage = backgroundFrame;

			// apply the the filters sequence
			Bitmap tmpImage2 = processingFilter.Apply(tmpImage);

			// dispose old background
			backgroundFrame.Dispose();
			// set backgound to current
			backgroundFrame = tmpImage;

			// extract red channel from the original image
			Bitmap redChannel = extrachChannel.Apply(image);

			//  merge red channel with moving object
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
