namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class AvailableExportModes
    {
        private IEnumerable<RtfExportMode> rtf;
        private IEnumerable<DocxExportMode> docx;
        private IEnumerable<HtmlExportMode> html;
        private IEnumerable<ImageExportMode> image;
        private IEnumerable<XlsExportMode> xls;
        private IEnumerable<XlsxExportMode> xlsx;

        public AvailableExportModes();
        public AvailableExportModes(IEnumerable<RtfExportMode> rtf, IEnumerable<DocxExportMode> docx, IEnumerable<HtmlExportMode> html, IEnumerable<ImageExportMode> image, IEnumerable<XlsExportMode> xls, IEnumerable<XlsxExportMode> xlsx);
        public object[] GetExportModesByType(Type exportModeType);

        public IEnumerable<RtfExportMode> Rtf { get; set; }

        public IEnumerable<DocxExportMode> Docx { get; set; }

        public IEnumerable<HtmlExportMode> Html { get; set; }

        public IEnumerable<ImageExportMode> Image { get; set; }

        public IEnumerable<XlsExportMode> Xls { get; set; }

        public IEnumerable<XlsxExportMode> Xlsx { get; set; }
    }
}

