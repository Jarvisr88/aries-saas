namespace DevExpress.Office.Crypto
{
    using System;
    using System.Security.Cryptography;

    public interface ICipherProvider
    {
        ICryptoTransform GetCryptoTransform(byte[] blockKey, bool isEncryption);

        int BlockBytes { get; }
    }
}

