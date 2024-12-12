namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Documents;

    internal class PageRangeCustomPaginator : DocumentPaginator
    {
        private readonly DocumentPaginator paginator;
        private readonly int[] pageIndexes;

        public PageRangeCustomPaginator(DocumentPaginator paginator, int[] pageIndexes)
        {
            Guard.ArgumentNotNull(paginator, "paginator");
            Guard.ArgumentNotNull(pageIndexes, "pageIndexes");
            this.paginator = paginator;
            this.pageIndexes = pageIndexes;
        }

        public override DocumentPage GetPage(int pageNumber) => 
            this.paginator.GetPage(this.pageIndexes[pageNumber]);

        public override int PageCount =>
            this.pageIndexes.Length;

        public override bool IsPageCountValid =>
            this.paginator.IsPageCountValid;

        public override Size PageSize
        {
            get => 
                this.paginator.PageSize;
            set => 
                this.paginator.PageSize = value;
        }

        public override IDocumentPaginatorSource Source =>
            this.paginator.Source;
    }
}

