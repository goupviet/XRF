using System.Text.RegularExpressions;

namespace Xrf.Imaging.XSSL
{
    public class TokenDefinition
    {
        public TokenDefinition(string type, Regex regex) : this(type, regex, false) { }

        public TokenDefinition(string type, Regex regex, bool isIgnored)
        {
            Type = type;
            Regex = regex;
            IsIgnored = isIgnored;
        }

        public bool IsIgnored { get; set; }
        public Regex Regex { get; set; }
        public string Type { get; set; }
    }
}
