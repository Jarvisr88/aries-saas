namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfDocumentMDPSignatureTransformMethod : PdfSignatureTransformMethod
    {
        private const string permissionsDictionaryKey = "P";
        private readonly PdfDocumentAccessPermissions permissions;

        public PdfDocumentMDPSignatureTransformMethod(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.permissions = (dictionary == null) ? PdfDocumentAccessPermissions.FormFillingAndSignatures : PdfEnumToValueConverter.Parse<PdfDocumentAccessPermissions>(dictionary.GetInteger("P"), 1);
        }

        protected override string ValidVersion =>
            "1.2";

        public PdfDocumentAccessPermissions Permissions =>
            this.permissions;
    }
}

