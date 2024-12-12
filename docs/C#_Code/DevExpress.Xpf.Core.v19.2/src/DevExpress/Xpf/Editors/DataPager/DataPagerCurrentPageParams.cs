namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DataPagerCurrentPageParams
    {
        public int PageCount;
        public int PageIndex;
    }
}

