namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;

    public class SvgPathCloseSegment : SvgPathSegment
    {
        public SvgPathCloseSegment()
        {
            base.End = SvgPoint.Empty;
        }

        public override SvgPathSegment DeepCopy() => 
            new SvgPathCloseSegment();

        public override bool Equals(object obj) => 
            (obj != null) && (base.GetType() == obj.GetType());

        public override int GetHashCode() => 
            0;

        public override string ToString() => 
            "z";
    }
}

