namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using System;

    public class PdfTwoColorLinearGradientPatternConstructor : PdfLinearGradientPatternConstructor
    {
        public PdfTwoColorLinearGradientPatternConstructor(DXLinearGradientBrush brush, PdfTransformationMatrix actualBrushTransform) : base(brush, actualBrushTransform)
        {
        }

        protected override PdfLinearGradientShadingBuilder CreateBuilder() => 
            new PdfTwoColorLinearGradientShadingBuilder(base.Brush.LinearColors, base.Brush.Blend);

        protected override PdfLinearGradientShadingBuilder CreateMaskBuilder()
        {
            ARGBColor[] linearColors = base.Brush.LinearColors;
            if (!this.IsTransparencyGradient)
            {
                return null;
            }
            ARGBColor[] brushColors = new ARGBColor[2];
            for (int i = 0; i < 2; i++)
            {
                byte a = linearColors[i].A;
                brushColors[i] = ARGBColor.FromArgb(a, a, a);
            }
            return new PdfTwoColorLinearGradientShadingBuilder(brushColors, base.Brush.Blend);
        }

        public override PdfPattern CreatePattern(PdfGraphicsCommandConstructor commandConstructor)
        {
            DXLinearGradientBrush brush = base.Brush;
            if (this.IsTransparencyGradient || (brush.Blend.Positions.Length > 1))
            {
                return base.CreatePattern(commandConstructor);
            }
            ARGBColor[] linearColors = brush.LinearColors;
            PdfRectangle tileBounds = PdfGraphicsConverter.ConvertRectangle(brush.Rectangle);
            return PdfBrushPatternConstructor.CreatePattern(brush.WrapMode, base.ActualBrushTransform, tileBounds, commandConstructor.ShadingCache.GetShading(linearColors[0], linearColors[1]), null, commandConstructor.DocumentCatalog, new PdfTransformationMatrix(tileBounds.Width, 0.0, 0.0, 1.0, tileBounds.Left, tileBounds.Bottom));
        }

        private bool IsTransparencyGradient
        {
            get
            {
                ARGBColor[] linearColors = base.Brush.LinearColors;
                return (linearColors[0].A != linearColors[1].A);
            }
        }

        public override byte Alpha =>
            !this.IsTransparencyGradient ? base.Brush.LinearColors[0].A : 0xff;
    }
}

