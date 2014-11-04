using System.IO;

namespace Xrf.Video.IO
{
    /// <summary>
    /// Provides basic routines for finding and using FFmpeg on the end-user's machine.
    /// </summary>
    public static class FFmpeg
    {
        /// <summary>
        /// Checks to see if a local instance of FFmpeg is present in the working directory.
        /// </summary>
        /// <returns>The file path for FFmpeg, null otherwise.</returns>
        public static string GetLocalInstance()
        {
            var ffmpegPath = Path.Combine(Directory.GetCurrentDirectory(), "ffmpeg.exe");

            if (File.Exists(ffmpegPath))
            {
                return ffmpegPath;
            }
            else
            {
                return null;
            }
        }
    }
}
