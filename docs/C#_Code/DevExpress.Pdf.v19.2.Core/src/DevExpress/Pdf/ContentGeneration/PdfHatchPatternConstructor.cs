namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfHatchPatternConstructor
    {
        private const double DefaultLineStep = 6.0;
        private const double DefaultLineWidth = 0.699999988079071;
        private readonly ARGBColor foreColor;
        private readonly ARGBColor backColor;
        private readonly double lineWidth;
        private readonly double lineStep;
        private PdfTransformationMatrix transform;

        protected PdfHatchPatternConstructor(DXHatchBrush hatchBrush)
        {
            this.foreColor = hatchBrush.ForegroundColor;
            this.backColor = hatchBrush.BackgroundColor;
            this.lineWidth = 0.699999988079071;
            this.lineStep = 6.0;
            this.transform = new PdfTransformationMatrix();
        }

        public static PdfHatchPatternConstructor Create(DXHatchBrush hatchBrush)
        {
            switch (hatchBrush.HatchStyle)
            {
                case DXHatchStyle.Min:
                    return new PdfHorizontalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Vertical:
                    return new PdfVerticalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.ForwardDiagonal:
                    return new PdfForwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.BackwardDiagonal:
                    return new PdfBackwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Max:
                    return new PdfCrossHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DiagonalCross:
                    return new PdfDiagonalCrossHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent05:
                    return new PdfPercent05HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent10:
                    return new PdfPercent10HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent20:
                    return new PdfPercent20HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent25:
                    return new PdfPercent25HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent30:
                    return new PdfPercent30HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent40:
                    return new PdfPercent40HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent50:
                    return new PdfPercent50HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent60:
                    return new PdfPercent60HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent70:
                    return new PdfPercent70HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent75:
                    return new PdfPercent75HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent80:
                    return new PdfPercent80HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Percent90:
                    return new PdfPercent90HatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LightDownwardDiagonal:
                    return new PdfLightDownwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LightUpwardDiagonal:
                    return new PdfLightUpwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DarkDownwardDiagonal:
                    return new PdfDarkDownwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DarkUpwardDiagonal:
                    return new PdfDarkUpwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.WideDownwardDiagonal:
                    return new PdfWideDownwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.WideUpwardDiagonal:
                    return new PdfWideUpwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LightVertical:
                    return new PdfLightVerticalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LightHorizontal:
                    return new PdfLightHorizontalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.NarrowVertical:
                    return new PdfNarrowVerticalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.NarrowHorizontal:
                    return new PdfNarrowHorizontalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DarkVertical:
                    return new PdfDarkVerticalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DarkHorizontal:
                    return new PdfDarkHorizontalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DashedDownwardDiagonal:
                    return new PdfDashedDownwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DashedUpwardDiagonal:
                    return new PdfDashedUpwardDiagonalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DashedHorizontal:
                    return new PdfDashedHorizontalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DashedVertical:
                    return new PdfDashedVerticalHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.SmallConfetti:
                    return new PdfSmallConfettiHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LargeConfetti:
                    return new PdfLargeConfettiHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.ZigZag:
                    return new PdfZigZagHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Wave:
                    return new PdfWaveHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DiagonalBrick:
                    return new PdfDiagonalBricksHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.HorizontalBrick:
                    return new PdfHorizontalBricksHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Weave:
                    return new PdfWeaveHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Plaid:
                    return new PdfPlaidHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Divot:
                    return new PdfDivotHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DottedGrid:
                    return new PdfDottedGridHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.DottedDiamond:
                    return new PdfDottedDiamondHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Shingle:
                    return new PdfShingleHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Trellis:
                    return new PdfTrellisHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.Sphere:
                    return new PdfSphereHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.SmallGrid:
                    return new PdfSmallGridHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.SmallCheckerBoard:
                    return new PdfSmallCheckerBoardHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.LargeCheckerBoard:
                    return new PdfCheckerBoardHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.OutlinedDiamond:
                    return new PdfOutlinedDiamondHatchPatternConstructor(hatchBrush);

                case DXHatchStyle.SolidDiamond:
                    return new PdfSolidDiamondHatchPatternConstructor(hatchBrush);
            }
            return null;
        }

        public PdfPattern CreatePattern(PdfRectangle bBox, PdfDocumentCatalog documentCatalog)
        {
            double lineStep = this.LineStep;
            double num2 = this.LineWidth / 2.0;
            PdfTilingPattern pattern = new PdfTilingPattern(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, bBox.Height), this.transform), new PdfRectangle(-num2, -num2, lineStep + num2, lineStep + num2), lineStep, lineStep, documentCatalog);
            using (PdfCommandConstructor constructor = new PdfCommandConstructor(pattern.Resources))
            {
                this.FillCommands(constructor);
                pattern.ReplaceCommands(constructor.Commands);
            }
            return pattern;
        }

        protected virtual void FillCommands(PdfCommandConstructor constructor)
        {
            constructor.SetColorForNonStrokingOperations(PdfGraphicsConverter.ConvertColor(this.backColor));
            PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters();
            parameters.NonStrokingAlphaConstant = new double?((double) (((float) this.backColor.A) / 255f));
            constructor.SetGraphicsStateParameters(parameters);
            constructor.FillRectangle(new PdfRectangle(new PdfPoint(0.0, 0.0), new PdfPoint(this.LineStep, this.LineStep)));
            constructor.SetColorForNonStrokingOperations(PdfGraphicsConverter.ConvertColor(this.foreColor));
            constructor.SetColorForStrokingOperations(new PdfRGBColor((double) (((float) this.foreColor.R) / 255f), (double) (((float) this.foreColor.G) / 255f), (double) (((float) this.foreColor.B) / 255f)));
            PdfGraphicsStateParameters parameters2 = new PdfGraphicsStateParameters();
            parameters2.NonStrokingAlphaConstant = new double?(((double) this.foreColor.A) / 255.0);
            parameters2.StrokingAlphaConstant = new double?(((double) this.foreColor.A) / 255.0);
            constructor.SetGraphicsStateParameters(parameters2);
            constructor.SetLineWidth(this.LineWidth);
            constructor.SetLineCapStyle(this.LineCapStyle);
        }

        protected void MultipleTransform(PdfTransformationMatrix transform)
        {
            this.transform = PdfTransformationMatrix.Multiply(this.transform, transform);
        }

        protected void RotateTransform(float degree)
        {
            double d = (3.1415926535897931 * degree) / 180.0;
            double a = Math.Cos(d);
            double c = Math.Sin(d);
            this.transform = PdfTransformationMatrix.Multiply(this.transform, new PdfTransformationMatrix(a, -c, c, a, 0.0, 0.0));
        }

        protected virtual double LineWidth =>
            this.lineWidth;

        protected virtual double LineStep =>
            this.lineStep;

        protected virtual PdfLineCapStyle LineCapStyle =>
            PdfLineCapStyle.ProjectingSquare;
    }
}

