using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Shaders.Matrix
{
    /// <summary>Represents a matrix shader that inverts the image.</summary>
    public static class NegativeMatrix
    {
        /// <summary>Draws the matrix shader onto the source bitmap.</summary>
        /// <param name="sourceImage">The bitmap to apply the shader to.</param>
        /// <returns>The shaded bitmap.</returns>
        public static Bitmap Draw(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
                    {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                    });


            return ColorMatrixIO.ApplyColorMatrix(sourceImage, colorMatrix);
        }
    }
}
