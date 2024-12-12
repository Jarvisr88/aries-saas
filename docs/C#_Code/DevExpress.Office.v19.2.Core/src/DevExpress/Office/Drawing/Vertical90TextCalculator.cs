namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    internal class Vertical90TextCalculator : HorizontalTextCalculator
    {
        public Vertical90TextCalculator(TextRendererContext textRendererContext) : base(textRendererContext)
        {
        }

        public override void ApplyGraphicsTransformation(Graphics graphics)
        {
            graphics.TranslateTransform(base.TextRectangle.Height, 0f);
            graphics.RotateTransform(90f);
            base.ApplyGraphicsTransformationCore(graphics);
        }

        protected override void CalculateOnlyTextRectangle()
        {
            DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter = base.DocumentModel.ToDocumentLayoutUnitConverter;
            float left = 0f;
            float top = 0f;
            float right = 0f;
            float bottom = 0f;
            DrawingTextInset inset = base.BodyProperties.Inset;
            if (inset != null)
            {
                top = toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Left);
                left = toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Top);
                bottom = base.TextRectangle.Height - toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Right);
                right = base.TextRectangle.Width - toDocumentLayoutUnitConverter.ToLayoutUnits(inset.Bottom);
            }
            if (left > right)
            {
                left = right;
                right = left;
            }
            if (top > bottom)
            {
                top = bottom;
                bottom = top;
            }
            base.TextOnlyRectangle = RectangleF.FromLTRB(left, top, right, bottom);
        }

        public override Size GetResultBitmapSize() => 
            new Size((int) base.TextRectangle.Height, (int) base.TextRectangle.Width);

        protected override void NormalizeTextRectangle()
        {
            RectangleF textRectangle = base.TextRectangle;
            float num = textRectangle.Width / 2f;
            float num2 = textRectangle.Height / 2f;
            float x = (textRectangle.Left + num) - num2;
            base.TextRectangle = new RectangleF(x, (textRectangle.Top + num2) - num, textRectangle.Height, textRectangle.Width);
        }
    }
}

