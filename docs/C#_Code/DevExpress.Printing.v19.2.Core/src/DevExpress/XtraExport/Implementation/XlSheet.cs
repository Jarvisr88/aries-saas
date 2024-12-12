namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class XlSheet : IXlSheet, IDisposable, IXlOutlineProperties, IXlSheetViewOptions, IXlShapeContainer, IXlTableContainer, IXlSheetSelection
    {
        private const int maxHyperlinksCount = 0xfffa;
        private static char[] illegalCharacters = new char[] { '\0', '\x0003', ':', '\\', '*', '?', '/', '[', ']' };
        private readonly XlMergedCellsCollection mergedCells = new XlMergedCellsCollection();
        private readonly List<XlConditionalFormatting> conditionalFormattings = new List<XlConditionalFormatting>();
        private readonly List<XlDataValidation> dataValidations = new List<XlDataValidation>();
        private readonly XlHeaderFooter headerFooter = new XlHeaderFooter();
        private readonly XlPrintTitles printTitles;
        private readonly XlPageBreaksCollection columnPageBreaks;
        private readonly XlPageBreaksCollection rowPageBreaks;
        private readonly XlHyperlinksCollection hyperlinks;
        private readonly List<XlTable> innerTables = new List<XlTable>();
        private readonly XlTableCollection tables;
        private readonly List<XlTable> activeTables = new List<XlTable>();
        private readonly List<XlSparklineGroup> sparklineGroups = new List<XlSparklineGroup>();
        private readonly XlFilterColumnsCollection autoFilterColumns = new XlFilterColumnsCollection();
        private IXlExport exporter;
        private int leftColumnIndex = -1;
        private int rightColumnIndex = -1;
        private int topRowIndex = -1;
        private int bottomRowIndex = -1;
        private int firstColumnIndex = -1;
        private int lastColumnIndex = -1;
        private bool summaryBelow;
        private bool summaryRight = true;
        private string name;
        private bool showFormulas;
        private bool showGridLines = true;
        private bool showRowColumnHeaders = true;
        private bool showZeroValues = true;
        private bool showOutlineSymbols = true;
        private bool rightToLeft;
        private XlTable lastTable;
        private bool hasActiveAutoFilter;
        private XlTable pendingTable;
        private readonly List<XlCellRange> selectedRanges = new List<XlCellRange>();
        private static char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public XlSheet(IXlExport exporter)
        {
            this.exporter = exporter;
            this.tables = new XlTableCollection(this.innerTables);
            this.printTitles = new XlPrintTitles(this);
            this.columnPageBreaks = new XlPageBreaksCollection((exporter != null) ? (exporter.DocumentOptions.MaxColumnCount - 1) : 0x3fff);
            this.rowPageBreaks = new XlPageBreaksCollection((exporter != null) ? (exporter.DocumentOptions.MaxRowCount - 1) : 0xfffff);
            this.hyperlinks = new XlHyperlinksCollection(((exporter == null) || (exporter.DocumentOptions.DocumentFormat != XlDocumentFormat.Csv)) ? 0xfffa : 0x7fffffff);
            this.DefaultMaxDigitWidthInPixels = this.CalculateMaxDigitWidthInPixels();
            this.DefaultColumnWidthInPixels = 64f;
            this.DefaultRowHeightInPixels = 20f;
            this.VisibleState = XlSheetVisibleState.Visible;
            this.IgnoreErrors = DevExpress.Export.Xl.XlIgnoreErrors.NumberStoredAsText;
        }

        protected virtual void AddShapeCore(XlShape shape)
        {
            IXlShapeContainer exporter = this.exporter as IXlShapeContainer;
            if (exporter != null)
            {
                exporter.AddShape(shape);
            }
        }

        public void AddTable(XlTable table)
        {
            this.innerTables.Add(table);
            int index = Algorithms.BinarySearchReverseOrder<XlTable>(this.activeTables, table);
            if (index >= 0)
            {
                throw new InvalidOperationException("Cannot add new table");
            }
            index = ~index;
            this.activeTables.Insert(index, table);
            this.hasActiveAutoFilter = false;
        }

        public void BeginFiltering(XlCellRange autoFilterRange)
        {
            Guard.ArgumentNotNull(autoFilterRange, "autoFilterRange");
            if (this.HasActiveTables)
            {
                throw new InvalidOperationException("Worksheet auto filter cannot be activated inside table(s).");
            }
            if (this.AutoFilterColumns.Count == 0)
            {
                throw new InvalidOperationException("AutoFilter columns does not specified.");
            }
            this.AutoFilterRange = autoFilterRange;
            this.hasActiveAutoFilter = true;
        }

        public int BeginGroup(bool collapsed)
        {
            IXlGroup group = this.exporter.BeginGroup();
            group.OutlineLevel = this.exporter.CurrentOutlineLevel + 1;
            if (collapsed)
            {
                group.IsCollapsed = collapsed;
            }
            return this.exporter.CurrentOutlineLevel;
        }

        public int BeginGroup(int outlineLevel, bool collapsed)
        {
            if ((outlineLevel < 1) || (outlineLevel > 7))
            {
                throw new ArgumentOutOfRangeException("Outline level out of range 1..7!");
            }
            IXlGroup group = this.exporter.BeginGroup();
            group.OutlineLevel = outlineLevel;
            if (collapsed)
            {
                group.IsCollapsed = collapsed;
            }
            return this.exporter.CurrentOutlineLevel;
        }

        private IXlFilter[] BuildFilters()
        {
            int columnCount = this.AutoFilterRange.ColumnCount;
            IXlFilter[] filterArray = new IXlFilter[columnCount];
            foreach (XlFilterColumn column in this.AutoFilterColumns)
            {
                if (column.ColumnId < columnCount)
                {
                    filterArray[column.ColumnId] = column.FilterCriteria as IXlFilter;
                }
            }
            return filterArray;
        }

        private IXlFilter[] BuildFilters(XlTable table)
        {
            int count = table.Columns.Count;
            IXlFilter[] filterArray = new IXlFilter[count];
            for (int i = 0; i < count; i++)
            {
                filterArray[i] = table.Columns[i].FilterCriteria as IXlFilter;
            }
            return filterArray;
        }

        private float CalculateMaxDigitWidthInPixels()
        {
            float num = 7f;
            IXlDocumentOptionsEx documentOptions = null;
            if (this.exporter != null)
            {
                documentOptions = this.exporter.DocumentOptions as IXlDocumentOptionsEx;
            }
            if ((documentOptions != null) && (!documentOptions.UseDeviceIndependentPixels && (GraphicsDpi.Pixel != 96f)))
            {
                try
                {
                    using (Bitmap bitmap = new Bitmap(1, 1))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            using (Font font = new Font("Calibri", 11f, FontStyle.Regular))
                            {
                                using (StringFormat format = (StringFormat) StringFormat.GenericTypographic.Clone())
                                {
                                    format.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
                                    int length = digits.Length;
                                    float num3 = this.MeasureCharacterWidth(digits[0], graphics, font, format);
                                    int index = 1;
                                    while (true)
                                    {
                                        if (index >= length)
                                        {
                                            num = (float) Math.Round((double) num3);
                                            break;
                                        }
                                        num3 = Math.Max(num3, this.MeasureCharacterWidth(digits[index], graphics, font, format));
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return num;
        }

        private void CheckSheetName(string value)
        {
            if (string.IsNullOrEmpty(value) || (value.Length > 0x1f))
            {
                throw new ArgumentException("Worksheet name must be greater than or equal to 1 and less than or equal to 31 characters long.");
            }
            if ((value.IndexOfAny(illegalCharacters) >= 0) || ((value[0] == '\'') || (value[value.Length - 1] == '\'')))
            {
                throw new ArgumentException("Worksheet name contains illegal characters.");
            }
        }

        public bool ContainsTable(IXlTable table)
        {
            bool flag;
            using (IEnumerator<XlTable> enumerator = this.InnerTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTable current = enumerator.Current;
                        if (!ReferenceEquals(current, table))
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

        public IXlColumn CreateColumn() => 
            new XlColumnProxy(this.exporter, this.exporter.BeginColumn());

        public IXlColumn CreateColumn(int columnIndex)
        {
            int count = columnIndex - this.exporter.CurrentColumnIndex;
            if (count < 0)
            {
                throw new ArgumentException("Value must be greater than or equals to current column index.");
            }
            if (count > 0)
            {
                this.SkipColumns(count);
            }
            return new XlColumnProxy(this.exporter, this.exporter.BeginColumn());
        }

        public IXlPicture CreatePicture() => 
            this.exporter.BeginPicture();

        public IXlRow CreateRow() => 
            new XlRowProxy(this.exporter, this.exporter.BeginRow());

        public IXlRow CreateRow(int rowIndex)
        {
            int count = rowIndex - this.exporter.CurrentRowIndex;
            if (count < 0)
            {
                throw new ArgumentException("Value must be greater than or equals to current row index.");
            }
            if (count > 0)
            {
                this.SkipRows(count);
            }
            return new XlRowProxy(this.exporter, this.exporter.BeginRow());
        }

        public void DeactivateTable(XlTable table)
        {
            if (ReferenceEquals(table, this.lastTable))
            {
                this.lastTable = null;
            }
            this.activeTables.Remove(table);
        }

        void IXlShapeContainer.AddShape(XlShape shape)
        {
            Guard.ArgumentNotNull(shape, "shape");
            this.AddShapeCore(shape);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.exporter = null;
        }

        public void EndFiltering()
        {
            this.hasActiveAutoFilter = false;
        }

        public void EndGroup()
        {
            this.exporter.EndGroup();
        }

        internal void ExtendTableRanges(int rowIndex)
        {
            foreach (XlTable table in this.InnerTables)
            {
                table.ExtendRange(rowIndex);
            }
        }

        internal void FilterRow(IXlRow row, IList<XlCell> cells)
        {
            if (this.hasActiveAutoFilter && (this.AutoFilterRange != null))
            {
                this.AutoFilterRange.BottomRight = new XlCellPosition(this.AutoFilterRange.BottomRight.Column, Math.Max(row.RowIndex, this.AutoFilterRange.BottomRight.Row));
                IXlFilter[] filterArray = this.BuildFilters();
                XlCell[] cellsInFilterRange = this.GetCellsInFilterRange(cells, this.AutoFilterRange);
                IXlCellFormatter exporter = this.exporter as IXlCellFormatter;
                int length = filterArray.Length;
                for (int i = 0; i < length; i++)
                {
                    IXlFilter filter = filterArray[i];
                    if ((filter != null) && !filter.MeetCriteria(cellsInFilterRange[i], exporter))
                    {
                        row.IsHidden = true;
                        return;
                    }
                }
            }
            else if (!this.HasActiveTables)
            {
                if (this.pendingTable != null)
                {
                    this.FilterRowInTable(row, cells, this.pendingTable);
                    this.pendingTable = null;
                }
            }
            else
            {
                foreach (XlTable table in this.activeTables)
                {
                    this.FilterRowInTable(row, cells, table);
                }
            }
        }

        private void FilterRowInTable(IXlRow row, IList<XlCell> cells, XlTable table)
        {
            if (table.HasHeaderRow && (row.RowIndex > table.Range.TopLeft.Row))
            {
                IXlFilter[] filterArray = this.BuildFilters(table);
                XlCell[] cellsInFilterRange = this.GetCellsInFilterRange(cells, table.InnerRange);
                IXlCellFormatter exporter = this.exporter as IXlCellFormatter;
                int length = filterArray.Length;
                for (int i = 0; i < length; i++)
                {
                    IXlFilter filter = filterArray[i];
                    if ((filter != null) && !filter.MeetCriteria(cellsInFilterRange[i], exporter))
                    {
                        row.IsHidden = true;
                        return;
                    }
                }
            }
        }

        internal XlExpression GetCalculatedColumnExpression(int columnIndex, int rowIndex)
        {
            XlExpression expression;
            if (this.activeTables.Count == 0)
            {
                return null;
            }
            if ((this.lastTable != null) && this.lastTable.Contains(columnIndex, rowIndex))
            {
                return this.lastTable.GetCalculatedColumnExpression(this.exporter, columnIndex, rowIndex);
            }
            this.lastTable = null;
            using (List<XlTable>.Enumerator enumerator = this.activeTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTable current = enumerator.Current;
                        if (!current.Contains(columnIndex, rowIndex))
                        {
                            continue;
                        }
                        this.lastTable = current;
                        expression = this.lastTable.GetCalculatedColumnExpression(this.exporter, columnIndex, rowIndex);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return expression;
        }

        private XlCell[] GetCellsInFilterRange(IList<XlCell> cells, XlCellRange filterRange)
        {
            int firstColumn = filterRange.FirstColumn;
            int columnCount = filterRange.ColumnCount;
            XlCell[] cellArray = new XlCell[columnCount];
            foreach (XlCell cell in cells)
            {
                int index = cell.ColumnIndex - firstColumn;
                if ((index >= 0) && (index < columnCount))
                {
                    cellArray[index] = cell;
                }
            }
            return cellArray;
        }

        internal IEnumerable<int> GetCellsToCreate(XlCellRange range)
        {
            if (this.activeTables.Count == 0)
            {
                return null;
            }
            List<int> result = new List<int>();
            foreach (XlTable table in this.activeTables)
            {
                table.GetCellsToCreate(result, range);
            }
            return result;
        }

        internal XlDifferentialFormatting GetDifferentialFormat(int columnIndex, int rowIndex)
        {
            XlDifferentialFormatting differentialFormat;
            if (this.activeTables.Count == 0)
            {
                return null;
            }
            if ((this.lastTable != null) && this.lastTable.Contains(columnIndex, rowIndex))
            {
                return this.lastTable.GetDifferentialFormat(columnIndex, rowIndex);
            }
            this.lastTable = null;
            using (List<XlTable>.Enumerator enumerator = this.activeTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTable current = enumerator.Current;
                        if (!current.Contains(columnIndex, rowIndex))
                        {
                            continue;
                        }
                        this.lastTable = current;
                        differentialFormat = this.lastTable.GetDifferentialFormat(columnIndex, rowIndex);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return differentialFormat;
        }

        public bool HasIntersectionWithTable(XlCellRange range)
        {
            bool flag;
            using (IEnumerator<XlTable> enumerator = this.InnerTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTable current = enumerator.Current;
                        if (!current.HasIntersection(range))
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

        public bool IsValidRange(XlCellRange range)
        {
            int num = (this.exporter != null) ? (this.exporter.DocumentOptions.MaxColumnCount - 1) : 0x3fff;
            return (range.BottomRight.Column <= num);
        }

        private float MeasureCharacterWidth(char character, Graphics graph, Font font, StringFormat sf) => 
            graph.MeasureString(new string(character, 1), font, 0x7fffffff, sf).Width;

        internal void RegisterCellPosition(IXlCell cell)
        {
            if (cell != null)
            {
                this.leftColumnIndex = (this.leftColumnIndex >= 0) ? Math.Min(this.leftColumnIndex, cell.ColumnIndex) : cell.ColumnIndex;
                this.rightColumnIndex = (this.rightColumnIndex >= 0) ? Math.Max(this.rightColumnIndex, cell.ColumnIndex) : cell.ColumnIndex;
                this.topRowIndex = (this.topRowIndex >= 0) ? Math.Min(this.topRowIndex, cell.RowIndex) : cell.RowIndex;
                if (this.bottomRowIndex < 0)
                {
                    this.bottomRowIndex = cell.RowIndex;
                }
                else
                {
                    this.bottomRowIndex = Math.Max(this.bottomRowIndex, cell.RowIndex);
                }
            }
        }

        internal void RegisterColumnIndex(IXlColumn column)
        {
            if (column != null)
            {
                this.firstColumnIndex = (this.firstColumnIndex >= 0) ? Math.Min(this.firstColumnIndex, column.ColumnIndex) : column.ColumnIndex;
                if (this.lastColumnIndex < 0)
                {
                    this.lastColumnIndex = column.ColumnIndex;
                }
                else
                {
                    this.lastColumnIndex = Math.Max(this.lastColumnIndex, column.ColumnIndex);
                }
            }
        }

        public void SetPendingTable(XlTable table)
        {
            this.pendingTable = table;
        }

        public void SkipColumns(int count)
        {
            this.exporter.SkipColumns(count);
        }

        public void SkipRows(int count)
        {
            this.exporter.SkipRows(count);
        }

        public string Name
        {
            get => 
                this.name;
            set
            {
                this.CheckSheetName(value);
                this.name = value;
            }
        }

        public IXlMergedCells MergedCells =>
            this.mergedCells;

        public XlCellPosition SplitPosition { get; set; }

        public XlCellRange AutoFilterRange { get; set; }

        public IXlFilterColumns AutoFilterColumns =>
            this.autoFilterColumns;

        public float DefaultMaxDigitWidthInPixels { get; private set; }

        public float DefaultColumnWidthInPixels { get; private set; }

        public float DefaultRowHeightInPixels { get; private set; }

        public IList<XlConditionalFormatting> ConditionalFormattings =>
            this.conditionalFormattings;

        public IList<XlDataValidation> DataValidations =>
            this.dataValidations;

        public XlSheetVisibleState VisibleState { get; set; }

        public XlPageMargins PageMargins { get; set; }

        public XlPageSetup PageSetup { get; set; }

        public XlHeaderFooter HeaderFooter =>
            this.headerFooter;

        public XlPrintTitles PrintTitles =>
            this.printTitles;

        public XlCellRange PrintArea { get; set; }

        public XlPrintOptions PrintOptions { get; set; }

        public IXlPageBreaks ColumnPageBreaks =>
            this.columnPageBreaks;

        public IXlPageBreaks RowPageBreaks =>
            this.rowPageBreaks;

        public IList<XlHyperlink> Hyperlinks =>
            this.hyperlinks;

        public XlCellRange DataRange =>
            (this.leftColumnIndex >= 0) ? new XlCellRange(new XlCellPosition(this.leftColumnIndex, this.topRowIndex), new XlCellPosition(this.rightColumnIndex, this.bottomRowIndex)) : null;

        public XlCellRange ColumnRange =>
            (this.firstColumnIndex >= 0) ? new XlCellRange(new XlCellPosition(this.firstColumnIndex, -1), new XlCellPosition(this.lastColumnIndex, -1)) : null;

        public DevExpress.Export.Xl.XlIgnoreErrors IgnoreErrors { get; set; }

        public IXlOutlineProperties OutlineProperties =>
            this;

        protected internal IList<XlTable> InnerTables =>
            this.innerTables;

        public IXlTableCollection Tables =>
            this.tables;

        public IXlSheetViewOptions ViewOptions =>
            this;

        public IList<XlSparklineGroup> SparklineGroups =>
            this.sparklineGroups;

        internal bool HasActiveTables =>
            this.activeTables.Count > 0;

        internal bool HasActiveAutoFilter =>
            this.hasActiveAutoFilter;

        public IXlSheetSelection Selection =>
            this;

        bool IXlOutlineProperties.SummaryBelow
        {
            get => 
                this.summaryBelow;
            set => 
                this.summaryBelow = value;
        }

        bool IXlOutlineProperties.SummaryRight
        {
            get => 
                this.summaryRight;
            set => 
                this.summaryRight = value;
        }

        public int CurrentOutlineLevel =>
            this.exporter.CurrentOutlineLevel;

        public int CurrentRowIndex =>
            this.exporter.CurrentRowIndex;

        public int CurrentColumnIndex =>
            this.exporter.CurrentColumnIndex;

        bool IXlSheetViewOptions.ShowFormulas
        {
            get => 
                this.showFormulas;
            set => 
                this.showFormulas = value;
        }

        bool IXlSheetViewOptions.ShowGridLines
        {
            get => 
                this.showGridLines;
            set => 
                this.showGridLines = value;
        }

        bool IXlSheetViewOptions.ShowRowColumnHeaders
        {
            get => 
                this.showRowColumnHeaders;
            set => 
                this.showRowColumnHeaders = value;
        }

        bool IXlSheetViewOptions.ShowZeroValues
        {
            get => 
                this.showZeroValues;
            set => 
                this.showZeroValues = value;
        }

        bool IXlSheetViewOptions.ShowOutlineSymbols
        {
            get => 
                this.showOutlineSymbols;
            set => 
                this.showOutlineSymbols = value;
        }

        bool IXlSheetViewOptions.RightToLeft
        {
            get => 
                this.rightToLeft;
            set => 
                this.rightToLeft = value;
        }

        IList<XlCellRange> IXlSheetSelection.SelectedRanges =>
            this.selectedRanges;

        XlCellPosition IXlSheetSelection.ActiveCell { get; set; }
    }
}

