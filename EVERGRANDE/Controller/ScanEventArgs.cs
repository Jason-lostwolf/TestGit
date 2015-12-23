using System;
using System.Windows.Forms;
using ENPOT.View;

namespace EVERGRANDE.Controller
{
    public class ScanEventArgs : EventArgs
    {
        public ScanData ScanData { get; private set; }

        public ScanEventArgs(ScanData s)
        {
            this.ScanData = s;
        }

        public Label RefreshLabel { get; private set; }
        public ScanEventArgs(ScanData s, Label l)
        {
            this.ScanData = s;
            this.RefreshLabel = l;
        }
    }
}
