using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static readonly DependencyProperty FileListProperty = DependencyProperty.Register("FileList", 
            typeof(List<string>), typeof(Scratchdisk));

        public List<string> FileList
        {
            get { return (List<string>)GetValue(FileListProperty); }
            set { SetValue(FileListProperty, value); }
        }

        public event ScratchdiskFileAddedEventHandler FileAdded;
        public event ScratchdiskFileDeletedEventHandler FileDeleted;

        private FileSystemWatcher _watcher;

        /// <summary>
        /// Creates a new scratchdisk in the temporary folder.
        /// </summary>
        public Scratchdisk()
        {
            Location = GetTemporaryDirectory();
            FileList = new List<string>();

            // Watch for files added to the scratchdisk location and add them to the internal file table.
            _watcher = new FileSystemWatcher
            {
                Path = Location,
                Filter = @"*.jpg",
                NotifyFilter = NotifyFilters.CreationTime,
                IncludeSubdirectories = true,
            };

            _watcher.Created += new FileSystemEventHandler(Watcher_Created);
            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Provides access to each file dependent on file table index.
        /// </summary>
        /// <param name="i">File table index.</param>
        /// <returns>The file path for the file.</returns>
        public string this[int i]
        {
            get
            {
                return FileList[i];
            }
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
        /// <param name="filename">The filename to add.</param>
        private void AddFileReference(string filename)
        {
            FileList.Add(filename);
            var e = new ScratchdiskFileEventArgs(filename, ScratchdiskFileOperations.Added);

            OnFileAdded(e);
        }

        /// <summary>
        /// Deletes the specified file and removes it's reference from the internal file table.
        /// </summary>
        /// <param name="filename">The filename to remove.</param>
        private void DeleteFile(string filename)
        {
            File.Delete(filename);
            FileList.Remove(filename);

            var e = new ScratchdiskFileEventArgs(filename, ScratchdiskFileOperations.Deleted);
            OnFileDeleted(e);
        }

        /// <summary>
        /// Estimates the size of the scratchdisk required. Usually about 80% of the original file size, or 10% if the scratchdisk uses 1/8 frame-size thumbnails.
        /// Uses MediaInfo to get the size of the video stream, disregards the audio stream.
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
            FileList.Clear();
            _watcher.Dispose();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            AddFileReference(e.FullPath);
        }

        protected virtual void OnFileAdded(ScratchdiskFileEventArgs e)
        {
            if (FileAdded != null)
                FileAdded(this, e);
        }

        protected virtual void OnFileDeleted(ScratchdiskFileEventArgs e)
        {
            if (FileDeleted != null)
                FileDeleted(this, e);
        }

    }
}
