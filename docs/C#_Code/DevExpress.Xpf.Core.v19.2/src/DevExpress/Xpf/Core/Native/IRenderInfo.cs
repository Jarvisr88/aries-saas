namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IRenderInfo
    {
        void Render(DrawingContext dc, Rect bounds);

        double Width { get; }

        double Height { get; }

        Thickness Margin { get; }

        double RelativeHeight { get; }

        double RelativeWidth { get; }

        int ZIndex { get; }

        System.Windows.HorizontalAlignment HorizontalAlignment { get; }

        System.Windows.VerticalAlignment VerticalAlignment { get; }
    }
}

