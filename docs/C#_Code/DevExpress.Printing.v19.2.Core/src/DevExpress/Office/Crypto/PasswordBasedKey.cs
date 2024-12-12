namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class PasswordBasedKey : IPasswordBasedKey, ICipherProvider
    {
        private readonly DevExpress.Office.Crypto.HashInfo hashInfo;
        private readonly DevExpress.Office.Crypto.CipherInfo cipherInfo;
        private readonly int spinCount;
        private readonly byte[] saltValue;
        private byte[] passwordHash;

        public PasswordBasedKey(DevExpress.Office.Crypto.HashInfo hashInfo, DevExpress.Office.Crypto.CipherInfo cipherInfo, int spinCount, byte[] saltValue)
        {
            this.cipherInfo = cipherInfo.Clone();
            this.hashInfo = hashInfo.Clone();
            this.spinCount = spinCount;
            this.saltValue = (byte[]) saltValue.Clone();
        }

        public ICryptoTransform GetCryptoTransform(byte[] blockKey, bool isEncryption)
        {
            ICryptoTransform transform;
            using (HashAlgorithm algorithm = this.GetHashAlgorithm())
            {
                byte[] rgbKey = algorithm.Transform2Blocks(this.passwordHash, blockKey).CloneToFit(this.cipherInfo.KeyBits / 8);
                byte[] rgbIV = this.saltValue.CloneToFit(this.cipherInfo.BlockBits / 8);
                using (SymmetricAlgorithm algorithm2 = this.cipherInfo.GetAlgorithm())
                {
                    transform = !isEncryption ? algorithm2.CreateDecryptor(rgbKey, rgbIV) : algorithm2.CreateEncryptor(rgbKey, rgbIV);
                }
            }
            return transform;
        }

        public HashAlgorithm GetHashAlgorithm() => 
            this.hashInfo.GetAlgorithm();

        public void SetPassword(string password)
        {
            using (HashAlgorithm algorithm = this.GetHashAlgorithm())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] second = algorithm.Transform2Blocks(this.saltValue, bytes);
                int num = 0;
                while (true)
                {
                    if (num >= this.spinCount)
                    {
                        this.passwordHash = second;
                        break;
                    }
                    second = algorithm.Transform2Blocks(BitConverter.GetBytes(num), second);
                    num++;
                }
            }
        }

        public DevExpress.Office.Crypto.HashInfo HashInfo =>
            this.hashInfo;

        public DevExpress.Office.Crypto.CipherInfo CipherInfo =>
            this.cipherInfo;

        public int SpinCount =>
            this.spinCount;

        public byte[] SaltValue =>
            this.saltValue;

        public int BlockBytes =>
            this.cipherInfo.BlockBits / 8;
    }
}

