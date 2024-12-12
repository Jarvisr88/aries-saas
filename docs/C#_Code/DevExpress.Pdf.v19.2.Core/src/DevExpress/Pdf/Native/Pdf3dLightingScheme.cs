namespace DevExpress.Pdf.Native
{
    using System;

    public class Pdf3dLightingScheme : PdfObject
    {
        private readonly Pdf3dLightingSchemeType type;

        public Pdf3dLightingScheme(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.type = dictionary.GetEnumName<Pdf3dLightingSchemeType>("Subtype");
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "3DLightingScheme");
            dictionary.AddEnumName<Pdf3dLightingSchemeType>("Subtype", this.type);
            return dictionary;
        }

        public Pdf3dLightingSchemeType Type =>
            this.type;
    }
}

