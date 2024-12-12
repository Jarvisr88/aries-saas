namespace DevExpress.SpreadsheetSource.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.SpreadsheetSource.Xlsx.Import;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class XlsxSourceDataReader : SpreadsheetDataReaderBase
    {
        private const int defaultCellFormatIndex = 0;
        private XlsxSpreadsheetSourceImporter importer;
        private int fieldOffset;
        private int rowIndexCounter;
        private int lastColumnIndex;
        private XlsxRowAttributes row;

        public XlsxSourceDataReader(XlsxSpreadsheetSourceImporter importer)
        {
            Guard.ArgumentNotNull(importer, "importer");
            this.importer = importer;
        }

        internal void AddCell(int columnIndex, XlVariantValue value, int formatIndex)
        {
            Cell cell = new Cell(columnIndex - this.fieldOffset, value, columnIndex, formatIndex);
            base.AddCell(cell);
        }

        internal bool CanAddCell(int columnIndex)
        {
            if (!this.IsColumnsFitToDataRange(columnIndex, columnIndex))
            {
                return false;
            }
            if (this.SkipHiddenColumns)
            {
                int num = this.lastColumnIndex + 1;
                while (true)
                {
                    ColumnInfo info;
                    if (num >= columnIndex)
                    {
                        this.lastColumnIndex = columnIndex;
                        info = base.Columns.FindColumn(columnIndex);
                        if ((info == null) || !info.IsHidden)
                        {
                            break;
                        }
                        this.fieldOffset++;
                        return false;
                    }
                    info = base.Columns.FindColumn(num);
                    if ((info != null) && info.IsHidden)
                    {
                        this.fieldOffset++;
                    }
                    num++;
                }
            }
            return true;
        }

        internal bool CanAddColumn(int firstIndex, int lastIndex) => 
            this.IsColumnsFitToDataRange(firstIndex, lastIndex);

        public override void Close()
        {
            this.importer.CloseWorksheetReader();
            this.importer = null;
            base.Close();
        }

        private bool IsCellFartherDataRange(int index) => 
            (base.Range != null) && (index > base.Range.LastColumn);

        private bool IsCellNearerDataRange(int index) => 
            (base.Range != null) && (index < base.Range.FirstColumn);

        private bool IsColumnsFitToDataRange(int firsColumntIndex, int lastColumnIndex) => 
            (base.Range != null) ? (((firsColumntIndex < base.Range.FirstColumn) || (firsColumntIndex > base.Range.LastColumn)) ? ((lastColumnIndex >= base.Range.FirstColumn) && (lastColumnIndex <= base.Range.LastColumn)) : true) : true;

        public override void Open(IWorksheet worksheet, XlCellRange range)
        {
            base.Open(worksheet, range);
            XlsxWorksheet worksheet2 = worksheet as XlsxWorksheet;
            if (worksheet2 == null)
            {
                throw new InvalidOperationException("Can't find worksheet relation");
            }
            this.importer.PrepareBeforeReadSheet(worksheet2.RelationId);
            this.importer.ReadWorksheetColumns();
            this.ReadFirstRow();
        }

        private void ReadCells()
        {
            while (this.ReadToNextCell())
            {
                int index = this.ReadCurrentCellIndex();
                if (this.IsCellNearerDataRange(index))
                {
                    this.SkipToNextCell();
                    continue;
                }
                if (this.IsCellFartherDataRange(index))
                {
                    this.SkipToNextRow();
                    return;
                }
                this.ReadCurrentCell();
            }
        }

        protected override bool ReadCore()
        {
            if (this.row == null)
            {
                if ((base.Range == null) || this.SkipEmptyRows)
                {
                    return false;
                }
                int rowIndexCounter = this.rowIndexCounter;
                this.rowIndexCounter = rowIndexCounter + 1;
                return (base.Range.LastRow >= rowIndexCounter);
            }
            this.ResetFieldOffset();
            while (this.SkipEmptyRows || (this.row.Index <= this.rowIndexCounter))
            {
                if ((base.Range != null) && (base.Range.LastRow < this.row.Index))
                {
                    return false;
                }
                if ((base.Range != null) && (base.Range.FirstRow > this.row.Index))
                {
                    this.SkipToNextRow();
                }
                else
                {
                    this.rowIndexCounter++;
                    if (this.SkipHiddenRows && this.row.IsHidden)
                    {
                        this.SkipToNextRow();
                    }
                    else
                    {
                        this.ReadCells();
                        if (!this.SkipEmptyRows || (base.ExistingCells.Count != 0))
                        {
                            this.ReadNextRow();
                            return true;
                        }
                    }
                }
                if (!this.ReadNextRow())
                {
                    return false;
                }
            }
            this.rowIndexCounter++;
            return true;
        }

        private void ReadCurrentCell()
        {
            this.importer.ReadCell();
        }

        private int ReadCurrentCellIndex() => 
            this.importer.ReadCellIndex();

        private XlsxRowAttributes ReadCurrentRow()
        {
            XlsxRowAttributes attributes = this.importer.ReadRowAttributes();
            this.CurrentRowIndex = (attributes != null) ? attributes.Index : -1;
            return attributes;
        }

        private void ReadFirstRow()
        {
            if (this.ReadToNextRow())
            {
                this.row = this.ReadCurrentRow();
                this.rowIndexCounter = (base.Range == null) ? 0 : base.Range.FirstRow;
            }
        }

        private bool ReadNextRow()
        {
            if (this.ReadToNextRow())
            {
                this.row = this.ReadCurrentRow();
                return true;
            }
            this.row = null;
            base.CurrentRowIndex = -1;
            return false;
        }

        private bool ReadToNextCell() => 
            this.importer.ReadToNextCell();

        private bool ReadToNextRow() => 
            this.importer.ReadToNextRow();

        private void ResetFieldOffset()
        {
            this.fieldOffset = (base.Range == null) ? 0 : base.Range.FirstColumn;
            this.lastColumnIndex = (base.Range == null) ? 0 : base.Range.FirstColumn;
        }

        private void SkipToNextCell()
        {
            this.importer.SkipToNextCell();
        }

        private void SkipToNextRow()
        {
            this.importer.SkipToNextRow();
        }

        private bool SkipEmptyRows =>
            this.importer.Source.Options.SkipEmptyRows;

        private bool SkipHiddenColumns =>
            this.importer.Source.Options.SkipHiddenColumns;

        private bool SkipHiddenRows =>
            this.importer.Source.Options.SkipHiddenRows;

        protected override List<int> NumberFormatIds =>
            this.importer.Source.NumberFormatIds;

        protected override Dictionary<int, string> NumberFormatCodes =>
            this.importer.Source.NumberFormatCodes;

        protected override bool UseDate1904 =>
            this.importer.Source.UseDate1904;

        protected override int DefaultCellFormatIndex =>
            0;
    }
}

