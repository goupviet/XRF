using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    /// <summary>Represents a matrix shader that alters the images transparency.</summary>
    public static class TransparencyMatrix
    {
        /// <summary>Draws the matrix shader onto the source bitmap.</summary>
        /// <param name="sourceImage">The bitmap to apply the shader to.</param>
        /// <param name="opacity">The opacity value to apply. (Between 0 and 1)</param>
        /// <returns>The shaded bitmap.</returns>
        public static Bitmap Draw(Image sourceImage, float opacity)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                        {
                            new float[]{1, 0, 0, 0, 0},
                            new float[]{0, 1, 0, 0, 0},
                            new float[]{0, 0, 1, 0, 0},
                            new float[]{0, 0, 0, opacity, 0},
                            new float[]{0, 0, 0, 0, 1}
                        });


            return ColorMatrixIO.ApplyColorMatrix(sourceImage, colorMatrix);
        }
    }
}
