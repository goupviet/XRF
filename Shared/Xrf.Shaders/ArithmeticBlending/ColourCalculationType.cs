﻿namespace Xrf.Shaders.ArithmeticBlending
{
    /// <summary>Defines arithmetic blending operations.</summary>
    public enum ColourCalculationType
    {
        Average,
        Add,
        SubtractLeft,
        SubtractRight,
        Difference,
        Multiply,
        Min,
        Max,
        Amplitude
    }
}