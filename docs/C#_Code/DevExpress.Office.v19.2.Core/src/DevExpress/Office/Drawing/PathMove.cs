namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PathMove : IDocumentModelObject, IPathInstruction, ICloneable<IPathInstruction>, ISupportsCopyFrom<PathMove>
    {
        private AdjustablePoint point;

        public PathMove()
        {
        }

        public PathMove(AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.Point = new AdjustablePoint(x, y);
        }

        public PathMove(double x, double y)
        {
            this.Point = new AdjustablePoint(new AdjustableCoordinate(x), new AdjustableCoordinate(y));
        }

        public PathMove(string x, string y)
        {
            this.Point = new AdjustablePoint();
            this.Point.X = AdjustableCoordinate.FromString(x);
            this.Point.Y = AdjustableCoordinate.FromString(y);
        }

        public IPathInstruction Clone()
        {
            PathMove move = new PathMove();
            move.CopyFrom(this);
            return move;
        }

        public void CopyFrom(PathMove value)
        {
            Guard.ArgumentNotNull(value, "PathMove");
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

