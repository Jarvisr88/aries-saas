namespace DevExpress.Office.Crypto
{
    public interface IEncryptionSession
    {
        IPasswordKeyEncryptor PasswordKeyEncryptor { get; }

        IPrimaryCipher PrimaryCipher { get; }
    }
}

