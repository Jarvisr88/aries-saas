namespace DevExpress.DataAccess.Native
{
    using DevExpress.Data.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class InMemoryConnectionStringProvider : IConnectionStringsProvider
    {
        private readonly Func<IDictionary<string, string>> connectionStringsProvider;

        public InMemoryConnectionStringProvider(Func<IDictionary<string, string>> connectionStringsProvider)
        {
            this.connectionStringsProvider = connectionStringsProvider;
        }

        public IConnectionStringInfo[] GetConfigFileConnections()
        {
            Func<KeyValuePair<string, string>, bool> predicate = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<KeyValuePair<string, string>, bool> local1 = <>c.<>9__2_0;
                predicate = <>c.<>9__2_0 = x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value);
            }
            Func<KeyValuePair<string, string>, ConnectionStringInfo> selector = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<KeyValuePair<string, string>, ConnectionStringInfo> local2 = <>c.<>9__2_1;
                selector = <>c.<>9__2_1 = delegate (KeyValuePair<string, string> x) {
                    ConnectionStringInfo info1 = new ConnectionStringInfo();
                    info1.Name = x.Key;
                    info1.RunTimeConnectionString = x.Value;
                    return info1;
                };
            }
            return this.connectionStringsProvider().Where<KeyValuePair<string, string>>(predicate).Select<KeyValuePair<string, string>, ConnectionStringInfo>(selector).ToArray<ConnectionStringInfo>();
        }

        public IConnectionStringInfo[] GetConnections() => 
            new ConnectionStringInfo[0];

        public string GetConnectionString(string connectionStringName)
        {
            IConnectionStringInfo connectionStringInfo = this.GetConnectionStringInfo(connectionStringName);
            if (connectionStringInfo != null)
            {
                return connectionStringInfo.RunTimeConnectionString;
            }
            IConnectionStringInfo local1 = connectionStringInfo;
            return null;
        }

        public IConnectionStringInfo GetConnectionStringInfo(string connectionStringName)
        {
            string str = null;
            this.connectionStringsProvider().TryGetValue(connectionStringName, out str);
            ConnectionStringInfo info1 = new ConnectionStringInfo();
            info1.Name = connectionStringName;
            info1.RunTimeConnectionString = str;
            return info1;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InMemoryConnectionStringProvider.<>c <>9 = new InMemoryConnectionStringProvider.<>c();
            public static Func<KeyValuePair<string, string>, bool> <>9__2_0;
            public static Func<KeyValuePair<string, string>, ConnectionStringInfo> <>9__2_1;

            internal bool <GetConfigFileConnections>b__2_0(KeyValuePair<string, string> x) => 
                !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value);

            internal ConnectionStringInfo <GetConfigFileConnections>b__2_1(KeyValuePair<string, string> x)
            {
                ConnectionStringInfo info1 = new ConnectionStringInfo();
                info1.Name = x.Key;
                info1.RunTimeConnectionString = x.Value;
                return info1;
            }
        }
    }
}

