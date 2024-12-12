namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;

    public class RequestPasswordEventArgs : EventArgs
    {
        public RequestPasswordEventArgs(string path, int tryNumber)
        {
            this.Path = path;
            this.TryNumber = tryNumber;
        }

        public bool Handled { get; set; }

        public SecureString Password { get; set; }

        public int TryNumber { get; private set; }

        public string Path { get; private set; }
    }
}

