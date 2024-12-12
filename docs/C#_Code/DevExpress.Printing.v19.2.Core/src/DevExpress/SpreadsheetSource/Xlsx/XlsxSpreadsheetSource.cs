namespace DevExpress.SpreadsheetSource.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.SpreadsheetSource.Xlsx.Import;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsxSpreadsheetSource : SpreadsheetSourceBase
    {
        private XlsxSpreadsheetSourceImporter importer;
        private XlsxSourceDataReader dataReader;

        public XlsxSpreadsheetSource(Stream stream, ISpreadsheetSourceOptions options) : base(stream, options)
        {
            this.CreateImporter();
            this.ReadWorkbookData();
        }

        public XlsxSpreadsheetSource(string fileName, ISpreadsheetSourceOptions options) : base(fileName, options)
        {
            this.CreateImporter();
            this.ReadWorkbookData();
        }

        private void CloseDataReader()
        {
            if (this.dataReader != null)
            {
                if (!this.dataReader.IsClosed)
                {
                    this.dataReader.Close();
                }
                this.dataReader = null;
            }
        }

        private void CreateImporter()
        {
            this.importer = new XlsxSpreadsheetSourceImporter(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.importer != null))
            {
                this.importer.Dispose();
                this.importer = null;
            }
            base.Dispose(disposing);
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new XlsxSourceDataReader(this.importer);
            this.dataReader.Open(worksheet, null);
            return this.dataReader;
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, XlCellRange range)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new XlsxSourceDataReader(this.importer);
            this.dataReader.Open(worksheet, range);
            return this.dataReader;
        }

        private void ReadWorkbookData()
        {
            this.importer.Import(base.InputStream);
        }

        public override SpreadsheetDocumentFormat DocumentFormat =>
            SpreadsheetDocumentFormat.Xlsx;

        public override int MaxColumnCount =>
            XlCellPosition.MaxColumnCount;

        public override int MaxRowCount =>
            XlCellPosition.MaxRowCount;

        internal XlsxSourceDataReader DataReader =>
            this.dataReader;

        protected internal bool UseDate1904 { get; set; }
    }
}

