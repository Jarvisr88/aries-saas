namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Media;

    public interface IWatermarkViewModel
    {
        string Text { get; set; }

        DirectionMode TextDirection { get; set; }

        string TextFontFamily { get; set; }

        int TextFontSize { get; set; }

        System.Windows.Media.Color TextForeground { get; set; }

        bool TextIsItalic { get; set; }

        bool TextIsBold { get; set; }

        string ImageFileName { get; set; }

        DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; }

        DevExpress.XtraPrinting.Drawing.ImageViewMode ImageViewMode { get; set; }

        bool ImageIsTiled { get; set; }

        HorizontalAlignment ImageHorizontalAlignment { get; set; }

        VerticalAlignment ImageVerticalAlignment { get; set; }

        double ImageTransparency { get; set; }

        bool ShowBehind { get; set; }

        double TextTransparency { get; set; }

        bool IsPageRange { get; set; }

        string PageRangePlaceholder { get; set; }

        string PageRange { get; set; }

        ContentAlignment ImageAlignment { get; set; }

        System.Drawing.Color TextColor { get; set; }

        Font TextFont { get; set; }
    }
}

