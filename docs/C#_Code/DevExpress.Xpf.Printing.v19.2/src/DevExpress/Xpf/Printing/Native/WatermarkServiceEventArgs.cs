namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class WatermarkServiceEventArgs : EventArgs
    {
        public WatermarkServiceEventArgs(XpfWatermark watermark, bool? isAssigned)
        {
            this.IsWatermarkAssigned = isAssigned;
            this.Watermark = watermark;
        }

        public bool? IsWatermarkAssigned { get; set; }

        public XpfWatermark Watermark { get; set; }
    }
}

