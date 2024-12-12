namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingBlipFill : DrawingMultiIndexObject<DrawingBlipFill, PropertyKey>, ICloneable<DrawingBlipFill>, ISupportsCopyFrom<DrawingBlipFill>, IDrawingFill, IOfficeNotifyPropertyChanged, IUnderlineFill
    {
        public static readonly PropertyKey DpiPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey RotateWithShapePropertyKey = new PropertyKey(1);
        public static readonly PropertyKey StretchPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey SourceRectanglePropertyKey = new PropertyKey(3);
        public static readonly PropertyKey FillRectanglePropertyKey = new PropertyKey(4);
        public static readonly PropertyKey TileAlignPropertyKey = new PropertyKey(5);
        public static readonly PropertyKey TileFlipPropertyKey = new PropertyKey(6);
        public static readonly PropertyKey ScaleXPropertyKey = new PropertyKey(7);
        public static readonly PropertyKey ScaleYPropertyKey = new PropertyKey(8);
        public static readonly PropertyKey OffsetXPropertyKey = new PropertyKey(9);
        public static readonly PropertyKey OffsetYPropertyKey = new PropertyKey(10);
        public static readonly PropertyKey BlipPropertyKey = new PropertyKey(11);
        private static readonly DrawingBlipFillInfoIndexAccessor fillInfoIndexAccessor = new DrawingBlipFillInfoIndexAccessor();
        private static readonly DrawingBlipTileInfoIndexAccessor tileInfoIndexAccessor = new DrawingBlipTileInfoIndexAccessor();
        private static readonly IIndexAccessorBase<DrawingBlipFill, PropertyKey>[] indexAccessors = new IIndexAccessorBase<DrawingBlipFill, PropertyKey>[] { fillInfoIndexAccessor, tileInfoIndexAccessor };
        private readonly DrawingBlip blip;
        private int fillInfoIndex;
        private int tileInfoIndex;
        private RectangleOffset sourceRectangle;
        private RectangleOffset fillRectangle;
        private readonly PropertyChangedNotifier notifier;
        private bool blackAndWhitePrintMode;

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

        public DrawingBlipFill(IDocumentModel documentModel) : base(documentModel)
        {
            this.notifier = new PropertyChangedNotifier(this);
            this.blip = new DrawingBlip(documentModel);
            this.blip.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnBlipPropertyChanged);
            this.blip.Parent = base.InnerParent;
            this.sourceRectangle = RectangleOffset.Empty;
            this.fillRectangle = RectangleOffset.Empty;
        }

        protected internal override void ApplyChanges(PropertyKey propertyKey)
        {
            base.ApplyChanges(propertyKey);
            this.notifier.OnPropertyChanged(propertyKey);
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            base.DocumentModel.History.Add(item);
            item.Execute();
        }

        public void AssignFillInfoIndex(int value)
        {
            this.fillInfoIndex = value;
        }

        internal void AssignInfoes(DrawingBlipFillInfo fillInfo, DrawingBlipTileInfo tileInfo)
        {
            this.AssignFillInfoIndex(base.DrawingCache.DrawingBlipFillInfoCache.AddItem(fillInfo));
            this.AssignTileInfoIndex(base.DrawingCache.DrawingBlipTileInfoCache.AddItem(tileInfo));
        }

        public void AssignTileInfoIndex(int value)
        {
            this.tileInfoIndex = value;
        }

        public DrawingBlipFill Clone()
        {
            DrawingBlipFill fill = new DrawingBlipFill(base.DocumentModel);
            fill.CopyFrom(this);
            return fill;
        }

        public IDrawingFill CloneTo(IDocumentModel documentModel)
        {
            DrawingBlipFill fill = new DrawingBlipFill(documentModel);
            fill.CopyFrom(this);
            return fill;
        }

        public void CopyFrom(DrawingBlipFill value)
        {
            Guard.ArgumentNotNull(value, "value");
            base.CopyFrom(value);
            this.blip.CopyFrom(value.blip);
            this.SourceRectangle = value.SourceRectangle;
            this.FillRectangle = value.FillRectangle;
        }

        public static DrawingBlipFill Create(IDocumentModel documentModel, DrawingBlipFillInfo fillInfo) => 
            Create(documentModel, fillInfo, new DrawingBlipTileInfo());

        public static DrawingBlipFill Create(IDocumentModel documentModel, OfficeImage image) => 
            new DrawingBlipFill(documentModel) { 
                Blip = { Image = image },
                Stretch = true
            };

        public static DrawingBlipFill Create(IDocumentModel documentModel, string fileName)
        {
            DrawingBlipFill fill = new DrawingBlipFill(documentModel);
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                fill.Blip.Image = documentModel.CreateImage(stream);
            }
            fill.Stretch = true;
            return fill;
        }

        public static DrawingBlipFill Create(IDocumentModel documentModel, DrawingBlipFillInfo fillInfo, DrawingBlipTileInfo tileInfo)
        {
            DrawingBlipFill fill = new DrawingBlipFill(documentModel);
            fill.AssignInfoes(fillInfo, tileInfo);
            return fill;
        }

        protected override MultiIndexBatchUpdateHelper CreateBatchInitHelper() => 
            new DrawingBlipFillBatchInitHelper(this);

        protected override MultiIndexBatchUpdateHelper CreateBatchUpdateHelper() => 
            new DrawingBlipFillBatchUpdateHelper(this);

        IUnderlineFill IUnderlineFill.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel) as IUnderlineFill;

        public override bool Equals(object obj)
        {
            DrawingBlipFill fill = obj as DrawingBlipFill;
            if (fill == null)
            {
                return false;
            }
            if (fill.FillType != DrawingFillType.Picture)
            {
                return false;
            }
            DrawingBlipFill fill2 = fill;
            return (base.Equals(fill2) && (this.blip.Equals(fill2.blip) && (this.sourceRectangle.Equals(fill2.sourceRectangle) && this.fillRectangle.Equals(fill2.fillRectangle))));
        }

        public override PropertyKey GetBatchUpdateChangeActions() => 
            PropertyKey.Undefined;

        public override int GetHashCode() => 
            ((base.GetHashCode() ^ this.blip.GetHashCode()) ^ this.sourceRectangle.GetHashCode()) ^ this.fillRectangle.GetHashCode();

        internal float GetOffsetXInPixels() => 
            base.DocumentModel.UnitConverter.ModelUnitsToPixelsF((float) this.OffsetX);

        internal float GetOffsetYInPixels() => 
            base.DocumentModel.UnitConverter.ModelUnitsToPixelsF((float) this.OffsetY);

        public override DrawingBlipFill GetOwner() => 
            this;

        private void OnBlipPropertyChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            this.notifier.OnPropertyChanged(BlipPropertyKey, sender, e);
        }

        private PropertyKey SetDpiCore(DrawingBlipFillInfo info, int value)
        {
            info.Dpi = value;
            return DpiPropertyKey;
        }

        private void SetFillRectangle(RectangleOffset value)
        {
            base.DocumentModel.History.BeginTransaction();
            try
            {
                this.ApplyHistoryItem(new FillRectangleHistoryItem(this, this.fillRectangle, value));
                if (!Equals(value, RectangleOffset.Empty))
                {
                    this.Stretch = true;
                }
            }
            finally
            {
                base.DocumentModel.History.EndTransaction();
            }
        }

        public void SetFillRectangleCore(RectangleOffset value)
        {
            this.fillRectangle = value;
            this.notifier.OnPropertyChanged(FillRectanglePropertyKey);
            base.InvalidateParent();
        }

        private PropertyKey SetOffsetXCore(DrawingBlipTileInfo info, long value)
        {
            info.OffsetX = value;
            return OffsetXPropertyKey;
        }

        private PropertyKey SetOffsetYCore(DrawingBlipTileInfo info, long value)
        {
            info.OffsetY = value;
            return OffsetYPropertyKey;
        }

        protected override void SetPropertyValueCore<TInfo, U>(IIndexAccessor<DrawingBlipFill, TInfo, PropertyKey> indexHolder, MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<TInfo, U> setter, U newValue) where TInfo: ICloneable<TInfo>, ISupportsSizeOf
        {
            base.SetPropertyValueCore<TInfo, U>(indexHolder, setter, newValue);
        }

        private PropertyKey SetRotateWithShapeCore(DrawingBlipFillInfo info, bool value)
        {
            info.RotateWithShape = value;
            return RotateWithShapePropertyKey;
        }

        private PropertyKey SetScaleXCore(DrawingBlipTileInfo info, int value)
        {
            info.ScaleX = value;
            return ScaleXPropertyKey;
        }

        private PropertyKey SetScaleYCore(DrawingBlipTileInfo info, int value)
        {
            info.ScaleY = value;
            return ScaleYPropertyKey;
        }

        public void SetSourceRectangleCore(RectangleOffset value)
        {
            this.sourceRectangle = value;
            this.notifier.OnPropertyChanged(SourceRectanglePropertyKey);
            base.InvalidateParent();
        }

        private PropertyKey SetStretchCore(DrawingBlipFillInfo info, bool value)
        {
            info.Stretch = value;
            return StretchPropertyKey;
        }

        private PropertyKey SetTileAlignCore(DrawingBlipTileInfo info, RectangleAlignType value)
        {
            info.TileAlign = value;
            return TileAlignPropertyKey;
        }

        private PropertyKey SetTileFlipCore(DrawingBlipTileInfo info, TileFlipType value)
        {
            info.TileFlip = value;
            return TileFlipPropertyKey;
        }

        public void Visit(IDrawingFillVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static DrawingBlipFillInfoIndexAccessor FillInfoIndexAccessor =>
            fillInfoIndexAccessor;

        public static DrawingBlipTileInfoIndexAccessor TileInfoIndexAccessor =>
            tileInfoIndexAccessor;

        public int FillInfoIndex =>
            this.fillInfoIndex;

        public int TileInfoIndex =>
            this.tileInfoIndex;

        protected override IIndexAccessorBase<DrawingBlipFill, PropertyKey>[] IndexAccessors =>
            indexAccessors;

        internal DrawingBlipFillBatchUpdateHelper BatchUpdateHelper =>
            (DrawingBlipFillBatchUpdateHelper) base.BatchUpdateHelper;

        protected internal DrawingBlipFillInfo FillInfo =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.BlipFillInfo : this.FillInfoCore;

        protected internal DrawingBlipTileInfo TileInfo =>
            base.IsUpdateLocked ? this.BatchUpdateHelper.BlipTileInfo : this.TileInfoCore;

        private DrawingBlipFillInfo FillInfoCore =>
            fillInfoIndexAccessor.GetInfo(this);

        private DrawingBlipTileInfo TileInfoCore =>
            tileInfoIndexAccessor.GetInfo(this);

        public int Dpi
        {
            get => 
                this.FillInfo.Dpi;
            set
            {
                if (this.Dpi != value)
                {
                    this.SetPropertyValue<DrawingBlipFillInfo, int>(fillInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipFillInfo, int>(this.SetDpiCore), value);
                }
            }
        }

        public bool RotateWithShape
        {
            get => 
                this.FillInfo.RotateWithShape;
            set
            {
                if (this.RotateWithShape != value)
                {
                    this.SetPropertyValue<DrawingBlipFillInfo, bool>(fillInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipFillInfo, bool>(this.SetRotateWithShapeCore), value);
                }
            }
        }

        public bool Stretch
        {
            get => 
                this.FillInfo.Stretch;
            set
            {
                if (this.Stretch != value)
                {
                    this.SetPropertyValue<DrawingBlipFillInfo, bool>(fillInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipFillInfo, bool>(this.SetStretchCore), value);
                }
            }
        }

        public DrawingBlip Blip =>
            this.blip;

        public RectangleOffset SourceRectangle
        {
            get => 
                this.sourceRectangle;
            set
            {
                if (!this.sourceRectangle.Equals(value))
                {
                    this.ApplyHistoryItem(new SourceRectangleHistoryItem(this, this.sourceRectangle, value));
                }
            }
        }

        public RectangleOffset FillRectangle
        {
            get => 
                this.fillRectangle;
            set
            {
                if (!this.fillRectangle.Equals(value))
                {
                    this.SetFillRectangle(value);
                }
            }
        }

        public RectangleAlignType TileAlign
        {
            get => 
                this.TileInfo.TileAlign;
            set
            {
                if (this.TileAlign != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, RectangleAlignType>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, RectangleAlignType>(this.SetTileAlignCore), value);
                }
            }
        }

        public TileFlipType TileFlip
        {
            get => 
                this.TileInfo.TileFlip;
            set
            {
                if (this.TileFlip != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, TileFlipType>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, TileFlipType>(this.SetTileFlipCore), value);
                }
            }
        }

        public int ScaleX
        {
            get => 
                this.TileInfo.ScaleX;
            set
            {
                if (this.ScaleX != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, int>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, int>(this.SetScaleXCore), value);
                }
            }
        }

        public int ScaleY
        {
            get => 
                this.TileInfo.ScaleY;
            set
            {
                if (this.ScaleY != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, int>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, int>(this.SetScaleYCore), value);
                }
            }
        }

        public long OffsetX
        {
            get => 
                this.TileInfo.OffsetX;
            set
            {
                if (this.OffsetX != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, long>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, long>(this.SetOffsetXCore), value);
                }
            }
        }

        public long OffsetY
        {
            get => 
                this.TileInfo.OffsetY;
            set
            {
                if (this.OffsetY != value)
                {
                    this.SetPropertyValue<DrawingBlipTileInfo, long>(tileInfoIndexAccessor, new MultiIndexObject<DrawingBlipFill, PropertyKey>.SetPropertyValueDelegate<DrawingBlipTileInfo, long>(this.SetOffsetYCore), value);
                }
            }
        }

        public bool BlackAndWhitePrintMode
        {
            get => 
                this.blackAndWhitePrintMode;
            set
            {
                if (this.BlackAndWhitePrintMode != value)
                {
                    this.blackAndWhitePrintMode = value;
                }
            }
        }

        public DrawingFillType FillType =>
            DrawingFillType.Picture;

        DrawingUnderlineFillType IUnderlineFill.Type =>
            DrawingUnderlineFillType.Fill;
    }
}

