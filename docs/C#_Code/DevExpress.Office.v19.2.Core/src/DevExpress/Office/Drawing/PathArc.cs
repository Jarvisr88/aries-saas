namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PathArc : IDocumentModelObject, IPathInstruction, ICloneable<IPathInstruction>, ISupportsCopyFrom<PathArc>
    {
        private AdjustableCoordinate heightRadius;
        private AdjustableCoordinate widthRadius;
        private AdjustableAngle startAngle;
        private AdjustableAngle swingAngle;

        public PathArc()
        {
        }

        public PathArc(AdjustableCoordinate widthRadius, AdjustableCoordinate heightRadius, AdjustableAngle startAngle, AdjustableAngle swingAngle)
        {
            this.HeightRadius = heightRadius;
            this.WidthRadius = widthRadius;
            this.StartAngle = startAngle;
            this.SwingAngle = swingAngle;
        }

        public PathArc(string widthRadius, string heightRadius, string startAngle, string swingAngle)
        {
            this.HeightRadius = AdjustableCoordinate.FromString(heightRadius);
            this.WidthRadius = AdjustableCoordinate.FromString(widthRadius);
            this.StartAngle = AdjustableAngle.FromString(startAngle);
            this.SwingAngle = AdjustableAngle.FromString(swingAngle);
        }

        public IPathInstruction Clone()
        {
            PathArc arc = new PathArc();
            arc.CopyFrom(this);
            return arc;
        }

        public void CopyFrom(PathArc value)
        {
            Guard.ArgumentNotNull(value, "PathArc");
            this.HeightRadius = value.HeightRadius;
            this.WidthRadius = value.WidthRadius;
            this.StartAngle = value.StartAngle;
            this.SwingAngle = value.SwingAngle;
        }

        private void SetHeightRadius(AdjustableCoordinate heightRadius)
        {
            this.heightRadius = heightRadius;
        }

        private void SetStartAngle(AdjustableAngle startAngle)
        {
            this.startAngle = startAngle;
        }

        private void SetSwingAngle(AdjustableAngle swingAngle)
        {
            this.swingAngle = swingAngle;
        }

        private void SetWidthRadius(AdjustableCoordinate widthRadius)
        {
            this.widthRadius = widthRadius;
        }

        public void Visit(IPathInstructionWalker visitor)
        {
            visitor.Visit(this);
        }

        public AdjustableCoordinate HeightRadius
        {
            get => 
                this.heightRadius;
            set
            {
                AdjustableCoordinate heightRadius = this.HeightRadius;
                if (!ReferenceEquals(heightRadius, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetHeightRadius(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(this.DocumentModelPart, heightRadius, value, new Action<AdjustableCoordinate>(this.SetHeightRadius));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate WidthRadius
        {
            get => 
                this.widthRadius;
            set
            {
                AdjustableCoordinate widthRadius = this.WidthRadius;
                if (!ReferenceEquals(widthRadius, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetWidthRadius(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(this.DocumentModelPart, widthRadius, value, new Action<AdjustableCoordinate>(this.SetWidthRadius));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableAngle StartAngle
        {
            get => 
                this.startAngle;
            set
            {
                AdjustableAngle startAngle = this.StartAngle;
                if (!ReferenceEquals(startAngle, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetStartAngle(value);
                    }
                    else
                    {
                        ActionAdjustableAngleHistoryItem item = new ActionAdjustableAngleHistoryItem(this.DocumentModelPart, startAngle, value, new Action<AdjustableAngle>(this.SetStartAngle));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableAngle SwingAngle
        {
            get => 
                this.swingAngle;
            set
            {
                AdjustableAngle swingAngle = this.SwingAngle;
                if (!ReferenceEquals(swingAngle, value))
                {
                    if (this.DocumentModelPart == null)
                    {
                        this.SetSwingAngle(value);
                    }
                    else
                    {
                        ActionAdjustableAngleHistoryItem item = new ActionAdjustableAngleHistoryItem(this.DocumentModelPart, swingAngle, value, new Action<AdjustableAngle>(this.SetSwingAngle));
                        this.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public IDocumentModelPart DocumentModelPart { get; set; }
    }
}

