namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextState
    {
        private double characterSpacing;
        private double wordSpacing;
        private double absoluteHorizontalScaling = 100.0;
        private double horizontalScaling = 100.0;
        private double leading;
        private PdfFont font;
        private double absoluteFontSize;
        private double fontSize;
        private PdfTextRenderingMode renderingMode;
        private double rise;
        private double knockout;
        private PdfTransformationMatrix textLineMatrix = PdfTransformationMatrix.Identity;
        private PdfTransformationMatrix textMatrix = PdfTransformationMatrix.Identity;

        public PdfTextState Clone() => 
            new PdfTextState { 
                characterSpacing = this.characterSpacing,
                wordSpacing = this.wordSpacing,
                absoluteHorizontalScaling = this.absoluteHorizontalScaling,
                horizontalScaling = this.horizontalScaling,
                leading = this.leading,
                font = this.font,
                absoluteFontSize = this.absoluteFontSize,
                fontSize = this.fontSize,
                renderingMode = this.renderingMode,
                rise = this.rise,
                knockout = this.knockout
            };

        public double CharacterSpacing
        {
            get => 
                this.characterSpacing;
            set => 
                this.characterSpacing = value;
        }

        public double WordSpacing
        {
            get => 
                this.wordSpacing;
            set => 
                this.wordSpacing = value;
        }

        public double AbsoluteHorizontalScaling
        {
            get => 
                this.absoluteHorizontalScaling;
            set => 
                this.absoluteHorizontalScaling = value;
        }

        public double HorizontalScaling
        {
            get => 
                this.horizontalScaling;
            set => 
                this.horizontalScaling = value;
        }

        public double Leading
        {
            get => 
                this.leading;
            set => 
                this.leading = value;
        }

        public PdfFont Font
        {
            get => 
                this.font;
            set => 
                this.font = value;
        }

        public double AbsoluteFontSize =>
            this.absoluteFontSize;

        public double FontSize
        {
            get => 
                this.fontSize;
            set
            {
                this.fontSize = value;
                this.absoluteFontSize = Math.Abs(this.fontSize);
            }
        }

        public PdfTextRenderingMode RenderingMode
        {
            get => 
                this.renderingMode;
            set => 
                this.renderingMode = value;
        }

        public double Rise
        {
            get => 
                this.rise;
            set => 
                this.rise = value;
        }

        public double Knockout
        {
            get => 
                this.knockout;
            set => 
                this.knockout = value;
        }

        public PdfTransformationMatrix TextLineMatrix
        {
            get => 
                this.textLineMatrix;
            set => 
                this.textLineMatrix = value;
        }

        public PdfTransformationMatrix TextMatrix
        {
            get => 
                this.textMatrix;
            set => 
                this.textMatrix = value;
        }

        public PdfTransformationMatrix TextSpaceMatrix =>
            new PdfTransformationMatrix((this.fontSize * this.horizontalScaling) / 100.0, 0.0, 0.0, this.fontSize, 0.0, this.rise);
    }
}

