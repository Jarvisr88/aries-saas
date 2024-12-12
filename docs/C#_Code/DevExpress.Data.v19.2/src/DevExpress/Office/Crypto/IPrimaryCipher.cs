namespace DevExpress.Office.Crypto
{
    using System;

    public interface IPrimaryCipher
    {
        DevExpress.Office.Crypto.HashInfo HashInfo { get; }

        DevExpress.Office.Crypto.CipherInfo CipherInfo { get; }

        byte[] SaltValue { get; }
    }
}

