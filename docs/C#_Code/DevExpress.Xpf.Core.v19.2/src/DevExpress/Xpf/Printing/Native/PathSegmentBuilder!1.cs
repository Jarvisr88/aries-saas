namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class PathSegmentBuilder<TSegment> : PathSegmentBuilder where TSegment: PathSegment
    {
        private readonly Matrix transform;
        private readonly TSegment segment;

        protected PathSegmentBuilder(TSegment segment, Matrix transform)
        {
            this.segment = segment;
            this.transform = transform;
        }

        protected Point GetPoint(Point sourcePoint) => 
            sourcePoint.Transform(this.transform);

        protected TSegment Segment =>
            this.segment;
    }
}

