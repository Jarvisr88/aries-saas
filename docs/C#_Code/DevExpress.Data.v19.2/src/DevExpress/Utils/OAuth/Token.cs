namespace DevExpress.Utils.OAuth
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Token : IToken
    {
        public static readonly Token Empty;
        private string _value;
        private bool _isCallbackConfirmed;
        private string _secret;
        private string _callback;
        private string _authenticationTicket;
        private string _consumerKey;
        private string _consumerSecret;
        private string _verifier;
        public Token(Parameters paramters, string consumerKey, string consumerSecret, string callback, string version)
        {
            if (paramters == null)
            {
                throw new ArgumentNullException("paramters");
            }
            if (string.IsNullOrEmpty(version))
            {
                version = "1.0";
            }
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }
            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }
            this._callback = callback;
            this._verifier = string.Empty;
            this._authenticationTicket = string.Empty;
            this._consumerKey = consumerKey;
            this._consumerSecret = consumerSecret;
            if (version == "1.0")
            {
                this._value = paramters["oauth_token"].Value;
                this._secret = paramters["oauth_token_secret"].Value;
                this._isCallbackConfirmed = string.Equals("true", paramters["oauth_callback_confirmed"].Value, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                if (version != "2.0")
                {
                    throw new ArgumentException($"The specified OAuth version '{version}' is not supported.", "version");
                }
                this._value = paramters["access_token"].Value;
                this._secret = "n/a";
                this._isCallbackConfirmed = true;
            }
        }

        public Token(string consumerKey, string consumerSecret, string value, string secret, string authenticationTicket, string verifier, string callback)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("secret");
            }
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }
            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }
            this._value = value;
            this._secret = secret;
            this._authenticationTicket = authenticationTicket;
            this._consumerKey = consumerKey;
            this._consumerSecret = consumerSecret;
            this._verifier = verifier;
            this._callback = callback;
            this._isCallbackConfirmed = true;
        }

        public Token(string consumerKey, string consumerSecret, string value, string secret) : this(consumerKey, consumerSecret, value, secret, string.Empty, string.Empty, string.Empty)
        {
        }

        public static string NewToken(TokenLength length)
        {
            string str = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N")));
            int num = str.Length;
            while (str[num - 1] == '=')
            {
                num--;
            }
            if (num == 0)
            {
                throw new InvalidOperationException();
            }
            if (length == TokenLength.Short)
            {
                num = Math.Min(6, num);
            }
            return str.Substring(0, num);
        }

        public bool IsEmpty =>
            string.IsNullOrEmpty(this.Value) || (string.IsNullOrEmpty(this.Secret) || (string.IsNullOrEmpty(this.ConsumerKey) || string.IsNullOrEmpty(this.ConsumerSecret)));
        public string Value =>
            this._value;
        public bool IsCallbackConfirmed =>
            this._isCallbackConfirmed;
        public string Secret =>
            this._secret;
        public string Callback =>
            this._callback;
        public string AuthenticationTicket =>
            this._authenticationTicket;
        public string ConsumerKey =>
            this._consumerKey;
        public string ConsumerSecret =>
            this._consumerSecret;
        public string Verifier =>
            this._verifier;
        static Token()
        {
        }
    }
}

