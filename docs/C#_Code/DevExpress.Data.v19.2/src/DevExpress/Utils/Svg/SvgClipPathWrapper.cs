namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;
    using System.Drawing.Drawing2D;

    public class SvgClipPathWrapper : SvgElementWrapper
    {
        public SvgClipPathWrapper(SvgElement element) : base(element)
        {
        }

        private void CombinePaths(GraphicsPath path, SvgElementWrapper wrapper, double scale)
        {
            if (wrapper != null)
            {
                path.FillMode = (wrapper.Element.ClipRule == SvgClipRule.NonZero) ? FillMode.Winding : FillMode.Alternate;
                GraphicsPath addingPath = wrapper.GetPath(scale);
                if (wrapper.Element.Transformations != null)
                {
                    foreach (SvgTransform transform in wrapper.Element.Transformations)
                    {
                        addingPath.Transform(transform.Matrix);
                    }
                }
                if (addingPath.PointCount > 0)
                {
                    path.AddPath(addingPath, false);
                }
            }
            foreach (SvgElementWrapper wrapper2 in wrapper.Childs)
            {
                this.CombinePaths(path, wrapper2, scale);
            }
        }

        protected override GraphicsPath GetPathCore(double scale)
        {
            GraphicsPath path = new GraphicsPath();
            foreach (SvgElementWrapper wrapper in base.Childs)
            {
                this.CombinePaths(path, wrapper, scale);
            }
            return path;
        }

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
        }

        private SvgClipPath SvgUse =>
            base.Element as SvgClipPath;
    }
}

