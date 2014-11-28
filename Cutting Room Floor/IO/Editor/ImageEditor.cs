using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xrf.IO.Temporary;
using Xrf.IO.Video;

namespace Xrf.IO.Editor
{
    public class ImageEditor : DependencyObject
    {
        public static readonly DependencyProperty OriginalImageProperty = DependencyProperty.Register("OriginalImage", typeof(Frame), typeof(ImageEditor));
        public Frame OriginalImage
        {
            get { return (Frame)GetValue(OriginalImageProperty); }
            set { SetValue(OriginalImageProperty, value); }
        }

        public static readonly DependencyProperty EditedImageProperty = DependencyProperty.Register("EditedImage", typeof(Frame), typeof(ImageEditor));
        public Frame EditedImage
        {
            get { return (Frame)GetValue(EditedImageProperty); }
            set { SetValue(EditedImageProperty, value); }
        }

        public ImageEditor(Frame toEdit)
        {
            OriginalImage = toEdit;
            EditedImage = toEdit;
        }
    }
}
