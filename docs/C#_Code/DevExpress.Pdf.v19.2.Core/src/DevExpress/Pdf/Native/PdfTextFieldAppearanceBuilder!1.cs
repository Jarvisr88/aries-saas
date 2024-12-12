namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public abstract class PdfTextFieldAppearanceBuilder<T> : PdfTextBasedFormFieldAppearanceBuilder<T> where T: PdfInteractiveFormField
    {
        protected PdfTextFieldAppearanceBuilder(PdfWidgetAnnotation widget, T formField, IPdfExportFontProvider fontSearch, PdfRgbaColor backgroundColor) : base(widget, formField, fontSearch, backgroundColor)
        {
        }

        protected virtual PdfStringFormat CreateStringFormat()
        {
            PdfStringFormat format = new PdfStringFormat(PdfStringFormatFlags.NoClip) {
                LeadingMarginFactor = 0.0,
                TrailingMarginFactor = 0.0,
                Trimming = PdfStringTrimming.None
            };
            switch (base.FormField.TextJustification)
            {
                case PdfTextJustification.LeftJustified:
                    format.Alignment = PdfStringAlignment.Near;
                    break;

                case PdfTextJustification.Centered:
                    format.Alignment = PdfStringAlignment.Center;
                    break;

                case PdfTextJustification.RightJustified:
                    format.Alignment = PdfStringAlignment.Far;
                    break;

                default:
                    break;
            }
            if (!this.Multiline)
            {
                format.FormatFlags |= PdfStringFormatFlags.NoWrap;
                format.LineAlignment = PdfStringAlignment.Center;
            }
            return format;
        }

        protected void DrawTextField(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle, string text)
        {
            Tuple<PdfFontMetrics, PdfRectangle> actualContentBounds = this.GetActualContentBounds(contentRectangle, (float) base.FontSize);
            PdfRectangle layoutRect = actualContentBounds.Item2;
            PdfFontMetrics metrics = actualContentBounds.Item1;
            PdfExportFont shaper = base.Font;
            PdfInteractiveFormFieldTextState textState = base.FormField.TextState;
            if (textState != null)
            {
                textState.FillCommands(constructor);
            }
            PdfStringFormat stringFormat = this.CreateStringFormat();
            DXSizeF ef = new DXSizeF((float) layoutRect.Width, (float) layoutRect.Height);
            IList<IList<DXCluster>> lines = DXLineFormatter.FormatText(text, new DXSizeF?(ef), metrics, (float) base.FontSize, stringFormat, 0f, new PdfWidgetShaper(shaper, textState), DXKerningMode.None);
            if (!this.Multiline && (lines.Count > 1))
            {
                List<IList<DXCluster>> list1 = new List<IList<DXCluster>>();
                list1.Add(lines[0]);
                lines = list1;
            }
            new PdfTextPainter(constructor).DrawLines(lines, base.FontInfo, layoutRect, metrics, stringFormat, false);
            shaper.UpdateFont();
        }

        public Tuple<PdfFontMetrics, PdfRectangle> GetActualContentBounds(float fontSize) => 
            this.GetActualContentBounds(base.Annotation.GetAppearanceContentRectangle(), fontSize);

        private Tuple<PdfFontMetrics, PdfRectangle> GetActualContentBounds(PdfRectangle contentRectangle, float fontSize)
        {
            PdfRectangle rectangle;
            PdfExportFont font = base.Font;
            PdfFontMetrics metrics = PdfTextFormField.CreateMultilineFormFieldMetrics(font, base.FormField.TextState);
            PdfFontMetrics metrics2 = font.Metrics;
            if (((base.Annotation.InteractiveFormField.Flags & PdfInteractiveFormFieldFlags.Multiline) == PdfInteractiveFormFieldFlags.None) || (metrics.GetLineSpacing((double) fontSize) >= contentRectangle.Height))
            {
                rectangle = contentRectangle;
            }
            else
            {
                metrics2 = metrics;
                rectangle = new PdfRectangle(contentRectangle.Left, contentRectangle.Bottom, contentRectangle.Right, (contentRectangle.Top - metrics2.GetLineSpacing((double) fontSize)) + metrics2.GetAscent((double) fontSize));
            }
            return new Tuple<PdfFontMetrics, PdfRectangle>(metrics2, rectangle);
        }

        protected abstract bool Multiline { get; }
    }
}

