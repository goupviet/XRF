using System;
using System.Collections.Generic;
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
using Fluent;
using Xrf.ViewModels;

namespace Xrf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            System.Diagnostics.Debug.WriteLine("HELLO");

            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.InnerException);
            }
            DataContext = new EditorViewModel();
        }
    }
}
