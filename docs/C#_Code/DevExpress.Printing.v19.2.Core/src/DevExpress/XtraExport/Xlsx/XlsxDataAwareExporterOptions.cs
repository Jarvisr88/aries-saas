namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class XlsxDataAwareExporterOptions : IXlDocumentOptionsEx, IXlDocumentOptions
    {
        public XlDocumentFormat DocumentFormat =>
            XlDocumentFormat.Xlsx;

        public CultureInfo Culture { get; set; }

        public bool SupportsFormulas =>
            true;

        public bool SupportsDocumentParts =>
            true;

        public bool SupportsOutlineGrouping =>
            true;

        public int MaxColumnCount =>
            0x4000;

        public int MaxRowCount =>
            0x100000;

        public bool UseDeviceIndependentPixels { get; set; }

        public bool TruncateStringsToMaxLength { get; set; }

        public bool SuppressEmptyStrings { get; set; }
    }
}

