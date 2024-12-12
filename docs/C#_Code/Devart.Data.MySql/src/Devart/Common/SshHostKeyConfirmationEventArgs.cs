namespace Devart.Common
{
    using System;

    public class SshHostKeyConfirmationEventArgs : EventArgs
    {
        private readonly string a;
        private readonly string b;
        private readonly int c;
        private readonly string d;
        private readonly string e;
        private readonly string f;
        private SshHostKeyConfirmation g;

        internal SshHostKeyConfirmationEventArgs(string A_0, string A_1, int A_2, string A_3, string A_4, string A_5, SshHostKeyConfirmation A_6);

        public string Warning { get; }

        public string Host { get; }

        public int Port { get; }

        public string KeyType { get; }

        public string Md5Fingerprint { get; }

        public string Sha1Fingerprint { get; }

        public SshHostKeyConfirmation Confirmation { get; set; }
    }
}

