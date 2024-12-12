namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using System;
    using System.Security.Cryptography;

    public class PrimaryCipher : IPrimaryCipher, ICipherProvider
    {
        private DevExpress.Office.Crypto.HashInfo hashInfo;
        private DevExpress.Office.Crypto.CipherInfo cipherInfo;
        private byte[] secretKey;
        private byte[] saltValue;

        public PrimaryCipher(DevExpress.Office.Crypto.HashInfo hashInfo, DevExpress.Office.Crypto.CipherInfo cipherInfo, byte[] saltValue)
        {
            this.hashInfo = hashInfo;
            this.cipherInfo = cipherInfo;
            this.saltValue = (byte[]) saltValue.Clone();
        }

        public ICryptoTransform GetCryptoTransform(byte[] blockKey, bool isEncryption)
        {
            byte[] buffer;
            using (HashAlgorithm algorithm = this.hashInfo.GetAlgorithm())
            {
                buffer = algorithm.Transform2Blocks(this.saltValue, blockKey).CloneToFit(this.BlockBytes);
            }
            using (SymmetricAlgorithm algorithm2 = this.cipherInfo.GetAlgorithm())
            {
                return (!isEncryption ? algorithm2.CreateDecryptor(this.secretKey, buffer) : algorithm2.CreateEncryptor(this.secretKey, buffer));
            }
        }

        public void SetSecretKey(byte[] value)
        {
            this.secretKey = value;
        }

        public bool Locked =>
            this.secretKey == null;

        public int BlockBytes =>
            this.cipherInfo.BlockBits / 8;

        public DevExpress.Office.Crypto.HashInfo HashInfo =>
            this.hashInfo;

        public DevExpress.Office.Crypto.CipherInfo CipherInfo =>
            this.cipherInfo;

        public byte[] SaltValue =>
            this.saltValue;
    }
}

