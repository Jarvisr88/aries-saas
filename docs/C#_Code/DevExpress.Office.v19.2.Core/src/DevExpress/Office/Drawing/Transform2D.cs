namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public class Transform2D : ICloneable<Transform2D>, ISupportsCopyFrom<Transform2D>, ISupportsBinaryReadWrite, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey RotationPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey FlipHPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey FlipVPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey OffsetXPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey OffsetYPropertyKey = new PropertyKey(4);
        public static readonly PropertyKey CxPropertyKey = new PropertyKey(5);
        public static readonly PropertyKey CyPropertyKey = new PropertyKey(6);
        private int rotation;
        private bool flipH;
        private bool flipV;
        private float offsetX;
        private float offsetY;
        private float cx;
        private float cy;
        private readonly InvalidateProxy innerParent;
        private readonly IDocumentModel documentModel;
        private readonly PropertyChangedNotifier notifier;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public Transform2D(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "DocumentModel");
            this.documentModel = documentModel;
            this.innerParent = new InvalidateProxy();
            this.notifier = new PropertyChangedNotifier(this);
        }

        protected virtual void ChangeCxProperty(float value)
        {
            ActionFloatHistoryItem item = new ActionFloatHistoryItem(this.DocumentModel.MainPart, this.Cx, value, new Action<float>(this.SetCxCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeCyProperty(float value)
        {
            ActionFloatHistoryItem item = new ActionFloatHistoryItem(this.DocumentModel.MainPart, this.Cy, value, new Action<float>(this.SetCyCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeFlipHProperty(bool value)
        {
            ActionBooleanHistoryItem item = new ActionBooleanHistoryItem(this.DocumentModel.MainPart, this.FlipH, value, new Action<bool>(this.SetFlipHCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeFlipVProperty(bool value)
        {
            ActionBooleanHistoryItem item = new ActionBooleanHistoryItem(this.DocumentModel.MainPart, this.FlipV, value, new Action<bool>(this.SetFlipVCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeOffsetXProperty(float value)
        {
            ActionFloatHistoryItem item = new ActionFloatHistoryItem(this.DocumentModel.MainPart, this.OffsetX, value, new Action<float>(this.SetOffsetXCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeOffsetYProperty(float value)
        {
            ActionFloatHistoryItem item = new ActionFloatHistoryItem(this.DocumentModel.MainPart, this.OffsetY, value, new Action<float>(this.SetOffsetYCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected virtual void ChangeRotationProperty(int value)
        {
            ActionIntHistoryItem item = new ActionIntHistoryItem(this.DocumentModel.MainPart, this.Rotation, value, new Action<int>(this.SetRotationCore));
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void CheckRotationAndSwapBox()
        {
            if (this.NeedToSwap())
            {
                float cx = this.Cx;
                float cy = this.Cy;
                DocumentModelUnitConverter unitConverter = this.DocumentModel.UnitConverter;
                float num4 = unitConverter.EmuToModelUnitsF((unitConverter.ModelUnitsToEmuF(cy) - unitConverter.ModelUnitsToEmuF(cx)) / 2);
                this.SetOffsetYCore(this.OffsetY + num4);
                this.SetOffsetXCore(this.OffsetX - num4);
                this.SetCxCore(cy);
                this.SetCyCore(cx);
            }
        }

        public Transform2D Clone()
        {
            Transform2D transformd = new Transform2D(this.DocumentModel);
            transformd.CopyFrom(this);
            return transformd;
        }

        public virtual void CopyFrom(Transform2D value)
        {
            Guard.ArgumentNotNull(value, "Transform2D");
            this.Rotation = value.rotation;
            this.FlipH = value.flipH;
            this.FlipV = value.flipV;
            this.OffsetX = value.offsetX;
            this.OffsetY = value.offsetY;
            this.Cx = value.cx;
            this.Cy = value.cy;
        }

        public RectangleF GetRotatedBox()
        {
            double angle = (this.GetRotationAngleInDegrees() * 3.1415926535897931) / 180.0;
            double cx = this.OffsetX + (this.Cx / 2f);
            double cy = this.OffsetY + (this.Cy / 2f);
            PointF tf = RotatePoint(new PointF(this.OffsetX, this.OffsetY), cx, cy, angle);
            PointF tf2 = RotatePoint(new PointF(this.OffsetX, this.OffsetY + this.Cy), cx, cy, angle);
            PointF tf3 = RotatePoint(new PointF(this.OffsetX + this.Cx, this.OffsetY), cx, cy, angle);
            PointF tf4 = RotatePoint(new PointF(this.OffsetX + this.Cx, this.OffsetY + this.Cy), cx, cy, angle);
            return RectangleF.FromLTRB(Math.Min(Math.Min(tf.X, tf2.X), Math.Min(tf3.X, tf4.X)), Math.Min(Math.Min(tf.Y, tf2.Y), Math.Min(tf3.Y, tf4.Y)), Math.Max(Math.Max(tf.X, tf2.X), Math.Max(tf3.X, tf4.X)), Math.Max(Math.Max(tf.Y, tf2.Y), Math.Max(tf3.Y, tf4.Y)));
        }

        public float GetRotationAngleInDegrees() => 
            this.DocumentModel.UnitConverter.ModelUnitsToDegreeF(this.Rotation);

        private bool NeedToSwap()
        {
            float num = this.Rotation % 0xa4cb80;
            if (num < 0f)
            {
                num += 1.08E+07f;
            }
            return ((num >= 2700000f) && (num < 8100000f));
        }

        public void Read(BinaryReader reader)
        {
            this.rotation = reader.ReadInt32();
            this.flipH = reader.ReadBoolean();
            this.flipV = reader.ReadBoolean();
            this.offsetX = reader.ReadSingle();
            this.offsetY = reader.ReadSingle();
            this.cx = reader.ReadSingle();
            this.cy = reader.ReadSingle();
        }

        public void RotateCore(int newValue)
        {
            this.Rotation = newValue;
        }

        private static PointF RotatePoint(PointF point, double cx, double cy, double angle) => 
            new PointF((float) (((Math.Cos(angle) * (point.X - cx)) + (Math.Sin(angle) * (point.Y - cy))) + cx), (float) (((-Math.Sin(angle) * (point.X - cx)) + (Math.Cos(angle) * (point.Y - cy))) + cy));

        public void SetCxCore(float value)
        {
            this.cx = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(CxPropertyKey);
        }

        public void SetCyCore(float value)
        {
            this.cy = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(CyPropertyKey);
        }

        public void SetFlipHCore(bool value)
        {
            this.flipH = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(FlipHPropertyKey);
        }

        public void SetFlipVCore(bool value)
        {
            this.flipV = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(FlipVPropertyKey);
        }

        public void SetOffsetXCore(float value)
        {
            this.offsetX = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(OffsetXPropertyKey);
        }

        public void SetOffsetYCore(float value)
        {
            this.offsetY = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(OffsetYPropertyKey);
        }

        public void SetRotationCore(int value)
        {
            this.rotation = value;
            this.innerParent.Invalidate();
            this.notifier.OnPropertyChanged(RotationPropertyKey);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.rotation);
            writer.Write(this.flipH);
            writer.Write(this.flipV);
            writer.Write(this.offsetX);
            writer.Write(this.offsetY);
            writer.Write(this.cx);
            writer.Write(this.cy);
        }

        public ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        public int Rotation
        {
            get => 
                this.rotation;
            set
            {
                if (this.Rotation != value)
                {
                    this.ChangeRotationProperty(value);
                }
            }
        }

        public bool FlipH
        {
            get => 
                this.flipH;
            set
            {
                if (this.FlipH != value)
                {
                    this.ChangeFlipHProperty(value);
                }
            }
        }

        public bool FlipV
        {
            get => 
                this.flipV;
            set
            {
                if (this.FlipV != value)
                {
                    this.ChangeFlipVProperty(value);
                }
            }
        }

        public float OffsetX
        {
            get => 
                this.offsetX;
            set
            {
                if (this.OffsetX != value)
                {
                    this.ChangeOffsetXProperty(value);
                }
            }
        }

        public float OffsetY
        {
            get => 
                this.offsetY;
            set
            {
                if (this.OffsetY != value)
                {
                    this.ChangeOffsetYProperty(value);
                }
            }
        }

        public float Cx
        {
            get => 
                this.cx;
            set
            {
                if (this.Cx != value)
                {
                    this.ChangeCxProperty(value);
                }
            }
        }

        public float Cy
        {
            get => 
                this.cy;
            set
            {
                if (this.Cy != value)
                {
                    this.ChangeCyProperty(value);
                }
            }
        }

        public bool IsEmpty =>
            (this.Rotation == 0) && (!this.FlipH && (!this.FlipV && ((this.OffsetX == 0f) && ((this.OffsetY == 0f) && ((this.Cx == 0f) && (this.Cy == 0f))))));
    }
}

