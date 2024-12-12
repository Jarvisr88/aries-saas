namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections.Generic;

    public class DataPairComparer : IEqualityComparer<DataPair>
    {
        public bool Equals(DataPair x, DataPair y);
        public int GetHashCode(DataPair obj);
    }
}

