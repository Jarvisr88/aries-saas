namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfButtonFormFieldAppearanceBuilder : PdfWidgetAnnotationAppearanceBuilder<PdfButtonFormField>
    {
        private readonly PdfAcroFormButtonStyle buttonStyle;
        private readonly PdfColor foreColor;
        private readonly bool isChecked;
        private readonly bool useCircularAppearance;

        public PdfButtonFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, PdfRadioGroupFieldAppearance field, bool isChecked) : this(widget, buttonFormField, field.ButtonStyle, ConvertColor(field.Appearance?.ForeColor), isChecked, true, null)
        {
        }

        public PdfButtonFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, PdfAcroFormCheckBoxField field, bool isChecked) : this(widget, buttonFormField, field.ButtonStyle, ConvertColor(field.Appearance?.ForeColor), isChecked, false, null)
        {
        }

        public PdfButtonFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, PdfButtonFormField buttonFormField, PdfAcroFormButtonStyle buttonStyle, PdfColor foreColor, bool isChecked, bool isRadioButton, PdfRgbaColor backgroundColor) : base(widget, buttonFormField, backgroundColor)
        {
            this.buttonStyle = buttonStyle;
            this.foreColor = foreColor;
            this.isChecked = isChecked;
            this.useCircularAppearance = isRadioButton && (buttonStyle == PdfAcroFormButtonStyle.Circle);
        }

        private void AppendEllipticStroke(PdfFormCommandConstructor constructor, PdfPoint startPoint)
        {
            constructor.SaveGraphicsState();
            PdfRectangle contentSquare = constructor.ContentSquare;
            PdfGraphicsPath path = new PdfGraphicsPath(startPoint);
            path.AppendLineSegment(contentSquare.TopRight);
            path.AppendLineSegment(contentSquare.BottomLeft);
            constructor.IntersectClip(path);
            constructor.DrawEllipse(PdfRectangle.Inflate(contentSquare, (3.0 * base.BorderWidth) / 2.0));
            constructor.RestoreGraphicsState();
        }

        private static PdfColor ConvertColor(PdfRGBColor color) => 
            (color != null) ? new PdfColor(color) : null;

        protected override void DrawBeveledBorder(PdfFormCommandConstructor constructor)
        {
            if (!this.useCircularAppearance)
            {
                base.DrawRectangularBeveledBorder(constructor);
            }
            else
            {
                PdfRectangle contentSquare = constructor.ContentSquare;
                constructor.SaveGraphicsState();
                constructor.SetLineWidth(base.BorderWidth);
                double[] components = new double[] { 1.0 };
                constructor.SetColorForStrokingOperations(new PdfColor(components));
                this.AppendEllipticStroke(constructor, contentSquare.TopLeft);
                double[] numArray2 = new double[] { 0.5 };
                constructor.SetColorForStrokingOperations(new PdfColor(numArray2));
                this.AppendEllipticStroke(constructor, contentSquare.BottomRight);
                constructor.RestoreGraphicsState();
            }
        }

        protected override void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            if (this.isChecked)
            {
                constructor.SaveGraphicsState();
                double num = PdfMathUtils.Min(contentRectangle.Width, contentRectangle.Height) / 2.0;
                double x = (contentRectangle.Left + contentRectangle.Right) / 2.0;
                double y = (contentRectangle.Bottom + contentRectangle.Top) / 2.0;
                PdfRectangle rectangle = new PdfRectangle(x - num, y - num, x + num, y + num);
                num *= 0.75;
                PdfRectangle rect = new PdfRectangle(x - num, y - num, x + num, y + num);
                switch (this.buttonStyle)
                {
                    case PdfAcroFormButtonStyle.Circle:
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForNonStrokingOperations(this.foreColor);
                        }
                        constructor.FillEllipse(rect);
                        break;

                    case PdfAcroFormButtonStyle.Check:
                    {
                        double num4 = rectangle.Width / 4.0;
                        double lineWidth = num4 / 2.0;
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForStrokingOperations(this.foreColor);
                        }
                        constructor.SetLineWidth(lineWidth);
                        PdfPoint[] points = new PdfPoint[] { new PdfPoint(rectangle.Left + lineWidth, y), new PdfPoint(x - lineWidth, y - num4), new PdfPoint(rectangle.Right - lineWidth, y + num4) };
                        constructor.DrawLines(points);
                        break;
                    }
                    case PdfAcroFormButtonStyle.Star:
                    {
                        PdfPoint point = new PdfPoint(x, rect.Top);
                        double num6 = rect.Top - (num / 1.3763819204711734);
                        PdfPoint point2 = new PdfPoint(rect.Left, num6);
                        PdfPoint point3 = new PdfPoint(rect.Right, num6);
                        double num7 = (num6 - rect.Bottom) / 3.0776835371752527;
                        PdfPoint point4 = new PdfPoint(rect.Left + num7, rect.Bottom);
                        PdfPoint point5 = new PdfPoint(rect.Right - num7, rect.Bottom);
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForNonStrokingOperations(this.foreColor);
                        }
                        PdfPoint[] points = new PdfPoint[] { point4, point, point5, point2, point3 };
                        constructor.FillPolygon(points, true);
                        break;
                    }
                    case PdfAcroFormButtonStyle.Cross:
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForStrokingOperations(this.foreColor);
                        }
                        constructor.DrawLine(rectangle.BottomLeft, rectangle.TopRight);
                        constructor.DrawLine(rectangle.TopLeft, rectangle.BottomRight);
                        break;

                    case PdfAcroFormButtonStyle.Diamond:
                    {
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForNonStrokingOperations(this.foreColor);
                        }
                        PdfPoint[] points = new PdfPoint[] { new PdfPoint(x, rect.Top), new PdfPoint(rect.Right, y), new PdfPoint(x, rect.Bottom), new PdfPoint(rect.Left, y) };
                        constructor.FillPolygon(points, true);
                        break;
                    }
                    case PdfAcroFormButtonStyle.Square:
                        if (this.foreColor != null)
                        {
                            constructor.SetColorForNonStrokingOperations(this.foreColor);
                        }
                        constructor.FillRectangle(rect);
                        break;

                    default:
                        break;
                }
                constructor.RestoreGraphicsState();
            }
        }

        protected override void DrawInsetBorder(PdfFormCommandConstructor constructor)
        {
            if (!this.useCircularAppearance)
            {
                base.DrawRectangularInsetBorder(constructor);
            }
            else
            {
                PdfRectangle contentSquare = constructor.ContentSquare;
                constructor.SaveGraphicsState();
                constructor.SetLineWidth(base.BorderWidth);
                double[] components = new double[] { 0.5 };
                constructor.SetColorForStrokingOperations(new PdfColor(components));
                this.AppendEllipticStroke(constructor, contentSquare.TopLeft);
                double[] numArray2 = new double[] { 0.75 };
                constructor.SetColorForStrokingOperations(new PdfColor(numArray2));
                this.AppendEllipticStroke(constructor, contentSquare.BottomRight);
                constructor.RestoreGraphicsState();
            }
        }

        protected override void DrawSolidBorder(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            if (this.useCircularAppearance)
            {
                constructor.DrawEllipse(PdfRectangle.Inflate(constructor.ContentSquare, base.BorderWidth / 2.0));
            }
            else
            {
                base.DrawRectangularBorderStroke(constructor);
            }
        }

        protected override void DrawUnderlineBorder(PdfFormCommandConstructor constructor)
        {
            if (!this.useCircularAppearance)
            {
                base.DrawRectangularUnderlineBorder(constructor);
            }
        }

        protected override void FillBackground(PdfFormCommandConstructor constructor)
        {
            if (this.useCircularAppearance)
            {
                base.FillBackgroundEllipse(constructor, constructor.ContentSquare);
            }
            else
            {
                base.FillBackgroundRectangle(constructor, constructor.BoundingBox);
            }
        }
    }
}

