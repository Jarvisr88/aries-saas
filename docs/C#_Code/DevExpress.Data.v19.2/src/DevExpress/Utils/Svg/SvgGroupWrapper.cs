namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;

    public class SvgGroupWrapper : SvgElementWrapper
    {
        public SvgGroupWrapper(SvgElement element) : base(element)
        {
        }

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
            if (base.IsDisplay)
            {
                object graphicsState = g.Save();
                g.Transform = this.GetTransform(g.Transform, scale, false);
                this.SetClip(g, scale);
                this.RenderChild(g, paletteProvider, scale, this);
                g.Restore(graphicsState);
            }
        }

        private SvgGroup Group =>
            base.Element as SvgGroup;
    }
}

