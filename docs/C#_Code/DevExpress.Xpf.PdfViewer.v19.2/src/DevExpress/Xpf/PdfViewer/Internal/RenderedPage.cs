namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RenderedPage : RenderedPageBase
    {
        public RenderedPage(int pageIndex) : base(pageIndex)
        {
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            Action<Bitmap> action = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Action<Bitmap> local1 = <>c.<>9__5_0;
                action = <>c.<>9__5_0 = x => x.Dispose();
            }
            this.RenderedContent.Do<Bitmap>(action);
            this.RenderedContent = null;
        }

        public override int GetAllocatedSize()
        {
            Func<Bitmap, int> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<Bitmap, int> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => ((x.Width * x.Height) * 0x20) / 8;
            }
            return this.RenderedContent.Return<Bitmap, int>(evaluator, (<>c.<>9__6_1 ??= () => 0));
        }

        public virtual bool Match(double zoomFactor, double angle) => 
            base.ZoomFactor.AreClose(zoomFactor) && base.Angle.AreClose(angle);

        public Bitmap RenderedContent { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderedPage.<>c <>9 = new RenderedPage.<>c();
            public static Action<Bitmap> <>9__5_0;
            public static Func<Bitmap, int> <>9__6_0;
            public static Func<int> <>9__6_1;

            internal void <DisposeInternal>b__5_0(Bitmap x)
            {
                x.Dispose();
            }

            internal int <GetAllocatedSize>b__6_0(Bitmap x) => 
                ((x.Width * x.Height) * 0x20) / 8;

            internal int <GetAllocatedSize>b__6_1() => 
                0;
        }
    }
}

