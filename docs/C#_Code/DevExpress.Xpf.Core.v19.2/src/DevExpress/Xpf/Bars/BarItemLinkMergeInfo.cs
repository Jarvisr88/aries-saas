namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemLinkMergeInfo
    {
        public BarItemLinkMergeInfo(BarItemLinkBase link);

        public BarItemLinkBase Link { get; set; }

        public int Index { get; set; }

        public int CollectionIndex { get; set; }
    }
}

