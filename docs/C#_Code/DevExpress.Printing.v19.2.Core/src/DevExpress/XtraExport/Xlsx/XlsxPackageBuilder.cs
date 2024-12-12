namespace DevExpress.XtraExport.Xlsx
{
    using System;
    using System.IO;

    public class XlsxPackageBuilder : OfficePackageBuilder
    {
        public const string WorksheetContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml";
        public const string ChartsheetContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.chartsheet+xml";
        public const string WorkbookContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml";
        public const string SharedStringsContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml";
        public const string StylesContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml";
        public const string SpreadsheetNamespaceConst = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        public const string RelsWorksheetNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";

        protected XlsxPackageBuilder()
        {
        }

        public XlsxPackageBuilder(Stream outputStream) : base(outputStream)
        {
        }
    }
}

