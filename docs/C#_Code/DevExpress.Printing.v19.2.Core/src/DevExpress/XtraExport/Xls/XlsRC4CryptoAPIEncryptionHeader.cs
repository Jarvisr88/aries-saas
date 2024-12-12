namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlsRC4CryptoAPIEncryptionHeader : XlsRC4EncryptionHeaderBase
    {
        public XlsRC4CryptoAPIEncryptionHeader()
        {
            base.VersionMajor = 4;
            base.VersionMinor = 2;
        }

        private int GetHeaderSize() => 
            !string.IsNullOrEmpty(this.CSPName) ? (0x20 + ((this.CSPName.Length + 1) * 2)) : 0x20;

        public override int GetSize() => 
            (base.GetSize() + this.GetHeaderSize()) + 8;

        protected override void ReadCore(XlReader reader)
        {
            reader.ReadInt32();
            int num = reader.ReadNotCryptedInt32();
            int num2 = reader.ReadNotCryptedInt32();
            this.CryptoAPI = Convert.ToBoolean((int) (num2 & 4));
            this.DocumentPropertiesEncrypted = Convert.ToBoolean((int) (num2 & 8));
            this.ExternalEncryption = Convert.ToBoolean((int) (num2 & 0x10));
            this.AES = Convert.ToBoolean((int) (num2 & 0x20));
            if (reader.ReadNotCryptedInt32() != 0)
            {
                throw new Exception("Invalid Xls file : XlsRC4CryptoAPIEncryptionHeader.ReadCore: sizeExtra != 0");
            }
            this.AlgorithmId = reader.ReadNotCryptedInt32();
            this.AlgorithmIdHash = reader.ReadNotCryptedInt32();
            this.KeySize = reader.ReadNotCryptedInt32();
            this.ProviderType = reader.ReadNotCryptedInt32();
            reader.ReadNotCryptedInt32();
            reader.ReadNotCryptedInt32();
            int count = num - 0x20;
            if (count > 0)
            {
                byte[] bytes = reader.ReadNotCryptedBytes(count);
                this.CSPName = Encoding.Unicode.GetString(bytes, 0, bytes.Length - 2);
            }
        }

        protected override void WriteCore(BinaryWriter writer)
        {
            int num = 0;
            if (this.CryptoAPI)
            {
                num |= 4;
            }
            if (this.DocumentPropertiesEncrypted)
            {
                num |= 8;
            }
            if (this.ExternalEncryption)
            {
                num |= 0x10;
            }
            if (this.AES)
            {
                num |= 0x20;
            }
            writer.Write(num);
            writer.Write(this.GetHeaderSize());
            writer.Write(num);
            writer.Write(0);
            writer.Write(this.AlgorithmId);
            writer.Write(this.AlgorithmIdHash);
            writer.Write(this.KeySize);
            writer.Write(this.ProviderType);
            writer.Write(0);
            writer.Write(0);
            if (!string.IsNullOrEmpty(this.CSPName))
            {
                byte[] bytes = Encoding.Unicode.GetBytes(this.CSPName + "\0");
                writer.Write(bytes);
            }
        }

        public bool CryptoAPI { get; set; }

        public bool DocumentPropertiesEncrypted { get; set; }

        public bool ExternalEncryption { get; set; }

        public bool AES { get; set; }

        public int AlgorithmId { get; set; }

        public int AlgorithmIdHash { get; set; }

        public int KeySize { get; set; }

        public int ProviderType { get; set; }

        public string CSPName { get; set; }
    }
}

