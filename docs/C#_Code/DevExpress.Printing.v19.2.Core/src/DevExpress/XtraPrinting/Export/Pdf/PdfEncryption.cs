namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.IO;

    public class PdfEncryption : PdfDocumentDictionaryObject
    {
        private DevExpress.XtraPrinting.PrintingPermissions printingPermissions;
        private DevExpress.XtraPrinting.ChangingPermissions changingPermissions;
        private string permissionsPassword;
        private string openPassword;
        private bool enableCoping;
        private bool enableScreenReaders;
        private PdfDocument document;
        private string ownerPassword;
        private string userPassword;
        private byte[] key;
        private int version;
        private int revision;
        private int keyLength;
        private bool encryptMetadata;
        private int permissions;

        public PdfEncryption(PdfDocument document) : base(false)
        {
            this.printingPermissions = DevExpress.XtraPrinting.PrintingPermissions.HighResolution;
            this.changingPermissions = DevExpress.XtraPrinting.ChangingPermissions.AnyExceptExtractingPages;
            this.enableCoping = true;
            this.enableScreenReaders = true;
            this.version = 4;
            this.revision = 4;
            this.keyLength = 0x80;
            this.encryptMetadata = true;
            this.document = document;
        }

        public void Calculate()
        {
            if (this.IsEncryptionOn)
            {
                this.ownerPassword = PdfEncryptHelper.ComputeOwnerPassword(this.openPassword, this.permissionsPassword, this.revision, this.keyLength);
                this.permissions = this.GetPermissions();
                this.key = PdfEncryptHelper.ComputeEncryptionKey(this.openPassword, this.ownerPassword, this.permissions, this.revision, this.keyLength, this.encryptMetadata, this.document.ID);
                this.userPassword = PdfEncryptHelper.ComputeUserPassword(this.key, this.revision, this.document.ID);
            }
        }

        public MemoryStream EncryptStream(MemoryStream stream, int objectNumber, int generationNumber) => 
            this.IsEncryptionOn ? PdfEncryptHelper.EncryptStream(stream, this.key, objectNumber, generationNumber) : stream;

        public string EncryptString(string text, int objectNumber, int generationNumber) => 
            this.IsEncryptionOn ? PdfEncryptHelper.EncryptString(text, this.key, objectNumber, generationNumber) : text;

        public override void FillUp()
        {
            if (this.IsEncryptionOn)
            {
                base.Dictionary.Add("Filter", new PdfName("Standard"));
                base.Dictionary.Add("V", new PdfNumber(this.version));
                base.Dictionary.Add("Length", new PdfNumber(this.keyLength));
                PdfDictionary dictionary = new PdfDictionary();
                string name = "StdCF";
                dictionary.Add(name, this.GetAesCryptFilter());
                base.Dictionary.Add("CF", dictionary);
                base.Dictionary.Add("StmF", new PdfName(name));
                base.Dictionary.Add("StrF", new PdfName(name));
                base.Dictionary.Add("R", new PdfNumber(this.revision));
                base.Dictionary.Add("O", new PdfLiteralString(this.ownerPassword, false));
                base.Dictionary.Add("U", new PdfLiteralString(this.userPassword, false));
                base.Dictionary.Add("P", new PdfNumber(this.permissions));
                base.Dictionary.Add("EncryptMetadata", new PdfBoolean(this.encryptMetadata));
            }
        }

        private PdfDictionary GetAesCryptFilter()
        {
            PdfDictionary dictionary = new PdfDictionary();
            dictionary.Add("CFM", new PdfName("AESV2"));
            dictionary.Add("Length", new PdfNumber(this.keyLength / 8));
            if (!string.IsNullOrEmpty(this.openPassword))
            {
                dictionary.Add("AuthEvent", new PdfName("DocOpen"));
            }
            return dictionary;
        }

        private int GetPermissions()
        {
            BitArray array = new BitArray(0x20);
            array.Set(0, false);
            array.Set(1, false);
            array.Set(6, true);
            array.Set(7, true);
            for (int i = 12; i < 0x20; i++)
            {
                array.Set(i, true);
            }
            switch (this.PrintingPermissions)
            {
                case DevExpress.XtraPrinting.PrintingPermissions.LowResolution:
                    array.Set(2, true);
                    break;

                case DevExpress.XtraPrinting.PrintingPermissions.HighResolution:
                    array.Set(2, true);
                    array.Set(11, true);
                    break;

                default:
                    break;
            }
            switch (this.ChangingPermissions)
            {
                case DevExpress.XtraPrinting.ChangingPermissions.None:
                    break;

                case DevExpress.XtraPrinting.ChangingPermissions.InsertingDeletingRotating:
                    array.Set(10, true);
                    break;

                case DevExpress.XtraPrinting.ChangingPermissions.FillingSigning:
                    array.Set(8, true);
                    break;

                case DevExpress.XtraPrinting.ChangingPermissions.CommentingFillingSigning:
                    array.Set(5, true);
                    break;

                case DevExpress.XtraPrinting.ChangingPermissions.AnyExceptExtractingPages:
                    array.Set(3, true);
                    array.Set(5, true);
                    array.Set(8, true);
                    break;

                default:
                    throw new ArgumentException("ChangingPermissions");
            }
            array.Set(9, this.EnableScreenReaders);
            array.Set(4, this.EnableCoping);
            byte[] buffer = new byte[0x20];
            array.CopyTo(buffer, 0);
            return BitConverter.ToInt32(buffer, 0);
        }

        public DevExpress.XtraPrinting.PrintingPermissions PrintingPermissions
        {
            get => 
                this.printingPermissions;
            set => 
                this.printingPermissions = value;
        }

        public DevExpress.XtraPrinting.ChangingPermissions ChangingPermissions
        {
            get => 
                this.changingPermissions;
            set => 
                this.changingPermissions = value;
        }

        public string PermissionsPassword
        {
            get => 
                this.permissionsPassword;
            set => 
                this.permissionsPassword = value;
        }

        public string OpenPassword
        {
            get => 
                this.openPassword;
            set => 
                this.openPassword = value;
        }

        public bool EnableCoping
        {
            get => 
                this.enableCoping;
            set => 
                this.enableCoping = value;
        }

        public bool EnableScreenReaders
        {
            get => 
                this.enableScreenReaders;
            set => 
                this.enableScreenReaders = value;
        }

        public bool IsEncryptionOn =>
            !string.IsNullOrEmpty(this.openPassword) || !string.IsNullOrEmpty(this.permissionsPassword);

        public byte[] Key =>
            this.key;
    }
}

