namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public class JoinContextPropertyInfo
    {
        public readonly EvaluatorProperty Property;
        public readonly string PropertyNameInCriteria;

        public JoinContextPropertyInfo(EvaluatorProperty property, string propertyNameInCriteria);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();
    }
}

