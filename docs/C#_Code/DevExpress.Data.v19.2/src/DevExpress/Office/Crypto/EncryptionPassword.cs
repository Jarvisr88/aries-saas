namespace DevExpress.Office.Crypto
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public sealed class EncryptionPassword : IDisposable
    {
        private SecureString innerPassword;

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && (this.innerPassword != null))
            {
                this.innerPassword.Dispose();
                this.innerPassword = null;
            }
        }

        [SecuritySafeCritical]
        private string FromSecureString(SecureString password)
        {
            if (password == null)
            {
                return null;
            }
            int length = password.Length;
            short[] destination = new short[length];
            IntPtr source = Marshal.SecureStringToGlobalAllocUnicode(password);
            try
            {
                Marshal.Copy(source, destination, 0, length);
            }
            finally
            {
                Marshal.ZeroFreeCoTaskMemUnicode(source);
            }
            StringBuilder builder = new StringBuilder();
            foreach (short num3 in destination)
            {
                builder.Append((char) ((ushort) num3));
            }
            return builder.ToString();
        }

        private SecureString ToSecureString(string str)
        {
            if (str == null)
            {
                return null;
            }
            SecureString str2 = new SecureString();
            foreach (char ch in str)
            {
                str2.AppendChar(ch);
            }
            return str2;
        }

        public string Password
        {
            get => 
                this.FromSecureString(this.innerPassword);
            set
            {
                if (this.innerPassword == null)
                {
                    SecureString innerPassword = this.innerPassword;
                }
                else
                {
                    this.innerPassword.Dispose();
                }
                this.innerPassword = this.ToSecureString(value);
            }
        }
    }
}

