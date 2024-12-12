namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IScrollControl : IPanel, IControl
    {
        void SetOffset(Point offset);

        double HorizontalOffset { get; set; }

        double VerticalOffset { get; set; }

        Point Offset { get; }
    }
}

