namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;

    public class ScrollByPixelInfo : ScrollInfoBase
    {
        public ScrollByPixelInfo(IScrollInfoOwner scrollOwner, SizeHelperBase sizeHelper) : base(scrollOwner, sizeHelper)
        {
        }

        public override void LineDown()
        {
            base.LinesDown((double) this.ScrollOwner.ScrollStep);
        }

        public override void LineUp()
        {
            base.LinesUp((double) this.ScrollOwner.ScrollStep);
        }

        protected override void NeedMeasure()
        {
            base.NeedMeasure();
            this.ScrollOwner.InvalidateHorizontalScrolling();
        }

        protected override bool OnBeforeChangeOffset() => 
            this.ScrollOwner.OnBeforeChangePixelScrollOffset();

        protected override void OnScrollInfoChanged()
        {
            if ((this.Offset + base.Viewport) > base.Extent)
            {
                base.fOffset = Math.Max((double) 0.0, (double) (base.Extent - base.Viewport));
            }
            this.ScrollOwner.OnSecondaryScrollInfoChanged();
        }

        protected override double ValidateOffsetCore(double value) => 
            (value > 0.0) ? Math.Min(value, Math.Max((double) 0.0, (double) (base.Extent - base.Viewport))) : 0.0;
    }
}

