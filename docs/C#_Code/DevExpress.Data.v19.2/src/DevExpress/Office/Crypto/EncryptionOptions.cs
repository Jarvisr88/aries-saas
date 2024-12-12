namespace DevExpress.Office.Crypto
{
    using System;
    using System.Runtime.CompilerServices;

    public class EncryptionOptions
    {
        public const ModelEncryptionType DefaultType = ModelEncryptionType.Strong;
        private EncryptionPassword encryptionPassword = new EncryptionPassword();
        private DevExpress.Office.Crypto.PreservedSession preservedSession;

        public EncryptionOptions()
        {
            this.Type = ModelEncryptionType.Strong;
        }

        public void CopyFrom(EncryptionOptions options)
        {
            this.encryptionPassword.Password = options.encryptionPassword.Password;
            this.preservedSession = options.preservedSession;
            this.Type = options.Type;
        }

        ~EncryptionOptions()
        {
            if (this.encryptionPassword == null)
            {
                EncryptionPassword encryptionPassword = this.encryptionPassword;
            }
            else
            {
                this.encryptionPassword.Dispose();
            }
            this.encryptionPassword = null;
        }

        public void PreserveSession(IEncryptionSession session, string password)
        {
            if (session == null)
            {
                this.Type = ModelEncryptionType.Compatible;
            }
            else
            {
                this.preservedSession = new DevExpress.Office.Crypto.PreservedSession();
                this.preservedSession.PrimaryCipher = session.PrimaryCipher.CipherInfo.Clone();
                this.preservedSession.PrimaryHash = session.PrimaryCipher.HashInfo.Clone();
                IAgilePasswordKeyEncryptor passwordKeyEncryptor = session.PasswordKeyEncryptor as IAgilePasswordKeyEncryptor;
                if (passwordKeyEncryptor == null)
                {
                    this.preservedSession.Type = PreservedEncryptionType.Standard;
                    this.preservedSession.EncryptorCipher = this.preservedSession.PrimaryCipher;
                    this.preservedSession.EncryptorHash = this.preservedSession.PrimaryHash;
                    this.preservedSession.SpinCount = 0xc350;
                    this.Type = ModelEncryptionType.Compatible;
                }
                else
                {
                    this.preservedSession.Type = PreservedEncryptionType.Agile;
                    this.preservedSession.EncryptorCipher = passwordKeyEncryptor.PasswordEncryptor.CipherInfo.Clone();
                    this.preservedSession.EncryptorHash = passwordKeyEncryptor.PasswordEncryptor.HashInfo.Clone();
                    this.preservedSession.SpinCount = passwordKeyEncryptor.PasswordEncryptor.SpinCount;
                    this.Type = ModelEncryptionType.Strong;
                }
            }
            this.Password = password;
        }

        public void Reset()
        {
            this.encryptionPassword.Password = null;
            this.preservedSession = null;
            this.Type = ModelEncryptionType.Strong;
        }

        public ModelEncryptionType Type { get; set; }

        public string Password
        {
            get => 
                this.encryptionPassword.Password;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.Reset();
                }
                else
                {
                    if (value.Length > 0xff)
                    {
                        throw new ArgumentException();
                    }
                    this.encryptionPassword.Password = value;
                }
            }
        }

        internal DevExpress.Office.Crypto.PreservedSession PreservedSession =>
            this.preservedSession;
    }
}

