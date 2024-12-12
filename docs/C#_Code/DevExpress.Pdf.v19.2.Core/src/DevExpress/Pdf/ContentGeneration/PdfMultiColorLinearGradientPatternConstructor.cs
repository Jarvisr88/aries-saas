namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public class PdfMultiColorLinearGradientPatternConstructor : PdfLinearGradientPatternConstructor
    {
        public PdfMultiColorLinearGradientPatternConstructor(DXLinearGradientBrush brush, PdfTransformationMatrix actualBrushTransform) : base(brush, actualBrushTransform)
        {
        }

        protected override PdfLinearGradientShadingBuilder CreateBuilder() => 
            new PdfMultiColorLinearGradientShadingBuilder(base.Brush.InterpolationColors);

        protected override PdfLinearGradientShadingBuilder CreateMaskBuilder()
        {
            DXColorBlend interpolationColors = base.Brush.InterpolationColors;
            ARGBColor[] colors = interpolationColors.Colors;
            if (!this.IsTransparencyGradient)
            {
                return null;
            }
            ARGBColor[] colorArray2 = new ARGBColor[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                byte a = colors[i].A;
                colorArray2[i] = ARGBColor.FromArgb(a, a, a);
            }
            DXColorBlend colorBlend = new DXColorBlend();
            colorBlend.Colors = colorArray2;
            colorBlend.Positions = interpolationColors.Positions;
            return new PdfMultiColorLinearGradientShadingBuilder(colorBlend);
        }

        private bool IsTransparencyGradient
        {
            get
            {
                ARGBColor[] colors = base.Brush.InterpolationColors.Colors;
                byte a = colors[0].A;
                bool flag = false;
                for (int i = 1; i < colors.Length; i++)
                {
                    flag |= colors[i].A != a;
                }
                return flag;
            }
        }

        public override byte Alpha =>
            !this.IsTransparencyGradient ? base.Brush.InterpolationColors.Colors[0].A : 0xff;
    }
}

