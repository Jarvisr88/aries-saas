namespace DevExpress.DataAccess
{
    using DevExpress.DataAccess.Native;
    using System;
    using System.Collections.Generic;

    public class DefaultConnectionStringProvider
    {
        public static void AssignConnectionStrings(IDictionary<string, string> connectionStrings)
        {
            AssignConnectionStrings(() => connectionStrings);
        }

        public static void AssignConnectionStrings(Func<IDictionary<string, string>> connectionStringProvider)
        {
            RuntimeConnectionStringsProvider.CreateDefault = () => new InMemoryConnectionStringProvider(connectionStringProvider);
        }
    }
}

