namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class ARC4KeyGen : IKeyGen
    {
        private const int saltLength = 0x10;
        private const int truncatedHashLength = 5;
        private const int blockLength = 0x15;
        private byte[] salt;
        private byte[] truncatedHash;

        public ARC4KeyGen(string password)
        {
            this.salt = new byte[0x10];
            this.truncatedHash = new byte[5];
            Guard.ArgumentNotNull(password, "password");
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(this.salt);
            }
            this.CalcTruncatedHash(password);
        }

        public ARC4KeyGen(string password, byte[] salt)
        {
            this.salt = new byte[0x10];
            this.truncatedHash = new byte[5];
            Guard.ArgumentNotNull(password, "password");
            Guard.ArgumentNotNull(salt, "salt");
            if (salt.Length != 0x10)
            {
                throw new ArgumentException("Salt must be 16 bytes long");
            }
            Array.Copy(salt, this.salt, 0x10);
            this.CalcTruncatedHash(password);
        }

        private void CalcTruncatedHash(string password)
        {
            byte[] sourceArray = MD5Hash.ComputeHash(Encoding.Unicode.GetBytes(password));
            byte[] destinationArray = new byte[0x150];
            for (int i = 0; i < 0x10; i++)
            {
                int destinationIndex = i * 0x15;
                Array.Copy(sourceArray, 0, destinationArray, destinationIndex, 5);
                Array.Copy(this.salt, 0, destinationArray, destinationIndex + 5, 0x10);
            }
            Array.Copy(MD5Hash.ComputeHash(destinationArray), this.truncatedHash, 5);
        }

        public byte[] DeriveKey(int blockNumber)
        {
            byte[] destinationArray = new byte[9];
            Array.Copy(this.truncatedHash, destinationArray, 5);
            Array.Copy(BitConverter.GetBytes(blockNumber), 0, destinationArray, 5, 4);
            return MD5Hash.ComputeHash(destinationArray);
        }

        public byte[] Salt =>
            this.salt;
    }
}

