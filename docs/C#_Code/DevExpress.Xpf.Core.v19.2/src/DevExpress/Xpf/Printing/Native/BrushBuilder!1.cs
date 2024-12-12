namespace DevExpress.Xpf.Printing.Native
{
    using System;

    public abstract class BrushBuilder<TBrush> : BrushBuilder where TBrush: System.Windows.Media.Brush
    {
        private readonly TBrush brush;

        protected BrushBuilder(TBrush brush)
        {
            this.brush = brush;
        }

        protected TBrush Brush =>
            this.brush;
    }
}

