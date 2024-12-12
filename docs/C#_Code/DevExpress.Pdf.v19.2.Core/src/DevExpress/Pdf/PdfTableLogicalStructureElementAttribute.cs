namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfTableLogicalStructureElementAttribute : PdfLogicalStructureElementAttribute
    {
        internal const string Owner = "Table";
        private const string rowSpanKey = "RowSpan";
        private const string colSpanKey = "ColSpan";
        private const string headersKey = "Headers";
        private const string scopeKey = "Scope";
        private const string summaryKey = "Summary";
        private readonly int rowSpan;
        private readonly int colSpan;
        private readonly IList<string> headers;
        private readonly PdfTableLogicalStructureElementAttributeScope? scope;
        private readonly string summary;

        internal PdfTableLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.rowSpan = 1;
            this.colSpan = 1;
            int? integer = dictionary.GetInteger("RowSpan");
            this.rowSpan = (integer != null) ? integer.GetValueOrDefault() : 1;
            integer = dictionary.GetInteger("ColSpan");
            this.colSpan = (integer != null) ? integer.GetValueOrDefault() : 1;
            Func<object, string> create = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__21_0;
                create = <>c.<>9__21_0 = delegate (object o) {
                    byte[] buffer = o as byte[];
                    if (buffer == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfDocumentReader.ConvertToString(buffer);
                };
            }
            this.headers = dictionary.GetArray<string>("Headers", create);
            string name = dictionary.GetName("Scope");
            if (!string.IsNullOrEmpty(name))
            {
                this.scope = new PdfTableLogicalStructureElementAttributeScope?(PdfEnumToStringConverter.Parse<PdfTableLogicalStructureElementAttributeScope>(name, true));
            }
            this.summary = dictionary.GetString("Summary");
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.AddName("O", "Table");
            dictionary.Add("RowSpan", this.rowSpan, 1);
            dictionary.Add("ColSpan", this.colSpan, 1);
            if (this.headers != null)
            {
                dictionary.Add("Headers", new PdfWritableArray(this.headers));
            }
            if (this.scope != null)
            {
                dictionary.AddEnumName<PdfTableLogicalStructureElementAttributeScope>("Scope", this.scope.Value);
            }
            dictionary.AddNotNullOrEmptyString("Summary", this.summary);
            return dictionary;
        }

        public int RowSpan =>
            this.rowSpan;

        public int ColSpan =>
            this.colSpan;

        public IList<string> Headers =>
            this.headers;

        public PdfTableLogicalStructureElementAttributeScope? Scope =>
            this.scope;

        public string Summary =>
            this.summary;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfTableLogicalStructureElementAttribute.<>c <>9 = new PdfTableLogicalStructureElementAttribute.<>c();
            public static Func<object, string> <>9__21_0;

            internal string <.ctor>b__21_0(object o)
            {
                byte[] buffer = o as byte[];
                if (buffer == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfDocumentReader.ConvertToString(buffer);
            }
        }
    }
}

