using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using Xrf.IO.Editor;
using Xrf.IO.Temporary;
using Xrf.IO.Video;
using Xrf.Views;
using System.Linq;

namespace Xrf.ViewModels
{
    /// <summary>
    /// Represents the live frame editor environment environment
    /// </summary>
    public class EditorViewModel
    {
        private string MovieFilePath;

        public bool IsEditorReady { get; set; }
        public bool IsExtractingThumbnails { get; set; }

        public float ZoomFactor { get; set; }
        public string StatusText { get; set; }

        public Movie MovieMetadata { get; set; }
        public Scratchdisk ThumbnailScratchdisk { get; set; }
        public Scratchdisk EditorScratchdisk { get; set; }
        public List<ImageEditor> Editors { get; set; }
        public ImageEditor CurrentEditor { get; set; }
        public Frame SelectedThumbnail
        {
            get;
            set
            {
                // Check whether the selected frame already has an Editor created
                if (Editors.Any<ImageEditor>(x => x.OriginalImage.Index == value.Index))
                {
                    CurrentEditor = Editors.Where(x => x.OriginalImage.Index == value.Index).First();
                }
                else
                {
                    CurrentEditor = new ImageEditor(value);
                }
            }
        }

        public EditorViewModel()
        {
            // Create a temporary scratchdisk to store thumbnails.
            ThumbnailScratchdisk = new Scratchdisk();

            Editors = new List<ImageEditor>();

            IsEditorReady = true;
            IsExtractingThumbnails = false;
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

            MovieMetadata = new Movie(MovieFilePath);

            // Create a new FrameExtractor configured to extract sqcif thumbnails.
            FrameExtractor thumbnailExtractor = new FrameExtractor(MovieFilePath, ThumbnailScratchdisk, FrameExtractionMode.Thumbnails);
            thumbnailExtractor.ExtractionStarted += (sender, e) => { IsExtractingThumbnails = true; };
            thumbnailExtractor.ExtractionComplete += (sender, e) => { IsExtractingThumbnails = false; };

            thumbnailExtractor.Extract();

            IsEditorReady = true;
        }
        #endregion
    }
}
