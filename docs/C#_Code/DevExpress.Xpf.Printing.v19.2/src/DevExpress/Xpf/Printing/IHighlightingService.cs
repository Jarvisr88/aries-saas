namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public interface IHighlightingService
    {
        void HideHighlighting();
        void ShowHighlighting(FrameworkElement parent, FrameworkElement target);
    }
}

