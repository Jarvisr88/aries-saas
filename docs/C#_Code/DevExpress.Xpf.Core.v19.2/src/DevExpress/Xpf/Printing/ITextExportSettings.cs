namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface ITextExportSettings : IExportSettings
    {
        System.Windows.HorizontalAlignment HorizontalAlignment { get; }

        System.Windows.VerticalAlignment VerticalAlignment { get; }

        string Text { get; }

        object TextValue { get; }

        string TextValueFormatString { get; }

        System.Windows.Media.FontFamily FontFamily { get; }

        System.Windows.FontStyle FontStyle { get; }

        System.Windows.FontWeight FontWeight { get; }

        double FontSize { get; }

        Thickness Padding { get; }

        System.Windows.TextWrapping TextWrapping { get; }

        bool NoTextExport { get; }

        bool? XlsExportNativeFormat { get; }

        string XlsxFormatString { get; }

        TextDecorationCollection TextDecorations { get; }

        System.Windows.TextTrimming TextTrimming { get; }
    }
}

