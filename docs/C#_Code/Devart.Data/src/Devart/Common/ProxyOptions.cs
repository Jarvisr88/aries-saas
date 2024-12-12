namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [TypeConverter(typeof(Devart.Common.b))]
    public class ProxyOptions
    {
        private string a;
        private int b;
        private string c;
        private string d;
        public const int DefaultPort = 0xc38;
        public static readonly string ProxyHostKeyword = "Proxy Host";
        public static readonly string ProxyPortKeyword = "Proxy Port";
        public static readonly string ProxyUserKeyword = "Proxy User";
        public static readonly string ProxyPasswordKeyword = "Proxy Password";

        public event ProxyOptionsPropertyChanged PropertyChanged;

        public ProxyOptions()
        {
            this.a = string.Empty;
            this.b = 0xc38;
            this.c = string.Empty;
            this.d = string.Empty;
        }

        public ProxyOptions(string host, int port, string user, string password)
        {
            this.a = string.Empty;
            this.b = 0xc38;
            this.c = string.Empty;
            this.d = string.Empty;
            this.a = host;
            this.b = port;
            this.c = user;
            this.d = password;
        }

        private void a(string A_0, object A_1)
        {
            if (this.e != null)
            {
                this.e(A_0, A_1);
            }
        }

        public bool ShouldSerialize() => 
            (this.a != string.Empty) || ((this.b != 0xc38) || ((this.c != string.Empty) || (this.d != string.Empty)));

        public override string ToString() => 
            "ProxyOptions";

        [y("ProxyOptions_Host"), Browsable(true), RefreshProperties(RefreshProperties.Repaint), DefaultValue("")]
        public string Host
        {
            get => 
                this.a;
            set
            {
                if (this.a != value)
                {
                    this.a = value;
                    this.a(ProxyHostKeyword, value);
                }
            }
        }

        [y("ProxyOptions_Port"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(0xc38), Browsable(true)]
        public int Port
        {
            get => 
                this.b;
            set
            {
                if (this.b != value)
                {
                    this.b = value;
                    this.a(ProxyPortKeyword, value);
                }
            }
        }

        [y("ProxyOptions_User"), RefreshProperties(RefreshProperties.Repaint), Browsable(true), DefaultValue("")]
        public string User
        {
            get => 
                this.c;
            set
            {
                if (this.c != value)
                {
                    this.c = value;
                    this.a(ProxyUserKeyword, value);
                }
            }
        }

        [PasswordPropertyText(true), DefaultValue(""), Browsable(true), y("ProxyOptions_Password"), RefreshProperties(RefreshProperties.Repaint)]
        public string Password
        {
            get => 
                this.d;
            set
            {
                if (this.d != value)
                {
                    this.d = value;
                    this.a(ProxyPasswordKeyword, value);
                }
            }
        }

        [y("ProxyOptions_ProxyAddress"), DefaultValue(""), RefreshProperties(RefreshProperties.Repaint), Browsable(true)]
        public Uri ProxyAddress
        {
            get
            {
                UriBuilder builder1 = new UriBuilder();
                builder1.Host = this.a;
                builder1.Port = this.b;
                builder1.UserName = this.c;
                builder1.Password = this.d;
                return builder1.Uri;
            }
        }
    }
}

