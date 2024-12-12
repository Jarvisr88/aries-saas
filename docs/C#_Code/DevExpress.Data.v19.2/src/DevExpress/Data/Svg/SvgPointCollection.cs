namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SvgPointCollection : List<SvgPoint>
    {
        private static readonly RegexOptions RegOptions;

        static SvgPointCollection();
        public SvgPointCollection();
        public SvgPointCollection(IList<SvgPoint> svgPoints);
        public SvgPointCollection(int capacity);
        public void Export(StringBuilder stringBuilder);
        public SvgRect GetBoundaryPoints();
        internal static string NormalizeSourceString(string sourceString);
        public static SvgPointCollection Parse(string pointsString);
        public static SvgPointCollection Parse(string[] pointsList);
        public override string ToString();

        public IList<SvgPoint> Points { get; }
    }
}

