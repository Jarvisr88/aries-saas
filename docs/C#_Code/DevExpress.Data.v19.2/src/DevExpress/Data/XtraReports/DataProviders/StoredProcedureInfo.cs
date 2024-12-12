namespace DevExpress.Data.XtraReports.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class StoredProcedureInfo : EntityInfo<string>
    {
        private readonly List<KeyValuePair<string, Type>> parameters;

        public StoredProcedureInfo();
        public StoredProcedureInfo(string name, string displayName, IEnumerable<KeyValuePair<string, Type>> parameters);
        public object Clone();

        private List<KeyValuePair<string, Type>> Parameters { get; }
    }
}

