namespace DevExpress.SpreadsheetSource.Csv
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class CsvSpreadsheetSource : SpreadsheetSourceBase
    {
        private CsvSourceDataReader dataReader;

        public CsvSpreadsheetSource(Stream stream, ISpreadsheetSourceOptions options) : base(stream, options)
        {
            this.InitializeWorkbookStructure();
        }

        public CsvSpreadsheetSource(string fileName, ISpreadsheetSourceOptions options) : base(fileName, options)
        {
            this.InitializeWorkbookStructure();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.CloseDataReader();
            }
            base.Dispose(disposing);
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new CsvSourceDataReader(this, base.InputStream);
            this.dataReader.Open(worksheet, null);
            return this.dataReader;
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, XlCellRange range)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new CsvSourceDataReader(this, base.InputStream);
            this.dataReader.Open(worksheet, range);
            return this.dataReader;
        }

        private void InitializeWorkbookStructure()
        {
            base.InnerWorksheets.Add(new Worksheet("Sheet", XlSheetVisibleState.Visible));
        }

        protected internal CsvSpreadsheetSourceOptions InnerOptions =>
            base.Options as CsvSpreadsheetSourceOptions;

        public override SpreadsheetDocumentFormat DocumentFormat =>
            SpreadsheetDocumentFormat.Csv;

        public override int MaxRowCount =>
            0x7fffffff;

        public override int MaxColumnCount =>
            0x7fffffff;
    }
}

