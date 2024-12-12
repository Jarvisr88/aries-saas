namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRichMediaSettings : PdfObject
    {
        private const string richMediaSettingsDictionaryName = "RichMediaSettings";
        private const string activationKey = "Activation";
        private const string deactivationKey = "Deactivation";
        private readonly PdfRichMediaActivation activation;
        private readonly PdfRichMediaDeactivation deactivation;

        internal PdfRichMediaSettings(PdfRichMediaAnnotation annotation, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            if (dictionary != null)
            {
                PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Activation");
                if (dictionary2 != null)
                {
                    this.activation = new PdfRichMediaActivation(annotation, dictionary2);
                }
                PdfReaderDictionary dictionary3 = dictionary.GetDictionary("Deactivation");
                if (dictionary3 != null)
                {
                    this.deactivation = new PdfRichMediaDeactivation(dictionary3);
                }
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("RichMediaSettings"));
            dictionary.Add("Activation", this.activation);
            dictionary.Add("Deactivation", this.deactivation);
            return dictionary;
        }

        public PdfRichMediaActivation Activation =>
            this.activation;

        public PdfRichMediaDeactivation Deactivation =>
            this.deactivation;
    }
}

