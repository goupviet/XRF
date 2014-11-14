using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xrf.Logging;
using Xrf.Video;
using Xrf.Video.IO;

namespace Xrf.ViewModels
{
    public class EditorViewModel : DependencyObject
    {
        private string MovieFilePath;
        private Scratchdisk ThumbnailScratchdisk;
        private Log EventLog;
        private FileSystemWatcher ThumbnailFsWatcher;
        private int FramesPerSecond;
        List<string> pathsTEST;

        private ObservableCollection<BitmapImage> _thumbnailCollection;
        public ObservableCollection<BitmapImage> ThumbnailCollection
        {
            get { return _thumbnailCollection; }
        }

        public static readonly DependencyProperty CurrentFrameProperty = DependencyProperty.Register("CurrentFrame", typeof(ImageSource), typeof(EditorViewModel));
        public ImageSource CurrentFrame
        {
            get { return (ImageSource)GetValue(CurrentFrameProperty); }
            set { SetValue(CurrentFrameProperty, value); }
        }

        public static readonly DependencyProperty IsReadyProperty = DependencyProperty.Register("IsEditorReady", typeof(bool), typeof(EditorViewModel));
        public bool IsEditorReady
        {
            get { return (bool)GetValue(IsReadyProperty); }
            set { SetValue(IsReadyProperty, value); }
        }

        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register("ZoomFactor", typeof(float), typeof(EditorViewModel));
        public float ZoomFactor
        {
            get { return (float)GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }

        public static readonly DependencyProperty StatusTextProperty = DependencyProperty.Register("StatusText", typeof(string), typeof(EditorViewModel));
        public string StatusText
        {
            get { return (string)GetValue(StatusTextProperty); }
            set { SetValue(StatusTextProperty, value); }
        }

        public EditorViewModel()
        {
            IsEditorReady = true;
            EventLog = new Log();
        }

        #region Clear Command
        private ICommand _clearProjectCommand;
        public ICommand ClearProjectCommand
        {
            get
            {
                return _clearProjectCommand ?? (_clearProjectCommand = new CommandHandler(() => ClearProject(), IsEditorReady));
            }
        }

        public void ClearProject()
        {
            var AreYouSure = MessageBox.Show("Clearing the project will remove all scratchdisks. Any un-exported frames will be lost. Continue?", 
                                             "Cutting Room Floor XRF", 
                                             MessageBoxButton.YesNo, 
                                             MessageBoxImage.Exclamation);

            if (AreYouSure != MessageBoxResult.Yes)
                return;

            // CLEAR, EVERYTHING.
            IsEditorReady = false;
            _thumbnailCollection.Clear();
            FramesPerSecond = 0;
            MovieFilePath = null;
            ThumbnailScratchdisk.Dispose();
            ThumbnailFsWatcher.Dispose();
            CurrentFrame = null;
        }
        #endregion

        #region Open Command
        private ICommand _openMovieCommand;
        public ICommand OpenMovieCommand
        {
            get
            {
                return _openMovieCommand ?? (_openMovieCommand = new CommandHandler(() =>OpenMovie(), true));
            }
        }
        
        public void OpenMovie()
        {
            // Self-explanatory, show a filesystem dialog, ask for a movie path.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Movie for editing",
                CheckFileExists = true,
                InitialDirectory = Environment.SpecialFolder.MyVideos.ToString()
            };

            if (openFileDialog.ShowDialog() == true)
                MovieFilePath = openFileDialog.FileName;

            // Estimate FPS here.
            FramesPerSecond = 30;

            // Create a temporary scratchdisk to store thumbnails.
            LogMessage("Creating thumbnail scratchdisk...", EventLog);
            ThumbnailScratchdisk = new Scratchdisk();

            LogMessage(string.Format("Scratchdisk created at {0}", ThumbnailScratchdisk.Location), EventLog);

            // Create a file system watcher to watch the scratchdisk for file creation.
            // Created or deleted files will be added or removed from the timeline queue respectively.
            LogMessage("Initialising File System Watcher for the thumbnail scratchdisk...", EventLog);
            ThumbnailFsWatcher = new FileSystemWatcher
            {
                Path = ThumbnailScratchdisk.Location,
                Filter = "thumb-*.jpg",
                NotifyFilter = NotifyFilters.CreationTime,
                IncludeSubdirectories = true
            };

            // Bind creation event and start watching.
            ThumbnailFsWatcher.Created += new FileSystemEventHandler(Watcher_FileCreated);
            ThumbnailFsWatcher.EnableRaisingEvents = true;

            // Create a new FrameExtractor configured to extract sqcif thumbnails.
            FrameExtractor thumbnailExtractor = new FrameExtractor(MovieFilePath, ThumbnailScratchdisk, ExtractionMode.Thumbnails);

            // Bind status messages to logger.
            thumbnailExtractor.ExtractionStarted += (sender, e) => LogMessage("Extracting thumbnails...", EventLog);
            thumbnailExtractor.ExtractionComplete += (sender, e) => LogMessage("Thumbnails extracted successfully.", EventLog);

            LogMessage("Creating a bindable image source...", EventLog);
            _thumbnailCollection = new ObservableCollection<BitmapImage>();
            pathsTEST = new List<string>();

            // Start extraction, FFmpeg process will be spawned, files will be added to the scratchdisk,
            // file system watcher will pick them up and add them to the timeline.
            thumbnailExtractor.Extract();
            IsEditorReady = true;
        }
        #endregion

        void Watcher_FileCreated(object sender, FileSystemEventArgs e)
        {
            _thumbnailCollection.Add(new BitmapImage(new Uri(e.FullPath)));
            pathsTEST.Add(e.FullPath);
        }

        void LogMessage(string message, Log log)
        {
            Dispatcher.Invoke(new Action(() => {
                log.Write(message);
                StatusText = message;
            }));
        }
    }
}
