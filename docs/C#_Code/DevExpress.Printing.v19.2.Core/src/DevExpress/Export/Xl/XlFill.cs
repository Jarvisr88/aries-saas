namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlFill : ISupportsCopyFrom<XlFill>
    {
        private XlColor foreColor = XlColor.Empty;
        private XlColor backColor = XlColor.Empty;

        public XlFill Clone()
        {
            XlFill fill = new XlFill();
            fill.CopyFrom(this);
            return fill;
        }

        public void CopyFrom(XlFill value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.PatternType = value.PatternType;
            this.ForeColor = value.ForeColor;
            this.BackColor = value.BackColor;
        }

        public override bool Equals(object obj)
        {
            XlFill fill = obj as XlFill;
            return ((fill != null) ? ((this.PatternType == fill.PatternType) && (this.ForeColor.Equals(fill.ForeColor) && this.BackColor.Equals(fill.BackColor))) : false);
        }

        public override int GetHashCode() => 
            (((int) this.PatternType) ^ this.ForeColor.GetHashCode()) ^ this.BackColor.GetHashCode();

        public static XlFill NoFill() => 
            new XlFill();

        public static XlFill PatternFill(XlPatternType patternType) => 
            new XlFill { PatternType = patternType };

        public static XlFill PatternFill(XlPatternType patternType, XlColor backColor, XlColor patternColor) => 
            new XlFill { 
                PatternType = patternType,
                ForeColor = patternColor,
                BackColor = backColor
            };

        public static XlFill SolidFill(XlColor color) => 
            new XlFill { 
                PatternType = XlPatternType.Solid,
                ForeColor = color
            };

        public XlPatternType PatternType { get; set; }

        public XlColor ForeColor
        {
            get => 
                this.foreColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.foreColor = empty;
            }
        }

        public XlColor BackColor
        {
            get => 
                this.backColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.backColor = empty;
            }
        }
    }
}

