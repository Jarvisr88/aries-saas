namespace DevExpress.Internal
{
    using DevExpress.Utils.About;
    using System;

    public class UserData
    {
        private string userName;
        private string uniqueId;
        internal long licensedProducts;
        internal long licensedSources;
        private int userNo;
        private int keyNumber;
        internal int version;
        internal bool isValid;
        internal DateTime expiration;
        private static UserData empty;

        public UserData() : this(-1, 0, string.Empty, 0L, 0L)
        {
        }

        internal UserData(DateTime date) : this(-2, 0, string.Empty, 0L, 0L)
        {
            this.expiration = date;
            this.isValid = true;
            this.userName = "TRIAL";
        }

        public UserData(int userNo, int keyNumber, string userName, long licensedProducts, long licensedSources)
        {
            this.userName = "";
            this.uniqueId = "";
            this.version = 0x3d;
            this.expiration = new DateTime(0x7db, 11, 1);
            this.userNo = userNo;
            this.keyNumber = keyNumber;
            this.userName = userName;
            this.licensedProducts = licensedProducts;
            this.licensedSources = licensedSources;
            this.uniqueId = string.Empty;
            this.UpdateUserName();
        }

        internal void Clear()
        {
            this.isValid = false;
            this.userNo = -1;
            this.keyNumber = 0;
            this.userName = string.Empty;
            this.uniqueId = string.Empty;
            this.licensedProducts = 0L;
            this.licensedSources = 0L;
        }

        internal string GetExp()
        {
            if (this.expiration < new DateTime(0x7db, 11, 1))
            {
                this.expiration = new DateTime(0x7db, 11, 1);
            }
            try
            {
                return this.expiration.ToFileTime().ToString();
            }
            catch
            {
                return "0";
            }
        }

        public string GetText(string securityId) => 
            $"{this.Version},{this.LicensedProducts},{this.LicensedSources},{this.UserName},{this.UserNo},{this.KeyNumber},{securityId}";

        public bool IsLicensed(ProductKind kind) => 
            (this.LicensedProducts & kind) == kind;

        public bool IsLicensedSource(ProductKind kind) => 
            (this.LicensedSources & kind) == kind;

        internal void Parse(string text)
        {
            this.Clear();
            if ((text != null) && (text.Length != 0))
            {
                char[] separator = new char[] { ',' };
                string[] strArray = text.Split(separator, 7);
                if (strArray.Length == 7)
                {
                    try
                    {
                        this.version = Convert.ToInt32(strArray[0]);
                        if (0xc0 != this.version)
                        {
                            this.Clear();
                        }
                        else
                        {
                            this.licensedProducts = Convert.ToInt64(strArray[1]);
                            this.licensedSources = Convert.ToInt64(strArray[2]);
                            this.userName = strArray[3];
                            this.userNo = Convert.ToInt32(strArray[4]);
                            this.keyNumber = Convert.ToInt32(strArray[5]);
                            this.uniqueId = strArray[6];
                            this.UpdateUserName();
                            this.isValid = true;
                        }
                    }
                    catch
                    {
                        this.Clear();
                    }
                }
            }
        }

        internal static string UpdateText(string text)
        {
            text ??= string.Empty;
            string str = text.Replace(",", " ").Trim().Replace("  ", " ");
            if (str.Length > 30)
            {
                str = str.Substring(0, 30);
            }
            return str;
        }

        internal void UpdateUserName()
        {
            this.userName = UpdateText(this.userName);
        }

        public bool IsTrial =>
            this.userNo == -2;

        public bool IsExpired =>
            this.IsTrial && (DateTime.Now > this.expiration);

        public static UserData Empty
        {
            get
            {
                empty ??= new UserData();
                return empty;
            }
        }

        public string UniqueId =>
            this.uniqueId;

        public bool IsValid =>
            this.isValid;

        public int UserNo =>
            this.userNo;

        public int KeyNumber =>
            this.keyNumber;

        public string UserName =>
            this.userName;

        public int Version =>
            this.version;

        public string VersionText =>
            (this.version <= 0) ? "" : $"{(this.Version / 10)}.{(this.Version % 10)}";

        protected long LicensedProducts =>
            this.licensedProducts;

        protected long LicensedSources =>
            this.licensedSources;
    }
}

