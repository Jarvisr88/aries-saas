namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class RenderedPageBase : IDisposable
    {
        protected RenderedPageBase(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }

        public void Dispose()
        {
            this.DisposeInternal();
        }

        protected virtual void DisposeInternal()
        {
        }

        protected bool Equals(RenderedPageBase other) => 
            this.PageIndex == other.PageIndex;

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((RenderedPageBase) obj) : false) : true) : false;

        public virtual int GetAllocatedSize() => 
            0;

        public override int GetHashCode() => 
            this.PageIndex;

        public double ZoomFactor { get; set; }

        public double Angle { get; set; }

        public int PageIndex { get; private set; }
    }
}

