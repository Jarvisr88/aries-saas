namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("polygon")]
    public class SvgPolygon : DevExpress.Utils.Svg.SvgElement
    {
        public static DevExpress.Utils.Svg.SvgPolygon Create(SvgElementProperties properties, SvgPoint[] svgPoints)
        {
            DevExpress.Utils.Svg.SvgPolygon polygon1 = new DevExpress.Utils.Svg.SvgPolygon();
            polygon1.SvgPoints = svgPoints;
            DevExpress.Utils.Svg.SvgPolygon polygon = polygon1;
            polygon.Assign(properties);
            return polygon;
        }

        public override DevExpress.Utils.Svg.SvgElement DeepCopy(Action<DevExpress.Utils.Svg.SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<DevExpress.Utils.Svg.SvgPolygon>(updateStyle);

        protected virtual void OnPointsChanged()
        {
            if (!string.IsNullOrEmpty(this.Points))
            {
                this.SetValueCore<SvgPoint[]>("SvgPoints", CoordinateParser.GetPoints(this.Points));
            }
        }

        protected virtual void OnSvgPointsChanged()
        {
            if (this.SvgPoints != null)
            {
                Func<SvgPoint, string> selector = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<SvgPoint, string> local1 = <>c.<>9__7_0;
                    selector = <>c.<>9__7_0 = x => x.ToString();
                }
                this.SetValueCore<string>("Points", string.Join(" ", this.SvgPoints.Select<SvgPoint, string>(selector).ToArray<string>()));
            }
        }

        [SvgPropertyNameAlias("points")]
        public string Points
        {
            get => 
                this.GetValueCore<string>("Points", false);
            protected internal set => 
                this.SetValueCore<string>("Points", value, new Action(this.OnPointsChanged));
        }

        public SvgPoint[] SvgPoints
        {
            get => 
                this.GetValueCore<SvgPoint[]>("SvgPoints", false);
            protected set => 
                this.SetValueCore<SvgPoint[]>("SvgPoints", value, new Action(this.OnSvgPointsChanged));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Utils.Svg.SvgPolygon.<>c <>9 = new DevExpress.Utils.Svg.SvgPolygon.<>c();
            public static Func<SvgPoint, string> <>9__7_0;

            internal string <OnSvgPointsChanged>b__7_0(SvgPoint x) => 
                x.ToString();
        }
    }
}

