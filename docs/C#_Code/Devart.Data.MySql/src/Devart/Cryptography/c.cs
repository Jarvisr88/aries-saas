namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal class c : SymmetricAlgorithm
    {
        private readonly byte[] a;
        private readonly SymmetricAlgorithm b;

        public c(SymmetricAlgorithm A_0, byte[] A_1);
        public override ICryptoTransform a(byte[] A_0, byte[] A_1);
        public override ICryptoTransform b(byte[] A_0, byte[] A_1);
        public override void g();
        public override void i();

        public override byte[] System.Security.Cryptography.SymmetricAlgorithm.Key { get; set; }

        public override byte[] System.Security.Cryptography.SymmetricAlgorithm.IV { get; set; }

        public override CipherMode System.Security.Cryptography.SymmetricAlgorithm.Mode { get; set; }

        public override PaddingMode System.Security.Cryptography.SymmetricAlgorithm.Padding { get; set; }

        public override int System.Security.Cryptography.SymmetricAlgorithm.KeySize { get; set; }

        public override int System.Security.Cryptography.SymmetricAlgorithm.BlockSize { get; set; }

        public override KeySizes[] System.Security.Cryptography.SymmetricAlgorithm.LegalKeySizes { get; }

        public override KeySizes[] System.Security.Cryptography.SymmetricAlgorithm.LegalBlockSizes { get; }

        public override int System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize { get; set; }
    }
}

