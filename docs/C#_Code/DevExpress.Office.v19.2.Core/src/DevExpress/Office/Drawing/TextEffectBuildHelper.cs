namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal class TextEffectBuildHelper : ShapeEffectBuildHelper
    {
        private readonly ParagraphLayoutInfoConverter paragraphLayoutInfoConverter;

        public TextEffectBuildHelper(ShapeLayoutInfo shapeLayoutInfo, ParagraphLayoutInfoConverter paragraphLayoutInfoConverter) : base(shapeLayoutInfo)
        {
            this.paragraphLayoutInfoConverter = paragraphLayoutInfoConverter;
        }

        public override Rectangle CalculateReflectionTransformBounds(bool rotateWithShape)
        {
            RectangleF currentRunBounds = this.paragraphLayoutInfoConverter.GetCurrentRunBounds();
            float currentBaseLine = this.paragraphLayoutInfoConverter.GetCurrentBaseLine();
            return new Rectangle((int) currentRunBounds.Left, (int) currentRunBounds.Top, (int) currentRunBounds.Width, (int) currentBaseLine);
        }

        public override Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize) => 
            Rectangle.Round(this.paragraphLayoutInfoConverter.GetCurrentRunBounds());

        public override Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform)
        {
            GraphicsPathCollectFlags excludeInnerShadow = GraphicsPathCollectFlags.ExcludeInnerShadow;
            using (GraphicsPath path = base.GetTransformedFigure(excludeInnerShadow, false, additionalSize))
            {
                return path.GetBoundsExt();
            }
        }
    }
}

