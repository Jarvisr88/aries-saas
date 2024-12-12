namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfWatermarkAnnotation : PdfAnnotation
    {
        internal const string Type = "Watermark";
        private const string fixedPrintDictionaryKey = "FixedPrint";
        private const string horizontalTranslationDictionaryKey = "H";
        private const string verticalTranslationDictionaryKey = "V";
        private readonly double horizontalTranslationPercent;
        private readonly double verticalTranslationPercent;

        internal PdfWatermarkAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("FixedPrint");
            if (dictionary2 != null)
            {
                double? number = dictionary2.GetNumber("H");
                this.horizontalTranslationPercent = ((number != null) ? number.GetValueOrDefault() : 0.0) * 100.0;
                number = dictionary2.GetNumber("V");
                this.verticalTranslationPercent = ((number != null) ? number.GetValueOrDefault() : 0.0) * 100.0;
                if ((this.horizontalTranslationPercent < 0.0) || ((this.horizontalTranslationPercent > 100.0) || ((this.verticalTranslationPercent < 0.0) || (this.verticalTranslationPercent > 100.0))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if ((this.horizontalTranslationPercent != 0.0) || (this.verticalTranslationPercent != 0.0))
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(collection);
                dictionary2.AddName("Type", "FixedPrint");
                dictionary2.Add("H", this.horizontalTranslationPercent / 100.0, 0.0);
                dictionary2.Add("V", this.verticalTranslationPercent / 100.0, 0.0);
                dictionary.Add("FixedPrint", dictionary2);
            }
            return dictionary;
        }

        public double HorizontalTranslationPercent =>
            this.horizontalTranslationPercent;

        public double VerticalTranslationPercent =>
            this.verticalTranslationPercent;

        protected override string AnnotationType =>
            "Watermark";
    }
}

