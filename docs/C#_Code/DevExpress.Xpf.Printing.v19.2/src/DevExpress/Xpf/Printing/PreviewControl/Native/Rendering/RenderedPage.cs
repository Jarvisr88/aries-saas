namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RenderedPage : IDisposable
    {
        protected RenderedPage(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }

        public void Dispose()
        {
            Action<Bitmap> action = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Action<Bitmap> local1 = <>c.<>9__13_0;
                action = <>c.<>9__13_0 = x => x.Dispose();
            }
            this.RenderedContent.Do<Bitmap>(action);
            this.RenderedContent = null;
        }

        protected bool Equals(RenderedPage other) => 
            this.PageIndex == other.PageIndex;

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((RenderedPage) obj) : false) : true) : false;

        public int GetAllocatedSize()
        {
            Func<Bitmap, int> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<Bitmap, int> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => ((x.Width * x.Height) * 0x20) / 8;
            }
            return this.RenderedContent.Return<Bitmap, int>(evaluator, (<>c.<>9__9_1 ??= () => 0));
        }

        public override int GetHashCode() => 
            this.PageIndex;

        public Bitmap RenderedContent { get; set; }

        public int PageIndex { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderedPage.<>c <>9 = new RenderedPage.<>c();
            public static Func<Bitmap, int> <>9__9_0;
            public static Func<int> <>9__9_1;
            public static Action<Bitmap> <>9__13_0;

            internal void <Dispose>b__13_0(Bitmap x)
            {
                x.Dispose();
            }

            internal int <GetAllocatedSize>b__9_0(Bitmap x) => 
                ((x.Width * x.Height) * 0x20) / 8;

            internal int <GetAllocatedSize>b__9_1() => 
                0;
        }
    }
}

