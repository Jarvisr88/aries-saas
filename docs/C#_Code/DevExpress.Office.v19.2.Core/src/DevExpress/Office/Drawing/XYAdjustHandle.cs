namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class XYAdjustHandle : AdjustablePoint
    {
        private string horizontalGuide;
        private string verticalGuide;
        private AdjustableCoordinate minX;
        private AdjustableCoordinate maxX;
        private AdjustableCoordinate minY;
        private AdjustableCoordinate maxY;

        public XYAdjustHandle()
        {
        }

        public XYAdjustHandle(string gdRefX, AdjustableCoordinate minX, AdjustableCoordinate maxX, string gdRefY, AdjustableCoordinate minY, AdjustableCoordinate maxY, AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.HorizontalGuide = gdRefX;
            this.VerticalGuide = gdRefY;
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            base.X = x;
            base.Y = y;
        }

        public XYAdjustHandle(string gdRefX, string minX, string maxX, string gdRefY, string minY, string maxY, string x, string y)
        {
            this.HorizontalGuide = gdRefX;
            this.VerticalGuide = gdRefY;
            this.MinX = AdjustableCoordinate.FromString(minX);
            this.MaxX = AdjustableCoordinate.FromString(maxX);
            this.MinY = AdjustableCoordinate.FromString(minY);
            this.MaxY = AdjustableCoordinate.FromString(maxY);
            base.X = AdjustableCoordinate.FromString(x);
            base.Y = AdjustableCoordinate.FromString(y);
        }

        private void SetHorizontalGuide(string horizontalGuide)
        {
            this.horizontalGuide = horizontalGuide;
        }

        private void SetMaxX(AdjustableCoordinate maxX)
        {
            this.maxX = maxX;
        }

        private void SetMaxY(AdjustableCoordinate maxY)
        {
            this.maxY = maxY;
        }

        private void SetMinX(AdjustableCoordinate minX)
        {
            this.minX = minX;
        }

        private void SetMinY(AdjustableCoordinate minY)
        {
            this.minY = minY;
        }

        private void SetVerticalGuide(string verticalGuide)
        {
            this.verticalGuide = verticalGuide;
        }

        public override AdjustablePointType AdjustableType =>
            AdjustablePointType.XYAdjustHandle;

        public string HorizontalGuide
        {
            get => 
                this.horizontalGuide;
            set
            {
                string horizontalGuide = this.HorizontalGuide;
                if (horizontalGuide != value)
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetHorizontalGuide(value);
                    }
                    else
                    {
                        ActionStringHistoryItem item = new ActionStringHistoryItem(base.DocumentModelPart, horizontalGuide, value, new Action<string>(this.SetHorizontalGuide));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public string VerticalGuide
        {
            get => 
                this.verticalGuide;
            set
            {
                string verticalGuide = this.VerticalGuide;
                if (verticalGuide != value)
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetVerticalGuide(value);
                    }
                    else
                    {
                        ActionStringHistoryItem item = new ActionStringHistoryItem(base.DocumentModelPart, verticalGuide, value, new Action<string>(this.SetVerticalGuide));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MinX
        {
            get => 
                this.minX;
            set
            {
                AdjustableCoordinate minX = this.MinX;
                if (!ReferenceEquals(minX, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMinX(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, minX, value, new Action<AdjustableCoordinate>(this.SetMinX));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MaxX
        {
            get => 
                this.maxX;
            set
            {
                AdjustableCoordinate maxX = this.MaxX;
                if (!ReferenceEquals(maxX, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMaxX(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, maxX, value, new Action<AdjustableCoordinate>(this.SetMaxX));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MinY
        {
            get => 
                this.minY;
            set
            {
                AdjustableCoordinate minY = this.MinY;
                if (!ReferenceEquals(minY, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMinY(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, minY, value, new Action<AdjustableCoordinate>(this.SetMinY));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MaxY
        {
            get => 
                this.maxY;
            set
            {
                AdjustableCoordinate maxY = this.MaxY;
                if (!ReferenceEquals(maxY, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMaxY(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, maxY, value, new Action<AdjustableCoordinate>(this.SetMaxY));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }
    }
}

