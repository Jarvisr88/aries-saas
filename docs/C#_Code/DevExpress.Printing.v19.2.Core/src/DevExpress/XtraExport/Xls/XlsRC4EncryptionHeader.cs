namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class XlsRC4EncryptionHeader : XlsRC4EncryptionHeaderBase
    {
        private const int bytesLength = 0x10;
        private byte[] salt = new byte[0x10];
        private byte[] encryptedVerifier = new byte[0x10];
        private byte[] encryptedVerifierHash = new byte[0x10];

        public XlsRC4EncryptionHeader()
        {
            base.VersionMinor = 1;
            base.VersionMajor = 1;
        }

        public override int GetSize() => 
            base.GetSize() + 0x30;

        protected override void ReadCore(XlReader reader)
        {
            this.salt = reader.ReadNotCryptedBytes(0x10);
            this.encryptedVerifier = reader.ReadNotCryptedBytes(0x10);
            this.encryptedVerifierHash = reader.ReadNotCryptedBytes(0x10);
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.salt);
            writer.Write(this.encryptedVerifier);
            writer.Write(this.encryptedVerifierHash);
        }

        public byte[] Salt
        {
            get => 
                this.salt;
            set
            {
                Guard.ArgumentNotNull(value, "Salt value");
                if (value.Length != 0x10)
                {
                    throw new ArgumentException("Invalid Salt value length");
                }
                this.salt = value;
            }
        }

        public byte[] EncryptedVerifier
        {
            get => 
                this.encryptedVerifier;
            set
            {
                Guard.ArgumentNotNull(value, "EncryptedVerifier value");
                if (value.Length != 0x10)
                {
                    throw new ArgumentException("Invalid EncryptedVerifier value length");
                }
                this.encryptedVerifier = value;
            }
        }

        public byte[] EncryptedVerifierHash
        {
            get => 
                this.encryptedVerifierHash;
            set
            {
                Guard.ArgumentNotNull(value, "EncryptedVerifierHash value");
                if (value.Length != 0x10)
                {
                    throw new ArgumentException("Invalid EncryptedVerifierHash value length");
                }
                this.encryptedVerifierHash = value;
            }
        }
    }
}

