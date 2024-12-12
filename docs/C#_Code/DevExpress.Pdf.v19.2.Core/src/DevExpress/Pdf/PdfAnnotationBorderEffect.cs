namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAnnotationBorderEffect
    {
        internal const string DictionaryKey = "BE";
        private const string styleKey = "S";
        private const string intensityKey = "I";
        private readonly PdfAnnotationBorderEffectStyle style;
        private readonly double intensity;

        private PdfAnnotationBorderEffect(PdfReaderDictionary dictionary)
        {
            this.style = dictionary.GetEnumName<PdfAnnotationBorderEffectStyle>("S");
            double? number = dictionary.GetNumber("I");
            if (number != null)
            {
                this.intensity = number.Value;
                if ((this.style != PdfAnnotationBorderEffectStyle.CloudyEffect) || ((this.intensity < 0.0) || (this.intensity > 2.0)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        internal static PdfAnnotationBorderEffect Parse(PdfReaderDictionary dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("BE");
            return ((dictionary2 == null) ? null : new PdfAnnotationBorderEffect(dictionary2));
        }

        internal PdfWriterDictionary ToWritableObject()
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            dictionary.AddEnumName<PdfAnnotationBorderEffectStyle>("S", this.style);
            dictionary.Add("I", this.intensity, 0.0);
            return dictionary;
        }

        public PdfAnnotationBorderEffectStyle Style =>
            this.style;

        public double Intensity =>
            this.intensity;
    }
}

