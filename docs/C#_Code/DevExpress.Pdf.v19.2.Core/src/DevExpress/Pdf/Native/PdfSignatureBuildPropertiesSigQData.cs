namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSignatureBuildPropertiesSigQData : PdfSignatureBuildPropertiesData
    {
        private readonly bool preview;

        public PdfSignatureBuildPropertiesSigQData(PdfReaderDictionary dictionary) : base(dictionary)
        {
            bool? boolean = dictionary.GetBoolean("Preview");
            this.preview = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        public bool Preview =>
            this.preview;
    }
}

