namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSignatureFormField : PdfInteractiveFormField
    {
        internal const string Type = "Sig";
        private const string lockDictionaryKey = "Lock";
        private readonly PdfSignature signature;
        private readonly PdfSignatureFormFieldLock formFieldLock;
        private readonly bool shouldWriteSignature;

        internal PdfSignatureFormField(PdfInteractiveForm form, PdfWidgetAnnotation widget, PdfSignature signature) : base(form, widget)
        {
            this.signature = signature;
            string str = Guid.NewGuid().ToString();
            if (form != null)
            {
                IList<PdfInteractiveFormField> fields = form.Fields;
                while (true)
                {
                    bool flag = false;
                    foreach (PdfInteractiveFormField field in fields)
                    {
                        if (str.Equals(field.Name))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                    str = Guid.NewGuid().ToString();
                }
            }
            base.Name = str;
            form.SignatureFlags = PdfSignatureFlags.AppendOnly | PdfSignatureFlags.SignaturesExist;
            this.shouldWriteSignature = true;
        }

        internal PdfSignatureFormField(PdfInteractiveForm form, PdfInteractiveFormField parent, PdfReaderDictionary dictionary, PdfObjectReference valueReference) : base(form, parent, dictionary, valueReference)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("V", "Contents", true);
            if (dictionary2 != null)
            {
                try
                {
                    this.signature = new PdfSignature(dictionary2);
                }
                catch
                {
                }
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("Lock");
            if (dictionary3 != null)
            {
                this.formFieldLock = new PdfSignatureFormFieldLock(dictionary3);
            }
        }

        internal PdfSignatureFormField(PdfInteractiveFormField parent, IPdfExportFontProvider fontSearch, PdfDocument document, PdfAcroFormSignatureField signature, IPdfAnnotationAppearanceBuilder builder) : base(parent, fontSearch, document, signature)
        {
            PdfWidgetAnnotation widget = base.Widget;
            (builder ?? new PdfSignatureFormFieldAppearanceBuilder(signature, widget, this, fontSearch)).RebuildAppearance(widget.CreateAppearanceForm(PdfAnnotationAppearanceState.Normal));
        }

        protected internal override void FillDictionary(PdfWriterDictionary dictionary)
        {
            base.FillDictionary(dictionary);
            if (this.shouldWriteSignature && (this.signature != null))
            {
                dictionary.Add("V", this.signature);
            }
            dictionary.Add("Lock", this.formFieldLock);
        }

        public PdfSignature Signature =>
            this.signature;

        public PdfSignatureFormFieldLock Lock =>
            this.formFieldLock;

        protected override string FieldType =>
            "Sig";

        protected internal override bool ShouldHighlight =>
            false;
    }
}

