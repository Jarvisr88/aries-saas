namespace DevExpress.Office.Crypto
{
    using System;

    public interface IAgilePasswordKeyEncryptor : IPasswordKeyEncryptor
    {
        IPasswordBasedKey PasswordEncryptor { get; }

        byte[] EncryptedVerifierHashInput { get; }

        byte[] EncryptedVerifierHashValue { get; }

        byte[] EncryptedKeyValue { get; }
    }
}

