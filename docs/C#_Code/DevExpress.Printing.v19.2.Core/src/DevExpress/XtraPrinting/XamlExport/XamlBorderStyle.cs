namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class XamlBorderStyle : XamlResourceBase
    {
        private float borderWidth;
        private Color borderBrush;
        private BorderSide sides;
        private Color backColor;
        private PaddingInfo padding;

        public XamlBorderStyle(float borderWidth, Color borderBrush, BorderSide sides, Color backColor, PaddingInfo padding)
        {
            this.borderWidth = borderWidth;
            this.borderBrush = borderBrush;
            this.sides = sides;
            this.backColor = backColor;
            this.padding = padding;
        }

        public override bool Equals(object obj)
        {
            XamlBorderStyle style = obj as XamlBorderStyle;
            return ((style != null) ? ((style.BorderWidth == this.borderWidth) && ((style.BorderBrush == this.borderBrush) && ((style.Sides == this.sides) && ((style.BackColor == this.backColor) && (style.Padding == this.padding))))) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<int, int, int, int, int>((int) this.borderWidth, this.borderBrush.GetHashCode(), (int) this.sides, this.backColor.GetHashCode(), this.padding.GetHashCode());

        public float BorderWidth =>
            this.borderWidth;

        public Color BorderBrush =>
            this.borderBrush;

        public BorderSide Sides =>
            this.sides;

        public Color BackColor =>
            this.backColor;

        public PaddingInfo Padding =>
            this.padding;
    }
}

