namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSourceInformation : PdfObject
    {
        private const string urlKey = "AU";
        private const string timeStampKey = "TS";
        private const string expirationStampKey = "E";
        private const string submissionTypeKey = "S";
        private readonly string url;
        private readonly DateTimeOffset? timeStamp;
        private readonly DateTimeOffset? expirationStamp;
        private readonly PdfFormSubmissionType formSubmissionType;

        internal PdfSourceInformation(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.url = dictionary.GetString("AU");
            if (string.IsNullOrEmpty(this.url))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.timeStamp = dictionary.GetDate("TS");
            this.expirationStamp = dictionary.GetDate("E");
            this.formSubmissionType = PdfEnumToValueConverter.Parse<PdfFormSubmissionType>(dictionary.GetInteger("S"), 0);
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("AU", this.url, null);
            dictionary.Add("TS", this.timeStamp, null);
            dictionary.Add("E", this.expirationStamp, null);
            if (this.formSubmissionType != PdfFormSubmissionType.None)
            {
                dictionary.Add("S", PdfEnumToValueConverter.Convert<PdfFormSubmissionType>(this.formSubmissionType));
            }
            return dictionary;
        }

        public string Url =>
            this.url;

        public DateTimeOffset? TimeStamp =>
            this.timeStamp;

        public DateTimeOffset? ExpirationStamp =>
            this.expirationStamp;

        public PdfFormSubmissionType FormSubmissionType =>
            this.formSubmissionType;
    }
}

