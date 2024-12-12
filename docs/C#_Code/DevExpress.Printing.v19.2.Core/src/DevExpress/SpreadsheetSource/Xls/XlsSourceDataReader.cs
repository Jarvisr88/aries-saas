namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlsSourceDataReader : SpreadsheetDataReaderBase
    {
        private const int defaultCellFormatIndex = 15;
        private XlsSpreadsheetSource contentBuilder;
        private XlReader workbookReader;
        private IXlsSourceCommandFactory commandFactory;
        private readonly List<long> dbCellPositions = new List<long>();
        private readonly List<int> cellOffsets = new List<int>();
        private int currentRowBlockIndex = -1;
        private int firstRowInRangeIndex = -1;
        private int lastRowInRangeIndex = -1;
        private readonly Dictionary<int, XlsRow> rows = new Dictionary<int, XlsRow>();
        private int firstColumnIndex = -1;
        private int lastColumnIndex = -1;
        private readonly Dictionary<int, int> numberOfHiddenColumns = new Dictionary<int, int>();
        private long lastRecordPosition;

        public XlsSourceDataReader(XlsSpreadsheetSource contentBuilder)
        {
            this.contentBuilder = contentBuilder;
            this.workbookReader = contentBuilder.WorkbookReader;
            this.commandFactory = contentBuilder.CommandFactory;
        }

        protected internal void AddRow(XlsRow row)
        {
            row.RecordIndex = this.rows.Count;
            if (!this.rows.ContainsKey(row.Index))
            {
                this.rows.Add(row.Index, row);
            }
            if (this.Stage == XlsSourceReaderStage.Index)
            {
                this.FirstRowIndex = Math.Min(this.FirstRowIndex, row.Index);
                this.LastRowIndex = Math.Max(this.LastRowIndex, row.Index + 1);
            }
        }

        private int CalculateFirstCellOffset(XlsRow row)
        {
            int num = 0;
            for (int i = 0; i <= row.RecordIndex; i++)
            {
                num += this.cellOffsets[i];
            }
            return num;
        }

        private void CalculateRowRange()
        {
            if (this.FirstRowIndex == this.LastRowIndex)
            {
                this.firstRowInRangeIndex = -1;
                this.lastRowInRangeIndex = -1;
            }
            else if ((base.Range == null) || base.Range.TopLeft.IsColumn)
            {
                this.firstRowInRangeIndex = this.SkipEmptyRows ? this.FirstRowIndex : 0;
                this.lastRowInRangeIndex = this.LastRowIndex - 1;
            }
            else
            {
                this.firstRowInRangeIndex = this.SkipEmptyRows ? Math.Max(this.FirstRowIndex, base.Range.TopLeft.Row) : base.Range.TopLeft.Row;
                this.lastRowInRangeIndex = this.SkipEmptyRows ? Math.Min(this.LastRowIndex - 1, base.Range.BottomRight.Row) : base.Range.BottomRight.Row;
                if (this.lastRowInRangeIndex < this.firstRowInRangeIndex)
                {
                    this.firstRowInRangeIndex = -1;
                    this.lastRowInRangeIndex = -1;
                }
            }
        }

        private bool CheckRowFirstCellPositions(int rowBlockIndex)
        {
            bool flag;
            if (this.rows.Count != this.cellOffsets.Count)
            {
                return false;
            }
            long position = this.workbookReader.Position;
            try
            {
                using (Dictionary<int, XlsRow>.ValueCollection.Enumerator enumerator = this.rows.Values.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        XlsRow current = enumerator.Current;
                        this.workbookReader.Position = this.SecondRowPosition + this.CalculateFirstCellOffset(current);
                        IXlsSourceCommand command = this.commandFactory.CreateCommand(this.workbookReader);
                        if (!(command is XlsSourceCommandCellBase))
                        {
                            return false;
                        }
                    }
                }
                flag = true;
            }
            finally
            {
                this.workbookReader.Position = position;
            }
            return flag;
        }

        public override void Close()
        {
            if ((this.contentBuilder != null) && (this.contentBuilder.ContentType == XlsContentType.Sheet))
            {
                this.contentBuilder.EndContent();
            }
            this.contentBuilder = null;
            this.workbookReader = null;
            this.commandFactory = null;
            base.Close();
        }

        private void DetectRowFirstCellPositions(int rowBlockIndex)
        {
            this.Stage = XlsSourceReaderStage.Index;
            try
            {
                long num = Math.Min(this.dbCellPositions[rowBlockIndex], this.workbookReader.StreamLength);
                while (this.workbookReader.Position < num)
                {
                    this.lastRecordPosition = this.workbookReader.Position;
                    IXlsSourceCommand command = this.commandFactory.CreateCommand(this.workbookReader);
                    command.Read(this.workbookReader, this.contentBuilder);
                    command.Execute(this);
                }
            }
            finally
            {
                this.Stage = XlsSourceReaderStage.Data;
            }
        }

        internal void EndContent()
        {
            this.contentBuilder.ClearDataCollectors();
            this.contentBuilder.EndContent();
        }

        private int GetHiddenColumnsCount(int firstColumnIndex, int lastColumnIndex)
        {
            if (this.numberOfHiddenColumns.ContainsKey(lastColumnIndex))
            {
                return this.numberOfHiddenColumns[lastColumnIndex];
            }
            int num = 0;
            for (int i = firstColumnIndex; i < lastColumnIndex; i++)
            {
                if (base.Columns.IsColumnHidden(i))
                {
                    num++;
                }
            }
            this.numberOfHiddenColumns[lastColumnIndex] = num;
            return num;
        }

        protected internal string GetSharedString(int index) => 
            this.contentBuilder.SharedStrings[index];

        private bool MoveToNextRow()
        {
            if (base.CurrentRowIndex == -1)
            {
                base.CurrentRowIndex = this.firstRowInRangeIndex;
                if (base.CurrentRowIndex == -1)
                {
                    return false;
                }
            }
            else
            {
                int currentRowIndex = base.CurrentRowIndex;
                base.CurrentRowIndex = currentRowIndex + 1;
                if (base.CurrentRowIndex > this.lastRowInRangeIndex)
                {
                    return false;
                }
            }
            int rowBlockIndex = (base.CurrentRowIndex - this.FirstRowIndex) / 0x20;
            if (rowBlockIndex != this.currentRowBlockIndex)
            {
                this.ReadRowBlock(rowBlockIndex);
                this.currentRowBlockIndex = rowBlockIndex;
            }
            return true;
        }

        public override void Open(IWorksheet worksheet, XlCellRange range)
        {
            base.Open(worksheet, range);
            this.ReadBOF();
            if (this.ReadWorksheetIndex())
            {
                this.ReadColumns();
            }
            else
            {
                this.ReadBOF();
            }
            this.CalculateRowRange();
        }

        private void ReadBOF()
        {
            XlsWorksheet worksheet = base.Worksheet as XlsWorksheet;
            if (worksheet == null)
            {
                throw new InvalidOperationException("Can't setup worksheet substream position.");
            }
            this.workbookReader.Position = worksheet.StartPosition;
            XlsSourceCommandBOF dbof = this.commandFactory.CreateCommand(this.workbookReader) as XlsSourceCommandBOF;
            if (dbof == null)
            {
                throw new InvalidFileException(InvalidFileError.CorruptedFile, "Corrupted XLS file. Can't read worksheet substream BOF.");
            }
            dbof.Read(this.workbookReader, this.contentBuilder);
            dbof.Execute(this.contentBuilder);
        }

        private bool ReadCells()
        {
            XlsRow row;
            this.firstColumnIndex = -1;
            this.lastColumnIndex = -1;
            if (this.rows.TryGetValue(base.CurrentRowIndex, out row))
            {
                if (row.FirstColumnIndex == row.LastColumnIndex)
                {
                    return true;
                }
                if (this.SkipHiddenRows && row.IsHidden)
                {
                    return false;
                }
                this.firstColumnIndex = row.FirstColumnIndex;
                this.lastColumnIndex = row.LastColumnIndex - 1;
                if ((base.Range != null) && !base.Range.TopLeft.IsRow)
                {
                    this.firstColumnIndex = Math.Max(this.firstColumnIndex, base.Range.TopLeft.Column);
                    this.lastColumnIndex = Math.Min(this.lastColumnIndex, base.Range.BottomRight.Column);
                    if (this.lastColumnIndex < this.firstColumnIndex)
                    {
                        return true;
                    }
                }
                this.ReadCells(row);
            }
            return true;
        }

        private void ReadCells(XlsRow row)
        {
            if (!row.HasCellStreamPositions)
            {
                long streamLength = this.workbookReader.StreamLength;
                if (this.HasIndexRecord && (this.rows.Count == this.cellOffsets.Count))
                {
                    this.workbookReader.Position = this.SecondRowPosition + this.CalculateFirstCellOffset(row);
                    int num3 = (base.CurrentRowIndex - this.FirstRowIndex) / 0x20;
                    streamLength = Math.Min(this.dbCellPositions[num3], streamLength);
                    while ((this.workbookReader.Position < streamLength) && (this.contentBuilder.ContentType == XlsContentType.Sheet))
                    {
                        IXlsSourceCommand command2 = this.commandFactory.CreateCommand(this.workbookReader);
                        command2.Read(this.workbookReader, this.contentBuilder);
                        XlsSourceCommandCellBase base3 = command2 as XlsSourceCommandCellBase;
                        if (base3 != null)
                        {
                            if (base3.RowIndex != base.CurrentRowIndex)
                            {
                                return;
                            }
                            base3.Execute(this);
                            continue;
                        }
                        if ((command2 is XlsSourceCommandString) || (command2 is XlsSourceCommandContinue))
                        {
                            command2.Execute(this.contentBuilder);
                            continue;
                        }
                        if (command2.IsSubstreamBound)
                        {
                            command2.Execute(this.contentBuilder);
                        }
                    }
                }
            }
            else
            {
                foreach (long num in row.CellStreamPositions)
                {
                    this.workbookReader.Position = num;
                    IXlsSourceCommand command = this.commandFactory.CreateCommand(this.workbookReader);
                    command.Read(this.workbookReader, this.contentBuilder);
                    XlsSourceCommandCellBase base2 = command as XlsSourceCommandCellBase;
                    if (base2 != null)
                    {
                        base2.Execute(this);
                        continue;
                    }
                    if ((command is XlsSourceCommandString) || (command is XlsSourceCommandContinue))
                    {
                        command.Execute(this.contentBuilder);
                        continue;
                    }
                    if (command.IsSubstreamBound)
                    {
                        command.Execute(this.contentBuilder);
                    }
                }
            }
        }

        private void ReadColumns()
        {
            if (!this.SeekToDefaultColumnWidth())
            {
                throw new InvalidFileException(InvalidFileError.CorruptedFile, "Corrupted XLS file. Can't read DefColumnWidth.");
            }
            while ((this.workbookReader.Position != this.workbookReader.StreamLength) && (this.contentBuilder.ContentType == XlsContentType.Sheet))
            {
                XlsSourceCommandColumnInfo info = this.commandFactory.CreateCommand(this.workbookReader) as XlsSourceCommandColumnInfo;
                if (info == null)
                {
                    return;
                }
                info.Read(this.workbookReader, this.contentBuilder);
                info.Execute(this);
            }
        }

        protected override bool ReadCore()
        {
            while (this.MoveToNextRow())
            {
                if (this.ReadCells() && (!this.SkipEmptyRows || (base.ExistingCells.Count > 0)))
                {
                    return true;
                }
            }
            return false;
        }

        private void ReadDbCell(int rowBlockIndex)
        {
            if ((rowBlockIndex >= 0) && (rowBlockIndex < this.dbCellPositions.Count))
            {
                this.workbookReader.Position = this.dbCellPositions[rowBlockIndex];
                XlsSourceCommandDbCell cell = this.commandFactory.CreateCommand(this.workbookReader) as XlsSourceCommandDbCell;
                if (cell == null)
                {
                    throw new InvalidFileException(InvalidFileError.CorruptedFile, "Corrupted XLS file. Can't read row block index.");
                }
                cell.Read(this.workbookReader, this.contentBuilder);
                cell.Execute(this);
            }
        }

        private void ReadRowBlock(int rowBlockIndex)
        {
            if (this.HasIndexRecord)
            {
                this.FirstRowOffset = 0L;
                this.rows.Clear();
                this.ReadDbCell(rowBlockIndex);
                if (this.FirstRowOffset > 0L)
                {
                    this.ReadRows(rowBlockIndex);
                    if (!this.CheckRowFirstCellPositions(rowBlockIndex))
                    {
                        this.DetectRowFirstCellPositions(rowBlockIndex);
                    }
                }
            }
        }

        private void ReadRows(int rowBlockIndex)
        {
            this.workbookReader.Position = this.dbCellPositions[rowBlockIndex] - this.FirstRowOffset;
            while ((this.workbookReader.Position != this.workbookReader.StreamLength) && (this.contentBuilder.ContentType == XlsContentType.Sheet))
            {
                this.lastRecordPosition = this.workbookReader.Position;
                XlsSourceCommandRow row = this.commandFactory.CreateCommand(this.workbookReader) as XlsSourceCommandRow;
                if (row == null)
                {
                    this.workbookReader.Position = this.lastRecordPosition;
                    return;
                }
                row.Read(this.workbookReader, this.contentBuilder);
                row.Execute(this);
                if (this.rows.Count == 1)
                {
                    this.SecondRowPosition = this.workbookReader.Position;
                }
            }
        }

        private bool ReadToDefaultColumnWidth(long startPosition)
        {
            this.contentBuilder.WorkbookReader.Position = startPosition;
            while (this.workbookReader.Position != this.workbookReader.StreamLength)
            {
                IXlsSourceCommand command = this.commandFactory.CreateCommand(this.workbookReader);
                command.Read(this.workbookReader, this.contentBuilder);
                if (command is XlsSourceCommandDefaultColumnWidth)
                {
                    return true;
                }
                if (command is XlsSourceCommandEOF)
                {
                    return false;
                }
            }
            return false;
        }

        private bool ReadWorksheetIndex()
        {
            bool flag;
            this.Stage = XlsSourceReaderStage.Index;
            try
            {
                while (true)
                {
                    if ((this.workbookReader.Position == this.workbookReader.StreamLength) || (this.contentBuilder.ContentType != XlsContentType.Sheet))
                    {
                        flag = false;
                    }
                    else
                    {
                        this.lastRecordPosition = this.workbookReader.Position;
                        IXlsSourceCommand command = this.commandFactory.CreateCommand(this.workbookReader);
                        command.Read(this.workbookReader, this.contentBuilder);
                        command.Execute(this);
                        if (!this.HasIndexRecord)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    break;
                }
            }
            finally
            {
                this.Stage = XlsSourceReaderStage.Data;
            }
            return flag;
        }

        internal void RegisterCell(int rowIndex, int firstColumnIndex, int lastColumnIndex)
        {
            XlsRow row;
            if (this.rows.TryGetValue(rowIndex, out row))
            {
                row.CellStreamPositions.Add(this.lastRecordPosition);
                row.RegisterColumnIndexes(firstColumnIndex, lastColumnIndex + 1);
            }
            else
            {
                row = new XlsRow(rowIndex, firstColumnIndex, lastColumnIndex + 1, 15, false);
                this.rows.Add(rowIndex, row);
                row.CellStreamPositions.Add(this.lastRecordPosition);
                this.FirstRowIndex = Math.Min(this.FirstRowIndex, row.Index);
                this.LastRowIndex = Math.Max(this.LastRowIndex, row.Index + 1);
            }
        }

        private bool SeekToDefaultColumnWidth()
        {
            XlsWorksheet worksheet = base.Worksheet as XlsWorksheet;
            if (this.DefaultColumnWidthOffset < worksheet.StartPosition)
            {
                return this.ReadToDefaultColumnWidth((long) worksheet.StartPosition);
            }
            this.contentBuilder.WorkbookReader.Position = this.DefaultColumnWidthOffset;
            XlsSourceCommandDefaultColumnWidth width = this.commandFactory.CreateCommand(this.workbookReader) as XlsSourceCommandDefaultColumnWidth;
            if (width == null)
            {
                return this.ReadToDefaultColumnWidth((long) worksheet.StartPosition);
            }
            width.Read(this.workbookReader, this.contentBuilder);
            return true;
        }

        internal void StartContent(XlsSubstreamType substreamType)
        {
            this.contentBuilder.StartContent(substreamType);
        }

        protected internal int TranslateColumnIndex(int columnIndex)
        {
            if ((columnIndex < this.firstColumnIndex) || (columnIndex > this.lastColumnIndex))
            {
                return -1;
            }
            if (!this.SkipHiddenColumns)
            {
                if ((base.Range != null) && !base.Range.TopLeft.IsRow)
                {
                    if ((columnIndex < base.Range.TopLeft.Column) || (columnIndex > base.Range.BottomRight.Column))
                    {
                        return -1;
                    }
                    columnIndex -= base.Range.TopLeft.Column;
                }
            }
            else if ((base.Range != null) && !base.Range.TopLeft.IsRow)
            {
                if ((columnIndex < base.Range.TopLeft.Column) || ((columnIndex > base.Range.BottomRight.Column) || base.Columns.IsColumnHidden(columnIndex)))
                {
                    return -1;
                }
                columnIndex -= base.Range.TopLeft.Column + this.GetHiddenColumnsCount(base.Range.TopLeft.Column, columnIndex);
            }
            else
            {
                if (base.Columns.IsColumnHidden(columnIndex))
                {
                    return -1;
                }
                columnIndex -= this.GetHiddenColumnsCount(0, columnIndex);
            }
            return columnIndex;
        }

        protected internal int FirstRowIndex { get; set; }

        protected internal int LastRowIndex { get; set; }

        protected internal long DefaultColumnWidthOffset { get; set; }

        protected internal List<long> DbCellPositions =>
            this.dbCellPositions;

        protected internal long FirstRowOffset { get; set; }

        protected internal long SecondRowPosition { get; set; }

        protected internal List<int> CellOffsets =>
            this.cellOffsets;

        private bool SkipEmptyRows =>
            this.contentBuilder.Options.SkipEmptyRows;

        private bool SkipHiddenColumns =>
            this.contentBuilder.Options.SkipHiddenColumns;

        private bool SkipHiddenRows =>
            this.contentBuilder.Options.SkipHiddenRows;

        protected internal XlsSourceReaderStage Stage { get; set; }

        private bool HasIndexRecord =>
            this.DefaultColumnWidthOffset > 0L;

        protected override List<int> NumberFormatIds =>
            this.contentBuilder.NumberFormatIds;

        protected override Dictionary<int, string> NumberFormatCodes =>
            this.contentBuilder.NumberFormatCodes;

        protected override bool UseDate1904 =>
            this.contentBuilder.IsDate1904;

        protected override int DefaultCellFormatIndex =>
            15;
    }
}

