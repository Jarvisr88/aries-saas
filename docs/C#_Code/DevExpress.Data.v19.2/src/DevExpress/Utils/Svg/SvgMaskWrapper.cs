namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;

    public class SvgMaskWrapper : SvgElementWrapper
    {
        public SvgMaskWrapper(SvgElement element) : base(element)
        {
        }

        protected override void RenderChild(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale, SvgElementWrapper elementWrapper)
        {
        }

        protected override void RenderCore(ISvgGraphics g, double scale)
        {
        }

        private SvgMask Mask =>
            base.Element as SvgMask;
    }
}

