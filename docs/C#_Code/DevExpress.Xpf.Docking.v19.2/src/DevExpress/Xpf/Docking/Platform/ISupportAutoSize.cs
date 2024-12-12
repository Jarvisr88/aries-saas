namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public interface ISupportAutoSize
    {
        Size FitToContent(Size availableSize);

        bool IsAutoSize { get; }
    }
}

