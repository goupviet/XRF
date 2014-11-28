using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrf.IO.Video
{
    public class Movie
    {
        public string Filename { get; private set; }
        public int FPS { get; private set; }
        public int Duration { get; private set; }

        public Movie(string filepath)
        {
            Filename = filepath;
            FPS = GetMediaInfoParameter("FrameRate");
            Duration = GetMediaInfoParameter("Duration");
        }

        private int GetMediaInfoParameter(string parameter)
        {
            MediaInfo info = new MediaInfo();
            info.Open(Filename);
            var str = info.Get(StreamKind.Video, 0, parameter);
            info.Close();

            Int32 val;
            Int32.TryParse(str, out val);

            return val;
        }
    }
}
