using System.Drawing;

namespace Xrf.Shaders.Substitution
{
    /// <summary>Represents a colour substitution and replacement filter.</summary>
    public class ColorSubstitutionFilter
    {
        private int thresholdValue = 10;

        /// <summary>The threshold value to use during calculation.</summary>
        public int ThresholdValue
        {
            get { return thresholdValue; }
            set { thresholdValue = value; }
        }

        private Color sourceColor = Color.White;

        /// <summary>The colour to replace.</summary>
        public Color SourceColor
        {
            get { return sourceColor; }
            set { sourceColor = value; }
        }

        private Color newColor = Color.White;

        /// <summary>The replacement colour.</summary>
        public Color NewColor
        {
            get { return newColor; }
            set { newColor = value; }
        }
    }
}
