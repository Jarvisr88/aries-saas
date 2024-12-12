namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlsSpreadsheetSource : SpreadsheetSourceBase
    {
        private readonly Stack<XlsContentType> contentTypes;
        private readonly Stack<IXlsSourceDataCollector> dataCollectors;
        private readonly List<string> sheetNames;
        private readonly List<XlsXTI> externSheets;
        private int supBookCount;
        private int selfRefBookIndex;
        private readonly List<XlsDefinedNameInfo> definedNameInfos;
        private const string workbookStreamName = "Workbook";
        private const string bookStreamName = "Book";
        private PackageFileReader packageFileReader;
        private BinaryReader baseReader;
        private XlReader workbookReader;
        private IXlsSourceCommandFactory commandFactory;
        private XlsSourceDataReader dataReader;
        private IWorksheet currentSheet;
        private Encoding workbookEncoding;

        public XlsSpreadsheetSource(Stream stream, ISpreadsheetSourceOptions options) : base(stream, options)
        {
            this.contentTypes = new Stack<XlsContentType>();
            this.dataCollectors = new Stack<IXlsSourceDataCollector>();
            this.sheetNames = new List<string>();
            this.externSheets = new List<XlsXTI>();
            this.selfRefBookIndex = -2147483648;
            this.definedNameInfos = new List<XlsDefinedNameInfo>();
            this.workbookEncoding = XLStringEncoder.GetSingleByteEncoding();
            this.InitializeSource();
        }

        public XlsSpreadsheetSource(string fileName, ISpreadsheetSourceOptions options) : base(fileName, options)
        {
            this.contentTypes = new Stack<XlsContentType>();
            this.dataCollectors = new Stack<IXlsSourceDataCollector>();
            this.sheetNames = new List<string>();
            this.externSheets = new List<XlsXTI>();
            this.selfRefBookIndex = -2147483648;
            this.definedNameInfos = new List<XlsDefinedNameInfo>();
            this.workbookEncoding = XLStringEncoder.GetSingleByteEncoding();
            this.InitializeSource();
        }

        internal void ClearDataCollectors()
        {
            this.dataCollectors.Clear();
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
                this.commandFactory = null;
                this.CloseDataReader();
                if (this.workbookReader != null)
                {
                    this.workbookReader.Dispose();
                    this.workbookReader = null;
                }
                if (this.packageFileReader != null)
                {
                    this.packageFileReader.Dispose();
                    this.packageFileReader = null;
                }
            }
            base.Dispose(disposing);
        }

        internal void EndContent()
        {
            this.contentTypes.Pop();
        }

        private XlsContentType GetContentTypeBySubstreamType(XlsSubstreamType dataType)
        {
            if (dataType <= XlsSubstreamType.Sheet)
            {
                if (dataType == XlsSubstreamType.WorkbookGlobals)
                {
                    return XlsContentType.WorkbookGlobals;
                }
                if (dataType == XlsSubstreamType.VisualBasicModule)
                {
                    return XlsContentType.VisualBasicModule;
                }
                if (dataType == XlsSubstreamType.Sheet)
                {
                    return XlsContentType.Sheet;
                }
            }
            else
            {
                if (dataType == XlsSubstreamType.Chart)
                {
                    return ((this.ContentType != XlsContentType.Sheet) ? XlsContentType.ChartSheet : XlsContentType.Chart);
                }
                if (dataType == XlsSubstreamType.MacroSheet)
                {
                    return XlsContentType.MacroSheet;
                }
                if (dataType == XlsSubstreamType.Workspace)
                {
                    return XlsContentType.Workspace;
                }
            }
            return XlsContentType.None;
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new XlsSourceDataReader(this);
            this.dataReader.Open(worksheet, null);
            return this.dataReader;
        }

        public override ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, XlCellRange range)
        {
            Guard.ArgumentNotNull(worksheet, "worksheet");
            this.CloseDataReader();
            this.dataReader = new XlsSourceDataReader(this);
            this.dataReader.Open(worksheet, range);
            return this.dataReader;
        }

        protected internal string GetScopeSheetName(int sheetIndex)
        {
            int num = sheetIndex - 1;
            if ((num < 0) || (num >= this.SheetNames.Count))
            {
                return string.Empty;
            }
            string str = this.SheetNames[num];
            return ((base.Worksheets[str] != null) ? str : string.Empty);
        }

        private void InitializeSource()
        {
            this.InitializeWorkbookReader();
            this.ReadWorkbookGlobals();
            this.RegisterDefinedNames();
            this.ReadTableDefinitions();
        }

        private void InitializeWorkbookReader()
        {
            this.packageFileReader = new PackageFileReader(base.InputStream, true);
            this.baseReader = this.packageFileReader.GetCachedPackageFileReader("Workbook");
            if (this.baseReader != null)
            {
                this.workbookReader = new XlReader(this.baseReader);
                this.commandFactory = new XlsSourceCommandFactory();
            }
            else
            {
                this.baseReader = this.packageFileReader.GetCachedPackageFileReader("Book");
                if (this.baseReader == null)
                {
                    throw new InvalidFileException(InvalidFileError.WrongFormat, "Workbook stream not found!");
                }
                this.workbookReader = new XlReader(this.baseReader);
                this.commandFactory = new XlsSourceCommandFactory5();
            }
        }

        internal void PopDataCollector()
        {
            if (this.dataCollectors.Count > 0)
            {
                this.dataCollectors.Pop();
            }
        }

        internal void PushDataCollector(IXlsSourceDataCollector collector)
        {
            Guard.ArgumentNotNull(collector, "collector");
            this.dataCollectors.Push(collector);
        }

        protected internal string ReadString(XlReader reader)
        {
            int count = reader.ReadByte();
            byte[] bytes = reader.ReadBytes(count);
            return this.WorkbookEncoding.GetString(bytes, 0, count);
        }

        protected internal string ReadString2(XlReader reader)
        {
            int count = reader.ReadByte();
            reader.ReadByte();
            byte[] bytes = reader.ReadBytes(count);
            return this.WorkbookEncoding.GetString(bytes, 0, count);
        }

        protected internal string ReadStringNoCch(XlReader reader, int length)
        {
            byte[] bytes = reader.ReadBytes(length);
            return this.WorkbookEncoding.GetString(bytes, 0, length);
        }

        private void ReadTableDefinitions()
        {
            foreach (IWorksheet worksheet in base.InnerWorksheets)
            {
                this.ReadTableDefinitions(worksheet as XlsWorksheet);
            }
        }

        private void ReadTableDefinitions(XlsWorksheet worksheet)
        {
            if (worksheet != null)
            {
                this.WorkbookReader.Position = worksheet.StartPosition;
                XlsSourceCommandBOF dbof = this.commandFactory.CreateCommand(this.WorkbookReader) as XlsSourceCommandBOF;
                if (dbof != null)
                {
                    dbof.Read(this.WorkbookReader, this);
                    dbof.Execute(this);
                    this.currentSheet = worksheet;
                    try
                    {
                        while (this.WorkbookReader.Position != this.WorkbookReader.StreamLength)
                        {
                            IXlsSourceCommand command = this.commandFactory.CreateCommand(this.WorkbookReader);
                            if (!(command is XlsSourceCommandFeature11) && !command.IsSubstreamBound)
                            {
                                this.commandFactory.CreateCommand(0).Read(this.WorkbookReader, this);
                            }
                            else
                            {
                                command.Read(this.WorkbookReader, this);
                                command.Execute(this);
                            }
                            if (this.ContentType == XlsContentType.None)
                            {
                                break;
                            }
                        }
                    }
                    finally
                    {
                        this.currentSheet = null;
                    }
                }
            }
        }

        private void ReadWorkbookGlobals()
        {
            while (this.WorkbookReader.Position != this.WorkbookReader.StreamLength)
            {
                XlsSourceCommandBOF dbof = this.commandFactory.CreateCommand(this.WorkbookReader) as XlsSourceCommandBOF;
                if (dbof != null)
                {
                    dbof.Read(this.WorkbookReader, this);
                    dbof.Execute(this);
                    while (this.WorkbookReader.Position != this.WorkbookReader.StreamLength)
                    {
                        IXlsSourceCommand command = this.commandFactory.CreateCommand(this.WorkbookReader);
                        command.Read(this.WorkbookReader, this);
                        command.Execute(this);
                        if (this.ContentType == XlsContentType.None)
                        {
                            return;
                        }
                    }
                }
            }
            this.contentTypes.Clear();
        }

        private void RegisterDefinedName(XlsDefinedNameInfo info)
        {
            if ((info.SheetIndex >= 0) && (info.SheetIndex < this.SheetNames.Count))
            {
                string str = this.SheetNames[info.SheetIndex];
                if (base.Worksheets[str] != null)
                {
                    string scopeSheetName = this.GetScopeSheetName(info.ScopeSheetIndex);
                    if (!string.IsNullOrEmpty(str) || !string.IsNullOrEmpty(scopeSheetName))
                    {
                        XlCellRange range = info.Range;
                        range.SheetName = str;
                        DefinedName item = new DefinedName(info.Name, scopeSheetName, info.IsHidden, range, range.ToString(true));
                        base.InnerDefinedNames.Add(item);
                    }
                }
            }
        }

        private void RegisterDefinedNames()
        {
            foreach (XlsDefinedNameInfo info in this.DefinedNameInfos)
            {
                this.RegisterDefinedName(info);
            }
        }

        internal void SetupRC4Decryptor(string password, byte[] salt)
        {
            this.workbookReader.Dispose();
            this.workbookReader = new XlsRC4EncryptedReader(this.baseReader, password, salt);
        }

        internal void SetupXORDecryptor(string password, int key)
        {
            this.workbookReader.Dispose();
            this.workbookReader = new XlsXORObfuscatedReader(this.baseReader, password, key);
        }

        internal void StartContent(XlsContentType contentType)
        {
            this.contentTypes.Push(contentType);
        }

        internal void StartContent(XlsSubstreamType substreamType)
        {
            this.StartContent(this.GetContentTypeBySubstreamType(substreamType));
        }

        internal XlsContentType ContentType =>
            (this.contentTypes.Count != 0) ? this.contentTypes.Peek() : XlsContentType.None;

        internal bool IsDate1904 { get; set; }

        internal IXlsSourceDataCollector DataCollector =>
            (this.dataCollectors.Count != 0) ? this.dataCollectors.Peek() : null;

        internal List<string> SheetNames =>
            this.sheetNames;

        internal List<XlsXTI> ExternSheets =>
            this.externSheets;

        internal int SupBookCount
        {
            get => 
                this.supBookCount;
            set => 
                this.supBookCount = value;
        }

        internal int SelfRefBookIndex
        {
            get => 
                this.selfRefBookIndex;
            set => 
                this.selfRefBookIndex = value;
        }

        internal List<XlsDefinedNameInfo> DefinedNameInfos =>
            this.definedNameInfos;

        protected internal XlReader WorkbookReader =>
            this.workbookReader;

        protected internal IXlsSourceCommandFactory CommandFactory =>
            this.commandFactory;

        protected internal XlsSourceDataReader DataReader =>
            this.dataReader;

        protected internal IWorksheet CurrentSheet =>
            this.currentSheet;

        protected internal Encoding WorkbookEncoding
        {
            get => 
                this.workbookEncoding;
            set
            {
                Encoding singleByteEncoding = value;
                if (value == null)
                {
                    Encoding local1 = value;
                    singleByteEncoding = XLStringEncoder.GetSingleByteEncoding();
                }
                this.workbookEncoding = singleByteEncoding;
            }
        }

        public override SpreadsheetDocumentFormat DocumentFormat =>
            SpreadsheetDocumentFormat.Xls;

        public override int MaxColumnCount =>
            0x100;

        public override int MaxRowCount =>
            0x10000;
    }
}

