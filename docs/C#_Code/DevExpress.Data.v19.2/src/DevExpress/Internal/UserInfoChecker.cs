namespace DevExpress.Internal
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal class UserInfoChecker
    {
        internal const int SignatureLength = 0x88;
        private const string puk = "<RSAKeyValue><Modulus>yNuZy4wH5P3B8z6JizxE8jCOcVi56/1pUEogqmBKb/oHzZoDknOZNS2Eezg90LjJPxht3wCzqWaL4EnpLa2KQmJYRUyxb3HawCFlmBmGIq0gUuYs98j9qsD2HgxgzWT5tV0A5w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private const string testkey = "MTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMQ==";
        private static UserInfoChecker def;

        private string BytesToString(byte[] bytes) => 
            this.BytesToString(bytes, 0);

        private string BytesToString(byte[] bytes, int position) => 
            Encoding.UTF8.GetString(bytes, position, bytes.Length - position);

        private string GetData(string key) => 
            key.Substring(0x88);

        private string GetInfo(string key)
        {
            string str;
            try
            {
                byte[] bytes = Convert.FromBase64String(this.GetData(key));
                str = this.BytesToString(bytes, 20);
            }
            catch
            {
                return string.Empty;
            }
            return str;
        }

        private string GetSign(string key) => 
            key.Substring(0, 0x88);

        public int GetVersion(string key)
        {
            UserData data = this.ParseCore(key);
            return (((data == null) || !data.IsValid) ? 0 : data.Version);
        }

        public bool IsValid(string key) => 
            this.Parse(key).IsValid;

        protected bool IsValidCore(string key) => 
            this.IsValidKey(new UserData(), key);

        private bool IsValidKey(UserData data, string key)
        {
            bool flag;
            if ((key == null) || (key.Length < 0x88))
            {
                return false;
            }
            try
            {
                byte[] buffer = Convert.FromBase64String(this.GetData(key));
                byte[] rgbSignature = Convert.FromBase64String(this.GetSign(key));
                using (SHA1 sha = SHA1.Create())
                {
                    flag = HashHelper.VerifyHash("<RSAKeyValue><Modulus>yNuZy4wH5P3B8z6JizxE8jCOcVi56/1pUEogqmBKb/oHzZoDknOZNS2Eezg90LjJPxht3wCzqWaL4EnpLa2KQmJYRUyxb3HawCFlmBmGIq0gUuYs98j9qsD2HgxgzWT5tV0A5w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", sha.ComputeHash(buffer), rgbSignature);
                }
            }
            catch
            {
                return false;
            }
            return flag;
        }

        public UserData Parse(string key)
        {
            UserData data = this.ParseCore(key);
            return ((data != null) ? data : new UserData());
        }

        protected UserData ParseCore(string key)
        {
            UserData data = new UserData();
            if (!this.IsValidKey(data, key) || this.IsValidKey(data, "MTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMTExMQ=="))
            {
                return null;
            }
            data.Parse(this.GetInfo(key));
            return data;
        }

        public static UserInfoChecker Default
        {
            get
            {
                def ??= new UserInfoChecker();
                return def;
            }
        }
    }
}

