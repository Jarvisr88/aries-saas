namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfEditorBorder
    {
        private readonly PdfEditorBorderStyle borderStyle;
        private readonly PdfColor color;
        private readonly double borderWidth = 1.0;
        private readonly double horizontalRadius;
        private readonly double verticalRadius;
        private readonly double[] dashPattern;
        private readonly double dashPhase;

        public PdfEditorBorder(PdfWidgetAnnotation widget)
        {
            PdfWidgetAppearanceCharacteristics appearanceCharacteristics = widget.AppearanceCharacteristics;
            this.color = appearanceCharacteristics?.BorderColor;
            PdfAnnotationBorderStyle borderStyle = widget.BorderStyle;
            if (borderStyle == null)
            {
                PdfAnnotationBorder border = widget.Border;
                if (border != null)
                {
                    this.horizontalRadius = border.HorizontalCornerRadius;
                    this.verticalRadius = border.VerticalCornerRadius;
                    this.borderWidth = (this.color == null) ? 0.0 : border.LineWidth;
                    PdfLineStyle lineStyle = border.LineStyle;
                    if ((lineStyle != null) && lineStyle.IsDashed)
                    {
                        this.borderStyle = PdfEditorBorderStyle.Dashed;
                        this.dashPattern = lineStyle.DashPattern;
                    }
                }
            }
            else
            {
                string styleName = borderStyle.StyleName;
                if (styleName != "D")
                {
                    this.borderStyle = (styleName == "B") ? PdfEditorBorderStyle.Beveled : ((styleName == "I") ? PdfEditorBorderStyle.Inset : ((styleName == "U") ? PdfEditorBorderStyle.Underline : PdfEditorBorderStyle.Solid));
                }
                else
                {
                    this.borderStyle = PdfEditorBorderStyle.Dashed;
                    PdfLineStyle lineStyle = borderStyle.LineStyle;
                    if (lineStyle != null)
                    {
                        this.dashPattern = lineStyle.DashPattern;
                        this.dashPhase = lineStyle.DashPhase;
                    }
                }
                this.borderWidth = (this.color == null) ? 0.0 : borderStyle.Width;
            }
        }

        public PdfEditorBorderStyle BorderStyle =>
            this.borderStyle;

        public PdfColor Color =>
            this.color;

        public double BorderWidth =>
            this.borderWidth;

        public double HorizontalRadius =>
            this.horizontalRadius;

        public double VerticalRadius =>
            this.verticalRadius;

        public double[] DashPattern =>
            this.dashPattern;

        public double DashPhase =>
            this.dashPhase;
    }
}

