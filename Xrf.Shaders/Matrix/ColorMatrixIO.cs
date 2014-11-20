using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Shaders
{
    /// <summary>Provides methods for drawing a ColorMatrix shader onto a bitmap.</summary>
    public static class ColorMatrixIO
    {
        /// <summary>Applies a ColorMatrix shader onto a Bitmap.</summary>
        /// <param name="sourceImage">The bitmap to apply the ColorMatrix shader to.</param>
        /// <param name="colorMatrix">The ColorMatrix to draw.</param>
        /// <returns>A shaded bitmap.</returns>
        public static Bitmap ApplyColorMatrix(Image sourceImage, ColorMatrix colorMatrix)
        {
            Bitmap bmp32BppSource = GetArgbCopy(sourceImage);
            Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
            {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);
            }

            bmp32BppSource.Dispose();

            return bmp32BppDest;
        }

        /// <summary>Converts a Bitmap to 32bpp Argb image.</summary>
        /// <param name="sourceImage">The image to convert.</param>
        /// <returns>A 32bpp ARGB representation of the source bitmap.</returns>
        private static Bitmap GetArgbCopy(Image sourceImage)
        {
            Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }

            return bmpNew;
        }

    }
}
