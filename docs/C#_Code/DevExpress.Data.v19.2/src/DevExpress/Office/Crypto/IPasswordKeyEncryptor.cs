namespace DevExpress.Office.Crypto
{
    using System;

    public interface IPasswordKeyEncryptor
    {
        void Accept(IPasswordKeyEncryptorVisitor visitor);
        byte[] Lock(string password, int secretKeySize);
        byte[] Unlock(string password);

        bool SupportsIntegrityCheck { get; }
    }
}

