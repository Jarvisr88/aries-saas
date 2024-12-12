namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class BrickInfo
    {
        public BrickInfo(string brickTag, int pageIndex)
        {
            this.BrickTag = brickTag;
            this.PageIndex = pageIndex;
        }

        public string BrickTag { get; private set; }

        public int PageIndex { get; private set; }

        public static BrickInfo Empty =>
            new BrickInfo(string.Empty, 0);
    }
}

