using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Xrf.Shaders.PixelManipulation
{
    /// <summary>Implements LINQ pixel colour manipulation.</summary>
    public static class PixelManipulationIO
    {
        /// <summary>Create a list of ARGB data for each pixel in the source image and create a list representing them.</summary>
        /// <param name="sourceImage">The image to analyse.</param>
        /// <returns>List of ArgbPixel objects representing the bitmap's underlying pixel data.</returns>
        private static List<ArgbPixel> GetPixelListFromBitmap(Bitmap sourceImage)
        {
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0,
                        sourceImage.Width, sourceImage.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] sourceBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);
            sourceImage.UnlockBits(sourceData);

            List<ArgbPixel> pixelList = new List<ArgbPixel>(sourceBuffer.Length / 4);

            using (MemoryStream memoryStream = new MemoryStream(sourceBuffer))
            {
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                while (memoryStream.Position + 4 <= memoryStream.Length)
                {
                    ArgbPixel pixel = new ArgbPixel(binaryReader.ReadBytes(4));
                    pixelList.Add(pixel);
                }

                binaryReader.Close();
            }

            return pixelList;
        }

        /// <summary>Applies colour manipulation queries to the specified bitmap.</summary>
        /// <param name="sourceImage">The bitmap to modify.</param>
        /// <param name="swapType">The type of colour swap to perfom.</param>
        /// <param name="fixedValue">A value to fix a certain colour channel to.</param>
        /// <returns>The modified bitmap.</returns>
        public static Bitmap SwapColors(this Bitmap sourceImage, ColourSwapType swapType, byte fixedValue = 0)
        {
             List<ArgbPixel> pixelListSource = GetPixelListFromBitmap(sourceImage);
             List<ArgbPixel> pixelListResult = null;

             switch (swapType)
             {
                 case ColourSwapType.ShiftRight:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Green, 
                                                Green = t.Blue, 
                                                Blue = t.Red
                                            }).ToList();
                         break;

                 case ColourSwapType.ShiftLeft:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Blue, 
                                                Green = t.Red, 
                                                Blue = t.Green
                                            }).ToList();                        
                         break;

                 case ColourSwapType.SwapBlueAndRed:
                         pixelListResult = (from t in pixelListSource
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Blue, 
                                                Green = t.Green, 
                                                Blue = t.Red
                                            }).ToList();
                         break;
                    
                 case ColourSwapType.SwapBlueAndRedFixGreen:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Blue, 
                                                Green = fixedValue, 
                                                Blue = t.Red
                                            }).ToList();
                         break;

                 case ColourSwapType.SwapBlueAndGreen:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Red, 
                                                Green = t.Blue, 
                                                Blue = t.Green
                                            }).ToList();                        
                         break;

                 case ColourSwapType.SwapBlueAndGreenFixRed:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = fixedValue,
                                                Green = t.Blue, 
                                                Blue = t.Green
                                            }).ToList();
                         break;

                 case ColourSwapType.SwapRedAndGreen:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Green, 
                                                Green = t.Red, 
                                                Blue = t.Blue
                                            }).ToList();
                         break;

                 case ColourSwapType.SwapRedAndGreenFixBlue:
                         pixelListResult = (from t in pixelListSource 
                                            select new ArgbPixel 
                                            {
                                                Alpha = t.Alpha,
                                                Red = t.Green, 
                                                Green = t.Red, 
                                                Blue = fixedValue
                                            }).ToList();
                         break;
             }

             return GetBitmapFromPixelList(pixelListResult, sourceImage.Width, sourceImage.Height);
        }

        /// <summary>Recreates a Bitmap from a list of ARGB pixel data.</summary>
        /// <param name="pixelList">The list of ArgbPixel objects to create the bitmap from.</param>
        /// <param name="width">The output bitmap's width.</param>
        /// <param name="height">The output bitmap's height.</param>
        /// <returns>The bitmap representation of the pixel list.</returns>
        private static Bitmap GetBitmapFromPixelList(List<ArgbPixel> pixelList, int width, int height)
        {
            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                        ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] resultBuffer = new byte[resultData.Stride * resultData.Height];
            using (MemoryStream memoryStream = new MemoryStream(resultBuffer))
            {
                memoryStream.Position = 0;
                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

                foreach (ArgbPixel pixel in pixelList)
                {
                    binaryWriter.Write(pixel.GetColorBytes());
                }

                binaryWriter.Close();
            }

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        /// <summary>Flips an image using LINQ queries on it's pixel list representation.</summary>
        /// <param name="sourceImage">The bitmap to flip.</param>
        /// <returns>The flipped bitmap.</returns>
        public static Bitmap FlipPixels(this Bitmap sourceImage)
        {
            List<ArgbPixel> pixelList = GetPixelListFromBitmap(sourceImage);
            pixelList.Reverse();

            return GetBitmapFromPixelList(pixelList, sourceImage.Width, sourceImage.Height);
        }
    }
}