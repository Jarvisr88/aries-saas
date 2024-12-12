namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using System;

    public class SvgDefinitionsWrapper : SvgElementWrapper
    {
        public SvgDefinitionsWrapper(SvgElement element) : base(element)
        {
        }

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
        }

        private SvgDefinitions Definitions =>
            base.Element as SvgDefinitions;
    }
}

