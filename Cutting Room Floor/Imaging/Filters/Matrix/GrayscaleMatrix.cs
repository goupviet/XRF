using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    /// <summary>Represents a grayscale matrix shader.</summary>
    public static class GrayscaleMatrix
    {
        /// <summary>Draws the matrix shader onto the source bitmap.</summary>
        /// <param name="sourceImage">The bitmap to apply the shader to.</param>
        /// <returns>The shaded bitmap.</returns>
        public static Bitmap Draw(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                        {
                            new float[]{.3f, .3f, .3f, 0, 0},
                            new float[]{.59f, .59f, .59f, 0, 0},
                            new float[]{.11f, .11f, .11f, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{0, 0, 0, 0, 1}
                        });


            return ColorMatrixIO.ApplyColorMatrix(sourceImage, colorMatrix);
        }
    }
}
