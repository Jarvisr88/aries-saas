namespace DevExpress.Office.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class DrawingTableInfo
    {
        public DrawingTableInfo()
        {
        }

        public DrawingTableInfo(int index, int refCount)
        {
            this.Index = index;
            this.RefCount = refCount;
        }

        public int Index { get; set; }

        public int RefCount { get; set; }
    }
}

