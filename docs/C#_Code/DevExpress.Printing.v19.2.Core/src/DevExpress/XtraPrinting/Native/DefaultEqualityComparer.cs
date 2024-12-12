namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class DefaultEqualityComparer : IEqualityComparer<object>
    {
        public static readonly IEqualityComparer<object> Instance;

        static DefaultEqualityComparer();
        private DefaultEqualityComparer();
        bool IEqualityComparer<object>.Equals(object x, object y);
        int IEqualityComparer<object>.GetHashCode(object obj);
    }
}

