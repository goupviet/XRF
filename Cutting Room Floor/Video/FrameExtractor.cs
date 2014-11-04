using Xrf.Video.IO;
using System;
using System.Diagnostics;
using System.IO;

namespace Xrf.Video
{
    public class FrameExtractor
    {
        private string _input;
        private Scratchdisk _output;
        private string _ffmpeg;
        private ExtractionMode _mode;

        public event ExtractionStartedEventHandler ExtractionStarted;
        public event ExtractionCompleteEventHandler ExtractionComplete;

        public FrameExtractor(string path, Scratchdisk output, ExtractionMode mode = ExtractionMode.Frames)
        {
            _input = path;
            _output = output;
            _ffmpeg = FFmpeg.GetLocalInstance();
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
                string arguments;

                if (_mode == ExtractionMode.Thumbnails)
                {
                    string filename_template = "thumb-%3d.jpeg";
                    string output_template = Path.Combine(_output.Location, filename_template);
                    string arguments_template = "-i \"{0}\" -s {1} \"{2}\"";

                    arguments = string.Format(arguments_template, _input, "sqcif", output_template);
                }
                else
                {
                    // Frames, ignore -s sqcif flag
                    string filename_template = "frame-%3d.jpeg";
                    string output_template = Path.Combine(_output.Location, filename_template);
                    string arguments_template = "-i \"{0}\" \"{1}\"";

                    arguments = string.Format(arguments_template, _input, output_template);
                }

                Process extract = new Process()
                {
                    EnableRaisingEvents = true
                };

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

    public enum ExtractionMode
    {
        Frames,
        Thumbnails
    }
}
