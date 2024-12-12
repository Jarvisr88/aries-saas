namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface ITabHeaderInfo
    {
        object TabHeader { get; }

        bool IsSelected { get; }

        Size DesiredSize { get; }

        Size CaptionImage { get; }

        Size CaptionText { get; }

        Size ControlBox { get; }

        double CaptionImageToCaptionDistance { get; }

        double CaptionToControlBoxDistance { get; }

        TabHeaderPinLocation PinLocation { get; }

        bool IsPinned { get; }

        System.Windows.Rect Rect { get; set; }

        bool IsVisible { get; set; }

        int ZIndex { get; set; }

        bool ShowCaption { get; set; }

        bool ShowCaptionImage { get; set; }

        int ScrollIndex { get; set; }

        int Index { get; }

        IMultiLineLayoutResult MultiLineResult { get; set; }
    }
}

