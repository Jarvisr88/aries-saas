namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SvgPathSegment
    {
        protected SvgPathSegment()
        {
        }

        public abstract SvgPathSegment DeepCopy();

        public SvgPoint Start { get; protected set; }

        public SvgPoint End { get; protected set; }
    }
}

