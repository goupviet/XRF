using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xrf.IO.Video;
using Xrf.IO.Temporary;
using Xrf.Views;
using System.Drawing;

namespace Xrf.ViewModels
{
    public class EditorViewModel : DependencyObject
    {
        private string MovieFilePath;
        private Scratchdisk ThumbnailScratchdisk;

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

            // Create a temporary scratchdisk to store thumbnails.
            ThumbnailScratchdisk = new Scratchdisk();

            // Create a new FrameExtractor configured to extract sqcif thumbnails.
            FrameExtractor thumbnailExtractor = new FrameExtractor(MovieFilePath, ThumbnailScratchdisk, FrameExtractionMode.Thumbnails);
            
            thumbnailExtractor.Extract();
            IsEditorReady = true;
        }
        #endregion
    }
}
