namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAcroFormRadioButton : PdfAcroFormVisualField
    {
        private readonly PdfButtonFormField radioGroupField;
        private readonly bool isChecked;
        private readonly PdfRectangle rectangle;

        public PdfAcroFormRadioButton(PdfButtonFormField radioGroupField, bool isChecked, PdfRectangle rectangle, string name, int pageNumber) : base(name, pageNumber)
        {
            this.radioGroupField = radioGroupField;
            this.isChecked = isChecked;
            this.rectangle = rectangle;
        }

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent)
        {
            PdfWidgetAnnotation radioButtonWidget = new PdfWidgetAnnotation(document.Pages[base.PageNumber - 1], this.CreateWidgetBuilder(this.rectangle).SetButtonStyle(PdfAcroFormButtonStyle.Check)) {
                AppearanceName = "Off"
            };
            PdfButtonFormField field = new PdfButtonFormField(document, this.radioGroupField, new PdfRadioGroupFieldAppearance(true, PdfAcroFormButtonStyle.Check, base.Appearance), base.Name, radioButtonWidget);
            if (this.isChecked)
            {
                this.radioGroupField.SetValue(base.Name, null);
            }
            return field;
        }
    }
}

