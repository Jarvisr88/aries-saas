namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class XlRowProxy : IXlRow, IDisposable
    {
        private IXlExport exporter;
        private readonly IXlRow subject;

        public XlRowProxy(IXlExport exporter, IXlRow subject)
        {
            this.exporter = exporter;
            this.subject = subject;
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            this.subject.ApplyFormatting(formatting);
        }

        public IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow) => 
            this.subject.BeginTable(columnNames, hasHeaderRow);

        public IXlTable BeginTable(IEnumerable<XlTableColumnInfo> columns, bool hasHeaderRow, XlCellFormatting headerRowFormatting) => 
            this.subject.BeginTable(columns, hasHeaderRow, headerRowFormatting);

        public IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow, XlCellFormatting headerRowFormatting) => 
            this.subject.BeginTable(columnNames, hasHeaderRow, headerRowFormatting);

        public void BlankCells(int count, XlCellFormatting formatting)
        {
            this.subject.BlankCells(count, formatting);
        }

        public void BulkCells(IEnumerable values, XlCellFormatting formatting)
        {
            this.subject.BulkCells(values, formatting);
        }

        public IXlCell CreateCell() => 
            this.subject.CreateCell();

        public IXlCell CreateCell(int columnIndex) => 
            this.subject.CreateCell(columnIndex);

        public void Dispose()
        {
            if (this.exporter != null)
            {
                this.exporter.EndRow();
                this.exporter = null;
            }
        }

        public void EndTable(IXlTable table, bool hasTotalRow)
        {
            this.subject.EndTable(table, hasTotalRow);
        }

        public void SkipCells(int count)
        {
            this.subject.SkipCells(count);
        }

        public int RowIndex =>
            this.subject.RowIndex;

        public XlCellFormatting Formatting
        {
            get => 
                this.subject.Formatting;
            set => 
                this.subject.Formatting = value;
        }

        public bool IsHidden
        {
            get => 
                this.subject.IsHidden;
            set => 
                this.subject.IsHidden = value;
        }

        public bool IsCollapsed
        {
            get => 
                this.subject.IsCollapsed;
            set => 
                this.subject.IsCollapsed = value;
        }

        public int HeightInPixels
        {
            get => 
                this.subject.HeightInPixels;
            set => 
                this.subject.HeightInPixels = value;
        }

        public float HeightInPoints
        {
            get => 
                this.subject.HeightInPoints;
            set => 
                this.subject.HeightInPoints = value;
        }
    }
}

