namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public class HashInfo
    {
        public HashInfo Clone() => 
            (HashInfo) base.MemberwiseClone();

        public HashAlgorithm GetAlgorithm()
        {
            string name = this.Name;
            HashAlgorithm algorithm = (name == "SHA256") ? ((HashAlgorithm) new SHA256CryptoServiceProvider()) : ((name == "SHA384") ? ((HashAlgorithm) new SHA384CryptoServiceProvider()) : ((name == "SHA512") ? ((HashAlgorithm) new SHA512CryptoServiceProvider()) : HashAlgorithm.Create(this.Name)));
            if (algorithm.HashSize != this.HashBits)
            {
                throw new InvalidDataException("Unexpected hash size");
            }
            algorithm.Initialize();
            return algorithm;
        }

        public int HashBits { get; set; }

        public string Name { get; set; }
    }
}

