namespace DevExpress.Office.Crypto
{
    using System;

    public interface IStandardPasswordKeyEncryptor : IPasswordKeyEncryptor
    {
        DevExpress.Office.Crypto.HashInfo HashInfo { get; }

        DevExpress.Office.Crypto.CipherInfo CipherInfo { get; }

        byte[] SaltValue { get; }

        byte[] EncryptedVerifier { get; }

        byte[] EncryptedVerifierHash { get; }
    }
}

