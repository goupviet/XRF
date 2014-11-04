using System.Drawing;

namespace Xrf.Imaging.Filters.Substitution
{
    public class ColorSubstitutionFilter
    {
        private int thresholdValue = 10;
        public int ThresholdValue
        {
            get { return thresholdValue; }
            set { thresholdValue = value; }
        }

        private Color sourceColor = Color.White;
        public Color SourceColor
        {
            get { return sourceColor; }
            set { sourceColor = value; }
        }

        private Color newColor = Color.White;
        public Color NewColor
        {
            get { return newColor; }
            set { newColor = value; }
        }
    }
}
