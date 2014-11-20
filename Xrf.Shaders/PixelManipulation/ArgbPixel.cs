namespace Xrf.Shaders.PixelManipulation
{
    /// <summary>Represents the ARGB value of a pixel.</summary>
    public class ArgbPixel
    {
        /// <summary>The blue channel value.</summary>
        public byte Blue = 0;

        /// <summary>The green channel value.</summary>
        public byte Green = 0;

        /// <summary>The red channel value.</summary>
        public byte Red = 0;

        /// <summary>The alpha channel value.</summary>
        public byte Alpha = 0;

        /// <summary>Default constructor.</summary>
        public ArgbPixel() { }

        /// <summary>Creates an instance from a BGRA byte array.</summary>
        /// <param name="colorComponents">BGRA byte array.</param>
        public ArgbPixel(byte[] colorComponents)
        {
            Blue = colorComponents[0];
            Green = colorComponents[1];
            Red = colorComponents[2];
            Alpha = colorComponents[3];
        }

        /// <summary>Creates a BGRA byte array.</summary>
        /// <returns>The BGRA byte array.</returns>
        public byte[] GetColorBytes()
        {
            return new byte[] { Blue, Green, Red, Alpha };
        }
    }
}
