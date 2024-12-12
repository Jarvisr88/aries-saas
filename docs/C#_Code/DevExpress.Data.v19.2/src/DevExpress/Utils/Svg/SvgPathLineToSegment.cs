namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public class SvgPathLineToSegment : SvgPathSegment
    {
        public SvgPathLineToSegment(SvgPoint start, SvgPoint end)
        {
            base.Start = start;
            base.End = end;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathLineToSegment(base.Start, base.End);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((obj == null) || !(obj.GetType() == typeof(SvgPathLineToSegment)))
            {
                return false;
            }
            SvgPathLineToSegment segment = (SvgPathLineToSegment) obj;
            return (base.Start.Equals(segment.Start) && base.End.Equals(segment.End));
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<SvgPoint, SvgPoint>(base.Start, base.End);

        public override string ToString() => 
            "L" + base.End.ToString(CultureInfo.InvariantCulture);
    }
}

