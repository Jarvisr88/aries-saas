namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ExportOptionsHelper
    {
        public static HtmlExportOptions ChangeHtmlExportOptions(HtmlExportOptions source, string newTitle);
        public static MhtExportOptions ChangeMhtExportOptionsTitle(MhtExportOptions source, string newTitle);
        public static CsvExportOptions ChangeOldCsvProperties(CsvExportOptions source, Encoding newEncoding, string newSeparator);
        public static HtmlExportOptions ChangeOldHtmlProperties(HtmlExportOptions source, string newCharacterSet, string newTitle, bool newCompressed);
        private static HtmlExportOptionsBase ChangeOldHtmlPropertiesBase(HtmlExportOptionsBase source, string newCharacterSet, string newTitle, bool newCompressed);
        public static ImageExportOptions ChangeOldImageProperties(ImageExportOptions source, ImageFormat newFormat);
        public static MhtExportOptions ChangeOldMhtProperties(MhtExportOptions source, string newCharacterSet, string newTitle, bool newCompressed);
        public static TextExportOptions ChangeOldTextProperties(TextExportOptions source, Encoding newEncoding, string newSeparator);
        private static TextExportOptionsBase ChangeOldTextPropertiesBase(TextExportOptionsBase source, Encoding newEncoding, string newSeparator);
        public static XlsExportOptions ChangeOldXlsProperties(XlsExportOptions source, TextExportMode newTextExportMode, bool newShowGridLines);
        internal static XlsExportOptions ChangeOldXlsProperties(XlsExportOptions source, bool usingNativeFormat, bool newShowGridLines);
        public static ExportOptionsBase CloneOptions(ExportOptionsBase source);
        public static string GetFileExtension(this ExportOptionsBase exportOptions);
        public static ExportFormat GetFormat(this ExportOptionsBase exportOptions);
        private static string GetImageFormatFileExtension(this ImageExportOptions exportOptions);
        public static int[] GetPageIndices(PageByPageExportOptionsBase pageByPageOptions, int pageCount);
        public static bool GetShowOptionsBeforeExport(ExportOptionsBase options, bool defaultValue);
        public static bool GetUseActionAfterExportAndSaveModeValue(ExportOptionsBase options);

        public static Dictionary<string, ImageFormat> ImageFormats { get; }
    }
}

