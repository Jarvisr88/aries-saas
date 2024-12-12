namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class StandardEncryptionVerifier
    {
        private const int bytesLength = 0x10;
        private const int hashLength = 0x20;
        private byte[] salt;
        private byte[] encryptedVerifier;
        private byte[] encryptedVerifierHash;
        private int verifierHashSize;

        public int GetSize() => 
            0x48;

        public void Read(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            this.salt = reader.ReadBytes(0x10);
            this.encryptedVerifier = reader.ReadBytes(0x10);
            this.verifierHashSize = reader.ReadInt32();
            this.encryptedVerifierHash = reader.ReadBytes(0x20);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(0x10);
            writer.Write(this.salt);
            writer.Write(this.encryptedVerifier);
            writer.Write(this.verifierHashSize);
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
                if (value.Length != 0x20)
                {
                    throw new ArgumentException("Invalid EncryptedVerifierHash value length");
                }
                this.encryptedVerifierHash = value;
            }
        }

        public int VerifierHashSize
        {
            get => 
                this.verifierHashSize;
            set => 
                this.verifierHashSize = value;
        }
    }
}

