namespace Devart.Cryptography
{
    using System;
    using System.Security.Cryptography;

    internal abstract class h : SymmetricAlgorithm
    {
        private RNGCryptoServiceProvider a;

        public h();
        public static Devart.Cryptography.h a();
        public override void g();
        public override void i();
        protected RNGCryptoServiceProvider j();

        public override int System.Security.Cryptography.SymmetricAlgorithm.BlockSize { get; set; }

        public override int System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize { get; set; }

        public override byte[] System.Security.Cryptography.SymmetricAlgorithm.IV { get; set; }

        public override KeySizes[] System.Security.Cryptography.SymmetricAlgorithm.LegalBlockSizes { get; }

        public override KeySizes[] System.Security.Cryptography.SymmetricAlgorithm.LegalKeySizes { get; }

        public override CipherMode System.Security.Cryptography.SymmetricAlgorithm.Mode { get; set; }

        public override PaddingMode System.Security.Cryptography.SymmetricAlgorithm.Padding { get; set; }
    }
}

