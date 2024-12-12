namespace DevExpress.Export.Xl
{
    using System;
    using System.Globalization;

    public interface IXlDocumentOptions
    {
        XlDocumentFormat DocumentFormat { get; }

        CultureInfo Culture { get; set; }

        bool SupportsFormulas { get; }

        bool SupportsDocumentParts { get; }

        bool SupportsOutlineGrouping { get; }

        int MaxColumnCount { get; }

        int MaxRowCount { get; }
    }
}

