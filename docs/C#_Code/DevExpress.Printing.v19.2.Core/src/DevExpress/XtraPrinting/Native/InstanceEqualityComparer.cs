namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class InstanceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly InstanceEqualityComparer Instance;

        static InstanceEqualityComparer();
        private InstanceEqualityComparer();
        public bool Equals(object x, object y);
        public int GetHashCode(object obj);
    }
}

