using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Windows.Resources;

namespace Xrf.StainedGlass.Editor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StainedGlassEditor : UserControl
    {
        public StainedGlassEditor()
        {
            var syntaxdef = new Uri(@"/Resources/XSSL.xshd", UriKind.Relative);

            using (var xshd_stream = Application.GetContentStream(syntaxdef).Stream)
            using (var xshd_reader = new XmlTextReader(xshd_stream))
            {
                textEditor.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);
            }

            InitializeComponent();
        }
    }
}
