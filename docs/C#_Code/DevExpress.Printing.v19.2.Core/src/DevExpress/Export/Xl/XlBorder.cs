namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;

    public class XlBorder : XlBordersBase, ISupportsCopyFrom<XlBorder>
    {
        private XlColor leftColor = XlColor.Empty;
        private XlColor rightColor = XlColor.Empty;
        private XlColor topColor = XlColor.Empty;
        private XlColor bottomColor = XlColor.Empty;
        private XlColor diagonalColor = XlColor.Empty;
        private XlColor horizontalColor = XlColor.Empty;
        private XlColor verticalColor = XlColor.Empty;

        public static XlBorder AllBorders(XlColor color) => 
            AllBorders(color, XlBorderLineStyle.Thin);

        public static XlBorder AllBorders(XlColor color, XlBorderLineStyle lineStyle)
        {
            XlBorder border = OutlineBorders(color, lineStyle);
            border.HorizontalColor = color;
            border.VerticalColor = color;
            border.HorizontalLineStyle = lineStyle;
            border.VerticalLineStyle = lineStyle;
            return border;
        }

        public XlBorder Clone()
        {
            XlBorder border = new XlBorder();
            border.CopyFrom(this);
            return border;
        }

        public void CopyFrom(XlBorder other)
        {
            Guard.ArgumentNotNull(other, "other");
            base.CopyFrom(other);
            this.LeftColor = other.LeftColor;
            this.RightColor = other.RightColor;
            this.TopColor = other.TopColor;
            this.BottomColor = other.BottomColor;
            this.DiagonalColor = other.DiagonalColor;
            this.HorizontalColor = other.HorizontalColor;
            this.VerticalColor = other.VerticalColor;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            XlBorder border = obj as XlBorder;
            return ((border != null) ? (!base.HasBorderLines || (((base.LeftLineStyle == XlBorderLineStyle.None) || this.LeftColor.Equals(border.LeftColor)) ? (((base.RightLineStyle == XlBorderLineStyle.None) || this.RightColor.Equals(border.RightColor)) ? (((base.TopLineStyle == XlBorderLineStyle.None) || this.TopColor.Equals(border.TopColor)) ? (((base.BottomLineStyle == XlBorderLineStyle.None) || this.BottomColor.Equals(border.BottomColor)) ? (((base.DiagonalLineStyle == XlBorderLineStyle.None) || this.DiagonalColor.Equals(border.DiagonalColor)) ? (((base.HorizontalLineStyle == XlBorderLineStyle.None) || this.HorizontalColor.Equals(border.HorizontalColor)) ? ((base.VerticalLineStyle == XlBorderLineStyle.None) || this.VerticalColor.Equals(border.VerticalColor)) : false) : false) : false) : false) : false) : false)) : false);
        }

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            if (base.HasBorderLines)
            {
                if (base.LeftLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.LeftColor.GetHashCode();
                }
                if (base.RightLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.RightColor.GetHashCode();
                }
                if (base.TopLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.TopColor.GetHashCode();
                }
                if (base.BottomLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.BottomColor.GetHashCode();
                }
                if (base.DiagonalLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.DiagonalColor.GetHashCode();
                }
                if (base.HorizontalLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.HorizontalColor.GetHashCode();
                }
                if (base.VerticalLineStyle != XlBorderLineStyle.None)
                {
                    hashCode ^= this.VerticalColor.GetHashCode();
                }
            }
            return hashCode;
        }

        [Browsable(false)]
        public static XlBorder InsideBorders(XlColor color) => 
            InsideBorders(color, XlBorderLineStyle.Thin);

        [Browsable(false)]
        public static XlBorder InsideBorders(XlColor color, XlBorderLineStyle lineStyle) => 
            new XlBorder { 
                HorizontalColor = color,
                VerticalColor = color,
                HorizontalLineStyle = lineStyle,
                VerticalLineStyle = lineStyle
            };

        public static XlBorder NoBorders() => 
            new XlBorder();

        public static XlBorder OutlineBorders(XlColor color) => 
            OutlineBorders(color, XlBorderLineStyle.Thin);

        public static XlBorder OutlineBorders(XlColor color, XlBorderLineStyle lineStyle) => 
            new XlBorder { 
                TopColor = color,
                BottomColor = color,
                LeftColor = color,
                RightColor = color,
                TopLineStyle = lineStyle,
                BottomLineStyle = lineStyle,
                LeftLineStyle = lineStyle,
                RightLineStyle = lineStyle
            };

        public XlColor LeftColor
        {
            get => 
                this.leftColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.leftColor = empty;
            }
        }

        public XlColor RightColor
        {
            get => 
                this.rightColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.rightColor = empty;
            }
        }

        public XlColor TopColor
        {
            get => 
                this.topColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.topColor = empty;
            }
        }

        public XlColor BottomColor
        {
            get => 
                this.bottomColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.bottomColor = empty;
            }
        }

        public XlColor DiagonalColor
        {
            get => 
                this.diagonalColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.diagonalColor = empty;
            }
        }

        [Browsable(false)]
        public XlColor HorizontalColor
        {
            get => 
                this.horizontalColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.horizontalColor = empty;
            }
        }

        [Browsable(false)]
        public XlColor VerticalColor
        {
            get => 
                this.verticalColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.verticalColor = empty;
            }
        }
    }
}

