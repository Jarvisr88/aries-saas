namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public interface IElementSizer : IControl
    {
        bool CollapseOnDoubleClick { get; }

        FrameworkElement Element { get; }

        DevExpress.Xpf.Core.Side Side { get; }

        bool UseSizingStep { get; }
    }
}

