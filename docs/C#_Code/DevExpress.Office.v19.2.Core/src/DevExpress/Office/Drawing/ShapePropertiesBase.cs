namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class ShapePropertiesBase : DrawingUndoableIndexBasedObjectEx<ShapePropertiesInfo>, ISupportsCopyFrom<ShapePropertiesBase>, IFillOwner, ISupportsInvalidateNotify
    {
        public static readonly PropertyKey EffectStylePropertyKey = new PropertyKey(0);
        public static readonly PropertyKey FillPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey BlackAndWhiteModePropertyKey = new PropertyKey(2);
        public static readonly PropertyKey Transform2DPropertyKey = new PropertyKey(3);
        private readonly DrawingEffectStyle effectStyle;
        private IDrawingFill fill;
        private DevExpress.Office.Drawing.Transform2D transform2D;
        private bool blackAndWhitePrintMode;
        private SetFillEventHandler onSetFill;
        private EventHandler onFillChanged;

        protected internal event EventHandler Changed;

        public event EventHandler FillChanged
        {
            add
            {
                this.onFillChanged += value;
            }
            remove
            {
                this.onFillChanged -= value;
            }
        }

        public event SetFillEventHandler SetFill
        {
            add
            {
                this.onSetFill += value;
            }
            remove
            {
                this.onSetFill -= value;
            }
        }

        protected ShapePropertiesBase(IDocumentModel documentModel) : this(documentModel, new DevExpress.Office.Drawing.Transform2D(documentModel))
        {
        }

        protected ShapePropertiesBase(IDocumentModel documentModel, DevExpress.Office.Drawing.Transform2D transform2D) : base(documentModel.MainPart)
        {
            base.InnerParent.NotifyTarget = this;
            this.fill = this.GetDefaultFill();
            this.fill.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnFillChanged);
            this.effectStyle = new DrawingEffectStyle(base.DocumentModel);
            this.effectStyle.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnEffectStyleChanged);
            this.transform2D = transform2D;
            this.transform2D.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnTransform2DChanged);
        }

        public virtual void CopyFrom(ShapePropertiesBase value)
        {
            Guard.ArgumentNotNull(value, "ShapePropertiesBase");
            base.CopyFrom(value);
            this.Fill = value.Fill.CloneTo(base.DocumentModel);
            this.effectStyle.CopyFrom(value.effectStyle);
            this.transform2D.CopyFrom(value.transform2D);
        }

        public override PropertyKey GetBatchUpdateChangeActions() => 
            PropertyKey.Undefined;

        protected internal override UniqueItemsCache<ShapePropertiesInfo> GetCache(IDocumentModel documentModel) => 
            base.DocumentModel.DrawingCache.ShapePropertiesInfoCache;

        protected virtual IDrawingFill GetDefaultFill() => 
            DrawingFill.Automatic;

        public void InvalidateNotify()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        private void OnEffectStyleChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(EffectStylePropertyKey, sender, e);
        }

        private void OnFillChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(FillPropertyKey, sender, e);
        }

        private void OnTransform2DChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(Transform2DPropertyKey, sender, e);
        }

        private void RaiseFillChanged()
        {
            if (this.onFillChanged == null)
            {
                EventHandler onFillChanged = this.onFillChanged;
            }
            else
            {
                this.onFillChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseSetFill(IDrawingFill value)
        {
            if (this.onSetFill != null)
            {
                SetFillEventArgs e = new SetFillEventArgs(value);
                this.onSetFill(this, e);
            }
        }

        public void ResetToStyle()
        {
            this.ResetToStyle(false);
        }

        public void ResetToStyle(bool keepOutline)
        {
            base.DocumentModel.BeginUpdate();
            try
            {
                this.ResetToStyleCore(keepOutline);
            }
            finally
            {
                base.DocumentModel.EndUpdate();
            }
        }

        public virtual void ResetToStyleCore(bool keepOutline)
        {
            this.Fill = DrawingFill.Automatic;
        }

        public void RotateCore(int newValue)
        {
            this.Transform2D.RotateCore(newValue);
        }

        private PropertyKey SetBlackAndWhiteModeCore(ShapePropertiesInfo info, OpenXmlBlackWhiteMode value)
        {
            info.BlackAndWhiteMode = value;
            return BlackAndWhiteModePropertyKey;
        }

        public void SetDrawingFillCore(IDrawingFill value)
        {
            this.RaiseSetFill(value);
            this.fill.Parent = null;
            this.fill.PropertyChanged -= new EventHandler<OfficePropertyChangedEventArgs>(this.OnFillChanged);
            this.fill = value;
            this.fill.Parent = base.InnerParent;
            this.fill.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnFillChanged);
            base.InnerParent.Invalidate();
            base.Notifier.OnPropertyChanged(FillPropertyKey);
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

        public DrawingEffectStyle EffectStyle =>
            this.effectStyle;

        public IDrawingFill Fill
        {
            get => 
                this.fill;
            set
            {
                value ??= this.GetDefaultFill();
                if (!this.fill.Equals(value))
                {
                    DrawingFillPropertyChangedHistoryItem item = new DrawingFillPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fill, value);
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                    this.RaiseFillChanged();
                }
            }
        }

        public OpenXmlBlackWhiteMode BlackAndWhiteMode
        {
            get => 
                base.Info.BlackAndWhiteMode;
            set
            {
                if (this.BlackAndWhiteMode != value)
                {
                    this.SetPropertyValue<OpenXmlBlackWhiteMode>(new UndoableIndexBasedObject<ShapePropertiesInfo, PropertyKey>.SetPropertyValueDelegate<OpenXmlBlackWhiteMode>(this.SetBlackAndWhiteModeCore), value);
                }
            }
        }

        public DevExpress.Office.Drawing.Transform2D Transform2D =>
            this.transform2D;
    }
}

