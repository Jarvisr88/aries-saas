namespace DevExpress.Data.Svg
{
    [FormatElement("g")]
    public class SvgGroup : SvgElement
    {
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        public override SvgRect GetBoundaryPoints();
    }
}

