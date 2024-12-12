namespace DevExpress.Office.Drawing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RowHeightInfo
    {
        public RowHeightInfo(int rowHeight, int emHeight)
        {
            this.<RowHeight>k__BackingField = rowHeight;
            this.<EmHeight>k__BackingField = emHeight;
        }

        public RowHeightInfo(int height)
        {
            this.<RowHeight>k__BackingField = height;
            this.<EmHeight>k__BackingField = height;
        }

        public int RowHeight { get; }
        public int EmHeight { get; }
    }
}

