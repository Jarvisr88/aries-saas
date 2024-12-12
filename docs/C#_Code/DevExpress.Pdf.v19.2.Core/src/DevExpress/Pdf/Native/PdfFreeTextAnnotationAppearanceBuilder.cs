namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class PdfFreeTextAnnotationAppearanceBuilder : PdfMarkupAnnotationAppearanceBuilder<PdfFreeTextAnnotation>
    {
        private const double defaultFontSize = 12.0;
        private readonly IPdfExportFontProvider fontSearch;

        public PdfFreeTextAnnotationAppearanceBuilder(PdfFreeTextAnnotation freeTextAnnotation, IPdfExportFontProvider fontSearch) : base(freeTextAnnotation)
        {
            this.fontSearch = fontSearch;
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            double width;
            base.RebuildAppearance(constructor);
            PdfFreeTextAnnotation annotation = base.Annotation;
            PdfSetTextFontCommand setTextFontCommand = null;
            PdfColor color = null;
            foreach (PdfCommand command2 in annotation.AppearanceCommands)
            {
                PdfSetRGBColorSpaceForNonStrokingOperationsCommand command3 = command2 as PdfSetRGBColorSpaceForNonStrokingOperationsCommand;
                if (command3 == null)
                {
                    PdfSetTextFontCommand command4 = command2 as PdfSetTextFontCommand;
                    if (command4 == null)
                    {
                        continue;
                    }
                    setTextFontCommand = command4;
                    continue;
                }
                double[] components = new double[] { command3.R, command3.G, command3.B };
                color = new PdfColor(components);
                constructor.SetColorForStrokingOperations(color);
            }
            PdfColor color1 = annotation.Color;
            PdfColor color2 = color1;
            if (color1 == null)
            {
                PdfColor local1 = color1;
                double[] components = new double[] { 1.0, 1.0, 1.0 };
                color2 = new PdfColor(components);
            }
            constructor.SetColorForNonStrokingOperations(color2);
            PdfAnnotationBorderStyle borderStyle = annotation.BorderStyle;
            if (borderStyle != null)
            {
                width = borderStyle.Width;
            }
            else
            {
                PdfAnnotationBorderStyle local2 = borderStyle;
                width = 1.0;
            }
            double lineWidth = width;
            PdfRectangle rect = annotation.Rect;
            PdfRectangle padding = annotation.Padding;
            PdfRectangle rectangle2 = (padding != null) ? new PdfRectangle(padding.Left, padding.Bottom, rect.Width - padding.Right, rect.Height - padding.Top) : new PdfRectangle(0.0, 0.0, rect.Width, rect.Height);
            double num2 = lineWidth / 2.0;
            double left = rectangle2.Left + num2;
            double bottom = rectangle2.Bottom + num2;
            rectangle2 = new PdfRectangle(left, bottom, PdfMathUtils.Max(left, rectangle2.Right - num2), PdfMathUtils.Max(bottom, rectangle2.Top - num2));
            if (lineWidth == 0.0)
            {
                constructor.AppendRectangle(rectangle2);
                constructor.FillPath(true);
            }
            else
            {
                constructor.SetLineWidth(lineWidth);
                constructor.AppendRectangle(rectangle2);
                constructor.FillAndStrokePath(true);
            }
            string contents = annotation.Contents;
            if ((this.fontSearch != null) && !string.IsNullOrEmpty(contents))
            {
                constructor.BeginMarkedContent();
                PdfColor color3 = color;
                if (color == null)
                {
                    PdfColor local3 = color;
                    color3 = new PdfColor(new double[1]);
                }
                constructor.SetColorForNonStrokingOperations(color3);
                PdfColor color4 = color;
                if (color == null)
                {
                    PdfColor local4 = color;
                    color4 = new PdfColor(new double[1]);
                }
                constructor.SetColorForStrokingOperations(color4);
                double num5 = (setTextFontCommand != null) ? setTextFontCommand.FontSize : 12.0;
                PdfExportFont matchingFont = this.fontSearch.GetMatchingFont(setTextFontCommand);
                if (matchingFont != null)
                {
                    PdfTextJustification textJustification = annotation.TextJustification;
                    PdfStringAlignment alignment = (textJustification == PdfTextJustification.Centered) ? PdfStringAlignment.Center : ((textJustification == PdfTextJustification.RightJustified) ? PdfStringAlignment.Far : PdfStringAlignment.Near);
                    PdfStringFormat genericDefault = PdfStringFormat.GenericDefault;
                    genericDefault.Alignment = alignment;
                    IList<IList<DXCluster>> lines = DXLineFormatter.FormatText(contents, new DXSizeF((float) rectangle2.Width, (float) rectangle2.Height), matchingFont.Metrics, (float) num5, genericDefault, 0f, matchingFont, DXKerningMode.None);
                    new PdfTextPainter(constructor).DrawLines(lines, new PdfExportFontInfo(matchingFont, (float) num5), rectangle2.TopLeft, genericDefault, true);
                    matchingFont.UpdateFont();
                }
                constructor.EndMarkedContent();
            }
        }
    }
}

