namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public interface IScaleService
    {
        void Scale(PrintingSystemPreviewModel previewModel, Window ownerWindow);
    }
}

