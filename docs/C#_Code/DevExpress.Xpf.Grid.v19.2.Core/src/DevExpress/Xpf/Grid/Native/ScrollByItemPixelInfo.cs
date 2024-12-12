namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;

    public class ScrollByItemPixelInfo : ScrollByItemInfo
    {
        public ScrollByItemPixelInfo(IScrollInfoOwner scrollOwner, SizeHelperBase sizeHelper) : base(scrollOwner, sizeHelper)
        {
        }

        protected override double ValidateOffset(double value) => 
            this.ValidateOffsetCore(value);

        public override double Offset =>
            Math.Max(0.0, Math.Min(base.fOffset, (double) this.ScrollOwner.ItemCount));
    }
}

