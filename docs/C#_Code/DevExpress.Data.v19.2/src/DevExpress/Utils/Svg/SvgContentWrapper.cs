namespace DevExpress.Utils.Svg
{
    using System;

    public class SvgContentWrapper : SvgElementWrapper
    {
        public SvgContentWrapper(SvgElement element) : base(element)
        {
        }

        public string Text =>
            (base.Element as SvgContent).Content;
    }
}

