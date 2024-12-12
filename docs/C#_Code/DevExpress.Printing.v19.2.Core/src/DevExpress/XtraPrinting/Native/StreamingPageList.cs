namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class StreamingPageList : PageList
    {
        private readonly List<long> pageIndexes;
        private readonly IStreamingDocument streamingDocument;

        public StreamingPageList(IStreamingDocument document);
        public StreamingPageList(IStreamingDocument document, IList<Page> list);
        public override void Add(Page page);
        public override int GetPageIndexByID(long id);
        public override void Insert(int index, Page page);
        protected override void InvalidateIndices(int fromIndex);
        public override void RemoveAt(int index);
        protected override void ValidatePage(Page page);

        public int PageIndexesCount { get; }

        public override Page this[int index] { get; set; }
    }
}

