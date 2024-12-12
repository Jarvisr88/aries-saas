namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ModelShapeConnection : AdjustablePoint, ICloneable<ModelShapeConnection>, ISupportsCopyFrom<ModelShapeConnection>
    {
        private AdjustableAngle angle;

        public ModelShapeConnection()
        {
        }

        public ModelShapeConnection(AdjustableAngle angle, AdjustableCoordinate x, AdjustableCoordinate y)
        {
            this.Angle = angle;
            base.X = x;
            base.Y = y;
        }

        public ModelShapeConnection(string angle, string x, string y)
        {
            this.Angle = AdjustableAngle.FromString(angle);
            base.X = AdjustableCoordinate.FromString(x);
            base.Y = AdjustableCoordinate.FromString(y);
        }

        public ModelShapeConnection Clone()
        {
            ModelShapeConnection connection = new ModelShapeConnection();
            connection.CopyFrom(this);
            return connection;
        }

        public void CopyFrom(ModelShapeConnection value)
        {
            Guard.ArgumentNotNull(value, "ModelShapeConnection");
            base.CopyFrom(value);
            this.Angle = value.Angle;
        }

        private void SetAngle(AdjustableAngle angle)
        {
            this.angle = angle;
        }

        public AdjustableAngle Angle
        {
            get => 
                this.angle;
            set
            {
                AdjustableAngle objA = this.Angle;
                if (!ReferenceEquals(objA, value))
                {
                    if (base.DocumentModelPart == null)
                    {
                        this.SetAngle(value);
                    }
                    else
                    {
                        ActionAdjustableAngleHistoryItem item = new ActionAdjustableAngleHistoryItem(base.DocumentModelPart, objA, value, new Action<AdjustableAngle>(this.SetAngle));
                        base.DocumentModelPart.DocumentModel.History.Add(item);
                        item.Execute();
                    }
                }
            }
        }
    }
}

