namespace DevExpress.Office.Crypto
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public class CipherInfo
    {
        public CipherInfo Clone() => 
            (CipherInfo) base.MemberwiseClone();

        private SymmetricAlgorithm CreateSymmetricAlgorithm(string name) => 
            SymmetricAlgorithm.Create(name);

        public SymmetricAlgorithm GetAlgorithm()
        {
            SymmetricAlgorithm algorithm = this.CreateSymmetricAlgorithm(this.Name);
            algorithm.BlockSize = this.BlockBits;
            algorithm.KeySize = this.KeyBits;
            algorithm.Padding = PaddingMode.None;
            algorithm.Mode = this.Mode;
            return algorithm;
        }

        public int BlockBits { get; set; }

        public int KeyBits { get; set; }

        public string Name { get; set; }

        public CipherMode Mode { get; set; }
    }
}

