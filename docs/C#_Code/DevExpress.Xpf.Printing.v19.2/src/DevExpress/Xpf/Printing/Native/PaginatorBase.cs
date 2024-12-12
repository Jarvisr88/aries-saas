namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Documents;

    public abstract class PaginatorBase : System.Windows.Documents.DocumentPaginator, IDocumentPaginatorSource
    {
        protected PaginatorBase()
        {
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            FrameworkElement pageContent = this.GetPageContent(pageNumber);
            Size size = new Size(pageContent.Width, pageContent.Height);
            pageContent.Arrange(new Rect(size));
            return new DocumentPage(pageContent, size, new Rect(size), new Rect(size));
        }

        protected abstract FrameworkElement GetPageContent(int pageNumber);

        public override bool IsPageCountValid =>
            true;

        public override Size PageSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override IDocumentPaginatorSource Source =>
            this;

        public System.Windows.Documents.DocumentPaginator DocumentPaginator =>
            this;
    }
}

