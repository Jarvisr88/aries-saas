namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class BrickStyleEqualityComparer : IEqualityComparer<object>
    {
        public static readonly IEqualityComparer<object> Instance;

        static BrickStyleEqualityComparer();
        private BrickStyleEqualityComparer();
        bool IEqualityComparer<object>.Equals(object x, object y);
        int IEqualityComparer<object>.GetHashCode(object obj);
    }
}

