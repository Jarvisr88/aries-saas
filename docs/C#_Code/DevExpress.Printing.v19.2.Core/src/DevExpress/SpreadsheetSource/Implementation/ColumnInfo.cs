namespace DevExpress.SpreadsheetSource.Implementation
{
    using System;
    using System.Runtime.CompilerServices;

    public class ColumnInfo
    {
        public ColumnInfo(int firstIndex, int lastIndex, bool isHidden, int formatIndex)
        {
            this.FirstIndex = firstIndex;
            this.LastIndex = lastIndex;
            this.IsHidden = isHidden;
            this.FormatIndex = formatIndex;
        }

        public int FirstIndex { get; private set; }

        public int LastIndex { get; private set; }

        public bool IsHidden { get; private set; }

        public int FormatIndex { get; private set; }
    }
}

