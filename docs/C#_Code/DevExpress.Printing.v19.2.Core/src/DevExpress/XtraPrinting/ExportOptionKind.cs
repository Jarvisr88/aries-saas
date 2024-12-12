namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum ExportOptionKind : long
    {
        PdfPageRange = 1L,
        [EditorBrowsable(EditorBrowsableState.Never)]
        PdfCompressed = 2L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.PdfACompatibility))]
        PdfACompatibility = 4L,
        PdfShowPrintDialogOnOpen = 8L,
        PdfNeverEmbeddedFonts = 0x10L,
        PdfPasswordSecurityOptions = 0x20L,
        PdfSignatureOptions = 0x40L,
        PdfConvertImagesToJpeg = 0x80L,
        PdfExportEditingFieldsToAcroForms = 0x100L,
        [OptionKindPropertyType(typeof(PdfJpegImageQuality))]
        PdfImageQuality = 0x200L,
        PdfDocumentAuthor = 0x400L,
        PdfDocumentApplication = 0x800L,
        PdfDocumentTitle = 0x1000L,
        PdfDocumentSubject = 0x2000L,
        PdfDocumentKeywords = 0x4000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.HtmlExportMode))]
        HtmlExportMode = 0x8000L,
        HtmlCharacterSet = 0x10000L,
        HtmlTitle = 0x20000L,
        HtmlRemoveSecondarySymbols = 0x40000L,
        HtmlEmbedImagesInHTML = 0x80000L,
        HtmlPageRange = 0x100000L,
        HtmlPageBorderWidth = 0x200000L,
        HtmlPageBorderColor = 0x400000L,
        HtmlTableLayout = 0x800000L,
        HtmlExportWatermarks = 0x1000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.RtfExportMode))]
        RtfExportMode = 0x2000000L,
        RtfPageRange = 0x4000000L,
        RtfExportWatermarks = 0x8000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.DocxExportMode))]
        DocxExportMode = 0x10000000L,
        DocxPageRange = 0x20000000L,
        DocxTableLayout = 0x40000000L,
        DocxKeepRowHeight = 0x80000000L,
        DocxExportWatermarks = 0x100000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.XlsExportMode))]
        XlsExportMode = 0x200000000L,
        XlsPageRange = 0x400000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.XlsxExportMode))]
        XlsxExportMode = 0x800000000L,
        XlsxPageRange = 0x1000000000L,
        TextSeparator = 0x2000000000L,
        TextEncoding = 0x4000000000L,
        TextQuoteStringsWithSeparators = 0x8000000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.TextExportMode))]
        TextExportMode = 0x10000000000L,
        XlsShowGridLines = 0x20000000000L,
        [EditorBrowsable(EditorBrowsableState.Never)]
        XlsUseNativeFormat = 0x40000000000L,
        XlsExportHyperlinks = 0x80000000000L,
        XlsRawDataMode = 0x100000000000L,
        XlsSheetName = 0x200000000000L,
        [OptionKindPropertyType(typeof(DevExpress.XtraPrinting.ImageExportMode))]
        ImageExportMode = 0x400000000000L,
        ImagePageRange = 0x800000000000L,
        ImagePageBorderWidth = 0x1000000000000L,
        ImagePageBorderColor = 0x2000000000000L,
        ImageFormat = 0x4000000000000L,
        ImageResolution = 0x8000000000000L,
        NativeFormatCompressed = 0x10000000000000L,
        XpsPageRange = 0x20000000000000L,
        [OptionKindPropertyType(typeof(XpsCompressionOption))]
        XpsCompression = 0x40000000000000L,
        XpsDocumentCreator = 0x80000000000000L,
        XpsDocumentCategory = 0x100000000000000L,
        XpsDocumentTitle = 0x200000000000000L,
        XpsDocumentSubject = 0x400000000000000L,
        XpsDocumentKeywords = 0x800000000000000L,
        XpsDocumentVersion = 0x1000000000000000L,
        XpsDocumentDescription = 0x2000000000000000L
    }
}

