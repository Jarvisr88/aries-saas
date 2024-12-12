namespace DevExpress.Office.Crypto
{
    using System;

    public interface IPasswordBasedKey
    {
        DevExpress.Office.Crypto.HashInfo HashInfo { get; }

        DevExpress.Office.Crypto.CipherInfo CipherInfo { get; }

        int SpinCount { get; }

        byte[] SaltValue { get; }
    }
}

