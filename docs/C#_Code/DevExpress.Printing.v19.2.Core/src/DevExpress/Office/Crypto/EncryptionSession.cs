namespace DevExpress.Office.Crypto
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class EncryptionSession : IEncryptionSession
    {
        public const string MagicMSPassword = "VelvetSweatshop";
        private IPasswordKeyEncryptor passwordKeyEncryptor;
        private DevExpress.Office.Crypto.PrimaryCipher primaryCipher;
        private DevExpress.Office.Crypto.HmacData hmacData;

        public EncryptionSession(DevExpress.Office.Crypto.PrimaryCipher primaryCipher, IPasswordKeyEncryptor passwordKeyEncryptor)
        {
            this.passwordKeyEncryptor = passwordKeyEncryptor;
            this.primaryCipher = primaryCipher;
        }

        public EncryptionSession(DevExpress.Office.Crypto.PrimaryCipher primaryCipher, IPasswordKeyEncryptor passwordKeyEncryptor, DevExpress.Office.Crypto.HmacData hmacData) : this(primaryCipher, passwordKeyEncryptor)
        {
            this.hmacData = hmacData;
        }

        internal Stream BeginSession(string password, Stream encryptionInfoStream, Stream encryptedPackageStream)
        {
            this.Lock(password);
            return new EncryptedStream(this.primaryCipher, encryptedPackageStream);
        }

        public static EncryptionSession CreateOpenXmlSession(EncryptionOptions options) => 
            EncryptionSessionFactory.CreateOpenXmlSession(options);

        internal void EndSession(Stream encryptionInfoStream, Stream encryptedPackageStream)
        {
            if (this.passwordKeyEncryptor.SupportsIntegrityCheck)
            {
                this.hmacData = Hmac.GetHmac(this.primaryCipher, this.primaryCipher.HashInfo, encryptedPackageStream);
            }
            new EncryptionSessionExporter(this).Export(encryptionInfoStream);
        }

        public EncryptionSessionError GetDecryptedStream(ref string password, Stream encryptedPackageStream, out Stream decryptedStream)
        {
            decryptedStream = null;
            EncryptionSessionError integrityCheckFailed = this.Unlock(ref password);
            if (integrityCheckFailed == EncryptionSessionError.None)
            {
                if (this.passwordKeyEncryptor.SupportsIntegrityCheck && ((this.hmacData == null) || !Hmac.CheckStream(this.primaryCipher, this.primaryCipher.HashInfo, this.hmacData, encryptedPackageStream)))
                {
                    integrityCheckFailed = EncryptionSessionError.IntegrityCheckFailed;
                }
                decryptedStream = new EncryptedStream(this.primaryCipher, encryptedPackageStream);
            }
            return integrityCheckFailed;
        }

        public static EncryptionSession Load(Stream stream) => 
            new EncryptionSessionImporter().Load(stream);

        private void Lock(string password)
        {
            if (!this.primaryCipher.Locked)
            {
                throw new InvalidOperationException("Already locked.");
            }
            byte[] buffer = this.passwordKeyEncryptor.Lock(password, this.primaryCipher.CipherInfo.KeyBits);
            this.primaryCipher.SetSecretKey(buffer);
        }

        public void Save(string password, Stream encryptionInfoStream, Stream encryptedPackageStream, Action<Stream> exportData)
        {
            this.Lock(password);
            Stream stream = new EncryptedStream(this.primaryCipher, encryptedPackageStream);
            exportData(stream);
            if (this.passwordKeyEncryptor.SupportsIntegrityCheck)
            {
                this.hmacData = Hmac.GetHmac(this.primaryCipher, this.primaryCipher.HashInfo, encryptedPackageStream);
            }
            new EncryptionSessionExporter(this).Export(encryptionInfoStream);
        }

        private EncryptionSessionError Unlock(ref string password)
        {
            if (!this.primaryCipher.Locked)
            {
                throw new InvalidOperationException("Already unlocked.");
            }
            byte[] buffer = null;
            if (!string.IsNullOrEmpty(password))
            {
                buffer = this.passwordKeyEncryptor.Unlock(password);
            }
            if (buffer == null)
            {
                buffer = this.passwordKeyEncryptor.Unlock("VelvetSweatshop");
                if (buffer == null)
                {
                    return (!string.IsNullOrEmpty(password) ? EncryptionSessionError.WrongPassword : EncryptionSessionError.PasswordRequired);
                }
                password = "VelvetSweatshop";
            }
            this.primaryCipher.SetSecretKey(buffer);
            return EncryptionSessionError.None;
        }

        public IPasswordKeyEncryptor PasswordKeyEncryptor =>
            this.passwordKeyEncryptor;

        public IPrimaryCipher PrimaryCipher =>
            this.primaryCipher;

        public DevExpress.Office.Crypto.HmacData HmacData =>
            this.hmacData;
    }
}

