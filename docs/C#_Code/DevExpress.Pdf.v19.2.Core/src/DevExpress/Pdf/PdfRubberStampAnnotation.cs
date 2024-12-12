namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRubberStampAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Stamp";
        private const string iconNameKey = "Name";
        private readonly string iconName;

        internal PdfRubberStampAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            string name = dictionary.GetName("Name");
            string text2 = name;
            if (name == null)
            {
                string local1 = name;
                text2 = "Draft";
            }
            this.iconName = text2;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.AddName("Name", this.iconName);
            return dictionary;
        }

        public string IconName =>
            this.iconName;

        protected override string AnnotationType =>
            "Stamp";
    }
}

