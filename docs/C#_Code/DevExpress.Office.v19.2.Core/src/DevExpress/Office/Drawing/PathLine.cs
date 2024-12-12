namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PathLine : IDocumentModelObject, IPathInstruction, ICloneable<IPathInstruction>, ISupportsCopyFrom<PathLine>
    {
        private AdjustablePoint point;

        public PathLine()
        {
        }

        public PathLine(AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.Point = new AdjustablePoint(x, y);
        }

        public PathLine(double x, double y)
        {
            this.Point = new AdjustablePoint(new AdjustableCoordinate(x), new AdjustableCoordinate(y));
        }

        public PathLine(string x, string y)
        {
            this.Point = new AdjustablePoint();
            this.Point.X = AdjustableCoordinate.FromString(x);
            this.Point.Y = AdjustableCoordinate.FromString(y);
        }

        public IPathInstruction Clone()
        {
            PathLine line = new PathLine();
            line.CopyFrom(this);
            return line;
        }

        public void CopyFrom(PathLine value)
        {
            Guard.ArgumentNotNull(value, "PathLine");
            this.Point = AdjustablePoint.Clone(value.Point);
        }

        private void SetPoint(AdjustablePoint point)
        {
            this.point = point;
        }

        public void Visit(IPathInstructionWalker visitor)
        {
            visitor.Visit(this);
        }

        public AdjustablePoint Point
        {
            get => 
                this.point;
            set
            {
                AdjustablePoint objA = this.Point;
                if (!ReferenceEquals(objA, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetPoint(value);
                    }
                    else
                    {
                        ActionAdjustablePointHistoryItem item = new ActionAdjustablePointHistoryItem(this.DocumentModelPart, objA, value, new Action<AdjustablePoint>(this.SetPoint));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public IDocumentModelPart DocumentModelPart { get; set; }
    }
}

