namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class AdjustablePoint : IDocumentModelObject, ICloneable<AdjustablePoint>, ISupportsCopyFrom<AdjustablePoint>
    {
        private AdjustableCoordinate x;
        private AdjustableCoordinate y;

        public AdjustablePoint()
        {
        }

        public AdjustablePoint(AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.X = x;
            this.Y = y;
        }

        public AdjustablePoint(string x, string y)
        {
            this.X = AdjustableCoordinate.FromString(x);
            this.Y = AdjustableCoordinate.FromString(y);
        }

        public AdjustablePoint Clone()
        {
            AdjustablePoint point = new AdjustablePoint();
            point.CopyFrom(this);
            return point;
        }

        public static AdjustablePoint Clone(AdjustablePoint other) => 
            other?.Clone();

        public void CopyFrom(AdjustablePoint value)
        {
            Guard.ArgumentNotNull(value, "AdjustablePoint");
            this.X = value.X;
            this.Y = value.Y;
        }

        public override bool Equals(object obj)
        {
            AdjustablePoint point = obj as AdjustablePoint;
            return ((point != null) ? (ReferenceEquals(this.X, point.X) && ReferenceEquals(this.Y, point.Y)) : false);
        }

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            if (this.X != null)
            {
                hashCode ^= this.X.GetHashCode();
            }
            if (this.Y != null)
            {
                hashCode ^= this.Y.GetHashCode();
            }
            return hashCode;
        }

        private void SetX(AdjustableCoordinate x)
        {
            this.x = x;
        }

        private void SetY(AdjustableCoordinate y)
        {
            this.y = y;
        }

        public virtual AdjustablePointType AdjustableType =>
            AdjustablePointType.AdjustablePoint;

        public AdjustableCoordinate X
        {
            get => 
                this.x;
            set
            {
                AdjustableCoordinate x = this.X;
                if (!ReferenceEquals(x, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetX(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(this.DocumentModelPart, x, value, new Action<AdjustableCoordinate>(this.SetX));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate Y
        {
            get => 
                this.y;
            set
            {
                AdjustableCoordinate y = this.Y;
                if (!ReferenceEquals(y, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetY(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(this.DocumentModelPart, y, value, new Action<AdjustableCoordinate>(this.SetY));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public IDocumentModelPart DocumentModelPart { get; set; }
    }
}

