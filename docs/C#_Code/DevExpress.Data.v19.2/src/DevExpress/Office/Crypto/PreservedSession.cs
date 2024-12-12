namespace DevExpress.Office.Crypto
{
    using System;
    using System.Runtime.CompilerServices;

    public class PreservedSession
    {
        public PreservedEncryptionType Type { get; set; }

        public HashInfo PrimaryHash { get; set; }

        public HashInfo EncryptorHash { get; set; }

        public CipherInfo PrimaryCipher { get; set; }

        public CipherInfo EncryptorCipher { get; set; }

        public int SpinCount { get; set; }
    }
}

