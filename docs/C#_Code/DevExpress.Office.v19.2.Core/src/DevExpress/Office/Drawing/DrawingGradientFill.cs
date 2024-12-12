namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class DrawingGradientFill : DrawingUndoableIndexBasedObjectEx<DrawingGradientFillInfo>, ISupportsCopyFrom<DrawingGradientFill>, IDrawingFill, IOfficeNotifyPropertyChanged, IUnderlineFill
    {
        public static readonly PropertyKey GradientTypePropertyKey = new PropertyKey(0);
        public static readonly PropertyKey FlipPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey RotateWithShapePropertyKey = new PropertyKey(2);
        public static readonly PropertyKey ScaledPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey AnglePropertyKey = new PropertyKey(4);
        public static readonly PropertyKey TileRectPropertyKey = new PropertyKey(5);
        public static readonly PropertyKey FillRectPropertyKey = new PropertyKey(6);
        private GradientStopCollection gradientStops;
        private RectangleOffset tileRect;
        private RectangleOffset fillRect;

        public DrawingGradientFill(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            GradientStopCollection collection1 = new GradientStopCollection(documentModel);
            collection1.Parent = base.InnerParent;
            this.gradientStops = collection1;
            this.tileRect = RectangleOffset.Empty;
            this.fillRect = RectangleOffset.Empty;
        }

        public void AddGradientStop(DrawingGradientStop stop)
        {
            this.gradientStops.Add(stop);
        }

        public virtual IDrawingFill CloneTo(IDocumentModel documentModel)
        {
            DrawingGradientFill fill = new DrawingGradientFill(documentModel);
            fill.CopyFrom(this);
            return fill;
        }

        public void CopyFrom(DrawingGradientFill value)
        {
            Guard.ArgumentNotNull(value, "value");
            base.CopyFrom(value);
            this.TileRect = value.TileRect;
            this.FillRect = value.FillRect;
            this.gradientStops.Clear();
            for (int i = 0; i < value.GradientStops.Count; i++)
            {
                DrawingGradientStop stop = value.GradientStops[i];
                DrawingGradientStop stop2 = new DrawingGradientStop(base.DocumentModel);
                stop2.CopyFrom(stop);
                this.AddGradientStop(stop2);
            }
        }

        public static DrawingGradientFill Create(IDocumentModel documentModel, DevExpress.Office.Drawing.GradientType type, IList<GradientStopInfo> stopInfoes)
        {
            DrawingGradientFill fill = new DrawingGradientFill(documentModel);
            fill.AssignInfo(DrawingGradientFillInfo.Create(type));
            fill.GradientStops.AddStopsFromInfoes(stopInfoes);
            if (type != DevExpress.Office.Drawing.GradientType.Linear)
            {
                int bottomOffset = 0xc350;
                fill.SetFillRectCore(new RectangleOffset(bottomOffset, bottomOffset, bottomOffset, bottomOffset));
            }
            return fill;
        }

        IUnderlineFill IUnderlineFill.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel) as IUnderlineFill;

        public override bool Equals(object obj)
        {
            DrawingGradientFill fill = obj as DrawingGradientFill;
            if (fill == null)
            {
                return false;
            }
            if (fill.FillType != DrawingFillType.Gradient)
            {
                return false;
            }
            DrawingGradientFill fill2 = fill;
            if (!base.Info.Equals(fill2.Info))
            {
                return false;
            }
            if (!this.FillRect.Equals(fill2.FillRect))
            {
                return false;
            }
            if (!this.TileRect.Equals(fill2.TileRect))
            {
                return false;
            }
            if (this.GradientStops.Count != fill2.GradientStops.Count)
            {
                return false;
            }
            for (int i = 0; i < this.GradientStops.Count; i++)
            {
                if (!this.GradientStops[i].Equals(fill2.GradientStops[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override PropertyKey GetBatchUpdateChangeActions() => 
            PropertyKey.Undefined;

        protected internal override UniqueItemsCache<DrawingGradientFillInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.DrawingGradientFillInfoCache;

        protected virtual int GetDefaultAngle() => 
            base.DocumentModel.DrawingCache.DrawingGradientFillInfoCache.DefaultItem.Angle;

        protected virtual TileFlipType GetDefaultFlip() => 
            base.DocumentModel.DrawingCache.DrawingGradientFillInfoCache.DefaultItem.Flip;

        protected virtual DevExpress.Office.Drawing.GradientType GetDefaultGradientType() => 
            base.DocumentModel.DrawingCache.DrawingGradientFillInfoCache.DefaultItem.GradientType;

        protected virtual bool GetDefaultRotateWithShape() => 
            base.DocumentModel.DrawingCache.DrawingGradientFillInfoCache.DefaultItem.RotateWithShape;

        protected virtual bool GetDefaultScaled() => 
            base.DocumentModel.DrawingCache.DrawingGradientFillInfoCache.DefaultItem.Scaled;

        protected virtual RectangleOffset GetFillRect() => 
            this.fillRect;

        protected virtual GradientStopCollection GetGradientStops() => 
            this.gradientStops;

        public override int GetHashCode() => 
            ((base.Info.GetHashCode() ^ this.gradientStops.GetHashCode()) ^ this.tileRect.GetHashCode()) ^ this.fillRect.GetHashCode();

        protected virtual RectangleOffset GetTileRect() => 
            this.tileRect;

        private PropertyKey SetAngleCore(DrawingGradientFillInfo info, int value)
        {
            info.UseAngle = true;
            info.Angle = value;
            return AnglePropertyKey;
        }

        private void SetFillRect(RectangleOffset value)
        {
            GradientFillRectPropertyChangedHistoryItem item = new GradientFillRectPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fillRect, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected internal void SetFillRectCore(RectangleOffset value)
        {
            this.fillRect = value;
            base.Notifier.OnPropertyChanged(FillRectPropertyKey);
            base.InvalidateParent();
        }

        private PropertyKey SetFlipCore(DrawingGradientFillInfo info, TileFlipType value)
        {
            info.UseFlip = true;
            info.Flip = value;
            return FlipPropertyKey;
        }

        private PropertyKey SetGradientTypeCore(DrawingGradientFillInfo info, DevExpress.Office.Drawing.GradientType value)
        {
            info.UseGradientType = true;
            info.GradientType = value;
            return GradientTypePropertyKey;
        }

        private PropertyKey SetRotateWithShapeCore(DrawingGradientFillInfo info, bool value)
        {
            info.UseRotateWithShape = true;
            info.RotateWithShape = value;
            return RotateWithShapePropertyKey;
        }

        private PropertyKey SetScaledCore(DrawingGradientFillInfo info, bool value)
        {
            info.UseScaled = true;
            info.Scaled = value;
            return ScaledPropertyKey;
        }

        private void SetTileRect(RectangleOffset value)
        {
            GradientTileRectPropertyChangedHistoryItem item = new GradientTileRectPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.tileRect, value);
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        protected internal void SetTileRectCore(RectangleOffset value)
        {
            this.tileRect = value;
            base.Notifier.OnPropertyChanged(TileRectPropertyKey);
            base.InvalidateParent();
        }

        public void Visit(IDrawingFillVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GradientStopCollection GradientStops =>
            this.GetGradientStops();

        public DevExpress.Office.Drawing.GradientType GradientType
        {
            get => 
                base.Info.UseGradientType ? base.Info.GradientType : this.GetDefaultGradientType();
            set
            {
                if (!base.Info.UseGradientType || (base.Info.GradientType != value))
                {
                    this.SetPropertyValue<DevExpress.Office.Drawing.GradientType>(new UndoableIndexBasedObject<DrawingGradientFillInfo, PropertyKey>.SetPropertyValueDelegate<DevExpress.Office.Drawing.GradientType>(this.SetGradientTypeCore), value);
                }
            }
        }

        public bool UseGradientType =>
            base.Info.UseGradientType;

        public TileFlipType Flip
        {
            get => 
                base.Info.UseFlip ? base.Info.Flip : this.GetDefaultFlip();
            set
            {
                if (!base.Info.UseFlip || (base.Info.Flip != value))
                {
                    this.SetPropertyValue<TileFlipType>(new UndoableIndexBasedObject<DrawingGradientFillInfo, PropertyKey>.SetPropertyValueDelegate<TileFlipType>(this.SetFlipCore), value);
                }
            }
        }

        public bool UseFlip =>
            base.Info.UseFlip;

        public bool RotateWithShape
        {
            get => 
                base.Info.UseRotateWithShape ? base.Info.RotateWithShape : this.GetDefaultRotateWithShape();
            set
            {
                if (!base.Info.UseRotateWithShape || (base.Info.RotateWithShape != value))
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingGradientFillInfo, PropertyKey>.SetPropertyValueDelegate<bool>(this.SetRotateWithShapeCore), value);
                }
            }
        }

        public bool UseRotateWithShape =>
            base.Info.UseRotateWithShape;

        public bool Scaled
        {
            get => 
                base.Info.UseScaled ? base.Info.Scaled : this.GetDefaultScaled();
            set
            {
                if (!base.Info.UseScaled || (base.Info.Scaled != value))
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<DrawingGradientFillInfo, PropertyKey>.SetPropertyValueDelegate<bool>(this.SetScaledCore), value);
                }
            }
        }

        public bool UseScaled =>
            base.Info.UseScaled;

        public int Angle
        {
            get => 
                base.Info.UseAngle ? base.Info.Angle : this.GetDefaultAngle();
            set
            {
                if (!base.Info.UseAngle || (base.Info.Angle != value))
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<DrawingGradientFillInfo, PropertyKey>.SetPropertyValueDelegate<int>(this.SetAngleCore), value);
                }
            }
        }

        public bool UseAngle =>
            base.Info.UseAngle;

        public RectangleOffset TileRect
        {
            get => 
                this.GetTileRect();
            set
            {
                if (!this.tileRect.Equals(value))
                {
                    this.SetTileRect(value);
                }
            }
        }

        public RectangleOffset FillRect
        {
            get => 
                this.GetFillRect();
            set
            {
                if (!this.fillRect.Equals(value))
                {
                    this.SetFillRect(value);
                }
            }
        }

        public DrawingFillType FillType =>
            DrawingFillType.Gradient;

        DrawingUnderlineFillType IUnderlineFill.Type =>
            DrawingUnderlineFillType.Fill;
    }
}

