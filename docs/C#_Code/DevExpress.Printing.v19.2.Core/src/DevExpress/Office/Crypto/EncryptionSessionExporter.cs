namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class EncryptionSessionExporter : IPasswordKeyEncryptorVisitor
    {
        public static string PasswordKeyEncryptorDataRootUri = EncryptionSessionImporter.PasswordKeyEncryptorDataRootUri;
        public static string EncryptionDataRootUri = EncryptionSessionImporter.EncryptionDataRootUri;
        private EncryptionSession session;
        private BinaryWriter writer;

        public EncryptionSessionExporter(EncryptionSession session)
        {
            this.session = session;
        }

        public void Export(Stream stream)
        {
            this.writer = new BinaryWriter(stream);
            this.session.PasswordKeyEncryptor.Accept(this);
        }

        public void Visit(IAgilePasswordKeyEncryptor encryptor)
        {
            this.writer.Write((short) 4);
            this.writer.Write((short) 4);
            this.writer.Write(0x40);
            XmlWriterSettings settings = new XmlWriterSettings {
                Encoding = Encoding.UTF8
            };
            using (XmlWriter writer = XmlWriter.Create(this.writer.BaseStream, settings))
            {
                this.WriteToXml(writer, encryptor);
            }
        }

        public void Visit(IStandardPasswordKeyEncryptor encryptor)
        {
            new StandardEncryptionHeader { 
                AES = true,
                AlgorithmId = (encryptor.CipherInfo.KeyBits == 0x80) ? 0x660e : ((encryptor.CipherInfo.KeyBits == 0xc0) ? 0x660f : 0x6610),
                AlgorithmIdHash = 0x8004,
                CryptoAPI = true,
                CSPName = "Microsoft Enhanced RSA and AES Cryptographic Provider",
                DocumentPropertiesEncrypted = false,
                ExternalEncryption = false,
                KeySize = encryptor.CipherInfo.KeyBits,
                ProviderType = 0x18
            }.Write(this.writer);
            new StandardEncryptionVerifier { 
                EncryptedVerifier = encryptor.EncryptedVerifier,
                EncryptedVerifierHash = encryptor.EncryptedVerifierHash,
                Salt = encryptor.SaltValue,
                VerifierHashSize = encryptor.HashInfo.HashBits / 8
            }.Write(this.writer);
        }

        private void WriteDataIntegrityElement(XmlWriter writer, HmacData hmacData)
        {
            if (hmacData != null)
            {
                writer.WriteStartElement("dataIntegrity", EncryptionDataRootUri);
                writer.WriteAttributeString(8.ToString(), Convert.ToBase64String(hmacData.EncryptedKey));
                writer.WriteAttributeString(9.ToString(), Convert.ToBase64String(hmacData.EncryptedValue));
                writer.WriteEndElement();
            }
        }

        private void WriteEncryptedKeyElement(XmlWriter writer, IAgilePasswordKeyEncryptor encryptor)
        {
            writer.WriteStartElement("encryptedKey", PasswordKeyEncryptorDataRootUri);
            writer.WriteAttributeString(12.ToString(), Convert.ToBase64String(encryptor.EncryptedVerifierHashInput));
            writer.WriteAttributeString(13.ToString(), Convert.ToBase64String(encryptor.EncryptedVerifierHashValue));
            writer.WriteAttributeString(14.ToString(), Convert.ToBase64String(encryptor.EncryptedKeyValue));
            IPasswordBasedKey passwordEncryptor = encryptor.PasswordEncryptor;
            writer.WriteAttributeString(11.ToString(), passwordEncryptor.SpinCount.ToString());
            HashInfo hashInfo = passwordEncryptor.HashInfo;
            writer.WriteAttributeString(2.ToString(), (hashInfo.HashBits / 8).ToString());
            writer.WriteAttributeString(7.ToString(), hashInfo.Name);
            CipherInfo cipherInfo = passwordEncryptor.CipherInfo;
            writer.WriteAttributeString(0.ToString(), (cipherInfo.BlockBits / 8).ToString());
            writer.WriteAttributeString(1.ToString(), cipherInfo.KeyBits.ToString());
            writer.WriteAttributeString(6.ToString(), EncryptionSessionImporter.GetCipherString(cipherInfo.Mode));
            writer.WriteAttributeString(5.ToString(), cipherInfo.Name);
            byte[] saltValue = passwordEncryptor.SaltValue;
            writer.WriteAttributeString(3.ToString(), saltValue.Length.ToString());
            writer.WriteAttributeString(4.ToString(), Convert.ToBase64String(saltValue));
            writer.WriteEndElement();
        }

        private void WriteKeyDataElement(XmlWriter writer, IPrimaryCipher primaryCipher)
        {
            writer.WriteStartElement("keyData", EncryptionDataRootUri);
            CipherInfo cipherInfo = primaryCipher.CipherInfo;
            writer.WriteAttributeString(0.ToString(), (cipherInfo.BlockBits / 8).ToString());
            writer.WriteAttributeString(1.ToString(), cipherInfo.KeyBits.ToString());
            writer.WriteAttributeString(5.ToString(), cipherInfo.Name);
            writer.WriteAttributeString(6.ToString(), EncryptionSessionImporter.GetCipherString(cipherInfo.Mode));
            HashInfo hashInfo = primaryCipher.HashInfo;
            writer.WriteAttributeString(2.ToString(), (hashInfo.HashBits / 8).ToString());
            writer.WriteAttributeString(7.ToString(), hashInfo.Name);
            byte[] saltValue = primaryCipher.SaltValue;
            writer.WriteAttributeString(3.ToString(), saltValue.Length.ToString());
            writer.WriteAttributeString(4.ToString(), Convert.ToBase64String(saltValue));
            writer.WriteEndElement();
        }

        private void WriteToXml(XmlWriter writer, IAgilePasswordKeyEncryptor encryptor)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("encryption", EncryptionDataRootUri);
            this.WriteKeyDataElement(writer, this.session.PrimaryCipher);
            this.WriteDataIntegrityElement(writer, this.session.HmacData);
            writer.WriteStartElement("keyEncryptors", EncryptionDataRootUri);
            writer.WriteStartElement("keyEncryptor", EncryptionDataRootUri);
            writer.WriteAttributeString(10.ToString(), PasswordKeyEncryptorDataRootUri);
            this.WriteEncryptedKeyElement(writer, encryptor);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }
    }
}

