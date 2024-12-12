namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfOptionalContent : PdfProperties
    {
        internal const string DictionaryKey = "OC";

        protected PdfOptionalContent(int objectNumber) : base(objectNumber)
        {
        }

        internal static PdfOptionalContent ParseOptionalContent(PdfReaderDictionary dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            string name = dictionary.GetName("Type");
            return ((name == "OCG") ? ((PdfOptionalContent) new PdfOptionalContentGroup(dictionary)) : ((name == "OCMD") ? ((PdfOptionalContent) new PdfOptionalContentMembership(dictionary)) : null));
        }
    }
}

