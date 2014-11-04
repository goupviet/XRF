using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    public static class CustomMatrix
    {
        public static Bitmap Draw(Image sourceImage, ColorMatrix matrix)
        {
            return ColorMatrixIO.ApplyColorMatrix(sourceImage, matrix);
        }
    }
}
