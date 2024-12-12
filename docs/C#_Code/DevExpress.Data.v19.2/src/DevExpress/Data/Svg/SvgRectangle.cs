namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("rect")]
    public class SvgRectangle : SvgSupportRectangle
    {
        public SvgRectangle();
        public SvgRectangle(double x, double y, double width, double height);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        public SvgPath ToPath();
    }
}

