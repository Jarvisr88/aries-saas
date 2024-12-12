namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;

    public class JoinContextPropertyInfoSet
    {
        private Dictionary<JoinContextPropertyInfo, bool> properties;

        public JoinContextPropertyInfoSet(Dictionary<JoinContextPropertyInfo, bool> properties);
        public JoinContextValueInfoSet GetJoinContextValueInfoSet(EvaluatorContext context);

        public int Count { get; }
    }
}

