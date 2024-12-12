namespace Devart.Data.MySql
{
    using Devart.Common;
    using Devart.Security.Ssh;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(Devart.Data.MySql.g))]
    public sealed class SshOptions
    {
        private int a;
        private string b;
        private string c;
        private string d;
        private string e;
        private string f;
        private string g;
        private SshAuthenticationType h;
        private string i;
        private bool j;
        internal const int l = 0x16;

        internal event bq PropertyChanged;

        public SshOptions();
        public SshOptions(string user, string password);
        public SshOptions(string host, string user, string password);
        public SshOptions(string host, int port, string user, string password);
        public SshOptions(string host, int port, string user, string password, string cipherList);
        public SshOptions(string host, int port, string user, string password, string cipherList, string privateKey);
        public SshOptions(string host, int port, string user, string password, string cipherList, string privateKey, string passphrase);
        public SshOptions(string host, int port, string user, string password, string cipherList, string privateKey, string passphrase, SshAuthenticationType authenticationType);
        public SshOptions(string host, int port, string user, string password, string cipherList, string privateKey, string passphrase, SshAuthenticationType authenticationType, string hostKeys);
        public SshOptions(string host, int port, string user, string password, string cipherList, string privateKey, string passphrase, SshAuthenticationType authenticationType, string hostKeys, bool strictHostKeyChecking);
        internal w a();
        private void a(string A_0, object A_1);
        internal bool ShouldSerialize();
        public override string ToString();

        [Browsable(true), DefaultValue(""), aa("SshOptions_Host"), RefreshProperties(RefreshProperties.Repaint)]
        public string Host { get; set; }

        [aa("SshOptions_Port"), Browsable(true), DefaultValue(0x16), RefreshProperties(RefreshProperties.Repaint)]
        public int Port { get; set; }

        [Browsable(true), DefaultValue(""), aa("SshOptions_User"), RefreshProperties(RefreshProperties.Repaint)]
        public string User { get; set; }

        [DefaultValue(""), aa("SshOptions_Password"), RefreshProperties(RefreshProperties.Repaint), PasswordPropertyText(true), Browsable(true)]
        public string Password { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), aa("SshOptions_CipherList"), DefaultValue(""), Browsable(true)]
        public string CipherList { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.SshOptionPrivateKeyEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("SshOptions_PrivateKey"), MergableProperty(false), DefaultValue(""), Browsable(true)]
        public string PrivateKey { get; set; }

        [PasswordPropertyText(true), DefaultValue(""), Browsable(true), RefreshProperties(RefreshProperties.Repaint), aa("SshOptions_Passphrase")]
        public string Passphrase { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue(1), aa("SshOptions_AuthenticationType"), Browsable(true)]
        public SshAuthenticationType AuthenticationType { get; set; }

        [DefaultValue(""), MergableProperty(false), RefreshProperties(RefreshProperties.Repaint), Browsable(true), aa("SshOptions_HostKey"), Editor("Devart.Data.MySql.Design.SshOptionHostKeyKeyEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string HostKey { get; set; }

        [Browsable(true), aa("SshOptions_Strict_Host_Key_Checking"), DefaultValue(""), RefreshProperties(RefreshProperties.Repaint)]
        public bool StrictHostKeyChecking { get; set; }
    }
}

