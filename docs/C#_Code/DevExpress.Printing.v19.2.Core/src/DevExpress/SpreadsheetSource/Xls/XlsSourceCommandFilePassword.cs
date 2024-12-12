namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Crypto;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.IO;

    public class XlsSourceCommandFilePassword : XlsSourceCommandBase
    {
        public const string MagicMSPassword = "VelvetSweatshop";
        private bool rc4Encrypted;
        private XlsXORObfuscation xorObfuscation = new XlsXORObfuscation(0, 0);
        private XlsRC4EncryptionHeaderBase rc4EncryptionHeader = new XlsRC4EncryptionHeader();

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (!this.rc4Encrypted)
            {
                throw new EncryptedFileException(EncryptedFileError.EncryptionTypeNotSupported, "XOR obfuscated files is not supported!");
            }
            XlsRC4EncryptionHeader header = this.rc4EncryptionHeader as XlsRC4EncryptionHeader;
            if (header == null)
            {
                throw new EncryptedFileException(EncryptedFileError.EncryptionTypeNotSupported, "RC4 CryptoAPI encrypted files is not supported!");
            }
            ARC4PasswordVerifier verifier = new ARC4PasswordVerifier(header.Salt, header.EncryptedVerifier, header.EncryptedVerifierHash);
            string password = contentBuilder.Options.Password;
            bool flag = string.IsNullOrEmpty(password);
            if (flag)
            {
                password = "VelvetSweatshop";
            }
            if (verifier.VerifyPassword(password))
            {
                contentBuilder.SetupRC4Decryptor(password, header.Salt);
            }
            else
            {
                if (flag)
                {
                    throw new EncryptedFileException(EncryptedFileError.PasswordRequired, "Password required to open this file!");
                }
                throw new EncryptedFileException(EncryptedFileError.WrongPassword, "Wrong password!");
            }
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.rc4Encrypted = Convert.ToBoolean(reader.ReadUInt16());
            if (!this.rc4Encrypted)
            {
                this.xorObfuscation = XlsXORObfuscation.FromStream(reader);
            }
            else
            {
                short num = reader.ReadInt16();
                short num2 = reader.ReadInt16();
                if ((num == 1) && (num2 == 1))
                {
                    this.rc4EncryptionHeader = new XlsRC4EncryptionHeader();
                }
                else
                {
                    if ((num < 2) || (num2 != 2))
                    {
                        throw new InvalidFileException(InvalidFileError.CorruptedFile, "Unknown FilePass header version");
                    }
                    this.rc4EncryptionHeader = new XlsRC4CryptoAPIEncryptionHeader();
                }
                reader.Seek((long) (-4), SeekOrigin.Current);
                this.rc4EncryptionHeader.Read(reader);
            }
        }

        protected bool RC4Encrypted
        {
            get => 
                this.rc4Encrypted;
            set => 
                this.rc4Encrypted = value;
        }

        protected XlsXORObfuscation XorObfuscation
        {
            get => 
                this.xorObfuscation;
            set => 
                this.xorObfuscation = value;
        }
    }
}

