namespace DevExpress.ReportServer.Printing
{
    using System;
    using System.Runtime.CompilerServices;

    public class CredentialsEventArgs : EventArgs
    {
        public CredentialsEventArgs()
        {
        }

        public CredentialsEventArgs(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Handled { get; set; }
    }
}

