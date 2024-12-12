namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IWatermarkService
    {
        event EventHandler<WatermarkServiceEventArgs> EditCompleted;

        void Edit(Window ownerWindow, Page currentPage, int pagesCount, Watermark currentWatermark);
        void Edit(Window ownerWindow, XtraPageSettingsBase pageSettings, int pagesCount, Watermark currentWatermark);
    }
}

