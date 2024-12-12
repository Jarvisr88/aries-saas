namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;

    public class LineSparklineMapping : SparklineMappingBase
    {
        public LineSparklineMapping(IList<SparklinePoint> sortedPoints, Bounds bounds, InternalRange argumentRange, InternalRange valueRange) : base(sortedPoints, bounds, argumentRange, valueRange)
        {
        }
    }
}

