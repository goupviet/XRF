using System.Drawing;
using System.Drawing.Imaging;

namespace Xrf.Imaging.Filters.Matrix
{
    public static class SepiaMatrix
    {
        public static Bitmap Draw(Image sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
                {
                        new float[]{.393f, .349f, .272f, 0, 0},
                        new float[]{.769f, .686f, .534f, 0, 0},
                        new float[]{.189f, .168f, .131f, 0, 0},
                        new float[]{0, 0, 0, 1, 0},
                        new float[]{0, 0, 0, 0, 1}
                });


            return ColorMatrixIO.ApplyColorMatrix(sourceImage, colorMatrix);
        }
    }
}
