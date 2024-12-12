namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class SvgPathArcSegment : SvgPathSegment
    {
        public SvgPathArcSegment(SvgPoint start, double radiusX, double radiusY, double angle, bool largeArc, bool sweep, SvgPoint end)
        {
            base.Start = start;
            this.RadiusX = radiusX;
            this.RadiusY = radiusY;
            this.Angle = angle;
            this.LargeArc = largeArc;
            this.Sweep = sweep;
            base.End = end;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathArcSegment(base.Start, this.RadiusX, this.RadiusY, this.Angle, this.LargeArc, this.Sweep, base.End);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((obj == null) || !(obj.GetType() == typeof(SvgPathArcSegment)))
            {
                return false;
            }
            SvgPathArcSegment segment = (SvgPathArcSegment) obj;
            return (((this.Sweep == segment.Sweep) && ((this.LargeArc == segment.LargeArc) && ((this.RadiusX == segment.RadiusX) && ((this.RadiusY == segment.RadiusY) && (this.Angle == segment.Angle))))) && (base.Start.Equals(segment.Start) && base.End.Equals(segment.End)));
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<SvgPoint, double, double, double, bool, bool, SvgPoint>(base.Start, this.RadiusX, this.RadiusY, this.Angle, this.LargeArc, this.Sweep, base.End);

        public override string ToString()
        {
            string str = this.LargeArc ? "1" : "0";
            string str2 = this.Sweep ? "1" : "0";
            string[] textArray1 = new string[12];
            textArray1[0] = "A";
            textArray1[1] = this.RadiusX.ToString(CultureInfo.InvariantCulture);
            textArray1[2] = " ";
            textArray1[3] = this.RadiusY.ToString(CultureInfo.InvariantCulture);
            textArray1[4] = " ";
            textArray1[5] = this.Angle.ToString(CultureInfo.InvariantCulture);
            textArray1[6] = " ";
            textArray1[7] = str;
            textArray1[8] = " ";
            textArray1[9] = str2;
            textArray1[10] = " ";
            textArray1[11] = base.End.ToString(CultureInfo.InvariantCulture);
            return string.Concat(textArray1);
        }

        public double RadiusX { get; private set; }

        public double RadiusY { get; private set; }

        public double Angle { get; private set; }

        public bool Sweep { get; private set; }

        public bool LargeArc { get; private set; }
    }
}

