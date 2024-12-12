namespace DevExpress.XtraPrinting.Native.Navigation
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchData : ImmutableObject
    {
        public SearchData(DevExpress.XtraPrinting.BrickPagePair brickPagePair, int pageIndex, DevExpress.XtraPrinting.BookmarkNode bookmarkNode);

        public DevExpress.XtraPrinting.BrickPagePair BrickPagePair { get; private set; }

        public int PageIndex { get; private set; }

        public DevExpress.XtraPrinting.BookmarkNode BookmarkNode { get; private set; }
    }
}

