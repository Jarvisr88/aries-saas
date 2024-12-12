namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Xml;

    internal class EncryptionSessionImporter
    {
        public static string PasswordKeyEncryptorDataRootUri = "http://schemas.microsoft.com/office/2006/keyEncryptor/password";
        public static string EncryptionDataRootUri = "http://schemas.microsoft.com/office/2006/encryption";
        private PrimaryCipher primaryCipher;
        private IPasswordKeyEncryptor passwordKeyEncryptor;
        private HmacData hmacData;

        public static CipherMode GetCipherMode(string value)
        {
            if (value == "ChainingModeCBC")
            {
                return CipherMode.CBC;
            }
            if (value != "ChainingModeCFB")
            {
                throw new InvalidDataException("Unexpected chaining mode");
            }
            return CipherMode.CFB;
        }

        public static string GetCipherString(CipherMode value)
        {
            if (value == CipherMode.CBC)
            {
                return "ChainingModeCBC";
            }
            if (value != CipherMode.CFB)
            {
                throw new InvalidDataException("Unexpected chaining mode");
            }
            return "ChainingModeCFB";
        }

        public EncryptionSession Load(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            int major = reader.ReadInt16();
            int num2 = reader.ReadInt16();
            if (num2 == 2)
            {
                this.ReadStandardEncryption(major, reader);
            }
            else if (num2 == 3)
            {
                this.ReadExtensibleEncryption(major, reader);
            }
            else
            {
                if (num2 != 4)
                {
                    throw new InvalidDataException("Unknown encryption version.");
                }
                this.ReadAgileEncryption(major, reader);
            }
            return new EncryptionSession(this.primaryCipher, this.passwordKeyEncryptor, this.hmacData);
        }

        private void LoadAgileEncryptionFromXml(XmlReader reader)
        {
            reader.MoveToStartElement(EncryptionDataRootUri, "encryption");
            Func<NameToken, string, bool> action = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<NameToken, string, bool> local1 = <>c.<>9__9_0;
                action = <>c.<>9__9_0 = (nameToken, value) => false;
            }
            reader.ParseAttributes(0, action);
            this.LoadKeyData(reader);
            this.LoadDataIntegrity(reader);
            reader.MoveToStartElement(EncryptionDataRootUri, "keyEncryptors");
            Func<NameToken, string, bool> func2 = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<NameToken, string, bool> local2 = <>c.<>9__9_1;
                func2 = <>c.<>9__9_1 = (nameToken, value) => false;
            }
            reader.ParseAttributes(0, func2);
            this.LoadKeyEncryptor(reader);
            reader.MoveToNextContent();
        }

        private void LoadDataIntegrity(XmlReader reader)
        {
            this.hmacData = new HmacData();
            reader.MoveToStartElement(EncryptionDataRootUri, "dataIntegrity");
            reader.ParseAttributes(2, delegate (NameToken nameToken, string value) {
                if (nameToken == NameToken.encryptedHmacKey)
                {
                    this.hmacData.EncryptedKey = Convert.FromBase64String(value);
                    return true;
                }
                if (nameToken != NameToken.encryptedHmacValue)
                {
                    return false;
                }
                this.hmacData.EncryptedValue = Convert.FromBase64String(value);
                return true;
            });
            reader.MoveToEndElement();
        }

        private void LoadEncryptedKey(XmlReader reader)
        {
            HashInfo hashInfo = new HashInfo();
            CipherInfo cipherInfo = new CipherInfo();
            int spinCount = -1;
            int saltSize = -1;
            byte[] saltValue = null;
            byte[] encryptedHashInput = null;
            byte[] encryptedHashValue = null;
            byte[] encryptedKeyValue = null;
            reader.MoveToStartElement(PasswordKeyEncryptorDataRootUri, "encryptedKey");
            reader.ParseAttributes(12, delegate (NameToken nameToken, string value) {
                switch (nameToken)
                {
                    case NameToken.blockSize:
                        cipherInfo.BlockBits = int.Parse(value) * 8;
                        return true;

                    case NameToken.keyBits:
                        cipherInfo.KeyBits = int.Parse(value);
                        return true;

                    case NameToken.hashSize:
                        hashInfo.HashBits = int.Parse(value) * 8;
                        return true;

                    case NameToken.saltSize:
                        saltSize = int.Parse(value);
                        return true;

                    case NameToken.saltValue:
                        saltValue = Convert.FromBase64String(value);
                        return true;

                    case NameToken.cipherAlgorithm:
                        cipherInfo.Name = value;
                        return true;

                    case NameToken.cipherChaining:
                        cipherInfo.Mode = GetCipherMode(value);
                        return true;

                    case NameToken.hashAlgorithm:
                        hashInfo.Name = value;
                        return true;

                    case NameToken.spinCount:
                        spinCount = int.Parse(value);
                        return true;

                    case NameToken.encryptedVerifierHashInput:
                        encryptedHashInput = Convert.FromBase64String(value);
                        return true;

                    case NameToken.encryptedVerifierHashValue:
                        encryptedHashValue = Convert.FromBase64String(value);
                        return true;

                    case NameToken.encryptedKeyValue:
                        encryptedKeyValue = Convert.FromBase64String(value);
                        return true;
                }
                return false;
            });
            if (saltSize != saltValue.Length)
            {
                throw new InvalidDataException("Invalid salt size/data found");
            }
            if (!reader.IsEmptyElement)
            {
                reader.MoveToNextContent();
            }
            reader.MoveToNextContent();
            this.passwordKeyEncryptor = new AgilePasswordKeyEncryptor(hashInfo, cipherInfo, spinCount, saltValue, encryptedHashInput, encryptedHashValue, encryptedKeyValue);
        }

        private void LoadKeyData(XmlReader reader)
        {
            HashInfo hashInfo = new HashInfo();
            CipherInfo cipherInfo = new CipherInfo();
            byte[] saltValue = null;
            int saltSize = -1;
            reader.MoveToStartElement(EncryptionDataRootUri, "keyData");
            reader.ParseAttributes(8, delegate (NameToken nameToken, string value) {
                switch (nameToken)
                {
                    case NameToken.blockSize:
                        cipherInfo.BlockBits = int.Parse(value) * 8;
                        return true;

                    case NameToken.keyBits:
                        cipherInfo.KeyBits = int.Parse(value);
                        return true;

                    case NameToken.hashSize:
                        hashInfo.HashBits = int.Parse(value) * 8;
                        return true;

                    case NameToken.saltSize:
                        saltSize = int.Parse(value);
                        return true;

                    case NameToken.saltValue:
                        saltValue = Convert.FromBase64String(value);
                        return true;

                    case NameToken.cipherAlgorithm:
                        cipherInfo.Name = value;
                        return true;

                    case NameToken.cipherChaining:
                        cipherInfo.Mode = GetCipherMode(value);
                        return true;

                    case NameToken.hashAlgorithm:
                        hashInfo.Name = value;
                        return true;
                }
                return false;
            });
            if (saltSize != saltValue.Length)
            {
                throw new InvalidDataException("Invalid salt size/data found");
            }
            reader.MoveToEndElement();
            this.primaryCipher = new PrimaryCipher(hashInfo, cipherInfo, saltValue);
        }

        private void LoadKeyEncryptor(XmlReader reader)
        {
            reader.MoveToStartElement(EncryptionDataRootUri, "keyEncryptor");
            while (true)
            {
                string encryptorUri = string.Empty;
                reader.ParseAttributes(1, delegate (NameToken nameToken, string value) {
                    if (nameToken != NameToken.uri)
                    {
                        return false;
                    }
                    encryptorUri = value;
                    return true;
                });
                if (encryptorUri.Length == 0)
                {
                    throw new InvalidDataException("Found empty Uri");
                }
                if (encryptorUri == PasswordKeyEncryptorDataRootUri)
                {
                    this.LoadEncryptedKey(reader);
                }
                else
                {
                    string namespaceURI = reader.NamespaceURI;
                    do
                    {
                        reader.MoveToNextContent();
                    }
                    while ((reader.NodeType != XmlNodeType.EndElement) || ((reader.Name != "keyEncryptor") || (reader.NamespaceURI != namespaceURI)));
                }
                reader.MoveToNextContent();
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    return;
                }
            }
        }

        private void ReadAgileEncryption(int major, BinaryReader reader)
        {
            if (major != 4)
            {
                throw new InvalidDataException("Invalid version of the encrypted file.");
            }
            int num = reader.ReadInt32();
            using (XmlReader reader2 = XmlReader.Create(reader.BaseStream))
            {
                this.LoadAgileEncryptionFromXml(reader2);
            }
        }

        private void ReadExtensibleEncryption(int major, BinaryReader reader)
        {
            if ((major != 3) && (major != 4))
            {
                throw new InvalidDataException("Invalid version of the encrypted file.");
            }
            throw new InvalidDataException("Extensible encryption is not supported.");
        }

        private void ReadStandardEncryption(int major, BinaryReader reader)
        {
            if ((major < 2) || (major > 4))
            {
                throw new InvalidDataException("Invalid version of the encrypted file.");
            }
            StandardEncryptionHeader header = new StandardEncryptionHeader();
            header.Read(reader);
            if (((((header.ExternalEncryption || (!header.CryptoAPI || (!header.AES || (header.DocumentPropertiesEncrypted || ((header.AlgorithmIdHash != 0x8004) || ((header.ProviderType != 0x18) || ((header.CSPName != "Microsoft Enhanced RSA and AES Cryptographic Provider") && (header.CSPName != "Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)")))))))) || (header.AlgorithmId != 0x660e)) || (header.KeySize != 0x80)) && ((header.AlgorithmId != 0x660f) || (header.KeySize != 0xc0))) && ((header.AlgorithmId != 0x6610) || (header.KeySize != 0x100)))
            {
                throw new InvalidDataException("Encrypted file properties aren't match with Standard Encryption properties");
            }
            StandardEncryptionVerifier verifier = new StandardEncryptionVerifier();
            verifier.Read(reader);
            HashInfo hashInfo = new HashInfo {
                HashBits = verifier.VerifierHashSize * 8,
                Name = "SHA1"
            };
            CipherInfo cipherInfo = new CipherInfo {
                BlockBits = 0x80,
                KeyBits = header.KeySize,
                Mode = CipherMode.ECB,
                Name = "AES"
            };
            byte[] salt = verifier.Salt;
            this.passwordKeyEncryptor = new StandardPasswordKeyEncryptor(hashInfo, cipherInfo, verifier.Salt, verifier.EncryptedVerifier, verifier.EncryptedVerifierHash);
            this.primaryCipher = new PrimaryCipher(hashInfo, cipherInfo, verifier.Salt);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EncryptionSessionImporter.<>c <>9 = new EncryptionSessionImporter.<>c();
            public static Func<NameToken, string, bool> <>9__9_0;
            public static Func<NameToken, string, bool> <>9__9_1;

            internal bool <LoadAgileEncryptionFromXml>b__9_0(NameToken nameToken, string value) => 
                false;

            internal bool <LoadAgileEncryptionFromXml>b__9_1(NameToken nameToken, string value) => 
                false;
        }
    }
}

