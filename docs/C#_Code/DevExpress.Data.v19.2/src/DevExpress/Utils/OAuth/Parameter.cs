namespace DevExpress.Utils.OAuth
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Parameter : IComparable<Parameter>, IEquatable<Parameter>
    {
        public static readonly Parameter Empty;
        private string _name;
        private string _value;
        public static Parameter Parse(string token) => 
            Parse(token, true, false);

        public static Parameter Parse(string token, bool unescape, bool unquote)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return Parse(token, 0, token.Length, unescape, unquote);
            }
            return new Parameter();
        }

        public static Parameter Parse(string token, int index, int count, bool unescape, bool unquote)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            if (count == 0)
            {
                return new Parameter();
            }
            int length = token.Length;
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (index > length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (index > (length - count))
            {
                throw new ArgumentOutOfRangeException("count");
            }
            int num2 = token.IndexOf('=', index, count);
            if ((num2 == 0) || (index == num2))
            {
                throw new FormatException($"The specified token is not valid. {token.Substring(index, count)}");
            }
            return ((num2 >= 0) ? new Parameter(Escaping.Unescape(token, index, num2 - index, unescape, unquote), Escaping.Unescape(token, num2 + 1, count - ((num2 + 1) - index), unescape, unquote)) : new Parameter(Escaping.Unescape(token, index, count, unescape, unquote), string.Empty));
        }

        public static Parameter Version(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentNullException("version");
            }
            return new Parameter("oauth_version", version);
        }

        public static Parameter Version() => 
            new Parameter("oauth_version", "1.0");

        public static Parameter Nonce() => 
            new Parameter("oauth_nonce", DevExpress.Utils.OAuth.Nonce.CreateNonce().Value);

        public static Parameter Timestamp()
        {
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
            long totalSeconds = (long) (DateTime.UtcNow - time).TotalSeconds;
            return new Parameter("oauth_timestamp", totalSeconds.ToString());
        }

        public bool IsTimestamp()
        {
            long num;
            if (this.IsEmpty || !string.Equals(this.Name, "oauth_timestamp", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0);
            if (!long.TryParse(this.Value, out num))
            {
                return false;
            }
            long totalSeconds = (long) (DateTime.UtcNow - time).TotalSeconds;
            return ((num >= (totalSeconds - 60)) && (num <= (totalSeconds + 60)));
        }

        public bool IsNonce() => 
            !this.IsEmpty && (string.Equals(this.Name, "oauth_nonce", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(this.Value));

        public static Parameter Callback(string callback)
        {
            if (string.IsNullOrEmpty(callback))
            {
                throw new ArgumentNullException("callback");
            }
            return new Parameter("oauth_callback", callback);
        }

        public static Parameter Verifier(string verifier)
        {
            if (string.IsNullOrEmpty(verifier))
            {
                throw new ArgumentNullException("verifier");
            }
            return new Parameter("oauth_verifier", verifier);
        }

        public static Parameter SignatureMethod(DevExpress.Utils.OAuth.Signature sig)
        {
            switch (sig)
            {
                case DevExpress.Utils.OAuth.Signature.HMACSHA1:
                    return new Parameter("oauth_signature_method", "HMAC-SHA1");

                case DevExpress.Utils.OAuth.Signature.PLAINTEXT:
                    return new Parameter("oauth_signature_method", "PLAINTEXT");

                case DevExpress.Utils.OAuth.Signature.RSASHA1:
                    return new Parameter("oauth_signature_method", "RSA-SHA1");
            }
            throw new NotSupportedException();
        }

        public static Parameter Token(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }
            return new Parameter("oauth_token", token);
        }

        public static Parameter ConsumerKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return new Parameter("oauth_consumer_key", key);
        }

        public Parameter(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this._name = name;
            string text1 = value;
            if (value == null)
            {
                string local1 = value;
                text1 = string.Empty;
            }
            this._value = text1;
        }

        public DevExpress.Utils.OAuth.Signature ToSignatureMethod()
        {
            if (!string.Equals(this.Name, "oauth_signature_method", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidOperationException($"Parameter "oauth_signature_method" expected but "{this.Name}" found.");
            }
            string str = this.Value;
            if (str == "HMAC-SHA1")
            {
                return DevExpress.Utils.OAuth.Signature.HMACSHA1;
            }
            if (str == "PLAINTEXT")
            {
                return DevExpress.Utils.OAuth.Signature.PLAINTEXT;
            }
            if (str != "RSASHA1")
            {
                throw new InvalidOperationException($"oauth_signature_method: "{this.Value}" is not supported.");
            }
            return DevExpress.Utils.OAuth.Signature.RSASHA1;
        }

        public bool IsEmpty =>
            string.IsNullOrEmpty(this._name);
        public string Name =>
            (this._name != null) ? this._name : string.Empty;
        public string Value =>
            (this._value != null) ? this._value : string.Empty;
        public static bool operator !=(Parameter left, Parameter right) => 
            !(left == right);

        public override bool Equals(object source) => 
            (source != null) && ((source is Parameter) && Equals(this, (Parameter) source));

        public bool Equals(Parameter value) => 
            Equals(this, value);

        public static bool operator ==(Parameter left, Parameter right) => 
            Equals(left, right);

        private static bool Equals(Parameter left, Parameter right) => 
            !left.IsEmpty ? ((left.Name == right.Name) && (left.Value == right.Value)) : right.IsEmpty;

        public int CompareTo(Parameter other)
        {
            int num = string.Compare(this.Name, other.Name, StringComparison.Ordinal);
            return string.Compare(this.Value, other.Value, StringComparison.Ordinal);
        }

        public override int GetHashCode() => 
            !this.IsEmpty ? HashCodeHelper.CalculateGeneric<string, string>(this.Name, this.Value) : 0;

        static Parameter()
        {
        }
    }
}

