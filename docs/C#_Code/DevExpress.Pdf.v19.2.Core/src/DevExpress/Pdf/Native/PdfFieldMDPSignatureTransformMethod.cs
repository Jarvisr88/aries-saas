namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfFieldMDPSignatureTransformMethod : PdfSignatureTransformMethod
    {
        private readonly PdfSignatureFormFieldLock formFieldLock;

        public PdfFieldMDPSignatureTransformMethod(PdfReaderDictionary dictionary) : base(dictionary)
        {
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.formFieldLock = new PdfSignatureFormFieldLock(dictionary);
        }

        public PdfSignatureFormFieldLock Lock =>
            this.formFieldLock;

        protected override string ValidVersion =>
            "1.2";
    }
}

