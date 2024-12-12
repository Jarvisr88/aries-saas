namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.XtraExport.Csv;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class SpreadsheetDataReaderBase : ISpreadsheetDataReader
    {
        private readonly CellCollection cells = new CellCollection();
        private readonly Dictionary<int, ICell> cellsMap = new Dictionary<int, ICell>();
        private int fieldsCount;
        private readonly ColumnsCollection columns = new ColumnsCollection();
        private XlFormatterFactory formatterFactory;

        protected SpreadsheetDataReaderBase()
        {
            this.CurrentRowIndex = -1;
        }

        internal void AddCell(ICell cell)
        {
            this.cells.Add(cell);
        }

        private void Clear()
        {
            this.cells.Clear();
            this.cellsMap.Clear();
            this.fieldsCount = 0;
        }

        public virtual void Close()
        {
            this.IsClosed = true;
            this.Clear();
            this.columns.Clear();
        }

        public bool GetBoolean(int index) => 
            this.GetValue(index).BooleanValue;

        public DateTime GetDateTime(int index) => 
            this.GetValue(index).GetDateTime();

        protected internal XlVariantValue GetDateTimeOrNumericValue(double value, int formatIndex)
        {
            if (!this.IsDateTimeNumberFormat(formatIndex))
            {
                return value;
            }
            XlVariantValue value2 = new XlVariantValue();
            value2.SetDateTimeSerial(value, this.UseDate1904);
            return value2;
        }

        public string GetDisplayText(int index, CultureInfo culture)
        {
            if ((index < 0) || (index >= this.fieldsCount))
            {
                throw new IndexOutOfRangeException();
            }
            Cell cell = null;
            if (this.cellsMap.ContainsKey(index))
            {
                cell = this.cellsMap[index] as Cell;
            }
            return ((cell != null) ? this.GetDisplayTextCore(cell, culture) : string.Empty);
        }

        protected virtual string GetDisplayTextCore(Cell cell, CultureInfo culture)
        {
            IXlValueFormatter formatter;
            int numberFormatId = this.GetNumberFormatId(cell.FormatIndex, this.DefaultCellFormatIndex);
            if (!this.NumberFormatCodes.ContainsKey(numberFormatId))
            {
                formatter = this.FormatterFactory.CreateFormatter(numberFormatId);
            }
            else
            {
                XlNumberFormat numberFormat = this.NumberFormatCodes[numberFormatId];
                formatter = this.FormatterFactory.CreateFormatter(numberFormat);
            }
            return ((formatter == null) ? cell.Value.ToText(culture).TextValue : formatter.Format(cell.Value, culture));
        }

        public double GetDouble(int index) => 
            this.GetValue(index).NumericValue;

        public XlVariantValueType GetFieldType(int index) => 
            this.GetValue(index).Type;

        private int GetNumberFormatId(int formatIndex, int defaultIndex) => 
            ((formatIndex < 0) || (formatIndex >= this.NumberFormatIds.Count)) ? (((defaultIndex < 0) || (defaultIndex >= this.NumberFormatIds.Count)) ? 0 : this.NumberFormatIds[defaultIndex]) : this.NumberFormatIds[formatIndex];

        public string GetString(int index) => 
            this.GetValue(index).ToText().TextValue;

        public XlVariantValue GetValue(int index)
        {
            ICell cell;
            if ((index < 0) || (index >= this.fieldsCount))
            {
                throw new IndexOutOfRangeException();
            }
            return (!this.cellsMap.TryGetValue(index, out cell) ? XlVariantValue.Empty : cell.Value);
        }

        private bool IsDateTimeNumberFormat(int formatIndex)
        {
            int numberFormatId = this.GetNumberFormatId(formatIndex, this.DefaultCellFormatIndex);
            return (((numberFormatId < 14) || (numberFormatId > 0x16)) ? (((numberFormatId < 0x2d) || (numberFormatId > 0x2f)) ? (this.NumberFormatCodes.ContainsKey(numberFormatId) && this.NumberFormatCodes[numberFormatId].IsDateTime) : true) : true);
        }

        public virtual void Open(IWorksheet worksheet, XlCellRange range)
        {
            this.Worksheet = worksheet;
            this.Range = range;
        }

        private void Prepare()
        {
            foreach (ICell cell in this.cells)
            {
                this.cellsMap.Add(cell.FieldIndex, cell);
                this.fieldsCount = Math.Max(this.fieldsCount, cell.FieldIndex + 1);
            }
        }

        public bool Read()
        {
            if (this.IsClosed)
            {
                return false;
            }
            this.Clear();
            if (!this.ReadCore())
            {
                return false;
            }
            this.Prepare();
            return true;
        }

        protected abstract bool ReadCore();

        public bool IsClosed { get; private set; }

        public int FieldsCount =>
            this.fieldsCount;

        public XlVariantValue this[int index] =>
            this.GetValue(index);

        public ICellCollection ExistingCells =>
            this.cells;

        public IWorksheet Worksheet { get; private set; }

        public XlCellRange Range { get; private set; }

        protected internal ColumnsCollection Columns =>
            this.columns;

        protected int CurrentRowIndex { get; set; }

        protected internal XlFormatterFactory FormatterFactory
        {
            get
            {
                this.formatterFactory ??= new XlFormatterFactory();
                return this.formatterFactory;
            }
        }

        protected abstract Dictionary<int, string> NumberFormatCodes { get; }

        protected abstract List<int> NumberFormatIds { get; }

        protected abstract bool UseDate1904 { get; }

        protected abstract int DefaultCellFormatIndex { get; }
    }
}

