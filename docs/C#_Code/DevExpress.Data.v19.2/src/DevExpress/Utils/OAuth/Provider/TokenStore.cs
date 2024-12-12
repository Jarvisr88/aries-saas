namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Configuration;

    public abstract class TokenStore : ConfigurationSection
    {
        protected TokenStore()
        {
        }

        public abstract IToken GetToken(string token);

        [ConfigurationProperty("expirationSeconds", DefaultValue="60", IsRequired=false)]
        public int ExpirationSeconds
        {
            get => 
                (int) base["expirationSeconds"];
            set => 
                base["expirationSeconds"] = value;
        }
    }
}

