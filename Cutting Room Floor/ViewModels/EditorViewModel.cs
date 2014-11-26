using System.Windows.Input;
using System.Windows.Media;
using Xrf.IO.Editor;
using Xrf.IO.Temporary;
using Xrf.IO.Video;
using Xrf.Views;

namespace Xrf.ViewModels
{
    public class EditorViewModel
    {
        private string MovieFilePath;

        public ImageSource CurrentFrame { get; set; }
        public bool IsEditorReady { get; set; }
        public float ZoomFactor { get; set; }
        public string StatusText { get; set; }
        public Scratchdisk ThumbnailScratchdisk { get; set; }
        public ImageEditor Editor { get; set; }

        public EditorViewModel()
        {
            // Create a temporary scratchdisk to store thumbnails.
            ThumbnailScratchdisk = new Scratchdisk();

            IsEditorReady = true;
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
            // Ask user to confirm.
            if (!Dialogs.ConfirmProjectShutdown()) return;

            // CLEAR, EVERYTHING.
            IsEditorReady = false;
            MovieFilePath = null;
            ThumbnailScratchdisk.Dispose();
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
            MovieFilePath = Dialogs.ShowOpenMovieDialog();

            // Cancel loading if the dialog is closed.
            if (string.IsNullOrEmpty(MovieFilePath)) return;

            // Create a new FrameExtractor configured to extract sqcif thumbnails.
            FrameExtractor thumbnailExtractor = new FrameExtractor(MovieFilePath, ThumbnailScratchdisk, FrameExtractionMode.Thumbnails);
            
            thumbnailExtractor.Extract();

            IsEditorReady = true;
        }
        #endregion
    }
}
