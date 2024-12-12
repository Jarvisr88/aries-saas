namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCIDSystemInfo : PdfObject
    {
        internal const string RegistryKey = "Registry";
        internal const string OrderingKey = "Ordering";
        internal const string SupplementKey = "Supplement";
        private readonly string registry;
        private readonly string ordering;
        private readonly int supplement;

        internal PdfCIDSystemInfo(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.registry = dictionary.GetString("Registry");
            this.ordering = dictionary.GetString("Ordering");
            int? integer = dictionary.GetInteger("Supplement");
            if ((this.registry == null) || ((this.ordering == null) || (integer == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.supplement = integer.Value;
        }

        internal PdfCIDSystemInfo(string registry, string ordering, int supplement) : base(-1)
        {
            this.registry = registry;
            this.ordering = ordering;
            this.supplement = supplement;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddASCIIString("Registry", this.registry);
            dictionary.AddASCIIString("Ordering", this.ordering);
            dictionary.Add("Supplement", this.supplement);
            return dictionary;
        }

        public string Registry =>
            this.registry;

        public string Ordering =>
            this.ordering;

        public int Supplement =>
            this.supplement;
    }
}

