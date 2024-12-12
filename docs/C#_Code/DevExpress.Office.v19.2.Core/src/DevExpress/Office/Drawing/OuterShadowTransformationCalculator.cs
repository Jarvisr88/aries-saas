namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class OuterShadowTransformationCalculator
    {
        private readonly DocumentModelUnitConverter unitConverter;
        private readonly DocumentModelUnitToLayoutUnitConverter toLayoutUnitConverter;
        private readonly float scaleFactor;

        public OuterShadowTransformationCalculator(DocumentModelUnitConverter unitConverter, DocumentModelUnitToLayoutUnitConverter toLayoutUnitConverter) : this(unitConverter, toLayoutUnitConverter, 1f)
        {
        }

        public OuterShadowTransformationCalculator(DocumentModelUnitConverter unitConverter, DocumentModelUnitToLayoutUnitConverter toLayoutUnitConverter, float scaleFactor)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            Guard.ArgumentNotNull(toLayoutUnitConverter, "toLayoutUnitConverter");
            this.unitConverter = unitConverter;
            this.toLayoutUnitConverter = toLayoutUnitConverter;
            this.scaleFactor = scaleFactor;
        }

        public OuterShadowTransformationInfo Calculate(Rectangle shapeBounds, OuterShadowEffectInfo shadowInfo)
        {
            OffsetShadowInfo offsetShadow = shadowInfo.OffsetShadow;
            float num = this.toLayoutUnitConverter.ToLayoutUnits((float) this.unitConverter.EmuToModelUnitsL(offsetShadow.Distance)) * this.scaleFactor;
            double d = DrawingValueConverter.DegreeToRadian(DrawingValueConverter.FromPositiveFixedAngle(offsetShadow.Direction));
            ScalingFactorInfo scalingFactor = shadowInfo.ScalingFactor;
            float scaleX = (float) DrawingValueConverter.FromPercentage(scalingFactor.Horizontal);
            float scaleY = (float) DrawingValueConverter.FromPercentage(scalingFactor.Vertical);
            float num3 = ((float) (num * Math.Cos(d))) + (shapeBounds.X - (shapeBounds.X * scaleX));
            float num4 = ((float) (num * Math.Sin(d))) + (shapeBounds.Y - (shapeBounds.Y * scaleY));
            SkewAnglesInfo skewAngles = shadowInfo.SkewAngles;
            float skewX = (float) Math.Tan(DrawingValueConverter.DegreeToRadian(DrawingValueConverter.FromPositiveFixedAngle(skewAngles.Horizontal)));
            float skewOffsetX = shapeBounds.Height * skewX;
            float skewY = (float) Math.Tan(DrawingValueConverter.DegreeToRadian(DrawingValueConverter.FromPositiveFixedAngle(skewAngles.Vertical)));
            float skewOffsetY = shapeBounds.Width * skewY;
            if (skewX != 0f)
            {
                num3 -= (skewX * shapeBounds.Bottom) - (skewX * shapeBounds.Height);
            }
            if (skewY != 0f)
            {
                num4 -= (skewY * shapeBounds.Right) - (skewY * shapeBounds.Width);
            }
            float scaledWidth = shapeBounds.Width * scaleX;
            PointF tf = this.CalculateAlignmentOffset(shadowInfo.ShadowAlignment, (float) shapeBounds.Width, (float) shapeBounds.Height, scaledWidth, shapeBounds.Height * scaleY, skewOffsetX, skewOffsetY);
            return new OuterShadowTransformationInfo(num3 + tf.X, num4 + tf.Y, scaleX, scaleY, skewX, skewY);
        }

        private unsafe PointF CalculateAlignmentOffset(RectangleAlignType alignment, float shapeWidth, float shapeHeight, float scaledWidth, float scaledHeight, float skewOffsetX, float skewOffsetY)
        {
            PointF tf = new PointF();
            switch (alignment)
            {
                case RectangleAlignType.Top:
                {
                    PointF* tfPtr1 = &tf;
                    tfPtr1.X += this.CalculateCenterOffset(shapeWidth, scaledWidth);
                    PointF* tfPtr2 = &tf;
                    tfPtr2.Y -= skewOffsetY / 2f;
                    break;
                }
                case RectangleAlignType.TopRight:
                {
                    PointF* tfPtr3 = &tf;
                    tfPtr3.X += this.CalculateFarOffset(shapeWidth, scaledWidth);
                    PointF* tfPtr4 = &tf;
                    tfPtr4.Y -= skewOffsetY;
                    break;
                }
                case RectangleAlignType.Left:
                {
                    PointF* tfPtr5 = &tf;
                    tfPtr5.X -= skewOffsetX / 2f;
                    PointF* tfPtr6 = &tf;
                    tfPtr6.Y += this.CalculateCenterOffset(shapeHeight, scaledHeight);
                    break;
                }
                case RectangleAlignType.Center:
                {
                    PointF* tfPtr7 = &tf;
                    tfPtr7.X += this.CalculateCenterOffset(shapeWidth, scaledWidth) - (skewOffsetX / 2f);
                    PointF* tfPtr8 = &tf;
                    tfPtr8.Y += this.CalculateCenterOffset(shapeHeight, scaledHeight) - (skewOffsetY / 2f);
                    break;
                }
                case RectangleAlignType.Right:
                {
                    PointF* tfPtr9 = &tf;
                    tfPtr9.X += this.CalculateFarOffset(shapeWidth, scaledWidth) - (skewOffsetX / 2f);
                    PointF* tfPtr10 = &tf;
                    tfPtr10.Y += this.CalculateCenterOffset(shapeHeight, scaledHeight) - skewOffsetY;
                    break;
                }
                case RectangleAlignType.BottomLeft:
                {
                    PointF* tfPtr11 = &tf;
                    tfPtr11.X -= skewOffsetX;
                    PointF* tfPtr12 = &tf;
                    tfPtr12.Y += this.CalculateFarOffset(shapeHeight, scaledHeight);
                    break;
                }
                case RectangleAlignType.Bottom:
                {
                    PointF* tfPtr13 = &tf;
                    tfPtr13.X += this.CalculateCenterOffset(shapeWidth, scaledWidth) - skewOffsetX;
                    PointF* tfPtr14 = &tf;
                    tfPtr14.Y += this.CalculateFarOffset(shapeHeight, scaledHeight) - (skewOffsetY / 2f);
                    break;
                }
                case RectangleAlignType.BottomRight:
                {
                    PointF* tfPtr15 = &tf;
                    tfPtr15.X += this.CalculateFarOffset(shapeWidth, scaledWidth) - skewOffsetX;
                    PointF* tfPtr16 = &tf;
                    tfPtr16.Y += this.CalculateFarOffset(shapeHeight, scaledHeight) - skewOffsetY;
                    break;
                }
                default:
                    break;
            }
            return tf;
        }

        private float CalculateCenterOffset(float figureSize, float shadowSize) => 
            (figureSize / 2f) - (shadowSize / 2f);

        private float CalculateFarOffset(float figureSize, float shadowSize) => 
            figureSize - shadowSize;
    }
}

