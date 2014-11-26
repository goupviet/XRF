using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Xrf.Shaders.Substitution
{
    /// <summary>Provides methods for substituting a certain colour in a bitmap with another.</summary>
    public static class SubstitutionIO
    {
        /// <summary>Applies a substitution filter to a bitmap.</summary>
        /// <param name="sourceBitmap">The source bitmap.</param>
        /// <param name="filterData">The substitution filter.</param>
        /// <returns>The shaded bitmap.</returns>
        public static Bitmap SubstituteColour(Bitmap sourceBitmap, ColorSubstitutionFilter filterData)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);


            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                                                          ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                                                          ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];
            Marshal.Copy(sourceData.Scan0, resultBuffer, 0, resultBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            byte sourceRed = 0, sourceGreen = 0, sourceBlue = 0, sourceAlpha = 0;
            int resultRed = 0, resultGreen = 0, resultBlue = 0;

            byte newRedValue = filterData.NewColor.R;
            byte newGreenValue = filterData.NewColor.G;
            byte newBlueValue = filterData.NewColor.B;

            byte redFilter = filterData.SourceColor.R;
            byte greenFilter = filterData.SourceColor.G;
            byte blueFilter = filterData.SourceColor.B;

            byte minValue = 0;
            byte maxValue = 255;


            for (int k = 0; k < resultBuffer.Length; k += 4)
            {
                sourceAlpha = resultBuffer[k + 3];


                if (sourceAlpha != 0)
                {
                    sourceBlue = resultBuffer[k];
                    sourceGreen = resultBuffer[k + 1];
                    sourceRed = resultBuffer[k + 2];


                    if ((sourceBlue < blueFilter + filterData.ThresholdValue &&
                            sourceBlue > blueFilter - filterData.ThresholdValue) &&


                        (sourceGreen < greenFilter + filterData.ThresholdValue &&
                            sourceGreen > greenFilter - filterData.ThresholdValue) &&


                        (sourceRed < redFilter + filterData.ThresholdValue &&
                            sourceRed > redFilter - filterData.ThresholdValue))
                    {
                        resultBlue = blueFilter - sourceBlue + newBlueValue;


                        if (resultBlue > maxValue)
                        { resultBlue = maxValue; }
                        else if (resultBlue < minValue)
                        { resultBlue = minValue; }


                        resultGreen = greenFilter - sourceGreen + newGreenValue;


                        if (resultGreen > maxValue)
                        { resultGreen = maxValue; }
                        else if (resultGreen < minValue)
                        { resultGreen = minValue; }


                        resultRed = redFilter - sourceRed + newRedValue;


                        if (resultRed > maxValue)
                        { resultRed = maxValue; }
                        else if (resultRed < minValue)
                        { resultRed = minValue; }


                        resultBuffer[k] = (byte)resultBlue;
                        resultBuffer[k + 1] = (byte)resultGreen;
                        resultBuffer[k + 2] = (byte)resultRed;
                        resultBuffer[k + 3] = sourceAlpha;
                    }
                }
            }


            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        /// <summary>Creates a 32bpp ARGB Copy of a Bitmap.</summary>
        /// <param name="sourceBitmap">The source bitmap.</param>
        /// <returns>A 32bpp ARGB representation of the source bitmap.</returns>
        public static Bitmap Format32bppArgbCopy(this Bitmap sourceBitmap)
        {
            Bitmap copyBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphicsObject = Graphics.FromImage(copyBitmap))
            {
                graphicsObject.CompositingQuality = CompositingQuality.HighQuality;
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                graphicsObject.DrawImage(sourceBitmap, new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                 new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), GraphicsUnit.Pixel);
            }


            return copyBitmap;
        }
    }
}
