namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(ak))]
    public sealed class SslOptions
    {
        private string a;
        private string b;
        private string c;
        private string d;
        private string e;

        internal event av PropertyChanged;

        public SslOptions();
        public SslOptions(string caCert);
        public SslOptions(string caCert, string cert, string key);
        public SslOptions(string cipherList, string caCert, string cert, string key, string tlsProtocol);
        private void a(string A_0, object A_1);
        internal bool ShouldSerialize();
        public override string ToString();

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue(""), Browsable(true), aa("SslOptions_CipherList")]
        public string CipherList { get; set; }

        [MergableProperty(false), Browsable(true), aa("SslOptions_CACert"), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.SslOptionCACertEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), DefaultValue("")]
        public string CACert { get; set; }

        [MergableProperty(false), DefaultValue(""), Browsable(true), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.SslOptionCertEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("SslOptions_Cert")]
        public string Cert { get; set; }

        [DefaultValue(""), MergableProperty(false), aa("SslOptions_Key"), Editor("Devart.Data.MySql.Design.SslOptionKeyEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.Repaint), Browsable(true)]
        public string Key { get; set; }

        [aa("SslOptions_TlsProtocol"), DefaultValue(""), Browsable(true), RefreshProperties(RefreshProperties.Repaint)]
        public string TlsProtocol { get; set; }
    }
}

