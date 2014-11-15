using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xrf.IO.Temporary
{
    public delegate void ScratchdiskFileAddedEventHandler(object sender, ScratchdiskFileEventArgs e);
    public delegate void ScratchdiskFileDeletedEventHandler(object sender, ScratchdiskFileEventArgs e);

    public class ScratchdiskFileEventArgs : EventArgs
    {
        public string FileName { get; private set; }
        public ScratchdiskFileOperations Operation { get; private set; }

        public ScratchdiskFileEventArgs(string filename, ScratchdiskFileOperations sfo)
        {
            FileName = filename;
            Operation = sfo;
        }
    }

    public enum ScratchdiskFileOperations
    {
        Added,
        Deleted
    }
}
