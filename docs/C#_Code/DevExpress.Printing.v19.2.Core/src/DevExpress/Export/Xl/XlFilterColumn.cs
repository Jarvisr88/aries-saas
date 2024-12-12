namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlFilterColumn
    {
        public XlFilterColumn(int columnId)
        {
            Guard.ArgumentNonNegative(columnId, "columnId");
            this.ColumnId = columnId;
        }

        public XlFilterColumn(int columnId, IXlFilterCriteria filterCriteria)
        {
            Guard.ArgumentNonNegative(columnId, "columnId");
            this.ColumnId = columnId;
            this.FilterCriteria = filterCriteria;
        }

        public int ColumnId { get; private set; }

        public bool HiddenButton { get; set; }

        public IXlFilterCriteria FilterCriteria { get; set; }
    }
}

