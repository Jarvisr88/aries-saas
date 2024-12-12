namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Runtime.CompilerServices;

    public class Cell : ICell
    {
        internal Cell(int fieldIndex, XlVariantValue value) : this(fieldIndex, value, fieldIndex, 0)
        {
        }

        public Cell(int fieldIndex, XlVariantValue value, int columnIndex, int formatIndex)
        {
            this.FieldIndex = fieldIndex;
            this.Value = value;
            this.ColumnIndex = columnIndex;
            this.FormatIndex = formatIndex;
        }

        public int FieldIndex { get; private set; }

        public XlVariantValue Value { get; private set; }

        public int ColumnIndex { get; private set; }

        public int FormatIndex { get; private set; }
    }
}

