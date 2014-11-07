using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    /// <summary>Represents a custom, user-defined matrix shadery.</summary>
    public static class CustomMatrix
    {
        /// <summary>Draws the matrix shader onto the source bitmap.</summary>
        /// <param name="sourceImage">The bitmap to apply the shader to.</param>
        /// <param name="matrix">The ColorMatrix to apply.</param>
        /// <returns>The shaded bitmap.</returns>
        public static Bitmap Draw(Image sourceImage, ColorMatrix matrix)
        {
            return ColorMatrixIO.ApplyColorMatrix(sourceImage, matrix);
        }
    }
}
