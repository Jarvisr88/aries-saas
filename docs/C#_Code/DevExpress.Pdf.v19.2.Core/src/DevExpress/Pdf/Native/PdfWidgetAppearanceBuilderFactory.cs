namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfWidgetAppearanceBuilderFactory : PdfVisitorBasedFactory<PdfInteractiveFormField, IPdfAnnotationAppearanceBuilder>, IPdfInteractiveFormFieldVisitor
    {
        private readonly IPdfExportFontProvider fontSearch;

        public PdfWidgetAppearanceBuilderFactory(IPdfExportFontProvider fontSearch)
        {
            this.fontSearch = fontSearch;
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfButtonFormField formField)
        {
            if (formField.Flags.HasFlag(PdfInteractiveFormFieldFlags.PushButton))
            {
                base.SetResult(new PdfPushButtonFormFieldAppearanceBuilder(formField.Widget, formField, this.fontSearch));
            }
            else
            {
                base.SetResult(null);
            }
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfChoiceFormField formField)
        {
            base.SetResult(new PdfChoiceFormFieldAppearanceBuilder(formField.Widget, formField, this.fontSearch, null));
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfInteractiveFormField formField)
        {
            base.SetResult(null);
        }

        void IPdfInteractiveFormFieldVisitor.Visit(PdfTextFormField formField)
        {
            base.SetResult(new PdfTextFormFieldAppearanceBuilder(formField.Widget, formField, this.fontSearch, null));
        }

        protected override void Visit(PdfInteractiveFormField input)
        {
            input.Accept(this);
        }
    }
}

