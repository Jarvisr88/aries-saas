namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTransparencyGroup : PdfObject
    {
        private const string dictionaryType = "Group";
        private const string dictionarySubtype = "Transparency";
        private const string subtypeDictionaryKey = "S";
        private const string colorSpaceDictionaryKey = "CS";
        private const string isolatedDictionaryKey = "I";
        private const string knockoutDictionaryKey = "K";
        private readonly PdfColorSpace colorSpace;
        private readonly bool isolated;
        private readonly bool knockout;

        internal PdfTransparencyGroup()
        {
            this.colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
        }

        internal PdfTransparencyGroup(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.colorSpace = dictionary.GetColorSpace("CS");
            bool? boolean = dictionary.GetBoolean("I");
            this.isolated = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("K");
            this.knockout = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("Group"));
            dictionary.Add("S", new PdfName("Transparency"));
            if (this.colorSpace != null)
            {
                dictionary.Add("CS", this.colorSpace.Write(collection));
            }
            dictionary.Add("I", this.isolated, false);
            dictionary.Add("K", this.knockout, false);
            return dictionary;
        }

        public PdfColorSpace ColorSpace =>
            this.colorSpace;

        public bool Isolated =>
            this.isolated;

        public bool Knockout =>
            this.knockout;
    }
}

