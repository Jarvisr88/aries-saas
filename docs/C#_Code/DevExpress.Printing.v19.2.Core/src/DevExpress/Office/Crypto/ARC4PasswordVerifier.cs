namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using System;

    public class ARC4PasswordVerifier
    {
        private const int bytesLength = 0x10;
        private byte[] salt;
        private byte[] encryptedVerifier;
        private byte[] encryptedVerifierHash;

        public ARC4PasswordVerifier(ARC4KeyGen keygen)
        {
            this.salt = new byte[0x10];
            this.encryptedVerifier = new byte[0x10];
            this.encryptedVerifierHash = new byte[0x10];
            Guard.ArgumentNotNull(keygen, "keygen");
            Array.Copy(keygen.Salt, this.salt, 0x10);
            byte[] buffer = new byte[0x10];
            new Random().NextBytes(buffer);
            ARC4Cipher cipher = new ARC4Cipher(keygen.DeriveKey(0));
            this.encryptedVerifier = cipher.Encrypt(buffer);
            this.encryptedVerifierHash = cipher.Encrypt(MD5Hash.ComputeHash(buffer));
        }

        public ARC4PasswordVerifier(byte[] salt, byte[] encryptedVerifier, byte[] encryptedVerifierHash)
        {
            this.salt = new byte[0x10];
            this.encryptedVerifier = new byte[0x10];
            this.encryptedVerifierHash = new byte[0x10];
            Guard.ArgumentNotNull(salt, "salt");
            Guard.ArgumentNotNull(encryptedVerifier, "encryptedVerifier");
            Guard.ArgumentNotNull(encryptedVerifierHash, "encryptedVerifierHash");
            Array.Copy(salt, this.salt, 0x10);
            Array.Copy(encryptedVerifier, this.encryptedVerifier, 0x10);
            Array.Copy(encryptedVerifierHash, this.encryptedVerifierHash, 0x10);
        }

        public bool VerifyPassword(string password)
        {
            ARC4Cipher cipher = new ARC4Cipher(new ARC4KeyGen(password, this.salt).DeriveKey(0));
            byte[] buffer2 = cipher.Decrypt(this.encryptedVerifierHash);
            byte[] buffer3 = MD5Hash.ComputeHash(cipher.Decrypt(this.encryptedVerifier));
            for (int i = 0; i < 0x10; i++)
            {
                if (buffer3[i] != buffer2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public byte[] Salt =>
            this.salt;

        public byte[] EncryptedVerifier =>
            this.encryptedVerifier;

        public byte[] EncryptedVerifierHash =>
            this.encryptedVerifierHash;
    }
}

