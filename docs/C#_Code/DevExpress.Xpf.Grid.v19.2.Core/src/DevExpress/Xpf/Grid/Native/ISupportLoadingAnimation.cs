namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public interface ISupportLoadingAnimation
    {
        DataViewBase DataView { get; }

        bool IsReady { get; }

        FrameworkElement Element { get; }

        bool IsGroupRow { get; }
    }
}

