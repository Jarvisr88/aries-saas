namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class SvgPathCurveToQuadraticSegment : SvgPathSegment
    {
        private SvgPoint firstAdditionalPointCore;
        private SvgPoint secondAdditionalPointCore;

        public SvgPathCurveToQuadraticSegment(SvgPoint start, SvgPoint additionalPoint, SvgPoint end)
        {
            base.Start = start;
            this.AdditionalPoint = additionalPoint;
            base.End = end;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathCurveToQuadraticSegment(base.Start, this.AdditionalPoint, base.End);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if ((obj == null) || !(obj.GetType() == typeof(SvgPathCurveToQuadraticSegment)))
            {
                return false;
            }
            SvgPathCurveToQuadraticSegment segment = (SvgPathCurveToQuadraticSegment) obj;
            return (base.Start.Equals(segment.Start) && (this.AdditionalPoint.Equals(segment.AdditionalPoint) && base.End.Equals(segment.End)));
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<SvgPoint, SvgPoint, SvgPoint>(base.Start, this.AdditionalPoint, base.End);

        public override string ToString() => 
            "Q" + this.AdditionalPoint.ToString(CultureInfo.InvariantCulture) + " " + base.End.ToString(CultureInfo.InvariantCulture);

        public SvgPoint AdditionalPoint { get; private set; }

        public SvgPoint FirstAdditionalPoint
        {
            get
            {
                if (this.firstAdditionalPointCore == SvgPoint.Empty)
                {
                    double x = base.Start.X + (0.66666666666666663 * (this.AdditionalPoint.X - base.Start.X));
                    this.firstAdditionalPointCore = new SvgPoint(x, base.Start.Y + (0.66666666666666663 * (this.AdditionalPoint.Y - base.Start.Y)));
                }
                return this.firstAdditionalPointCore;
            }
        }

        public SvgPoint SecondAdditionalPoint
        {
            get
            {
                if (this.secondAdditionalPointCore == SvgPoint.Empty)
                {
                    double x = base.End.X + (0.66666666666666663 * (this.AdditionalPoint.X - base.End.X));
                    this.secondAdditionalPointCore = new SvgPoint(x, base.End.Y + (0.66666666666666663 * (this.AdditionalPoint.Y - base.End.Y)));
                }
                return this.secondAdditionalPointCore;
            }
        }
    }
}

