namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;

    public class ScrollByItemInfo : ScrollInfoBase
    {
        public ScrollByItemInfo(IScrollInfoOwner scrollOwner, SizeHelperBase sizeHelper) : base(scrollOwner, sizeHelper)
        {
        }

        protected override bool OnBeforeChangeOffset() => 
            this.ScrollOwner.OnBeforeChangeItemScrollOffset();

        protected override void OnScrollInfoChanged()
        {
            this.ScrollOwner.OnDefineScrollInfoChanged();
        }

        protected override double ValidateOffsetCore(double value)
        {
            if (value > (base.Extent - base.GetScrollableViewportSize()))
            {
                value = base.Extent - base.GetScrollableViewportSize();
            }
            return ((value > 0.0) ? value : 0.0);
        }

        public override double Offset =>
            Math.Max(0.0, Math.Min(base.Offset, (double) (this.ScrollOwner.ItemCount - 1)));
    }
}

