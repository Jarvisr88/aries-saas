namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class XlsDataAwareExporterOptions : IXlDocumentOptionsEx, IXlDocumentOptions
    {
        public XlDocumentFormat DocumentFormat =>
            XlDocumentFormat.Xls;

        public CultureInfo Culture { get; set; }

        public bool SupportsFormulas =>
            true;

        public bool SupportsDocumentParts =>
            true;

        public bool SupportsOutlineGrouping =>
            true;

        public int MaxColumnCount =>
            0x100;

        public int MaxRowCount =>
            0x10000;

        public bool UseDeviceIndependentPixels { get; set; }

        public bool TruncateStringsToMaxLength { get; set; }

        public bool UseCustomPalette { get; set; }

        public bool SuppressEmptyStrings { get; set; }
    }
}

