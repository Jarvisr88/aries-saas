namespace DevExpress.Data.Helpers
{
    using System;

    public class GroupObjectKeyInfo
    {
        private object[] values;
        private int? combinedHashCode;

        public GroupObjectKeyInfo(object[] values);
        public override bool Equals(object obj);
        public override int GetHashCode();
        private int UpdateHashCode();

        public object[] Values { get; }
    }
}

