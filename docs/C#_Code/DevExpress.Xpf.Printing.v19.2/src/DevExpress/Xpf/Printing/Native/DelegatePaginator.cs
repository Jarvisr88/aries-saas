namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;

    public class DelegatePaginator : PaginatorBase
    {
        private readonly Func<int, FrameworkElement> getPageDelegate;
        private readonly Func<int> getPageCountDelegate;

        public DelegatePaginator(Func<int, FrameworkElement> getPageDelegate, Func<int> getPageCountDelegate)
        {
            this.getPageDelegate = getPageDelegate;
            this.getPageCountDelegate = getPageCountDelegate;
        }

        protected override FrameworkElement GetPageContent(int pageNumber) => 
            this.getPageDelegate(pageNumber);

        public override int PageCount =>
            this.getPageCountDelegate();
    }
}

