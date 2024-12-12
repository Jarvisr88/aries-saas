namespace DevExpress.Data.Svg
{
    public interface ISvgElementFactory<T>
    {
        T CreateCircle(SvgCircle circle);
        T CreateEllipse(SvgEllipse ellipse);
        T CreateLine(SvgLine line);
        T CreatePath(SvgPath path);
        T CreatePolygon(SvgPolygon polygon);
        T CreatePolyline(SvgPolyline polyine);
        T CreateRectangle(SvgRectangle rectangle);
        T CreateText(SvgText text);
    }
}

