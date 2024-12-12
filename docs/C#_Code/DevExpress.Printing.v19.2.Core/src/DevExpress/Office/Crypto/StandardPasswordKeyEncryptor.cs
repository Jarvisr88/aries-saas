namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public class StandardPasswordKeyEncryptor : IStandardPasswordKeyEncryptor, IPasswordKeyEncryptor
    {
        private const int spinCount = 0xc350;
        private readonly DevExpress.Office.Crypto.HashInfo hashInfo;
        private readonly DevExpress.Office.Crypto.CipherInfo cipherInfo;
        private readonly byte[] saltValue;

        public StandardPasswordKeyEncryptor(DevExpress.Office.Crypto.HashInfo hashInfo, DevExpress.Office.Crypto.CipherInfo cipherInfo, byte[] saltValue)
        {
            this.hashInfo = hashInfo;
            this.cipherInfo = cipherInfo;
            this.saltValue = saltValue;
        }

        public StandardPasswordKeyEncryptor(DevExpress.Office.Crypto.HashInfo hashInfo, DevExpress.Office.Crypto.CipherInfo cipherInfo, byte[] saltValue, byte[] encryptedVerifier, byte[] encryptedVerifierHash) : this(hashInfo, cipherInfo, saltValue)
        {
            this.EncryptedVerifier = encryptedVerifier;
            this.EncryptedVerifierHash = encryptedVerifierHash;
        }

        public void Accept(IPasswordKeyEncryptorVisitor visitor)
        {
            visitor.Visit(this);
        }

        private byte[] DeriveKey(byte[] encryptionKey)
        {
            int countBytesHash = this.hashInfo.HashBits / 8;
            byte[] sourceArray = this.PrepareBuffer(encryptionKey, countBytesHash, 0x5c);
            byte[] destinationArray = new byte[countBytesHash * 2];
            Array.Copy(this.PrepareBuffer(encryptionKey, countBytesHash, 0x36), 0, destinationArray, 0, countBytesHash);
            Array.Copy(sourceArray, 0, destinationArray, countBytesHash, countBytesHash);
            return destinationArray.CloneToFit((this.cipherInfo.KeyBits / 8));
        }

        private byte[] GetEncryptionKey(string password)
        {
            byte[] buffer3;
            using (HashAlgorithm algorithm = this.hashInfo.GetAlgorithm())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(password);
                byte[] first = algorithm.Transform2Blocks(this.saltValue, bytes);
                int num = 0;
                while (true)
                {
                    if (num >= 0xc350)
                    {
                        buffer3 = algorithm.Transform2Blocks(first, BitConverter.GetBytes(0));
                        break;
                    }
                    first = algorithm.Transform2Blocks(BitConverter.GetBytes(num), first);
                    num++;
                }
            }
            return buffer3;
        }

        public byte[] Lock(string password, int secretKeySize)
        {
            this.EncryptedVerifier = DevExpress.Office.Crypto.Utils.GetRandomBytes(0x10);
            using (HashAlgorithm algorithm = this.hashInfo.GetAlgorithm())
            {
                this.EncryptedVerifierHash = algorithm.ComputeHash(this.EncryptedVerifier);
            }
            this.EncryptedVerifierHash = this.EncryptedVerifierHash.CloneToFit(DevExpress.Office.Crypto.Utils.RoundUp(this.EncryptedVerifierHash.Length, this.cipherInfo.BlockBits / 8), 0);
            byte[] encryptionKey = this.GetEncryptionKey(password);
            encryptionKey = this.DeriveKey(encryptionKey);
            using (SymmetricAlgorithm algorithm2 = this.cipherInfo.GetAlgorithm())
            {
                algorithm2.Key = encryptionKey;
                using (ICryptoTransform transform = algorithm2.CreateEncryptor())
                {
                    this.EncryptedVerifier = transform.TransformWithChecks(this.EncryptedVerifier);
                    this.EncryptedVerifierHash = transform.TransformWithChecks(this.EncryptedVerifierHash);
                }
            }
            return encryptionKey;
        }

        private unsafe byte[] PrepareBuffer(byte[] passwordHash, int countBytesHash, byte defaultValue)
        {
            byte[] buffer = new byte[0x40];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = defaultValue;
            }
            for (int j = 0; j < countBytesHash; j++)
            {
                byte* numPtr1 = &(buffer[j]);
                numPtr1[0] = (byte) (numPtr1[0] ^ passwordHash[j]);
            }
            using (HashAlgorithm algorithm = this.hashInfo.GetAlgorithm())
            {
                return algorithm.ComputeHash(buffer);
            }
        }

        public byte[] Unlock(string password)
        {
            if ((this.EncryptedVerifier == null) || (this.EncryptedVerifierHash == null))
            {
                throw new InvalidDataException("No encrypted verifier");
            }
            byte[] encryptionKey = this.GetEncryptionKey(password);
            encryptionKey = this.DeriveKey(encryptionKey);
            return (this.Verify(encryptionKey) ? encryptionKey : null);
        }

        private bool Verify(byte[] encryptionKey)
        {
            byte[] buffer;
            byte[] buffer2;
            byte[] buffer3;
            using (SymmetricAlgorithm algorithm = this.cipherInfo.GetAlgorithm())
            {
                algorithm.Key = encryptionKey;
                using (ICryptoTransform transform = algorithm.CreateDecryptor())
                {
                    buffer = transform.TransformWithChecks(this.EncryptedVerifier);
                    buffer2 = transform.TransformWithChecks(this.EncryptedVerifierHash);
                }
            }
            using (HashAlgorithm algorithm2 = this.hashInfo.GetAlgorithm())
            {
                buffer3 = algorithm2.ComputeHash(buffer);
            }
            return buffer3.EqualBytes(buffer2.CloneToFit((this.hashInfo.HashBits / 8), 0));
        }

        public DevExpress.Office.Crypto.HashInfo HashInfo =>
            this.hashInfo;

        public DevExpress.Office.Crypto.CipherInfo CipherInfo =>
            this.cipherInfo;

        public byte[] SaltValue =>
            this.saltValue;

        public byte[] EncryptedVerifier { get; private set; }

        public byte[] EncryptedVerifierHash { get; private set; }

        public bool SupportsIntegrityCheck =>
            false;
    }
}

