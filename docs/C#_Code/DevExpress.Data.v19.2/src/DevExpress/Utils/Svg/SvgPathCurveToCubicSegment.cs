namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class SvgPathCurveToCubicSegment : SvgPathSegment
    {
        public SvgPathCurveToCubicSegment(SvgPoint start, SvgPoint firstAdditionalPoint, SvgPoint secondAdditionalPoint, SvgPoint end)
        {
            base.Start = start;
            this.FirstAdditionalPoint = firstAdditionalPoint;
            this.SecondAdditionalPoint = secondAdditionalPoint;
            base.End = end;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathCurveToCubicSegment(base.Start, this.FirstAdditionalPoint, this.SecondAdditionalPoint, base.End);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((obj == null) || !(obj.GetType() == typeof(SvgPathCurveToCubicSegment)))
            {
                return false;
            }
            SvgPathCurveToCubicSegment segment = (SvgPathCurveToCubicSegment) obj;
            return (base.Start.Equals(segment.Start) && (this.FirstAdditionalPoint.Equals(segment.FirstAdditionalPoint) && (this.SecondAdditionalPoint.Equals(segment.SecondAdditionalPoint) && base.End.Equals(segment.End))));
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<SvgPoint, SvgPoint, SvgPoint, SvgPoint>(base.Start, this.FirstAdditionalPoint, this.SecondAdditionalPoint, base.End);

        public override string ToString()
        {
            string[] textArray1 = new string[] { "C", this.FirstAdditionalPoint.ToString(CultureInfo.InvariantCulture), " ", this.SecondAdditionalPoint.ToString(CultureInfo.InvariantCulture), " ", base.End.ToString(CultureInfo.InvariantCulture) };
            return string.Concat(textArray1);
        }

        public SvgPoint FirstAdditionalPoint { get; private set; }

        public SvgPoint SecondAdditionalPoint { get; private set; }
    }
}

