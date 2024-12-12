namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.SpreadsheetSource.Xlsx;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Xml;

    public class XlsxSpreadsheetSourceImporter : XlsxImporter
    {
        private const int defaultFirstRowIndex = -1;
        private const int defaultFirstCellIndex = -1;
        private const string relsTableType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table";
        private readonly XlsxSpreadsheetSource source;
        private XmlReader worksheetXmlReader;
        private int lastRowIndex = -1;
        private int lastCellIndex = -1;

        public XlsxSpreadsheetSourceImporter(XlsxSpreadsheetSource source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.source = source;
        }

        protected internal override void AfterImportMainDocument()
        {
            this.RemoveNonWorksheets();
            this.ImportStyles();
            this.ImportSharedStrings();
            this.ImportTables();
        }

        private string BuildPathToRelation(string fileName) => 
            Path.GetDirectoryName(fileName) + "/_rels/" + Path.GetFileName(fileName) + ".rels";

        internal void CloseWorksheetReader()
        {
            if (this.worksheetXmlReader != null)
            {
                this.worksheetXmlReader.Close();
                this.worksheetXmlReader = null;
            }
        }

        protected internal override Destination CreateMainDocumentDestination() => 
            new DocumentDestination(this);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.CloseWorksheetReader();
            }
            base.Dispose(disposing);
        }

        private int GetCellIndex(string reference)
        {
            int num = string.IsNullOrEmpty(reference) ? (this.lastCellIndex + 1) : XlCellReferenceParser.Parse(reference).Column;
            this.lastCellIndex = num;
            return num;
        }

        internal int GetCurrentCellIndex() => 
            this.lastCellIndex;

        private string GetWorksheetFileName(string sheetRelationId)
        {
            OpenXmlRelation relationById = base.DocumentRelations.LookupRelationById(sheetRelationId);
            if (relationById == null)
            {
                base.ThrowInvalidFile("Can't find worksheet relation");
            }
            return base.CalculateRelationTarget(relationById, base.DocumentRootFolder, string.Empty);
        }

        private XmlReader GetWorksheetXmlReader(string sheetRelationId)
        {
            string worksheetFileName = this.GetWorksheetFileName(sheetRelationId);
            XmlReader packageFileXmlReaderBasedSeekableStream = base.GetPackageFileXmlReaderBasedSeekableStream(worksheetFileName);
            return (this.ReadToRootElement(packageFileXmlReaderBasedSeekableStream, "worksheet", base.SpreadsheetNamespace) ? packageFileXmlReaderBasedSeekableStream : null);
        }

        private void ImportSharedStrings()
        {
            string fileName = base.LookupRelationTargetByType(base.DocumentRelations, base.RelsSharedStringsNamespace, base.DocumentRootFolder, "sharedStrings.xml");
            XmlReader packageFileXmlReader = base.GetPackageFileXmlReader(fileName);
            if ((packageFileXmlReader != null) && this.ReadToRootElement(packageFileXmlReader, "sst", base.SpreadsheetNamespace))
            {
                this.ImportContent(packageFileXmlReader, new SharedStringsDestination(this));
            }
        }

        private void ImportStyles()
        {
            string fileName = base.LookupRelationTargetByType(base.DocumentRelations, base.RelsStylesNamespace, base.DocumentRootFolder, "styles.xml");
            XmlReader packageFileXmlReader = base.GetPackageFileXmlReader(fileName);
            if ((packageFileXmlReader != null) && this.ReadToRootElement(packageFileXmlReader, "styleSheet", base.SpreadsheetNamespace))
            {
                this.ImportContent(packageFileXmlReader, new StylesDestination(this));
            }
        }

        private void ImportTables()
        {
            int count = this.Source.InnerWorksheets.Count;
            for (int i = 0; i < count; i++)
            {
                XlsxWorksheet sheet = (XlsxWorksheet) this.Source.InnerWorksheets[i];
                this.ImportTables(sheet);
            }
        }

        private void ImportTables(XlsxWorksheet sheet)
        {
            string worksheetFileName = this.GetWorksheetFileName(sheet.RelationId);
            worksheetFileName = this.BuildPathToRelation(worksheetFileName);
            try
            {
                base.DocumentRelationsStack.Push(base.ImportRelations(worksheetFileName));
                foreach (OpenXmlRelation relation in base.DocumentRelations)
                {
                    if (relation.Type == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table")
                    {
                        this.ReadTableContentCore(relation.Id, sheet.Name);
                    }
                }
            }
            finally
            {
                base.DocumentRelationsStack.Pop();
            }
        }

        internal void PrepareBeforeReadSheet(string relationId)
        {
            this.worksheetXmlReader = this.GetWorksheetXmlReader(relationId);
            this.lastRowIndex = -1;
        }

        internal void ReadCell()
        {
            WorksheetCellDestination instance = WorksheetCellDestination.GetInstance(this);
            instance.ProcessElementOpen(this.worksheetXmlReader);
            base.ImportContent(this.worksheetXmlReader, instance, new XmlContentImporter(this.ReadToEndCell));
        }

        internal int ReadCellIndex()
        {
            string reference = this.ReadAttribute(this.worksheetXmlReader, "r");
            return this.GetCellIndex(reference);
        }

        internal XlsxRowAttributes ReadRowAttributes()
        {
            int num = base.GetWpSTIntegerValue(this.worksheetXmlReader, "r", -2147483648);
            num = (num == -2147483648) ? (this.lastRowIndex + 1) : (num - 1);
            this.lastRowIndex = num;
            bool flag = base.GetWpSTOnOffValue(this.worksheetXmlReader, "hidden", false);
            int wpSTIntegerValue = base.GetWpSTIntegerValue(this.worksheetXmlReader, "s");
            XlsxRowAttributes attributes1 = new XlsxRowAttributes();
            attributes1.Index = num;
            attributes1.IsHidden = flag;
            attributes1.StyleIndex = wpSTIntegerValue;
            return attributes1;
        }

        private void ReadTableContentCore(string tableRelationId, string sheetName)
        {
            string fileName = base.LookupRelationTargetById(base.DocumentRelations, tableRelationId, base.DocumentRootFolder, string.Empty);
            XmlReader packageFileXmlReader = base.GetPackageFileXmlReader(fileName);
            if (this.ReadToRootElement(packageFileXmlReader, "table", base.SpreadsheetNamespace))
            {
                string str2 = this.ReadAttribute(packageFileXmlReader, "displayName");
                if (string.IsNullOrEmpty(str2))
                {
                    base.ThrowInvalidFile("Table display name is null or empty");
                }
                XlCellRange range = XlRangeReferenceParser.Parse(this.ReadAttribute(packageFileXmlReader, "ref"));
                range.SheetName = sheetName;
                Table item = new Table(str2, range, base.GetIntegerValue(packageFileXmlReader, "headerRowCount", 1) > 0, base.GetIntegerValue(packageFileXmlReader, "totalsRowCount", 0) > 0);
                this.Source.InnerTables.Add(item);
            }
        }

        private void ReadToEndCell(XmlReader reader)
        {
            while ((reader.LocalName != "c") || ((reader.NodeType != XmlNodeType.EndElement) && !reader.IsEmptyElement))
            {
                reader.Read();
                this.ProcessCurrentDestination(reader);
            }
        }

        private void ReadToEndColumns(XmlReader reader)
        {
            this.ReadToEndElement(reader, "cols");
        }

        private void ReadToEndElement(XmlReader reader, string elementName)
        {
            while (true)
            {
                reader.Read();
                if ((reader.NodeType == XmlNodeType.EndElement) && (reader.LocalName == elementName))
                {
                    return;
                }
                this.ProcessCurrentDestination(reader);
            }
        }

        internal bool ReadToNextCell() => 
            this.ReadToNextElement(this.worksheetXmlReader, "c");

        private bool ReadToNextElement(XmlReader reader, string elementName)
        {
            if ((reader.NodeType == XmlNodeType.Element) && ((reader.LocalName == elementName) && !reader.IsEmptyElement))
            {
                return true;
            }
            reader.Read();
            return ((reader.NodeType == XmlNodeType.Element) && (reader.LocalName == elementName));
        }

        internal bool ReadToNextRow()
        {
            this.ResetFirstCellIndex();
            if (this.ReadToNextElement(this.worksheetXmlReader, "row"))
            {
                return true;
            }
            WorksheetCellDestination.ClearInstance();
            return false;
        }

        internal void ReadWorksheetColumns()
        {
            this.worksheetXmlReader.Read();
            do
            {
                if (this.worksheetXmlReader.NodeType == XmlNodeType.Element)
                {
                    if (this.worksheetXmlReader.LocalName == "cols")
                    {
                        base.ImportContent(this.worksheetXmlReader, new WorksheetColumnsDestination(this), new XmlContentImporter(this.ReadToEndColumns));
                    }
                    else if (this.worksheetXmlReader.LocalName == "sheetData")
                    {
                        break;
                    }
                }
                this.worksheetXmlReader.Skip();
            }
            while ((this.worksheetXmlReader.ReadState != System.Xml.ReadState.EndOfFile) && (this.worksheetXmlReader.ReadState != System.Xml.ReadState.Error));
        }

        private void RemoveNonWorksheets()
        {
            for (int i = this.Source.InnerWorksheets.Count - 1; i >= 0; i--)
            {
                XlsxWorksheet worksheet = (XlsxWorksheet) this.Source.InnerWorksheets[i];
                OpenXmlRelation relationById = base.DocumentRelations.LookupRelationById(worksheet.RelationId);
                if (relationById.Type != base.RelsWorksheetNamespace)
                {
                    this.Source.InnerWorksheets.RemoveAt(i);
                }
            }
        }

        private void ResetFirstCellIndex()
        {
            this.lastCellIndex = -1;
        }

        internal void SkipToNextCell()
        {
            if ((this.worksheetXmlReader.NodeType == XmlNodeType.Element) && (this.worksheetXmlReader.LocalName == "c"))
            {
                this.worksheetXmlReader.Skip();
            }
        }

        internal void SkipToNextRow()
        {
            if ((this.worksheetXmlReader.NodeType == XmlNodeType.Element) && (this.worksheetXmlReader.LocalName == "row"))
            {
                this.worksheetXmlReader.Skip();
            }
            else
            {
                while (true)
                {
                    this.worksheetXmlReader.Skip();
                    if ((this.worksheetXmlReader.LocalName == "row") && (this.worksheetXmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        return;
                    }
                }
            }
        }

        public XlsxSpreadsheetSource Source =>
            this.source;

        public override string EncryptionPassword
        {
            get => 
                this.source.Options.Password;
            set => 
                this.source.Options.Password = value;
        }
    }
}

