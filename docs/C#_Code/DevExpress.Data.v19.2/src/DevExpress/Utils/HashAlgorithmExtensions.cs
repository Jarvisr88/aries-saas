namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public static class HashAlgorithmExtensions
    {
        public static byte[] Transform2Blocks(this HashAlgorithm hashAlgorithm, byte[] first, byte[] second)
        {
            hashAlgorithm.Initialize();
            hashAlgorithm.TransformBlock(first, 0, first.Length, null, 0);
            hashAlgorithm.TransformFinalBlock(second, 0, second.Length);
            return hashAlgorithm.Hash;
        }
    }
}

