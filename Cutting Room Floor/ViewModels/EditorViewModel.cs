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
        private Log _log;
        private FileSystemWatcher ThumbnailFsWatcher;

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
            _log = new Log();
        }

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

            // Create a temporary scratchdisk to store thumbnails.
            LogMessage("Creating thumbnail scratchdisk...");
            ThumbnailScratchdisk = new Scratchdisk();

            LogMessage(string.Format("Scratchdisk created at {0}", ThumbnailScratchdisk.Location));

            // Create a new FrameExtractor configured to extract sqcif thumbnails.
            FrameExtractor thumbnailExtractor = new FrameExtractor(MovieFilePath, ThumbnailScratchdisk, ExtractionMode.Thumbnails);

            // Bind status messages to logger.
            thumbnailExtractor.ExtractionStarted += (sender, e) => LogMessage("Extracting thumbnails...");
            thumbnailExtractor.ExtractionComplete += (sender, e) => LogMessage("Thumbnails extracted successfully.");

            LogMessage("Creating a bindable image source...");
            _thumbnailCollection = new ObservableCollection<BitmapImage>();

            // Create a file system watcher to watch the scratchdisk for file creation.
            // Created or deleted files will be added or removed from the timeline queue respectively.
            LogMessage("Initialising File System Watcher for the thumbnail scratchdisk...");
            ThumbnailFsWatcher = new FileSystemWatcher
            {
                Path = ThumbnailScratchdisk.Location,
                Filter = "thumb-*.jpg",
            };

            // Bind creation event and start watching.
            ThumbnailFsWatcher.Created += new FileSystemEventHandler(Watcher_FileCreated);
            ThumbnailFsWatcher.EnableRaisingEvents = true;

            // Start extraction, FFmpeg process will be spawned, files will be added to the scratchdisk,
            // file system watcher will pick them up and add them to the timeline.
            thumbnailExtractor.Extract();
        }

        void Watcher_FileCreated(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => {
                LogMessage(string.Format("Created file {0}", e.Name));
                _thumbnailCollection.Add(new BitmapImage(new Uri(e.FullPath)));
            }));
        }

        void LogMessage(string message)
        {
            Dispatcher.Invoke(new Action(() => {
                _log.Write(message);
                StatusText = message;
            }));
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
