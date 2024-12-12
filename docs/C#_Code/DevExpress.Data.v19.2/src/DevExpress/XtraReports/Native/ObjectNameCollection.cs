namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ObjectNameCollection : CollectionBase
    {
        public int Add(ObjectName item);
        public int Add(string name, string displayName);
        public void AddRange(ObjectNameCollection items);
        public void CopyFrom(ObjectNameCollection source);
        public ObjectName GetItemByName(string name);
        public int IndexOf(string displayName);
        public int IndexOfByName(string name);

        public ObjectName this[int index] { get; }
    }
}

