namespace DevExpress.XtraReports.Native
{
    using System;

    public class ObjectName
    {
        private string displayName;
        private string name;
        private string dataMember;

        public ObjectName(string name, string displayName);
        public ObjectName(string name, string displayName, string dataMember);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string Name { get; }

        public string FullName { get; }

        public string DisplayName { get; }
    }
}

