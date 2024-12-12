namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;

    internal class GetPasswordState
    {
        private readonly Func<string, int, SecureString> getPasswordHandler;

        public GetPasswordState(Func<string, int, SecureString> getPasswordHandler)
        {
            this.getPasswordHandler = getPasswordHandler;
        }

        public SecureString GetPassword(string path, int number)
        {
            this.Password = this.getPasswordHandler(path, number);
            return this.Password;
        }

        public SecureString Password { get; private set; }
    }
}

