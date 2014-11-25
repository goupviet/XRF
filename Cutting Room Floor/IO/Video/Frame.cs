using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Xrf.IO.Video
{
    public class Frame
    {
        public int Index { get; set; }
        public string ImageUrl { get; set; }

        public Frame(int index, string url)
        {
            Index = index;
            ImageUrl = url;
        }
    }
}
