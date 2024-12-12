namespace DevExpress.Utils.OAuth.Provider
{
    using DevExpress.Utils.OAuth;
    using System;
    using System.Configuration;

    public class ConsumerStore : ConfigurationSection, IConsumerStore
    {
        public virtual IConsumer GetConsumer(string consumerKey)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentException("consumerKey is null or empty.", "consumerKey");
            }
            if (!string.Equals(consumerKey, "anonymous"))
            {
                return null;
            }
            return new ConsumerBase { 
                CallbackUri = null,
                ConsumerKey = "anonymous",
                ConsumerSecret = "anonymous"
            };
        }
    }
}

