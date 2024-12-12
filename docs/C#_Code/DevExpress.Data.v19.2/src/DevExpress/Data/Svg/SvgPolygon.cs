namespace DevExpress.Data.Svg
{
    [FormatElement("polygon")]
    public class SvgPolygon : SvgPointContainer
    {
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
    }
}

