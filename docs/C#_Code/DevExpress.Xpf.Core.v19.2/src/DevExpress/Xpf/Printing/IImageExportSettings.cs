namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public interface IImageExportSettings : IExportSettings
    {
        FrameworkElement SourceElement { get; }

        DevExpress.Xpf.Printing.ImageRenderMode ImageRenderMode { get; }

        bool ForceCenterImageMode { get; }

        object ImageKey { get; }
    }
}

