namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class BrickStyleHelper
    {
        private static BrickStyleHelper instance;

        public virtual BrickStyle ChangeBackColor(BrickStyle source, Color color)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.BackColor = color;
            return style;
        }

        public virtual BrickStyle ChangeBorderColor(BrickStyle source, Color color)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.BorderColor = color;
            return style;
        }

        public virtual BrickStyle ChangeBorderDashStyle(BrickStyle source, BorderDashStyle borderDashStyle)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.BorderDashStyle = borderDashStyle;
            return style;
        }

        public virtual BrickStyle ChangeBorderStyle(BrickStyle source, BrickBorderStyle borderStyle)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.BorderStyle = borderStyle;
            return style;
        }

        public virtual BrickStyle ChangeBorderWidth(BrickStyle source, float width)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.BorderWidth = width;
            return style;
        }

        public virtual BrickStyle ChangeFont(BrickStyle source, Font font)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.Font = font;
            return style;
        }

        public virtual BrickStyle ChangeForeColor(BrickStyle source, Color color)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.ForeColor = color;
            return style;
        }

        public virtual BrickStyle ChangePadding(BrickStyle source, PaddingInfo padding)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.Padding = padding;
            return style;
        }

        public virtual BrickStyle ChangeProperties(BrickStyle source, IDictionary<StyleProperty, object> properties, BrickStringFormat stringFormat = null)
        {
            BrickStyle val = (BrickStyle) source.Clone();
            foreach (KeyValuePair<StyleProperty, object> pair in properties)
            {
                val.SetValue(pair.Key, pair.Value);
            }
            if (stringFormat != null)
            {
                this.SetStringFormatCore(val, stringFormat);
            }
            return val;
        }

        public virtual BrickStyle ChangeSides(BrickStyle source, BorderSide sides)
        {
            BrickStyle style = (BrickStyle) source.Clone();
            style.Sides = sides;
            return style;
        }

        public virtual BrickStyle ChangeStringFormat(BrickStyle source, BrickStringFormat sf)
        {
            BrickStyle val = (BrickStyle) source.Clone();
            this.SetStringFormatCore(val, sf);
            return val;
        }

        public BrickStyle ChangeStringFormat(BrickStyle source, StringFormatFlags flags)
        {
            BrickStringFormat sf = source.StringFormat.ChangeFormatFlags(flags);
            return this.ChangeStringFormat(source, sf);
        }

        private void SetStringFormatCore(BrickStyle val, BrickStringFormat sf)
        {
            val.StringFormat = sf;
            val.TextAlignment = TextAlignmentConverter.ToTextAlignment(sf.Alignment, sf.LineAlignment);
        }

        public static BrickStyleHelper Instance
        {
            get
            {
                instance ??= new BrickStyleHelper();
                return instance;
            }
            set => 
                instance = value;
        }
    }
}

