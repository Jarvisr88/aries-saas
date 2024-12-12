namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPushButtonFormFieldAppearanceBuilder : PdfTextBasedFormFieldAppearanceBuilder<PdfButtonFormField>
    {
        public PdfPushButtonFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, IPdfExportFontProvider fontSearch) : base(widget, buttonFormField, fontSearch, null)
        {
        }

        private void DrawCenteredTextBox(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle, double textWidth, string text)
        {
            this.DrawTextBox(constructor, contentRectangle.Left + ((contentRectangle.Width - textWidth) / 2.0), base.CalculateCenteredLineYOffset(contentRectangle), text);
        }

        protected override void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            PdfWidgetAppearanceCharacteristics appearanceCharacteristics = base.Annotation.AppearanceCharacteristics;
            if (appearanceCharacteristics != null)
            {
                contentRectangle = PdfRectangle.Inflate(contentRectangle, base.Annotation.BorderWidth);
                if (appearanceCharacteristics.TextPosition != PdfWidgetAnnotationTextPosition.NoCaption)
                {
                    string text = appearanceCharacteristics.Caption ?? "";
                    double fontSize = base.FontSize;
                    PdfFontMetrics metrics = base.Font.Metrics;
                    double descent = metrics.GetDescent(fontSize);
                    double num3 = descent / 2.0;
                    double num4 = metrics.GetLineSpacing(fontSize) + descent;
                    double textWidth = base.GetTextWidth(text);
                    double left = contentRectangle.Left;
                    double bottom = contentRectangle.Bottom;
                    switch (appearanceCharacteristics.TextPosition)
                    {
                        case PdfWidgetAnnotationTextPosition.CaptionBelowTheIcon:
                            this.DrawTextBox(constructor, left + ((contentRectangle.Width - textWidth) / 2.0), (bottom + num3) + metrics.GetDescent(base.FontSize), text);
                            contentRectangle = new PdfRectangle(left, bottom + num4, contentRectangle.Right, contentRectangle.Top);
                            break;

                        case PdfWidgetAnnotationTextPosition.CaptionAboveTheIcon:
                            this.DrawTextBox(constructor, left + ((contentRectangle.Width - textWidth) / 2.0), (contentRectangle.Top - num3) - metrics.GetAscent(base.FontSize), text);
                            contentRectangle = new PdfRectangle(left, bottom, contentRectangle.Right, contentRectangle.Top - num4);
                            break;

                        case PdfWidgetAnnotationTextPosition.CaptionToTheRightOfTheIcon:
                            this.DrawTextBox(constructor, (left + contentRectangle.Width) - textWidth, base.CalculateCenteredLineYOffset(contentRectangle), text);
                            contentRectangle = new PdfRectangle(left, bottom, contentRectangle.Right - textWidth, contentRectangle.Top);
                            break;

                        case PdfWidgetAnnotationTextPosition.CaptionToTheLeftOfTheIcon:
                            this.DrawTextBox(constructor, left, base.CalculateCenteredLineYOffset(contentRectangle), text);
                            contentRectangle = new PdfRectangle(left + textWidth, bottom, contentRectangle.Right, contentRectangle.Top);
                            break;

                        case PdfWidgetAnnotationTextPosition.CaptionOverlaidDirectlyOnTheIcon:
                            this.DrawIcon(constructor, contentRectangle);
                            this.DrawCenteredTextBox(constructor, contentRectangle, textWidth, text);
                            return;

                        default:
                            this.DrawCenteredTextBox(constructor, contentRectangle, textWidth, text);
                            return;
                    }
                }
                this.DrawIcon(constructor, contentRectangle);
            }
        }

        private void DrawIcon(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            PdfWidgetAppearanceCharacteristics appearanceCharacteristics = base.Annotation.AppearanceCharacteristics;
            PdfForm normalIcon = appearanceCharacteristics.NormalIcon as PdfForm;
            if ((normalIcon != null) && !ReferenceEquals(normalIcon, constructor.Form))
            {
                bool flag;
                PdfIconFit fit = appearanceCharacteristics.IconFit ?? new PdfIconFit();
                PdfRectangle bBox = normalIcon.BBox;
                double num = contentRectangle.Width / bBox.Width;
                double num2 = contentRectangle.Height / bBox.Height;
                switch (fit.ScalingCircumstances)
                {
                    case PdfIconScalingCircumstances.BiggerThanAnnotationRectangle:
                        flag = (num < 1.0) || (num2 < 1.0);
                        break;

                    case PdfIconScalingCircumstances.SmallerThanAnnotationRectangle:
                        flag = (num > 1.0) || (num2 > 1.0);
                        break;

                    case PdfIconScalingCircumstances.Never:
                        flag = false;
                        break;

                    default:
                        flag = true;
                        break;
                }
                if (!flag)
                {
                    num = 1.0;
                    num2 = 1.0;
                }
                else if (fit.ScalingType == PdfIconScalingType.Proportional)
                {
                    double num5 = PdfMathUtils.Min(num, num2);
                    num = num5;
                    num2 = num5;
                }
                PdfTransformationMatrix matrix = new PdfTransformationMatrix(num, 0.0, 0.0, num2, 0.0, 0.0);
                PdfPoint point = matrix.Transform(normalIcon.Matrix.Transform(bBox.BottomLeft));
                PdfPoint point2 = matrix.Transform(new PdfPoint(bBox.Width, bBox.Height));
                constructor.DrawForm(normalIcon.ObjectNumber, new PdfTransformationMatrix(num, 0.0, 0.0, num2, (contentRectangle.Left + ((contentRectangle.Width - point2.X) * fit.HorizontalPosition)) - point.X, (contentRectangle.Bottom + ((contentRectangle.Height - point2.Y) * fit.VerticalPosition)) - point.Y));
            }
        }

        private void DrawTextBox(PdfFormCommandConstructor constructor, double xOffset, double yOffset, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                base.StartDrawTextBox(constructor, null);
                base.DrawTextBoxText(constructor, xOffset, yOffset, text);
                base.EndDrawTextBox(constructor);
            }
        }
    }
}

