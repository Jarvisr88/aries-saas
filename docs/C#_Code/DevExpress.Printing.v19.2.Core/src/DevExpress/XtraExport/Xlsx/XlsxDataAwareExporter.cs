namespace DevExpress.XtraExport.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.Office;
    using DevExpress.Office.Crypto;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using DevExpress.Utils.StructuredStorage.Internal.Writer;
    using DevExpress.Utils.StructuredStorage.Writer;
    using DevExpress.Utils.Zip;
    using DevExpress.XtraExport;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraExport.Xls;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class XlsxDataAwareExporter : IXlShapeContainer, IXlExportEx, IXlExport, IXlFormulaEngine, IXlExporter, IXlCellFormatter
    {
        private Dictionary<XlDynamicFilterType, string> dynamicFilterTypeTable;
        private Dictionary<XlFilterOperator, string> filterOperatorTable;
        private Dictionary<XlCalendarType, string> calendarTypeTable;
        private Dictionary<XlDateTimeGroupingType, string> dateTimeGroupingTable;
        private const string documentApplicationPropertiesContentType = "application/vnd.openxmlformats-officedocument.extended-properties+xml";
        private const string documentCorePropertiesContentType = "application/vnd.openxmlformats-package.core-properties+xml";
        private const string extendedPropertiesNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties";
        private const string corePropertiesNamespace = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";
        private const string customPropertiesNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties";
        private const string corePropertiesPrefix = "cp";
        private const string dcPropertiesPrefix = "dc";
        private const string dcPropertiesNamespace = "http://purl.org/dc/elements/1.1/";
        private const string dcTermsPropertiesPrefix = "dcterms";
        private const string dcTermsPropertiesNamespace = "http://purl.org/dc/terms/";
        private const string xsiPrefix = "xsi";
        private const string xsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        private const string documentCustomPropertiesContentType = "application/vnd.openxmlformats-officedocument.custom-properties+xml";
        private const string variantTypePrefix = "vt";
        private const string variantTypeNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes";
        private static Dictionary<XlGeometryPreset, string> geometryPresetTable = CreateGeometryPresetTable();
        private static Dictionary<XlThemeColor, string> schemeColorTable = CreateSchemeColorTable();
        private static Dictionary<XlOutlineDashing, string> presetDashTable = CreatePresetDashTable();
        private static Dictionary<XlOutlineCompoundType, string> compoundTypeTable = CreateCompoundTypeTable();
        private static Dictionary<XlLineCapType, string> capTypeTable = CreateCapTypeTable();
        private static Dictionary<XlSparklineType, string> sparklineTypeTable = CreateSparklineTypeTable();
        private static Dictionary<XlSparklineAxisScaling, string> sparklineAxisScalingTable = CreateSparklineAxisScalingTable();
        private static Dictionary<XlDisplayBlanksAs, string> displayBlanksAsTable = CreateDisplayBlanksAsTable();
        private const string SparklineGroupsExtUri = "{05C60535-1F16-4fd2-B633-F4F36F0B64E0}";
        private const string relsTableType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table";
        private const string tableContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml";
        private int tableId;
        private static readonly Dictionary<XlTotalRowFunction, string> totalRowFunctionTable = CreateTotalRowFunctionTable();
        private StringBuilder cellRefBuilder;
        private XlCell currentCell;
        private XlCellFormatting inheritedFormatting;
        private readonly XlExpressionContext expressionContext;
        private readonly Dictionary<XlCellPosition, int> sharedFormulaTable;
        private bool insideCellScope;
        private readonly List<XlCell> rowCells;
        private int columnIndex;
        private readonly List<XlColumn> pendingColumns;
        private readonly Dictionary<int, IXlColumn> columns;
        private XlColumn currentColumn;
        private static Dictionary<XlCondFmtType, string> conditionalFormattingTypeTable = CreateConditionalFormattingTypeTable();
        private static Dictionary<XlCondFmtOperator, string> conditionalFormattingOperatorTable = CreateConditionalFormattingOperatorTable();
        private static Dictionary<XlCondFmtValueObjectType, string> conditionalFormattingValueTypeTable = CreateConditionalFormattingValueTypeTable();
        private static Dictionary<XlCondFmtIconSetType, string> iconSetTypeTable = CreateIconSetTypeTable();
        private static Dictionary<XlCondFmtTimePeriod, string> timePeriodTable = CreateTimePeriodTable();
        private const string condFmtExtRefUri = "{B025F937-C7B1-47D3-B67F-A62EFF666E3E}";
        private const string condFmtExtUri = "{78C0D931-6437-407d-A8EE-F0AAD7539E65}";
        private const string x14NamespaceReference = "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main";
        private const string xmNamespaceReference = "http://schemas.microsoft.com/office/excel/2006/main";
        private static Dictionary<XlDataValidationType, string> dataValidationTypeTable = CreateDataValidationTypeTable();
        private static Dictionary<XlDataValidationOperator, string> dataValidationOperatorTable = CreateDataValidationOperatorTable();
        private static Dictionary<XlDataValidationErrorStyle, string> dataValidationErrorStyleTable = CreateDataValidationErrorStyleTable();
        private static Dictionary<XlDataValidationImeMode, string> dataValidationImeModeTable = CreateDataValidationImeModeTable();
        private const string DataValidationExtUri = "{CCE6A557-97BC-4b89-ADB6-D9C93CAAB3DF}";
        private readonly Stack<XlGroup> groups;
        private XlGroup currentGroup;
        private const string officeHyperlinkType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";
        private static readonly Dictionary<XlAnchorType, string> anchorTypeTable = CreateAnchorTypeTable();
        private const string relsImageType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";
        private const string relsDrawingType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing";
        private const string drawingContentType = "application/vnd.openxmlformats-officedocument.drawing+xml";
        private const string spreadsheetDrawingNamespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing";
        private const string drawingMLNamespace = "http://schemas.openxmlformats.org/drawingml/2006/main";
        private int shapeId;
        private int drawingId;
        private int imageId;
        private readonly OpenXmlRelationCollection drawingRelations;
        private readonly List<XlDrawingObjectBase> drawingObjects;
        private readonly List<XlsxPictureFileInfo> imageFiles;
        private readonly Dictionary<string, string> exportedPictureHyperlinkTable;
        private static readonly Dictionary<ImageFormat, string> imageContentTypeTable = CreateImageContentTypeTable();
        private static readonly Dictionary<ImageFormat, string> imageExtenstionTable = CreateImageExtenstionTable();
        private XlPicture currentPicture;
        private int rowIndex;
        private XlRow pendingRow;
        private IXlRow currentRow;
        private readonly Dictionary<XlFont, int> fontsTable;
        private readonly Dictionary<XlFill, int> fillsTable;
        private readonly Dictionary<XlBorder, int> bordersTable;
        private readonly Dictionary<XlCellAlignment, int> alignmentTable;
        private readonly List<XlCellAlignment> alignmentList;
        private readonly Dictionary<string, ExcelNumberFormat> numberFormatsTable;
        private readonly Dictionary<XlNetNumberFormat, ExcelNumberFormat> netNumberFormatsTable;
        private readonly Dictionary<XlCellXf, int> cellXfTable;
        private readonly XlExportNumberFormatConverter numberFormatConverter;
        private int customNumberFormatId;
        private XlFont defaultFont;
        private XlCellAlignment defaultAlignment;
        private XlBorder defaultBorder;
        private readonly Dictionary<XlDxf, int> dxfTable;
        internal static readonly Dictionary<XlUnderlineType, string> underlineTypeTable = CreateUnderlineTypeTable();
        private static Dictionary<XlScriptType, string> verticalAlignmentRunTypeTable = CreateVerticalAlignmentRunTypeTable();
        private static Dictionary<XlFontSchemeStyles, string> fontSchemeStyleTable = CreateFontSchemeStyleTable();
        internal static readonly Dictionary<XlPatternType, string> patternTypeTable = CreatePatternTypeTable();
        internal static readonly Dictionary<XlHorizontalAlignment, string> horizontalAlignmentTable = CreateHorizontalAlignmentTable();
        internal static readonly Dictionary<XlVerticalAlignment, string> verticalAlignmentTable = CreateVerticalAlignmentTable();
        internal static readonly Dictionary<XlReadingOrder, string> readingOrderTable = CreateReadingOrderTable();
        internal static readonly Dictionary<XlBorderLineStyle, string> borderLineStylesTable = CreateBorderLineStylesTable();
        public static Dictionary<XlSheetVisibleState, string> VisibilityTypeTable = CreateVisibilityTypeTable();
        private XlDocument currentDocument;
        private XlDocumentProperties documentProperties;
        private XlCalculationOptions calculationOptions;
        private static readonly Dictionary<XlCommentsPrintMode, string> commentsPrintModeTable = CreateCommentsPrintModeTable();
        private static readonly Dictionary<XlErrorsPrintMode, string> errorsPrintModeTable = CreateErrorsPrintModeTable();
        private static readonly Dictionary<XlPagePrintOrder, string> pagePrintOrderTable = CreatePagePrintOrderTable();
        private static readonly Dictionary<XlPageOrientation, string> pageOrientationTable = CreatePageOrientationTable();
        private XlSheet currentSheet;
        private XlSheet pendingSheet;
        private readonly OpenXmlRelationCollection sheetRelations;
        private bool rowContentStarted;
        private XlsxPackageBuilder builder;
        private XmlWriter writer;
        private int sheetIndex;
        private Stream outputStream;
        private Stream bufferStream;
        private EncryptionSession encryptionSession;
        private Stream encryptionInfoStream;
        private Stream encryptedPackageStream;
        private readonly List<IXlSheet> sheets;
        private readonly Dictionary<IXlSheet, SheetInfo> sheetInfos;
        private readonly Stack<CompressedXmlStreamInfo> streamInfoStack;
        private readonly SimpleSharedStringTable sharedStringTable;
        private readonly XlsxDataAwareExporterOptions options;
        private readonly IXlFormulaParser formulaParser;
        private XlCellFormatter cellFormatter;

        public XlsxDataAwareExporter()
        {
            this.cellRefBuilder = new StringBuilder(10);
            this.currentCell = new XlCell();
            this.expressionContext = new XlExpressionContext();
            this.sharedFormulaTable = new Dictionary<XlCellPosition, int>();
            this.rowCells = new List<XlCell>();
            this.columnIndex = -1;
            this.pendingColumns = new List<XlColumn>();
            this.columns = new Dictionary<int, IXlColumn>();
            this.groups = new Stack<XlGroup>();
            this.drawingRelations = new OpenXmlRelationCollection();
            this.drawingObjects = new List<XlDrawingObjectBase>();
            this.imageFiles = new List<XlsxPictureFileInfo>();
            this.exportedPictureHyperlinkTable = new Dictionary<string, string>();
            this.rowIndex = -1;
            this.fontsTable = new Dictionary<XlFont, int>();
            this.fillsTable = new Dictionary<XlFill, int>();
            this.bordersTable = new Dictionary<XlBorder, int>();
            this.alignmentTable = new Dictionary<XlCellAlignment, int>();
            this.alignmentList = new List<XlCellAlignment>();
            this.numberFormatsTable = new Dictionary<string, ExcelNumberFormat>();
            this.netNumberFormatsTable = new Dictionary<XlNetNumberFormat, ExcelNumberFormat>();
            this.cellXfTable = new Dictionary<XlCellXf, int>();
            this.numberFormatConverter = new XlExportNumberFormatConverter();
            this.dxfTable = new Dictionary<XlDxf, int>();
            this.sheetRelations = new OpenXmlRelationCollection();
            this.sheets = new List<IXlSheet>();
            this.sheetInfos = new Dictionary<IXlSheet, SheetInfo>();
            this.streamInfoStack = new Stack<CompressedXmlStreamInfo>();
            this.sharedStringTable = new SimpleSharedStringTable();
            this.options = new XlsxDataAwareExporterOptions();
            this.formulaParser = null;
        }

        public XlsxDataAwareExporter(IXlFormulaParser formulaParser)
        {
            this.cellRefBuilder = new StringBuilder(10);
            this.currentCell = new XlCell();
            this.expressionContext = new XlExpressionContext();
            this.sharedFormulaTable = new Dictionary<XlCellPosition, int>();
            this.rowCells = new List<XlCell>();
            this.columnIndex = -1;
            this.pendingColumns = new List<XlColumn>();
            this.columns = new Dictionary<int, IXlColumn>();
            this.groups = new Stack<XlGroup>();
            this.drawingRelations = new OpenXmlRelationCollection();
            this.drawingObjects = new List<XlDrawingObjectBase>();
            this.imageFiles = new List<XlsxPictureFileInfo>();
            this.exportedPictureHyperlinkTable = new Dictionary<string, string>();
            this.rowIndex = -1;
            this.fontsTable = new Dictionary<XlFont, int>();
            this.fillsTable = new Dictionary<XlFill, int>();
            this.bordersTable = new Dictionary<XlBorder, int>();
            this.alignmentTable = new Dictionary<XlCellAlignment, int>();
            this.alignmentList = new List<XlCellAlignment>();
            this.numberFormatsTable = new Dictionary<string, ExcelNumberFormat>();
            this.netNumberFormatsTable = new Dictionary<XlNetNumberFormat, ExcelNumberFormat>();
            this.cellXfTable = new Dictionary<XlCellXf, int>();
            this.numberFormatConverter = new XlExportNumberFormatConverter();
            this.dxfTable = new Dictionary<XlDxf, int>();
            this.sheetRelations = new OpenXmlRelationCollection();
            this.sheets = new List<IXlSheet>();
            this.sheetInfos = new Dictionary<IXlSheet, SheetInfo>();
            this.streamInfoStack = new Stack<CompressedXmlStreamInfo>();
            this.sharedStringTable = new SimpleSharedStringTable();
            this.options = new XlsxDataAwareExporterOptions();
            this.formulaParser = formulaParser;
        }

        private void AddContentTypes()
        {
            this.BeginWriteXmlContent();
            if (this.ShouldExportDocumentApplicationProperties())
            {
                this.Builder.OverriddenContentTypes.Add("/docProps/app.xml", "application/vnd.openxmlformats-officedocument.extended-properties+xml");
            }
            if (this.ShouldExportDocumentCoreProperties())
            {
                this.Builder.OverriddenContentTypes.Add("/docProps/core.xml", "application/vnd.openxmlformats-package.core-properties+xml");
            }
            if (this.ShouldExportDocumentCustomProperties())
            {
                this.Builder.OverriddenContentTypes.Add("/docProps/custom.xml", "application/vnd.openxmlformats-officedocument.custom-properties+xml");
            }
            this.Builder.GenerateContentTypesContent(this.writer);
            this.AddPackageContent("[Content_Types].xml", this.EndWriteXmlContent());
        }

        private void AddDocumentApplicationPropertiesContent()
        {
            if (this.ShouldExportDocumentApplicationProperties())
            {
                this.BeginWriteXmlContent();
                this.AddDocumentApplicationPropertiesContentCore();
                this.AddPackageContent(@"docProps\app.xml", this.EndWriteXmlContent());
            }
        }

        private void AddDocumentApplicationPropertiesContentCore()
        {
            this.WriteStartElement("Properties", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties");
            try
            {
                if (!string.IsNullOrEmpty(this.documentProperties.Application))
                {
                    this.WriteString("Application", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", this.EncodeXmlChars(this.documentProperties.Application));
                }
                if (this.documentProperties.Security != XlDocumentSecurity.None)
                {
                    this.WriteString("DocSecurity", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", ((int) this.documentProperties.Security).ToString());
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Manager))
                {
                    this.WriteString("Manager", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", this.EncodeXmlChars(this.documentProperties.Manager));
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Company))
                {
                    this.WriteString("Company", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", this.EncodeXmlChars(this.documentProperties.Company));
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Version))
                {
                    this.WriteString("AppVersion", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", this.EncodeXmlChars(this.documentProperties.Version));
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void AddDocumentCorePropertiesContent()
        {
            if (this.ShouldExportDocumentCoreProperties())
            {
                this.BeginWriteXmlContent();
                this.AddDocumentCorePropertiesContentCore();
                this.AddPackageContent(@"docProps\core.xml", this.EndWriteXmlContent());
            }
        }

        private void AddDocumentCorePropertiesContentCore()
        {
            this.WriteStartElement("cp", "coreProperties", "http://schemas.openxmlformats.org/package/2006/metadata/core-properties");
            try
            {
                this.WriteStringAttr("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
                this.WriteStringAttr("xmlns", "dcterms", null, "http://purl.org/dc/terms/");
                this.WriteStringAttr("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                if (!string.IsNullOrEmpty(this.documentProperties.Title))
                {
                    this.WriteString("dc", "title", this.EncodeXmlChars(this.documentProperties.Title), "http://purl.org/dc/elements/1.1/", false);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Subject))
                {
                    this.WriteString("dc", "subject", this.EncodeXmlChars(this.documentProperties.Subject), "http://purl.org/dc/elements/1.1/", false);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Author))
                {
                    this.WriteString("dc", "creator", this.EncodeXmlChars(this.documentProperties.Author), "http://purl.org/dc/elements/1.1/", false);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Keywords))
                {
                    this.WriteString("cp", "keywords", this.EncodeXmlChars(this.documentProperties.Keywords), "http://schemas.openxmlformats.org/package/2006/metadata/core-properties", false);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Description))
                {
                    this.WriteString("dc", "description", this.EncodeXmlChars(this.documentProperties.Description), "http://purl.org/dc/elements/1.1/", false);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Author))
                {
                    this.WriteString("cp", "lastModifiedBy", this.EncodeXmlChars(this.documentProperties.Author), "http://schemas.openxmlformats.org/package/2006/metadata/core-properties", false);
                }
                if (this.documentProperties.Created != DateTime.MinValue)
                {
                    this.WriteDcTermsDateTime("created", this.documentProperties.Created);
                    this.WriteDcTermsDateTime("modified", this.documentProperties.Created);
                }
                if (!string.IsNullOrEmpty(this.documentProperties.Category))
                {
                    this.WriteString("cp", "category", this.EncodeXmlChars(this.documentProperties.Category), "http://schemas.openxmlformats.org/package/2006/metadata/core-properties", false);
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void AddDocumentCustomPropertiesContent()
        {
            if (this.ShouldExportDocumentCustomProperties())
            {
                this.BeginWriteXmlContent();
                this.AddDocumentCustomPropertiesContentCore();
                this.AddPackageContent(@"docProps\custom.xml", this.EndWriteXmlContent());
            }
        }

        private void AddDocumentCustomPropertiesContentCore()
        {
            this.WriteStartElement("Properties", "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties");
            try
            {
                this.WriteStringAttr("xmlns", "vt", null, "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
                int index = 2;
                XlDocumentCustomProperties custom = this.documentProperties.Custom;
                foreach (string str in custom.Names)
                {
                    this.WriteDocumentCustomProperty(str, custom[str], index);
                    index++;
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void AddPackageContent(string fileName, CompressedStream content)
        {
            if (content != null)
            {
                this.Builder.Package.AddCompressed(fileName, this.Builder.Now, content);
            }
        }

        protected internal virtual void AddPackageContent(string fileName, Stream content)
        {
            if (content != null)
            {
                this.Builder.Package.Add(fileName, this.Builder.Now, content);
            }
        }

        private void AddPackageRelations()
        {
            this.BeginWriteXmlContent();
            OpenXmlRelationCollection relations = new OpenXmlRelationCollection();
            relations.Add(new OpenXmlRelation(relations.GenerateId(), "xl/workbook.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"));
            if (this.ShouldExportDocumentApplicationProperties())
            {
                relations.Add(new OpenXmlRelation(relations.GenerateId(), "docProps/app.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties"));
            }
            if (this.ShouldExportDocumentCoreProperties())
            {
                relations.Add(new OpenXmlRelation(relations.GenerateId(), "docProps/core.xml", "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties"));
            }
            if (this.ShouldExportDocumentCustomProperties())
            {
                relations.Add(new OpenXmlRelation(relations.GenerateId(), "docProps/custom.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties"));
            }
            this.Builder.GenerateRelationsContent(this.writer, relations);
            this.AddPackageContent(@"_rels\.rels", this.EndWriteXmlContent());
        }

        protected internal virtual void AddSharedStringContent()
        {
            if (this.ShouldExportSharedStrings())
            {
                this.BeginWriteXmlContent();
                this.WriteShStartElement("sst");
                try
                {
                    int count = this.sharedStringTable.Count;
                    IList<IXlString> stringList = this.sharedStringTable.StringList;
                    for (int i = 0; i < count; i++)
                    {
                        this.ExportSharedStringItem(stringList[i]);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
                this.AddPackageContent(@"xl\sharedStrings.xml", this.EndWriteXmlContent());
                this.Builder.OverriddenContentTypes.Add("/xl/sharedStrings.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml");
                this.Builder.WorkbookRelations.Add(new OpenXmlRelation(this.Builder.WorkbookRelations.GenerateId(), "sharedStrings.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings"));
            }
        }

        private void AddStylesContent()
        {
            this.BeginWriteXmlContent();
            this.WriteShStartElement("styleSheet");
            try
            {
                this.GenerateNumberFormatsContent();
                this.GenerateFontsContent();
                this.GenerateFillsContent();
                this.GenerateBordersContent();
                this.GenerateCellStyleFormatsContent();
                this.GenerateCellFormatsContent();
                this.GenerateCellStylesContent();
                this.GenerateDifferentialFormatsContent();
            }
            finally
            {
                this.WriteShEndElement();
            }
            this.AddPackageContent("xl/styles.xml", this.EndWriteXmlContent());
            this.Builder.OverriddenContentTypes.Add("/xl/styles.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml");
            this.Builder.WorkbookRelations.Add(new OpenXmlRelation(this.Builder.WorkbookRelations.GenerateId(), "styles.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"));
        }

        private void AddThemeContent()
        {
            if (this.currentDocument.Theme != XlDocumentTheme.None)
            {
                Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream((this.currentDocument.Theme == XlDocumentTheme.Office2010) ? "DevExpress.Printing.Export.Xlsx.Theme.theme2010.xml" : "DevExpress.Printing.Export.Xlsx.Theme.theme2013.xml");
                this.AddPackageContent(@"xl\theme\theme1.xml", manifestResourceStream);
                OpenXmlRelationCollection workbookRelations = this.Builder.WorkbookRelations;
                workbookRelations.Add(new OpenXmlRelation(workbookRelations.GenerateId(), "theme/theme1.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme"));
                this.Builder.OverriddenContentTypes.Add("/xl/theme/theme1.xml", "application/vnd.openxmlformats-officedocument.theme+xml");
            }
        }

        private void AddWorkbookContent()
        {
            this.BeginWriteXmlContent();
            this.WriteShStartElement("workbook");
            try
            {
                this.WriteStringAttr("xmlns", "r", null, "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                this.GenerateWorkbookProperties();
                this.GenerateWorkbookViews();
                this.GenerateSheetReferences();
                this.GenerateDefinedNames();
                this.GenerateCalculationProperties();
            }
            finally
            {
                this.WriteShEndElement();
            }
            this.AddPackageContent("xl/workbook.xml", this.EndWriteXmlContent());
            this.Builder.OverriddenContentTypes.Add("/xl/workbook.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml");
        }

        private void AddWorkbookRelations()
        {
            this.BeginWriteXmlContent();
            this.Builder.GenerateRelationsContent(this.writer, this.Builder.WorkbookRelations);
            this.AddPackageContent(@"xl\_rels\workbook.xml.rels", this.EndWriteXmlContent());
        }

        private void ApplySheetName(IXlSheet sheet, XlSparklineGroup sparklineGroup)
        {
            if ((sparklineGroup.DateRange != null) && string.IsNullOrEmpty(sparklineGroup.DateRange.SheetName))
            {
                sparklineGroup.DateRange.SheetName = sheet.Name;
            }
            foreach (XlSparkline sparkline in sparklineGroup.Sparklines)
            {
                if (string.IsNullOrEmpty(sparkline.DataRange.SheetName))
                {
                    sparkline.DataRange.SheetName = sheet.Name;
                }
            }
        }

        public IXlCell BeginCell()
        {
            IXlColumn column;
            this.currentCell = new XlCell();
            this.currentCell.RowIndex = this.rowIndex;
            this.currentCell.ColumnIndex = this.columnIndex;
            this.inheritedFormatting = !this.columns.TryGetValue(this.columnIndex, out column) ? XlFormatting.CopyObject<XlCellFormatting>(this.currentRow.Formatting) : XlCellFormatting.Merge(XlFormatting.CopyObject<XlCellFormatting>(column.Formatting), this.currentRow.Formatting);
            this.currentCell.Formatting = XlFormatting.CopyObject<XlCellFormatting>(this.inheritedFormatting);
            XlExpression calculatedColumnExpression = this.currentSheet.GetCalculatedColumnExpression(this.currentCell.ColumnIndex, this.currentCell.RowIndex);
            if (calculatedColumnExpression != null)
            {
                this.currentCell.SetFormula(calculatedColumnExpression);
            }
            this.insideCellScope = true;
            return this.currentCell;
        }

        public IXlColumn BeginColumn()
        {
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            this.currentColumn = new XlColumn(this.currentSheet);
            this.currentColumn.ColumnIndex = this.columnIndex;
            return this.currentColumn;
        }

        protected IXlDocument BeginDocument()
        {
            this.documentProperties = new XlDocumentProperties();
            this.documentProperties.Created = DateTimeHelper.Now;
            this.currentDocument = new XlDocument(this);
            this.calculationOptions = new XlCalculationOptions();
            return this.currentDocument;
        }

        public IXlDocument BeginExport(Stream outputStream) => 
            this.BeginExport(outputStream, null);

        public IXlDocument BeginExport(Stream outputStream, EncryptionOptions encryptionOptions)
        {
            Guard.ArgumentNotNull(outputStream, "outputStream");
            if (this.Builder != null)
            {
                throw new InvalidOperationException();
            }
            this.encryptionSession = null;
            this.encryptionInfoStream = null;
            this.encryptedPackageStream = null;
            if (encryptionOptions != null)
            {
                this.encryptionSession = EncryptionSession.CreateOpenXmlSession(encryptionOptions);
                this.encryptionInfoStream = new ChunkedMemoryStream();
                this.encryptedPackageStream = new ChunkedMemoryStream();
            }
            this.outputStream = outputStream;
            if (this.encryptionSession == null)
            {
                this.bufferStream = !outputStream.CanSeek ? new ChunkedMemoryStream() : outputStream;
            }
            else
            {
                string password = encryptionOptions.Password;
                if (string.IsNullOrEmpty(password))
                {
                    password = "VelvetSweatshop";
                }
                this.bufferStream = this.encryptionSession.BeginSession(password, this.encryptionInfoStream, this.encryptedPackageStream);
            }
            this.cellFormatter = new XlCellFormatter(this);
            this.builder = new XlsxPackageBuilder(this.bufferStream);
            this.Builder.BeginExport();
            this.InitializeExport();
            return this.BeginDocument();
        }

        public IXlGroup BeginGroup()
        {
            XlGroup item = new XlGroup();
            if (this.currentGroup == null)
            {
                item.OutlineLevel = 1;
            }
            else
            {
                item.OutlineLevel = this.currentGroup.OutlineLevel;
                item.IsCollapsed = this.currentGroup.IsCollapsed;
            }
            this.groups.Push(item);
            this.currentGroup = item;
            return item;
        }

        public IXlPicture BeginPicture()
        {
            this.CheckCurrentSheet();
            this.currentPicture = new XlPicture(this);
            this.currentPicture.Name = $"Picture {this.shapeId + 1}";
            this.currentPicture.Format = ImageFormat.Png;
            this.currentPicture.AnchorType = XlAnchorType.TwoCell;
            this.currentPicture.AnchorBehavior = XlAnchorType.OneCell;
            return this.currentPicture;
        }

        public IXlRow BeginRow()
        {
            this.ExportPendingSheet();
            this.columnIndex = 0;
            this.insideCellScope = false;
            this.rowCells.Clear();
            this.pendingRow = new XlRow(this);
            this.pendingRow.RowIndex = this.rowIndex;
            this.currentRow = this.pendingRow;
            this.rowContentStarted = true;
            this.currentSheet.ExtendTableRanges(this.rowIndex);
            this.WriteShStartElement("row");
            return this.pendingRow;
        }

        public IXlSheet BeginSheet()
        {
            this.BeginWriteXmlContent();
            this.WriteShStartElement("worksheet");
            this.WriteStringAttr("xmlns", "r", null, "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            XlSheet sheet = new XlSheet(this) {
                Name = $"Sheet{this.sheetIndex + 1}"
            };
            this.pendingSheet = sheet;
            this.currentSheet = sheet;
            this.rowIndex = 0;
            this.columnIndex = 0;
            this.groups.Clear();
            this.columns.Clear();
            this.currentGroup = null;
            this.sharedFormulaTable.Clear();
            this.rowContentStarted = false;
            return sheet;
        }

        private void BeginWriteXmlContent()
        {
            CompressedXmlStreamInfo streamInfo = XmlBasedExporterUtils.Instance.BeginCreateCompressedXmlContent(this.CreateXmlWriterSettings());
            this.BeginWriteXmlContentCore(streamInfo);
        }

        private void BeginWriteXmlContentCore(CompressedXmlStreamInfo streamInfo)
        {
            this.streamInfoStack.Push(streamInfo);
            this.writer = streamInfo.Writer;
        }

        private void CheckCurrentSheet()
        {
            if (this.currentSheet == null)
            {
                throw new InvalidOperationException("No current sheet!");
            }
        }

        protected internal virtual string ConvertARGBColorToString(Color value) => 
            $"{value.A:X2}{value.R:X2}{value.G:X2}{value.B:X2}";

        protected internal string ConvertBoolToString(bool value) => 
            value ? "1" : "0";

        private ExcelNumberFormat ConvertNetFormatStringToXlFormatCode(string netFormatString, bool isDateTimeFormatString)
        {
            CultureInfo culture = this.options.Culture;
            culture ??= CultureInfo.InvariantCulture;
            return this.numberFormatConverter.Convert(netFormatString, isDateTimeFormatString, culture);
        }

        private int CountItems(IList<XlDataValidation> validations, bool countExtItems)
        {
            int num = 0;
            int num2 = 0;
            foreach (XlDataValidation validation in validations)
            {
                if (validation.IsExtended)
                {
                    num2++;
                    continue;
                }
                num++;
            }
            return (countExtItems ? num2 : num);
        }

        private static Dictionary<XlAnchorType, string> CreateAnchorTypeTable() => 
            new Dictionary<XlAnchorType, string> { 
                { 
                    XlAnchorType.TwoCell,
                    "twoCell"
                },
                { 
                    XlAnchorType.OneCell,
                    "oneCell"
                },
                { 
                    XlAnchorType.Absolute,
                    "absolute"
                }
            };

        private static Dictionary<XlBorderLineStyle, string> CreateBorderLineStylesTable() => 
            new Dictionary<XlBorderLineStyle, string> { 
                { 
                    XlBorderLineStyle.DashDot,
                    "dashDot"
                },
                { 
                    XlBorderLineStyle.DashDotDot,
                    "dashDotDot"
                },
                { 
                    XlBorderLineStyle.Dashed,
                    "dashed"
                },
                { 
                    XlBorderLineStyle.Dotted,
                    "dotted"
                },
                { 
                    XlBorderLineStyle.Double,
                    "double"
                },
                { 
                    XlBorderLineStyle.Hair,
                    "hair"
                },
                { 
                    XlBorderLineStyle.Medium,
                    "medium"
                },
                { 
                    XlBorderLineStyle.MediumDashDot,
                    "mediumDashDot"
                },
                { 
                    XlBorderLineStyle.MediumDashDotDot,
                    "mediumDashDotDot"
                },
                { 
                    XlBorderLineStyle.MediumDashed,
                    "mediumDashed"
                },
                { 
                    XlBorderLineStyle.None,
                    "none"
                },
                { 
                    XlBorderLineStyle.SlantDashDot,
                    "slantDashDot"
                },
                { 
                    XlBorderLineStyle.Thick,
                    "thick"
                },
                { 
                    XlBorderLineStyle.Thin,
                    "thin"
                }
            };

        private static Dictionary<XlCalendarType, string> CreateCalendarTypeTable() => 
            new Dictionary<XlCalendarType, string> { 
                { 
                    XlCalendarType.None,
                    "none"
                },
                { 
                    XlCalendarType.Gregorian,
                    "gregorian"
                },
                { 
                    XlCalendarType.GregorianUS,
                    "gregorianUs"
                },
                { 
                    XlCalendarType.JapaneseEmperorEra,
                    "japan"
                },
                { 
                    XlCalendarType.TaiwanEra,
                    "taiwan"
                },
                { 
                    XlCalendarType.KoreanTangunEra,
                    "korea"
                },
                { 
                    XlCalendarType.ArabicLunar,
                    "hijri"
                },
                { 
                    XlCalendarType.Thai,
                    "thai"
                },
                { 
                    XlCalendarType.HebrewLunar,
                    "hebrew"
                },
                { 
                    XlCalendarType.GregorianMiddleEastFrench,
                    "gregorianMeFrench"
                },
                { 
                    XlCalendarType.GregorianArabic,
                    "gregorianArabic"
                },
                { 
                    XlCalendarType.GregorianTransliteratedEnglish,
                    "gregorianXlitEnglish"
                },
                { 
                    XlCalendarType.GregorianTransliteratedFrench,
                    "gregorianXlitFrench"
                }
            };

        private static Dictionary<XlLineCapType, string> CreateCapTypeTable() => 
            new Dictionary<XlLineCapType, string> { 
                { 
                    XlLineCapType.Flat,
                    "flat"
                },
                { 
                    XlLineCapType.Round,
                    "rnd"
                },
                { 
                    XlLineCapType.Square,
                    "sq"
                }
            };

        private XlDifferentialFormatting CreateColorFilterFormat(XlColorFilter filter)
        {
            XlDifferentialFormatting formatting = new XlDifferentialFormatting();
            XlColor rgb = filter.Color.Rgb;
            formatting.Fill = rgb.IsEmpty ? (!filter.FilterByCellColor ? XlFill.PatternFill(XlPatternType.Solid, XlColor.DefaultBackground, XlColor.Auto) : XlFill.PatternFill(XlPatternType.None, XlColor.DefaultBackground, XlColor.DefaultForeground)) : ((filter.PatternType != XlPatternType.Solid) ? XlFill.PatternFill(filter.PatternType, rgb, filter.PatternColor.Rgb) : XlFill.PatternFill(XlPatternType.Solid, XlColor.FromArgb(0xff, 0xff, 0xff), rgb));
            return formatting;
        }

        private static Dictionary<XlCommentsPrintMode, string> CreateCommentsPrintModeTable() => 
            new Dictionary<XlCommentsPrintMode, string> { 
                { 
                    XlCommentsPrintMode.None,
                    "none"
                },
                { 
                    XlCommentsPrintMode.AtEnd,
                    "atEnd"
                },
                { 
                    XlCommentsPrintMode.AsDisplayed,
                    "asDisplayed"
                }
            };

        private static Dictionary<XlOutlineCompoundType, string> CreateCompoundTypeTable() => 
            new Dictionary<XlOutlineCompoundType, string> { 
                { 
                    XlOutlineCompoundType.Single,
                    "sng"
                },
                { 
                    XlOutlineCompoundType.Double,
                    "dbl"
                },
                { 
                    XlOutlineCompoundType.ThickThin,
                    "thickThin"
                },
                { 
                    XlOutlineCompoundType.ThinThick,
                    "thinThick"
                },
                { 
                    XlOutlineCompoundType.Triple,
                    "tri"
                }
            };

        private static Dictionary<XlCondFmtOperator, string> CreateConditionalFormattingOperatorTable() => 
            new Dictionary<XlCondFmtOperator, string> { 
                { 
                    XlCondFmtOperator.BeginsWith,
                    "beginsWith"
                },
                { 
                    XlCondFmtOperator.Between,
                    "between"
                },
                { 
                    XlCondFmtOperator.ContainsText,
                    "containsText"
                },
                { 
                    XlCondFmtOperator.EndsWith,
                    "endsWith"
                },
                { 
                    XlCondFmtOperator.Equal,
                    "equal"
                },
                { 
                    XlCondFmtOperator.GreaterThan,
                    "greaterThan"
                },
                { 
                    XlCondFmtOperator.GreaterThanOrEqual,
                    "greaterThanOrEqual"
                },
                { 
                    XlCondFmtOperator.LessThan,
                    "lessThan"
                },
                { 
                    XlCondFmtOperator.LessThanOrEqual,
                    "lessThanOrEqual"
                },
                { 
                    XlCondFmtOperator.NotBetween,
                    "notBetween"
                },
                { 
                    XlCondFmtOperator.NotContains,
                    "notContains"
                },
                { 
                    XlCondFmtOperator.NotEqual,
                    "notEqual"
                }
            };

        private static Dictionary<XlCondFmtType, string> CreateConditionalFormattingTypeTable() => 
            new Dictionary<XlCondFmtType, string> { 
                { 
                    XlCondFmtType.AboveOrBelowAverage,
                    "aboveAverage"
                },
                { 
                    XlCondFmtType.BeginsWith,
                    "beginsWith"
                },
                { 
                    XlCondFmtType.CellIs,
                    "cellIs"
                },
                { 
                    XlCondFmtType.ColorScale,
                    "colorScale"
                },
                { 
                    XlCondFmtType.ContainsBlanks,
                    "containsBlanks"
                },
                { 
                    XlCondFmtType.ContainsErrors,
                    "containsErrors"
                },
                { 
                    XlCondFmtType.ContainsText,
                    "containsText"
                },
                { 
                    XlCondFmtType.DataBar,
                    "dataBar"
                },
                { 
                    XlCondFmtType.DuplicateValues,
                    "duplicateValues"
                },
                { 
                    XlCondFmtType.EndsWith,
                    "endsWith"
                },
                { 
                    XlCondFmtType.Expression,
                    "expression"
                },
                { 
                    XlCondFmtType.IconSet,
                    "iconSet"
                },
                { 
                    XlCondFmtType.NotContainsBlanks,
                    "notContainsBlanks"
                },
                { 
                    XlCondFmtType.NotContainsErrors,
                    "notContainsErrors"
                },
                { 
                    XlCondFmtType.NotContainsText,
                    "notContainsText"
                },
                { 
                    XlCondFmtType.TimePeriod,
                    "timePeriod"
                },
                { 
                    XlCondFmtType.Top10,
                    "top10"
                },
                { 
                    XlCondFmtType.UniqueValues,
                    "uniqueValues"
                }
            };

        private static Dictionary<XlCondFmtValueObjectType, string> CreateConditionalFormattingValueTypeTable() => 
            new Dictionary<XlCondFmtValueObjectType, string> { 
                { 
                    XlCondFmtValueObjectType.Number,
                    "num"
                },
                { 
                    XlCondFmtValueObjectType.Percent,
                    "percent"
                },
                { 
                    XlCondFmtValueObjectType.Max,
                    "max"
                },
                { 
                    XlCondFmtValueObjectType.Min,
                    "min"
                },
                { 
                    XlCondFmtValueObjectType.Formula,
                    "formula"
                },
                { 
                    XlCondFmtValueObjectType.Percentile,
                    "percentile"
                },
                { 
                    XlCondFmtValueObjectType.AutoMax,
                    "autoMax"
                },
                { 
                    XlCondFmtValueObjectType.AutoMin,
                    "autoMin"
                }
            };

        private static Dictionary<XlDataValidationErrorStyle, string> CreateDataValidationErrorStyleTable() => 
            new Dictionary<XlDataValidationErrorStyle, string> { 
                { 
                    XlDataValidationErrorStyle.Information,
                    "information"
                },
                { 
                    XlDataValidationErrorStyle.Stop,
                    "stop"
                },
                { 
                    XlDataValidationErrorStyle.Warning,
                    "warning"
                }
            };

        private static Dictionary<XlDataValidationImeMode, string> CreateDataValidationImeModeTable() => 
            new Dictionary<XlDataValidationImeMode, string> { 
                { 
                    XlDataValidationImeMode.Disabled,
                    "disabled"
                },
                { 
                    XlDataValidationImeMode.FullAlpha,
                    "fullAlpha"
                },
                { 
                    XlDataValidationImeMode.FullHangul,
                    "fullHangul"
                },
                { 
                    XlDataValidationImeMode.FullKatakana,
                    "fullKatakana"
                },
                { 
                    XlDataValidationImeMode.HalfAlpha,
                    "halfAlpha"
                },
                { 
                    XlDataValidationImeMode.HalfHangul,
                    "halfHangul"
                },
                { 
                    XlDataValidationImeMode.HalfKatakana,
                    "halfKatakana"
                },
                { 
                    XlDataValidationImeMode.Hiragana,
                    "hiragana"
                },
                { 
                    XlDataValidationImeMode.NoControl,
                    "noControl"
                },
                { 
                    XlDataValidationImeMode.Off,
                    "off"
                },
                { 
                    XlDataValidationImeMode.On,
                    "on"
                }
            };

        private static Dictionary<XlDataValidationOperator, string> CreateDataValidationOperatorTable() => 
            new Dictionary<XlDataValidationOperator, string> { 
                { 
                    XlDataValidationOperator.Between,
                    "between"
                },
                { 
                    XlDataValidationOperator.Equal,
                    "equal"
                },
                { 
                    XlDataValidationOperator.GreaterThan,
                    "greaterThan"
                },
                { 
                    XlDataValidationOperator.GreaterThanOrEqual,
                    "greaterThanOrEqual"
                },
                { 
                    XlDataValidationOperator.LessThan,
                    "lessThan"
                },
                { 
                    XlDataValidationOperator.LessThanOrEqual,
                    "lessThanOrEqual"
                },
                { 
                    XlDataValidationOperator.NotBetween,
                    "notBetween"
                },
                { 
                    XlDataValidationOperator.NotEqual,
                    "notEqual"
                }
            };

        private static Dictionary<XlDataValidationType, string> CreateDataValidationTypeTable() => 
            new Dictionary<XlDataValidationType, string> { 
                { 
                    XlDataValidationType.Custom,
                    "custom"
                },
                { 
                    XlDataValidationType.Date,
                    "date"
                },
                { 
                    XlDataValidationType.Decimal,
                    "decimal"
                },
                { 
                    XlDataValidationType.List,
                    "list"
                },
                { 
                    XlDataValidationType.None,
                    "none"
                },
                { 
                    XlDataValidationType.TextLength,
                    "textLength"
                },
                { 
                    XlDataValidationType.Time,
                    "time"
                },
                { 
                    XlDataValidationType.Whole,
                    "whole"
                }
            };

        private static Dictionary<XlDateTimeGroupingType, string> CreateDateTimeGroupingTable() => 
            new Dictionary<XlDateTimeGroupingType, string> { 
                { 
                    XlDateTimeGroupingType.Year,
                    "year"
                },
                { 
                    XlDateTimeGroupingType.Month,
                    "month"
                },
                { 
                    XlDateTimeGroupingType.Day,
                    "day"
                },
                { 
                    XlDateTimeGroupingType.Hour,
                    "hour"
                },
                { 
                    XlDateTimeGroupingType.Minute,
                    "minute"
                },
                { 
                    XlDateTimeGroupingType.Second,
                    "second"
                }
            };

        private static Dictionary<XlDisplayBlanksAs, string> CreateDisplayBlanksAsTable() => 
            new Dictionary<XlDisplayBlanksAs, string> { 
                { 
                    XlDisplayBlanksAs.Gap,
                    "gap"
                },
                { 
                    XlDisplayBlanksAs.Span,
                    "span"
                },
                { 
                    XlDisplayBlanksAs.Zero,
                    "zero"
                }
            };

        public IXlDocument CreateDocument(Stream stream) => 
            this.BeginExport(stream);

        public IXlDocument CreateDocument(Stream stream, EncryptionOptions encryptionOptions) => 
            this.BeginExport(stream, encryptionOptions);

        private static Dictionary<XlDynamicFilterType, string> CreateDynamicFilterTypeTable() => 
            new Dictionary<XlDynamicFilterType, string> { 
                { 
                    XlDynamicFilterType.AboveAverage,
                    "aboveAverage"
                },
                { 
                    XlDynamicFilterType.BelowAverage,
                    "belowAverage"
                },
                { 
                    XlDynamicFilterType.Tomorrow,
                    "tomorrow"
                },
                { 
                    XlDynamicFilterType.Today,
                    "today"
                },
                { 
                    XlDynamicFilterType.Yesterday,
                    "yesterday"
                },
                { 
                    XlDynamicFilterType.NextWeek,
                    "nextWeek"
                },
                { 
                    XlDynamicFilterType.ThisWeek,
                    "thisWeek"
                },
                { 
                    XlDynamicFilterType.LastWeek,
                    "lastWeek"
                },
                { 
                    XlDynamicFilterType.NextMonth,
                    "nextMonth"
                },
                { 
                    XlDynamicFilterType.ThisMonth,
                    "thisMonth"
                },
                { 
                    XlDynamicFilterType.LastMonth,
                    "lastMonth"
                },
                { 
                    XlDynamicFilterType.NextQuarter,
                    "nextQuarter"
                },
                { 
                    XlDynamicFilterType.ThisQuarter,
                    "thisQuarter"
                },
                { 
                    XlDynamicFilterType.LastQuarter,
                    "lastQuarter"
                },
                { 
                    XlDynamicFilterType.NextYear,
                    "nextYear"
                },
                { 
                    XlDynamicFilterType.ThisYear,
                    "thisYear"
                },
                { 
                    XlDynamicFilterType.LastYear,
                    "lastYear"
                },
                { 
                    XlDynamicFilterType.YearToDate,
                    "yearToDate"
                },
                { 
                    XlDynamicFilterType.Quarter1,
                    "Q1"
                },
                { 
                    XlDynamicFilterType.Quarter2,
                    "Q2"
                },
                { 
                    XlDynamicFilterType.Quarter3,
                    "Q3"
                },
                { 
                    XlDynamicFilterType.Quarter4,
                    "Q4"
                },
                { 
                    XlDynamicFilterType.Month1,
                    "M1"
                },
                { 
                    XlDynamicFilterType.Month2,
                    "M2"
                },
                { 
                    XlDynamicFilterType.Month3,
                    "M3"
                },
                { 
                    XlDynamicFilterType.Month4,
                    "M4"
                },
                { 
                    XlDynamicFilterType.Month5,
                    "M5"
                },
                { 
                    XlDynamicFilterType.Month6,
                    "M6"
                },
                { 
                    XlDynamicFilterType.Month7,
                    "M7"
                },
                { 
                    XlDynamicFilterType.Month8,
                    "M8"
                },
                { 
                    XlDynamicFilterType.Month9,
                    "M9"
                },
                { 
                    XlDynamicFilterType.Month10,
                    "M10"
                },
                { 
                    XlDynamicFilterType.Month11,
                    "M11"
                },
                { 
                    XlDynamicFilterType.Month12,
                    "M12"
                }
            };

        private static Dictionary<XlErrorsPrintMode, string> CreateErrorsPrintModeTable() => 
            new Dictionary<XlErrorsPrintMode, string> { 
                { 
                    XlErrorsPrintMode.Displayed,
                    "displayed"
                },
                { 
                    XlErrorsPrintMode.Dash,
                    "dash"
                },
                { 
                    XlErrorsPrintMode.Blank,
                    "blank"
                },
                { 
                    XlErrorsPrintMode.NA,
                    "NA"
                }
            };

        private static Dictionary<XlFilterOperator, string> CreateFilterOperatorTable() => 
            new Dictionary<XlFilterOperator, string> { 
                { 
                    XlFilterOperator.Equal,
                    "equal"
                },
                { 
                    XlFilterOperator.LessThan,
                    "lessThan"
                },
                { 
                    XlFilterOperator.LessThanOrEqual,
                    "lessThanOrEqual"
                },
                { 
                    XlFilterOperator.NotEqual,
                    "notEqual"
                },
                { 
                    XlFilterOperator.GreaterThanOrEqual,
                    "greaterThanOrEqual"
                },
                { 
                    XlFilterOperator.GreaterThan,
                    "greaterThan"
                }
            };

        private static Dictionary<XlFontSchemeStyles, string> CreateFontSchemeStyleTable() => 
            new Dictionary<XlFontSchemeStyles, string> { 
                { 
                    XlFontSchemeStyles.None,
                    "none"
                },
                { 
                    XlFontSchemeStyles.Minor,
                    "minor"
                },
                { 
                    XlFontSchemeStyles.Major,
                    "major"
                }
            };

        private static Dictionary<XlGeometryPreset, string> CreateGeometryPresetTable() => 
            new Dictionary<XlGeometryPreset, string> { 
                { 
                    XlGeometryPreset.Line,
                    "line"
                },
                { 
                    XlGeometryPreset.LineInv,
                    "lineInv"
                },
                { 
                    XlGeometryPreset.Triangle,
                    "triangle"
                },
                { 
                    XlGeometryPreset.RtTriangle,
                    "rtTriangle"
                },
                { 
                    XlGeometryPreset.Rect,
                    "rect"
                },
                { 
                    XlGeometryPreset.Diamond,
                    "diamond"
                },
                { 
                    XlGeometryPreset.Parallelogram,
                    "parallelogram"
                },
                { 
                    XlGeometryPreset.Trapezoid,
                    "trapezoid"
                },
                { 
                    XlGeometryPreset.NonIsoscelesTrapezoid,
                    "nonIsoscelesTrapezoid"
                },
                { 
                    XlGeometryPreset.Pentagon,
                    "pentagon"
                },
                { 
                    XlGeometryPreset.Hexagon,
                    "hexagon"
                },
                { 
                    XlGeometryPreset.Heptagon,
                    "heptagon"
                },
                { 
                    XlGeometryPreset.Octagon,
                    "octagon"
                },
                { 
                    XlGeometryPreset.Decagon,
                    "decagon"
                },
                { 
                    XlGeometryPreset.Dodecagon,
                    "dodecagon"
                },
                { 
                    XlGeometryPreset.Star4,
                    "star4"
                },
                { 
                    XlGeometryPreset.Star5,
                    "star5"
                },
                { 
                    XlGeometryPreset.Star6,
                    "star6"
                },
                { 
                    XlGeometryPreset.Star7,
                    "star7"
                },
                { 
                    XlGeometryPreset.Star8,
                    "star8"
                },
                { 
                    XlGeometryPreset.Star10,
                    "star10"
                },
                { 
                    XlGeometryPreset.Star12,
                    "star12"
                },
                { 
                    XlGeometryPreset.Star16,
                    "star16"
                },
                { 
                    XlGeometryPreset.Star24,
                    "star24"
                },
                { 
                    XlGeometryPreset.Star32,
                    "star32"
                },
                { 
                    XlGeometryPreset.RoundRect,
                    "roundRect"
                },
                { 
                    XlGeometryPreset.Round1Rect,
                    "round1Rect"
                },
                { 
                    XlGeometryPreset.Round2SameRect,
                    "round2SameRect"
                },
                { 
                    XlGeometryPreset.Round2DiagRect,
                    "round2DiagRect"
                },
                { 
                    XlGeometryPreset.SnipRoundRect,
                    "snipRoundRect"
                },
                { 
                    XlGeometryPreset.Snip1Rect,
                    "snip1Rect"
                },
                { 
                    XlGeometryPreset.Snip2SameRect,
                    "snip2SameRect"
                },
                { 
                    XlGeometryPreset.Snip2DiagRect,
                    "snip2DiagRect"
                },
                { 
                    XlGeometryPreset.Plaque,
                    "plaque"
                },
                { 
                    XlGeometryPreset.Ellipse,
                    "ellipse"
                },
                { 
                    XlGeometryPreset.Teardrop,
                    "teardrop"
                },
                { 
                    XlGeometryPreset.HomePlate,
                    "homePlate"
                },
                { 
                    XlGeometryPreset.Chevron,
                    "chevron"
                },
                { 
                    XlGeometryPreset.PieWedge,
                    "pieWedge"
                },
                { 
                    XlGeometryPreset.Pie,
                    "pie"
                },
                { 
                    XlGeometryPreset.BlockArc,
                    "blockArc"
                },
                { 
                    XlGeometryPreset.Donut,
                    "donut"
                },
                { 
                    XlGeometryPreset.NoSmoking,
                    "noSmoking"
                },
                { 
                    XlGeometryPreset.RightArrow,
                    "rightArrow"
                },
                { 
                    XlGeometryPreset.LeftArrow,
                    "leftArrow"
                },
                { 
                    XlGeometryPreset.UpArrow,
                    "upArrow"
                },
                { 
                    XlGeometryPreset.DownArrow,
                    "downArrow"
                },
                { 
                    XlGeometryPreset.StripedRightArrow,
                    "stripedRightArrow"
                },
                { 
                    XlGeometryPreset.NotchedRightArrow,
                    "notchedRightArrow"
                },
                { 
                    XlGeometryPreset.BentUpArrow,
                    "bentUpArrow"
                },
                { 
                    XlGeometryPreset.LeftRightArrow,
                    "leftRightArrow"
                },
                { 
                    XlGeometryPreset.UpDownArrow,
                    "upDownArrow"
                },
                { 
                    XlGeometryPreset.LeftUpArrow,
                    "leftUpArrow"
                },
                { 
                    XlGeometryPreset.LeftRightUpArrow,
                    "leftRightUpArrow"
                },
                { 
                    XlGeometryPreset.QuadArrow,
                    "quadArrow"
                },
                { 
                    XlGeometryPreset.LeftArrowCallout,
                    "leftArrowCallout"
                },
                { 
                    XlGeometryPreset.RightArrowCallout,
                    "rightArrowCallout"
                },
                { 
                    XlGeometryPreset.UpArrowCallout,
                    "upArrowCallout"
                },
                { 
                    XlGeometryPreset.DownArrowCallout,
                    "downArrowCallout"
                },
                { 
                    XlGeometryPreset.LeftRightArrowCallout,
                    "leftRightArrowCallout"
                },
                { 
                    XlGeometryPreset.UpDownArrowCallout,
                    "upDownArrowCallout"
                },
                { 
                    XlGeometryPreset.QuadArrowCallout,
                    "quadArrowCallout"
                },
                { 
                    XlGeometryPreset.BentArrow,
                    "bentArrow"
                },
                { 
                    XlGeometryPreset.UturnArrow,
                    "uturnArrow"
                },
                { 
                    XlGeometryPreset.CircularArrow,
                    "circularArrow"
                },
                { 
                    XlGeometryPreset.LeftCircularArrow,
                    "leftCircularArrow"
                },
                { 
                    XlGeometryPreset.LeftRightCircularArrow,
                    "leftRightCircularArrow"
                },
                { 
                    XlGeometryPreset.CurvedRightArrow,
                    "curvedRightArrow"
                },
                { 
                    XlGeometryPreset.CurvedLeftArrow,
                    "curvedLeftArrow"
                },
                { 
                    XlGeometryPreset.CurvedUpArrow,
                    "curvedUpArrow"
                },
                { 
                    XlGeometryPreset.CurvedDownArrow,
                    "curvedDownArrow"
                },
                { 
                    XlGeometryPreset.SwooshArrow,
                    "swooshArrow"
                },
                { 
                    XlGeometryPreset.Cube,
                    "cube"
                },
                { 
                    XlGeometryPreset.Can,
                    "can"
                },
                { 
                    XlGeometryPreset.LightningBolt,
                    "lightningBolt"
                },
                { 
                    XlGeometryPreset.Heart,
                    "heart"
                },
                { 
                    XlGeometryPreset.Sun,
                    "sun"
                },
                { 
                    XlGeometryPreset.Moon,
                    "moon"
                },
                { 
                    XlGeometryPreset.SmileyFace,
                    "smileyFace"
                },
                { 
                    XlGeometryPreset.IrregularSeal1,
                    "irregularSeal1"
                },
                { 
                    XlGeometryPreset.IrregularSeal2,
                    "irregularSeal2"
                },
                { 
                    XlGeometryPreset.FoldedCorner,
                    "foldedCorner"
                },
                { 
                    XlGeometryPreset.Bevel,
                    "bevel"
                },
                { 
                    XlGeometryPreset.Frame,
                    "frame"
                },
                { 
                    XlGeometryPreset.HalfFrame,
                    "halfFrame"
                },
                { 
                    XlGeometryPreset.Corner,
                    "corner"
                },
                { 
                    XlGeometryPreset.DiagStripe,
                    "diagStripe"
                },
                { 
                    XlGeometryPreset.Chord,
                    "chord"
                },
                { 
                    XlGeometryPreset.Arc,
                    "arc"
                },
                { 
                    XlGeometryPreset.LeftBracket,
                    "leftBracket"
                },
                { 
                    XlGeometryPreset.RightBracket,
                    "rightBracket"
                },
                { 
                    XlGeometryPreset.LeftBrace,
                    "leftBrace"
                },
                { 
                    XlGeometryPreset.RightBrace,
                    "rightBrace"
                },
                { 
                    XlGeometryPreset.BracketPair,
                    "bracketPair"
                },
                { 
                    XlGeometryPreset.BracePair,
                    "bracePair"
                },
                { 
                    XlGeometryPreset.StraightConnector1,
                    "straightConnector1"
                },
                { 
                    XlGeometryPreset.BentConnector2,
                    "bentConnector2"
                },
                { 
                    XlGeometryPreset.BentConnector3,
                    "bentConnector3"
                },
                { 
                    XlGeometryPreset.BentConnector4,
                    "bentConnector4"
                },
                { 
                    XlGeometryPreset.BentConnector5,
                    "bentConnector5"
                },
                { 
                    XlGeometryPreset.CurvedConnector2,
                    "curvedConnector2"
                },
                { 
                    XlGeometryPreset.CurvedConnector3,
                    "curvedConnector3"
                },
                { 
                    XlGeometryPreset.CurvedConnector4,
                    "curvedConnector4"
                },
                { 
                    XlGeometryPreset.CurvedConnector5,
                    "curvedConnector5"
                },
                { 
                    XlGeometryPreset.Callout1,
                    "callout1"
                },
                { 
                    XlGeometryPreset.Callout2,
                    "callout2"
                },
                { 
                    XlGeometryPreset.Callout3,
                    "callout3"
                },
                { 
                    XlGeometryPreset.AccentCallout1,
                    "accentCallout1"
                },
                { 
                    XlGeometryPreset.AccentCallout2,
                    "accentCallout2"
                },
                { 
                    XlGeometryPreset.AccentCallout3,
                    "accentCallout3"
                },
                { 
                    XlGeometryPreset.BorderCallout1,
                    "borderCallout1"
                },
                { 
                    XlGeometryPreset.BorderCallout2,
                    "borderCallout2"
                },
                { 
                    XlGeometryPreset.BorderCallout3,
                    "borderCallout3"
                },
                { 
                    XlGeometryPreset.AccentBorderCallout1,
                    "accentBorderCallout1"
                },
                { 
                    XlGeometryPreset.AccentBorderCallout2,
                    "accentBorderCallout2"
                },
                { 
                    XlGeometryPreset.AccentBorderCallout3,
                    "accentBorderCallout3"
                },
                { 
                    XlGeometryPreset.WedgeRectCallout,
                    "wedgeRectCallout"
                },
                { 
                    XlGeometryPreset.WedgeRoundRectCallout,
                    "wedgeRoundRectCallout"
                },
                { 
                    XlGeometryPreset.WedgeEllipseCallout,
                    "wedgeEllipseCallout"
                },
                { 
                    XlGeometryPreset.CloudCallout,
                    "cloudCallout"
                },
                { 
                    XlGeometryPreset.Cloud,
                    "cloud"
                },
                { 
                    XlGeometryPreset.Ribbon,
                    "ribbon"
                },
                { 
                    XlGeometryPreset.Ribbon2,
                    "ribbon2"
                },
                { 
                    XlGeometryPreset.EllipseRibbon,
                    "ellipseRibbon"
                },
                { 
                    XlGeometryPreset.EllipseRibbon2,
                    "ellipseRibbon2"
                },
                { 
                    XlGeometryPreset.LeftRightRibbon,
                    "leftRightRibbon"
                },
                { 
                    XlGeometryPreset.VerticalScroll,
                    "verticalScroll"
                },
                { 
                    XlGeometryPreset.HorizontalScroll,
                    "horizontalScroll"
                },
                { 
                    XlGeometryPreset.Wave,
                    "wave"
                },
                { 
                    XlGeometryPreset.DoubleWave,
                    "doubleWave"
                },
                { 
                    XlGeometryPreset.Plus,
                    "plus"
                },
                { 
                    XlGeometryPreset.FlowChartProcess,
                    "flowChartProcess"
                },
                { 
                    XlGeometryPreset.FlowChartDecision,
                    "flowChartDecision"
                },
                { 
                    XlGeometryPreset.FlowChartInputOutput,
                    "flowChartInputOutput"
                },
                { 
                    XlGeometryPreset.FlowChartPredefinedProcess,
                    "flowChartPredefinedProcess"
                },
                { 
                    XlGeometryPreset.FlowChartInternalStorage,
                    "flowChartInternalStorage"
                },
                { 
                    XlGeometryPreset.FlowChartDocument,
                    "flowChartDocument"
                },
                { 
                    XlGeometryPreset.FlowChartMultidocument,
                    "flowChartMultidocument"
                },
                { 
                    XlGeometryPreset.FlowChartTerminator,
                    "flowChartTerminator"
                },
                { 
                    XlGeometryPreset.FlowChartPreparation,
                    "flowChartPreparation"
                },
                { 
                    XlGeometryPreset.FlowChartManualInput,
                    "flowChartManualInput"
                },
                { 
                    XlGeometryPreset.FlowChartManualOperation,
                    "flowChartManualOperation"
                },
                { 
                    XlGeometryPreset.FlowChartConnector,
                    "flowChartConnector"
                },
                { 
                    XlGeometryPreset.FlowChartPunchedCard,
                    "flowChartPunchedCard"
                },
                { 
                    XlGeometryPreset.FlowChartPunchedTape,
                    "flowChartPunchedTape"
                },
                { 
                    XlGeometryPreset.FlowChartSummingJunction,
                    "flowChartSummingJunction"
                },
                { 
                    XlGeometryPreset.FlowChartOr,
                    "flowChartOr"
                },
                { 
                    XlGeometryPreset.FlowChartCollate,
                    "flowChartCollate"
                },
                { 
                    XlGeometryPreset.FlowChartSort,
                    "flowChartSort"
                },
                { 
                    XlGeometryPreset.FlowChartExtract,
                    "flowChartExtract"
                },
                { 
                    XlGeometryPreset.FlowChartMerge,
                    "flowChartMerge"
                },
                { 
                    XlGeometryPreset.FlowChartOfflineStorage,
                    "flowChartOfflineStorage"
                },
                { 
                    XlGeometryPreset.FlowChartOnlineStorage,
                    "flowChartOnlineStorage"
                },
                { 
                    XlGeometryPreset.FlowChartMagneticTape,
                    "flowChartMagneticTape"
                },
                { 
                    XlGeometryPreset.FlowChartMagneticDisk,
                    "flowChartMagneticDisk"
                },
                { 
                    XlGeometryPreset.FlowChartMagneticDrum,
                    "flowChartMagneticDrum"
                },
                { 
                    XlGeometryPreset.FlowChartDisplay,
                    "flowChartDisplay"
                },
                { 
                    XlGeometryPreset.FlowChartDelay,
                    "flowChartDelay"
                },
                { 
                    XlGeometryPreset.FlowChartAlternateProcess,
                    "flowChartAlternateProcess"
                },
                { 
                    XlGeometryPreset.FlowChartOffpageConnector,
                    "flowChartOffpageConnector"
                },
                { 
                    XlGeometryPreset.ActionButtonBlank,
                    "actionButtonBlank"
                },
                { 
                    XlGeometryPreset.ActionButtonHome,
                    "actionButtonHome"
                },
                { 
                    XlGeometryPreset.ActionButtonHelp,
                    "actionButtonHelp"
                },
                { 
                    XlGeometryPreset.ActionButtonInformation,
                    "actionButtonInformation"
                },
                { 
                    XlGeometryPreset.ActionButtonForwardNext,
                    "actionButtonForwardNext"
                },
                { 
                    XlGeometryPreset.ActionButtonBackPrevious,
                    "actionButtonBackPrevious"
                },
                { 
                    XlGeometryPreset.ActionButtonEnd,
                    "actionButtonEnd"
                },
                { 
                    XlGeometryPreset.ActionButtonBeginning,
                    "actionButtonBeginning"
                },
                { 
                    XlGeometryPreset.ActionButtonReturn,
                    "actionButtonReturn"
                },
                { 
                    XlGeometryPreset.ActionButtonDocument,
                    "actionButtonDocument"
                },
                { 
                    XlGeometryPreset.ActionButtonSound,
                    "actionButtonSound"
                },
                { 
                    XlGeometryPreset.ActionButtonMovie,
                    "actionButtonMovie"
                },
                { 
                    XlGeometryPreset.Gear6,
                    "gear6"
                },
                { 
                    XlGeometryPreset.Gear9,
                    "gear9"
                },
                { 
                    XlGeometryPreset.Funnel,
                    "funnel"
                },
                { 
                    XlGeometryPreset.MathPlus,
                    "mathPlus"
                },
                { 
                    XlGeometryPreset.MathMinus,
                    "mathMinus"
                },
                { 
                    XlGeometryPreset.MathMultiply,
                    "mathMultiply"
                },
                { 
                    XlGeometryPreset.MathDivide,
                    "mathDivide"
                },
                { 
                    XlGeometryPreset.MathEqual,
                    "mathEqual"
                },
                { 
                    XlGeometryPreset.MathNotEqual,
                    "mathNotEqual"
                },
                { 
                    XlGeometryPreset.CornerTabs,
                    "cornerTabs"
                },
                { 
                    XlGeometryPreset.SquareTabs,
                    "squareTabs"
                },
                { 
                    XlGeometryPreset.PlaqueTabs,
                    "plaqueTabs"
                },
                { 
                    XlGeometryPreset.ChartX,
                    "chartX"
                },
                { 
                    XlGeometryPreset.ChartStar,
                    "chartStar"
                },
                { 
                    XlGeometryPreset.ChartPlus,
                    "chartPlus"
                }
            };

        private static Dictionary<XlHorizontalAlignment, string> CreateHorizontalAlignmentTable() => 
            new Dictionary<XlHorizontalAlignment, string> { 
                { 
                    XlHorizontalAlignment.Center,
                    "center"
                },
                { 
                    XlHorizontalAlignment.CenterContinuous,
                    "centerContinuous"
                },
                { 
                    XlHorizontalAlignment.Distributed,
                    "distributed"
                },
                { 
                    XlHorizontalAlignment.Fill,
                    "fill"
                },
                { 
                    XlHorizontalAlignment.General,
                    "general"
                },
                { 
                    XlHorizontalAlignment.Justify,
                    "justify"
                },
                { 
                    XlHorizontalAlignment.Left,
                    "left"
                },
                { 
                    XlHorizontalAlignment.Right,
                    "right"
                }
            };

        private static Dictionary<XlCondFmtIconSetType, string> CreateIconSetTypeTable() => 
            new Dictionary<XlCondFmtIconSetType, string> { 
                { 
                    XlCondFmtIconSetType.Arrows3,
                    "3Arrows"
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray3,
                    "3ArrowsGray"
                },
                { 
                    XlCondFmtIconSetType.Flags3,
                    "3Flags"
                },
                { 
                    XlCondFmtIconSetType.TrafficLights3,
                    "3TrafficLights1"
                },
                { 
                    XlCondFmtIconSetType.TrafficLights3Black,
                    "3TrafficLights2"
                },
                { 
                    XlCondFmtIconSetType.Signs3,
                    "3Signs"
                },
                { 
                    XlCondFmtIconSetType.Symbols3,
                    "3Symbols"
                },
                { 
                    XlCondFmtIconSetType.Symbols3Circled,
                    "3Symbols2"
                },
                { 
                    XlCondFmtIconSetType.Stars3,
                    "3Stars"
                },
                { 
                    XlCondFmtIconSetType.Triangles3,
                    "3Triangles"
                },
                { 
                    XlCondFmtIconSetType.Arrows4,
                    "4Arrows"
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray4,
                    "4ArrowsGray"
                },
                { 
                    XlCondFmtIconSetType.RedToBlack4,
                    "4RedToBlack"
                },
                { 
                    XlCondFmtIconSetType.Rating4,
                    "4Rating"
                },
                { 
                    XlCondFmtIconSetType.TrafficLights4,
                    "4TrafficLights"
                },
                { 
                    XlCondFmtIconSetType.Arrows5,
                    "5Arrows"
                },
                { 
                    XlCondFmtIconSetType.ArrowsGray5,
                    "5ArrowsGray"
                },
                { 
                    XlCondFmtIconSetType.Rating5,
                    "5Rating"
                },
                { 
                    XlCondFmtIconSetType.Quarters5,
                    "5Quarters"
                },
                { 
                    XlCondFmtIconSetType.Boxes5,
                    "5Boxes"
                },
                { 
                    XlCondFmtIconSetType.NoIcons,
                    "NoIcons"
                }
            };

        private static Dictionary<ImageFormat, string> CreateImageContentTypeTable() => 
            new Dictionary<ImageFormat, string> { 
                { 
                    ImageFormat.Jpeg,
                    "image/jpeg"
                },
                { 
                    ImageFormat.Png,
                    "image/png"
                },
                { 
                    ImageFormat.Bmp,
                    "image/bitmap"
                },
                { 
                    ImageFormat.Tiff,
                    "image/tiff"
                },
                { 
                    ImageFormat.Gif,
                    "image/gif"
                },
                { 
                    ImageFormat.Icon,
                    "image/x-icon"
                },
                { 
                    ImageFormat.Wmf,
                    "application/x-msmetafile"
                },
                { 
                    ImageFormat.Emf,
                    "application/x-msmetafile"
                }
            };

        private static Dictionary<ImageFormat, string> CreateImageExtenstionTable() => 
            new Dictionary<ImageFormat, string> { 
                { 
                    ImageFormat.Bmp,
                    "bmp"
                },
                { 
                    ImageFormat.Emf,
                    "emf"
                },
                { 
                    ImageFormat.Gif,
                    "gif"
                },
                { 
                    ImageFormat.Icon,
                    "ico"
                },
                { 
                    ImageFormat.Jpeg,
                    "jpg"
                },
                { 
                    ImageFormat.Png,
                    "png"
                },
                { 
                    ImageFormat.Tiff,
                    "tif"
                },
                { 
                    ImageFormat.Wmf,
                    "wmf"
                }
            };

        private string CreateListValues(IList<XlVariantValue> values)
        {
            StringBuilder builder = new StringBuilder();
            int count = values.Count;
            if (count > 0)
            {
                builder.Append('"');
            }
            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    builder.Append(',');
                }
                builder.Append(values[i].ToText().TextValue);
            }
            if (count > 0)
            {
                builder.Append('"');
            }
            return builder.ToString();
        }

        private ExcelNumberFormat CreateNumberFormat(string netFormatString, bool isDateTimeFormatString, XlNumberFormat xlNumberFormat)
        {
            ExcelNumberFormat format;
            if (string.IsNullOrEmpty(netFormatString))
            {
                if (xlNumberFormat == null)
                {
                    return null;
                }
                format = new ExcelNumberFormat(xlNumberFormat.FormatId, xlNumberFormat.FormatCode);
                if (format.Id < 0)
                {
                    format.Id = this.customNumberFormatId;
                    this.customNumberFormatId++;
                }
                return format;
            }
            XlNetNumberFormat format1 = new XlNetNumberFormat();
            format1.FormatString = netFormatString;
            format1.IsDateTimeFormat = isDateTimeFormatString;
            XlNetNumberFormat key = format1;
            if (!this.netNumberFormatsTable.TryGetValue(key, out format))
            {
                format = this.ConvertNetFormatStringToXlFormatCode(netFormatString, isDateTimeFormatString);
                if (format != null)
                {
                    if (format.Id < 0)
                    {
                        format.Id = this.customNumberFormatId;
                        this.customNumberFormatId++;
                    }
                    if (string.IsNullOrEmpty(format.FormatString))
                    {
                        format = new ExcelNumberFormat(0, string.Empty);
                    }
                }
            }
            return format;
        }

        private static Dictionary<XlPageOrientation, string> CreatePageOrientationTable() => 
            new Dictionary<XlPageOrientation, string> { 
                { 
                    XlPageOrientation.Default,
                    "default"
                },
                { 
                    XlPageOrientation.Portrait,
                    "portrait"
                },
                { 
                    XlPageOrientation.Landscape,
                    "landscape"
                }
            };

        private static Dictionary<XlPagePrintOrder, string> CreatePagePrintOrderTable() => 
            new Dictionary<XlPagePrintOrder, string> { 
                { 
                    XlPagePrintOrder.DownThenOver,
                    "downThenOver"
                },
                { 
                    XlPagePrintOrder.OverThenDown,
                    "overThenDown"
                }
            };

        private static Dictionary<XlPatternType, string> CreatePatternTypeTable() => 
            new Dictionary<XlPatternType, string> { 
                { 
                    XlPatternType.DarkDown,
                    "darkDown"
                },
                { 
                    XlPatternType.DarkGray,
                    "darkGray"
                },
                { 
                    XlPatternType.DarkGrid,
                    "darkGrid"
                },
                { 
                    XlPatternType.DarkHorizontal,
                    "darkHorizontal"
                },
                { 
                    XlPatternType.DarkTrellis,
                    "darkTrellis"
                },
                { 
                    XlPatternType.DarkUp,
                    "darkUp"
                },
                { 
                    XlPatternType.DarkVertical,
                    "darkVertical"
                },
                { 
                    XlPatternType.Gray0625,
                    "gray0625"
                },
                { 
                    XlPatternType.Gray125,
                    "gray125"
                },
                { 
                    XlPatternType.LightDown,
                    "lightDown"
                },
                { 
                    XlPatternType.LightGray,
                    "lightGray"
                },
                { 
                    XlPatternType.LightGrid,
                    "lightGrid"
                },
                { 
                    XlPatternType.LightHorizontal,
                    "lightHorizontal"
                },
                { 
                    XlPatternType.LightTrellis,
                    "lightTrellis"
                },
                { 
                    XlPatternType.LightUp,
                    "lightUp"
                },
                { 
                    XlPatternType.LightVertical,
                    "lightVertical"
                },
                { 
                    XlPatternType.MediumGray,
                    "mediumGray"
                },
                { 
                    XlPatternType.None,
                    "none"
                },
                { 
                    XlPatternType.Solid,
                    "solid"
                }
            };

        private static Dictionary<XlOutlineDashing, string> CreatePresetDashTable() => 
            new Dictionary<XlOutlineDashing, string> { 
                { 
                    XlOutlineDashing.Solid,
                    "solid"
                },
                { 
                    XlOutlineDashing.Dash,
                    "dash"
                },
                { 
                    XlOutlineDashing.DashDot,
                    "dashDot"
                },
                { 
                    XlOutlineDashing.Dot,
                    "dot"
                },
                { 
                    XlOutlineDashing.LongDash,
                    "lgDash"
                },
                { 
                    XlOutlineDashing.LongDashDot,
                    "lgDashDot"
                },
                { 
                    XlOutlineDashing.LongDashDotDot,
                    "lgDashDotDot"
                },
                { 
                    XlOutlineDashing.SystemDash,
                    "sysDash"
                },
                { 
                    XlOutlineDashing.SystemDashDot,
                    "sysDashDot"
                },
                { 
                    XlOutlineDashing.SystemDashDotDot,
                    "sysDashDotDot"
                },
                { 
                    XlOutlineDashing.SystemDot,
                    "sysDot"
                }
            };

        private static Dictionary<XlReadingOrder, string> CreateReadingOrderTable() => 
            new Dictionary<XlReadingOrder, string> { 
                { 
                    XlReadingOrder.Context,
                    "0"
                },
                { 
                    XlReadingOrder.LeftToRight,
                    "1"
                },
                { 
                    XlReadingOrder.RightToLeft,
                    "2"
                }
            };

        private static Dictionary<XlThemeColor, string> CreateSchemeColorTable() => 
            new Dictionary<XlThemeColor, string> { 
                { 
                    XlThemeColor.Accent1,
                    "accent1"
                },
                { 
                    XlThemeColor.Accent2,
                    "accent2"
                },
                { 
                    XlThemeColor.Accent3,
                    "accent3"
                },
                { 
                    XlThemeColor.Accent4,
                    "accent4"
                },
                { 
                    XlThemeColor.Accent5,
                    "accent5"
                },
                { 
                    XlThemeColor.Accent6,
                    "accent6"
                },
                { 
                    XlThemeColor.Dark1,
                    "tx1"
                },
                { 
                    XlThemeColor.Dark2,
                    "tx2"
                },
                { 
                    XlThemeColor.Light1,
                    "bg1"
                },
                { 
                    XlThemeColor.Light2,
                    "bg2"
                },
                { 
                    XlThemeColor.FollowedHyperlink,
                    "folHlink"
                },
                { 
                    XlThemeColor.Hyperlink,
                    "hlink"
                }
            };

        private static Dictionary<XlSparklineAxisScaling, string> CreateSparklineAxisScalingTable() => 
            new Dictionary<XlSparklineAxisScaling, string> { 
                { 
                    XlSparklineAxisScaling.Individual,
                    "individual"
                },
                { 
                    XlSparklineAxisScaling.Group,
                    "group"
                },
                { 
                    XlSparklineAxisScaling.Custom,
                    "custom"
                }
            };

        private static Dictionary<XlSparklineType, string> CreateSparklineTypeTable() => 
            new Dictionary<XlSparklineType, string> { 
                { 
                    XlSparklineType.Line,
                    "line"
                },
                { 
                    XlSparklineType.Column,
                    "column"
                },
                { 
                    XlSparklineType.WinLoss,
                    "stacked"
                }
            };

        private void CreateTableCells(XlCellRange range)
        {
            IEnumerable<int> cellsToCreate = this.currentSheet.GetCellsToCreate(range);
            if (cellsToCreate != null)
            {
                foreach (int num in cellsToCreate)
                {
                    this.columnIndex = num;
                    this.BeginCell();
                    this.EndCell();
                }
            }
        }

        private static Dictionary<XlCondFmtTimePeriod, string> CreateTimePeriodTable() => 
            new Dictionary<XlCondFmtTimePeriod, string> { 
                { 
                    XlCondFmtTimePeriod.Last7Days,
                    "last7Days"
                },
                { 
                    XlCondFmtTimePeriod.LastMonth,
                    "lastMonth"
                },
                { 
                    XlCondFmtTimePeriod.LastWeek,
                    "lastWeek"
                },
                { 
                    XlCondFmtTimePeriod.NextMonth,
                    "nextMonth"
                },
                { 
                    XlCondFmtTimePeriod.NextWeek,
                    "nextWeek"
                },
                { 
                    XlCondFmtTimePeriod.ThisMonth,
                    "thisMonth"
                },
                { 
                    XlCondFmtTimePeriod.ThisWeek,
                    "thisWeek"
                },
                { 
                    XlCondFmtTimePeriod.Today,
                    "today"
                },
                { 
                    XlCondFmtTimePeriod.Tomorrow,
                    "tomorrow"
                },
                { 
                    XlCondFmtTimePeriod.Yesterday,
                    "yesterday"
                }
            };

        private static Dictionary<XlTotalRowFunction, string> CreateTotalRowFunctionTable() => 
            new Dictionary<XlTotalRowFunction, string> { 
                { 
                    XlTotalRowFunction.None,
                    "none"
                },
                { 
                    XlTotalRowFunction.Average,
                    "average"
                },
                { 
                    XlTotalRowFunction.Count,
                    "count"
                },
                { 
                    XlTotalRowFunction.CountNums,
                    "countNums"
                },
                { 
                    XlTotalRowFunction.Max,
                    "max"
                },
                { 
                    XlTotalRowFunction.Min,
                    "min"
                },
                { 
                    XlTotalRowFunction.StdDev,
                    "stdDev"
                },
                { 
                    XlTotalRowFunction.Sum,
                    "sum"
                },
                { 
                    XlTotalRowFunction.Var,
                    "var"
                }
            };

        private static Dictionary<XlUnderlineType, string> CreateUnderlineTypeTable() => 
            new Dictionary<XlUnderlineType, string> { 
                { 
                    XlUnderlineType.Single,
                    "single"
                },
                { 
                    XlUnderlineType.Double,
                    "double"
                },
                { 
                    XlUnderlineType.SingleAccounting,
                    "singleAccounting"
                },
                { 
                    XlUnderlineType.DoubleAccounting,
                    "doubleAccounting"
                },
                { 
                    XlUnderlineType.None,
                    "none"
                }
            };

        private static Dictionary<XlScriptType, string> CreateVerticalAlignmentRunTypeTable() => 
            new Dictionary<XlScriptType, string> { 
                { 
                    XlScriptType.Baseline,
                    "baseline"
                },
                { 
                    XlScriptType.Subscript,
                    "subscript"
                },
                { 
                    XlScriptType.Superscript,
                    "superscript"
                }
            };

        private static Dictionary<XlVerticalAlignment, string> CreateVerticalAlignmentTable() => 
            new Dictionary<XlVerticalAlignment, string> { 
                { 
                    XlVerticalAlignment.Bottom,
                    "bottom"
                },
                { 
                    XlVerticalAlignment.Center,
                    "center"
                },
                { 
                    XlVerticalAlignment.Distributed,
                    "distributed"
                },
                { 
                    XlVerticalAlignment.Justify,
                    "justify"
                },
                { 
                    XlVerticalAlignment.Top,
                    "top"
                }
            };

        private static Dictionary<XlSheetVisibleState, string> CreateVisibilityTypeTable() => 
            new Dictionary<XlSheetVisibleState, string> { 
                { 
                    XlSheetVisibleState.Hidden,
                    "hidden"
                },
                { 
                    XlSheetVisibleState.VeryHidden,
                    "veryHidden"
                },
                { 
                    XlSheetVisibleState.Visible,
                    "visible"
                }
            };

        protected internal virtual XmlWriterSettings CreateXmlWriterSettings() => 
            XmlBasedExporterUtils.Instance.CreateDefaultXmlWriterSettings();

        private string DateTimeToString(DateTime dateTime) => 
            dateTime.ToString(CultureInfo.InvariantCulture.DateTimeFormat.SortableDateTimePattern, CultureInfo.InvariantCulture) + "Z";

        private string DateTimeToStringUniversal(DateTime dateTime) => 
            this.DateTimeToString(dateTime.ToUniversalTime());

        string IXlCellFormatter.GetFormattedValue(IXlCell cell) => 
            this.cellFormatter.GetFormattedValue(cell);

        IXlFormulaParameter IXlFormulaEngine.Concatenate(params IXlFormulaParameter[] parameters) => 
            new XlConcatenateFunction(parameters);

        IXlFormulaParameter IXlFormulaEngine.Param(XlVariantValue value) => 
            new XlFormulaParameter(value);

        IXlFormulaParameter IXlFormulaEngine.Subtotal(XlCellRange range, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(range, summary, ignoreHidden);

        IXlFormulaParameter IXlFormulaEngine.Subtotal(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(ranges, summary, ignoreHidden);

        IXlFormulaParameter IXlFormulaEngine.Text(IXlFormulaParameter formula, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(formula, netFormatString, isDateTimeFormatString);

        IXlFormulaParameter IXlFormulaEngine.Text(XlVariantValue value, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(value, netFormatString, isDateTimeFormatString);

        IXlFormulaParameter IXlFormulaEngine.VLookup(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);

        IXlFormulaParameter IXlFormulaEngine.VLookup(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);

        void IXlShapeContainer.AddShape(XlShape shape)
        {
            if ((shape.GeometryPreset == XlGeometryPreset.Line) || (shape.GeometryPreset == XlGeometryPreset.Rect))
            {
                if (this.drawingObjects.Count == 0)
                {
                    this.drawingId++;
                }
                this.drawingObjects.Add(shape);
            }
        }

        private string EncodeXmlChars(string value) => 
            XmlBasedExporterUtils.Instance.EncodeXmlChars(value);

        private string EncodeXmlCharsNoCrLf(string value) => 
            XmlBasedExporterUtils.Instance.EncodeXmlCharsNoCrLf(value);

        private string EncodeXmlCharsText(string text) => 
            ExcelXmlCharsCodec.Encode(text);

        public void EndCell()
        {
            if ((this.currentCell.ColumnIndex < 0) || (this.currentCell.ColumnIndex >= this.options.MaxColumnCount))
            {
                throw new ArgumentOutOfRangeException($"Cell column index out of range 0..{this.options.MaxColumnCount - 1}.");
            }
            try
            {
                XlVariantValue empty = this.currentCell.Value;
                if (this.options.SuppressEmptyStrings && (empty.IsText && string.IsNullOrEmpty(empty.TextValue)))
                {
                    empty = XlVariantValue.Empty;
                    this.currentCell.Value = empty;
                }
                XlDifferentialFormatting differentialFormat = this.currentSheet.GetDifferentialFormat(this.currentCell.ColumnIndex, this.currentCell.RowIndex);
                this.currentCell.ApplyDifferentialFormatting(differentialFormat);
                if (!empty.IsEmpty || (this.currentCell.HasFormula || !XlCellFormatting.Equals(this.inheritedFormatting, this.currentCell.Formatting)))
                {
                    this.rowCells.Add(this.currentCell);
                    this.currentSheet.RegisterCellPosition(this.currentCell);
                }
                this.columnIndex = this.currentCell.ColumnIndex + 1;
            }
            finally
            {
                this.insideCellScope = false;
            }
        }

        public void EndColumn()
        {
            if (this.currentColumn == null)
            {
                throw new InvalidOperationException("BeginColumn/EndColumn calls consistency.");
            }
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            if (this.currentColumn.ColumnIndex >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Column index out of range 0...{this.options.MaxColumnCount - 1}");
            }
            this.currentSheet.RegisterColumnIndex(this.currentColumn);
            if ((this.currentGroup != null) && this.currentGroup.IsCollapsed)
            {
                this.currentColumn.IsHidden = true;
            }
            if ((this.currentGroup != null) && (this.currentGroup.OutlineLevel > 0))
            {
                this.currentColumn.OutlineLevel = Math.Min(7, this.currentGroup.OutlineLevel);
            }
            this.pendingColumns.Add(this.currentColumn);
            this.columns[this.currentColumn.ColumnIndex] = this.currentColumn;
            this.columnIndex = this.currentColumn.ColumnIndex + 1;
            this.currentColumn = null;
        }

        protected void EndDocument()
        {
            if (this.sheets.Count == 0)
            {
                this.BeginSheet();
                this.EndSheet();
            }
            this.AddDocumentApplicationPropertiesContent();
            this.AddDocumentCorePropertiesContent();
            this.AddDocumentCustomPropertiesContent();
            this.AddThemeContent();
            this.AddStylesContent();
            this.AddSharedStringContent();
            this.AddWorkbookContent();
            this.AddWorkbookRelations();
            this.AddContentTypes();
            this.AddPackageRelations();
            this.currentDocument = null;
            this.documentProperties = null;
        }

        public void EndExport()
        {
            this.EndDocument();
            this.Builder.EndExport();
            this.builder = null;
            this.cellFormatter = null;
            if (!ReferenceEquals(this.outputStream, this.bufferStream))
            {
                this.bufferStream.Flush();
                this.bufferStream.Position = 0L;
                if (this.encryptionSession == null)
                {
                    this.bufferStream.CopyTo(this.outputStream);
                }
                else
                {
                    this.encryptionSession.EndSession(this.encryptionInfoStream, this.encryptedPackageStream);
                    StructuredStorageWriter writer = new StructuredStorageWriter();
                    StorageDirectoryEntry rootDirectoryEntry = writer.RootDirectoryEntry;
                    rootDirectoryEntry.AddStreamDirectoryEntry("EncryptionInfo", this.encryptionInfoStream);
                    rootDirectoryEntry.AddStreamDirectoryEntry("EncryptedPackage", this.encryptedPackageStream);
                    writer.Write(this.outputStream);
                    this.encryptionInfoStream = null;
                    this.encryptedPackageStream = null;
                    this.encryptionSession = null;
                }
            }
            this.bufferStream = null;
            this.outputStream = null;
        }

        public void EndGroup()
        {
            this.groups.Pop();
            if (this.groups.Count <= 0)
            {
                this.currentGroup = null;
            }
            else
            {
                this.currentGroup = this.groups.Peek();
            }
        }

        public void EndPicture()
        {
            this.CheckCurrentSheet();
            if ((this.currentPicture == null) || ((this.currentPicture.Image == null) || !imageExtenstionTable.ContainsKey(this.currentPicture.Format)))
            {
                this.currentPicture = null;
            }
            else
            {
                byte[] imageBytes;
                string id;
                this.shapeId++;
                if (this.drawingObjects.Count == 0)
                {
                    this.drawingId++;
                }
                try
                {
                    imageBytes = this.currentPicture.GetImageBytes(this.currentPicture.Format);
                }
                catch
                {
                    this.currentPicture.Format = ImageFormat.Png;
                    imageBytes = this.currentPicture.GetImageBytes(this.currentPicture.Format);
                }
                string imageExtension = this.GetImageExtension(this.currentPicture.Format);
                this.Builder.UsedContentTypes[imageExtension] = this.GetImageContentType(this.currentPicture.Format);
                MD4HashCalculator calculator = new MD4HashCalculator();
                uint[] initialCheckSumValue = calculator.InitialCheckSumValue;
                calculator.UpdateCheckSum(initialCheckSumValue, imageBytes, 0, imageBytes.Length);
                byte[] digest = MD4HashConverter.ToArray(calculator.GetFinalCheckSum(initialCheckSumValue));
                string str4 = this.FindImageFile(digest);
                if (string.IsNullOrEmpty(str4))
                {
                    this.imageId++;
                    str4 = $"image{this.imageId}.{imageExtension}";
                    id = this.drawingRelations.GenerateId();
                    OpenXmlRelation relation = new OpenXmlRelation(id, "../media/" + str4, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image");
                    this.drawingRelations.Add(relation);
                    this.imageFiles.Add(new XlsxPictureFileInfo(str4, digest));
                    this.Builder.Package.Add("xl/media/" + str4, this.Builder.Now, imageBytes);
                }
                else
                {
                    string target = "../media/" + str4;
                    OpenXmlRelation relationByTargetAndType = this.drawingRelations.LookupRelationByTargetAndType(target, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image");
                    if (relationByTargetAndType != null)
                    {
                        id = relationByTargetAndType.Id;
                    }
                    else
                    {
                        id = this.drawingRelations.GenerateId();
                        relationByTargetAndType = new OpenXmlRelation(id, target, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image");
                        this.drawingRelations.Add(relationByTargetAndType);
                    }
                }
                XlsxPicture item = new XlsxPicture {
                    RelationId = id,
                    PictureId = this.shapeId + 1,
                    Name = this.currentPicture.Name,
                    AnchorType = this.currentPicture.AnchorType,
                    AnchorBehavior = this.currentPicture.AnchorBehavior,
                    TopLeft = this.currentPicture.TopLeft,
                    BottomRight = this.currentPicture.BottomRight,
                    HyperlinkClick = this.currentPicture.HyperlinkClick.Clone(),
                    SourceRectangle = this.currentPicture.SourceRectangle
                };
                this.drawingObjects.Add(item);
                this.currentPicture = null;
            }
        }

        public void EndRow()
        {
            if (this.currentRow == null)
            {
                throw new InvalidOperationException("BeginRow/EndRow calls consistency.");
            }
            if (this.currentRow.RowIndex >= this.options.MaxRowCount)
            {
                throw new ArgumentOutOfRangeException("Maximum number of rows exceeded.");
            }
            XlCellRange range = XlCellRange.FromLTRB(this.columnIndex, this.rowIndex, this.options.MaxColumnCount - 1, this.rowIndex);
            this.CreateTableCells(range);
            this.currentSheet.FilterRow(this.currentRow, this.rowCells);
            this.ExportPendingRow();
            this.ExportRowCells();
            this.rowCells.Clear();
            this.rowIndex++;
            this.WriteShEndElement();
            this.currentRow = null;
        }

        public void EndSheet()
        {
            if (this.currentSheet == null)
            {
                throw new InvalidOperationException("BeginSheet/EndSheet calls consistency.");
            }
            if (!this.IsUniqueSheetName())
            {
                throw new InvalidOperationException($"Worksheet name '{this.currentSheet.Name}' is not unique.");
            }
            this.ExportPendingSheet();
            this.WriteShEndElement();
            this.GenerateAutoFilterContent(this.currentSheet);
            this.GenerateMergedCellsContent(this.currentSheet);
            this.GenerateConditionalFormattings(this.currentSheet.ConditionalFormattings);
            this.GenerateDataValidations(this.currentSheet.DataValidations);
            this.GenerateHyperlinks(this.currentSheet.Hyperlinks);
            this.GeneratePrintOptions(this.currentSheet.PrintOptions);
            this.GeneratePageMargins(this.currentSheet.PageMargins);
            this.GeneratePageSetup(this.currentSheet.PageSetup);
            this.GenerateHeaderFooter(this.currentSheet.HeaderFooter);
            this.GenerateRowPageBreaks(this.currentSheet);
            this.GenerateColumnPageBreaks(this.currentSheet);
            this.GenerateIgnoredErrors(this.currentSheet);
            this.GenerateDrawingRef();
            this.GenerateTableParts();
            this.GenerateExtList(this.currentSheet);
            this.WriteShEndElement();
            CompressedStream content = this.EndWriteXmlContent();
            SheetInfo info = new SheetInfo();
            int num = this.sheetIndex + 1;
            this.sheetIndex = num;
            info.SheetId = num;
            info.RelationId = this.Builder.WorkbookRelations.GenerateId();
            this.Builder.OverriddenContentTypes.Add($"/xl/worksheets/sheet{info.SheetId}.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml");
            this.Builder.WorkbookRelations.Add(new OpenXmlRelation(info.RelationId, $"worksheets/sheet{info.SheetId}.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"));
            this.AddPackageContent($"xl\worksheets\sheet{info.SheetId}.xml", content);
            this.sheets.Add(this.currentSheet);
            this.sheetInfos.Add(this.currentSheet, info);
            this.GenerateSheetRelations(info.SheetId);
            this.GenerateDrawingContent();
            this.GenerateTablesContent();
            this.currentSheet = null;
            this.shapeId = 0;
            this.drawingRelations.Clear();
            this.drawingObjects.Clear();
            this.sheetRelations.Clear();
            this.exportedPictureHyperlinkTable.Clear();
            this.columns.Clear();
        }

        private CompressedStream EndWriteXmlContent()
        {
            CompressedXmlStreamInfo info = this.EndWriteXmlContentCore();
            return XmlBasedExporterUtils.Instance.EndCreateCompressedXmlContent(info);
        }

        private CompressedXmlStreamInfo EndWriteXmlContentCore()
        {
            CompressedXmlStreamInfo info = this.streamInfoStack.Pop();
            this.writer = (this.streamInfoStack.Count <= 0) ? null : this.streamInfoStack.Peek().Writer;
            return info;
        }

        private string EscapeHyperlinkUri(string targetUri)
        {
            try
            {
                string scheme = new Uri(targetUri).Scheme;
                if (!string.IsNullOrEmpty(scheme) && (scheme != Uri.UriSchemeFile))
                {
                    return Uri.EscapeUriString(targetUri);
                }
            }
            catch
            {
            }
            return targetUri.Replace(" ", "%20");
        }

        private void ExportCellFormula(XlCell cell)
        {
            if (cell.SharedFormulaPosition.IsValid)
            {
                if (this.sharedFormulaTable.ContainsKey(cell.SharedFormulaPosition))
                {
                    this.WriteShStartElement("f");
                    try
                    {
                        this.WriteStringValue("t", "shared");
                        this.WriteStringValue("si", this.sharedFormulaTable[cell.SharedFormulaPosition].ToString());
                    }
                    finally
                    {
                        this.WriteShEndElement();
                    }
                }
            }
            else
            {
                bool isSharedFormula = cell.SharedFormulaRange != null;
                string formulaString = this.GetFormulaString(cell, isSharedFormula);
                if (!string.IsNullOrEmpty(formulaString))
                {
                    this.WriteShStartElement("f");
                    try
                    {
                        if (isSharedFormula)
                        {
                            int count = this.sharedFormulaTable.Count;
                            XlCellPosition key = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
                            this.sharedFormulaTable.Add(key, count);
                            XlCellRange range = cell.SharedFormulaRange.AsRelative();
                            range.SheetName = string.Empty;
                            this.WriteStringValue("t", "shared");
                            this.WriteStringValue("ref", range.ToString());
                            this.WriteStringValue("si", count.ToString());
                        }
                        this.WriteShString(this.EncodeXmlCharsNoCrLf(formulaString));
                    }
                    finally
                    {
                        this.WriteShEndElement();
                    }
                }
            }
        }

        private void ExportCellValue(XlCell cell, bool hasFormula)
        {
            XlVariantValue value2 = cell.Value;
            if (value2.IsNumeric)
            {
                double numericValue = value2.NumericValue;
                if (XNumChecker.IsNegativeZero(numericValue))
                {
                    numericValue = 0.0;
                }
                this.WriteShString("v", numericValue.ToString("G17", CultureInfo.InvariantCulture));
            }
            else if (!value2.IsText)
            {
                if (value2.IsBoolean)
                {
                    this.WriteShString("v", value2.BooleanValue ? "1" : "0");
                }
                else if (value2.IsError)
                {
                    this.WriteShString("v", value2.ErrorValue.Name);
                }
            }
            else if (hasFormula)
            {
                this.WriteShString("v", this.EncodeXmlChars(XlStringHelper.CheckLength(value2.TextValue, this.options)), true);
            }
            else
            {
                XlRichTextString richTextValue = cell.RichTextValue;
                if (richTextValue != null)
                {
                    this.WriteShString("v", this.sharedStringTable.RegisterString(XlStringHelper.CheckLength(richTextValue, this.options)).ToString());
                }
                else
                {
                    this.WriteShString("v", this.sharedStringTable.RegisterString(XlStringHelper.CheckLength(value2.TextValue, this.options)).ToString());
                }
            }
        }

        protected internal virtual void ExportColumn(XlColumn column)
        {
            this.WriteShStartElement("col");
            try
            {
                this.ExportColumnProperties(column);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        protected internal virtual void ExportColumnProperties(XlColumn column)
        {
            this.WriteShIntValue("min", column.ColumnIndex + 1);
            this.WriteShIntValue("max", column.ColumnIndex + 1);
            if (column.IsCollapsed)
            {
                this.WriteShBoolValue("collapsed", true);
            }
            if (column.IsHidden)
            {
                this.WriteShBoolValue("hidden", true);
            }
            if (column.OutlineLevel > 0)
            {
                this.WriteShIntValue("outlineLevel", column.OutlineLevel);
            }
            if (column.WidthInPixels < 0)
            {
                this.WriteStringValue("width", ColumnWidthConverter.PixelsToCharactersWidth(this.currentSheet.DefaultColumnWidthInPixels, this.currentSheet.DefaultMaxDigitWidthInPixels).ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                this.WriteStringValue("width", ColumnWidthConverter.PixelsToCharactersWidth((float) column.WidthInPixels, this.currentSheet.DefaultMaxDigitWidthInPixels).ToString(CultureInfo.InvariantCulture));
                this.WriteShIntValue("customWidth", 1);
            }
            int num = this.RegisterFormatting(column.Formatting);
            if (num > 0)
            {
                this.WriteShIntValue("style", num);
            }
        }

        protected internal virtual void ExportMergedCell(XlCellRange mergedCell)
        {
            this.WriteShStartElement("mergeCell");
            try
            {
                this.WriteStringValue("ref", mergedCell.ToString(true));
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void ExportPageBreak(int index)
        {
            this.WriteShStartElement("brk");
            try
            {
                this.WriteIntValue("id", index);
                this.WriteBoolValue("man", true);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void ExportPendingColumns()
        {
            if (this.pendingColumns.Count > 0)
            {
                this.WriteShStartElement("cols");
                try
                {
                    this.pendingColumns.ForEach(new Action<XlColumn>(this.ExportColumn));
                }
                finally
                {
                    this.WriteShEndElement();
                }
                this.pendingColumns.Clear();
            }
        }

        private void ExportPendingRow()
        {
            if (this.pendingRow != null)
            {
                this.WriteShIntValue("r", this.pendingRow.RowIndex + 1);
                if (this.pendingRow.IsCollapsed)
                {
                    this.WriteShBoolValue("collapsed", true);
                }
                if (this.pendingRow.IsHidden || ((this.currentGroup != null) && this.currentGroup.IsCollapsed))
                {
                    this.WriteShBoolValue("hidden", true);
                }
                if ((this.currentGroup != null) && (this.currentGroup.OutlineLevel > 0))
                {
                    this.WriteShIntValue("outlineLevel", Math.Min(7, this.currentGroup.OutlineLevel));
                }
                int num = this.RegisterFormatting(this.pendingRow.Formatting);
                if (num > 0)
                {
                    this.WriteShIntValue("s", num);
                    this.WriteBoolValue("customFormat", true);
                }
                if (this.pendingRow.HeightInPixels >= 0)
                {
                    this.WriteStringValue("ht", Math.Min(409.5f, XlGraphicUnitConverter.PixelsToPointsF((float) this.pendingRow.HeightInPixels, this.DpiY)).ToString(CultureInfo.InvariantCulture));
                    this.WriteBoolValue("customHeight", true);
                }
                this.rowIndex = this.pendingRow.RowIndex;
                this.pendingRow = null;
            }
        }

        private void ExportPendingSheet()
        {
            if (this.pendingSheet != null)
            {
                this.GenerateSheetProperties(this.pendingSheet);
                this.GenerateSheetViews(this.pendingSheet);
                this.GenerateSheetFormatProperties(this.pendingSheet);
                this.ExportPendingColumns();
                this.WriteShStartElement("sheetData");
                this.pendingSheet = null;
            }
        }

        private void ExportRichTextString(XlRichTextString richText)
        {
            IList<XlRichTextRun> runs = richText.Runs;
            int count = runs.Count;
            for (int i = 0; i < count; i++)
            {
                this.WriteShStartElement("r");
                try
                {
                    XlRichTextRun run = runs[i];
                    this.GenerateTextRunPropertiesContent(run.Font);
                    this.WriteShString("t", this.EncodeXmlCharsText(run.Text), true);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void ExportRowCells()
        {
            foreach (XlCell cell in this.rowCells)
            {
                this.WriteShStartElement("c");
                bool hasFormula = cell.HasFormula;
                XlVariantValue value2 = cell.Value;
                if (value2.IsText)
                {
                    this.WriteShStringValue("t", hasFormula ? "str" : "s");
                }
                else if (value2.IsBoolean)
                {
                    this.WriteShStringValue("t", "b");
                }
                else if (value2.IsError)
                {
                    this.WriteShStringValue("t", "e");
                }
                this.WriteShStringValue("r", this.GetCellReference(cell));
                int num = this.RegisterFormatting(cell.Formatting);
                if (num > 0)
                {
                    this.WriteShIntValue("s", num);
                }
                if (hasFormula)
                {
                    this.ExportCellFormula(cell);
                    this.calculationOptions.FullCalculationOnLoad |= value2.IsEmpty;
                }
                if (!value2.IsEmpty)
                {
                    this.ExportCellValue(cell, hasFormula);
                }
                this.WriteShEndElement();
            }
        }

        private void ExportSharedStringItem(IXlString item)
        {
            this.WriteShStartElement("si");
            try
            {
                if (item.IsPlainText)
                {
                    this.WriteShString("t", this.EncodeXmlCharsText(item.Text), true);
                }
                else
                {
                    this.ExportRichTextString(item as XlRichTextString);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private string FindImageFile(byte[] digest)
        {
            string fileName;
            using (List<XlsxPictureFileInfo>.Enumerator enumerator = this.imageFiles.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlsxPictureFileInfo current = enumerator.Current;
                        if (!current.EqualsDigest(digest))
                        {
                            continue;
                        }
                        fileName = current.FileName;
                    }
                    else
                    {
                        return string.Empty;
                    }
                    break;
                }
            }
            return fileName;
        }

        private void GenerateAutoFilterColumns(IXlSheet sheet)
        {
            XlFilterColumnsChecker.Check(sheet.AutoFilterColumns, sheet.AutoFilterRange);
            foreach (XlFilterColumn column in sheet.AutoFilterColumns)
            {
                this.WriteShStartElement("filterColumn");
                try
                {
                    this.WriteIntValue("colId", column.ColumnId);
                    if (column.HiddenButton)
                    {
                        this.WriteBoolValue("hiddenButton", true);
                    }
                    if (column.FilterCriteria != null)
                    {
                        if (column.FilterCriteria.FilterType == XlFilterType.Values)
                        {
                            this.GenerateValuesFilter(column.FilterCriteria as XlValuesFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Top10)
                        {
                            this.GenerateTop10Filter(column.FilterCriteria as XlTop10Filter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Custom)
                        {
                            this.GenerateCustomFilters(column.FilterCriteria as XlCustomFilters);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Dynamic)
                        {
                            this.GenerateDynamicFilter(column.FilterCriteria as XlDynamicFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Color)
                        {
                            this.GenerateColorFilter(column.FilterCriteria as XlColorFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Icon)
                        {
                            this.GenerateIconFilter(column.FilterCriteria as XlIconFilter);
                        }
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateAutoFilterContent(IXlSheet sheet)
        {
            if (sheet.AutoFilterRange != null)
            {
                this.WriteShStartElement("autoFilter");
                try
                {
                    this.WriteStringValue("ref", sheet.AutoFilterRange.ToString());
                    this.GenerateAutoFilterColumns(sheet);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateAutoFilterDatabase(int sheetIndex, IXlSheet sheet)
        {
            if (sheet.AutoFilterRange != null)
            {
                XlCellRange range = sheet.AutoFilterRange.AsAbsolute();
                range.SheetName = sheet.Name;
                this.WriteShStartElement("definedName");
                try
                {
                    SheetInfo info = this.sheetInfos[sheet];
                    this.WriteShStringValue("name", "_xlnm._FilterDatabase");
                    this.WriteBoolValue("hidden", true);
                    this.WriteShIntValue("localSheetId", sheetIndex);
                    this.WriteShString(range.ToString(true));
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateBordersContent()
        {
            this.WriteShStartElement("borders");
            try
            {
                this.WriteIntValue("count", this.bordersTable.Count);
                foreach (XlBorder border in this.bordersTable.Keys)
                {
                    this.WriteBorders(border);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCalculationProperties()
        {
            if (this.ShouldWriteCalculationProperties())
            {
                this.WriteShStartElement("calcPr");
                try
                {
                    this.WriteShBoolValue("fullCalcOnLoad", true);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateCellFormatsContent()
        {
            this.WriteShStartElement("cellXfs");
            try
            {
                this.WriteIntValue("count", this.cellXfTable.Count);
                foreach (XlCellXf xf in this.cellXfTable.Keys)
                {
                    this.WriteCellFormat(xf);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCellStyleFormatsContent()
        {
            this.WriteShStartElement("cellStyleXfs");
            try
            {
                this.WriteIntValue("count", 1);
                this.WriteShStartElement("xf");
                try
                {
                    this.WriteIntValue("numFmtId", 0);
                    this.WriteIntValue("fontId", 0);
                    this.WriteIntValue("fillId", 0);
                    this.WriteIntValue("borderId", 0);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCellStylesContent()
        {
            this.WriteShStartElement("cellStyles");
            try
            {
                this.WriteIntValue("count", 1);
                this.WriteShStartElement("cellStyle");
                try
                {
                    this.WriteStringValue("name", "Normal");
                    this.WriteIntValue("xfId", 0);
                    this.WriteIntValue("builtinId", 0);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateColorFilter(XlColorFilter filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("colorFilter");
                try
                {
                    XlDifferentialFormatting formatting = this.CreateColorFilterFormat(filter);
                    int num = this.RegisterDifferentialFormatting(formatting);
                    this.WriteIntValue("dxfId", num);
                    if (!filter.FilterByCellColor)
                    {
                        this.WriteBoolValue("cellColor", false);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateColumnPageBreaks(IXlSheet sheet)
        {
            this.GeneratePageBreaksContent(sheet.ColumnPageBreaks, "colBreaks");
        }

        private void GenerateCondFmtRuleAboveAverage(XlCondFmtRuleAboveAverage rule)
        {
            if (!rule.AboveAverage)
            {
                this.WriteBoolValue("aboveAverage", rule.AboveAverage);
            }
            if (rule.EqualAverage)
            {
                this.WriteBoolValue("equalAverage", rule.EqualAverage);
            }
            if (rule.StdDev > 0)
            {
                this.WriteIntValue("stdDev", rule.StdDev);
            }
        }

        private void GenerateCondFmtRuleBlanks(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleBlanks rule)
        {
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            string text = (rule.RuleType != XlCondFmtType.ContainsBlanks) ? $"LEN(TRIM({topLeftCell.ToString()}))>0" : $"LEN(TRIM({topLeftCell.ToString()}))=0";
            this.WriteShString("formula", text, true);
        }

        private void GenerateCondFmtRuleCellIs(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleCellIs rule)
        {
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            this.WriteStringValue("operator", conditionalFormattingOperatorTable[rule.Operator]);
            this.WriteCondFmtValue("formula", rule.Value, topLeftCell);
            if ((rule.Operator == XlCondFmtOperator.Between) || (rule.Operator == XlCondFmtOperator.NotBetween))
            {
                this.WriteCondFmtValue("formula", rule.SecondValue, topLeftCell);
            }
        }

        private void GenerateCondFmtRuleColorScale(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleColorScale rule)
        {
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            this.WriteShStartElement("colorScale");
            try
            {
                if (rule.ColorScaleType == XlCondFmtColorScaleType.ColorScale2)
                {
                    this.WriteCfvo(rule.MinValue, topLeftCell);
                    this.WriteCfvo(rule.MaxValue, topLeftCell);
                    this.WriteColor(rule.MinColor, "color");
                    this.WriteColor(rule.MaxColor, "color");
                }
                else
                {
                    this.WriteCfvo(rule.MinValue, topLeftCell);
                    this.WriteCfvo(rule.MidpointValue, topLeftCell);
                    this.WriteCfvo(rule.MaxValue, topLeftCell);
                    this.WriteColor(rule.MinColor, "color");
                    this.WriteColor(rule.MidpointColor, "color");
                    this.WriteColor(rule.MaxColor, "color");
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCondFmtRuleDataBar(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleDataBar rule)
        {
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            this.WriteShStartElement("dataBar");
            try
            {
                if (!rule.ShowValues)
                {
                    this.WriteBoolValue("showValue", rule.ShowValues);
                }
                this.WriteCfvo(rule.MinValue, topLeftCell);
                this.WriteCfvo(rule.MaxValue, topLeftCell);
                this.WriteColor(rule.FillColor, "color");
            }
            finally
            {
                this.WriteShEndElement();
            }
            this.WriteShStartElement("extLst");
            try
            {
                this.WriteExtListReference(rule.GetRuleId());
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCondFmtRuleExpression(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleExpression rule)
        {
            this.expressionContext.CurrentCell = conditionalFormatting.GetTopLeftCell();
            this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
            this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
            if (rule.Expression != null)
            {
                string text = rule.Expression.ToString(this.expressionContext);
                this.WriteShString("formula", text, true);
            }
            else if (!string.IsNullOrEmpty(rule.Formula))
            {
                if ((this.formulaParser != null) && (this.formulaParser.Parse(rule.Formula, this.expressionContext) == null))
                {
                    throw new InvalidOperationException($"Can't parse rule formula '{rule.Formula}'.");
                }
                this.WriteShString("formula", rule.Formula, true);
            }
        }

        private void GenerateCondFmtRuleSpecificText(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleSpecificText rule)
        {
            string str;
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            if (rule.RuleType == XlCondFmtType.ContainsText)
            {
                this.WriteStringValue("operator", conditionalFormattingOperatorTable[XlCondFmtOperator.ContainsText]);
                str = $"NOT(ISERROR(SEARCH("{rule.Text}",{topLeftCell.ToString()})))";
            }
            else if (rule.RuleType == XlCondFmtType.NotContainsText)
            {
                this.WriteStringValue("operator", conditionalFormattingOperatorTable[XlCondFmtOperator.NotContains]);
                str = $"ISERROR(SEARCH("{rule.Text}",{topLeftCell.ToString()}))";
            }
            else if (rule.RuleType == XlCondFmtType.BeginsWith)
            {
                this.WriteStringValue("operator", conditionalFormattingOperatorTable[XlCondFmtOperator.BeginsWith]);
                str = string.Format("LEFT({1},LEN(\"{0}\"))=\"{0}\"", rule.Text, topLeftCell.ToString());
            }
            else
            {
                this.WriteStringValue("operator", conditionalFormattingOperatorTable[XlCondFmtOperator.EndsWith]);
                str = string.Format("RIGHT({1},LEN(\"{0}\"))=\"{0}\"", rule.Text, topLeftCell.ToString());
            }
            this.WriteStringValue("text", rule.Text);
            this.WriteShString("formula", this.EncodeXmlChars(str), true);
        }

        private void GenerateCondFmtRuleTimePeriod(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleTimePeriod rule)
        {
            this.WriteStringValue("timePeriod", timePeriodTable[rule.TimePeriod]);
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            string timePeriodFormula = this.GetTimePeriodFormula(rule.TimePeriod, topLeftCell);
            this.WriteShString("formula", this.EncodeXmlChars(timePeriodFormula), true);
        }

        private void GenerateCondFmtRuleTop10(XlCondFmtRuleTop10 rule)
        {
            if (rule.Bottom)
            {
                this.WriteBoolValue("bottom", rule.Bottom);
            }
            if (rule.Percent)
            {
                this.WriteBoolValue("percent", rule.Percent);
            }
            this.WriteIntValue("rank", rule.Rank);
        }

        private void GenerateConditionalFormatting(XlConditionalFormatting conditionalFormatting)
        {
            if (this.ShouldExportConditionalFormatting(conditionalFormatting))
            {
                this.WriteShStartElement("conditionalFormatting");
                try
                {
                    this.WriteStringValue("sqref", this.GetSqRef(conditionalFormatting.Ranges));
                    foreach (XlCondFmtRule rule in conditionalFormatting.Rules)
                    {
                        this.GenerateConditionalFormattingRule(conditionalFormatting, rule);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateConditionalFormattingExt(XlConditionalFormatting conditionalFormatting)
        {
            if (this.ShouldExportCondFmtExt(conditionalFormatting))
            {
                this.WriteStartElement("conditionalFormatting", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                this.WriteStringAttr("xmlns", "xm", null, "http://schemas.microsoft.com/office/excel/2006/main");
                try
                {
                    foreach (XlCondFmtRule rule in conditionalFormatting.Rules)
                    {
                        if (rule.RuleType == XlCondFmtType.DataBar)
                        {
                            this.WriteCondFmtRuleDataBarExt(conditionalFormatting, rule as XlCondFmtRuleDataBar);
                        }
                        if (rule.RuleType == XlCondFmtType.IconSet)
                        {
                            this.WriteCondFmtRuleIconSetExt(conditionalFormatting, rule as XlCondFmtRuleIconSet);
                        }
                    }
                    this.WriteString("sqref", "http://schemas.microsoft.com/office/excel/2006/main", this.GetSqRef(conditionalFormatting.Ranges));
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void GenerateConditionalFormattingRule(XlConditionalFormatting conditionalFormatting, XlCondFmtRule rule)
        {
            if (rule.RuleType != XlCondFmtType.IconSet)
            {
                this.WriteShStartElement("cfRule");
                try
                {
                    this.WriteIntValue("priority", rule.Priority);
                    if (rule.StopIfTrue)
                    {
                        this.WriteBoolValue("stopIfTrue", rule.StopIfTrue);
                    }
                    this.WriteDxfId(rule as XlCondFmtRuleWithFormatting);
                    this.WriteStringValue("type", conditionalFormattingTypeTable[rule.RuleType]);
                    switch (rule.RuleType)
                    {
                        case XlCondFmtType.AboveOrBelowAverage:
                            this.GenerateCondFmtRuleAboveAverage(rule as XlCondFmtRuleAboveAverage);
                            break;

                        case XlCondFmtType.BeginsWith:
                        case XlCondFmtType.ContainsText:
                        case XlCondFmtType.EndsWith:
                        case XlCondFmtType.NotContainsText:
                            this.GenerateCondFmtRuleSpecificText(conditionalFormatting, rule as XlCondFmtRuleSpecificText);
                            break;

                        case XlCondFmtType.CellIs:
                            this.GenerateCondFmtRuleCellIs(conditionalFormatting, rule as XlCondFmtRuleCellIs);
                            break;

                        case XlCondFmtType.ColorScale:
                            this.GenerateCondFmtRuleColorScale(conditionalFormatting, rule as XlCondFmtRuleColorScale);
                            break;

                        case XlCondFmtType.ContainsBlanks:
                        case XlCondFmtType.NotContainsBlanks:
                            this.GenerateCondFmtRuleBlanks(conditionalFormatting, rule as XlCondFmtRuleBlanks);
                            break;

                        case XlCondFmtType.DataBar:
                            this.GenerateCondFmtRuleDataBar(conditionalFormatting, rule as XlCondFmtRuleDataBar);
                            break;

                        case XlCondFmtType.Expression:
                            this.GenerateCondFmtRuleExpression(conditionalFormatting, rule as XlCondFmtRuleExpression);
                            break;

                        case XlCondFmtType.TimePeriod:
                            this.GenerateCondFmtRuleTimePeriod(conditionalFormatting, rule as XlCondFmtRuleTimePeriod);
                            break;

                        case XlCondFmtType.Top10:
                            this.GenerateCondFmtRuleTop10(rule as XlCondFmtRuleTop10);
                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateConditionalFormattings(IList<XlConditionalFormatting> conditionalFormattings)
        {
            foreach (XlConditionalFormatting formatting in conditionalFormattings)
            {
                this.GenerateConditionalFormatting(formatting);
            }
        }

        private void GenerateConditionalFormattingsExt(IList<XlConditionalFormatting> conditionalFormattings)
        {
            this.WriteShStartElement("ext");
            try
            {
                this.WriteStringAttr("xmlns", "x14", null, "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                this.WriteStringValue("uri", "{78C0D931-6437-407d-A8EE-F0AAD7539E65}");
                this.WriteStartElement("x14", "conditionalFormattings", null);
                try
                {
                    foreach (XlConditionalFormatting formatting in conditionalFormattings)
                    {
                        this.GenerateConditionalFormattingExt(formatting);
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateCustomFilterCriteria(XlCustomFilterCriteria criteria)
        {
            if (criteria != null)
            {
                this.WriteShStartElement("customFilter");
                try
                {
                    this.WriteStringValue("operator", this.FilterOperatorTable[criteria.FilterOperator]);
                    this.WriteStringValue("val", criteria.Value.ToText().TextValue);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateCustomFilters(XlCustomFilters filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("customFilters");
                try
                {
                    if (filter.And)
                    {
                        this.WriteBoolValue("and", true);
                    }
                    this.GenerateCustomFilterCriteria(filter.First);
                    this.GenerateCustomFilterCriteria(filter.Second);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        protected internal virtual void GenerateDataValidation(XlDataValidation validation)
        {
            if (!validation.IsExtended)
            {
                this.WriteShStartElement("dataValidation");
                try
                {
                    this.WriteDataValidationAttributes(validation);
                    this.WriteStringValue("sqref", this.GetSqrefString(validation));
                    if (validation.Type == XlDataValidationType.List)
                    {
                        if (validation.ListRange != null)
                        {
                            this.WriteString("formula1", null, validation.ListRange.ToString());
                        }
                        else
                        {
                            this.WriteString("formula1", null, this.CreateListValues(validation.ListValues));
                        }
                    }
                    else
                    {
                        XlCellPosition topLeftCell = this.GetTopLeftCell(validation.Ranges);
                        if (!validation.Criteria1.IsEmpty)
                        {
                            this.WriteString("formula1", null, this.GetValueObjectString(validation.Criteria1, topLeftCell));
                        }
                        if (!validation.Criteria2.IsEmpty)
                        {
                            this.WriteString("formula2", null, this.GetValueObjectString(validation.Criteria2, topLeftCell));
                        }
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        protected internal void GenerateDataValidationExt(XlDataValidation validation)
        {
            if (validation.IsExtended)
            {
                this.WriteStartElement("dataValidation", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                try
                {
                    this.WriteDataValidationAttributes(validation);
                    if (validation.Type == XlDataValidationType.List)
                    {
                        if (validation.ListRange != null)
                        {
                            this.WriteDataValidationFormulaExt("formula1", validation.ListRange.ToString());
                        }
                        else
                        {
                            this.WriteDataValidationFormulaExt("formula1", this.CreateListValues(validation.ListValues));
                        }
                    }
                    else
                    {
                        XlCellPosition topLeftCell = this.GetTopLeftCell(validation.Ranges);
                        if (!validation.Criteria1.IsEmpty)
                        {
                            this.WriteDataValidationFormulaExt("formula1", this.GetValueObjectString(validation.Criteria1, topLeftCell));
                        }
                        if (!validation.Criteria2.IsEmpty)
                        {
                            this.WriteDataValidationFormulaExt("formula2", this.GetValueObjectString(validation.Criteria2, topLeftCell));
                        }
                    }
                    this.WriteString("sqref", "http://schemas.microsoft.com/office/excel/2006/main", this.GetSqrefString(validation));
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDataValidations(IList<XlDataValidation> dataValidations)
        {
            int num = this.CountItems(dataValidations, false);
            if (num > 0)
            {
                this.WriteShStartElement("dataValidations");
                try
                {
                    this.WriteIntValue("count", num);
                    foreach (XlDataValidation validation in dataValidations)
                    {
                        this.GenerateDataValidation(validation);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDataValidationsExt(IList<XlDataValidation> dataValidations)
        {
            int num = this.CountItems(dataValidations, true);
            if (num > 0)
            {
                this.WriteShStartElement("ext");
                try
                {
                    this.WriteStringAttr("xmlns", "x14", null, "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    this.WriteStringValue("uri", "{CCE6A557-97BC-4b89-ADB6-D9C93CAAB3DF}");
                    this.WriteStartElement("dataValidations", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    try
                    {
                        this.WriteStringAttr("xmlns", "xm", null, "http://schemas.microsoft.com/office/excel/2006/main");
                        this.WriteIntValue("count", num);
                        foreach (XlDataValidation validation in dataValidations)
                        {
                            this.GenerateDataValidationExt(validation);
                        }
                    }
                    finally
                    {
                        this.WriteShEndElement();
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDefinedNames()
        {
            if (this.ShouldWriteDefinedNames())
            {
                this.WriteShStartElement("definedNames");
                try
                {
                    this.GenerateSheetDefinedNames();
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDifferentialFormatContent(XlDxf dxf)
        {
            this.WriteShStartElement("dxf");
            try
            {
                if (dxf.Font != null)
                {
                    this.WriteFont(dxf.Font, false);
                }
                if (dxf.NumberFormat != null)
                {
                    this.WriteNumberFormat(dxf.NumberFormat);
                }
                if (dxf.Fill != null)
                {
                    this.WriteFill(dxf.Fill);
                }
                if (dxf.Alignment != null)
                {
                    this.WriteAlignment(dxf.Alignment);
                }
                if (dxf.Border != null)
                {
                    this.WriteBorders(dxf.Border);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateDifferentialFormatsContent()
        {
            if (this.dxfTable.Count != 0)
            {
                this.WriteShStartElement("dxfs");
                try
                {
                    this.WriteIntValue("count", this.dxfTable.Count);
                    foreach (XlDxf dxf in this.dxfTable.Keys)
                    {
                        this.GenerateDifferentialFormatContent(dxf);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDrawingContent()
        {
            if (this.drawingObjects.Count != 0)
            {
                this.BeginWriteXmlContent();
                this.GenerateDrawingContentCore();
                this.AddPackageContent($"xl\drawings\drawing{this.drawingId}.xml", this.EndWriteXmlContent());
                this.GenerateDrawingRelations();
                this.Builder.OverriddenContentTypes.Add($"/xl/drawings/drawing{this.drawingId}.xml", "application/vnd.openxmlformats-officedocument.drawing+xml");
            }
        }

        private void GenerateDrawingContentCore()
        {
            this.WriteStartElement("xdr", "wsDr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringAttr("xmlns", "xdr", null, "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
                this.WriteStringAttr("xmlns", "a", null, "http://schemas.openxmlformats.org/drawingml/2006/main");
                foreach (XlDrawingObjectBase base2 in this.drawingObjects)
                {
                    if (base2.DrawingObjectType == XlDrawingObjectType.Picture)
                    {
                        this.GenerateDrawingObject(base2 as XlsxPicture);
                        continue;
                    }
                    if (base2.DrawingObjectType == XlDrawingObjectType.Shape)
                    {
                        this.GenerateShape(base2 as XlShape);
                    }
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void GenerateDrawingObject(XlsxPicture drawingObject)
        {
            switch (drawingObject.AnchorType)
            {
                case XlAnchorType.TwoCell:
                    this.WriteTwoCellAnchor(drawingObject, new Action<XlDrawingObjectBase>(this.WritePicture));
                    return;

                case XlAnchorType.OneCell:
                    this.WriteOneCellAnchor(drawingObject, new Action<XlDrawingObjectBase>(this.WritePicture));
                    return;

                case XlAnchorType.Absolute:
                    this.WriteAbsoluteAnchor(drawingObject, new Action<XlDrawingObjectBase>(this.WritePicture));
                    return;
            }
        }

        private void GenerateDrawingRef()
        {
            if (this.drawingObjects.Count != 0)
            {
                string id = this.sheetRelations.GenerateId();
                OpenXmlRelation item = new OpenXmlRelation(id, $"../drawings/drawing{this.drawingId}.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing");
                this.sheetRelations.Add(item);
                this.WriteShStartElement("drawing");
                try
                {
                    this.WriteStringAttr("r", "id", null, id);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateDrawingRelations()
        {
            string fileName = $"xl\drawings\_rels\drawing{this.drawingId}.xml.rels";
            this.BeginWriteXmlContent();
            this.Builder.GenerateRelationsContent(this.writer, this.drawingRelations);
            this.AddPackageContent(fileName, this.EndWriteXmlContent());
        }

        private void GenerateDynamicFilter(XlDynamicFilter filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("dynamicFilter");
                try
                {
                    this.WriteStringValue("type", this.DynamicFilterTypeTable[filter.DynamicFilterType]);
                    if (filter.DynamicFilterValueRequired() && filter.Value.IsNumeric)
                    {
                        this.WriteDoubleValue("val", filter.Value.NumericValue);
                    }
                    if (filter.DynamicFilterMaxValueRequired() && filter.MaxValue.IsNumeric)
                    {
                        this.WriteDoubleValue("maxVal", filter.MaxValue.NumericValue);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateExtList(IXlSheet sheet)
        {
            bool flag = this.ShouldExportCondFmtExtList(sheet.ConditionalFormattings);
            bool flag2 = this.ShouldExportDataValidationsExt(sheet.DataValidations);
            bool flag3 = this.ShouldExportSparklineGroupsExt(sheet.SparklineGroups);
            if (flag || (flag2 || flag3))
            {
                this.WriteShStartElement("extLst");
                try
                {
                    if (flag)
                    {
                        this.GenerateConditionalFormattingsExt(sheet.ConditionalFormattings);
                    }
                    if (flag2)
                    {
                        this.GenerateDataValidationsExt(sheet.DataValidations);
                    }
                    if (flag3)
                    {
                        this.GenerateSparklineGroupsExt(sheet);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateFillsContent()
        {
            this.WriteShStartElement("fills");
            try
            {
                this.WriteIntValue("count", this.fillsTable.Count);
                foreach (XlFill fill in this.fillsTable.Keys)
                {
                    this.WriteFill(fill);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateFontsContent()
        {
            this.WriteShStartElement("fonts");
            try
            {
                this.WriteIntValue("count", this.fontsTable.Count);
                int num = 0;
                foreach (XlFont font in this.fontsTable.Keys)
                {
                    this.WriteFont(font, this.fontsTable[font] == num);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateHeaderFooter(XlHeaderFooter options)
        {
            if (!options.IsDefault())
            {
                this.WriteShStartElement("headerFooter");
                try
                {
                    if (!options.AlignWithMargins)
                    {
                        this.WriteBoolValue("alignWithMargins", options.AlignWithMargins);
                    }
                    if (options.DifferentFirst)
                    {
                        this.WriteBoolValue("differentFirst", options.DifferentFirst);
                    }
                    if (options.DifferentOddEven)
                    {
                        this.WriteBoolValue("differentOddEven", options.DifferentOddEven);
                    }
                    if (!options.ScaleWithDoc)
                    {
                        this.WriteBoolValue("scaleWithDoc", options.ScaleWithDoc);
                    }
                    this.WriteHeaderFooterItem("oddHeader", options.OddHeader);
                    this.WriteHeaderFooterItem("oddFooter", options.OddFooter);
                    this.WriteHeaderFooterItem("evenHeader", options.EvenHeader);
                    this.WriteHeaderFooterItem("evenFooter", options.EvenFooter);
                    this.WriteHeaderFooterItem("firstHeader", options.FirstHeader);
                    this.WriteHeaderFooterItem("firstFooter", options.FirstFooter);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateHyperlink(XlHyperlink hyperlink)
        {
            if ((hyperlink.Reference != null) && !string.IsNullOrEmpty(hyperlink.TargetUri))
            {
                this.WriteShStartElement("hyperlink");
                try
                {
                    this.WriteStringValue("ref", hyperlink.Reference.ToString());
                    string targetUri = hyperlink.TargetUri;
                    string str2 = string.Empty;
                    int index = targetUri.IndexOf('#');
                    if (index != -1)
                    {
                        str2 = targetUri.Substring(index + 1);
                        targetUri = targetUri.Substring(0, index);
                    }
                    if (!string.IsNullOrEmpty(targetUri))
                    {
                        string id = this.sheetRelations.GenerateId();
                        this.sheetRelations.Add(new OpenXmlRelation(id, this.EscapeHyperlinkUri(targetUri), "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink", "External"));
                        this.WriteStringAttr("r", "id", null, id);
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        this.WriteStringValue("location", this.EncodeXmlChars(str2));
                    }
                    if (!string.IsNullOrEmpty(hyperlink.Tooltip))
                    {
                        this.WriteStringValue("tooltip", this.EncodeXmlChars(hyperlink.Tooltip));
                    }
                    if (!string.IsNullOrEmpty(hyperlink.DisplayText))
                    {
                        this.WriteStringValue("display", this.EncodeXmlChars(hyperlink.DisplayText));
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateHyperlinks(IList<XlHyperlink> hyperlinks)
        {
            if (this.ShouldExportHyperlinks(hyperlinks))
            {
                this.WriteShStartElement("hyperlinks");
                try
                {
                    foreach (XlHyperlink hyperlink in hyperlinks)
                    {
                        this.GenerateHyperlink(hyperlink);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateIconFilter(XlIconFilter filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("iconFilter");
                try
                {
                    this.WriteStringValue("iconSet", iconSetTypeTable[filter.IconSet]);
                    if (filter.IconId >= 0)
                    {
                        this.WriteIntValue("iconId", filter.IconId);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateIgnoredErrors(IXlSheet sheet)
        {
            if (sheet.IgnoreErrors != DevExpress.Export.Xl.XlIgnoreErrors.None)
            {
                XlCellRange dataRange = sheet.DataRange;
                if (dataRange != null)
                {
                    this.WriteShStartElement("ignoredErrors");
                    try
                    {
                        this.WriteShStartElement("ignoredError");
                        try
                        {
                            this.WriteStringValue("sqref", dataRange.ToString());
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.CalculatedColumn) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("calculatedColumn", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.EmptyCellReference) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("emptyCellReference", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.EvaluationError) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("evalError", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.Formula) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("formula", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.FormulaRange) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("formulaRange", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.ListDataValidation) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("listDataValidation", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.NumberStoredAsText) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("numberStoredAsText", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.TwoDigitTextYear) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("twoDigitTextYear", true);
                            }
                            if ((sheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.UnlockedFormula) != DevExpress.Export.Xl.XlIgnoreErrors.None)
                            {
                                this.WriteBoolValue("unlockedFormula", true);
                            }
                        }
                        finally
                        {
                            this.WriteShEndElement();
                        }
                    }
                    finally
                    {
                        this.WriteShEndElement();
                    }
                }
            }
        }

        protected internal virtual void GenerateMergedCellsContent(IXlSheet sheet)
        {
            int count = sheet.MergedCells.Count;
            if (count > 0)
            {
                this.WriteShStartElement("mergeCells");
                try
                {
                    this.WriteIntValue("count", count);
                    foreach (XlCellRange range in sheet.MergedCells)
                    {
                        this.ExportMergedCell(range);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateNumberFormatsContent()
        {
            int count = this.numberFormatsTable.Count;
            if (count != 0)
            {
                this.WriteShStartElement("numFmts");
                try
                {
                    this.WriteIntValue("count", count);
                    foreach (ExcelNumberFormat format in this.numberFormatsTable.Values)
                    {
                        this.WriteNumberFormat(format);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateOutlineProperties(IXlSheet sheet)
        {
            if (!sheet.OutlineProperties.SummaryBelow || !sheet.OutlineProperties.SummaryRight)
            {
                this.WriteShStartElement("outlinePr");
                try
                {
                    if (!this.pendingSheet.OutlineProperties.SummaryBelow)
                    {
                        this.WriteShBoolValue("summaryBelow", false);
                    }
                    if (!this.pendingSheet.OutlineProperties.SummaryRight)
                    {
                        this.WriteShBoolValue("summaryRight", false);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePageBreaksContent(IXlPageBreaks breaks, string tagName)
        {
            if (breaks.Count > 0)
            {
                this.WriteShStartElement(tagName);
                try
                {
                    this.WriteIntValue("count", breaks.Count);
                    this.WriteIntValue("manualBreakCount", breaks.Count);
                    foreach (int num in breaks)
                    {
                        this.ExportPageBreak(num);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePageMargins(XlPageMargins margins)
        {
            if (margins != null)
            {
                this.WriteShStartElement("pageMargins");
                try
                {
                    this.WriteMarginValue(margins.LeftInches, "left");
                    this.WriteMarginValue(margins.RightInches, "right");
                    this.WriteMarginValue(margins.TopInches, "top");
                    this.WriteMarginValue(margins.BottomInches, "bottom");
                    this.WriteMarginValue(margins.HeaderInches, "header");
                    this.WriteMarginValue(margins.FooterInches, "footer");
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePageSetup(XlPageSetup pageSetup)
        {
            if ((pageSetup != null) && !pageSetup.IsDefault())
            {
                this.WriteShStartElement("pageSetup");
                try
                {
                    if (pageSetup.PaperKind != PaperKind.Letter)
                    {
                        this.WriteIntValue("paperSize", (int) pageSetup.PaperKind);
                    }
                    if (pageSetup.CommentsPrintMode != XlCommentsPrintMode.None)
                    {
                        this.WriteStringValue("cellComments", commentsPrintModeTable[pageSetup.CommentsPrintMode]);
                    }
                    if (pageSetup.ErrorsPrintMode != XlErrorsPrintMode.Displayed)
                    {
                        this.WriteStringValue("errors", errorsPrintModeTable[pageSetup.ErrorsPrintMode]);
                    }
                    if (pageSetup.PagePrintOrder != XlPagePrintOrder.DownThenOver)
                    {
                        this.WriteStringValue("pageOrder", pagePrintOrderTable[pageSetup.PagePrintOrder]);
                    }
                    if (pageSetup.PageOrientation != XlPageOrientation.Default)
                    {
                        this.WriteStringValue("orientation", pageOrientationTable[pageSetup.PageOrientation]);
                    }
                    if (pageSetup.Scale != 100)
                    {
                        this.WriteIntValue("scale", pageSetup.Scale);
                    }
                    if (pageSetup.BlackAndWhite)
                    {
                        this.WriteBoolValue("blackAndWhite", pageSetup.BlackAndWhite);
                    }
                    if (pageSetup.Draft)
                    {
                        this.WriteBoolValue("draft", pageSetup.Draft);
                    }
                    if (!pageSetup.AutomaticFirstPageNumber)
                    {
                        this.WriteBoolValue("useFirstPageNumber", true);
                    }
                    if (!pageSetup.UsePrinterDefaults)
                    {
                        this.WriteBoolValue("usePrinterDefaults", false);
                    }
                    if (pageSetup.Copies != 1)
                    {
                        this.WriteIntValue("copies", pageSetup.Copies);
                    }
                    if (pageSetup.FirstPageNumber != 1)
                    {
                        this.WriteIntValue("firstPageNumber", pageSetup.FirstPageNumber);
                    }
                    if (pageSetup.FitToWidth != 1)
                    {
                        this.WriteIntValue("fitToWidth", pageSetup.FitToWidth);
                    }
                    if (pageSetup.FitToHeight != 1)
                    {
                        this.WriteIntValue("fitToHeight", pageSetup.FitToHeight);
                    }
                    if (pageSetup.HorizontalDpi != 600)
                    {
                        this.WriteIntValue("horizontalDpi", pageSetup.HorizontalDpi);
                    }
                    if (pageSetup.VerticalDpi != 600)
                    {
                        this.WriteIntValue("verticalDpi", pageSetup.VerticalDpi);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePageSetupProperties(IXlSheet sheet)
        {
            if ((sheet.PageSetup != null) && sheet.PageSetup.FitToPage)
            {
                this.WriteShStartElement("pageSetUpPr");
                try
                {
                    this.WriteShBoolValue("fitToPage", true);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePrintArea(int sheetIndex, IXlSheet sheet)
        {
            if (sheet.PrintArea != null)
            {
                XlCellRange range = sheet.PrintArea.AsAbsolute();
                range.SheetName = sheet.Name;
                this.WriteShStartElement("definedName");
                try
                {
                    SheetInfo info = this.sheetInfos[sheet];
                    this.WriteShStringValue("name", "_xlnm.Print_Area");
                    this.WriteShIntValue("localSheetId", sheetIndex);
                    this.WriteShString(range.ToString(true));
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePrintOptions(XlPrintOptions options)
        {
            if ((options != null) && !options.IsDefault())
            {
                this.WriteShStartElement("printOptions");
                try
                {
                    if (options.HorizontalCentered)
                    {
                        this.WriteBoolValue("horizontalCentered", options.HorizontalCentered);
                    }
                    if (options.VerticalCentered)
                    {
                        this.WriteBoolValue("verticalCentered", options.VerticalCentered);
                    }
                    if (options.Headings)
                    {
                        this.WriteBoolValue("headings", options.Headings);
                    }
                    if (options.GridLines)
                    {
                        this.WriteBoolValue("gridLines", options.GridLines);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GeneratePrintTitles(int sheetIndex, IXlSheet sheet)
        {
            if (sheet.PrintTitles.IsValid())
            {
                this.WriteShStartElement("definedName");
                try
                {
                    SheetInfo info = this.sheetInfos[sheet];
                    this.WriteShStringValue("name", "_xlnm.Print_Titles");
                    this.WriteShIntValue("localSheetId", sheetIndex);
                    this.WriteShString(sheet.PrintTitles.ToString());
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateRowPageBreaks(IXlSheet sheet)
        {
            this.GeneratePageBreaksContent(sheet.RowPageBreaks, "rowBreaks");
        }

        private void GenerateShape(XlShape shape)
        {
            if (shape != null)
            {
                if (shape.IsConnector)
                {
                    switch (shape.AnchorType)
                    {
                        case XlAnchorType.TwoCell:
                            this.WriteTwoCellAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteConnectorShape));
                            return;

                        case XlAnchorType.OneCell:
                            this.WriteOneCellAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteConnectorShape));
                            return;

                        case XlAnchorType.Absolute:
                            this.WriteAbsoluteAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteConnectorShape));
                            return;
                    }
                }
                else
                {
                    switch (shape.AnchorType)
                    {
                        case XlAnchorType.TwoCell:
                            this.WriteTwoCellAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteBodyShape));
                            return;

                        case XlAnchorType.OneCell:
                            this.WriteOneCellAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteBodyShape));
                            return;

                        case XlAnchorType.Absolute:
                            this.WriteAbsoluteAnchor(shape, new Action<XlDrawingObjectBase>(this.WriteBodyShape));
                            return;
                    }
                }
            }
        }

        private void GenerateSheetDefinedNames()
        {
            for (int i = 0; i < this.sheets.Count; i++)
            {
                this.GeneratePrintArea(i, this.sheets[i]);
                this.GeneratePrintTitles(i, this.sheets[i]);
                this.GenerateAutoFilterDatabase(i, this.sheets[i]);
            }
        }

        private void GenerateSheetFormatProperties(IXlSheet sheet)
        {
            int num = 0;
            foreach (XlColumn column in this.pendingColumns)
            {
                num = Math.Max(num, column.OutlineLevel);
            }
            if (num != 0)
            {
                this.WriteShStartElement("sheetFormatPr");
                try
                {
                    this.WriteStringValue("defaultRowHeight", "15");
                    this.WriteShIntValue("outlineLevelCol", num);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateSheetProperties(IXlSheet sheet)
        {
            if (this.ShouldExportSheetProperties(sheet))
            {
                this.WriteShStartElement("sheetPr");
                try
                {
                    this.GenerateOutlineProperties(sheet);
                    this.GeneratePageSetupProperties(sheet);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateSheetReference(IXlSheet sheet)
        {
            this.WriteShStartElement("sheet");
            try
            {
                SheetInfo info = this.sheetInfos[sheet];
                this.WriteShStringValue("name", sheet.Name);
                this.WriteShIntValue("sheetId", info.SheetId);
                if (sheet.VisibleState != XlSheetVisibleState.Visible)
                {
                    this.WriteShStringValue("state", VisibilityTypeTable[sheet.VisibleState]);
                }
                this.WriteStringAttr("r", "id", null, info.RelationId);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetReferences()
        {
            this.WriteShStartElement("sheets");
            try
            {
                this.sheets.ForEach(new Action<IXlSheet>(this.GenerateSheetReference));
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetRelations(int sheetId)
        {
            if (this.sheetRelations.Count != 0)
            {
                string fileName = $"xl\worksheets\_rels\sheet{sheetId}.xml.rels";
                this.BeginWriteXmlContent();
                this.Builder.GenerateRelationsContent(this.writer, this.sheetRelations);
                this.AddPackageContent(fileName, this.EndWriteXmlContent());
            }
        }

        protected internal virtual void GenerateSheetView(IXlSheet sheet)
        {
            this.WriteShStartElement("sheetView");
            try
            {
                this.WriteIntValue("workbookViewId", 0);
                IXlSheetViewOptions viewOptions = sheet.ViewOptions;
                if (viewOptions.ShowFormulas)
                {
                    this.WriteBoolValue("showFormulas", viewOptions.ShowFormulas);
                }
                if (!viewOptions.ShowGridLines)
                {
                    this.WriteBoolValue("showGridLines", viewOptions.ShowGridLines);
                }
                if (!viewOptions.ShowRowColumnHeaders)
                {
                    this.WriteBoolValue("showRowColHeaders", viewOptions.ShowRowColumnHeaders);
                }
                if (!viewOptions.ShowZeroValues)
                {
                    this.WriteBoolValue("showZeros", viewOptions.ShowZeroValues);
                }
                if (viewOptions.RightToLeft)
                {
                    this.WriteBoolValue("rightToLeft", viewOptions.RightToLeft);
                }
                if (!viewOptions.ShowOutlineSymbols)
                {
                    this.WriteBoolValue("showOutlineSymbols", viewOptions.ShowOutlineSymbols);
                }
                if (this.ShouldGenerateSheetViewPane(sheet))
                {
                    this.GenerateSheetViewPane(sheet);
                }
                this.GenerateSheetViewSelections(sheet);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetViewPane(IXlSheet sheet)
        {
            this.WriteShStartElement("pane");
            try
            {
                if (!sheet.SplitPosition.EqualsPosition(XlCellPosition.TopLeft))
                {
                    this.WriteStringValue("topLeftCell", sheet.SplitPosition.ToString());
                }
                bool flag = sheet.SplitPosition.Column != XlCellPosition.TopLeft.Column;
                bool flag2 = sheet.SplitPosition.Row != XlCellPosition.TopLeft.Row;
                if (flag)
                {
                    this.WriteIntValue("xSplit", sheet.SplitPosition.Column);
                }
                if (flag2)
                {
                    this.WriteIntValue("ySplit", sheet.SplitPosition.Row);
                }
                if (flag & flag2)
                {
                    this.WriteStringValue("activePane", "bottomRight");
                }
                else if (flag)
                {
                    this.WriteStringValue("activePane", "topRight");
                }
                else if (flag2)
                {
                    this.WriteStringValue("activePane", "bottomLeft");
                }
                this.WriteStringValue("state", "frozen");
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetViews(IXlSheet sheet)
        {
            this.WriteShStartElement("sheetViews");
            try
            {
                this.GenerateSheetView(sheet);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetViewSelection(IXlSheet sheet, XlViewPaneType pane, XlCellPosition splitPosition, XlViewPaneType activePane)
        {
            XlCellPosition topLeft;
            int count = -1;
            List<XlCellRange> ranges = new List<XlCellRange>();
            if (pane != activePane)
            {
                switch (pane)
                {
                    case XlViewPaneType.BottomLeft:
                        topLeft = new XlCellPosition(0, splitPosition.Row);
                        break;

                    case XlViewPaneType.BottomRight:
                        topLeft = splitPosition;
                        break;

                    case XlViewPaneType.TopRight:
                        topLeft = new XlCellPosition(splitPosition.Column, 0);
                        break;

                    default:
                        topLeft = XlCellPosition.TopLeft;
                        break;
                }
                if ((topLeft.Column == 0) && (topLeft.Row == 0))
                {
                    return;
                }
                ranges.Add(new XlCellRange(topLeft));
            }
            else
            {
                topLeft = sheet.Selection.ActiveCell.AsRelative();
                foreach (XlCellRange range in sheet.Selection.SelectedRanges)
                {
                    XlCellRange item = range.AsRelative();
                    item.SheetName = string.Empty;
                    if (item.Contains(topLeft))
                    {
                        count = ranges.Count;
                    }
                    ranges.Add(item);
                }
                if (count == -1)
                {
                    count = ranges.Count;
                    ranges.Add(new XlCellRange(topLeft));
                }
                if (((activePane == XlViewPaneType.TopLeft) && (topLeft.EqualsPosition(XlCellPosition.TopLeft) && (splitPosition.EqualsPosition(XlCellPosition.TopLeft) && (ranges.Count == 1)))) && (ranges[0].TopLeft.EqualsPosition(topLeft) && ranges[0].BottomRight.EqualsPosition(topLeft)))
                {
                    return;
                }
            }
            this.WriteShStartElement("selection");
            try
            {
                if (pane == XlViewPaneType.BottomRight)
                {
                    this.WriteStringValue("pane", "bottomRight");
                }
                else if (pane == XlViewPaneType.BottomLeft)
                {
                    this.WriteStringValue("pane", "bottomLeft");
                }
                else if (pane == XlViewPaneType.TopRight)
                {
                    this.WriteStringValue("pane", "topRight");
                }
                this.WriteStringValue("activeCell", topLeft.ToString());
                if (count > 0)
                {
                    this.WriteIntValue("activeCellId", count);
                }
                this.WriteStringValue("sqref", this.GetSqRef(ranges));
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSheetViewSelections(IXlSheet sheet)
        {
            XlCellPosition splitPosition = sheet.SplitPosition;
            bool hasRightPane = splitPosition.Column > 0;
            bool hasBottomPane = splitPosition.Row > 0;
            if (!(hasRightPane | hasBottomPane))
            {
                this.GenerateSheetViewSelection(sheet, XlViewPaneType.TopLeft, splitPosition, XlViewPaneType.TopLeft);
            }
            else
            {
                XlViewPaneType activePane = this.GetActivePane(hasRightPane, hasBottomPane);
                this.GenerateSheetViewSelection(sheet, XlViewPaneType.TopLeft, splitPosition, activePane);
                if (hasRightPane)
                {
                    this.GenerateSheetViewSelection(sheet, XlViewPaneType.TopRight, splitPosition, activePane);
                }
                if (hasBottomPane)
                {
                    this.GenerateSheetViewSelection(sheet, XlViewPaneType.BottomLeft, splitPosition, activePane);
                }
                if (hasRightPane & hasBottomPane)
                {
                    this.GenerateSheetViewSelection(sheet, XlViewPaneType.BottomRight, splitPosition, activePane);
                }
            }
        }

        private void GenerateSparkline(XlSparkline sparkline)
        {
            this.WriteStartElement("sparkline", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                this.WriteString("f", "http://schemas.microsoft.com/office/excel/2006/main", sparkline.DataRange.ToString());
                this.WriteString("sqref", "http://schemas.microsoft.com/office/excel/2006/main", sparkline.Location.ToString());
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSparklineGroup(IXlSheet sheet, XlSparklineGroup sparklineGroup)
        {
            if (sparklineGroup.Sparklines.Count != 0)
            {
                this.ApplySheetName(sheet, sparklineGroup);
                this.WriteStartElement("sparklineGroup", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                try
                {
                    this.WriteSparklineGroupAttributes(sparklineGroup);
                    if (!sparklineGroup.ColorSeries.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorSeries, "colorSeries", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorNegative.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorNegative, "colorNegative", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorAxis.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorAxis, "colorAxis", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorMarker.IsAutoOrEmpty && (sparklineGroup.SparklineType == XlSparklineType.Line))
                    {
                        this.WriteColor(sparklineGroup.ColorMarker, "colorMarkers", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorFirst.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorFirst, "colorFirst", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorLast.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorLast, "colorLast", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorHigh.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorHigh, "colorHigh", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (!sparklineGroup.ColorLow.IsAutoOrEmpty)
                    {
                        this.WriteColor(sparklineGroup.ColorLow, "colorLow", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                    }
                    if (sparklineGroup.DateRange != null)
                    {
                        this.WriteString("f", "http://schemas.microsoft.com/office/excel/2006/main", sparklineGroup.DateRange.ToString());
                    }
                    this.GenerateSparklines(sparklineGroup.Sparklines);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateSparklineGroupsExt(IXlSheet sheet)
        {
            this.WriteShStartElement("ext");
            try
            {
                this.WriteStringAttr("xmlns", "x14", null, "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                this.WriteStringValue("uri", "{05C60535-1F16-4fd2-B633-F4F36F0B64E0}");
                this.WriteStartElement("sparklineGroups", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                try
                {
                    this.WriteStringAttr("xmlns", "xm", null, "http://schemas.microsoft.com/office/excel/2006/main");
                    foreach (XlSparklineGroup group in sheet.SparklineGroups)
                    {
                        this.GenerateSparklineGroup(sheet, group);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateSparklines(IList<XlSparkline> sparklines)
        {
            this.WriteStartElement("sparklines", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                foreach (XlSparkline sparkline in sparklines)
                {
                    this.GenerateSparkline(sparkline);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateTableAttributes(XlTable table)
        {
            this.WriteIntValue("id", table.Id);
            this.WriteStringValue("name", table.Name);
            this.WriteStringValue("displayName", table.Name);
            this.WriteStringValue("ref", table.Range.ToString());
            if (!string.IsNullOrEmpty(table.Comment))
            {
                this.WriteStringValue("comment", table.Comment);
            }
            if (!table.HasHeaderRow)
            {
                this.WriteIntValue("headerRowCount", 0);
            }
            if (table.InsertRowShowing)
            {
                this.WriteBoolValue("insertRow", table.InsertRowShowing);
            }
            if (table.InsertRowShift)
            {
                this.WriteBoolValue("insertRowShift", table.InsertRowShift);
            }
            if (table.Published)
            {
                this.WriteBoolValue("published", table.Published);
            }
            if (table.HasTotalRow)
            {
                this.WriteIntValue("totalsRowCount", 1);
            }
            this.WriteDxfId("headerRowDxfId", XlDifferentialFormatting.ExcludeBorderFormatting(table.HeaderRowFormatting));
            this.WriteDxfId("dataDxfId", table.DataFormatting);
            this.WriteDxfId("totalsRowDxfId", XlDifferentialFormatting.ExcludeBorderFormatting(table.TotalRowFormatting));
            this.WriteDxfId("headerRowBorderDxfId", XlDifferentialFormatting.ExtractBorderFormatting(table.HeaderRowFormatting));
            this.WriteDxfId("tableBorderDxfId", XlDifferentialFormatting.ExtractBorderFormatting(table.TableBorderFormatting));
            this.WriteDxfId("totalsRowBorderDxfId", XlDifferentialFormatting.ExtractBorderFormatting(table.TotalRowFormatting));
        }

        private void GenerateTableAutoFilterContent(XlTable table)
        {
            if (table.HasHeaderRow && table.HasAutoFilter)
            {
                XlCellPosition bottomRight;
                if (table.HasTotalRow)
                {
                    bottomRight = new XlCellPosition(table.Range.BottomRight.Column, table.Range.BottomRight.Row - 1);
                }
                else
                {
                    bottomRight = table.Range.BottomRight;
                }
                XlCellRange range = new XlCellRange(table.Range.TopLeft, bottomRight);
                this.WriteShStartElement("autoFilter");
                try
                {
                    this.WriteStringValue("ref", range.ToString());
                    foreach (XlTableColumn column in table.InnerColumns)
                    {
                        this.GenerateTableColumnFilter(column);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateTableColumnContent(int columnIndex, XlTable table)
        {
            int num = columnIndex + 1;
            XlTableColumn column = table.InnerColumns[columnIndex];
            this.WriteShStartElement("tableColumn");
            try
            {
                this.WriteIntValue("id", num);
                this.WriteStringValue("name", this.EncodeXmlChars(column.Name));
                if (column.TotalRowFunction != XlTotalRowFunction.None)
                {
                    this.WriteStringValue("totalsRowFunction", totalRowFunctionTable[column.TotalRowFunction]);
                }
                if (!string.IsNullOrEmpty(column.TotalRowLabel))
                {
                    this.WriteStringValue("totalsRowLabel", this.EncodeXmlChars(column.TotalRowLabel));
                }
                if (!table.HasHeaderRow)
                {
                    XlDifferentialFormatting columnHeaderRowFormat = table.GetColumnHeaderRowFormat(column);
                    this.WriteDxfId("headerRowDxfId", columnHeaderRowFormat);
                }
                this.WriteDxfId("dataDxfId", column.DataFormatting);
                if (table.HasTotalRow)
                {
                    this.WriteDxfId("totalsRowDxfId", column.TotalRowFormatting);
                }
                else
                {
                    XlDifferentialFormatting columnTotalRowFormat = table.GetColumnTotalRowFormat(column);
                    this.WriteDxfId("totalsRowDxfId", columnTotalRowFormat);
                }
                if (column.HasColumnFormula)
                {
                    XlCellRange columnDataRange = table.GetColumnDataRange(columnIndex);
                    this.expressionContext.CurrentCell = columnDataRange.TopLeft;
                    this.expressionContext.ReferenceMode = XlCellReferenceMode.Reference;
                    this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                    XlExpression expression = column.GetExpression(this);
                    if ((expression != null) && (expression.Count > 0))
                    {
                        string str = expression.ToString(this.expressionContext);
                        if (str.Length <= 0x2000)
                        {
                            this.WriteString("calculatedColumnFormula", null, this.EncodeXmlCharsNoCrLf(str));
                        }
                    }
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateTableColumnFilter(XlTableColumn column)
        {
            if ((column.FilterCriteria != null) || column.HiddenButton)
            {
                this.WriteShStartElement("filterColumn");
                try
                {
                    this.WriteIntValue("colId", column.ColumnIndex);
                    if (column.HiddenButton)
                    {
                        this.WriteBoolValue("hiddenButton", true);
                    }
                    if (column.FilterCriteria != null)
                    {
                        if (column.FilterCriteria.FilterType == XlFilterType.Values)
                        {
                            this.GenerateValuesFilter(column.FilterCriteria as XlValuesFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Top10)
                        {
                            this.GenerateTop10Filter(column.FilterCriteria as XlTop10Filter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Custom)
                        {
                            this.GenerateCustomFilters(column.FilterCriteria as XlCustomFilters);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Dynamic)
                        {
                            this.GenerateDynamicFilter(column.FilterCriteria as XlDynamicFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Color)
                        {
                            this.GenerateColorFilter(column.FilterCriteria as XlColorFilter);
                        }
                        else if (column.FilterCriteria.FilterType == XlFilterType.Icon)
                        {
                            this.GenerateIconFilter(column.FilterCriteria as XlIconFilter);
                        }
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateTableColumnsContent(XlTable table)
        {
            this.WriteShStartElement("tableColumns");
            try
            {
                int count = table.InnerColumns.Count;
                this.WriteIntValue("count", count);
                for (int i = 0; i < count; i++)
                {
                    this.GenerateTableColumnContent(i, table);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateTableContent(XlTable table)
        {
            this.BeginWriteXmlContent();
            this.GenerateTableContentCore(table);
            this.AddPackageContent($"xl\tables\table{table.Id}.xml", this.EndWriteXmlContent());
            this.Builder.OverriddenContentTypes.Add($"/xl/tables/table{table.Id}.xml", "application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml");
        }

        private void GenerateTableContentCore(XlTable table)
        {
            this.WriteShStartElement("table");
            try
            {
                this.GenerateTableAttributes(table);
                this.GenerateTableAutoFilterContent(table);
                this.GenerateTableColumnsContent(table);
                this.GenerateTableStyleInfoContent(table);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateTableParts()
        {
            IList<XlTable> innerTables = this.currentSheet.InnerTables;
            if (innerTables.Count != 0)
            {
                this.ValidateTables();
                this.WriteShStartElement("tableParts");
                try
                {
                    this.WriteIntValue("count", innerTables.Count);
                    foreach (XlTable table in innerTables)
                    {
                        this.WriteTablePart(table);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateTablesContent()
        {
            foreach (XlTable table in this.currentSheet.InnerTables)
            {
                this.GenerateTableContent(table);
            }
        }

        private void GenerateTableStyleInfoContent(XlTable table)
        {
            this.WriteShStartElement("tableStyleInfo");
            try
            {
                if (!string.IsNullOrEmpty(table.Style.Name))
                {
                    this.WriteStringValue("name", table.Style.Name);
                }
                this.WriteBoolValue("showColumnStripes", table.Style.ShowColumnStripes);
                this.WriteBoolValue("showFirstColumn", table.Style.ShowFirstColumn);
                this.WriteBoolValue("showLastColumn", table.Style.ShowLastColumn);
                this.WriteBoolValue("showRowStripes", table.Style.ShowRowStripes);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateTextRunPropertiesContent(XlFont font)
        {
            if (font != null)
            {
                this.WriteShStartElement("rPr");
                try
                {
                    this.WriteElementWithValAttr("rFont", font.Name);
                    if (font.Charset != this.defaultFont.Charset)
                    {
                        this.WriteElementWithValAttr("charset", font.Charset);
                    }
                    if (font.FontFamily != this.defaultFont.FontFamily)
                    {
                        this.WriteElementWithValAttr("family", (int) font.FontFamily);
                    }
                    if (font.Bold != this.defaultFont.Bold)
                    {
                        this.WriteElementWithValAttr("b", font.Bold);
                    }
                    if (font.Italic != this.defaultFont.Italic)
                    {
                        this.WriteElementWithValAttr("i", font.Italic);
                    }
                    if (font.StrikeThrough != this.defaultFont.StrikeThrough)
                    {
                        this.WriteElementWithValAttr("strike", font.StrikeThrough);
                    }
                    if (font.Outline != this.defaultFont.Outline)
                    {
                        this.WriteElementWithValAttr("outline", font.Outline);
                    }
                    if (font.Shadow != this.defaultFont.Shadow)
                    {
                        this.WriteElementWithValAttr("shadow", font.Shadow);
                    }
                    if (font.Condense != this.defaultFont.Condense)
                    {
                        this.WriteElementWithValAttr("condense", font.Condense);
                    }
                    if (font.Extend != this.defaultFont.Extend)
                    {
                        this.WriteElementWithValAttr("extend", font.Extend);
                    }
                    if (!font.Color.IsEmpty)
                    {
                        this.WriteColor(font.Color, "color");
                    }
                    if (font.Size != this.defaultFont.Size)
                    {
                        this.WriteElementWithValAttr("sz", Math.Round(font.Size, 2).ToString(CultureInfo.InvariantCulture));
                    }
                    if (font.Underline != this.defaultFont.Underline)
                    {
                        this.WriteElementWithValAttr("u", underlineTypeTable[font.Underline]);
                    }
                    if (font.Script != this.defaultFont.Script)
                    {
                        this.WriteElementWithValAttr("vertAlign", verticalAlignmentRunTypeTable[font.Script]);
                    }
                    if (font.SchemeStyle != this.defaultFont.SchemeStyle)
                    {
                        this.WriteElementWithValAttr("scheme", fontSchemeStyleTable[font.SchemeStyle]);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateTop10Filter(XlTop10Filter filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("top10");
                try
                {
                    if (!filter.Top)
                    {
                        this.WriteBoolValue("top", false);
                    }
                    if (filter.Percent)
                    {
                        this.WriteBoolValue("percent", true);
                    }
                    this.WriteDoubleValue("val", filter.Value);
                    if (!double.IsNaN(filter.FilterValue))
                    {
                        this.WriteDoubleValue("filterVal", filter.FilterValue);
                    }
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateValuesFilter(XlValuesFilter filter)
        {
            if (filter != null)
            {
                this.WriteShStartElement("filters");
                try
                {
                    if (filter.FilterByBlank)
                    {
                        this.WriteBoolValue("blank", true);
                    }
                    if (filter.CalendarType != XlCalendarType.None)
                    {
                        this.WriteStringValue("calendarType", this.CalendarTypeTable[filter.CalendarType]);
                    }
                    this.GenerateValuesFilter(filter.Values);
                    this.GenerateValuesFilter(filter.DateGroups);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateValuesFilter(IList<XlDateGroupItem> items)
        {
            foreach (XlDateGroupItem item in items)
            {
                this.WriteShStartElement("dateGroupItem");
                try
                {
                    XlDateTimeGroupingType groupingType = item.GroupingType;
                    DateTime time = item.Value;
                    this.WriteIntValue("year", time.Year);
                    if (groupingType >= XlDateTimeGroupingType.Month)
                    {
                        this.WriteIntValue("month", time.Month);
                    }
                    if (groupingType >= XlDateTimeGroupingType.Day)
                    {
                        this.WriteIntValue("day", time.Day);
                    }
                    if (groupingType >= XlDateTimeGroupingType.Hour)
                    {
                        this.WriteIntValue("hour", time.Hour);
                    }
                    if (groupingType >= XlDateTimeGroupingType.Minute)
                    {
                        this.WriteIntValue("minute", time.Minute);
                    }
                    if (groupingType == XlDateTimeGroupingType.Second)
                    {
                        this.WriteIntValue("second", time.Second);
                    }
                    this.WriteStringValue("dateTimeGrouping", this.DateTimeGroupingTable[groupingType]);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateValuesFilter(IList<string> items)
        {
            foreach (string str in items)
            {
                this.WriteShStartElement("filter");
                try
                {
                    this.WriteStringValue("val", str);
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private void GenerateWorkbookProperties()
        {
            this.WriteShStartElement("workbookPr");
            try
            {
                this.WriteBoolValue("date1904", false);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateWorkbookView()
        {
            this.WriteShStartElement("workbookView");
            try
            {
                XlDocumentView view = this.currentDocument.View;
                if (!view.ShowHorizontalScrollBar)
                {
                    this.WriteBoolValue("showHorizontalScroll", view.ShowHorizontalScrollBar);
                }
                if (!view.ShowVerticalScrollBar)
                {
                    this.WriteBoolValue("showVerticalScroll", view.ShowVerticalScrollBar);
                }
                if (!view.ShowSheetTabs)
                {
                    this.WriteBoolValue("showSheetTabs", view.ShowSheetTabs);
                }
                if (view.TabRatio != 60)
                {
                    this.WriteShIntValue("tabRatio", view.TabRatio * 10);
                }
                if (view.HasWindowBounds())
                {
                    this.WriteShIntValue("xWindow", view.WindowX);
                    this.WriteShIntValue("yWindow", view.WindowY);
                    this.WriteShIntValue("windowWidth", view.WindowWidth);
                    this.WriteShIntValue("windowHeight", view.WindowHeight);
                }
                if (view.FirstVisibleTabIndex > 0)
                {
                    this.WriteShIntValue("firstSheet", view.FirstVisibleTabIndex);
                }
                if (view.SelectedTabIndex > 0)
                {
                    this.WriteShIntValue("activeTab", view.SelectedTabIndex);
                }
                if (!view.GroupDatesInAutoFilterMenu)
                {
                    this.WriteBoolValue("autoFilterDateGrouping", view.GroupDatesInAutoFilterMenu);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void GenerateWorkbookViews()
        {
            this.currentDocument.View.Validate(this.sheets);
            if (!this.currentDocument.View.IsDefault())
            {
                this.WriteShStartElement("bookViews");
                try
                {
                    this.GenerateWorkbookView();
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
        }

        private XlViewPaneType GetActivePane(bool hasRightPane, bool hasBottomPane) => 
            !(hasRightPane & hasBottomPane) ? (!hasBottomPane ? (!hasRightPane ? XlViewPaneType.TopLeft : XlViewPaneType.TopRight) : XlViewPaneType.BottomLeft) : XlViewPaneType.BottomRight;

        private string GetCellReference(XlCell cell)
        {
            this.cellRefBuilder.Clear();
            int num = cell.ColumnIndex + 1;
            int num2 = num % 0x1a;
            num2 ??= 0x1a;
            if (num > 0x2be)
            {
                int num3 = (num - num2) % 0x2a4;
                num3 ??= 0x2a4;
                this.cellRefBuilder.Append((char) (0x40 + (((num - num3) - num2) / 0x2a4)));
                this.cellRefBuilder.Append((char) (0x40 + (num3 / 0x1a)));
            }
            else if (num > 0x1a)
            {
                this.cellRefBuilder.Append((char) (0x40 + ((num - num2) / 0x1a)));
            }
            this.cellRefBuilder.Append((char) (0x40 + num2));
            this.cellRefBuilder.Append((int) (cell.RowIndex + 1));
            return this.cellRefBuilder.ToString();
        }

        private string GetFormulaString(XlCell cell, bool isSharedFormula)
        {
            if (cell.Formula != null)
            {
                string str = cell.Formula.ToString(this.CurrentCulture);
                if (str.Length <= 0x2000)
                {
                    return str;
                }
            }
            else if (cell.Expression != null)
            {
                this.expressionContext.CurrentCell = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
                this.expressionContext.ReferenceMode = isSharedFormula ? XlCellReferenceMode.Offset : XlCellReferenceMode.Reference;
                this.expressionContext.ExpressionStyle = isSharedFormula ? XlExpressionStyle.Shared : XlExpressionStyle.Normal;
                string str2 = cell.Expression.ToString(this.expressionContext);
                if (str2.Length <= 0x2000)
                {
                    return str2;
                }
            }
            else if (!string.IsNullOrEmpty(cell.FormulaString))
            {
                string formulaString = cell.FormulaString;
                if (this.formulaParser != null)
                {
                    this.expressionContext.CurrentCell = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
                    this.expressionContext.ReferenceMode = isSharedFormula ? XlCellReferenceMode.Offset : XlCellReferenceMode.Reference;
                    this.expressionContext.ExpressionStyle = isSharedFormula ? XlExpressionStyle.Shared : XlExpressionStyle.Normal;
                    XlExpression expression = this.formulaParser.Parse(formulaString, this.expressionContext);
                    if ((expression == null) || (expression.Count == 0))
                    {
                        throw new InvalidOperationException($"Can't parse formula '{cell.FormulaString}'.");
                    }
                }
                if (formulaString.Length <= 0x2000)
                {
                    return formulaString;
                }
            }
            return "#VALUE!";
        }

        private string GetHyperlinkRelationId(IXlHyperlinkOwner drawingObject)
        {
            string str2;
            string targetUri = drawingObject.HyperlinkClick.TargetUri;
            if (!this.exportedPictureHyperlinkTable.TryGetValue(targetUri, out str2))
            {
                str2 = this.drawingRelations.GenerateId();
                this.exportedPictureHyperlinkTable.Add(targetUri, str2);
                OpenXmlRelation item = new OpenXmlRelation {
                    Id = str2,
                    Target = this.EscapeHyperlinkUri(targetUri),
                    Type = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink"
                };
                if (drawingObject.HyperlinkClick.IsExternal)
                {
                    item.TargetMode = "External";
                }
                this.drawingRelations.Add(item);
            }
            return str2;
        }

        private string GetImageContentType(ImageFormat format)
        {
            string str;
            return (!imageContentTypeTable.TryGetValue(format, out str) ? "image/png" : str);
        }

        private string GetImageExtension(ImageFormat format)
        {
            string str;
            return (!imageExtenstionTable.TryGetValue(format, out str) ? "png" : str);
        }

        private XlColor GetNegativeFillColor(XlCondFmtRuleDataBar rule)
        {
            XlColor negativeFillColor = rule.NegativeFillColor;
            if (negativeFillColor.IsAutoOrEmpty)
            {
                negativeFillColor = DXColor.Red;
            }
            return negativeFillColor;
        }

        private string GetSqRef(IList<XlCellRange> ranges)
        {
            StringBuilder builder = new StringBuilder();
            foreach (XlCellRange range in ranges)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" ");
                }
                builder.Append(range.ToString(true));
            }
            return builder.ToString();
        }

        private string GetSqrefString(XlDataValidation validation)
        {
            StringBuilder builder = new StringBuilder();
            foreach (XlCellRange range in validation.Ranges)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" ");
                }
                builder.Append(range.ToString());
            }
            return builder.ToString();
        }

        private string GetTimePeriodFormula(XlCondFmtTimePeriod timePeriod, XlCellPosition topLeft)
        {
            switch (timePeriod)
            {
                case XlCondFmtTimePeriod.Last7Days:
                    return string.Format("AND(TODAY()-FLOOR({0},1)<=6,FLOOR({0},1)<=TODAY())", topLeft.ToString());

                case XlCondFmtTimePeriod.LastMonth:
                    return string.Format("AND(MONTH({0})=MONTH(EDATE(TODAY(),0-1)),YEAR({0})=YEAR(EDATE(TODAY(),0-1)))", topLeft.ToString());

                case XlCondFmtTimePeriod.LastWeek:
                    return string.Format("AND(TODAY()-ROUNDDOWN({0},0)>=(WEEKDAY(TODAY())),TODAY()-ROUNDDOWN({0},0)<(WEEKDAY(TODAY())+7))", topLeft.ToString());

                case XlCondFmtTimePeriod.NextMonth:
                    return string.Format("AND(MONTH({0})=MONTH(EDATE(TODAY(),0+1)),YEAR({0})=YEAR(EDATE(TODAY(),0+1)))", topLeft.ToString());

                case XlCondFmtTimePeriod.NextWeek:
                    return string.Format("AND(ROUNDDOWN({0},0)-TODAY()>(7-WEEKDAY(TODAY())),ROUNDDOWN({0},0)-TODAY()<(15-WEEKDAY(TODAY())))", topLeft.ToString());

                case XlCondFmtTimePeriod.ThisMonth:
                    return string.Format("AND(MONTH({0})=MONTH(TODAY()),YEAR({0})=YEAR(TODAY()))", topLeft.ToString());

                case XlCondFmtTimePeriod.ThisWeek:
                    return string.Format("AND(TODAY()-ROUNDDOWN({0},0)<=WEEKDAY(TODAY())-1,ROUNDDOWN({0},0)-TODAY()<=7-WEEKDAY(TODAY()))", topLeft.ToString());

                case XlCondFmtTimePeriod.Today:
                    return $"FLOOR({topLeft.ToString()},1)=TODAY()";

                case XlCondFmtTimePeriod.Tomorrow:
                    return $"FLOOR({topLeft.ToString()},1)=TODAY()+1";
            }
            return $"FLOOR({topLeft.ToString()},1)=TODAY()-1";
        }

        private XlCellPosition GetTopLeftCell(IList<XlCellRange> ranges)
        {
            if (ranges.Count == 0)
            {
                return XlCellPosition.InvalidValue;
            }
            XlCellRange range = ranges[0];
            int column = range.TopLeft.Column;
            int row = range.TopLeft.Row;
            for (int i = 1; i < ranges.Count; i++)
            {
                range = ranges[i];
                column = Math.Min(column, range.TopLeft.Column);
                XlCellPosition topLeft = range.TopLeft;
                row = Math.Min(row, topLeft.Row);
            }
            return new XlCellPosition(column, row);
        }

        private string GetValueObjectString(XlValueObject valueObject, XlCellPosition topLeft)
        {
            if (valueObject.IsExpression)
            {
                this.expressionContext.CurrentCell = topLeft;
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                return valueObject.Expression.ToString(this.expressionContext);
            }
            if (!valueObject.IsFormula)
            {
                return valueObject.ToString();
            }
            string formula = valueObject.Formula.Remove(0, 1);
            if (this.formulaParser != null)
            {
                this.expressionContext.CurrentCell = topLeft;
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                XlExpression expression = this.formulaParser.Parse(formula, this.expressionContext);
                if ((expression == null) || (expression.Count == 0))
                {
                    return "#VALUE!";
                }
            }
            return formula;
        }

        private void InitAlignmentTable()
        {
            this.alignmentTable.Clear();
            this.alignmentList.Clear();
            this.defaultAlignment = new XlCellAlignment();
            this.alignmentTable.Add(this.defaultAlignment, 0);
            this.alignmentList.Add(this.defaultAlignment);
        }

        private void InitBorderTable()
        {
            this.bordersTable.Clear();
            this.defaultBorder = new XlBorder();
            this.bordersTable.Add(this.defaultBorder, 0);
        }

        private void InitFillTable()
        {
            this.fillsTable.Clear();
            this.fillsTable.Add(XlFill.NoFill(), 0);
            this.fillsTable.Add(XlFill.PatternFill(XlPatternType.Gray125), 1);
        }

        private void InitFontTable()
        {
            this.fontsTable.Clear();
            this.defaultFont = XlFont.BodyFont();
            this.fontsTable.Add(this.defaultFont, 0);
        }

        protected internal virtual void InitializeExport()
        {
            this.streamInfoStack.Clear();
            this.sheets.Clear();
            this.sheetInfos.Clear();
            this.sharedStringTable.Clear();
            this.pendingColumns.Clear();
            this.columns.Clear();
            this.groups.Clear();
            this.imageFiles.Clear();
            this.exportedPictureHyperlinkTable.Clear();
            this.InitializeStyles();
            this.sheetIndex = 0;
            this.rowIndex = -1;
            this.columnIndex = -1;
            this.Builder.UsedContentTypes.Add("rels", "application/vnd.openxmlformats-package.relationships+xml");
            this.Builder.UsedContentTypes.Add("xml", "application/xml");
            this.shapeId = 0;
            this.drawingId = 0;
            this.imageId = 0;
            this.tableId = 0;
            this.sheetRelations.Clear();
        }

        private void InitializeStyles()
        {
            this.InitFontTable();
            this.InitAlignmentTable();
            this.InitFillTable();
            this.InitBorderTable();
            this.InitNumberFormatTable();
            this.cellXfTable.Clear();
            XlCellXf key = new XlCellXf();
            this.cellXfTable.Add(key, 0);
        }

        private void InitNumberFormatTable()
        {
            this.numberFormatsTable.Clear();
            this.netNumberFormatsTable.Clear();
            this.customNumberFormatId = 0xa4;
        }

        private bool IsNeedWriteXmlSpaceAttr(string text) => 
            !string.IsNullOrEmpty(text) ? (this.IsSpace(text[0]) || (this.IsSpace(text[text.Length - 1]) || text.Contains("  "))) : false;

        private bool IsSpace(char ch) => 
            (ch == ' ') || ((ch == ' ') || ((ch == ' ') || ((ch == ' ') || ((ch == '\n') || (ch == '\r')))));

        private bool IsUniqueSheetName()
        {
            bool flag;
            using (List<IXlSheet>.Enumerator enumerator = this.sheets.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IXlSheet current = enumerator.Current;
                        if (!string.Equals(this.currentSheet.Name, current.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private long PixelsToEMU(int value) => 
            (long) ((value * 914400.0) / ((double) this.DpiY));

        private int RegisterAlignment(XlCellAlignment alignment)
        {
            int count;
            if (alignment == null)
            {
                return -1;
            }
            if (!this.alignmentTable.TryGetValue(alignment, out count))
            {
                count = this.alignmentTable.Count;
                this.alignmentTable.Add(alignment, count);
                this.alignmentList.Add(alignment);
            }
            return count;
        }

        private int RegisterBorder(XlBorder border)
        {
            int count;
            if (border == null)
            {
                return -1;
            }
            if (!this.bordersTable.TryGetValue(border, out count))
            {
                count = this.bordersTable.Count;
                this.bordersTable.Add(border, count);
            }
            return count;
        }

        private int RegisterCondFmtDifferentialFormatting(XlDifferentialFormatting formatting)
        {
            int count;
            if (formatting == null)
            {
                return -1;
            }
            XlDxf key = new XlDxf();
            if (formatting.Font != null)
            {
                key.Font = formatting.Font.Clone();
                key.Font.Name = null;
                key.Font.Size = 0.0;
                key.Font.SchemeStyle = XlFontSchemeStyles.None;
            }
            key.Fill = formatting.Fill;
            key.Alignment = formatting.Alignment;
            key.Border = formatting.Border;
            key.NumberFormat = this.CreateNumberFormat(formatting.NetFormatString, formatting.IsDateTimeFormatString, formatting.NumberFormat);
            if (key.IsEmpty)
            {
                return -1;
            }
            if (!this.dxfTable.TryGetValue(key, out count))
            {
                count = this.dxfTable.Count;
                this.dxfTable.Add(key, count);
            }
            return count;
        }

        private int RegisterDifferentialFormatting(XlDifferentialFormatting formatting)
        {
            int count;
            if (formatting == null)
            {
                return -1;
            }
            XlDxf key = new XlDxf {
                Font = formatting.Font,
                Fill = formatting.Fill,
                Alignment = formatting.Alignment,
                Border = formatting.Border,
                NumberFormat = this.CreateNumberFormat(formatting.NetFormatString, formatting.IsDateTimeFormatString, formatting.NumberFormat)
            };
            if (key.IsEmpty)
            {
                return -1;
            }
            if (!this.dxfTable.TryGetValue(key, out count))
            {
                count = this.dxfTable.Count;
                this.dxfTable.Add(key, count);
            }
            return count;
        }

        private int RegisterFill(XlFill fill)
        {
            int count;
            if (fill == null)
            {
                return -1;
            }
            if (!this.fillsTable.TryGetValue(fill, out count))
            {
                count = this.fillsTable.Count;
                this.fillsTable.Add(fill, count);
            }
            return count;
        }

        private int RegisterFont(XlFont font)
        {
            int count;
            if (font == null)
            {
                return -1;
            }
            if (!this.fontsTable.TryGetValue(font, out count))
            {
                count = this.fontsTable.Count;
                this.fontsTable.Add(font, count);
            }
            return count;
        }

        private int RegisterFormatting(XlCellFormatting formatting)
        {
            int count;
            if (formatting == null)
            {
                return -1;
            }
            int num = this.RegisterFont(formatting.Font);
            int num2 = this.RegisterFill(formatting.Fill);
            int num4 = this.RegisterNumberFormat(formatting.NetFormatString, formatting.IsDateTimeFormatString, formatting.NumberFormat);
            int num5 = this.RegisterAlignment(formatting.Alignment);
            XlCellXf key = new XlCellXf {
                FontId = Math.Max(0, num),
                FillId = Math.Max(0, num2),
                NumberFormatId = Math.Max(0, num4),
                BorderId = Math.Max(0, this.RegisterBorder(formatting.Border)),
                AlignmentId = Math.Max(0, num5),
                ApplyFont = num >= 0,
                ApplyFill = num2 >= 0,
                ApplyNumberFormat = num4 >= 0,
                ApplyAlignment = num5 >= 0
            };
            if (!this.cellXfTable.TryGetValue(key, out count))
            {
                count = this.cellXfTable.Count;
                this.cellXfTable.Add(key, count);
            }
            return count;
        }

        private int RegisterNumberFormat(string netFormatString, bool isDateTimeFormatString, XlNumberFormat xlNumberFormat)
        {
            ExcelNumberFormat format;
            if (string.IsNullOrEmpty(netFormatString))
            {
                if (xlNumberFormat == null)
                {
                    return -1;
                }
                if (xlNumberFormat.FormatId >= 0)
                {
                    return xlNumberFormat.FormatId;
                }
                string formatCode = xlNumberFormat.FormatCode;
                if (this.numberFormatsTable.ContainsKey(formatCode))
                {
                    format = this.numberFormatsTable[formatCode];
                }
                else
                {
                    format = new ExcelNumberFormat(this.customNumberFormatId, formatCode);
                    this.numberFormatsTable.Add(formatCode, format);
                    this.customNumberFormatId++;
                }
                return format.Id;
            }
            XlNetNumberFormat format1 = new XlNetNumberFormat();
            format1.FormatString = netFormatString;
            format1.IsDateTimeFormat = isDateTimeFormatString;
            XlNetNumberFormat key = format1;
            if (!this.netNumberFormatsTable.TryGetValue(key, out format))
            {
                format = this.ConvertNetFormatStringToXlFormatCode(netFormatString, isDateTimeFormatString);
                if (format == null)
                {
                    return -1;
                }
                if (string.IsNullOrEmpty(format.FormatString))
                {
                    return 0;
                }
                if (format.Id >= 0)
                {
                    return format.Id;
                }
                if (this.numberFormatsTable.ContainsKey(format.FormatString))
                {
                    format = this.numberFormatsTable[format.FormatString];
                }
                else
                {
                    format.Id = this.customNumberFormatId;
                    this.numberFormatsTable.Add(format.FormatString, format);
                    this.customNumberFormatId++;
                }
                this.netNumberFormatsTable.Add(key, format);
            }
            return format.Id;
        }

        public void SetWorksheetPosition(string name, int position)
        {
            Guard.ArgumentIsInRange<IXlSheet>(this.sheets, position, "position");
            int index = this.sheets.FindIndex(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
            {
                throw new ArgumentException("Can't find worksheet with specified name!");
            }
            IXlSheet item = this.sheets[index];
            this.sheets.RemoveAt(index);
            this.sheets.Insert(position, item);
        }

        private bool ShouldExportCondFmtExt(XlConditionalFormatting conditionalFormatting)
        {
            if (conditionalFormatting.Ranges.Count > 0)
            {
                using (IEnumerator<XlCondFmtRule> enumerator = conditionalFormatting.Rules.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        XlCondFmtRule current = enumerator.Current;
                        if ((current.RuleType == XlCondFmtType.DataBar) || (current.RuleType == XlCondFmtType.IconSet))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ShouldExportCondFmtExtList(IList<XlConditionalFormatting> conditionalFormattings)
        {
            bool flag;
            using (IEnumerator<XlConditionalFormatting> enumerator = conditionalFormattings.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlConditionalFormatting current = enumerator.Current;
                        if (!this.ShouldExportCondFmtExt(current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool ShouldExportConditionalFormatting(XlConditionalFormatting conditionalFormatting)
        {
            bool flag;
            if (conditionalFormatting.Ranges.Count == 0)
            {
                return false;
            }
            using (IEnumerator<XlCondFmtRule> enumerator = conditionalFormatting.Rules.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlCondFmtRule current = enumerator.Current;
                        if (current.RuleType == XlCondFmtType.IconSet)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool ShouldExportDataValidationsExt(IList<XlDataValidation> validations) => 
            this.CountItems(validations, true) > 0;

        private bool ShouldExportDocumentApplicationProperties() => 
            !string.IsNullOrEmpty(this.documentProperties.Application) || (!string.IsNullOrEmpty(this.documentProperties.Company) || (!string.IsNullOrEmpty(this.documentProperties.Manager) || (!string.IsNullOrEmpty(this.documentProperties.Version) || (this.documentProperties.Security != XlDocumentSecurity.None))));

        private bool ShouldExportDocumentCoreProperties() => 
            true;

        private bool ShouldExportDocumentCustomProperties() => 
            this.documentProperties.Custom.Count > 0;

        private bool ShouldExportHyperlinks(IList<XlHyperlink> hyperlinks)
        {
            bool flag;
            if ((hyperlinks == null) || (hyperlinks.Count == 0))
            {
                return false;
            }
            using (IEnumerator<XlHyperlink> enumerator = hyperlinks.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlHyperlink current = enumerator.Current;
                        if ((current.Reference == null) || string.IsNullOrEmpty(current.TargetUri))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal virtual bool ShouldExportSharedStrings() => 
            this.sharedStringTable.Count != 0;

        private bool ShouldExportSheetProperties(IXlSheet sheet) => 
            !sheet.OutlineProperties.SummaryBelow || (!sheet.OutlineProperties.SummaryRight || ((sheet.PageSetup != null) && sheet.PageSetup.FitToPage));

        private bool ShouldExportSparklineGroupsExt(IList<XlSparklineGroup> sparklineGroups)
        {
            bool flag;
            using (IEnumerator<XlSparklineGroup> enumerator = sparklineGroups.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlSparklineGroup current = enumerator.Current;
                        if (current.Sparklines.Count <= 0)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal virtual bool ShouldGenerateSheetViewPane(IXlSheet sheet) => 
            !sheet.SplitPosition.EqualsPosition(XlCellPosition.TopLeft);

        private bool ShouldWriteCalculationProperties() => 
            this.CalculationOptions.FullCalculationOnLoad;

        private bool ShouldWriteDefinedNames()
        {
            bool flag;
            using (List<IXlSheet>.Enumerator enumerator = this.sheets.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IXlSheet current = enumerator.Current;
                        if ((current.AutoFilterRange == null) && (!current.PrintTitles.IsValid() && (current.PrintArea == null)))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void SkipCells(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.insideCellScope)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginCell/EndCell scope.");
            }
            if ((this.columnIndex + count) >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Cell column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            int num = this.columnIndex + count;
            XlCellRange range = XlCellRange.FromLTRB(this.columnIndex, this.CurrentRowIndex, num - 1, this.CurrentRowIndex);
            this.CreateTableCells(range);
            this.columnIndex = num;
        }

        public void SkipColumns(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentColumn != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginColumn/EndColumn scope.");
            }
            int num = this.columnIndex + count;
            if (num >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            if (this.CurrentOutlineLevel <= 0)
            {
                this.columnIndex = num;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    this.BeginColumn();
                    this.EndColumn();
                }
            }
        }

        public void SkipRows(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentRow != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginRow/EndRow scope.");
            }
            int num = this.rowIndex + count;
            if (num >= this.options.MaxRowCount)
            {
                throw new ArgumentOutOfRangeException($"Row index goes beyond range 0..{this.options.MaxRowCount - 1}.");
            }
            if ((this.CurrentOutlineLevel <= 0) && !this.currentSheet.HasActiveTables)
            {
                this.rowIndex = num;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    this.BeginRow();
                    this.EndRow();
                }
            }
        }

        private void ValidateTables()
        {
            IList<XlTable> innerTables = this.currentSheet.InnerTables;
            for (int i = 0; i < innerTables.Count; i++)
            {
                XlTable table = innerTables[i];
                this.tableId++;
                table.Id = this.tableId;
            }
        }

        private void WriteAbsoluteAnchor(XlDrawingObjectBase drawingObject, Action<XlDrawingObjectBase> action)
        {
            this.WriteStartElement("absoluteAnchor", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteShapePosition(drawingObject);
                this.WriteShapeExtent(drawingObject);
                action(drawingObject);
                this.WriteClientData(drawingObject);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteAlignment(XlCellAlignment info)
        {
            this.WriteShStartElement("alignment");
            try
            {
                if (this.defaultAlignment.HorizontalAlignment != info.HorizontalAlignment)
                {
                    this.WriteStringValue("horizontal", horizontalAlignmentTable[info.HorizontalAlignment]);
                }
                if (this.defaultAlignment.VerticalAlignment != info.VerticalAlignment)
                {
                    this.WriteStringValue("vertical", verticalAlignmentTable[info.VerticalAlignment]);
                }
                if (this.defaultAlignment.TextRotation != info.TextRotation)
                {
                    this.WriteIntValue("textRotation", info.TextRotation);
                }
                if (this.defaultAlignment.WrapText != info.WrapText)
                {
                    this.WriteBoolValue("wrapText", info.WrapText);
                }
                if (this.defaultAlignment.Indent != info.Indent)
                {
                    this.WriteIntValue("indent", info.Indent);
                }
                if (this.defaultAlignment.RelativeIndent != info.RelativeIndent)
                {
                    this.WriteIntValue("relativeIndent", info.RelativeIndent);
                }
                if (this.defaultAlignment.JustifyLastLine != info.JustifyLastLine)
                {
                    this.WriteBoolValue("justifyLastLine", info.JustifyLastLine);
                }
                if (this.defaultAlignment.ShrinkToFit != info.ShrinkToFit)
                {
                    this.WriteBoolValue("shrinkToFit", info.ShrinkToFit);
                }
                if (this.defaultAlignment.ReadingOrder != info.ReadingOrder)
                {
                    this.WriteStringValue("readingOrder", readingOrderTable[info.ReadingOrder]);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteAlignment(XlCellXf xf)
        {
            if (xf.AlignmentId > 0)
            {
                XlCellAlignment info = this.alignmentList[xf.AlignmentId];
                this.WriteAlignment(info);
            }
        }

        private void WriteAnchorPoint(string tag, XlAnchorPoint point, bool singleCellAnchor)
        {
            this.WriteStartElement(tag, "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                int num = singleCellAnchor ? (point.Column + 1) : point.Column;
                int num2 = singleCellAnchor ? (point.Row + 1) : point.Row;
                long num3 = this.PixelsToEMU(point.ColumnOffsetInPixels);
                long num4 = this.PixelsToEMU(point.RowOffsetInPixels);
                this.WriteAnchorTag("col", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", num.ToString());
                this.WriteAnchorTag("colOff", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", num3.ToString());
                this.WriteAnchorTag("row", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", num2.ToString());
                this.WriteAnchorTag("rowOff", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", num4.ToString());
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteAnchorTag(string tag, string ns, string value)
        {
            this.WriteStartElement(tag, ns);
            try
            {
                this.WriteShString(value);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteApplyValue(string nameAttribute, bool value)
        {
            if (value)
            {
                this.WriteBoolValue(nameAttribute, value);
            }
        }

        private void WriteBlipFill(XlsxPicture drawingObject)
        {
            this.WriteStartElement("blipFill", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteBoolValue("rotWithShape", true);
                this.WriteStartElement("blip", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteStringAttr("xmlns", "r", null, "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                    this.WriteStringAttr("r", "embed", "http://schemas.openxmlformats.org/officeDocument/2006/relationships", drawingObject.RelationId);
                }
                finally
                {
                    this.WriteEndElement();
                }
                this.WriteSourceRectangle(drawingObject);
                this.WriteStretch();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteBodyShape(XlDrawingObjectBase drawingObject)
        {
            XlShape shape = (XlShape) drawingObject;
            this.WriteStartElement("sp", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringValue("macro", string.Empty);
                this.WriteStringValue("textlink", string.Empty);
                this.WriteShapeNonVisualProperties(shape);
                this.WriteShapeProperties(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteBoolFontValue(string tag, bool value)
        {
            this.WriteShStartElement(tag);
            try
            {
                if (!value)
                {
                    this.WriteBoolValue("val", value);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        protected internal void WriteBoolValue(string tag, bool value)
        {
            this.WriteStringAttr(null, tag, null, this.ConvertBoolToString(value));
        }

        protected internal void WriteBoolValue(string attr, bool value, bool defaultValue)
        {
            if (value != defaultValue)
            {
                this.WriteBoolValue(attr, value);
            }
        }

        private void WriteBorder(XlBorderLineStyle lineStyle, XlColor color, string tag)
        {
            this.WriteShStartElement(tag);
            try
            {
                if (lineStyle != XlBorderLineStyle.None)
                {
                    this.WriteStringValue("style", borderLineStylesTable[lineStyle]);
                    this.WriteColor(color, "color");
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteBorders(XlBorder border)
        {
            XlBorder defaultBorder = this.defaultBorder;
            this.WriteShStartElement("border");
            try
            {
                if (defaultBorder.DiagonalUp != border.DiagonalUp)
                {
                    this.WriteBoolValue("diagonalUp", border.DiagonalUp);
                }
                if (defaultBorder.DiagonalDown != border.DiagonalDown)
                {
                    this.WriteBoolValue("diagonalDown", border.DiagonalDown);
                }
                if (defaultBorder.Outline != border.Outline)
                {
                    this.WriteBoolValue("outline", border.Outline);
                }
                if ((defaultBorder.LeftLineStyle == border.LeftLineStyle) && ReferenceEquals(defaultBorder.LeftColor, border.LeftColor))
                {
                    this.WriteEmptyBorder("left");
                }
                else
                {
                    this.WriteBorder(border.LeftLineStyle, border.LeftColor, "left");
                }
                if ((defaultBorder.RightLineStyle == border.RightLineStyle) && ReferenceEquals(defaultBorder.RightColor, border.RightColor))
                {
                    this.WriteEmptyBorder("right");
                }
                else
                {
                    this.WriteBorder(border.RightLineStyle, border.RightColor, "right");
                }
                if ((defaultBorder.TopLineStyle == border.TopLineStyle) && ReferenceEquals(defaultBorder.TopColor, border.TopColor))
                {
                    this.WriteEmptyBorder("top");
                }
                else
                {
                    this.WriteBorder(border.TopLineStyle, border.TopColor, "top");
                }
                if ((defaultBorder.BottomLineStyle == border.BottomLineStyle) && ReferenceEquals(defaultBorder.BottomColor, border.BottomColor))
                {
                    this.WriteEmptyBorder("bottom");
                }
                else
                {
                    this.WriteBorder(border.BottomLineStyle, border.BottomColor, "bottom");
                }
                if ((defaultBorder.DiagonalLineStyle == border.DiagonalLineStyle) && ReferenceEquals(defaultBorder.DiagonalColor, border.DiagonalColor))
                {
                    this.WriteEmptyBorder("diagonal");
                }
                else
                {
                    this.WriteBorder(border.DiagonalLineStyle, border.DiagonalColor, "diagonal");
                }
                if ((defaultBorder.VerticalLineStyle != border.VerticalLineStyle) || !ReferenceEquals(defaultBorder.VerticalColor, border.VerticalColor))
                {
                    this.WriteBorder(border.VerticalLineStyle, border.VerticalColor, "vertical");
                }
                if ((defaultBorder.HorizontalLineStyle != border.HorizontalLineStyle) || !ReferenceEquals(defaultBorder.HorizontalColor, border.HorizontalColor))
                {
                    this.WriteBorder(border.HorizontalLineStyle, border.HorizontalColor, "horizontal");
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteCellFormat(XlCellXf xf)
        {
            this.WriteShStartElement("xf");
            try
            {
                this.WriteIntValue("numFmtId", xf.NumberFormatId);
                this.WriteIntValue("fontId", xf.FontId);
                this.WriteIntValue("fillId", xf.FillId);
                this.WriteIntValue("borderId", xf.BorderId);
                this.WriteIntValue("xfId", xf.XfId);
                this.WriteApplyValue("applyNumberFormat", xf.ApplyNumberFormat);
                this.WriteApplyValue("applyFont", xf.ApplyFont);
                this.WriteApplyValue("applyFill", xf.ApplyFill);
                this.WriteApplyValue("applyBorder", xf.ApplyBorder);
                this.WriteApplyValue("applyAlignment", xf.ApplyAlignment);
                this.WriteApplyValue("applyProtection", xf.ApplyProtection);
                this.WriteAlignment(xf);
                if (xf.QuotePrefix)
                {
                    this.WriteBoolValue("quotePrefix", true);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteCfvo(XlCondFmtValueObject value, XlCellPosition topLeft)
        {
            this.WriteShStartElement("cfvo");
            try
            {
                XlCondFmtValueObjectType objectType = value.ObjectType;
                if (objectType == XlCondFmtValueObjectType.AutoMin)
                {
                    objectType = XlCondFmtValueObjectType.Min;
                }
                if (objectType == XlCondFmtValueObjectType.AutoMax)
                {
                    objectType = XlCondFmtValueObjectType.Max;
                }
                this.WriteStringValue("type", conditionalFormattingValueTypeTable[objectType]);
                if ((objectType != XlCondFmtValueObjectType.Min) && (objectType != XlCondFmtValueObjectType.Max))
                {
                    XlValueObject valueObject = value.Value;
                    if (!valueObject.IsEmpty)
                    {
                        this.WriteStringValue("val", this.EncodeXmlChars(this.GetValueObjectString(valueObject, topLeft)));
                    }
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteCfvoExt(XlCondFmtValueObject value, XlCellPosition topLeft)
        {
            this.WriteCfvoExt(value, topLeft, false);
        }

        private void WriteCfvoExt(XlCondFmtValueObject value, XlCellPosition topLeft, bool isIconSet)
        {
            this.WriteStartElement("x14", "cfvo", null);
            try
            {
                XlCondFmtValueObjectType objectType = value.ObjectType;
                this.WriteStringValue("type", conditionalFormattingValueTypeTable[objectType]);
                if (!value.GreaterThanOrEqual & isIconSet)
                {
                    this.WriteBoolValue("gte", false);
                }
                if (((objectType != XlCondFmtValueObjectType.Max) && ((objectType != XlCondFmtValueObjectType.Min) && (objectType != XlCondFmtValueObjectType.AutoMax))) && (objectType != XlCondFmtValueObjectType.AutoMin))
                {
                    XlValueObject valueObject = value.Value;
                    if (!valueObject.IsEmpty)
                    {
                        this.WriteCfvoFormula(this.EncodeXmlChars(this.GetValueObjectString(valueObject, topLeft)));
                    }
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCfvoFormula(string formula)
        {
            this.WriteStartElement("xm", "f", null);
            try
            {
                this.WriteShString(this.EncodeXmlChars(formula));
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCharset(int charset)
        {
            this.WriteShStartElement("charset");
            try
            {
                this.WriteIntValue("val", charset);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteClientData(XlDrawingObjectBase drawingObject)
        {
            this.WriteStartElement("clientData", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteColor(XlColor info, string shStartElement)
        {
            this.WriteColor(info, shStartElement, "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
        }

        private void WriteColor(XlColor color, string shStartElement, string ns)
        {
            if (!color.IsEmpty)
            {
                this.WriteStartElement(shStartElement, ns);
                try
                {
                    if (color.ColorType == XlColorType.Auto)
                    {
                        this.WriteBoolValue("auto", true);
                    }
                    else if (color.ColorType == XlColorType.Indexed)
                    {
                        this.WriteIntValue("indexed", color.ColorIndex);
                    }
                    else if (color.ColorType == XlColorType.Rgb)
                    {
                        this.WriteStringValue("rgb", this.ConvertARGBColorToString(color.Rgb));
                    }
                    else if (color.ColorType == XlColorType.Theme)
                    {
                        this.WriteIntValue("theme", (int) color.ThemeColor);
                        if (color.Tint != 0.0)
                        {
                            this.WriteStringValue("tint", color.Tint.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        }
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteCondFmtDataBarExt(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleDataBar rule)
        {
            this.WriteStringValue("type", "dataBar");
            this.WriteStartElement("dataBar", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                if (rule.MinLength != 10)
                {
                    this.WriteIntValue("minLength", rule.MinLength);
                }
                if (rule.MaxLength != 90)
                {
                    this.WriteIntValue("maxLength", rule.MaxLength);
                }
                if (!rule.GradientFill)
                {
                    this.WriteBoolValue("gradient", false);
                }
                bool flag = !rule.BorderColor.IsEmpty;
                bool flag2 = rule.FillColor.Equals(rule.NegativeFillColor);
                bool flag3 = rule.BorderColor.Equals(rule.NegativeBorderColor);
                if (flag2)
                {
                    this.WriteBoolValue("negativeBarColorSameAsPositive", true);
                }
                if (flag)
                {
                    this.WriteBoolValue("border", true);
                    if (!flag3)
                    {
                        this.WriteBoolValue("negativeBarBorderColorSameAsPositive", false);
                    }
                }
                if (rule.AxisPosition == XlCondFmtAxisPosition.None)
                {
                    this.WriteStringValue("axisPosition", "none");
                }
                else if (rule.AxisPosition == XlCondFmtAxisPosition.Midpoint)
                {
                    this.WriteStringValue("axisPosition", "middle");
                }
                if (rule.Direction == XlDataBarDirection.LeftToRight)
                {
                    this.WriteStringValue("direction", "leftToRight");
                }
                else if (rule.Direction == XlDataBarDirection.RightToLeft)
                {
                    this.WriteStringValue("direction", "rightToLeft");
                }
                XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
                this.WriteCfvoExt(rule.MinValue, topLeftCell);
                this.WriteCfvoExt(rule.MaxValue, topLeftCell);
                if (flag)
                {
                    this.WriteColor(rule.BorderColor, "borderColor", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                }
                if (!flag2)
                {
                    this.WriteColor(this.GetNegativeFillColor(rule), "negativeFillColor", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                }
                if (flag && !flag3)
                {
                    XlColor negativeBorderColor = rule.NegativeBorderColor;
                    if (negativeBorderColor.IsEmpty)
                    {
                        negativeBorderColor = flag2 ? rule.BorderColor : this.GetNegativeFillColor(rule);
                    }
                    this.WriteColor(negativeBorderColor, "negativeBorderColor", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                }
                if (rule.AxisPosition != XlCondFmtAxisPosition.None)
                {
                    XlColor axisColor = rule.AxisColor;
                    if (axisColor.IsEmpty)
                    {
                        axisColor = DXColor.Black;
                    }
                    this.WriteColor(axisColor, "axisColor", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCondFmtIconSetExt(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleIconSet rule)
        {
            XlCellPosition topLeftCell = this.GetTopLeftCell(conditionalFormatting.Ranges);
            this.WriteStartElement("iconSet", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                if (rule.IsCustom)
                {
                    this.WriteBoolValue("custom", true);
                }
                if (rule.IconSetType != XlCondFmtIconSetType.TrafficLights3)
                {
                    this.WriteStringValue("iconSet", iconSetTypeTable[rule.IconSetType]);
                }
                if (!rule.ShowValues)
                {
                    this.WriteBoolValue("showValue", rule.ShowValues);
                }
                if (!rule.Percent)
                {
                    this.WriteBoolValue("percent", rule.Percent);
                }
                if (rule.Reverse)
                {
                    this.WriteBoolValue("reverse", rule.Reverse);
                }
                foreach (XlCondFmtValueObject obj2 in rule.Values)
                {
                    this.WriteCfvoExt(obj2, topLeftCell, true);
                }
                if (rule.IsCustom)
                {
                    if (rule.Values.Count != rule.CustomIcons.Count)
                    {
                        throw new InvalidOperationException("Number of custom icons not equals to number of values");
                    }
                    foreach (XlCondFmtCustomIcon icon in rule.CustomIcons)
                    {
                        this.WriteCustomIcon(icon.Id, icon.IconSetType);
                    }
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCondFmtRuleDataBarExt(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleDataBar rule)
        {
            this.WriteStartElement("cfRule", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                this.WriteStringValue("id", rule.GetRuleId());
                this.WriteCondFmtDataBarExt(conditionalFormatting, rule);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCondFmtRuleIconSetExt(XlConditionalFormatting conditionalFormatting, XlCondFmtRuleIconSet rule)
        {
            this.WriteStartElement("cfRule", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                this.WriteStringValue("id", rule.GetRuleId());
                this.WriteIntValue("priority", rule.Priority);
                this.WriteStringValue("type", "iconSet");
                this.WriteCondFmtIconSetExt(conditionalFormatting, rule);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCondFmtValue(string tag, XlValueObject value, XlCellPosition topLeft)
        {
            if (!value.IsEmpty)
            {
                if (value.IsRange)
                {
                    this.WriteShString(tag, value.RangeValue.ToString(), true);
                }
                else if (value.IsExpression)
                {
                    this.expressionContext.CurrentCell = topLeft;
                    this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                    this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                    string str = value.Expression.ToString(this.expressionContext);
                    this.WriteShString(tag, this.EncodeXmlChars(str), true);
                }
                else if (!value.IsFormula)
                {
                    XlVariantValue variantValue = value.VariantValue;
                    string str3 = !variantValue.IsText ? variantValue.ToText().TextValue : $""{variantValue.TextValue}"";
                    this.WriteShString(tag, this.EncodeXmlChars(str3), true);
                }
                else
                {
                    string formula = value.Formula.Remove(0, 1);
                    if (this.formulaParser != null)
                    {
                        this.expressionContext.CurrentCell = topLeft;
                        this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                        this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                        XlExpression expression = this.formulaParser.Parse(formula, this.expressionContext);
                        if ((expression == null) || (expression.Count == 0))
                        {
                            formula = "#VALUE!";
                        }
                    }
                    this.WriteShString(tag, this.EncodeXmlChars(formula), true);
                }
            }
        }

        private void WriteConnectorShape(XlDrawingObjectBase drawingObject)
        {
            XlShape shape = (XlShape) drawingObject;
            this.WriteStartElement("cxnSp", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringValue("macro", string.Empty);
                this.WriteConnectorShapeNonVisualProperties(shape);
                this.WriteShapeProperties(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteConnectorShapeNonVisualProperties(XlShape shape)
        {
            this.WriteStartElement("nvCxnSpPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteNonVisualDrawingProperties(shape);
                this.WriteNonVisualConnectorShapeProperties(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteCustomIcon(int iconId, XlCondFmtIconSetType iconSetType)
        {
            this.WriteStartElement("x14", "cfIcon", null);
            try
            {
                this.WriteIntValue("iconId", iconId);
                this.WriteStringValue("iconSet", iconSetTypeTable[iconSetType]);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteDataValidationAttributes(XlDataValidation validation)
        {
            if (validation.Type != XlDataValidationType.None)
            {
                this.WriteShStringValue("type", dataValidationTypeTable[validation.Type]);
            }
            if (validation.AllowBlank)
            {
                this.WriteBoolValue("allowBlank", validation.AllowBlank);
            }
            if (validation.ErrorStyle != XlDataValidationErrorStyle.Stop)
            {
                this.WriteShStringValue("errorStyle", dataValidationErrorStyleTable[validation.ErrorStyle]);
            }
            if (validation.ImeMode != XlDataValidationImeMode.NoControl)
            {
                this.WriteShStringValue("imeMode", dataValidationImeModeTable[validation.ImeMode]);
            }
            if (validation.Operator != XlDataValidationOperator.Between)
            {
                this.WriteShStringValue("operator", dataValidationOperatorTable[validation.Operator]);
            }
            if (!validation.ShowDropDown && (validation.Type == XlDataValidationType.List))
            {
                this.WriteBoolValue("showDropDown", true);
            }
            if (validation.ShowInputMessage)
            {
                this.WriteBoolValue("showInputMessage", true);
            }
            if (validation.ShowErrorMessage && (validation.Type != XlDataValidationType.None))
            {
                this.WriteBoolValue("showErrorMessage", true);
            }
            if (!string.IsNullOrEmpty(validation.ErrorTitle))
            {
                this.WriteStringAttr(null, "errorTitle", null, validation.ErrorTitle);
            }
            if (!string.IsNullOrEmpty(validation.ErrorMessage))
            {
                this.WriteStringAttr(null, "error", null, validation.ErrorMessage);
            }
            if (!string.IsNullOrEmpty(validation.PromptTitle))
            {
                this.WriteStringAttr(null, "promptTitle", null, validation.PromptTitle);
            }
            if (!string.IsNullOrEmpty(validation.InputPrompt))
            {
                this.WriteStringAttr(null, "prompt", null, validation.InputPrompt);
            }
        }

        private void WriteDataValidationFormulaExt(string tag, string formulaBody)
        {
            this.WriteStartElement(tag, "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            try
            {
                this.WriteString("f", "http://schemas.microsoft.com/office/excel/2006/main", formulaBody);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteDcTermsDateTime(string tag, DateTime dateTime)
        {
            this.WriteStartElement("dcterms", tag, "http://purl.org/dc/terms/");
            try
            {
                this.WriteStringAttr("xsi", "type", null, "dcterms:W3CDTF");
                this.writer.WriteString(this.DateTimeToStringUniversal(dateTime));
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteDocumentCustomProperty(string name, XlCustomPropertyValue propertyValue, int index)
        {
            if (propertyValue.Type != XlVariantValueType.None)
            {
                this.WriteStartElement("property", "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties");
                try
                {
                    this.WriteStringValue("fmtid", "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}");
                    this.WriteIntValue("pid", index);
                    this.WriteStringValue("name", name);
                    switch (propertyValue.Type)
                    {
                        case XlVariantValueType.Boolean:
                            this.WriteVariantValue("bool", propertyValue.BooleanValue ? "true" : "false");
                            break;

                        case XlVariantValueType.Text:
                            this.WriteVariantValue("lpwstr", propertyValue.TextValue);
                            break;

                        case XlVariantValueType.Numeric:
                            if (propertyValue.NumericValue == ((int) propertyValue.NumericValue))
                            {
                                this.WriteVariantValue("i4", ((int) propertyValue.NumericValue).ToString());
                            }
                            else
                            {
                                this.WriteVariantValue("r8", propertyValue.NumericValue.ToString(CultureInfo.InvariantCulture));
                            }
                            break;

                        case XlVariantValueType.DateTime:
                            this.WriteVariantValue("filetime", propertyValue.DateTimeValue.ToUniversalTime().ToString(this.CurrentCulture.DateTimeFormat.SortableDateTimePattern) + "Z");
                            break;

                        default:
                            break;
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        protected internal void WriteDoubleValue(string tag, double value)
        {
            this.WriteStringAttr(null, tag, null, value.ToString(CultureInfo.InvariantCulture));
        }

        private void WriteDxfId(XlCondFmtRuleWithFormatting rule)
        {
            if (rule != null)
            {
                int num = this.RegisterCondFmtDifferentialFormatting(rule.Formatting);
                if (num >= 0)
                {
                    this.WriteIntValue("dxfId", num);
                }
            }
        }

        private void WriteDxfId(string attributeName, XlDifferentialFormatting formatting)
        {
            int num = this.RegisterDifferentialFormatting(formatting);
            if (num >= 0)
            {
                this.WriteIntValue(attributeName, num);
            }
        }

        private void WriteElementWithValAttr(string tag, bool val)
        {
            this.WriteShStartElement(tag);
            try
            {
                this.WriteBoolValue("val", val);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteElementWithValAttr(string tag, int val)
        {
            this.WriteShStartElement(tag);
            try
            {
                this.WriteIntValue("val", val);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteElementWithValAttr(string tag, string val)
        {
            this.WriteShStartElement(tag);
            try
            {
                this.WriteStringAttr(null, "val", null, val);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteEmptyBorder(string tag)
        {
            this.WriteShStartElement(tag);
            this.WriteShEndElement();
        }

        protected internal void WriteEndElement()
        {
            this.writer.WriteEndElement();
        }

        private void WriteExtListReference(string id)
        {
            this.WriteShStartElement("ext");
            try
            {
                this.WriteStringValue("uri", "{B025F937-C7B1-47D3-B67F-A62EFF666E3E}");
                this.WriteStringAttr("xmlns", "x14", null, "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
                this.WriteString("id", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main", id);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteFill(XlFill fill)
        {
            this.WriteShStartElement("fill");
            try
            {
                this.WriteShStartElement("patternFill");
                try
                {
                    this.WriteStringValue("patternType", patternTypeTable[fill.PatternType]);
                    this.WriteColor(fill.ForeColor, "fgColor");
                    this.WriteColor(fill.BackColor, "bgColor");
                }
                finally
                {
                    this.WriteShEndElement();
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteFont(XlFont font, bool isDefaultFont)
        {
            this.WriteShStartElement("font");
            try
            {
                if (font.Bold != this.defaultFont.Bold)
                {
                    this.WriteBoolFontValue("b", font.Bold);
                }
                if (font.Italic != this.defaultFont.Italic)
                {
                    this.WriteBoolFontValue("i", font.Italic);
                }
                if (font.StrikeThrough != this.defaultFont.StrikeThrough)
                {
                    this.WriteBoolFontValue("strike", font.StrikeThrough);
                }
                if (font.Condense != this.defaultFont.Condense)
                {
                    this.WriteBoolFontValue("condense", font.Condense);
                }
                if (font.Extend != this.defaultFont.Extend)
                {
                    this.WriteBoolFontValue("extend", font.Extend);
                }
                if (font.Outline != this.defaultFont.Outline)
                {
                    this.WriteBoolFontValue("outline", font.Outline);
                }
                if (font.Shadow != this.defaultFont.Shadow)
                {
                    this.WriteBoolFontValue("shadow", font.Shadow);
                }
                if (font.Underline != this.defaultFont.Underline)
                {
                    this.WriteUnderlineType(font.Underline);
                }
                if (font.Script != this.defaultFont.Script)
                {
                    this.WriteVerticalAlignment(font.Script);
                }
                if (font.Size != 0.0)
                {
                    this.WriteFontSize(font.Size);
                }
                XlColor info = font.Color;
                if ((!info.Equals(this.defaultFont.Color) | isDefaultFont) && !info.IsEmpty)
                {
                    this.WriteColor(info, "color");
                }
                if (!string.IsNullOrEmpty(font.Name))
                {
                    this.WriteFontName(font.Name);
                }
                if ((font.FontFamily != this.defaultFont.FontFamily) | isDefaultFont)
                {
                    this.WriteFontFamily((int) font.FontFamily);
                }
                if (font.Charset != this.defaultFont.Charset)
                {
                    this.WriteCharset(font.Charset);
                }
                if (font.SchemeStyle != XlFontSchemeStyles.None)
                {
                    this.WriteSchemeStyle(font.SchemeStyle);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteFontFamily(int fontFamily)
        {
            this.WriteShStartElement("family");
            try
            {
                this.WriteIntValue("val", fontFamily);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteFontName(string name)
        {
            this.WriteShStartElement("name");
            try
            {
                this.WriteStringValue("val", name);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteFontSize(double size)
        {
            this.WriteShStartElement("sz");
            try
            {
                string str = size.ToString(CultureInfo.InvariantCulture);
                this.WriteStringValue("val", str);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteGraphicFrame(XlShape shape)
        {
            XlGraphicFrame frame = shape.Frame;
            if (!frame.IsDefault())
            {
                this.WriteStartElement("xfrm", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    if (frame.Rotation != 0.0)
                    {
                        this.WriteIntValue("rot", (int) (frame.Rotation * 60000.0));
                    }
                    if (frame.FlipHorizontal)
                    {
                        this.WriteBoolValue("flipH", true);
                    }
                    if (frame.FlipVertical)
                    {
                        this.WriteBoolValue("flipV", true);
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteHeaderFooterItem(string tag, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.WriteShString(tag, this.EncodeXmlChars(value), true);
            }
        }

        private void WriteHyperlinkClick(IXlHyperlinkOwner drawingObject)
        {
            if ((drawingObject.HyperlinkClick != null) && !string.IsNullOrEmpty(drawingObject.HyperlinkClick.TargetUri))
            {
                string hyperlinkRelationId = this.GetHyperlinkRelationId(drawingObject);
                this.WriteStartElement("hlinkClick", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteStringAttr("xmlns", "r", null, "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
                    this.WriteStringAttr("r", "id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships", hyperlinkRelationId);
                    if (!string.IsNullOrEmpty(drawingObject.HyperlinkClick.Tooltip))
                    {
                        this.WriteStringValue("tooltip", this.EncodeXmlChars(drawingObject.HyperlinkClick.Tooltip));
                    }
                    if (!string.IsNullOrEmpty(drawingObject.HyperlinkClick.TargetFrame))
                    {
                        this.WriteStringValue("tgtFrame", drawingObject.HyperlinkClick.TargetFrame);
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        protected internal void WriteIntValue(string tag, int value)
        {
            this.WriteStringAttr(null, tag, null, value.ToString());
        }

        protected internal void WriteIntValue(string attr, int value, bool shouldExport)
        {
            if (shouldExport)
            {
                this.WriteIntValue(attr, value);
            }
        }

        protected internal void WriteIntValue(string attr, int value, int defaultValue)
        {
            this.WriteIntValue(attr, value, value != defaultValue);
        }

        private void WriteMarginValue(double value, string name)
        {
            this.WriteStringValue(name, value.ToString(CultureInfo.InvariantCulture));
        }

        private void WriteNonVisualConnectorShapeProperties(XlShape shape)
        {
            this.WriteStartElement("cNvCxnSpPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteNonVisualDrawingProperties(XlShape shape)
        {
            this.WriteStartElement("cNvPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.shapeId++;
                int num = this.shapeId + 1;
                this.WriteIntValue("id", num);
                string name = shape.Name;
                if (string.IsNullOrEmpty(name))
                {
                    if (shape.GeometryPreset == XlGeometryPreset.Line)
                    {
                        name = $"Straight Connector {this.shapeId}";
                    }
                    else if (shape.GeometryPreset == XlGeometryPreset.Rect)
                    {
                        name = $"Rectangle {this.shapeId}";
                    }
                }
                this.WriteStringValue("name", name);
                this.WriteHyperlinkClick(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteNonVisualDrawingProperties(XlsxPicture drawingObject)
        {
            this.WriteStartElement("cNvPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteIntValue("id", drawingObject.PictureId);
                this.WriteStringValue("name", drawingObject.Name);
                this.WriteHyperlinkClick(drawingObject);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteNonVisualPictureDrawingProperties(XlsxPicture drawingObject)
        {
            this.WriteStartElement("cNvPicPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteNonVisualShapeProperties(XlShape shape)
        {
            this.WriteStartElement("cNvSpPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteNumberFormat(ExcelNumberFormat format)
        {
            this.WriteShStartElement("numFmt");
            try
            {
                this.WriteIntValue("numFmtId", format.Id);
                this.WriteStringValue("formatCode", this.EncodeXmlChars(format.FormatString));
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteOneCellAnchor(XlDrawingObjectBase drawingObject, Action<XlDrawingObjectBase> action)
        {
            this.WriteStartElement("oneCellAnchor", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteAnchorPoint("from", drawingObject.TopLeft, false);
                this.WriteShapeExtent(drawingObject);
                action(drawingObject);
                this.WriteClientData(drawingObject);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal void WriteOptionalBoolValue(string attr, bool value, bool shouldExport)
        {
            if (shouldExport)
            {
                this.WriteBoolValue(attr, value);
            }
        }

        private void WritePercentage(string tag, double value)
        {
            long num = (long) (value * 100000.0);
            if (num != 0)
            {
                this.WriteStringValue(tag, num.ToString());
            }
        }

        private void WritePicture(XlDrawingObjectBase drawingObject)
        {
            XlsxPicture picture = (XlsxPicture) drawingObject;
            this.WriteStartElement("pic", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WritePictureNonVisualProperties(picture);
                this.WriteBlipFill(picture);
                this.WriteShapeProperties();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WritePictureNonVisualProperties(XlsxPicture drawingObject)
        {
            this.WriteStartElement("nvPicPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteNonVisualDrawingProperties(drawingObject);
                this.WriteNonVisualPictureDrawingProperties(drawingObject);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WritePresetGeometry()
        {
            this.WriteStartElement("prstGeom", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("prst", "rect");
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WritePresetGeometry(XlShape shape)
        {
            this.WriteStartElement("prstGeom", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("prst", geometryPresetTable[shape.GeometryPreset]);
                this.WriteShapeAdjustValues(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteSchemeStyle(XlFontSchemeStyles schemeStyle)
        {
            this.WriteShStartElement("scheme");
            try
            {
                this.WriteStringValue("val", fontSchemeStyleTable[schemeStyle]);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteShapeAdjustValues(XlShape shape)
        {
            this.WriteStartElement("avLst", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeColorRGB(XlColor color, double opacity)
        {
            this.WriteStartElement("srgbClr", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                Color rgb = color.Rgb;
                string str = $"{(int) rgb.R:X2}{(int) rgb.G:X2}{(int) rgb.B:X2}";
                this.WriteStringValue("val", str);
                this.WriteShapeFillOpacity(opacity);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeColorScheme(XlColor color, double opacity)
        {
            this.WriteStartElement("schemeClr", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStringValue("val", schemeColorTable[color.ThemeColor]);
                this.WriteShapeColorTint(color.Tint);
                this.WriteShapeFillOpacity(opacity);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeColorTint(double tint)
        {
            int num = Math.Min((int) ((1.0 - Math.Abs(tint)) * 100000.0), 0x186a0);
            int num2 = Math.Min((int) (Math.Abs(tint) * 100000.0), 0x186a0);
            if (num2 != 0)
            {
                this.WriteStartElement("lumMod", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteIntValue("val", num);
                }
                finally
                {
                    this.WriteEndElement();
                }
                if (tint > 0.0)
                {
                    this.WriteStartElement("lumOff", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    try
                    {
                        this.WriteIntValue("val", num2);
                    }
                    finally
                    {
                        this.WriteEndElement();
                    }
                }
            }
        }

        private void WriteShapeExtent(XlDrawingObjectBase drawingObject)
        {
            long num = this.PixelsToEMU(drawingObject.BottomRight.ColumnOffsetInPixels - drawingObject.TopLeft.ColumnOffsetInPixels);
            long num2 = this.PixelsToEMU(drawingObject.BottomRight.RowOffsetInPixels - drawingObject.TopLeft.RowOffsetInPixels);
            this.WriteStartElement("ext", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringValue("cx", num.ToString());
                this.WriteStringValue("cy", num2.ToString());
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeFill(XlShape shape)
        {
            if (!shape.IsConnector)
            {
                if (shape.Fill.FillType == XlDrawingFillType.None)
                {
                    this.WriteStartElement("noFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    this.WriteEndElement();
                }
                else if (shape.Fill.FillType == XlDrawingFillType.Solid)
                {
                    this.WriteStartElement("solidFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
                    try
                    {
                        if (shape.Fill.Color.ColorType == XlColorType.Rgb)
                        {
                            this.WriteShapeColorRGB(shape.Fill.Color, shape.Fill.Opacity);
                        }
                        else if (shape.Fill.Color.ColorType == XlColorType.Theme)
                        {
                            this.WriteShapeColorScheme(shape.Fill.Color, shape.Fill.Opacity);
                        }
                    }
                    finally
                    {
                        this.WriteEndElement();
                    }
                }
            }
        }

        private void WriteShapeFillOpacity(double opacity)
        {
            int num = Math.Min((int) (opacity * 100000.0), 0x186a0);
            if (num != 0x186a0)
            {
                this.WriteStartElement("alpha", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteIntValue("val", num);
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteShapeNonVisualProperties(XlShape shape)
        {
            this.WriteStartElement("nvSpPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteNonVisualDrawingProperties(shape);
                this.WriteNonVisualShapeProperties(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeOutline(XlShape shape)
        {
            XlOutline outline = shape.Outline;
            this.WriteStartElement("ln", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                int num = (int) (outline.Width * 12700.0);
                if (num > 0)
                {
                    this.WriteIntValue("w", num);
                }
                if (outline.CapType != XlLineCapType.Flat)
                {
                    this.WriteStringValue("cap", capTypeTable[outline.CapType]);
                }
                if (outline.CompoundType != XlOutlineCompoundType.Single)
                {
                    this.WriteStringValue("cmpd", compoundTypeTable[outline.CompoundType]);
                }
                if (outline.StrokeAlignment == XlOutlineStrokeAlignment.Inset)
                {
                    this.WriteStringValue("algn", "in");
                }
                this.WriteShapeOutlineFill(outline);
                this.WriteShapeOutlinePresetDash(outline);
                this.WriteShapeOutlineJoinStyle(outline);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeOutlineFill(XlOutline outline)
        {
            if (outline.Color.IsEmpty)
            {
                this.WriteStartElement("noFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
                this.WriteEndElement();
            }
            else
            {
                this.WriteStartElement("solidFill", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    if (outline.Color.ColorType == XlColorType.Rgb)
                    {
                        this.WriteShapeColorRGB(outline.Color, outline.Opacity);
                    }
                    else if (outline.Color.ColorType == XlColorType.Theme)
                    {
                        this.WriteShapeColorScheme(outline.Color, outline.Opacity);
                    }
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteShapeOutlineJoinStyle(XlOutline outline)
        {
            if (outline.JoinType == XlLineJoinType.Bevel)
            {
                this.WriteStartElement("bevel", "http://schemas.openxmlformats.org/drawingml/2006/main");
                this.WriteEndElement();
            }
            else if (outline.JoinType == XlLineJoinType.Miter)
            {
                this.WriteStartElement("miter", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteIntValue("lim", (int) (outline.MiterLimit * 100000.0));
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteShapeOutlinePresetDash(XlOutline outline)
        {
            if (outline.Dashing != XlOutlineDashing.Solid)
            {
                this.WriteStartElement("prstDash", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WriteStringValue("val", presetDashTable[outline.Dashing]);
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteShapePosition(XlDrawingObjectBase drawingObject)
        {
            long num = this.PixelsToEMU(drawingObject.TopLeft.ColumnOffsetInPixels);
            long num2 = this.PixelsToEMU(drawingObject.TopLeft.RowOffsetInPixels);
            this.WriteStartElement("pos", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringValue("x", num.ToString());
                this.WriteStringValue("y", num2.ToString());
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeProperties()
        {
            this.WriteStartElement("spPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WritePresetGeometry();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteShapeProperties(XlShape shape)
        {
            this.WriteStartElement("spPr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteGraphicFrame(shape);
                this.WritePresetGeometry(shape);
                this.WriteShapeFill(shape);
                this.WriteShapeOutline(shape);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal void WriteShBoolAttr(string attr, bool value)
        {
            this.WriteShStringAttr(attr, this.ConvertBoolToString(value));
        }

        protected internal void WriteShBoolValue(string tag, bool value)
        {
            this.WriteShStringValue(tag, this.ConvertBoolToString(value));
        }

        protected internal void WriteShEndElement()
        {
            this.WriteEndElement();
        }

        protected internal void WriteShIntAttr(string attr, int value)
        {
            this.WriteShStringAttr(attr, value.ToString());
        }

        protected internal void WriteShIntValue(string tag, int value)
        {
            this.WriteShStringValue(tag, value.ToString());
        }

        protected internal void WriteShStartElement(string tag)
        {
            this.WriteStartElement(tag, "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
        }

        protected internal virtual void WriteShString(string text)
        {
            this.writer.WriteString(text);
        }

        protected internal virtual void WriteShString(string tag, string text)
        {
            this.WriteShString(tag, text, false);
        }

        protected internal virtual void WriteShString(string tag, string text, bool writeXmlSpaceAttr)
        {
            this.WriteString(tag, text, "http://schemas.openxmlformats.org/spreadsheetml/2006/main", writeXmlSpaceAttr);
        }

        protected internal virtual void WriteShStringAttr(string attr, string value)
        {
            this.writer.WriteAttributeString(attr, "http://schemas.openxmlformats.org/spreadsheetml/2006/main", value);
        }

        protected internal virtual void WriteShStringValue(string tag, string value)
        {
            this.WriteStringValue(tag, value);
        }

        private void WriteSourceRectangle(XlsxPicture drawingObject)
        {
            XlSourceRectangle sourceRectangle = drawingObject.SourceRectangle;
            if (!sourceRectangle.IsDefault)
            {
                this.WriteStartElement("srcRect", "http://schemas.openxmlformats.org/drawingml/2006/main");
                try
                {
                    this.WritePercentage("l", drawingObject.SourceRectangle.Left);
                    this.WritePercentage("t", drawingObject.SourceRectangle.Top);
                    this.WritePercentage("r", drawingObject.SourceRectangle.Right);
                    this.WritePercentage("b", drawingObject.SourceRectangle.Bottom);
                }
                finally
                {
                    this.WriteEndElement();
                }
            }
        }

        private void WriteSparklineGroupAttributes(XlSparklineGroup sparklineGroup)
        {
            if (sparklineGroup.MaxScaling == XlSparklineAxisScaling.Custom)
            {
                this.WriteDoubleValue("manualMax", sparklineGroup.ManualMax);
            }
            if (sparklineGroup.MinScaling == XlSparklineAxisScaling.Custom)
            {
                this.WriteDoubleValue("manualMin", sparklineGroup.ManualMin);
            }
            if (sparklineGroup.LineWeight != 0.75)
            {
                this.WriteDoubleValue("lineWeight", sparklineGroup.LineWeight);
            }
            if (sparklineGroup.SparklineType != XlSparklineType.Line)
            {
                this.WriteShStringValue("type", sparklineTypeTable[sparklineGroup.SparklineType]);
            }
            if (sparklineGroup.DateRange != null)
            {
                this.WriteBoolValue("dateAxis", true);
            }
            if (sparklineGroup.DisplayBlanksAs != XlDisplayBlanksAs.Zero)
            {
                this.WriteShStringValue("displayEmptyCellsAs", displayBlanksAsTable[sparklineGroup.DisplayBlanksAs]);
            }
            if (sparklineGroup.DisplayMarkers && !sparklineGroup.ColorMarker.IsAutoOrEmpty)
            {
                this.WriteBoolValue("markers", true);
            }
            if (sparklineGroup.HighlightHighest && !sparklineGroup.ColorHigh.IsAutoOrEmpty)
            {
                this.WriteBoolValue("high", true);
            }
            if (sparklineGroup.HighlightLowest && !sparklineGroup.ColorLow.IsAutoOrEmpty)
            {
                this.WriteBoolValue("low", true);
            }
            if (sparklineGroup.HighlightFirst && !sparklineGroup.ColorFirst.IsAutoOrEmpty)
            {
                this.WriteBoolValue("first", true);
            }
            if (sparklineGroup.HighlightLast && !sparklineGroup.ColorLast.IsAutoOrEmpty)
            {
                this.WriteBoolValue("last", true);
            }
            if (sparklineGroup.HighlightNegative && !sparklineGroup.ColorNegative.IsAutoOrEmpty)
            {
                this.WriteBoolValue("negative", true);
            }
            if (sparklineGroup.DisplayXAxis && !sparklineGroup.ColorAxis.IsAutoOrEmpty)
            {
                this.WriteBoolValue("displayXAxis", true);
            }
            if (sparklineGroup.DisplayHidden)
            {
                this.WriteBoolValue("displayHidden", true);
            }
            if (sparklineGroup.MinScaling != XlSparklineAxisScaling.Individual)
            {
                this.WriteShStringValue("minAxisType", sparklineAxisScalingTable[sparklineGroup.MinScaling]);
            }
            if (sparklineGroup.MaxScaling != XlSparklineAxisScaling.Individual)
            {
                this.WriteShStringValue("maxAxisType", sparklineAxisScalingTable[sparklineGroup.MaxScaling]);
            }
            if (sparklineGroup.RightToLeft)
            {
                this.WriteBoolValue("rightToLeft", true);
            }
        }

        protected internal void WriteStartElement(string tag, string ns)
        {
            this.writer.WriteStartElement(tag, ns);
        }

        protected internal void WriteStartElement(string prefix, string tag, string ns)
        {
            this.writer.WriteStartElement(prefix, tag, ns);
        }

        private void WriteStretch()
        {
            this.WriteStartElement("stretch", "http://schemas.openxmlformats.org/drawingml/2006/main");
            try
            {
                this.WriteStartElement("fillRect", "http://schemas.openxmlformats.org/drawingml/2006/main");
                this.WriteEndElement();
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void WriteString(string tag, string ns, string text)
        {
            this.WriteString(tag, text, ns, false);
        }

        protected internal virtual void WriteString(string tag, string text, string ns, bool writeXmlSpaceAttr)
        {
            this.WriteString(string.Empty, tag, text, ns, writeXmlSpaceAttr);
        }

        protected internal virtual void WriteString(string prefix, string tag, string text, string ns, bool writeXmlSpaceAttr)
        {
            if (!string.IsNullOrEmpty(prefix))
            {
                this.WriteStartElement(prefix, tag, ns);
            }
            else
            {
                this.WriteStartElement(tag, ns);
            }
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    if (writeXmlSpaceAttr && this.IsNeedWriteXmlSpaceAttr(text))
                    {
                        this.WriteStringAttr("xml", "space", null, "preserve");
                    }
                    this.writer.WriteString(text);
                }
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        protected internal virtual void WriteStringAttr(string prefix, string attr, string ns, string value)
        {
            this.writer.WriteAttributeString(prefix, attr, ns, value);
        }

        protected internal virtual void WriteStringValue(string tag, string value)
        {
            this.WriteStringAttr(null, tag, null, value);
        }

        protected internal virtual void WriteStringValue(string attr, string value, bool shouldExport)
        {
            if (shouldExport)
            {
                this.WriteStringValue(attr, value);
            }
        }

        private void WriteTablePart(XlTable table)
        {
            string id = this.sheetRelations.GenerateId();
            OpenXmlRelation item = new OpenXmlRelation(id, $"../tables/table{table.Id}.xml", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table");
            this.sheetRelations.Add(item);
            this.WriteShStartElement("tablePart");
            try
            {
                this.WriteStringAttr("r", "id", null, id);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteTwoCellAnchor(XlDrawingObjectBase drawingObject, Action<XlDrawingObjectBase> action)
        {
            this.WriteStartElement("twoCellAnchor", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
            try
            {
                this.WriteStringValue("editAs", anchorTypeTable[drawingObject.AnchorBehavior]);
                this.WriteAnchorPoint("from", drawingObject.TopLeft, false);
                this.WriteAnchorPoint("to", drawingObject.BottomRight, drawingObject.IsSingleCellAnchor());
                action(drawingObject);
                this.WriteClientData(drawingObject);
            }
            finally
            {
                this.WriteEndElement();
            }
        }

        private void WriteUnderlineType(XlUnderlineType underlineType)
        {
            this.WriteShStartElement("u");
            try
            {
                if (underlineType != XlUnderlineType.Single)
                {
                    this.WriteStringValue("val", underlineTypeTable[underlineType]);
                }
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private void WriteVariantValue(string tag, string value)
        {
            this.WriteString("vt", tag, value, "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", false);
        }

        private void WriteVerticalAlignment(XlScriptType script)
        {
            this.WriteShStartElement("vertAlign");
            try
            {
                this.WriteStringValue("val", verticalAlignmentRunTypeTable[script]);
            }
            finally
            {
                this.WriteShEndElement();
            }
        }

        private Dictionary<XlDynamicFilterType, string> DynamicFilterTypeTable
        {
            get
            {
                this.dynamicFilterTypeTable ??= CreateDynamicFilterTypeTable();
                return this.dynamicFilterTypeTable;
            }
        }

        private Dictionary<XlFilterOperator, string> FilterOperatorTable
        {
            get
            {
                this.filterOperatorTable ??= CreateFilterOperatorTable();
                return this.filterOperatorTable;
            }
        }

        private Dictionary<XlCalendarType, string> CalendarTypeTable
        {
            get
            {
                this.calendarTypeTable ??= CreateCalendarTypeTable();
                return this.calendarTypeTable;
            }
        }

        private Dictionary<XlDateTimeGroupingType, string> DateTimeGroupingTable
        {
            get
            {
                this.dateTimeGroupingTable ??= CreateDateTimeGroupingTable();
                return this.dateTimeGroupingTable;
            }
        }

        public int CurrentColumnIndex =>
            this.columnIndex;

        public int CurrentOutlineLevel =>
            (this.currentGroup != null) ? this.currentGroup.OutlineLevel : 0;

        public int CurrentRowIndex =>
            this.rowIndex;

        public IXlDocument CurrentDocument =>
            this.currentDocument;

        public IXlSheet CurrentSheet =>
            this.currentSheet;

        protected XlsxPackageBuilder Builder =>
            this.builder;

        public IXlDocumentOptions DocumentOptions =>
            this.options;

        public XlDocumentProperties DocumentProperties =>
            this.documentProperties;

        internal XlCalculationOptions CalculationOptions =>
            this.calculationOptions;

        public CultureInfo CurrentCulture =>
            (this.options.Culture == null) ? CultureInfo.InvariantCulture : this.options.Culture;

        public IXlFormulaEngine FormulaEngine =>
            this;

        public IXlFormulaParser FormulaParser =>
            this.formulaParser;

        private float DpiY =>
            this.options.UseDeviceIndependentPixels ? 96f : GraphicsDpi.Pixel;
    }
}

