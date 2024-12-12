namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;

    public class JoinContextValueInfoSet
    {
        private Dictionary<string, object> properties;

        public JoinContextValueInfoSet(Dictionary<string, object> properties);

        public Dictionary<string, object> Properties { get; }
    }
}

