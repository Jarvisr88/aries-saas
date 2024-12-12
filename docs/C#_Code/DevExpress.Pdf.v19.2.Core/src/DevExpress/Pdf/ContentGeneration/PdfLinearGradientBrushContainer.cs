namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfLinearGradientBrushContainer : PdfBrushContainer
    {
        private const double epsilon = 0.1;
        private readonly DXLinearGradientBrush brush;

        public PdfLinearGradientBrushContainer(DXLinearGradientBrush brush)
        {
            this.brush = brush;
        }

        public override bool FillShape(PdfGraphicsCommandConstructor commandConstructor, PdfShapeFillingStrategy shape)
        {
            DXBlend blend = this.brush.Blend;
            if ((this.brush.WrapMode == DXWrapMode.Clamp) || ((this.brush.InterpolationColors != null) || ((blend != null) && (blend.Factors.Length > 1))))
            {
                return false;
            }
            PdfRectangle rectangle = PdfRectangle.CreateBoundingBox(PdfTransformationMatrix.Invert(GetTransformationMatrix(this.brush)).Transform(shape.ShapePoints));
            PdfRectangle rectangle2 = PdfGraphicsConverter.ConvertRectangle(this.brush.Rectangle);
            double left = rectangle2.Left;
            double bottom = rectangle2.Bottom;
            double width = rectangle2.Width;
            double height = rectangle2.Height;
            double num5 = rectangle.Left - left;
            double num6 = rectangle.Bottom - bottom;
            double num7 = Math.Floor((double) (num5 / width));
            double num8 = Math.Floor((double) (num6 / height));
            if (Math.Abs((double) ((num5 - (num7 * width)) - width)) < 0.1)
            {
                num7++;
            }
            if (Math.Abs((double) ((num6 - (num8 * height)) - height)) < 0.1)
            {
                num8++;
            }
            double e = (num7 * width) + left;
            double f = (num8 * height) + bottom;
            ARGBColor[] linearColors = this.brush.LinearColors;
            if ((((rectangle.Right - e) - width) >= 0.1) || (((rectangle.Top - f) - height) >= 0.1))
            {
                return false;
            }
            ARGBColor startColor = linearColors[0];
            ARGBColor endColor = linearColors[1];
            byte a = startColor.A;
            bool flag = a != endColor.A;
            if (!flag)
            {
                commandConstructor.SetNonStrokingAlpha(((double) a) / 255.0);
            }
            PdfCommandConstructor constructor = commandConstructor.CommandConstructor;
            commandConstructor.SaveGraphicsState();
            shape.Clip(constructor);
            constructor.ModifyTransformationMatrix(PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(width, 0.0, 0.0, height, e, f), GetTransformationMatrix(this.brush)), commandConstructor.CoordinateTransformationMatrix));
            if (flag)
            {
                PdfLuminositySoftMask mask = PdfLuminositySoftMaskBuilder.CreateSoftMask(commandConstructor.ShadingCache.GetShading(ARGBColor.FromArgb(a, 0, 0), ARGBColor.FromArgb(endColor.A, 0, 0)), new PdfRectangle(0.0, 0.0, 1.0, 1.0), commandConstructor.DocumentCatalog);
                PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters();
                parameters.SoftMask = mask;
                constructor.SetGraphicsStateParameters(parameters);
            }
            constructor.DrawShading(commandConstructor.ShadingCache.GetShading(startColor, endColor));
            commandConstructor.RestoreGraphicsState();
            return true;
        }

        public override PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor)
        {
            PdfLinearGradientPatternConstructor constructor = PdfLinearGradientPatternConstructor.Create(this.brush, commandConstructor.BBox, GetActualTransformationMatrix(commandConstructor, this.brush));
            return new PdfTransparentColor(constructor.Alpha, constructor.CreatePattern(commandConstructor), new double[0]);
        }
    }
}

