namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class GroupingInfo
    {
        public GroupingInfo();

        public int[] Path { get; set; }

        public int BeginPageIndex { get; set; }

        public int EndPageIndex { get; set; }
    }
}

