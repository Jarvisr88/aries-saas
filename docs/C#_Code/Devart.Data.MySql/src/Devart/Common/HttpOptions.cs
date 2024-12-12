namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(t))]
    public class HttpOptions
    {
        private string a;
        private string b;
        private string c;
        private bool d;
        private bool e;
        internal const string g = "HTTP Url";
        internal const string h = "HTTP User";
        internal const string i = "HTTP Password";
        internal const string j = "HTTP Keep Alive";
        internal const string k = "HTTP Base64";
        public const bool DefaultKeepAlive = true;
        public const bool DefaultBase64 = false;

        internal event ay PropertyChanged;

        internal HttpOptions();
        public HttpOptions(string url, string user, string password, bool keepAlive, bool base64);
        private void a(string A_0, object A_1);
        internal bool ShouldSerialize();
        public override string ToString();

        [Browsable(true), aa("HttpOptions_Url"), RefreshProperties(RefreshProperties.Repaint), DefaultValue("")]
        public string Url { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), Browsable(true), DefaultValue(""), aa("HttpOptions_User")]
        public string User { get; set; }

        [aa("HttpOptions_Password"), DefaultValue(""), Browsable(true), RefreshProperties(RefreshProperties.Repaint)]
        public string Password { get; set; }

        [DefaultValue(true), aa("HttpOptions_KeepAlive"), Browsable(true), RefreshProperties(RefreshProperties.Repaint)]
        public bool KeepAlive { get; set; }

        [DefaultValue(true), RefreshProperties(RefreshProperties.Repaint), aa("HttpOptions_Base64"), Browsable(true)]
        public bool Base64 { get; set; }
    }
}

