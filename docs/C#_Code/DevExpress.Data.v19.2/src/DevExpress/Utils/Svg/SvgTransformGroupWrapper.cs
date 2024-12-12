namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;

    public class SvgTransformGroupWrapper : SvgElementWrapper
    {
        public SvgTransformGroupWrapper(SvgElement element) : base(element)
        {
        }

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
            if (base.IsDisplay)
            {
                object graphicsState = g.Save();
                if (base.Element.Parent == null)
                {
                    g.Transform = this.GetTransform(g.Transform, scale, false);
                }
                this.SetClip(g, scale);
                this.RenderChild(g, paletteProvider, scale, this);
                g.Restore(graphicsState);
            }
        }

        private SvgTransformGroup Group =>
            base.Element as SvgTransformGroup;
    }
}

