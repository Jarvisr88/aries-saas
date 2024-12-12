namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfShapeAnnotationAppearanceBuilder : PdfMarkupAnnotationAppearanceBuilder<PdfShapeAnnotation>
    {
        protected PdfShapeAnnotationAppearanceBuilder(PdfShapeAnnotation shapeAnnotation) : base(shapeAnnotation)
        {
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            bool flag;
            bool flag2;
            base.RebuildAppearance(constructor);
            PdfShapeAnnotation annotation = base.Annotation;
            PdfColor interiorColor = annotation.Color;
            if (interiorColor == null)
            {
                flag = false;
            }
            else
            {
                flag = true;
                constructor.SetColorForStrokingOperations(interiorColor);
            }
            interiorColor = annotation.InteriorColor;
            if (interiorColor == null)
            {
                flag2 = false;
            }
            else
            {
                flag2 = true;
                constructor.SetColorForNonStrokingOperations(interiorColor);
            }
            PdfRectangle rect = annotation.Rect;
            double left = 0.5;
            double bottom = 0.5;
            double right = rect.Width - 0.5;
            double top = rect.Height - 0.5;
            PdfRectangle padding = annotation.Padding;
            if (padding != null)
            {
                left += padding.Left;
                bottom += padding.Bottom;
                right -= padding.Right;
                top -= padding.Top;
            }
            if ((right > left) && (top > bottom))
            {
                if (flag)
                {
                    PdfAnnotationBorderStyle borderStyle = annotation.BorderStyle;
                    if (borderStyle != null)
                    {
                        double width = borderStyle.Width;
                        constructor.SetLineWidth(width);
                        if (borderStyle.StyleName == "D")
                        {
                            constructor.SetLineStyle(borderStyle.LineStyle);
                        }
                        if ((width >= rect.Width) || (width >= rect.Height))
                        {
                            double num1 = (left + right) / 2.0;
                            left = right = num1;
                            double num7 = (bottom + top) / 2.0;
                            bottom = top = num7;
                        }
                        else
                        {
                            double num6 = width / 2.0;
                            if (num6 > 0.0)
                            {
                                left += num6;
                                bottom += num6;
                                right -= num6;
                                top -= num6;
                            }
                        }
                    }
                }
                this.RebuildShapeAppearance(constructor, new PdfRectangle(left, bottom, right, top));
                if (flag)
                {
                    if (flag2)
                    {
                        constructor.FillAndStrokePath(true);
                    }
                    else
                    {
                        constructor.StrokePath();
                    }
                }
                else if (flag2)
                {
                    constructor.FillPath(true);
                }
            }
        }

        protected abstract void RebuildShapeAppearance(PdfFormCommandConstructor constructor, PdfRectangle rect);
    }
}

