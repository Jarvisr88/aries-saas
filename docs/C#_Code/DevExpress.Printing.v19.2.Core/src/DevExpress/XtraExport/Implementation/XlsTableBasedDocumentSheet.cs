namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsTableBasedDocumentSheet : XlSheet, IDisposable
    {
        private List<long> dbCellsPositions;
        private ChunkedMemoryStream cellTableStream;
        private BinaryWriter cellTableWriter;
        private int columnIndex;
        private List<XlsTableColumn> columns;
        private Dictionary<int, IXlColumn> columnsTable;
        private List<XlDrawingObjectBase> drawingObjects;
        private readonly Dictionary<int, int> rowHeights;

        public XlsTableBasedDocumentSheet(IXlExport exporter) : base(exporter)
        {
            this.dbCellsPositions = new List<long>();
            this.columns = new List<XlsTableColumn>();
            this.columnsTable = new Dictionary<int, IXlColumn>();
            this.drawingObjects = new List<XlDrawingObjectBase>();
            this.rowHeights = new Dictionary<int, int>();
        }

        protected override void AddShapeCore(XlShape shape)
        {
            if ((shape.GeometryPreset == XlGeometryPreset.Line) || (shape.GeometryPreset == XlGeometryPreset.Rect))
            {
                this.drawingObjects.Add(shape);
            }
        }

        public XlsTableColumn CreateXlsColumn() => 
            new XlsTableColumn(this) { ColumnIndex = this.columnIndex };

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.cellTableWriter != null)
                {
                    this.cellTableWriter.Dispose();
                    this.cellTableWriter = null;
                }
                if (this.cellTableStream != null)
                {
                    this.cellTableStream.Dispose();
                    this.cellTableStream = null;
                }
                this.Dimensions = null;
                this.dbCellsPositions = null;
                this.columns = null;
                this.columnsTable = null;
                this.drawingObjects = null;
            }
            base.Dispose(disposing);
        }

        public BinaryWriter GetCellTableWriter()
        {
            if (this.cellTableWriter == null)
            {
                this.cellTableStream = new ChunkedMemoryStream();
                this.cellTableWriter = new BinaryWriter(this.cellTableStream);
            }
            return this.cellTableWriter;
        }

        internal bool HasAutoFilterCriteria()
        {
            bool flag;
            if ((base.AutoFilterRange != null) && (base.AutoFilterColumns.Count > 0))
            {
                return true;
            }
            using (IEnumerator<XlTable> enumerator = base.InnerTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTable current = enumerator.Current;
                        if (!current.HasHeaderRow || !current.HasAutoFilter)
                        {
                            continue;
                        }
                        using (IEnumerator<XlTableColumn> enumerator2 = current.InnerColumns.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator2.MoveNext())
                                {
                                    break;
                                }
                                XlTableColumn column = enumerator2.Current;
                                if ((column.FilterCriteria != null) || column.HiddenButton)
                                {
                                    return true;
                                }
                            }
                        }
                        continue;
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

        public void RegisterColumn(XlsTableColumn column)
        {
            if (this.columnsTable.ContainsKey(column.ColumnIndex))
            {
                throw new InvalidOperationException("Column with such index already exists.");
            }
            base.RegisterColumnIndex(column);
            this.columns.Add(column);
            this.columnsTable[column.ColumnIndex] = column;
            this.columnIndex = column.ColumnIndex + 1;
        }

        public void RegisterRow(XlsTableRow row)
        {
            int heightInPixels = row.HeightInPixels;
            if (heightInPixels < 0)
            {
                heightInPixels = row.AutomaticHeightInPixels;
            }
            if (row.IsHidden)
            {
                heightInPixels = 0;
            }
            if (heightInPixels >= 0)
            {
                this.rowHeights.Add(row.RowIndex, heightInPixels);
            }
        }

        public int SheetId { get; set; }

        public List<XlsTableColumn> Columns =>
            this.columns;

        public Dictionary<int, IXlColumn> ColumnsTable =>
            this.columnsTable;

        public List<XlDrawingObjectBase> DrawingObjects =>
            this.drawingObjects;

        public XlDimensions Dimensions { get; set; }

        public List<long> DbCellsPositions =>
            this.dbCellsPositions;

        public BinaryWriter CellTableWriter =>
            this.cellTableWriter;

        protected internal Dictionary<int, int> RowHeights =>
            this.rowHeights;

        protected internal int ColumnIndex
        {
            get => 
                this.columnIndex;
            set => 
                this.columnIndex = value;
        }
    }
}

