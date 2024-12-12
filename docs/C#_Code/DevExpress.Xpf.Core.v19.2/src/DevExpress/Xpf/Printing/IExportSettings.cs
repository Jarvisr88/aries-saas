namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IExportSettings
    {
        Color Background { get; }

        Color Foreground { get; }

        Color BorderColor { get; }

        Thickness BorderThickness { get; }

        string Url { get; }

        IOnPageUpdater OnPageUpdater { get; }

        DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle { get; }

        object MergeValue { get; }

        System.Windows.FlowDirection FlowDirection { get; }
    }
}

