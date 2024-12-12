namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Security;

    public class PdfPasswordRequestedEventArgs : EventArgs
    {
        private readonly string filePath;
        private readonly string fileName;
        private readonly int passwordRequestsCount;
        private string password;

        internal PdfPasswordRequestedEventArgs(string filePath, string fileName, int passwordRequestsCount)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.passwordRequestsCount = passwordRequestsCount;
        }

        public string FilePath =>
            this.filePath;

        public string FileName =>
            this.fileName;

        public int PasswordRequestsCount =>
            this.passwordRequestsCount;

        public string PasswordString
        {
            get => 
                this.password;
            set => 
                this.password = value;
        }

        [Obsolete("The Password property is now obsolete. Use the PasswordString property instead.")]
        public SecureString Password
        {
            get => 
                PdfSecureStringAccessor.ToSecureString(this.password);
            set => 
                this.password = PdfSecureStringAccessor.FromSecureString(value);
        }
    }
}

