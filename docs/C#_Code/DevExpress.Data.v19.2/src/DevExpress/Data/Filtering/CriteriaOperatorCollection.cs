namespace DevExpress.Data.Filtering
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class CriteriaOperatorCollection : List<CriteriaOperator>
    {
        private static int HashSeed;

        static CriteriaOperatorCollection();
        public CriteriaOperatorCollection();
        public CriteriaOperatorCollection(IEnumerable<CriteriaOperator> collection);
        public CriteriaOperatorCollection(int capacity);
        public override bool Equals(object obj);
        public override int GetHashCode();
        public override string ToString();
    }
}

