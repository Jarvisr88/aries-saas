namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class UnboundColumnInfoCollection : CollectionBase
    {
        public UnboundColumnInfoCollection();
        public UnboundColumnInfoCollection(UnboundColumnInfo[] infos);
        public int Add(UnboundColumnInfo info);
        public void AddRange(UnboundColumnInfo[] infos);

        public UnboundColumnInfo this[int index] { get; }
    }
}

