namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfProperties : PdfObject
    {
        protected PdfProperties(int objectNumber) : base(objectNumber)
        {
        }

        internal static PdfProperties ParseProperties(PdfReaderDictionary dictionary)
        {
            object obj2;
            if (dictionary == null)
            {
                return null;
            }
            if (dictionary.TryGetValue("Type", out obj2))
            {
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    string str = name.Name;
                    if ((str == "OCG") || (str == "OCMD"))
                    {
                        return PdfOptionalContent.ParseOptionalContent(dictionary);
                    }
                }
            }
            return new PdfCustomProperties(dictionary);
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection) => 
            this.Write(collection);

        protected internal abstract object Write(PdfObjectCollection collection);
    }
}

