namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfEncryptionParameters
    {
        private readonly string ownerPassword;
        private readonly string userPassword;
        private readonly PdfEncryptionAlgorithm algorithm;
        private readonly long flags;

        public PdfEncryptionParameters(string ownerPassword, string userPassword, PdfEncryptionAlgorithm algorithm, long flags)
        {
            this.ownerPassword = ownerPassword;
            this.userPassword = userPassword;
            this.algorithm = algorithm;
            this.flags = flags;
        }

        public string OwnerPassword =>
            this.ownerPassword;

        public string UserPassword =>
            this.userPassword;

        public PdfEncryptionAlgorithm Algorithm =>
            this.algorithm;

        public long Flags =>
            this.flags;
    }
}

