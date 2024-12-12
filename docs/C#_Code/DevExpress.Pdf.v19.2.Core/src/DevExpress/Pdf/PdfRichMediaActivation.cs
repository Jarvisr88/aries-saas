namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRichMediaActivation : PdfObject
    {
        private const string richMediaActivationDictionaryName = "RichMediaActivation";
        private const string conditionKey = "Condition";
        private const string configurationKey = "Configuration";
        private readonly PdfRichMediaActivationCondition condition;
        private readonly PdfRichMediaConfiguration configuration;

        internal PdfRichMediaActivation(PdfRichMediaAnnotation annotation, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.condition = PdfEnumToStringConverter.Parse<PdfRichMediaActivationCondition>(dictionary.GetName("Condition"), true);
            PdfObjectReference objectReference = dictionary.GetObjectReference("Configuration");
            if (objectReference != null)
            {
                int number = objectReference.Number;
                foreach (PdfRichMediaConfiguration configuration in annotation.Configurations)
                {
                    if (configuration.Number == number)
                    {
                        this.configuration = configuration;
                        break;
                    }
                }
                if (this.configuration == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("RichMediaActivation"));
            dictionary.AddEnumName<PdfRichMediaActivationCondition>("Condition", this.condition);
            dictionary.Add("Configuration", this.configuration);
            return dictionary;
        }

        public PdfRichMediaActivationCondition Condition =>
            this.condition;

        public PdfRichMediaConfiguration Configuration =>
            this.configuration;
    }
}

