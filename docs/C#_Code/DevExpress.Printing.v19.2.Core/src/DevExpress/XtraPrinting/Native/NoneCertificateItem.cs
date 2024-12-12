namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Localization;
    using System;

    public sealed class NoneCertificateItem : ICertificateItem
    {
        public static NoneCertificateItem Instance = new NoneCertificateItem();

        private NoneCertificateItem()
        {
        }

        private static string NoneText =>
            PreviewLocalizer.GetString(PreviewStringId.ExportOption_PdfSignature_EmptyCertificate);

        public string Subject =>
            NoneText;

        public string Description =>
            null;
    }
}

