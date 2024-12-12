namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Windows;

    public interface IPreviewAreaProvider
    {
        double GetScaleX();

        Size PreviewArea { get; }
    }
}

