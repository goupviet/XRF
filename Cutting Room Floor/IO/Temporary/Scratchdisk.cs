using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Xrf.IO.Video;

namespace Xrf.IO.Temporary
{
    /// <summary>
    /// Represents an iterable temporary folder for storing frames and thumbnails.
    /// </summary>
    public class Scratchdisk : DependencyObject, IDisposable
    {
        /// <summary>
        /// The actual location of the scratchdisk.
        /// </summary>
        public string Location { get; set; }

        public static readonly DependencyProperty FramesProperty = DependencyProperty.Register("Frames", typeof(ObservableCollection<Frame>), typeof(Scratchdisk));

        /// <summary>
        /// A list of images with timecodes and frame indices.
        /// </summary>
        public ObservableCollection<Frame> Frames
        {
            get { return (ObservableCollection<Frame>)GetValue(FramesProperty); }
            set { SetValue(FramesProperty, value); }
        }

        private FileSystemWatcher _watcher;

        /// <summary>
        /// Creates a new scratchdisk in the temporary folder.
        /// </summary>
        //[PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        public Scratchdisk()
        {
            Location = GetTemporaryDirectory();
            Frames = new ObservableCollection<Frame>();

            // Watch for files added to the scratchdisk location and add them to the internal file table.
            _watcher = new FileSystemWatcher
            {
                Path = Location,
                Filter = @"*.jpeg",
                NotifyFilter = NotifyFilters.FileName,
                IncludeSubdirectories = false,
                InternalBufferSize = 65536
            };

            _watcher.Created += new FileSystemEventHandler(Watcher_Created);
            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Creates a new, randomly named, temporary folder to act as the scratchdisk location.
        /// </summary>
        /// <returns>The directory path.</returns>
        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".XRFScratchdisk");
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        /// Adds a file name to the internal file table.
        /// </summary>
        /// <param name="path">The filename to add.</param>
        private void AddFileReference(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            int framenumber = ParseFrameNumber(name);

            var newframe = new Frame(framenumber, path);
            Frames.Add(newframe);
        }

        private int ParseFrameNumber(string filename)
        {
            var resultString = Regex.Match(filename, @"\d+").Value;
            return Int32.Parse(resultString);
        }

        /// <summary>
        /// Estimates the size of the scratchdisk required.
        /// </summary>
        /// <param name="path">The movie file that will be used to populate the scratchdisk.</param>
        /// <returns>The approximate size of the scratchdisk in bytes.</returns>
        public static int EstimateSize(string path)
        {
            MediaInfo info = new MediaInfo();
            info.Open(path);
            var stream_size_str = info.Get(StreamKind.Video, 0, "StreamSize");
            info.Close();

            Int32 stream_size;
            Int32.TryParse(stream_size_str, out stream_size);

            return stream_size;
        }

        /// <summary>
        /// Deletes the scratchdisk temporary folder and clears the internal file table.
        /// </summary>
        public void Dispose()
        {
            Directory.Delete(Location);
            Frames.Clear();
            _watcher.Dispose();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => AddFileReference(e.FullPath)));
        }
    }
}
