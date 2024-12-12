namespace DevExpress.SpreadsheetSource.Xls
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlsRow
    {
        private List<long> cellStreamPositions;

        public XlsRow(int rowIndex, int firstColumnIndex, int lastColumnIndex, int formatIndex, bool isHidden)
        {
            this.Index = rowIndex;
            this.FirstColumnIndex = firstColumnIndex;
            this.LastColumnIndex = lastColumnIndex;
            this.FormatIndex = formatIndex;
            this.IsHidden = isHidden;
        }

        internal void RegisterColumnIndexes(int firstColumnIndex, int lastColumnIndex)
        {
            this.FirstColumnIndex = Math.Min(this.FirstColumnIndex, firstColumnIndex);
            this.LastColumnIndex = Math.Max(this.LastColumnIndex, lastColumnIndex);
        }

        public int Index { get; private set; }

        public int FirstColumnIndex { get; private set; }

        public int LastColumnIndex { get; private set; }

        public int FormatIndex { get; private set; }

        public bool IsHidden { get; private set; }

        internal int RecordIndex { get; set; }

        internal bool HasCellStreamPositions =>
            this.cellStreamPositions != null;

        internal IList<long> CellStreamPositions
        {
            get
            {
                this.cellStreamPositions ??= new List<long>();
                return this.cellStreamPositions;
            }
        }
    }
}

