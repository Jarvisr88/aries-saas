namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Globalization;

    public class SvgPathMoveToSegment : SvgPathSegment
    {
        public SvgPathMoveToSegment(SvgPoint moveToPoint)
        {
            base.Start = moveToPoint;
            base.End = moveToPoint;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathMoveToSegment(base.Start);

        public override bool Equals(object obj) => 
            !ReferenceEquals(this, obj) ? (((obj != null) && (obj.GetType() == typeof(SvgPathMoveToSegment))) && base.Start.Equals(((SvgPathMoveToSegment) obj).Start)) : true;

        public override int GetHashCode() => 
            base.Start.GetHashCode();

        public override string ToString() => 
            "M" + base.Start.ToString(CultureInfo.InvariantCulture);
    }
}

