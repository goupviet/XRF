using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XRF_FFmpeg_Invoke_Test
{
    class Program
    {
        [DllImport("Xrf.FFmpeg.dll", EntryPoint="ExtractAllFrames", CallingConvention=CallingConvention.StdCall)]
        static extern int ExtractAllFrames(string szPath, string szDestination, float scaleFactor);

        static void Main(string[] args)
        {
            string path = args[0];
            string dest = args[1];
            float sf = 1.0F;

            ExtractAllFrames(path, dest, sf);
            Console.ReadLine();
        }
    }
}
