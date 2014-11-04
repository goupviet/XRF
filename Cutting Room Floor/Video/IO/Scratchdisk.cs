﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrf.Video.IO
{
    /// <summary>
    /// Represents an iterable temporary folder for storing frames and thumbnails.
    /// </summary>
    public class Scratchdisk : IDisposable
    {
        /// <summary>
        /// The actual location of the scratchdisk.
        /// </summary>
        public string Location { get; set; }

        private List<string> _fileTable;

        /// <summary>
        /// Creates a new scratchdisk in the temporary folder.
        /// </summary>
        public Scratchdisk()
        {
            Location = GetTemporaryDirectory();
            _fileTable = new List<string>();
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
                return _fileTable[i];
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
        public void AddFileReference(string filename)
        {
            _fileTable.Add(filename);
        }

        /// <summary>
        /// Deletes the specified file and removes it's reference from the internal file table.
        /// </summary>
        /// <param name="filename">The filename to remove.</param>
        private void DeleteFile(string filename)
        {
            File.Delete(filename);
            _fileTable.Remove(filename);
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
            _fileTable.Clear();
        }
    }
}
