﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Xrf.Imaging.Filters
{
    public static class ArithmeticBlending
    {
        public static Bitmap Blend(this Bitmap sourceBitmap, Bitmap blendBitmap, ColorCalculationType calculationType)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                    sourceBitmap.Width, sourceBitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);


            BitmapData blendData = blendBitmap.LockBits(new Rectangle(0, 0,
                                   blendBitmap.Width, blendBitmap.Height),
                                   ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);


            byte[] blendBuffer = new byte[blendData.Stride * blendData.Height];
            Marshal.Copy(blendData.Scan0, blendBuffer, 0, blendBuffer.Length);
            blendBitmap.UnlockBits(blendData);


            for (int k = 0; (k + 4 < pixelBuffer.Length) &&
                            (k + 4 < blendBuffer.Length); k += 4)
            {
                pixelBuffer[k] = Calculate(pixelBuffer[k],blendBuffer[k], calculationType);
                pixelBuffer[k + 1] = Calculate(pixelBuffer[k + 1],blendBuffer[k + 1], calculationType);
                pixelBuffer[k + 2] = Calculate(pixelBuffer[k + 2],blendBuffer[k + 2], calculationType);
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);


            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);


            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        public static byte Calculate(byte color1, byte color2, ColorCalculationType calculationType)
        {
            byte resultValue = 0;
            int intResult = 0;

            switch (calculationType)
            {
                case ColorCalculationType.Add:
                    intResult = color1 + color2;
                    break;

                case ColorCalculationType.Average:
                    intResult = (color1 + color2) / 2;
                    break;

                case ColorCalculationType.SubtractLeft:
                    intResult = color1 - color2;
                    break;

                case ColorCalculationType.SubtractRight:
                    intResult = color2 - color1;
                    break;

                case ColorCalculationType.Difference:
                    intResult = Math.Abs(color1 - color2);
                    break;

                case ColorCalculationType.Multiply:
                    intResult = (int)((color1 / 255.0 * color2 / 255.0) * 255.0);
                    break;

                case ColorCalculationType.Min:
                    intResult = Math.Min(color1, color2);
                    break;

                case ColorCalculationType.Max:
                    intResult = Math.Max(color1, color2);
                    break;

                case ColorCalculationType.Amplitude:
                    intResult = (int)(Math.Sqrt(color1 * color1 + color2 * color2)
                                                            / Math.Sqrt(2.0));
                    break;
            }

            if (intResult < 0)
            {
                resultValue = 0;
            }
            else if (intResult > 255)
            {
                resultValue = 255;
            }
            else
            {
                resultValue = (byte)intResult;
            }


            return resultValue;
        }
    }

    public enum ColorCalculationType
    {
        Average,
        Add,
        SubtractLeft,
        SubtractRight,
        Difference,
        Multiply,
        Min,
        Max,
        Amplitude
    }
}