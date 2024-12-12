namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfSignatureTransformMethod
    {
        protected PdfSignatureTransformMethod(PdfReaderDictionary dictionary)
        {
            if (dictionary != null)
            {
                string name = dictionary.GetName("V");
                if ((name != null) && (name != this.ValidVersion))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected abstract string ValidVersion { get; }
    }
}

