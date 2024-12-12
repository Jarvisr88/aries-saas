namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class XamlTextBlockStyle : XamlResourceBase
    {
        private System.Drawing.Font font;
        private DevExpress.XtraPrinting.TextAlignment textAlignment;
        private bool wrapText;
        private Color foreground;
        private System.Drawing.StringTrimming stringTrimming;

        public XamlTextBlockStyle(System.Drawing.Font font, DevExpress.XtraPrinting.TextAlignment textAlignment, bool wrapText, Color foreground, System.Drawing.StringTrimming stringTrimming)
        {
            this.font = font;
            this.textAlignment = textAlignment;
            this.wrapText = wrapText;
            this.foreground = foreground;
            this.stringTrimming = stringTrimming;
        }

        public override bool Equals(object obj)
        {
            XamlTextBlockStyle style = obj as XamlTextBlockStyle;
            return ((style != null) ? ((style.TextAlignment == this.textAlignment) && ((style.WrapText == this.wrapText) && ((style.Foreground == this.foreground) && ((style.Font.Size == this.font.Size) && ((style.Font.Bold == this.font.Bold) && ((FontHelper.GetFamilyName(style.Font) == FontHelper.GetFamilyName(this.font)) && ((style.Font.Italic == this.font.Italic) && ((style.Font.Strikeout == this.font.Strikeout) && ((style.Font.Underline == this.font.Underline) && ((style.Font.Unit == this.font.Unit) && (style.stringTrimming == this.stringTrimming))))))))))) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGeneric<int, int, int, int>(this.font.GetHashCode(), (int) this.textAlignment, this.foreground.GetHashCode(), this.stringTrimming.GetHashCode());

        public System.Drawing.Font Font =>
            this.font;

        public DevExpress.XtraPrinting.TextAlignment TextAlignment =>
            this.textAlignment;

        public bool WrapText =>
            this.wrapText;

        public Color Foreground =>
            this.foreground;

        public System.Drawing.StringTrimming StringTrimming =>
            this.stringTrimming;
    }
}

