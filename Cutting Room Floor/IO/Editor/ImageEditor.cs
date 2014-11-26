using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Xrf.IO.Editor
{
    public class ImageEditor : DependencyObject
    {
        public static readonly DependencyProperty OriginalImageProperty = DependencyProperty.Register("OriginalImage", typeof(ImageSource), typeof(ImageEditor));
        public ImageSource OriginalImage
        {
            get { return (ImageSource)GetValue(OriginalImageProperty); }
            set { SetValue(OriginalImageProperty, value); }
        }

        public static readonly DependencyProperty EditedImageProperty = DependencyProperty.Register("EditedImage", typeof(ImageSource), typeof(ImageEditor));
        public ImageSource EditedImage
        {
            get { return (ImageSource)GetValue(EditedImageProperty); }
            set { SetValue(EditedImageProperty, value); }
        }
    }
}
