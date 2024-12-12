namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(af))]
    public class MySqlHttpOptions
    {
        private string a;
        private int b;
        private bool c;
        private int d;
        private int e;
        private int f;
        private string g;
        private string h;
        private string i;
        private bool j;
        public const int DefaultPort = 0x22b8;
        public const int DefaultMaxConnectionAge = 300;
        public const int DefaultContentLength = 0x10000;
        public const bool DefaultStrictContentLength = true;
        public const int DefaultKeepAlive = 1;
        public const bool DefaultBase64 = false;

        internal event ay PropertyChanged;

        internal MySqlHttpOptions();
        public MySqlHttpOptions(string url, string user, string password, string host, int port, bool strictContentLength, int contentLength, int maxConnectionAge, int keepAlive, bool base64);
        private void a(string A_0, object A_1);
        internal bool ShouldSerialize();
        public override string ToString();

        [DefaultValue(""), RefreshProperties(RefreshProperties.Repaint), Browsable(true), aa("HttpOptions_Url")]
        public string Url { get; set; }

        [Browsable(true), DefaultValue(""), aa("HttpOptions_User"), RefreshProperties(RefreshProperties.Repaint)]
        public string User { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue(""), aa("HttpOptions_Password"), Browsable(true)]
        public string Password { get; set; }

        [Obsolete("Use HTTP script tunnelling and 'Url' property instead."), aa("HttpOptions_Host"), Browsable(true), DefaultValue(""), RefreshProperties(RefreshProperties.Repaint)]
        public string Host { get; set; }

        [aa("HttpOptions_Port"), Browsable(true), DefaultValue(0x22b8), RefreshProperties(RefreshProperties.Repaint), Obsolete("Use HTTP script tunnelling and 'Url' property instead.")]
        public int Port { get; set; }

        [Browsable(true), aa("HttpOptions_MaxConnectionAge"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(300), Obsolete]
        public int MaxConnectionAge { get; set; }

        [DefaultValue(true), aa("HttpOptions_StrictContentLength"), RefreshProperties(RefreshProperties.Repaint), Obsolete, Browsable(true)]
        public bool StrictContentLength { get; set; }

        [Browsable(true), DefaultValue(0x10000), Obsolete, RefreshProperties(RefreshProperties.Repaint), aa("HttpOptions_ContentLength")]
        public int ContentLength { get; set; }

        [Browsable(true), DefaultValue(1), RefreshProperties(RefreshProperties.Repaint), Obsolete("Type of KeepAlive property is a subject to change: it will be changed from integer to boolean."), aa("HttpOptions_KeepAlive")]
        public int KeepAlive { get; set; }

        [Browsable(false), aa("HttpOptions_Base64"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(false)]
        public bool Base64 { get; set; }
    }
}

