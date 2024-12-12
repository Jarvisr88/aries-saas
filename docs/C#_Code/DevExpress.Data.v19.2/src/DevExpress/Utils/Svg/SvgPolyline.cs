namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("polyline")]
    public class SvgPolyline : DevExpress.Utils.Svg.SvgPolygon
    {
        public static DevExpress.Utils.Svg.SvgPolyline Create(SvgElementProperties properties, SvgPoint[] svgPoints)
        {
            DevExpress.Utils.Svg.SvgPolyline polyline1 = new DevExpress.Utils.Svg.SvgPolyline();
            polyline1.SvgPoints = svgPoints;
            DevExpress.Utils.Svg.SvgPolyline polyline = polyline1;
            polyline.Assign(properties);
            return polyline;
        }

        public override DevExpress.Utils.Svg.SvgElement DeepCopy(Action<DevExpress.Utils.Svg.SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<DevExpress.Utils.Svg.SvgPolyline>(updateStyle);
    }
}

