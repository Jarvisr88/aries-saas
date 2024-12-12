namespace DevExpress.Office.Crypto
{
    using System;
    using System.Security.Cryptography;

    internal static class EncryptionSessionFactory
    {
        public const int SaltSize = 0x10;

        private static EncryptionSession CreateAgileSession()
        {
            CipherInfo cipherInfo = new CipherInfo {
                BlockBits = 0x80,
                KeyBits = 0x100,
                Mode = CipherMode.CBC,
                Name = "AES"
            };
            HashInfo hashInfo = new HashInfo {
                HashBits = 0x200,
                Name = "SHA512"
            };
            return new EncryptionSession(new PrimaryCipher(hashInfo, cipherInfo, DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10)), new AgilePasswordKeyEncryptor(hashInfo, cipherInfo, 0x186a0, DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10)));
        }

        public static EncryptionSession CreateOpenXmlSession(EncryptionOptions options)
        {
            if ((options.PreservedSession == null) || (((options.Type != ModelEncryptionType.Strong) || (options.PreservedSession.Type != PreservedEncryptionType.Agile)) && ((options.Type != ModelEncryptionType.Compatible) || (options.PreservedSession.Type != PreservedEncryptionType.Standard))))
            {
                return ((options.Type != ModelEncryptionType.Compatible) ? CreateAgileSession() : CreateStandardSession());
            }
            return CreateOpenXmlSession(options.PreservedSession);
        }

        private static EncryptionSession CreateOpenXmlSession(PreservedSession preservedSession)
        {
            IPasswordKeyEncryptor encryptor;
            byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10);
            PrimaryCipher primaryCipher = new PrimaryCipher(preservedSession.PrimaryHash, preservedSession.PrimaryCipher, randomBytes);
            if (preservedSession.Type != PreservedEncryptionType.Agile)
            {
                encryptor = new StandardPasswordKeyEncryptor(preservedSession.EncryptorHash, preservedSession.EncryptorCipher, randomBytes);
            }
            else
            {
                randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10);
                encryptor = new AgilePasswordKeyEncryptor(preservedSession.EncryptorHash, preservedSession.EncryptorCipher, preservedSession.SpinCount, randomBytes);
            }
            return new EncryptionSession(primaryCipher, encryptor);
        }

        private static EncryptionSession CreateStandardSession()
        {
            CipherInfo cipherInfo = new CipherInfo {
                BlockBits = 0x80,
                KeyBits = 0x80,
                Mode = CipherMode.ECB,
                Name = "AES"
            };
            HashInfo hashInfo = new HashInfo {
                HashBits = 160,
                Name = "SHA1"
            };
            byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10);
            return new EncryptionSession(new PrimaryCipher(hashInfo, cipherInfo, randomBytes), new StandardPasswordKeyEncryptor(hashInfo, cipherInfo, randomBytes));
        }
    }
}

