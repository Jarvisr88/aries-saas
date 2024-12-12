namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PathQuadraticBezier : IDocumentModelObject, IPathInstruction, ICloneable<IPathInstruction>, ISupportsCopyFrom<PathQuadraticBezier>
    {
        public PathQuadraticBezier(IDocumentModelPart documentModelPart)
        {
            this.DocumentModelPart = documentModelPart;
            this.Points = new ModelAdjustablePointsList(documentModelPart);
        }

        public PathQuadraticBezier(IDocumentModelPart documentModelPart, AdjustableCoordinate x1, AdjustableCoordinate y1, AdjustableCoordinate x2, AdjustableCoordinate y2) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(x1, y1));
            this.Points.AddCore(new AdjustablePoint(x2, y2));
        }

        public PathQuadraticBezier(IDocumentModelPart documentModelPart, double x1, double y1, double x2, double y2) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(new AdjustableCoordinate(x1), new AdjustableCoordinate(y1)));
            this.Points.AddCore(new AdjustablePoint(new AdjustableCoordinate(x2), new AdjustableCoordinate(y2)));
        }

        public PathQuadraticBezier(IDocumentModelPart documentModelPart, string x1, string y1, string x2, string y2) : this(documentModelPart)
        {
            this.Points.AddCore(new AdjustablePoint(x1, y1));
            this.Points.AddCore(new AdjustablePoint(x2, y2));
        }

        public IPathInstruction Clone()
        {
            PathQuadraticBezier bezier = new PathQuadraticBezier(this.DocumentModelPart);
            bezier.CopyFrom(this);
            return bezier;
        }

        public void CopyFrom(PathQuadraticBezier value)
        {
            Guard.ArgumentNotNull(value, "PathQuadraticBezier");
            this.Points.CopyFrom(value.Points);
        }

        public void Visit(IPathInstructionWalker visitor)
        {
            visitor.Visit(this);
        }

        public IDocumentModelPart DocumentModelPart { get; set; }

        public ModelAdjustablePointsList Points { get; private set; }
    }
}

