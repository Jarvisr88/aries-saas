namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsXORObfuscation
    {
        private const int size = 4;

        protected XlsXORObfuscation()
        {
        }

        public XlsXORObfuscation(short key, short verificationId)
        {
            this.Key = key;
            this.VerificationId = verificationId;
        }

        public static XlsXORObfuscation FromStream(XlReader reader)
        {
            XlsXORObfuscation obfuscation = new XlsXORObfuscation();
            obfuscation.Read(reader);
            return obfuscation;
        }

        public int GetSize() => 
            4;

        protected void Read(XlReader reader)
        {
            this.Key = reader.ReadNotCryptedInt16();
            this.VerificationId = reader.ReadNotCryptedInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.Key);
            writer.Write(this.VerificationId);
        }

        public short Key { get; protected set; }

        public short VerificationId { get; protected set; }
    }
}

