using Microsoft.Win32;
using System;
using System.Windows;

namespace Xrf.Views
{
    public static class Dialogs
    {
        public static string ShowOpenMovieDialog()
        {
            // Self-explanatory, show a filesystem dialog, ask for a movie path.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Movie for editing",
                CheckFileExists = true,
                InitialDirectory = Environment.SpecialFolder.MyVideos.ToString()
            };

            return (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : null;
        }

        public static bool ConfirmProjectShutdown()
        {
            var intent = MessageBox.Show("Clearing the project will remove all scratchdisks. Any un-exported frames will be lost. Continue?",
                                             "Cutting Room Floor XRF",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Exclamation);

            return (intent == MessageBoxResult.Yes);
        }
    }
}
