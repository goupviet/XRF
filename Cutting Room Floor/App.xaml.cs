﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;

namespace Xrf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void OnStartup(object sender, StartupEventArgs e)
        {
            // Sanity checks

#if DEBUG
            Xrf.Properties.Settings.Default.FFMpegLocation = System.IO.Path.Combine(Environment.CurrentDirectory, "ffmpeg.exe");
#endif

            // Check for valid FFmpeg
            if (string.IsNullOrEmpty(Xrf.Properties.Settings.Default.FFMpegLocation))
            {
                Shutdown(1);
            }
        }
    }
}
