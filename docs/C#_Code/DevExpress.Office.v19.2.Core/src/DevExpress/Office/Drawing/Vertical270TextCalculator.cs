namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;

    internal class Vertical270TextCalculator : Vertical90TextCalculator
    {
        public Vertical270TextCalculator(TextRendererContext textRendererContext) : base(textRendererContext)
        {
        }

        public override void ApplyGraphicsTransformation(Graphics graphics)
        {
            graphics.TranslateTransform(0f, base.TextRectangle.Width);
            graphics.RotateTransform(-90f);
            base.ApplyGraphicsTransformationCore(graphics);
        }
    }
}

