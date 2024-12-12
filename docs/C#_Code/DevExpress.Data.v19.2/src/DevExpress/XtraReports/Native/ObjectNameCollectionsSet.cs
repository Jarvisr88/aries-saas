namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ObjectNameCollectionsSet : CollectionBase
    {
        public int Add(ObjectNameCollection collection);
        public void AddRange(ObjectNameCollectionsSet c);

        public ObjectNameCollection this[int index] { get; set; }
    }
}

