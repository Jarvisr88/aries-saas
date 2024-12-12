namespace DevExpress.XtraPrinting.Native
{
    using System;

    [Flags]
    public enum DocumentBandKind
    {
        public const DocumentBandKind Detail = DocumentBandKind.Detail;,
        public const DocumentBandKind Header = DocumentBandKind.Header;,
        public const DocumentBandKind Footer = DocumentBandKind.Footer;,
        public const DocumentBandKind Storage = DocumentBandKind.Storage;,
        public const DocumentBandKind PageBand = DocumentBandKind.PageBand;,
        public const DocumentBandKind TopMargin = DocumentBandKind.TopMargin;,
        public const DocumentBandKind BottomMargin = DocumentBandKind.BottomMargin;,
        public const DocumentBandKind ReportHeader = DocumentBandKind.ReportHeader;,
        public const DocumentBandKind ReportFooter = DocumentBandKind.ReportFooter;,
        public const DocumentBandKind PageBreak = DocumentBandKind.PageBreak;,
        public const DocumentBandKind VerticalHeader = DocumentBandKind.VerticalHeader;,
        public const DocumentBandKind VerticalTotal = DocumentBandKind.VerticalTotal;,
        public const DocumentBandKind PageHeader = DocumentBandKind.PageHeader;,
        public const DocumentBandKind PageFooter = DocumentBandKind.PageFooter;
    }
}

