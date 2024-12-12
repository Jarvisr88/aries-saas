namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PathCubicBezier : IDocumentModelObject, IPathInstruction, ICloneable<IPathInstruction>, ISupportsCopyFrom<PathCubicBezier>
    {
        private IDocumentModelPart documentModelPart;

        public PathCubicBezier(IDocumentModelPart documentModelPart)
        {
            this.documentModelPart = documentModelPart;
            this.Points = new ModelAdjustablePointsList(documentModelPart);
        }

        public PathCubicBezier(IDocumentModelPart documentModelPart, AdjustableCoordinate x1, AdjustableCoordinate y1, AdjustableCoordinate x2, AdjustableCoordinate y2, AdjustableCoordinate x3, AdjustableCoordinate y3) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(x1, y1));
            this.Points.AddCore(new AdjustablePoint(x2, y2));
            this.Points.AddCore(new AdjustablePoint(x3, y3));
        }

        public PathCubicBezier(IDocumentModelPart documentModelPart, double x1, double y1, double x2, double y2, double x3, double y3) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(new AdjustableCoordinate(x1), new AdjustableCoordinate(y1)));
            this.Points.AddCore(new AdjustablePoint(new AdjustableCoordinate(x2), new AdjustableCoordinate(y2)));
            this.Points.AddCore(new AdjustablePoint(new AdjustableCoordinate(x3), new AdjustableCoordinate(y3)));
        }

        public PathCubicBezier(IDocumentModelPart documentModelPart, string x1, string y1, string x2, string y2, string x3, string y3) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(x1, y1));
            this.Points.AddCore(new AdjustablePoint(x2, y2));
            this.Points.AddCore(new AdjustablePoint(x3, y3));
        }

        public IPathInstruction Clone()
        {
            PathCubicBezier bezier = new PathCubicBezier(this.documentModelPart);
            bezier.CopyFrom(this);
            return bezier;
        }

        public void CopyFrom(PathCubicBezier value)
        {
            Guard.ArgumentNotNull(value, "PathCubicBezier");
            this.Points.CopyFrom(value.Points);
        }

        public void Visit(IPathInstructionWalker visitor)
        {
            visitor.Visit(this);
        }

        public ModelAdjustablePointsList Points { get; private set; }

        public IDocumentModelPart DocumentModelPart { get; set; }
    }
}

