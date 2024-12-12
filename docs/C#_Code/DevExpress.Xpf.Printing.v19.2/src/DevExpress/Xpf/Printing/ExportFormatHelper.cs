namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.XtraPrinting;
    using System;

    internal static class ExportFormatHelper
    {
        private const string large = "32x32";
        private const string small = "16x16";
        private const string relativePath = @"\Images\BarItems\ExportTo{0}_{1}.png";
        private const string export = "Export";
        private const string send = "Send";
        private static readonly string dllName = Assembly.GetExecutingAssembly().GetName().Name;

        public static PrintingStringId GetExportCaption(ExportFormat format, bool isSendOperation)
        {
            PrintingStringId id;
            return (!Enum.TryParse<PrintingStringId>(isSendOperation ? "Send" : ("Export" + format.ToString()), out id) ? (isSendOperation ? PrintingStringId.SendFile : PrintingStringId.ExportFile) : id);
        }

        public static Uri GetImageUri(ExportFormat format, GlyphSize size) => 
            (format != ExportFormat.Htm) ? ((format != ExportFormat.Txt) ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, $"\Images\BarItems\ExportTo{format.ToString()}_{GlyphSizeToString(size)}.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, $"\Images\BarItems\ExportTo{"Text"}_{GlyphSizeToString(size)}.png")) : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(dllName, $"\Images\BarItems\ExportTo{"html"}_{GlyphSizeToString(size)}.png");

        public static string GlyphSizeToString(GlyphSize size)
        {
            switch (size)
            {
                case GlyphSize.Large:
                    return "32x32";
            }
            return "16x16";
        }
    }
}

