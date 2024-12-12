namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public interface ITabHeader
    {
        void Apply(ITabHeaderInfo info);
        ITabHeaderInfo CreateInfo(Size size);

        Rect ArrangeRect { get; }

        bool IsPinned { get; }

        TabHeaderPinLocation PinLocation { get; }

        int ScrollIndex { get; }
    }
}

