namespace DevExpress.Office.Crypto
{
    using System;

    public interface IPasswordKeyEncryptorVisitor
    {
        void Visit(IAgilePasswordKeyEncryptor encryptor);
        void Visit(IStandardPasswordKeyEncryptor encryptor);
    }
}

