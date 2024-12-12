namespace DevExpress.Data
{
    using System;
    using System.Collections;

    public class ValueComparer : IComparer
    {
        public static readonly IComparer Default;
        public static readonly IComparer TypeInvariant;

        static ValueComparer();
        public virtual int Compare(object x, object y);
        protected virtual int CompareCore(object x, object y);
        public bool ObjectEquals(object x, object y);
        protected virtual bool ObjectEqualsCore(object x, object y);

        private sealed class DefaultValueComparer : ValueComparer
        {
            internal DefaultValueComparer();
        }

        private sealed class TypeInvariantComparer : ValueComparer
        {
            private static readonly Type stringType;

            static TypeInvariantComparer();
            internal TypeInvariantComparer();
            protected override int CompareCore(object x, object y);
            private static object ConvertValue(object value, Type valueType, Type type);
        }
    }
}

