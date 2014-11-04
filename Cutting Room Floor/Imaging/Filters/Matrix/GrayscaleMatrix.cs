using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    public static class GrayscaleMatrix
    {
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
