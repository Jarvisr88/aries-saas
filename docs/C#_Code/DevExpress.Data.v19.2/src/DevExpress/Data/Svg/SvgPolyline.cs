namespace DevExpress.Data.Svg
{
    [FormatElement("polyline")]
    public class SvgPolyline : SvgPointContainer
    {
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
    }
}

