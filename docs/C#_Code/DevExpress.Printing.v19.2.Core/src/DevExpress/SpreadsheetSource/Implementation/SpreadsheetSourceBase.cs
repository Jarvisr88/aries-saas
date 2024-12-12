namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class SpreadsheetSourceBase : ISpreadsheetSource, IDisposable
    {
        private readonly WorksheetCollection worksheets;
        private readonly DefinedNamesCollection definedNames;
        private readonly TablesCollection tables;
        private readonly ISpreadsheetSourceOptions options;
        private readonly Dictionary<int, string> numberFormatCodes;
        private readonly List<int> numberFormatIds;
        private readonly ChunkedArray<string> sharedStrings;
        private string fileName;
        private Stream inputStream;

        protected SpreadsheetSourceBase(Stream stream, ISpreadsheetSourceOptions options)
        {
            this.worksheets = new WorksheetCollection();
            this.definedNames = new DefinedNamesCollection();
            this.tables = new TablesCollection();
            this.numberFormatCodes = new Dictionary<int, string>();
            this.numberFormatIds = new List<int>();
            this.sharedStrings = new ChunkedArray<string>(0x2000, 0x80000);
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            this.fileName = string.Empty;
            this.inputStream = stream;
            this.options = options;
        }

        protected SpreadsheetSourceBase(string fileName, ISpreadsheetSourceOptions options)
        {
            this.worksheets = new WorksheetCollection();
            this.definedNames = new DefinedNamesCollection();
            this.tables = new TablesCollection();
            this.numberFormatCodes = new Dictionary<int, string>();
            this.numberFormatIds = new List<int>();
            this.sharedStrings = new ChunkedArray<string>(0x2000, 0x80000);
            Guard.ArgumentIsNotNullOrEmpty(fileName, "fileName");
            Guard.ArgumentNotNull(options, "options");
            this.fileName = fileName;
            this.options = options;
            this.inputStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.inputStream != null))
            {
                if (!string.IsNullOrEmpty(this.fileName))
                {
                    this.inputStream.Dispose();
                }
                this.inputStream = null;
            }
        }

        public virtual ISpreadsheetDataReader GetDataReader(XlCellRange range)
        {
            Guard.ArgumentNotNull(range, "range");
            IWorksheet worksheet = null;
            if (!string.IsNullOrEmpty(range.SheetName))
            {
                worksheet = this.Worksheets[range.SheetName];
            }
            if (worksheet == null)
            {
                throw new InvalidOperationException("Unable to determine source worksheet.");
            }
            return this.GetDataReader(worksheet, range);
        }

        public virtual ISpreadsheetDataReader GetDataReader(IDefinedName definedName)
        {
            Guard.ArgumentNotNull(definedName, "definedName");
            XlCellRange range = definedName.Range;
            if (range == null)
            {
                throw new InvalidOperationException("Unable to determine source range.");
            }
            IWorksheet worksheet = null;
            if (!string.IsNullOrEmpty(range.SheetName))
            {
                worksheet = this.Worksheets[range.SheetName];
            }
            else if (!string.IsNullOrEmpty(definedName.Scope))
            {
                worksheet = this.Worksheets[definedName.Scope];
            }
            if (worksheet == null)
            {
                throw new InvalidOperationException("Unable to determine source worksheet.");
            }
            return this.GetDataReader(worksheet, range);
        }

        public abstract ISpreadsheetDataReader GetDataReader(IWorksheet worksheet);
        public abstract ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, XlCellRange range);
        public virtual ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, IDefinedName definedName)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            Guard.ArgumentNotNull(definedName, "definedName");
            XlCellRange range = definedName.Range;
            if (range == null)
            {
                throw new InvalidOperationException("Unable to determine source range.");
            }
            if (!string.IsNullOrEmpty(range.SheetName) && (worksheet.Name != range.SheetName))
            {
                throw new InvalidOperationException("Defined name refers to another worksheet.");
            }
            if (!string.IsNullOrEmpty(definedName.Scope) && (worksheet.Name != definedName.Scope))
            {
                throw new InvalidOperationException("Defined name scoped to another worksheet.");
            }
            return this.GetDataReader(worksheet, range);
        }

        protected string FileName =>
            this.fileName;

        protected Stream InputStream =>
            this.inputStream;

        protected internal WorksheetCollection InnerWorksheets =>
            this.worksheets;

        protected internal DefinedNamesCollection InnerDefinedNames =>
            this.definedNames;

        protected internal TablesCollection InnerTables =>
            this.tables;

        protected internal Dictionary<int, string> NumberFormatCodes =>
            this.numberFormatCodes;

        protected internal List<int> NumberFormatIds =>
            this.numberFormatIds;

        protected internal ChunkedArray<string> SharedStrings =>
            this.sharedStrings;

        public IWorksheetCollection Worksheets =>
            this.worksheets;

        public IDefinedNamesCollection DefinedNames =>
            this.definedNames;

        public ITablesCollection Tables =>
            this.tables;

        public ISpreadsheetSourceOptions Options =>
            this.options;

        public abstract SpreadsheetDocumentFormat DocumentFormat { get; }

        public abstract int MaxColumnCount { get; }

        public abstract int MaxRowCount { get; }
    }
}

