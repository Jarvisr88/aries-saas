namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class DataCacheConfiguration
    {
        private static readonly DataCacheConfiguration empty = new DataCacheConfiguration(DataCacheConfigurationCaching.All, null);
        [Obsolete("Caching may work incorrectly with\x00a0explicit transactions. Connect your data layer\x00a0to the database directly to use explicit transactions.")]
        public static bool SuppressExplicitTransactionExceptions;
        private string[] tables;
        private DataCacheConfigurationCaching caching;
        [NonSerialized]
        private Dictionary<string, bool> tableDictionary;

        public DataCacheConfiguration()
        {
        }

        public DataCacheConfiguration(DataCacheConfigurationCaching caching, params string[] tables)
        {
            this.tables = tables;
            this.caching = caching;
        }

        public static Dictionary<string, bool> CreateTableDictionary(string[] tableList)
        {
            if (tableList == null)
            {
                return null;
            }
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            foreach (string str in tableList)
            {
                dictionary[str] = true;
            }
            return dictionary;
        }

        public static DataCacheConfiguration Empty =>
            empty;

        public string[] Tables
        {
            get => 
                this.tables;
            set
            {
                this.tables = value;
                this.tableDictionary = null;
            }
        }

        public DataCacheConfigurationCaching Caching
        {
            get => 
                this.caching;
            set => 
                this.caching = value;
        }

        [XmlIgnore]
        public Dictionary<string, bool> TableDictionary
        {
            get
            {
                if ((this.tableDictionary == null) && (this.Tables != null))
                {
                    this.tableDictionary = CreateTableDictionary(this.Tables);
                }
                return this.tableDictionary;
            }
        }
    }
}

