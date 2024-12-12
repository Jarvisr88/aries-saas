namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;

    public class ScaleWindowViewModelEventArgs : EventArgs
    {
        public ScaleWindowViewModelEventArgs(DevExpress.Xpf.Printing.ScaleMode scaleMode, float scaleFactor, int pagesToFit)
        {
            this.ScaleMode = scaleMode;
            this.ScaleFactor = scaleFactor;
            this.PagesToFit = pagesToFit;
        }

        public DevExpress.Xpf.Printing.ScaleMode ScaleMode { get; private set; }

        public float ScaleFactor { get; private set; }

        public int PagesToFit { get; private set; }
    }
}

