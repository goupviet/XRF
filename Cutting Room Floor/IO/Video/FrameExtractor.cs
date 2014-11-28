using System;
using System.Diagnostics;
using System.IO;
using Xrf.IO.Temporary;

namespace Xrf.IO.Video
{
    public delegate void ExtractionStartedEventHandler(object sender, EventArgs e);
    public delegate void ExtractionCompleteEventHandler(object sender, EventArgs e);

    public class FrameExtractor
    {
        private string _input;
        private Scratchdisk _output;
        private string _ffmpeg;
        private FrameExtractionMode _mode;

        public event ExtractionStartedEventHandler ExtractionStarted;
        public event ExtractionCompleteEventHandler ExtractionComplete;

        public FrameExtractor(string path, Scratchdisk output, FrameExtractionMode mode = FrameExtractionMode.Frames)
        {
            _input = path;
            _output = output;
            _ffmpeg = Properties.Settings.Default.FFMpegLocation;
            _mode = mode;
        }

        public void Extract()
        {
            if ((_ffmpeg != null) && !File.Exists(_ffmpeg))
            {
                // FFmpeg was not found, frame extraction cannot continue.
                throw new FileNotFoundException("FFmpeg not found", "ffmpeg");
            }

            try
            {
                string filename_template = "%3d.jpeg";
                string output_template = Path.Combine(_output.Location, filename_template);
                string arguments_template = "-i \"{0}\" -s {1} \"{2}\"";
                string scale_flag = (_mode == FrameExtractionMode.Thumbnails) ? "sqcif" : "wxh";

                string arguments = string.Format(arguments_template, _input, scale_flag, output_template);

                Process extract = new Process() { EnableRaisingEvents = true };
                extract.Exited += (sender, e) => OnExtractionCompleted(e);

                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    FileName = "\"" + _ffmpeg + "\"",
                    Arguments = arguments
                };

                extract.StartInfo = psi;

                OnExtractionStarted(EventArgs.Empty);
                extract.Start();
            }
            catch (Exception)
            {

            }
        }

        protected virtual void OnExtractionStarted(EventArgs e)
        {
            if (ExtractionStarted != null)
            {
                ExtractionStarted(this, e);
            }
        }

        protected virtual void OnExtractionCompleted(EventArgs e)
        {
            if (ExtractionComplete != null)
            {
                ExtractionComplete(this, e);
            }
        }
    }

    public enum FrameExtractionMode
    {
        Frames,
        Thumbnails
    }
}
