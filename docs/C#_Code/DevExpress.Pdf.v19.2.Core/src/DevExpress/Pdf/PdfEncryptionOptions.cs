namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Security;

    public class PdfEncryptionOptions
    {
        private string ownerPassword;
        private string userPassword;
        private PdfEncryptionAlgorithm algorithm = PdfEncryptionAlgorithm.AES128;
        private PdfDocumentPrintingPermissions printingPermissions = PdfDocumentPrintingPermissions.Allowed;
        private PdfDocumentDataExtractionPermissions dataExtractionPermissions = PdfDocumentDataExtractionPermissions.Allowed;
        private PdfDocumentModificationPermissions modificationPermissions = PdfDocumentModificationPermissions.Allowed;
        private PdfDocumentInteractivityPermissions interactivityPermissions = PdfDocumentInteractivityPermissions.Allowed;

        [Obsolete("The OwnerPassword property is now obsolete. Use the OwnerPasswordString property instead.")]
        public SecureString OwnerPassword
        {
            get => 
                PdfSecureStringAccessor.ToSecureString(this.ownerPassword);
            set => 
                this.ownerPassword = PdfSecureStringAccessor.FromSecureString(value);
        }

        [Obsolete("The UserPassword property is now obsolete. Use the UserPasswordString property instead.")]
        public SecureString UserPassword
        {
            get => 
                PdfSecureStringAccessor.ToSecureString(this.userPassword);
            set => 
                this.userPassword = PdfSecureStringAccessor.FromSecureString(value);
        }

        public string OwnerPasswordString
        {
            get => 
                this.ownerPassword;
            set => 
                this.ownerPassword = value;
        }

        public string UserPasswordString
        {
            get => 
                this.userPassword;
            set => 
                this.userPassword = value;
        }

        public PdfEncryptionAlgorithm Algorithm
        {
            get => 
                this.algorithm;
            set => 
                this.algorithm = value;
        }

        public PdfDocumentPrintingPermissions PrintingPermissions
        {
            get => 
                this.printingPermissions;
            set => 
                this.printingPermissions = value;
        }

        public PdfDocumentDataExtractionPermissions DataExtractionPermissions
        {
            get => 
                this.dataExtractionPermissions;
            set => 
                this.dataExtractionPermissions = value;
        }

        public PdfDocumentModificationPermissions ModificationPermissions
        {
            get => 
                this.modificationPermissions;
            set => 
                this.modificationPermissions = value;
        }

        public PdfDocumentInteractivityPermissions InteractivityPermissions
        {
            get => 
                this.interactivityPermissions;
            set => 
                this.interactivityPermissions = value;
        }

        internal PdfEncryptionParameters EncryptionParameters
        {
            get
            {
                if ((this.userPassword == null) && (this.ownerPassword == null))
                {
                    return null;
                }
                long flags = 0xfffff0c0L;
                PdfDocumentPrintingPermissions printingPermissions = this.printingPermissions;
                if (printingPermissions == PdfDocumentPrintingPermissions.LowQuality)
                {
                    flags |= 4L;
                }
                else if (printingPermissions == PdfDocumentPrintingPermissions.Allowed)
                {
                    flags |= 0x804L;
                }
                PdfDocumentDataExtractionPermissions dataExtractionPermissions = this.dataExtractionPermissions;
                if (dataExtractionPermissions == PdfDocumentDataExtractionPermissions.Accessibility)
                {
                    flags |= 0x200L;
                }
                else if (dataExtractionPermissions == PdfDocumentDataExtractionPermissions.Allowed)
                {
                    flags |= 0x210L;
                }
                PdfDocumentInteractivityPermissions interactivityPermissions = this.interactivityPermissions;
                if (interactivityPermissions == PdfDocumentInteractivityPermissions.FormFillingAndSigning)
                {
                    flags |= 0x100L;
                }
                else if (interactivityPermissions == PdfDocumentInteractivityPermissions.Allowed)
                {
                    flags |= 0x120L;
                }
                PdfDocumentModificationPermissions modificationPermissions = this.modificationPermissions;
                if (modificationPermissions == PdfDocumentModificationPermissions.DocumentAssembling)
                {
                    flags |= 0x400L;
                }
                else if (modificationPermissions == PdfDocumentModificationPermissions.Allowed)
                {
                    flags |= 0x408L;
                }
                return new PdfEncryptionParameters(this.ownerPassword, this.userPassword, this.algorithm, flags);
            }
        }
    }
}

