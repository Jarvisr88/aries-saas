namespace DevExpress.XtraPrinting
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;

    public class BrickStyle : ICloneable, IDisposable
    {
        private static System.Drawing.Font fDefaultFont = new System.Drawing.Font("Times New Roman", 9.75f);
        private static BrickBorderStyle defaultBorderStyle = BrickBorderStyle.Center;
        private Color fBackColor;
        private Color fBorderColor;
        private BorderSide fSides;
        private float fBorderWidth;
        private DevExpress.XtraPrinting.BorderDashStyle fBorderDashStyle;
        private Color fForeColor;
        private System.Drawing.Font fFont;
        private PaddingInfo fPadding;
        private DevExpress.XtraPrinting.TextAlignment fTextAlignment;
        private float fTabInterval;
        private StyleProperty setProperties;
        private BrickStringFormat fStringFormat;
        private BrickBorderStyle fBorderStyle;
        private IPrintingSystem printingSystem;
        private System.Drawing.Font fontInPoints;
        private bool disposed;

        public BrickStyle()
        {
            this.fTabInterval = float.NaN;
            this.fBorderStyle = defaultBorderStyle;
        }

        public BrickStyle(BrickStyle src)
        {
            this.fTabInterval = float.NaN;
            this.fBorderStyle = defaultBorderStyle;
            this.Initialize(src.Sides, src.BorderWidth, src.BorderColor, src.BorderDashStyle, src.BorderStyle, src.BackColor, src.ForeColor, src.Font, src.StringFormat, src.TextAlignment, src.Padding);
        }

        public BrickStyle(float dpi)
        {
            this.fTabInterval = float.NaN;
            this.fBorderStyle = defaultBorderStyle;
            this.SetPadding(new PaddingInfo(dpi));
        }

        public BrickStyle(BorderSide sides, float borderWidth, Color borderColor, Color backColor, Color foreColor, System.Drawing.Font font, BrickStringFormat sf)
        {
            this.fTabInterval = float.NaN;
            this.fBorderStyle = defaultBorderStyle;
            PaddingInfo padding = new PaddingInfo();
            this.Initialize(sides, borderWidth, borderColor, DevExpress.XtraPrinting.BorderDashStyle.Solid, defaultBorderStyle, backColor, foreColor, font, sf, (DevExpress.XtraPrinting.TextAlignment) 0, padding);
            this.ResetBorderDashStyle();
            this.ResetTextAlignment();
            this.ResetPadding();
        }

        private static float AdjustBorderWidthToDeflate(float borderWidth, BrickBorderStyle borderStyle) => 
            (borderStyle == BrickBorderStyle.Inset) ? borderWidth : ((borderStyle == BrickBorderStyle.Center) ? (borderWidth / 2f) : 0f);

        private static float AdjustBorderWidthToInflate(float borderWidth, BrickBorderStyle borderStyle) => 
            (borderStyle == BrickBorderStyle.Inset) ? 0f : ((borderStyle == BrickBorderStyle.Center) ? (borderWidth / 2f) : borderWidth);

        internal float[] CalculateTabStops(IMeasurer measurer)
        {
            if (!this.IsSetTabInterval)
            {
                this.fTabInterval = measurer.MeasureString("Q", this.Font, (float) 0f, this.StringFormat.Value, GraphicsUnit.Document).Width * 8f;
            }
            return new float[] { this.fTabInterval };
        }

        public void ChangeAlignment(DevExpress.XtraPrinting.TextAlignment alignment)
        {
            if (this.TextAlignment != alignment)
            {
                this.TextAlignment = alignment;
                this.StringFormat.Value.Alignment = GraphicsConvertHelper.ToHorzStringAlignment(alignment);
                this.StringFormat.Value.LineAlignment = GraphicsConvertHelper.ToVertStringAlignment(alignment);
            }
        }

        public virtual object Clone()
        {
            BrickStyle style = this.CreateClone();
            style.Initialize(this.fSides, this.fBorderWidth, this.fBorderColor, this.fBorderDashStyle, this.fBorderStyle, this.fBackColor, this.fForeColor, this.fFont, this.fStringFormat, this.fTextAlignment, this.fPadding);
            style.setProperties = this.setProperties;
            return style;
        }

        internal void CopyProperties(BrickStyle dest, StyleProperty properties)
        {
            if ((properties & StyleProperty.BackColor) > StyleProperty.None)
            {
                dest.SetBackColor(this.BackColor);
            }
            if ((properties & StyleProperty.BorderColor) > StyleProperty.None)
            {
                dest.SetBorderColor(this.BorderColor);
            }
            if ((properties & StyleProperty.BorderDashStyle) > StyleProperty.None)
            {
                dest.SetBorderDashStyle(this.BorderDashStyle);
            }
            if ((properties & StyleProperty.Borders) > StyleProperty.None)
            {
                dest.SetBorders(this.Sides);
            }
            if ((properties & StyleProperty.BorderWidth) > StyleProperty.None)
            {
                dest.SetBorderWidth(this.BorderWidth);
            }
            if ((properties & StyleProperty.Font) > StyleProperty.None)
            {
                dest.SetFont(this.Font);
            }
            if ((properties & StyleProperty.ForeColor) > StyleProperty.None)
            {
                dest.SetForeColor(this.ForeColor);
            }
            if ((properties & StyleProperty.Padding) > StyleProperty.None)
            {
                dest.SetPadding(this.Padding);
            }
            if ((properties & StyleProperty.TextAlignment) > StyleProperty.None)
            {
                dest.SetTextAlignment(this.TextAlignment);
            }
        }

        protected virtual BrickStyle CreateClone() => 
            new BrickStyle();

        public static BrickStyle CreateDefault() => 
            new BrickStyle(BorderSide.All, 1f, DXColor.Black, DXColor.White, DXColor.Black, DefaultFont, new BrickStringFormat());

        private static System.Drawing.Font CreateFontInPoints(System.Drawing.Font source) => 
            new System.Drawing.Font(source.FontFamily, GraphicsUnitConverter.Convert(source.Size, source.Unit, GraphicsUnit.Point), source.Style, GraphicsUnit.Point, source.GdiCharSet);

        public RectangleF DeflateBorderWidth(RectangleF rect, float dpi) => 
            this.DeflateBorderWidth(rect, dpi, false);

        public static RectangleF DeflateBorderWidth(RectangleF rect, BorderSide sides, float borderWidth) => 
            InflateBorderWidth(rect, sides, -borderWidth);

        public RectangleF DeflateBorderWidth(RectangleF rect, float dpi, bool applyBorderStyle) => 
            this.DeflateBorderWidth(rect, this.Sides, dpi, applyBorderStyle);

        public static RectangleF DeflateBorderWidth(RectangleF rect, BorderSide sides, float borderWidth, BrickBorderStyle borderStyle) => 
            DeflateBorderWidth(rect, sides, AdjustBorderWidthToDeflate(borderWidth, borderStyle));

        public RectangleF DeflateBorderWidth(RectangleF rect, BorderSide sides, float dpi, bool applyBorderStyle)
        {
            float val = applyBorderStyle ? AdjustBorderWidthToDeflate(this.BorderWidth, this.fBorderStyle) : this.BorderWidth;
            return DeflateBorderWidth(rect, sides, GraphicsUnitConverter.Convert(val, (float) 96f, dpi));
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed & disposing)
            {
                if (this.fStringFormat != null)
                {
                    this.fStringFormat.Dispose();
                    this.fStringFormat = null;
                }
                this.disposed = true;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            BrickStyle style = obj as BrickStyle;
            return (this.PropertiesEqual(style) && ((base.GetType() == style.GetType()) && ReferenceEquals(this.PrintingSystem, style.PrintingSystem)));
        }

        private void FlagReset(StyleProperty value)
        {
            this.setProperties &= ~value;
        }

        private void FlagSet(StyleProperty value)
        {
            this.setProperties |= value;
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        internal int GetHashCodeInternal() => 
            HashCodeHelper.CalculateGeneric<int, int, int, int, int, int, int, int, int, int, int>((int) this.Sides, (int) this.BorderWidth, this.BorderColor.ToArgb(), this.BackColor.ToArgb(), this.ForeColor.ToArgb(), (int) this.TextAlignment, this.Font.GetHashCode(), this.StringFormat.GetHashCode(), this.Padding.GetHashCode(), (int) this.BorderDashStyle, (int) this.fBorderStyle);

        protected virtual Color GetInitialBackColor() => 
            DXColor.Empty;

        protected virtual Color GetInitialBorderColor() => 
            DXColor.Empty;

        protected virtual DevExpress.XtraPrinting.BorderDashStyle GetInitialBorderDashStyle() => 
            DevExpress.XtraPrinting.BorderDashStyle.Solid;

        protected virtual BorderSide GetInitialBorders() => 
            BorderSide.None;

        protected virtual float GetInitialBorderWidth() => 
            0f;

        protected virtual System.Drawing.Font GetInitialFont() => 
            DefaultFont;

        protected virtual Color GetInitialForeColor() => 
            DXColor.Empty;

        protected virtual PaddingInfo GetInitialPadding() => 
            PaddingInfo.Empty;

        protected virtual DevExpress.XtraPrinting.TextAlignment GetInitialTextAlignment() => 
            DevExpress.XtraPrinting.TextAlignment.TopLeft;

        internal object GetValue(StyleProperty property)
        {
            if (property > StyleProperty.BorderDashStyle)
            {
                if (property <= StyleProperty.BorderWidth)
                {
                    if (property == StyleProperty.Borders)
                    {
                        return this.Sides;
                    }
                    if (property == StyleProperty.BorderWidth)
                    {
                        return this.BorderWidth;
                    }
                }
                else
                {
                    if (property == StyleProperty.TextAlignment)
                    {
                        return this.TextAlignment;
                    }
                    if (property == StyleProperty.Padding)
                    {
                        return this.Padding;
                    }
                }
            }
            else
            {
                switch (property)
                {
                    case StyleProperty.BackColor:
                        return this.BackColor;

                    case StyleProperty.ForeColor:
                        return this.ForeColor;

                    case (StyleProperty.ForeColor | StyleProperty.BackColor):
                        break;

                    case StyleProperty.BorderColor:
                        return this.BorderColor;

                    default:
                        if (property == StyleProperty.Font)
                        {
                            return this.Font;
                        }
                        if (property != StyleProperty.BorderDashStyle)
                        {
                            break;
                        }
                        return this.BorderDashStyle;
                }
            }
            throw new ArgumentException("properties");
        }

        public RectangleF InflateBorderWidth(RectangleF rect, float dpi) => 
            this.InflateBorderWidth(rect, dpi, false);

        public static unsafe RectangleF InflateBorderWidth(RectangleF rect, BorderSide sides, float borderWidth)
        {
            if (borderWidth != 0f)
            {
                if ((sides & BorderSide.Left) > BorderSide.None)
                {
                    RectangleF* efPtr1 = &rect;
                    efPtr1.X -= borderWidth;
                    RectangleF* efPtr2 = &rect;
                    efPtr2.Width += borderWidth;
                }
                if ((sides & BorderSide.Top) > BorderSide.None)
                {
                    RectangleF* efPtr3 = &rect;
                    efPtr3.Y -= borderWidth;
                    RectangleF* efPtr4 = &rect;
                    efPtr4.Height += borderWidth;
                }
                if ((sides & BorderSide.Right) > BorderSide.None)
                {
                    RectangleF* efPtr5 = &rect;
                    efPtr5.Width += borderWidth;
                }
                if ((sides & BorderSide.Bottom) > BorderSide.None)
                {
                    RectangleF* efPtr6 = &rect;
                    efPtr6.Height += borderWidth;
                }
                if (rect.Width < 0f)
                {
                    rect.Width = 0f;
                }
                if (rect.Height < 0f)
                {
                    rect.Height = 0f;
                }
            }
            return rect;
        }

        public RectangleF InflateBorderWidth(RectangleF rect, float dpi, bool applyBorderStyle) => 
            this.InflateBorderWidth(rect, dpi, applyBorderStyle, this.Sides);

        public static RectangleF InflateBorderWidth(RectangleF rect, BorderSide sides, float borderWidth, BrickBorderStyle borderStyle) => 
            InflateBorderWidth(rect, sides, AdjustBorderWidthToInflate(borderWidth, borderStyle));

        public RectangleF InflateBorderWidth(RectangleF rect, float dpi, bool applyBorderStyle, BorderSide sides)
        {
            float val = applyBorderStyle ? AdjustBorderWidthToInflate(this.BorderWidth, this.fBorderStyle) : this.BorderWidth;
            return InflateBorderWidth(rect, sides, GraphicsUnitConverter.Convert(val, (float) 96f, dpi));
        }

        private void Initialize(BorderSide sides, float borderWidth, Color borderColor, DevExpress.XtraPrinting.BorderDashStyle borderDashStyle, BrickBorderStyle borderStyle, Color backColor, Color foreColor, System.Drawing.Font font, BrickStringFormat sf, DevExpress.XtraPrinting.TextAlignment textAlignment, PaddingInfo padding)
        {
            this.SetBackColor(backColor);
            this.SetBorders(sides);
            this.SetBorderWidth(borderWidth);
            this.SetBorderColor(borderColor);
            this.SetBorderDashStyle(borderDashStyle);
            this.fBorderStyle = borderStyle;
            this.SetForeColor(foreColor);
            this.SetFont(font);
            this.SetStringFormat(sf);
            this.SetTextAlignment(textAlignment);
            this.SetPadding(padding);
        }

        private void InvalidateFontInPoints()
        {
            this.fontInPoints = null;
        }

        internal bool IsSet(StyleProperty property) => 
            (this.SetProperties & property) > StyleProperty.None;

        private static bool IsValidFont(System.Drawing.Font font) => 
            ((IntPtr) typeof(System.Drawing.Font).GetField("nativeFont", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(font)) != IntPtr.Zero;

        internal bool PropertiesEqual(BrickStyle style) => 
            ((style != null) && ((this.Sides == style.Sides) && (this.BorderWidth == style.BorderWidth))) && ((this.BorderColor.ToArgb() == style.BorderColor.ToArgb()) && ((this.BackColor.ToArgb() == style.BackColor.ToArgb()) && (((this.ForeColor.ToArgb() == style.ForeColor.ToArgb()) && ((this.TextAlignment == style.TextAlignment) && (Equals(this.Font, style.Font) && Equals(this.fStringFormat, style.fStringFormat)))) && (this.Padding.Equals(style.Padding) && ((this.fBorderStyle == style.fBorderStyle) && (this.BorderDashStyle == style.BorderDashStyle))))));

        internal static bool PropertyIsSet(BrickStyle style, StyleProperty property) => 
            (style != null) && style.IsSet(property);

        internal void Reset(StyleProperty property)
        {
            this.FlagReset(property);
        }

        public void ResetBackColor()
        {
            this.FlagReset(StyleProperty.BackColor);
        }

        public void ResetBorderColor()
        {
            this.FlagReset(StyleProperty.BorderColor);
        }

        public void ResetBorderDashStyle()
        {
            this.FlagReset(StyleProperty.BorderDashStyle);
        }

        public void ResetBorders()
        {
            this.FlagReset(StyleProperty.Borders);
        }

        public void ResetBorderWidth()
        {
            this.FlagReset(StyleProperty.BorderWidth);
        }

        public void ResetFont()
        {
            this.FlagReset(StyleProperty.Font);
            this.fFont = null;
        }

        public void ResetForeColor()
        {
            this.FlagReset(StyleProperty.ForeColor);
        }

        public void ResetPadding()
        {
            this.FlagReset(StyleProperty.Padding);
        }

        private void ResetTabInterval()
        {
            this.fTabInterval = float.NaN;
        }

        public void ResetTextAlignment()
        {
            this.FlagReset(StyleProperty.TextAlignment);
        }

        public BrickStyle Scale(float ratio)
        {
            BrickStyle style = (BrickStyle) this.Clone();
            style.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size * ratio, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
            style.BorderWidth *= ratio;
            style.Padding = this.Padding.Scale(ratio);
            this.ResetTabInterval();
            return style;
        }

        public void SetAlignment(HorzAlignment horzAlignment, VertAlignment vertAlignment)
        {
            this.TextAlignment = TextAlignmentConverter.ToTextAlignment(horzAlignment, vertAlignment);
            this.StringFormat = this.StringFormat.ChangeAlignment(AlignmentConverter.HorzAlignmentToStringAlignment(horzAlignment), AlignmentConverter.VertAlignmentToStringAlignment(vertAlignment));
        }

        private void SetBackColor(Color value)
        {
            this.fBackColor = value;
            this.FlagSet(StyleProperty.BackColor);
        }

        private void SetBorderColor(Color value)
        {
            this.fBorderColor = value;
            this.FlagSet(StyleProperty.BorderColor);
        }

        private void SetBorderDashStyle(DevExpress.XtraPrinting.BorderDashStyle value)
        {
            this.fBorderDashStyle = value;
            this.FlagSet(StyleProperty.BorderDashStyle);
        }

        private void SetBorders(BorderSide value)
        {
            this.fSides = value;
            this.FlagSet(StyleProperty.Borders);
        }

        private void SetBorderWidth(float value)
        {
            this.fBorderWidth = value;
            this.FlagSet(StyleProperty.BorderWidth);
        }

        private void SetFont(System.Drawing.Font value)
        {
            this.fFont = value;
            this.ResetTabInterval();
            this.FlagSet(StyleProperty.Font);
        }

        private void SetForeColor(Color value)
        {
            this.fForeColor = value;
            this.FlagSet(StyleProperty.ForeColor);
        }

        private void SetPadding(PaddingInfo value)
        {
            this.fPadding = value;
            this.FlagSet(StyleProperty.Padding);
        }

        private void SetStringFormat(BrickStringFormat value)
        {
            this.fStringFormat = (value != null) ? ((BrickStringFormat) value.Clone()) : null;
            this.ResetTabInterval();
        }

        private void SetTextAlignment(DevExpress.XtraPrinting.TextAlignment value)
        {
            this.fTextAlignment = value;
            this.FlagSet(StyleProperty.TextAlignment);
        }

        internal void SetValue(StyleProperty property, object value)
        {
            if (property > StyleProperty.BorderDashStyle)
            {
                if (property <= StyleProperty.BorderWidth)
                {
                    if (property == StyleProperty.Borders)
                    {
                        this.SetBorders((BorderSide) value);
                        return;
                    }
                    if (property == StyleProperty.BorderWidth)
                    {
                        this.SetBorderWidth((float) value);
                        return;
                    }
                }
                else
                {
                    if (property == StyleProperty.TextAlignment)
                    {
                        this.SetTextAlignment((DevExpress.XtraPrinting.TextAlignment) value);
                        return;
                    }
                    if (property == StyleProperty.Padding)
                    {
                        this.SetPadding((PaddingInfo) value);
                        return;
                    }
                }
            }
            else
            {
                switch (property)
                {
                    case StyleProperty.BackColor:
                        this.SetBackColor((Color) value);
                        return;

                    case StyleProperty.ForeColor:
                        this.SetForeColor((Color) value);
                        return;

                    case (StyleProperty.ForeColor | StyleProperty.BackColor):
                        break;

                    case StyleProperty.BorderColor:
                        this.SetBorderColor((Color) value);
                        return;

                    default:
                        if (property == StyleProperty.Font)
                        {
                            this.SetFont((System.Drawing.Font) value);
                            return;
                        }
                        if (property != StyleProperty.BorderDashStyle)
                        {
                            break;
                        }
                        this.SetBorderDashStyle((DevExpress.XtraPrinting.BorderDashStyle) value);
                        return;
                }
            }
            throw new ArgumentException("properties");
        }

        [DXHelpExclude(true)]
        public static BorderSide SwapRightAndLeftSides(BorderSide sides) => 
            ((sides & (BorderSide.Bottom | BorderSide.Top)) | (((sides & BorderSide.Right) > BorderSide.None) ? BorderSide.Left : BorderSide.None)) | (((sides & BorderSide.Left) > BorderSide.None) ? BorderSide.Right : BorderSide.None);

        [Description("Gets the default font for a brick style.")]
        public static System.Drawing.Font DefaultFont =>
            fDefaultFont;

        [Description("Gets the BrickStyle object whose properties are set to their default values."), Obsolete("This property is now obsolete. You should use the CreateDefault method instead.")]
        public static BrickStyle Default =>
            CreateDefault();

        internal StyleProperty SetProperties =>
            this.setProperties;

        internal IPrintingSystem PrintingSystem
        {
            get => 
                this.printingSystem;
            set => 
                this.printingSystem = value;
        }

        internal System.Drawing.Font FontInPoints
        {
            get
            {
                if (this.Font.Unit == GraphicsUnit.Point)
                {
                    return this.Font;
                }
                this.fontInPoints ??= CreateFontInPoints(this.Font);
                return this.fontInPoints;
            }
        }

        internal bool IsDisposed =>
            this.disposed;

        [Description("Gets or sets the border style of a brick."), XtraSerializableProperty]
        public virtual BrickBorderStyle BorderStyle
        {
            get => 
                this.fBorderStyle;
            set => 
                this.fBorderStyle = value;
        }

        [Description("Gets or sets the padding values of a brick."), XtraSerializableProperty]
        public virtual PaddingInfo Padding
        {
            get => 
                this.IsSetPadding ? this.fPadding : this.GetInitialPadding();
            set => 
                this.SetPadding(value);
        }

        [XtraSerializableProperty(XtraSerializationFlags.SuppressDefaultValue), Description("Gets or sets the alignment of the text in the brick.")]
        public virtual DevExpress.XtraPrinting.TextAlignment TextAlignment
        {
            get => 
                this.IsSetTextAlignment ? this.fTextAlignment : this.GetInitialTextAlignment();
            set => 
                this.SetTextAlignment(value);
        }

        [XtraSerializableProperty, Description("Gets or sets a value defining which borders of the current brick are visible.")]
        public virtual BorderSide Sides
        {
            get => 
                this.IsSetBorders ? this.fSides : this.GetInitialBorders();
            set => 
                this.SetBorders(value);
        }

        [Description("Gets or sets the border width.")]
        public virtual float BorderWidth
        {
            get => 
                this.IsSetBorderWidth ? this.fBorderWidth : this.GetInitialBorderWidth();
            set => 
                this.SetBorderWidth(value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float BorderWidthSerializable
        {
            get => 
                this.BorderWidth;
            set => 
                this.BorderWidth = value;
        }

        [XtraSerializableProperty(XtraSerializationFlags.SuppressDefaultValue), Description("Gets or sets the border color.")]
        public virtual Color BorderColor
        {
            get => 
                this.IsSetBorderColor ? this.fBorderColor : this.GetInitialBorderColor();
            set => 
                this.SetBorderColor(value);
        }

        [XtraSerializableProperty(XtraSerializationFlags.SuppressDefaultValue), Description("Gets or sets the background color.")]
        public virtual Color BackColor
        {
            get => 
                this.IsSetBackColor ? this.fBackColor : this.GetInitialBackColor();
            set => 
                this.SetBackColor(value);
        }

        [XtraSerializableProperty(XtraSerializationFlags.SuppressDefaultValue), Description("Gets or sets the foreground color.")]
        public virtual Color ForeColor
        {
            get => 
                this.IsSetForeColor ? this.fForeColor : this.GetInitialForeColor();
            set => 
                this.SetForeColor(value);
        }

        [XtraSerializableProperty, Description("Gets or sets the Font used to display text.")]
        public virtual System.Drawing.Font Font
        {
            get => 
                this.IsSetFont ? this.fFont : this.GetInitialFont();
            set
            {
                if (value == null)
                {
                    this.ResetFont();
                }
                else
                {
                    this.SetFont(value);
                }
                this.InvalidateFontInPoints();
            }
        }

        [XtraSerializableProperty, Description("Gets or sets a BrickStringFormat instance specifying text formatting and layout.")]
        public virtual BrickStringFormat StringFormat
        {
            get
            {
                this.fStringFormat ??= new BrickStringFormat();
                return this.fStringFormat;
            }
            set
            {
                if (!ReferenceEquals(this.fStringFormat, value))
                {
                    if (this.fStringFormat != null)
                    {
                        this.fStringFormat.Dispose();
                    }
                    this.SetStringFormat(value);
                }
            }
        }

        [XtraSerializableProperty, Description("Specifies the dash style for the brick's border.")]
        public virtual DevExpress.XtraPrinting.BorderDashStyle BorderDashStyle
        {
            get => 
                this.IsSetBorderDashStyle ? this.fBorderDashStyle : this.GetInitialBorderDashStyle();
            set => 
                this.SetBorderDashStyle(value);
        }

        [Browsable(false)]
        public bool IsTransparent =>
            (this.BackColor.A == 0) || (this.BorderColor.A == 0);

        [Browsable(false)]
        public bool IsValid =>
            IsValidFont(this.Font);

        [Browsable(false)]
        public bool IsJustified =>
            this.TextAlignment >= DevExpress.XtraPrinting.TextAlignment.TopJustify;

        internal bool IsSetBackColor =>
            (this.setProperties & StyleProperty.BackColor) != StyleProperty.None;

        internal bool IsSetBorderColor =>
            (this.setProperties & StyleProperty.BorderColor) != StyleProperty.None;

        internal bool IsSetBorderDashStyle =>
            (this.setProperties & StyleProperty.BorderDashStyle) != StyleProperty.None;

        internal bool IsSetBorders =>
            (this.setProperties & StyleProperty.Borders) != StyleProperty.None;

        internal bool IsSetBorderWidth =>
            (this.setProperties & StyleProperty.BorderWidth) != StyleProperty.None;

        internal bool IsSetFont =>
            (this.setProperties & StyleProperty.Font) != StyleProperty.None;

        internal bool IsSetForeColor =>
            (this.setProperties & StyleProperty.ForeColor) != StyleProperty.None;

        internal bool IsSetPadding =>
            (this.setProperties & StyleProperty.Padding) != StyleProperty.None;

        internal bool IsSetTextAlignment =>
            (this.setProperties & StyleProperty.TextAlignment) != StyleProperty.None;

        private bool IsSetTabInterval =>
            !float.IsNaN(this.fTabInterval);
    }
}

