namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;

    public class DXWebStyle
    {
        private static readonly string[] borderStyles;
        private const string CssClassStr = "class";
        private DXWebFontInfo fontInfo;
        private bool marked;
        private int markedBits;
        private bool ownStateBag;
        private string registeredCssClass;
        private int setBits;
        private DXStateBag statebag;

        static DXWebStyle()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "NotSet";
            textArray1[1] = "None";
            textArray1[2] = "Dotted";
            textArray1[3] = "Dashed";
            textArray1[4] = "Solid";
            textArray1[5] = "Double";
            textArray1[6] = "Groove";
            textArray1[7] = "Ridge";
            textArray1[8] = "Inset";
            textArray1[9] = "Outset";
            borderStyles = textArray1;
        }

        public DXWebStyle() : this(null)
        {
            this.ownStateBag = true;
        }

        public DXWebStyle(DXStateBag bag)
        {
            this.statebag = bag;
            this.marked = false;
            this.setBits = 0;
        }

        public void AddAttributesToRender(DXHtmlTextWriter writer)
        {
            this.AddAttributesToRender(writer, null);
        }

        public virtual void AddAttributesToRender(DXHtmlTextWriter writer, DXHtmlControl owner)
        {
            string str = string.Empty;
            bool flag = true;
            if (this.IsSet(2))
            {
                str = (string) this.ViewState["class"];
                str ??= string.Empty;
            }
            if (!string.IsNullOrEmpty(this.registeredCssClass))
            {
                flag = false;
                str = (str.Length == 0) ? this.registeredCssClass : $"{str} {this.registeredCssClass}";
            }
            if (str.Length > 0)
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Class, str);
            }
            if (flag)
            {
                this.GetStyleAttributes().Render(writer);
            }
        }

        internal void ClearBit(int bit)
        {
            this.setBits &= ~bit;
        }

        public virtual void CopyFrom(DXWebStyle s)
        {
            if (this.RegisteredCssClass.Length != 0)
            {
                throw new InvalidOperationException("Style_RegisteredStylesAreReadOnly");
            }
            if ((s != null) && !s.IsEmpty)
            {
                this.Font.CopyFrom(s.Font);
                if (s.IsSet(2))
                {
                    this.CssClass = s.CssClass;
                }
                if (s.RegisteredCssClass.Length == 0)
                {
                    if (s.IsSet(8) && (s.BackColor != DXColor.Empty))
                    {
                        this.BackColor = s.BackColor;
                    }
                    if (s.IsSet(4) && (s.ForeColor != DXColor.Empty))
                    {
                        this.ForeColor = s.ForeColor;
                    }
                    if (s.IsSet(0x10) && (s.BorderColor != DXColor.Empty))
                    {
                        this.BorderColor = s.BorderColor;
                    }
                    if (s.IsSet(0x20) && (s.BorderWidth != DXWebUnit.Empty))
                    {
                        this.BorderWidth = s.BorderWidth;
                    }
                    if (s.IsSet(0x40))
                    {
                        this.BorderStyle = s.BorderStyle;
                    }
                    if (s.IsSet(0x80) && (s.Height != DXWebUnit.Empty))
                    {
                        this.Height = s.Height;
                    }
                    if (s.IsSet(0x100) && (s.Width != DXWebUnit.Empty))
                    {
                        this.Width = s.Width;
                    }
                }
                else
                {
                    this.CssClass = !this.IsSet(2) ? s.RegisteredCssClass : $"{this.CssClass} {s.RegisteredCssClass}";
                    if (s.IsSet(8) && (s.BackColor != DXColor.Empty))
                    {
                        this.ViewState.Remove("BackColor");
                        this.ClearBit(8);
                    }
                    if (s.IsSet(4) && (s.ForeColor != DXColor.Empty))
                    {
                        this.ViewState.Remove("ForeColor");
                        this.ClearBit(4);
                    }
                    if (s.IsSet(0x10) && (s.BorderColor != DXColor.Empty))
                    {
                        this.ViewState.Remove("BorderColor");
                        this.ClearBit(0x10);
                    }
                    if (s.IsSet(0x20) && (s.BorderWidth != DXWebUnit.Empty))
                    {
                        this.ViewState.Remove("BorderWidth");
                        this.ClearBit(0x20);
                    }
                    if (s.IsSet(0x40))
                    {
                        this.ViewState.Remove("BorderStyle");
                        this.ClearBit(0x40);
                    }
                    if (s.IsSet(0x80) && (s.Height != DXWebUnit.Empty))
                    {
                        this.ViewState.Remove("Height");
                        this.ClearBit(0x80);
                    }
                    if (s.IsSet(0x100) && (s.Width != DXWebUnit.Empty))
                    {
                        this.ViewState.Remove("Width");
                        this.ClearBit(0x100);
                    }
                }
            }
        }

        protected virtual void FillStyleAttributes(DXCssStyleCollection attributes)
        {
            Color color;
            DXWebUnit unit;
            if (this.IsSet(4))
            {
                color = (Color) this.ViewState["ForeColor"];
                if (!DXColor.IsEmpty(color))
                {
                    attributes.Add(DXHtmlTextWriterStyle.Color, DXColor.ToHtml(color));
                }
            }
            if (this.IsSet(8))
            {
                color = (Color) this.ViewState["BackColor"];
                if (!DXColor.IsEmpty(color))
                {
                    attributes.Add(DXHtmlTextWriterStyle.BackgroundColor, DXColor.ToHtml(color));
                }
            }
            if (this.IsSet(0x10))
            {
                color = (Color) this.ViewState["BorderColor"];
                if (!DXColor.IsEmpty(color))
                {
                    attributes.Add(DXHtmlTextWriterStyle.BorderColor, DXColor.ToHtml(color));
                }
            }
            DXWebBorderStyle borderStyle = this.BorderStyle;
            DXWebUnit borderWidth = this.BorderWidth;
            if (borderWidth.IsEmpty)
            {
                if (borderStyle != DXWebBorderStyle.NotSet)
                {
                    attributes.Add(DXHtmlTextWriterStyle.BorderStyle, borderStyles[(int) borderStyle]);
                }
            }
            else
            {
                attributes.Add(DXHtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.InvariantCulture));
                if (borderStyle != DXWebBorderStyle.NotSet)
                {
                    attributes.Add(DXHtmlTextWriterStyle.BorderStyle, borderStyles[(int) borderStyle]);
                }
                else if (borderWidth.Value != 0.0)
                {
                    attributes.Add(DXHtmlTextWriterStyle.BorderStyle, "solid");
                }
            }
            DXWebFontInfo font = this.Font;
            string[] names = font.Names;
            if (names.Length != 0)
            {
                attributes.Add(DXHtmlTextWriterStyle.FontFamily, FormatStringArray(names, ','));
            }
            DXWebFontUnit size = font.Size;
            if (!size.IsEmpty)
            {
                attributes.Add(DXHtmlTextWriterStyle.FontSize, size.ToString(CultureInfo.InvariantCulture));
            }
            if (this.IsSet(0x800))
            {
                if (font.Bold)
                {
                    attributes.Add(DXHtmlTextWriterStyle.FontWeight, "bold");
                }
                else
                {
                    attributes.Add(DXHtmlTextWriterStyle.FontWeight, "normal");
                }
            }
            if (this.IsSet(0x1000))
            {
                if (font.Italic)
                {
                    attributes.Add(DXHtmlTextWriterStyle.FontStyle, "italic");
                }
                else
                {
                    attributes.Add(DXHtmlTextWriterStyle.FontStyle, "normal");
                }
            }
            string str = string.Empty;
            if (font.Underline)
            {
                str = "underline";
            }
            if (font.Overline)
            {
                str = str + " overline";
            }
            if (font.Strikeout)
            {
                str = str + " line-through";
            }
            if (str.Length > 0)
            {
                attributes.Add(DXHtmlTextWriterStyle.TextDecoration, str);
            }
            else if (this.IsSet(0x2000) || (this.IsSet(0x4000) || this.IsSet(0x8000)))
            {
                attributes.Add(DXHtmlTextWriterStyle.TextDecoration, "none");
            }
            if (this.IsSet(0x80))
            {
                unit = (DXWebUnit) this.ViewState["Height"];
                if (!unit.IsEmpty)
                {
                    attributes.Add(DXHtmlTextWriterStyle.Height, unit.ToString(CultureInfo.InvariantCulture));
                }
            }
            if (this.IsSet(0x100))
            {
                unit = (DXWebUnit) this.ViewState["Width"];
                if (!unit.IsEmpty)
                {
                    attributes.Add(DXHtmlTextWriterStyle.Width, unit.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private static string FormatStringArray(string[] array, char delimiter)
        {
            int length = array.Length;
            return ((length == 0) ? string.Empty : ((length != 1) ? string.Join(new string(delimiter, 1), array) : array[0]));
        }

        public DXCssStyleCollection GetStyleAttributes()
        {
            DXCssStyleCollection attributes = new DXCssStyleCollection();
            this.FillStyleAttributes(attributes);
            return attributes;
        }

        internal bool IsSet(int propKey) => 
            (this.setBits & propKey) != 0;

        public virtual void MergeWith(DXWebStyle s)
        {
            if (this.RegisteredCssClass.Length != 0)
            {
                throw new InvalidOperationException("Style_RegisteredStylesAreReadOnly");
            }
            if ((s != null) && !s.IsEmpty)
            {
                if (this.IsEmpty)
                {
                    this.CopyFrom(s);
                }
                else
                {
                    this.Font.MergeWith(s.Font);
                    if (s.IsSet(2) && !this.IsSet(2))
                    {
                        this.CssClass = s.CssClass;
                    }
                    if (s.RegisteredCssClass.Length != 0)
                    {
                        if (this.IsSet(2))
                        {
                            this.CssClass = $"{this.CssClass} {s.RegisteredCssClass}";
                        }
                        else
                        {
                            this.CssClass = s.RegisteredCssClass;
                        }
                    }
                    else
                    {
                        if (s.IsSet(8) && (!this.IsSet(8) || (this.BackColor == DXColor.Empty)))
                        {
                            this.BackColor = s.BackColor;
                        }
                        if (s.IsSet(4) && (!this.IsSet(4) || (this.ForeColor == DXColor.Empty)))
                        {
                            this.ForeColor = s.ForeColor;
                        }
                        if (s.IsSet(0x10) && (!this.IsSet(0x10) || (this.BorderColor == DXColor.Empty)))
                        {
                            this.BorderColor = s.BorderColor;
                        }
                        if (s.IsSet(0x20) && (!this.IsSet(0x20) || (this.BorderWidth == DXWebUnit.Empty)))
                        {
                            this.BorderWidth = s.BorderWidth;
                        }
                        if (s.IsSet(0x40) && !this.IsSet(0x40))
                        {
                            this.BorderStyle = s.BorderStyle;
                        }
                        if (s.IsSet(0x80) && (!this.IsSet(0x80) || (this.Height == DXWebUnit.Empty)))
                        {
                            this.Height = s.Height;
                        }
                        if (s.IsSet(0x100) && (!this.IsSet(0x100) || (this.Width == DXWebUnit.Empty)))
                        {
                            this.Width = s.Width;
                        }
                    }
                }
            }
        }

        public virtual void Reset()
        {
            if (this.statebag != null)
            {
                if (this.IsSet(2))
                {
                    this.ViewState.Remove("class");
                }
                if (this.IsSet(8))
                {
                    this.ViewState.Remove("BackColor");
                }
                if (this.IsSet(4))
                {
                    this.ViewState.Remove("ForeColor");
                }
                if (this.IsSet(0x10))
                {
                    this.ViewState.Remove("BorderColor");
                }
                if (this.IsSet(0x20))
                {
                    this.ViewState.Remove("BorderWidth");
                }
                if (this.IsSet(0x40))
                {
                    this.ViewState.Remove("BorderStyle");
                }
                if (this.IsSet(0x80))
                {
                    this.ViewState.Remove("Height");
                }
                if (this.IsSet(0x100))
                {
                    this.ViewState.Remove("Width");
                }
                this.Font.Reset();
                this.ViewState.Remove("_!SB");
                this.markedBits = 0;
            }
            this.setBits = 0;
        }

        protected internal virtual void SetBit(int bit)
        {
            this.setBits |= bit;
            if (this.IsTrackingViewState)
            {
                this.markedBits |= bit;
            }
        }

        public void SetDirty()
        {
            this.ViewState.SetDirty(true);
            this.markedBits = this.setBits;
        }

        internal void SetRegisteredCssClass(string cssClass)
        {
            this.registeredCssClass = cssClass;
        }

        protected internal virtual void TrackViewState()
        {
            if (this.ownStateBag)
            {
                this.ViewState.TrackViewState();
            }
            this.marked = true;
        }

        public Color BackColor
        {
            get => 
                !this.IsSet(8) ? DXColor.Empty : ((Color) this.ViewState["BackColor"]);
            set
            {
                this.ViewState["BackColor"] = value;
                this.SetBit(8);
            }
        }

        public Color BorderColor
        {
            get => 
                !this.IsSet(0x10) ? DXColor.Empty : ((Color) this.ViewState["BorderColor"]);
            set
            {
                this.ViewState["BorderColor"] = value;
                this.SetBit(0x10);
            }
        }

        public DXWebBorderStyle BorderStyle
        {
            get => 
                !this.IsSet(0x40) ? DXWebBorderStyle.NotSet : ((DXWebBorderStyle) this.ViewState["BorderStyle"]);
            set
            {
                if ((value < DXWebBorderStyle.NotSet) || (value > DXWebBorderStyle.Outset))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["BorderStyle"] = value;
                this.SetBit(0x40);
            }
        }

        public DXWebUnit BorderWidth
        {
            get => 
                !this.IsSet(0x20) ? DXWebUnit.Empty : ((DXWebUnit) this.ViewState["BorderWidth"]);
            set
            {
                if ((value.Type == DXWebUnitType.Percentage) || (value.Value < 0.0))
                {
                    throw new ArgumentOutOfRangeException("value", "Style_InvalidBorderWidth");
                }
                this.ViewState["BorderWidth"] = value;
                this.SetBit(0x20);
            }
        }

        public string CssClass
        {
            get
            {
                if (this.IsSet(2))
                {
                    string str = (string) this.ViewState["class"];
                    if (str != null)
                    {
                        return str;
                    }
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["class"] = value;
                this.SetBit(2);
            }
        }

        public DXWebFontInfo Font
        {
            get
            {
                this.fontInfo ??= new DXWebFontInfo(this);
                return this.fontInfo;
            }
        }

        public Color ForeColor
        {
            get => 
                !this.IsSet(4) ? DXColor.Empty : ((Color) this.ViewState["ForeColor"]);
            set
            {
                this.ViewState["ForeColor"] = value;
                this.SetBit(4);
            }
        }

        public DXWebUnit Height
        {
            get => 
                !this.IsSet(0x80) ? DXWebUnit.Empty : ((DXWebUnit) this.ViewState["Height"]);
            set
            {
                if (value.Value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["Height"] = value;
                this.SetBit(0x80);
            }
        }

        public virtual bool IsEmpty =>
            (this.setBits == 0) && (this.RegisteredCssClass.Length == 0);

        protected bool IsTrackingViewState =>
            this.marked;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string RegisteredCssClass =>
            (this.registeredCssClass != null) ? this.registeredCssClass : string.Empty;

        public DXWebUnit Width
        {
            get => 
                !this.IsSet(0x100) ? DXWebUnit.Empty : ((DXWebUnit) this.ViewState["Width"]);
            set
            {
                if (value.Value < 0.0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["Width"] = value;
                this.SetBit(0x100);
            }
        }

        protected internal DXStateBag ViewState
        {
            get
            {
                if (this.statebag == null)
                {
                    this.statebag = new DXStateBag();
                    if (this.IsTrackingViewState)
                    {
                        this.statebag.TrackViewState();
                    }
                }
                return this.statebag;
            }
        }
    }
}

