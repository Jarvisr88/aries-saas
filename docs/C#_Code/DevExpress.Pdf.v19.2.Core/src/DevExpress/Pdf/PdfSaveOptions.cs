namespace DevExpress.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfSaveOptions
    {
        private PdfSignature signature;
        private PdfEncryptionOptions encryptionOptions;
        public PdfEncryptionOptions EncryptionOptions
        {
            get => 
                this.encryptionOptions;
            set => 
                this.encryptionOptions = value;
        }
        public PdfSignature Signature
        {
            get => 
                this.signature;
            set => 
                this.signature = value;
        }
    }
}

