namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing.Core.Native;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.LayoutAdjustment;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    [BrickExporter(typeof(VisualBrickExporter))]
    public class VisualBrick : Brick, IVisualBrick, IBaseBrick, IBrick, IXtraPartlyDeserializable
    {
        private BrickStyle fStyle;
        private IBrickOwner brickOwner;

        public VisualBrick() : this(new BrickStyle(BrickGraphics.InternalBrickStyle))
        {
        }

        public VisualBrick(BrickStyle style) : this(style, NullBrickOwner.Instance)
        {
        }

        public VisualBrick(IBrickOwner brickOwner) : this(new BrickStyle(BrickGraphics.InternalBrickStyle), brickOwner)
        {
        }

        internal VisualBrick(VisualBrick brick) : base(brick)
        {
            this.brickOwner = NullBrickOwner.Instance;
            this.fStyle = brick.Style;
            this.brickOwner = brick.brickOwner;
            this.CanGrow = brick.CanGrow;
            this.CanShrink = brick.CanShrink;
            this.AutoWidth = brick.AutoWidth;
            this.RightToLeftLayout = brick.RightToLeftLayout;
        }

        private VisualBrick(BrickStyle style, IBrickOwner brickOwner)
        {
            this.brickOwner = NullBrickOwner.Instance;
            this.fStyle = style;
            this.brickOwner = brickOwner;
            this.CanGrow = false;
            this.CanShrink = false;
            this.AutoWidth = false;
        }

        public VisualBrick(BorderSide sides, float borderWidth, Color borderColor, Color backColor) : this(new BrickStyle(sides, borderWidth, borderColor, backColor, BrickGraphics.InternalBrickStyle.ForeColor, BrickGraphics.InternalBrickStyle.Font, BrickGraphics.InternalBrickStyle.StringFormat))
        {
        }

        protected internal override bool AfterPrintOnPage(IList<int> indices, RectangleF brickBounds, RectangleF clipRect, Page page, int pageIndex, int pageCount, Action<BrickBase, RectangleF> callback)
        {
            if (!this.BrickOwner.RaiseAfterPrintOnPage(this, page, pageIndex, pageCount))
            {
                return false;
            }
            base.AfterPrintOnPage(indices, brickBounds, clipRect, page, pageIndex, pageCount, callback);
            return true;
        }

        public override object Clone() => 
            new VisualBrick(this);

        internal static DashStyle ConvertDashStyle(DevExpress.XtraPrinting.BorderDashStyle dashStyle)
        {
            switch (dashStyle)
            {
                case DevExpress.XtraPrinting.BorderDashStyle.Dash:
                    return DashStyle.Dash;

                case DevExpress.XtraPrinting.BorderDashStyle.Dot:
                    return DashStyle.Dot;

                case DevExpress.XtraPrinting.BorderDashStyle.DashDot:
                    return DashStyle.DashDot;

                case DevExpress.XtraPrinting.BorderDashStyle.DashDotDot:
                    return DashStyle.DashDotDot;
            }
            return DashStyle.Solid;
        }

        protected override object CreateContentPropertyValue(XtraItemEventArgs e) => 
            (e.Item.Name != "Style") ? base.CreateContentPropertyValue(e) : BrickFactory.CreateBrickStyle(e);

        public virtual ILayoutData CreateLayoutData(float dpi) => 
            new VisualBrickLayoutData(this, dpi);

        void IXtraPartlyDeserializable.Deserialize(object rootObject, IXtraPropertyCollection properties)
        {
            if (!(rootObject is StreamingSerializationRootObject))
            {
                this.NavigationPair = ((Document) rootObject).CreateBrickPagePair(properties["NavigationPageIndex"], properties["NavigationBrickIndices"]);
            }
        }

        protected static BorderSide GetAreaBorderSides(BorderSide sides, RectangleF area, RectangleF baseBounds)
        {
            if (area.Left != baseBounds.Left)
            {
                sides &= ~BorderSide.Left;
            }
            if (area.Top != baseBounds.Top)
            {
                sides &= ~BorderSide.Top;
            }
            if (area.Right != baseBounds.Right)
            {
                sides &= ~BorderSide.Right;
            }
            if (area.Bottom != baseBounds.Bottom)
            {
                sides &= ~BorderSide.Bottom;
            }
            return sides;
        }

        internal static BrickStyle GetAreaStyle(StylesContainer styles, BrickStyle style, RectangleF area, RectangleF baseBounds)
        {
            BrickStyle style2 = BrickStyleHelper.Instance.ChangeSides(style, GetAreaBorderSides(style.Sides, area, baseBounds));
            return styles.GetStyle(style2);
        }

        internal RectangleF GetBounds(float dpi) => 
            GraphicsUnitConverter.Convert(this.InitialRect, (float) 300f, dpi);

        public virtual RectangleF GetClientRectangle(RectangleF rect, float dpi) => 
            this.Style.DeflateBorderWidth(rect, dpi);

        internal static float[] GetDashPattern(DevExpress.XtraPrinting.BorderDashStyle dashStyle) => 
            GetDashPattern(ConvertDashStyle(dashStyle));

        internal static float[] GetDashPattern(DashStyle style)
        {
            switch (style)
            {
                case DashStyle.Dash:
                    return new float[] { 3f, 3f };

                case DashStyle.Dot:
                    return new float[] { 1f, 2f };

                case DashStyle.DashDot:
                    return new float[] { 5f, 3f, 1f, 3f };

                case DashStyle.DashDotDot:
                    return new float[] { 5f, 3f, 1f, 3f, 1f, 3f };
            }
            return new float[0];
        }

        public float GetScaleFactor(IPrintingSystemContext context)
        {
            BrickModifier[] modifiers = new BrickModifier[] { BrickModifier.MarginalHeader, BrickModifier.MarginalFooter };
            return (!base.HasModifier(modifiers) ? ((context.DrawingPage != null) ? context.DrawingPage.ScaleFactor : context.PrintingSystem.Document.ScaleFactor) : 1f);
        }

        private void OnNavigationPairUpdated()
        {
            StoredID storedID = this.StoredID;
            if (!storedID.IsUndefined)
            {
                PrintingSystemBase printingSystem = this.PrintingSystem;
                IStreamingDocument document = (printingSystem != null) ? (printingSystem.Document as IStreamingDocument) : null;
                if (document != null)
                {
                    document.UpdatedObjects.UpdateProperty(storedID, PSUpdatedObjects.NavigationPageIdProperty, this.NavigationPageId);
                    document.UpdatedObjects.UpdateProperty(storedID, PSUpdatedObjects.NavigationBrickIndicesProperty, this.NavigationBrickIndices);
                    document.UpdatedObjects.UpdateProperty(storedID, PSUpdatedObjects.NavigationBrickBoundsProperty, this.NavigationPair.BrickBounds);
                }
            }
        }

        protected override void OnSetPrintingSystem(bool cacheStyle)
        {
            base.OnSetPrintingSystem(cacheStyle);
            if ((this.fStyle != null) & cacheStyle)
            {
                this.fStyle = this.PrintingSystem.Styles.GetStyle(this.fStyle);
            }
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            base.PerformLayout(context);
            if (!ReferenceEquals(this.NavigationPair, BrickPagePair.Empty) && string.IsNullOrEmpty(this.NavigationRef))
            {
                VisualBrick brick = this.NavigationPair.GetBrick(context.PrintingSystem.Pages) as VisualBrick;
                if (brick != null)
                {
                    if (string.IsNullOrEmpty(brick.NavigationId))
                    {
                        brick.NavigationId = IdGenerator.GenerateRandomId();
                    }
                    this.NavigationRef = brick.NavigationId;
                }
            }
        }

        protected internal override void Scale(double scaleFactor)
        {
            base.Scale(scaleFactor);
            this.Style = this.Style.Scale((float) scaleFactor);
        }

        internal void SetBoundsHeight(float height, float dpi)
        {
            base.Height = GraphicsUnitConverter.Convert(height, dpi, (float) 300f);
        }

        internal void SetBoundsWidth(float width, float dpi)
        {
            base.Width = GraphicsUnitConverter.Convert(width, dpi, (float) 300f);
        }

        internal void SetBoundsX(float x, float dpi)
        {
            base.X = GraphicsUnitConverter.Convert(x, dpi, (float) 300f);
        }

        internal void SetBoundsY(float y, float dpi)
        {
            base.Y = GraphicsUnitConverter.Convert(y, dpi, (float) 300f);
        }

        internal static void SetPenStyle(Pen pen, DashStyle style)
        {
            if (style != DashStyle.Custom)
            {
                if (style == DashStyle.Solid)
                {
                    pen.DashStyle = DashStyle.Solid;
                }
                else
                {
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = GetDashPattern(style);
                }
            }
        }

        protected override bool ShouldSerializeCore(string propertyName) => 
            ((propertyName != "NavigationPageId") || (this.PrintingSystem == null)) ? ((propertyName != "UseTextAsDefaultHint") ? (((propertyName != "Hint") || !this.UseTextAsDefaultHint) ? base.ShouldSerializeCore(propertyName) : (base.GetDataIndex(BrickAttachedProperties.Hint.Index) >= 0)) : this.UseTextAsDefaultHint) : (this.PrintingSystem.Document is IStreamingDocument);

        protected static bool ToBoolean(DefaultBoolean value, bool defaultValue) => 
            (value == DefaultBoolean.True) || ((value != DefaultBoolean.False) && defaultValue);

        public override float ValidatePageRight(float pageRight, RectangleF rect) => 
            ((pageRight < rect.Left) || (pageRight > rect.Right)) ? pageRight : (!this.SeparableHorz ? rect.Left : this.ValidatePageRightInternal(pageRight, rect));

        protected virtual float ValidatePageRightInternal(float pageRight, RectangleF rect) => 
            rect.Left;

        internal virtual object EditValue
        {
            get => 
                null;
            set
            {
            }
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public override bool RightToLeftLayout
        {
            get => 
                base.flags[BrickBase.bitRightToLeftLayout];
            set => 
                base.flags[BrickBase.bitRightToLeftLayout] = value;
        }

        [XtraSerializableProperty, DefaultValue(false)]
        public bool UseTextAsDefaultHint
        {
            get => 
                base.flags[BrickBase.bitUseTextAsDefaultHint];
            set => 
                base.flags[BrickBase.bitUseTextAsDefaultHint] = value;
        }

        public override string Hint
        {
            get
            {
                int dataIndex = base.GetDataIndex(BrickAttachedProperties.Hint.Index);
                return ((dataIndex >= 0) ? base.GetDataValue<string>(dataIndex) : (this.UseTextAsDefaultHint ? this.Text : string.Empty));
            }
            set => 
                base.Hint = value;
        }

        [Description("Provides access to the brick-page pair, associated with the current brick.")]
        public virtual BrickPagePair NavigationPair
        {
            get => 
                base.GetValue<BrickPagePair>(BrickAttachedProperties.NavigationPair, BrickPagePair.Empty);
            set
            {
                base.SetAttachedValue<BrickPagePair>(BrickAttachedProperties.NavigationPair, value, BrickPagePair.Empty);
                this.NavigationRef = string.Empty;
                this.OnNavigationPairUpdated();
            }
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public string NavigationRef
        {
            get => 
                base.GetValue<string>(BrickAttachedProperties.NavigationRef, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.NavigationRef, text1, string.Empty);
            }
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public string NavigationId
        {
            get => 
                base.GetValue<string>(BrickAttachedProperties.NavigationId, string.Empty);
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.SetAttachedValue<string>(BrickAttachedProperties.NavigationId, text1, string.Empty);
            }
        }

        [Description("Gets an index of a page, which contains a bookmark's brick."), DefaultValue(-1), XtraSerializableProperty]
        public int NavigationPageIndex =>
            this.NavigationPair.PageIndex;

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never), DefaultValue((long) (-1L)), XtraSerializableProperty]
        public long NavigationPageId
        {
            get => 
                base.GetValue<long>(BrickAttachedProperties.NavigationPageId, this.NavigationPair.PageID);
            set => 
                base.SetAttachedValue<long>(BrickAttachedProperties.NavigationPageId, value, this.NavigationPair.PageID);
        }

        [Description("Gets a string value, which is intended for serialization of the brick's bookmark."), DefaultValue(""), XtraSerializableProperty]
        public string NavigationBrickIndices =>
            this.NavigationPair.Indices;

        [Description("Gets an object containing information about the bookmark for this VisualBrick.")]
        public DevExpress.XtraPrinting.BookmarkInfo BookmarkInfo
        {
            get => 
                base.GetValue<DevExpress.XtraPrinting.BookmarkInfo>(BrickAttachedProperties.BookmarkInfo, this.BrickOwner.EmptyBookmarkInfo);
            set => 
                base.SetAttachedValue<DevExpress.XtraPrinting.BookmarkInfo>(BrickAttachedProperties.BookmarkInfo, value, this.BrickOwner.EmptyBookmarkInfo);
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty, DefaultValue("")]
        public string SortData
        {
            get => 
                base.GetValue<string>(BrickAttachedProperties.SortData, string.Empty);
            set => 
                base.SetAttachedValue<string>(BrickAttachedProperties.SortData, value, string.Empty);
        }

        [Description("Gets the owner of this brick.")]
        public IBrickOwner BrickOwner
        {
            get => 
                this.brickOwner;
            protected internal set => 
                this.brickOwner = value;
        }

        [Description("Determines whether the current brick can be divided into several parts horizontally.")]
        public override bool SeparableHorz =>
            this.BrickOwner.IsSeparableHorz(base.SeparableHorz);

        [Description("Determines whether the current brick can be divided into several parts vertically.")]
        public override bool SeparableVert =>
            this.BrickOwner.IsSeparableVert(base.SeparableVert);

        [Description("Gets or sets the Printing System used to create and print this brick.")]
        public override PrintingSystemBase PrintingSystem
        {
            get => 
                (PrintingSystemBase) BrickPSHelper.GetPS(this.Style);
            set => 
                BrickPSHelper.SetPS(this.Style, value);
        }

        [Description("Specifies the original value for the VisualBrick.")]
        public virtual object TextValue
        {
            get => 
                null;
            set
            {
            }
        }

        [Description("Specifies the format string applied to the visual brick's TextValue.")]
        public virtual string TextValueFormatString
        {
            get => 
                null;
            set
            {
            }
        }

        [Description("Specifies the native XLSX format string, to accompany the VisualBrick instance.")]
        public virtual string XlsxFormatString
        {
            get => 
                null;
            set
            {
            }
        }

        [Description("Gets or sets the BrickStyle instance used to render a brick in an appropriate format."), XtraSerializableProperty(XtraSerializationVisibility.Content, true, false, false, 0, XtraSerializationFlags.Cached)]
        public BrickStyle Style
        {
            get => 
                this.fStyle;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("style");
                }
                BrickPSHelper.SetPS(value, this.PrintingSystem);
                if (this.PrintingSystem == null)
                {
                    this.fStyle = value;
                }
                else
                {
                    this.fStyle = this.PrintingSystem.Styles.GetStyle(value);
                }
            }
        }

        [Description("Defines the border settings for the current VisualBrick.")]
        public BorderSide Sides
        {
            get => 
                this.Style.Sides;
            set
            {
                if (this.Sides != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeSides(this.Style, value);
                }
            }
        }

        [Description("Specifies the border width of the current VisualBrick object.")]
        public float BorderWidth
        {
            get => 
                this.Style.BorderWidth;
            set
            {
                if (this.BorderWidth != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeBorderWidth(this.Style, value);
                }
            }
        }

        [Description("Specifies the dash style for the brick's border.")]
        public DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle
        {
            get => 
                this.Style.BorderDashStyle;
            set
            {
                if (this.BorderDashStyle != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeBorderDashStyle(this.Style, value);
                }
            }
        }

        [Description("Defines the border color for the current VisualBrick.")]
        public Color BorderColor
        {
            get => 
                this.Style.BorderColor;
            set
            {
                if (this.BorderColor != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeBorderColor(this.Style, value);
                }
            }
        }

        [Description("Defines the background color for the current VisualBrick.")]
        public Color BackColor
        {
            get => 
                this.Style.BackColor;
            set
            {
                if (this.BackColor != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeBackColor(this.Style, value);
                }
            }
        }

        [Description("Gets or sets the padding values of a brick.")]
        public PaddingInfo Padding
        {
            get => 
                this.Style.Padding;
            set
            {
                if (this.Padding != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangePadding(this.Style, value);
                }
            }
        }

        [Description("Gets or sets the border style of a brick.")]
        public BrickBorderStyle BorderStyle
        {
            get => 
                this.Style.BorderStyle;
            set
            {
                if (this.BorderStyle != value)
                {
                    this.Style = BrickStyleHelper.Instance.ChangeBorderStyle(this.Style, value);
                }
            }
        }

        [Description("Gets or sets the text associated with the VisualBrick.")]
        public virtual string Text
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Visual";

        internal bool CanGrow
        {
            get => 
                base.flags[BrickBase.bitCanGrow];
            set => 
                base.flags[BrickBase.bitCanGrow] = value;
        }

        internal bool CanShrink
        {
            get => 
                base.flags[BrickBase.bitCanShrink];
            set => 
                base.flags[BrickBase.bitCanShrink] = value;
        }

        internal bool AutoWidth
        {
            get => 
                base.flags[BrickBase.bitAutoWidth];
            set => 
                base.flags[BrickBase.bitAutoWidth] = value;
        }

        internal bool NeedAdjust =>
            this.CanGrow || (this.CanShrink || this.AutoWidth);
    }
}

