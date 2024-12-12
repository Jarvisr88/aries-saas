namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;

    public abstract class PdfTextBasedFormFieldAppearanceBuilder<T> : PdfWidgetAnnotationAppearanceBuilder<T> where T: PdfInteractiveFormField
    {
        private readonly PdfExportFontInfo fontInfo;

        protected PdfTextBasedFormFieldAppearanceBuilder(PdfWidgetAnnotation widget, T formField, IPdfExportFontProvider fontSearch, PdfRgbaColor backgroundColor) : base(widget, formField, backgroundColor)
        {
            this.fontInfo = formField.GetFontInfo(fontSearch);
        }

        protected double CalculateCenteredLineYOffset(PdfRectangle clipRect)
        {
            PdfFontMetrics metrics = this.Font.Metrics;
            return ((clipRect.Bottom + ((clipRect.Height - metrics.GetLineSpacing(this.FontSize)) / 2.0)) + metrics.GetDescent(this.FontSize));
        }

        protected override void DrawBeveledBorder(PdfFormCommandConstructor constructor)
        {
            base.DrawRectangularBeveledBorder(constructor);
        }

        protected override void DrawInsetBorder(PdfFormCommandConstructor constructor)
        {
            base.DrawRectangularInsetBorder(constructor);
        }

        protected override void DrawSolidBorder(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            base.DrawRectangularBorderStroke(constructor);
        }

        protected void DrawTextBoxText(PdfFormCommandConstructor constructor, double xOffset, double yOffset, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                PdfStringFormat genericTypographic = PdfStringFormat.GenericTypographic;
                genericTypographic.FormatFlags = 0;
                DXSizeF? layoutSize = null;
                PdfTextBuilder builder = new PdfTextBuilder(constructor, this.fontInfo);
                builder.StartNextLine(xOffset, yOffset);
                builder.Append(DXLineFormatter.FormatText(text, layoutSize, this.Font.Metrics, (float) this.FontSize, genericTypographic, 0f, new PdfWidgetShaper(this.Font, base.FormField.TextState), 0)[0]);
                this.fontInfo.Font.AddGlyphs(builder.Subset);
                this.Font.UpdateFont();
            }
        }

        protected override void DrawUnderlineBorder(PdfFormCommandConstructor constructor)
        {
            base.DrawRectangularUnderlineBorder(constructor);
        }

        protected void EndDrawTextBox(PdfFormCommandConstructor constructor)
        {
            constructor.EndText();
        }

        protected override void FillBackground(PdfFormCommandConstructor constructor)
        {
            base.FillBackgroundRectangle(constructor, constructor.BoundingBox);
        }

        protected double GetTextWidth(string text) => 
            TextWidthHelper.GetTextWidth(text, this.Font, (float) this.FontSize, base.FormField.TextState);

        protected void StartDrawTextBox(PdfFormCommandConstructor constructor, PdfColor foreColor)
        {
            constructor.BeginText();
            if (foreColor == null)
            {
                base.FormField.TextState.FillCommands(constructor);
            }
            else
            {
                constructor.SetColorForNonStrokingOperations(foreColor);
            }
            constructor.SetTextFont(this.Font.Font, this.FontSize);
        }

        protected double FontSize =>
            (double) this.fontInfo.FontSize;

        protected PdfExportFont Font =>
            this.fontInfo.Font;

        protected PdfExportFontInfo FontInfo =>
            this.fontInfo;
    }
}

