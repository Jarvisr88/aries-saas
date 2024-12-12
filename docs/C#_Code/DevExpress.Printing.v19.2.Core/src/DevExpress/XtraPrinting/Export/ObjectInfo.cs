namespace DevExpress.XtraPrinting.Export
{
    using System;

    public class ObjectInfo
    {
        private int rowIndex;
        private int colIndex;
        private int rowSpan;
        private int colSpan;
        private object obj;

        public ObjectInfo(int colIndex, int rowIndex, int colSpan, int rowSpan, object obj)
        {
            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
            this.rowSpan = rowSpan;
            this.colSpan = colSpan;
            this.obj = obj;
        }

        public int RowIndex =>
            this.rowIndex;

        public int ColIndex =>
            this.colIndex;

        public int RowSpan =>
            this.rowSpan;

        public int ColSpan =>
            this.colSpan;

        public object Object =>
            this.obj;
    }
}

