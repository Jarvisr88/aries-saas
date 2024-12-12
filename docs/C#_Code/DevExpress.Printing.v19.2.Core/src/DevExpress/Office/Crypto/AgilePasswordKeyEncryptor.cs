namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    public class AgilePasswordKeyEncryptor : IAgilePasswordKeyEncryptor, IPasswordKeyEncryptor
    {
        private static readonly byte[] hashInputBlockKey = new byte[] { 0xfe, 0xa7, 210, 0x76, 0x3b, 0x4b, 0x9e, 0x79 };
        private static readonly byte[] hashValueBlockKey = new byte[] { 0xd7, 170, 15, 0x6d, 0x30, 0x61, 0x34, 0x4e };
        private static readonly byte[] secretKeyBlockKey = new byte[] { 20, 110, 11, 0xe7, 0xab, 0xac, 0xd0, 0xd6 };
        private readonly PasswordBasedKey passwordEncryptor;

        public AgilePasswordKeyEncryptor(HashInfo hashInfo, CipherInfo cipherInfo, int spinCount, byte[] saltValue)
        {
            this.passwordEncryptor = new PasswordBasedKey(hashInfo, cipherInfo, spinCount, saltValue);
        }

        public AgilePasswordKeyEncryptor(HashInfo hashInfo, CipherInfo cipherInfo, int spinCount, byte[] saltValue, byte[] encryptedVerifierHashInput, byte[] encryptedVerifierHashValue, byte[] encryptedKeyValue) : this(hashInfo, cipherInfo, spinCount, saltValue)
        {
            this.EncryptedVerifierHashInput = encryptedVerifierHashInput;
            this.EncryptedVerifierHashValue = encryptedVerifierHashValue;
            this.EncryptedKeyValue = encryptedKeyValue;
        }

        public void Accept(IPasswordKeyEncryptorVisitor visitor)
        {
            visitor.Visit(this);
        }

        private byte[] CreateEncryptedHashInput(string password)
        {
            byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(DevExpress.Office.Crypto.Utils.RoundUp(this.passwordEncryptor.SaltValue.Length, this.passwordEncryptor.BlockBytes));
            using (ICryptoTransform transform = this.passwordEncryptor.GetEncryptor(hashValueBlockKey))
            {
                return transform.TransformWithChecks(randomBytes);
            }
        }

        private byte[] CreateVerifier(byte[] encryptedHashInput)
        {
            byte[] buffer;
            byte[] buffer2;
            using (ICryptoTransform transform = this.passwordEncryptor.GetDecryptor(hashInputBlockKey))
            {
                buffer = transform.TransformWithChecks(encryptedHashInput);
            }
            using (HashAlgorithm algorithm = this.passwordEncryptor.GetHashAlgorithm())
            {
                int num = algorithm.HashSize / 8;
                buffer2 = new byte[DevExpress.Office.Crypto.Utils.RoundUp(num, this.passwordEncryptor.BlockBytes)];
                Buffer.BlockCopy(algorithm.ComputeHash(buffer), 0, buffer2, 0, num);
            }
            using (ICryptoTransform transform2 = this.passwordEncryptor.GetEncryptor(hashValueBlockKey))
            {
                return transform2.TransformWithChecks(buffer2);
            }
        }

        public byte[] Lock(string password, int secretKeySize)
        {
            this.passwordEncryptor.SetPassword(password);
            this.EncryptedVerifierHashInput = this.CreateEncryptedHashInput(password);
            this.EncryptedVerifierHashValue = this.CreateVerifier(this.EncryptedVerifierHashInput);
            byte[] randomBytes = DevExpress.Office.Crypto.Utils.GetRandomBytes(secretKeySize / 8);
            using (ICryptoTransform transform = this.passwordEncryptor.GetEncryptor(secretKeyBlockKey))
            {
                this.EncryptedKeyValue = transform.TransformWithChecks(randomBytes);
            }
            return randomBytes;
        }

        public byte[] Unlock(string password)
        {
            if ((this.EncryptedVerifierHashInput == null) || (this.EncryptedVerifierHashValue == null))
            {
                throw new InvalidDataException("No encrypted verifier");
            }
            if (this.EncryptedKeyValue == null)
            {
                throw new InvalidDataException("No encrypted secret key");
            }
            this.passwordEncryptor.SetPassword(password);
            if (!this.CreateVerifier(this.EncryptedVerifierHashInput).EqualBytes(this.EncryptedVerifierHashValue))
            {
                return null;
            }
            using (ICryptoTransform transform = this.passwordEncryptor.GetDecryptor(secretKeyBlockKey))
            {
                return transform.TransformWithChecks(this.EncryptedKeyValue);
            }
        }

        public byte[] EncryptedVerifierHashInput { get; private set; }

        public byte[] EncryptedVerifierHashValue { get; private set; }

        public byte[] EncryptedKeyValue { get; private set; }

        public IPasswordBasedKey PasswordEncryptor =>
            this.passwordEncryptor;

        public bool SupportsIntegrityCheck =>
            true;
    }
}

