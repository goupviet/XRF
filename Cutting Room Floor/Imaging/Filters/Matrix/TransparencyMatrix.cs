using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    public static class TransparencyMatrix
    {
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
