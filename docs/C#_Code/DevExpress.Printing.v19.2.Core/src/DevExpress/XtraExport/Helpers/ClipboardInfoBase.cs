namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ClipboardInfoBase
    {
        protected readonly ClipboardInfoBase initialInfo;
        protected Color backColorCore;
        protected ClipboardCellBorderInfo borderBottomCore;
        protected ClipboardCellBorderInfo borderLeftCore;
        protected ClipboardCellBorderInfo borderRightCore;
        protected ClipboardCellBorderInfo borderTopCore;
        protected string displayValueCore;
        protected System.Drawing.Font fontCore;
        protected Color foreColorCore;
        protected readonly string InvalidDisplayValue;
        protected int rowHandleCore;
        protected object valueCore;

        protected ClipboardInfoBase()
        {
            this.InvalidDisplayValue = Guid.NewGuid().ToString();
        }

        protected ClipboardInfoBase(ClipboardInfoBase other)
        {
            this.InvalidDisplayValue = Guid.NewGuid().ToString();
            this.backColorCore = other.BackColor;
            this.borderBottomCore = new ClipboardCellBorderInfo(other.borderBottomCore);
            this.borderLeftCore = new ClipboardCellBorderInfo(other.borderLeftCore);
            this.borderRightCore = new ClipboardCellBorderInfo(other.borderRightCore);
            this.borderTopCore = new ClipboardCellBorderInfo(other.borderTopCore);
            this.displayValueCore = other.displayValueCore;
            if (other.fontCore != null)
            {
                this.fontCore = new System.Drawing.Font(other.fontCore, other.fontCore.Style);
            }
            this.foreColorCore = other.foreColorCore;
            this.<OriginalValue>k__BackingField = other.OriginalValue;
            this.valueCore = other.valueCore;
        }

        public ClipboardInfoBase(int rowHandle, ClipboardCellInfo rowInfo)
        {
            this.InvalidDisplayValue = Guid.NewGuid().ToString();
            this.displayValueCore = this.InvalidDisplayValue;
            this.rowHandleCore = rowHandle;
            this.backColorCore = (rowInfo.Formatting.Fill == null) ? Color.Empty : rowInfo.Formatting.Fill.ForeColor.Rgb;
            this.fontCore = this.Convert(rowInfo.Formatting.Font);
            this.foreColorCore = (Color) rowInfo.Formatting.Font.Color;
            this.<OriginalValue>k__BackingField = rowInfo.Value;
            this.valueCore = rowInfo.Value;
            if (rowInfo.Formatting.Border != null)
            {
                this.borderLeftCore = this.Convert(rowInfo.Formatting.Border, BorderSide.Left);
                this.borderTopCore = this.Convert(rowInfo.Formatting.Border, BorderSide.Top);
                this.borderRightCore = this.Convert(rowInfo.Formatting.Border, BorderSide.Right);
                this.borderBottomCore = this.Convert(rowInfo.Formatting.Border, BorderSide.Bottom);
            }
            this.initialInfo = new ClipboardInfoBase(this);
        }

        private BorderStyle Convert(XlBorderLineStyle xlBorderLineStyle) => 
            (BorderStyle) Enum.Parse(typeof(BorderStyle), xlBorderLineStyle.ToString(), true);

        private System.Drawing.Font Convert(XlFont xlFont)
        {
            FontStyle regular = FontStyle.Regular;
            if (xlFont.Bold)
            {
                regular |= FontStyle.Bold;
            }
            if (xlFont.Italic)
            {
                regular |= FontStyle.Italic;
            }
            if (xlFont.Underline == XlUnderlineType.Single)
            {
                regular |= FontStyle.Underline;
            }
            return new System.Drawing.Font(xlFont.Name, (float) xlFont.Size, regular);
        }

        private ClipboardCellBorderInfo Convert(XlBorder xlBorder, BorderSide side)
        {
            ClipboardCellBorderInfo info;
            switch (side)
            {
                case BorderSide.Left:
                    info = new ClipboardCellBorderInfo((Color) xlBorder.LeftColor, this.Convert(xlBorder.LeftLineStyle));
                    break;

                case BorderSide.Top:
                    info = new ClipboardCellBorderInfo((Color) xlBorder.TopColor, this.Convert(xlBorder.TopLineStyle));
                    break;

                case BorderSide.Right:
                    info = new ClipboardCellBorderInfo((Color) xlBorder.RightColor, this.Convert(xlBorder.RightLineStyle));
                    break;

                case BorderSide.Bottom:
                    info = new ClipboardCellBorderInfo((Color) xlBorder.BottomColor, this.Convert(xlBorder.BottomLineStyle));
                    break;

                default:
                    info = new ClipboardCellBorderInfo();
                    break;
            }
            return info;
        }

        public override bool Equals(object obj)
        {
            ClipboardInfoBase base2 = obj as ClipboardInfoBase;
            return ((base2 != null) ? (!Equals(this.Value, base2.Value) || (!Equals(this.ForeColor, base2.ForeColor) || (!Equals(this.BackColor, base2.BackColor) || (!Equals(this.Font, base2.Font) || (!Equals(this.BorderLeft, base2.BorderLeft) || (!Equals(this.BorderTop, base2.BorderTop) || (!Equals(this.BorderRight, base2.BorderRight) || !Equals(this.BorderBottom, base2.BorderBottom)))))))) : false);
        }

        protected virtual string GetDisplayValueText(object value) => 
            this.Value.ToString();

        public override int GetHashCode() => 
            base.GetHashCode();

        private void OnValueChanged(object value)
        {
            this.displayValueCore = this.GetDisplayValueText(value);
        }

        public void UpdateOriginalBorder(XlBorder xlBorder)
        {
            xlBorder.LeftColor = this.BorderLeft.Color;
            xlBorder.TopColor = this.BorderTop.Color;
            xlBorder.RightColor = this.BorderRight.Color;
            xlBorder.BottomColor = this.BorderBottom.Color;
            xlBorder.LeftLineStyle = (XlBorderLineStyle) Enum.Parse(typeof(XlBorderLineStyle), this.BorderLeft.BorderStyle.ToString(), true);
            xlBorder.TopLineStyle = (XlBorderLineStyle) Enum.Parse(typeof(XlBorderLineStyle), this.BorderTop.BorderStyle.ToString(), true);
            xlBorder.RightLineStyle = (XlBorderLineStyle) Enum.Parse(typeof(XlBorderLineStyle), this.BorderRight.BorderStyle.ToString(), true);
            xlBorder.BottomLineStyle = (XlBorderLineStyle) Enum.Parse(typeof(XlBorderLineStyle), this.BorderBottom.BorderStyle.ToString(), true);
        }

        public Color BackColor
        {
            get => 
                this.backColorCore;
            set
            {
                if (this.backColorCore != value)
                {
                    this.backColorCore = value;
                }
            }
        }

        public ClipboardCellBorderInfo BorderBottom
        {
            get => 
                this.borderBottomCore;
            set
            {
                if (!this.borderBottomCore.Equals(value))
                {
                    this.borderBottomCore = value;
                }
            }
        }

        public ClipboardCellBorderInfo BorderLeft
        {
            get => 
                this.borderLeftCore;
            set
            {
                if (!this.borderLeftCore.Equals(value))
                {
                    this.borderLeftCore = value;
                }
            }
        }

        public ClipboardCellBorderInfo BorderRight
        {
            get => 
                this.borderRightCore;
            set
            {
                if (!this.borderRightCore.Equals(value))
                {
                    this.borderRightCore = value;
                }
            }
        }

        public ClipboardCellBorderInfo BorderTop
        {
            get => 
                this.borderTopCore;
            set
            {
                if (!this.borderTopCore.Equals(value))
                {
                    this.borderTopCore = value;
                }
            }
        }

        public string DisplayValue
        {
            get
            {
                if (this.InvalidDisplayValue == this.displayValueCore)
                {
                    this.displayValueCore = this.GetDisplayValueText(this.Value);
                }
                return this.displayValueCore;
            }
        }

        public System.Drawing.Font Font
        {
            get => 
                this.fontCore;
            set
            {
                if (!ReferenceEquals(this.fontCore, value))
                {
                    this.fontCore = value;
                }
            }
        }

        public Color ForeColor
        {
            get => 
                this.foreColorCore;
            set
            {
                if (this.foreColorCore != value)
                {
                    this.foreColorCore = value;
                }
            }
        }

        public bool IsModified =>
            this.Equals(this.initialInfo);

        public object OriginalValue { get; }

        public object Value
        {
            get => 
                this.valueCore;
            set
            {
                if (this.valueCore != value)
                {
                    this.valueCore = value;
                    this.OnValueChanged(value);
                }
            }
        }

        private enum BorderSide
        {
            Left,
            Top,
            Right,
            Bottom
        }
    }
}

