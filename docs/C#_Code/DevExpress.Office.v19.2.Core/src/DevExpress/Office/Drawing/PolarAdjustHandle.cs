namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.History;
    using System;

    public class PolarAdjustHandle : AdjustablePoint
    {
        private string angleGuide;
        private string radialGuide;
        private AdjustableAngle maximumAngle;
        private AdjustableCoordinate maximumRadial;
        private AdjustableAngle minimumAngle;
        private AdjustableCoordinate minimumRadial;

        public PolarAdjustHandle()
        {
        }

        public PolarAdjustHandle(string angleGuide, AdjustableAngle minAng, AdjustableAngle maxAng, string radialGuide, AdjustableCoordinate minR, AdjustableCoordinate maxR, AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.AngleGuide = angleGuide;
            this.RadialGuide = radialGuide;
            this.MaximumAngle = maxAng;
            this.MaximumRadial = maxR;
            this.MinimumAngle = minAng;
            this.MinimumRadial = minR;
            base.X = x;
            base.Y = y;
        }

        public PolarAdjustHandle(string angleGuide, string minAng, string maxAng, string radialGuide, string minR, string maxR, string x, string y)
        {
            this.AngleGuide = angleGuide;
            this.RadialGuide = radialGuide;
            this.MaximumAngle = AdjustableAngle.FromString(maxAng);
            this.MaximumRadial = AdjustableCoordinate.FromString(maxR);
            this.MinimumAngle = AdjustableAngle.FromString(minAng);
            this.MinimumRadial = AdjustableCoordinate.FromString(minR);
            base.X = AdjustableCoordinate.FromString(x);
            base.Y = AdjustableCoordinate.FromString(y);
        }

        private void SetAngleGuide(string angleGuide)
        {
            this.angleGuide = angleGuide;
        }

        private void SetMaximumAngle(AdjustableAngle maximumAngle)
        {
            this.maximumAngle = maximumAngle;
        }

        private void SetMaximumRadial(AdjustableCoordinate maximumRadial)
        {
            this.maximumRadial = maximumRadial;
        }

        private void SetMinimumAngle(AdjustableAngle minimumAngle)
        {
            this.minimumAngle = minimumAngle;
        }

        private void SetMinimumRadial(AdjustableCoordinate minimumRadial)
        {
            this.minimumRadial = minimumRadial;
        }

        private void SetRadialGuide(string radialGuide)
        {
            this.radialGuide = radialGuide;
        }

        public override AdjustablePointType AdjustableType =>
            AdjustablePointType.PolarAdjustHandle;

        public string AngleGuide
        {
            get => 
                this.angleGuide;
            set
            {
                string angleGuide = this.AngleGuide;
                if (angleGuide != value)
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetAngleGuide(value);
                    }
                    else
                    {
                        ActionStringHistoryItem item = new ActionStringHistoryItem(base.DocumentModelPart, angleGuide, value, new Action<string>(this.SetAngleGuide));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public string RadialGuide
        {
            get => 
                this.radialGuide;
            set
            {
                string radialGuide = this.RadialGuide;
                if (radialGuide != value)
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetRadialGuide(value);
                    }
                    else
                    {
                        ActionStringHistoryItem item = new ActionStringHistoryItem(base.DocumentModelPart, radialGuide, value, new Action<string>(this.SetRadialGuide));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableAngle MaximumAngle
        {
            get => 
                this.maximumAngle;
            set
            {
                AdjustableAngle maximumAngle = this.MaximumAngle;
                if (!ReferenceEquals(maximumAngle, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMaximumAngle(value);
                    }
                    else
                    {
                        ActionAdjustableAngleHistoryItem item = new ActionAdjustableAngleHistoryItem(base.DocumentModelPart, maximumAngle, value, new Action<AdjustableAngle>(this.SetMaximumAngle));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MaximumRadial
        {
            get => 
                this.maximumRadial;
            set
            {
                AdjustableCoordinate maximumRadial = this.MaximumRadial;
                if (!ReferenceEquals(maximumRadial, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMaximumRadial(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, maximumRadial, value, new Action<AdjustableCoordinate>(this.SetMaximumRadial));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableAngle MinimumAngle
        {
            get => 
                this.minimumAngle;
            set
            {
                AdjustableAngle minimumAngle = this.MinimumAngle;
                if (!ReferenceEquals(minimumAngle, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMinimumAngle(value);
                    }
                    else
                    {
                        ActionAdjustableAngleHistoryItem item = new ActionAdjustableAngleHistoryItem(base.DocumentModelPart, minimumAngle, value, new Action<AdjustableAngle>(this.SetMinimumAngle));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }

        public AdjustableCoordinate MinimumRadial
        {
            get => 
                this.minimumRadial;
            set
            {
                AdjustableCoordinate minimumRadial = this.MinimumRadial;
                if (!ReferenceEquals(minimumRadial, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetMinimumRadial(value);
                    }
                    else
                    {
                        ActionAdjustableCoordinateHistoryItem item = new ActionAdjustableCoordinateHistoryItem(base.DocumentModelPart, minimumRadial, value, new Action<AdjustableCoordinate>(this.SetMinimumRadial));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }
    }
}

