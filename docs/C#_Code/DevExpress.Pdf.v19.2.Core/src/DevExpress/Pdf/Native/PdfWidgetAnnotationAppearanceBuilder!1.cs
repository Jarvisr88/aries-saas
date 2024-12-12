namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfWidgetAnnotationAppearanceBuilder<T> : PdfAnnotationAppearanceBuilder<PdfWidgetAnnotation> where T: PdfInteractiveFormField
    {
        private readonly double borderWidth;
        private readonly T formField;
        private readonly PdfRgbaColor backgroundColor;

        protected PdfWidgetAnnotationAppearanceBuilder(PdfWidgetAnnotation widget, T formField, PdfRgbaColor backgroundColor) : base(widget)
        {
            this.formField = formField;
            this.backgroundColor = backgroundColor;
            PdfAnnotationBorderStyle borderStyle = widget.BorderStyle;
            if (borderStyle != null)
            {
                this.borderWidth = borderStyle.Width;
            }
            else
            {
                PdfAnnotationBorder border = widget.Border;
                if (border != null)
                {
                    this.borderWidth = border.LineWidth;
                }
            }
        }

        protected override PdfTransformationMatrix CreateFormMatrix()
        {
            if (base.Annotation.AppearanceCharacteristics != null)
            {
                int rotationAngle = base.Annotation.AppearanceCharacteristics.RotationAngle;
                PdfRectangle rect = base.Annotation.Rect;
                if (rotationAngle != 0)
                {
                    if (rotationAngle == 90)
                    {
                        return new PdfTransformationMatrix(0.0, 1.0, -1.0, 0.0, rect.Width, 0.0);
                    }
                    if (rotationAngle == 180)
                    {
                        return new PdfTransformationMatrix(-1.0, 0.0, 0.0, -1.0, rect.Width, rect.Height);
                    }
                    if (rotationAngle == 270)
                    {
                        return new PdfTransformationMatrix(0.0, -1.0, 1.0, 0.0, 0.0, rect.Height);
                    }
                }
            }
            return base.CreateFormMatrix();
        }

        protected abstract void DrawBeveledBorder(PdfFormCommandConstructor constructor);
        protected void DrawBorder(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            PdfColor borderColor = null;
            PdfWidgetAnnotation annotation = base.Annotation;
            PdfWidgetAppearanceCharacteristics appearanceCharacteristics = annotation.AppearanceCharacteristics;
            if (appearanceCharacteristics != null)
            {
                borderColor = appearanceCharacteristics.BorderColor;
                if (borderColor != null)
                {
                    constructor.SetColorForStrokingOperations(borderColor);
                    PdfAnnotationBorderStyle borderStyle = annotation.BorderStyle;
                    if (borderStyle == null)
                    {
                        PdfAnnotationBorder border = annotation.Border;
                        if (border != null)
                        {
                            double lineWidth = border.LineWidth;
                            constructor.SetLineWidth(lineWidth);
                            PdfLineStyle lineStyle = border.LineStyle;
                            if (lineStyle != null)
                            {
                                constructor.SetLineStyle(lineStyle);
                            }
                            this.DrawSolidBorder(constructor, contentRectangle);
                        }
                    }
                    else
                    {
                        double width = borderStyle.Width;
                        constructor.SetLineWidth(width);
                        string styleName = borderStyle.StyleName;
                        if (styleName == "D")
                        {
                            constructor.SetLineStyle(borderStyle.LineStyle);
                            this.DrawSolidBorder(constructor, contentRectangle);
                        }
                        else if (styleName == "B")
                        {
                            this.DrawSolidBorder(constructor, contentRectangle);
                            this.DrawBeveledBorder(constructor);
                        }
                        else if (styleName == "I")
                        {
                            this.DrawSolidBorder(constructor, contentRectangle);
                            this.DrawInsetBorder(constructor);
                        }
                        else if (styleName == "U")
                        {
                            this.DrawUnderlineBorder(constructor);
                        }
                        else
                        {
                            this.DrawSolidBorder(constructor, contentRectangle);
                        }
                    }
                }
            }
        }

        protected abstract void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle);
        protected abstract void DrawInsetBorder(PdfFormCommandConstructor constructor);
        protected void DrawRectangularBeveledBorder(PdfFormCommandConstructor constructor)
        {
            double[] components = new double[] { 1.0 };
            constructor.SetColorForNonStrokingOperations(new PdfColor(components));
            this.DrawRectangularBorderUpperLeftStroke(constructor);
            double[] numArray2 = new double[] { 0.5 };
            constructor.SetColorForNonStrokingOperations(new PdfColor(numArray2));
            this.DrawRectangularBorderBottomRightStroke(constructor);
        }

        private void DrawRectangularBorderBottomRightStroke(PdfFormCommandConstructor constructor)
        {
            PdfRectangle boundingBox = constructor.BoundingBox;
            double height = boundingBox.Height;
            double x = boundingBox.Width - this.borderWidth;
            double num3 = x - this.borderWidth;
            double y = 2.0 * this.borderWidth;
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(this.borderWidth, this.borderWidth), new PdfPoint(x, this.borderWidth), new PdfPoint(x, height - this.borderWidth), new PdfPoint(num3, height - y), new PdfPoint(num3, y), new PdfPoint(y, y), new PdfPoint(this.borderWidth, this.borderWidth) };
            constructor.FillPolygon(points, true);
        }

        protected void DrawRectangularBorderStroke(PdfFormCommandConstructor constructor)
        {
            PdfRectangle boundingBox = constructor.BoundingBox;
            double num = this.borderWidth / 2.0;
            double num2 = boundingBox.Width - num;
            double num3 = boundingBox.Height - num;
            constructor.AppendRectangle(new PdfRectangle(PdfMathUtils.Min(num, num2), PdfMathUtils.Min(num, num3), PdfMathUtils.Max(num, num2), PdfMathUtils.Max(num, num3)));
            constructor.CloseAndStrokePath();
        }

        private void DrawRectangularBorderUpperLeftStroke(PdfFormCommandConstructor constructor)
        {
            PdfRectangle boundingBox = constructor.BoundingBox;
            double width = boundingBox.Width;
            double y = boundingBox.Height - this.borderWidth;
            double num3 = y - this.borderWidth;
            double x = 2.0 * this.borderWidth;
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(this.borderWidth, this.borderWidth), new PdfPoint(this.borderWidth, y), new PdfPoint(width - this.borderWidth, y), new PdfPoint(width - x, num3), new PdfPoint(x, num3), new PdfPoint(x, x), new PdfPoint(this.borderWidth, this.borderWidth) };
            constructor.FillPolygon(points, true);
        }

        protected void DrawRectangularInsetBorder(PdfFormCommandConstructor constructor)
        {
            double[] components = new double[] { 0.5 };
            constructor.SetColorForNonStrokingOperations(new PdfColor(components));
            this.DrawRectangularBorderUpperLeftStroke(constructor);
            double[] numArray2 = new double[] { 0.75 };
            constructor.SetColorForNonStrokingOperations(new PdfColor(numArray2));
            this.DrawRectangularBorderBottomRightStroke(constructor);
        }

        protected void DrawRectangularUnderlineBorder(PdfFormCommandConstructor constructor)
        {
            double y = this.BorderWidth / 2.0;
            PdfPoint[] points = new PdfPoint[] { new PdfPoint(0.0, y), new PdfPoint(constructor.BoundingBox.Width, y) };
            constructor.DrawLines(points);
        }

        protected abstract void DrawSolidBorder(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle);
        protected void DrawTextCombs(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle, int maxLen)
        {
            if (maxLen != 0)
            {
                double num = this.borderWidth / 2.0;
                double num2 = contentRectangle.Width / ((double) maxLen);
                double top = contentRectangle.Top;
                double bottom = contentRectangle.Bottom;
                for (int i = 1; i < maxLen; i++)
                {
                    double x = (i * num2) + num;
                    constructor.DrawLine(new PdfPoint(x, top), new PdfPoint(x, bottom));
                }
            }
        }

        protected abstract void DrawUnderlineBorder(PdfFormCommandConstructor constructor);
        protected abstract void FillBackground(PdfFormCommandConstructor constructor);
        protected void FillBackgroundEllipse(PdfFormCommandConstructor constructor, PdfRectangle rect)
        {
            PdfRgbaColor backgroundColor = this.BackgroundColor;
            if (backgroundColor != null)
            {
                this.SetNonStrokingColor(backgroundColor, constructor);
                constructor.FillEllipse(rect);
            }
        }

        protected void FillBackgroundRectangle(PdfFormCommandConstructor constructor, PdfRectangle rect)
        {
            PdfRgbaColor backgroundColor = this.BackgroundColor;
            if (backgroundColor != null)
            {
                this.SetNonStrokingColor(backgroundColor, constructor);
                constructor.FillRectangle(rect);
            }
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            PdfRectangle appearanceContentRectangle = base.Annotation.GetAppearanceContentRectangle();
            constructor.SaveGraphicsState();
            this.FillBackground(constructor);
            this.DrawBorder(constructor, appearanceContentRectangle);
            constructor.RestoreGraphicsState();
            constructor.BeginMarkedContent();
            constructor.SaveGraphicsState();
            constructor.IntersectClip(appearanceContentRectangle);
            this.DrawContent(constructor, appearanceContentRectangle);
            constructor.RestoreGraphicsState();
            constructor.EndMarkedContent();
        }

        private void SetNonStrokingColor(PdfRgbaColor color, PdfFormCommandConstructor constructor)
        {
            if (color.A != 1.0)
            {
                PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters();
                parameters.NonStrokingAlphaConstant = new double?(color.A);
                constructor.SetGraphicsStateParameters(parameters);
            }
            constructor.SetColorForNonStrokingOperations(color.ToPdfColor());
        }

        protected T FormField =>
            this.formField;

        protected double BorderWidth =>
            this.borderWidth;

        protected virtual PdfRgbaColor BackgroundColor =>
            (this.backgroundColor != null) ? this.backgroundColor : PdfRgbaColor.Create(base.Annotation.BackgroundColor);
    }
}

