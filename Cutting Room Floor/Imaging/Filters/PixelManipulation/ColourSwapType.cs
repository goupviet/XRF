namespace Xrf.Imaging.Filters.PixelManipulation
{
    /// <summary>Defines colour swap operations.</summary>
    public enum ColourSwapType
    {
        /// <summary>From Blue to Green to Red to Alpha, channel values will be shifted to replace the one on the right, with Alpha looping to Blue.</summary>
        ShiftRight,

        /// <summary>From Blue to Green to Red to Alpha, channel values will be shifted to replace the one on the left, with Blue looping to Alpha.</summary>
        ShiftLeft,

        /// <summary>Swaps the blue and red channels' values.</summary>
        SwapBlueAndRed,

        /// <summary>Swaps the blue and red channels' values, but fixes the green channel's value at a specified value.</summary>
        SwapBlueAndRedFixGreen,

        /// <summary>Swaps the blue and green channels' values.</summary>
        SwapBlueAndGreen,

        /// <summary>Swaps the blue and green channels' values, but fixes the red channel's value at a specified value.</summary>
        SwapBlueAndGreenFixRed,

        /// <summary>Swaps the red and green channels' values.</summary>
        SwapRedAndGreen,

        /// <summary>Swaps the red and green channels' values, but fixes the blue channel's value at a specified value.</summary>
        SwapRedAndGreenFixBlue
    }
}
