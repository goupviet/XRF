using ICSharpCode.AvalonEdit.Highlighting;
using Xrf.Imaging.XSSL;
using System.IO;
using System.Windows;
using System.Xml;
using System;

namespace XRSLTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IHighlightingDefinition customHighlighting;
            using (Stream s = new MemoryStream(Properties.Resources.XSSL))
            {
                if (s == null)
                    throw new FileNotFoundException("XSSL.xshd");

                using (XmlReader xr = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(xr, HighlightingManager.Instance);
                }
            }
            
            HighlightingManager.Instance.RegisterHighlighting("XRF Shader Sequencing Language", new string[] { ".xssl" }, customHighlighting);

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tokens = (new XsslLexer()).Tokenize("scale 0.5\ntransparency 0.25");
            foreach (var token in tokens)
            {
                Editor.Document.Insert(Editor.Document.Text.Length - 1, token + Environment.NewLine);
            }
        }

    }
}
