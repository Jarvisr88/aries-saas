namespace DevExpress.Xpf.Grid.EditForm
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EditFormLayoutSettings
    {
        private readonly int columnCount;
        private readonly int rowCount;
        public static readonly EditFormLayoutSettings Empty;
        public EditFormLayoutSettings(int columnCount, int rowCount)
        {
            this.columnCount = columnCount;
            this.rowCount = rowCount;
        }

        public int ColumnCount =>
            this.columnCount;
        public int RowCount =>
            this.rowCount;
        static EditFormLayoutSettings()
        {
            Empty = new EditFormLayoutSettings(0, 0);
        }
    }
}

