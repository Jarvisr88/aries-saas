namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;

    public class Outline : DrawingUndoableIndexBasedObjectEx<OutlineInfo>, ICloneable<Outline>, ISupportsCopyFrom<Outline>, IFillOwner, IStrokeUnderline
    {
        public static readonly PropertyKey FillPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey JoinStylePropertyKey = new PropertyKey(1);
        public static readonly PropertyKey DashingPropertyKey = new PropertyKey(2);
        public static readonly PropertyKey HeadLengthPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey HeadWidthPropertyKey = new PropertyKey(4);
        public static readonly PropertyKey HeadTypePropertyKey = new PropertyKey(5);
        public static readonly PropertyKey TailLengthPropertyKey = new PropertyKey(6);
        public static readonly PropertyKey TailWidthPropertyKey = new PropertyKey(7);
        public static readonly PropertyKey TailTypePropertyKey = new PropertyKey(8);
        public static readonly PropertyKey StrokeAlignmentPropertyKey = new PropertyKey(9);
        public static readonly PropertyKey EndCapStylePropertyKey = new PropertyKey(10);
        public static readonly PropertyKey CompoundTypePropertyKey = new PropertyKey(11);
        public static readonly PropertyKey WidthPropertyKey = new PropertyKey(12);
        public static readonly PropertyKey MiterLimitPropertyKey = new PropertyKey(13);
        private IDrawingFill fill;
        private SetFillEventHandler onSetFill;

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

        public Outline(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.fill = DrawingFill.Automatic;
        }

        public Outline Clone()
        {
            Outline outline = new Outline(base.DocumentModel);
            outline.CopyFrom(this);
            return outline;
        }

        public Outline CloneTo(IDocumentModel documentModel)
        {
            Outline outline = new Outline(documentModel);
            outline.CopyFrom(this);
            return outline;
        }

        public void CopyFrom(Outline value)
        {
            base.CopyFrom(value);
            this.Fill = value.Fill.CloneTo(base.DocumentModel);
        }

        private DrawingGradientStop CreateGradientStop(int position, SchemeColorValues schemeColor, int tint, int saturationModulation)
        {
            DrawingGradientStop stop = new DrawingGradientStop(base.DocumentModel) {
                Position = position
            };
            stop.Color.BeginUpdate();
            try
            {
                stop.Color.OriginalColor.Scheme = schemeColor;
                stop.Color.Transforms.Add(new TintColorTransform(tint));
                stop.Color.Transforms.Add(new SaturationModulationColorTransform(saturationModulation));
            }
            finally
            {
                stop.Color.EndUpdate();
            }
            return stop;
        }

        IStrokeUnderline IStrokeUnderline.CloneTo(IDocumentModel documentModel)
        {
            Outline outline = new Outline(documentModel);
            outline.CopyFrom(this);
            return outline;
        }

        public override bool Equals(object obj)
        {
            Outline outline = obj as Outline;
            return ((outline != null) ? (base.Info.Equals(outline.Info) && this.fill.Equals(outline.fill)) : false);
        }

        protected internal override UniqueItemsCache<OutlineInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.OutlineInfoCache;

        public override int GetHashCode() => 
            base.Info.GetHashCode() ^ this.fill.GetHashCode();

        private bool IsValidOutlineFill(IDrawingFill value) => 
            (value.FillType == DrawingFillType.Automatic) || ((value.FillType == DrawingFillType.Gradient) || ((value.FillType == DrawingFillType.None) || ((value.FillType == DrawingFillType.Pattern) || (value.FillType == DrawingFillType.Solid))));

        private void OnFillChanged(object sender, OfficePropertyChangedEventArgs e)
        {
            base.Notifier.OnPropertyChanged(FillPropertyKey, sender, e);
        }

        protected internal void RaiseSetFill(IDrawingFill value)
        {
            if (this.onSetFill != null)
            {
                SetFillEventArgs e = new SetFillEventArgs(value);
                this.onSetFill(this, e);
            }
        }

        public void ResetToStyle()
        {
            if (base.IsUpdateLocked)
            {
                base.Info.CopyFrom(base.DrawingCache.OutlineInfoCache.DefaultItem);
            }
            else
            {
                this.ChangeIndexCore(0, PropertyKey.Undefined);
            }
            this.Fill = DrawingFill.Automatic;
        }

        private PropertyKey SetCompoundTypeCore(OutlineInfo info, OutlineCompoundType value)
        {
            info.CompoundType = value;
            info.HasCompoundType = true;
            return CompoundTypePropertyKey;
        }

        public void SetDrawingFillCore(IDrawingFill value)
        {
            this.RaiseSetFill(value);
            this.fill.Parent = null;
            this.fill.PropertyChanged -= new EventHandler<OfficePropertyChangedEventArgs>(this.OnFillChanged);
            this.fill = value;
            this.fill.Parent = base.InnerParent;
            this.fill.PropertyChanged += new EventHandler<OfficePropertyChangedEventArgs>(this.OnFillChanged);
            base.Notifier.OnPropertyChanged(FillPropertyKey);
            base.InvalidateParent();
        }

        private PropertyKey SetEndCapStyleCore(OutlineInfo info, OutlineEndCapStyle value)
        {
            info.EndCapStyle = value;
            info.HasEndCapStyle = true;
            return EndCapStylePropertyKey;
        }

        private PropertyKey SetHeadLengthCore(OutlineInfo info, OutlineHeadTailSize value)
        {
            info.HeadLength = value;
            info.HasHeadLength = true;
            return HeadLengthPropertyKey;
        }

        private PropertyKey SetHeadTypeCore(OutlineInfo info, OutlineHeadTailType value)
        {
            info.HeadType = value;
            info.HasHeadType = true;
            return HeadTypePropertyKey;
        }

        private PropertyKey SetHeadWidthCore(OutlineInfo info, OutlineHeadTailSize value)
        {
            info.HeadWidth = value;
            info.HasHeadWidth = true;
            return HeadWidthPropertyKey;
        }

        private PropertyKey SetJoinStyleCore(OutlineInfo info, LineJoinStyle value)
        {
            info.JoinStyle = value;
            info.HasLineJoinStyle = true;
            return JoinStylePropertyKey;
        }

        private PropertyKey SetMiterLimitCore(OutlineInfo info, int value)
        {
            info.MiterLimit = value;
            info.HasMiterLimit = true;
            return MiterLimitPropertyKey;
        }

        private PropertyKey SetOutlineDashingCore(OutlineInfo info, OutlineDashing value)
        {
            info.Dashing = value;
            info.HasDashing = true;
            return DashingPropertyKey;
        }

        public void SetOutlineWidthCore(int value)
        {
            this.SetOutlineWidthCore(base.Info, value);
        }

        private PropertyKey SetOutlineWidthCore(OutlineInfo info, int value)
        {
            info.Width = value;
            info.HasWidth = true;
            return WidthPropertyKey;
        }

        private PropertyKey SetStrokeAlignmentCore(OutlineInfo info, OutlineStrokeAlignment value)
        {
            info.StrokeAlignment = value;
            info.HasStrokeAlignment = true;
            return StrokeAlignmentPropertyKey;
        }

        private PropertyKey SetTailLengthCore(OutlineInfo info, OutlineHeadTailSize value)
        {
            info.TailLength = value;
            info.HasTailLength = true;
            return TailLengthPropertyKey;
        }

        private PropertyKey SetTailTypeCore(OutlineInfo info, OutlineHeadTailType value)
        {
            info.TailType = value;
            info.HasTailType = true;
            return TailTypePropertyKey;
        }

        private PropertyKey SetTailWidthCore(OutlineInfo info, OutlineHeadTailSize value)
        {
            info.TailWidth = value;
            info.HasTailWidth = true;
            return TailWidthPropertyKey;
        }

        public IDrawingFill Fill
        {
            get => 
                this.fill;
            set
            {
                value ??= DrawingFill.Automatic;
                if (!this.IsValidOutlineFill(value))
                {
                    throw new ArgumentException("Wrong outline fill type.");
                }
                if (!this.fill.Equals(value))
                {
                    HistoryItem item = new DrawingFillPropertyChangedHistoryItem(base.DocumentModel.MainPart, this, this.fill, value);
                    base.DocumentModel.History.Add(item);
                    item.Execute();
                }
            }
        }

        public bool HasLineJoinStyle =>
            base.Info.HasLineJoinStyle;

        public LineJoinStyle JoinStyle
        {
            get => 
                base.Info.JoinStyle;
            set
            {
                if ((this.JoinStyle != value) || !this.HasLineJoinStyle)
                {
                    this.SetPropertyValue<LineJoinStyle>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<LineJoinStyle>(this.SetJoinStyleCore), value);
                }
            }
        }

        public bool HasDashing =>
            base.Info.HasDashing;

        public OutlineDashing Dashing
        {
            get => 
                base.Info.Dashing;
            set
            {
                if ((this.Dashing != value) || !this.HasDashing)
                {
                    this.SetPropertyValue<OutlineDashing>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineDashing>(this.SetOutlineDashingCore), value);
                }
            }
        }

        public OutlineHeadTailSize HeadLength
        {
            get => 
                base.Info.HeadLength;
            set
            {
                if (!this.HasHeadLength || (this.HeadLength != value))
                {
                    this.SetPropertyValue<OutlineHeadTailSize>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailSize>(this.SetHeadLengthCore), value);
                }
            }
        }

        public bool HasHeadLength =>
            base.Info.HasHeadLength;

        public OutlineHeadTailSize HeadWidth
        {
            get => 
                base.Info.HeadWidth;
            set
            {
                if (!this.HasHeadWidth || (this.HeadWidth != value))
                {
                    this.SetPropertyValue<OutlineHeadTailSize>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailSize>(this.SetHeadWidthCore), value);
                }
            }
        }

        public bool HasHeadWidth =>
            base.Info.HasHeadWidth;

        public OutlineHeadTailType HeadType
        {
            get => 
                base.Info.HeadType;
            set
            {
                if (!this.HasHeadType || (this.HeadType != value))
                {
                    this.SetPropertyValue<OutlineHeadTailType>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailType>(this.SetHeadTypeCore), value);
                }
            }
        }

        public bool HasHeadType =>
            base.Info.HasHeadType;

        public OutlineHeadTailSize TailLength
        {
            get => 
                base.Info.TailLength;
            set
            {
                if (!this.HasTailLength || (this.TailLength != value))
                {
                    this.SetPropertyValue<OutlineHeadTailSize>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailSize>(this.SetTailLengthCore), value);
                }
            }
        }

        public bool HasTailLength =>
            base.Info.HasTailLength;

        public OutlineHeadTailSize TailWidth
        {
            get => 
                base.Info.TailWidth;
            set
            {
                if (!this.HasTailWidth || (this.TailWidth != value))
                {
                    this.SetPropertyValue<OutlineHeadTailSize>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailSize>(this.SetTailWidthCore), value);
                }
            }
        }

        public bool HasTailWidth =>
            base.Info.HasTailWidth;

        public OutlineHeadTailType TailType
        {
            get => 
                base.Info.TailType;
            set
            {
                if (!this.HasTailType || (this.TailType != value))
                {
                    this.SetPropertyValue<OutlineHeadTailType>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineHeadTailType>(this.SetTailTypeCore), value);
                }
            }
        }

        public bool HasTailType =>
            base.Info.HasTailType;

        public OutlineStrokeAlignment StrokeAlignment
        {
            get => 
                base.Info.StrokeAlignment;
            set
            {
                if (!this.HasStrokeAlignment || (this.StrokeAlignment != value))
                {
                    this.SetPropertyValue<OutlineStrokeAlignment>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineStrokeAlignment>(this.SetStrokeAlignmentCore), value);
                }
            }
        }

        public bool HasStrokeAlignment =>
            base.Info.HasStrokeAlignment;

        public OutlineEndCapStyle EndCapStyle
        {
            get => 
                base.Info.EndCapStyle;
            set
            {
                if (!this.HasEndCapStyle || (this.EndCapStyle != value))
                {
                    this.SetPropertyValue<OutlineEndCapStyle>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineEndCapStyle>(this.SetEndCapStyleCore), value);
                }
            }
        }

        public bool HasEndCapStyle =>
            base.Info.HasEndCapStyle;

        public bool HasCompoundType =>
            base.Info.HasCompoundType;

        public OutlineCompoundType CompoundType
        {
            get => 
                base.Info.CompoundType;
            set
            {
                if ((this.CompoundType != value) || !this.HasCompoundType)
                {
                    this.SetPropertyValue<OutlineCompoundType>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<OutlineCompoundType>(this.SetCompoundTypeCore), value);
                }
            }
        }

        public bool HasWidth =>
            base.Info.HasWidth;

        public int Width
        {
            get => 
                base.Info.Width;
            set
            {
                if ((this.Width != value) || !this.HasWidth)
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<int>(this.SetOutlineWidthCore), value);
                }
            }
        }

        public int MiterLimit
        {
            get => 
                base.Info.MiterLimit;
            set
            {
                if (!this.HasMiterLimit || (this.MiterLimit != value))
                {
                    this.SetPropertyValue<int>(new UndoableIndexBasedObject<OutlineInfo, PropertyKey>.SetPropertyValueDelegate<int>(this.SetMiterLimitCore), value);
                }
            }
        }

        public bool HasMiterLimit =>
            base.Info.HasMiterLimit;

        public OutlineType Type
        {
            get => 
                (this.Fill.FillType != DrawingFillType.Solid) ? ((this.Fill.FillType != DrawingFillType.Pattern) ? ((this.Fill.FillType != DrawingFillType.Gradient) ? OutlineType.None : OutlineType.Gradient) : OutlineType.Pattern) : OutlineType.Solid;
            set
            {
                if ((this.Type != value) || (this.Fill.FillType == DrawingFillType.Automatic))
                {
                    base.DocumentModel.History.BeginTransaction();
                    try
                    {
                        if (value == OutlineType.Solid)
                        {
                            DrawingSolidFill fill = new DrawingSolidFill(base.DocumentModel) {
                                Color = { OriginalColor = { Scheme = SchemeColorValues.Text1 } }
                            };
                            this.Fill = fill;
                        }
                        else if (value == OutlineType.Pattern)
                        {
                            DrawingPatternFill fill2 = new DrawingPatternFill(base.DocumentModel) {
                                PatternType = DrawingPatternType.Percent5,
                                ForegroundColor = { OriginalColor = { Scheme = SchemeColorValues.Accent1 } }
                            };
                            fill2.BackgroundColor.OriginalColor.Scheme = SchemeColorValues.Background1;
                            this.Fill = fill2;
                        }
                        else if (value != OutlineType.Gradient)
                        {
                            this.Fill = DrawingFill.None;
                        }
                        else
                        {
                            DrawingGradientFill fill3 = new DrawingGradientFill(base.DocumentModel);
                            fill3.GradientStops.Add(this.CreateGradientStop(0, SchemeColorValues.Accent1, 0x101d0, 0x27100));
                            fill3.GradientStops.Add(this.CreateGradientStop(0xc350, SchemeColorValues.Accent1, 0xadd4, 0x27100));
                            fill3.GradientStops.Add(this.CreateGradientStop(0x186a0, SchemeColorValues.Accent1, 0x5bcc, 0x27100));
                            fill3.Angle = base.DocumentModel.UnitConverter.ModelUnitsToAdjAngle(0x5265c0);
                            this.Fill = fill3;
                        }
                    }
                    finally
                    {
                        base.DocumentModel.History.EndTransaction();
                    }
                }
            }
        }

        public bool IsDefault =>
            ReferenceEquals(this.fill, DrawingFill.Automatic) && (base.Index == 0);

        public DrawingColor Color
        {
            get => 
                DrawingFill.GetColor(base.DocumentModel, this.Fill);
            set
            {
                Guard.ArgumentNotNull(value, "OutlineColor");
                if ((this.Fill.FillType != DrawingFillType.Solid) || !this.Color.Equals(value))
                {
                    base.DocumentModel.History.BeginTransaction();
                    try
                    {
                        if (this.Fill.FillType != DrawingFillType.Solid)
                        {
                            DrawingSolidFill fill = new DrawingSolidFill(base.DocumentModel);
                            this.Fill = fill;
                        }
                        ((DrawingSolidFill) this.Fill).Color.CopyFrom(value);
                    }
                    finally
                    {
                        base.DocumentModel.History.EndTransaction();
                    }
                }
            }
        }

        DrawingStrokeUnderlineType IStrokeUnderline.Type =>
            DrawingStrokeUnderlineType.Outline;
    }
}

