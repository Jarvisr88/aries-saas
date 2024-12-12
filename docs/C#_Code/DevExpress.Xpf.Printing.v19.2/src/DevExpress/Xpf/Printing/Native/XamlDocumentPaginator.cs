namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    public class XamlDocumentPaginator : PaginatorBase
    {
        private readonly string[] pages;

        public XamlDocumentPaginator(string[] pages)
        {
            Guard.ArgumentNotNull(pages, "pages");
            this.pages = pages;
        }

        protected override FrameworkElement GetPageContent(int pageNumber) => 
            XamlReaderHelper.Load(this.pages[pageNumber]);

        public override int PageCount =>
            this.pages.Length;
    }
}

