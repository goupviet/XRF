using ICSharpCode.AvalonEdit.Highlighting;
using System.IO;
using System.Windows;
using System.Xml;

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
            
        }

    }
}
