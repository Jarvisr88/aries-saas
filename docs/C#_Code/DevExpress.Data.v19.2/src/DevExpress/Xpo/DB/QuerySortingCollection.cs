namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class QuerySortingCollection : List<SortingColumn>
    {
        public override bool Equals(object obj)
        {
            QuerySortingCollection sortings = obj as QuerySortingCollection;
            if (sortings == null)
            {
                return false;
            }
            if (base.Count != sortings.Count)
            {
                return false;
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (!Equals(base[i], sortings[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode() => 
            HashCodeHelper.CalculateGenericList<SortingColumn>((IList<SortingColumn>) this);
    }
}

