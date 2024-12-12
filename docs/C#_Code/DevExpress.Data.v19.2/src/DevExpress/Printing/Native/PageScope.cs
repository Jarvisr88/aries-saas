namespace DevExpress.Printing.Native
{
    using System;

    public class PageScope
    {
        public static readonly PageScope Empty = new PageScope(0, 0);
        private int fromPage;
        private int toPage;

        public PageScope(int fromPage, int toPage)
        {
            this.fromPage = fromPage;
            this.toPage = toPage;
        }

        public PageScope(string pageRange, int maximumPage)
        {
            int[] indices = PageRangeParser.GetIndices(pageRange, maximumPage);
            Array.Sort<int>(indices);
            if (indices.Length != 0)
            {
                this.fromPage = indices[0] + 1;
                this.toPage = indices[indices.Length - 1] + 1;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PageScope))
            {
                return base.Equals(obj);
            }
            PageScope scope = (PageScope) obj;
            return ((this.FromPage == scope.FromPage) && (this.ToPage == scope.ToPage));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public PageScope Validate(int pageCount)
        {
            PageScope scope = new PageScope(this.FromPage, this.ToPage);
            scope.FromPage = Math.Max(1, Math.Min(pageCount, scope.FromPage));
            scope.ToPage = Math.Max(1, Math.Min(pageCount, scope.ToPage));
            scope.ToPage = Math.Max(scope.FromPage, scope.ToPage);
            return scope;
        }

        public int FromPage
        {
            get => 
                this.fromPage;
            set => 
                this.fromPage = value;
        }

        public int ToPage
        {
            get => 
                this.toPage;
            set => 
                this.toPage = value;
        }

        public string PageRange =>
            !this.IsEmpty ? ((this.fromPage == this.toPage) ? this.fromPage.ToString() : $"{this.fromPage}-{this.toPage}") : string.Empty;

        public bool IsEmpty =>
            (this.fromPage == 0) && (this.toPage == 0);
    }
}

