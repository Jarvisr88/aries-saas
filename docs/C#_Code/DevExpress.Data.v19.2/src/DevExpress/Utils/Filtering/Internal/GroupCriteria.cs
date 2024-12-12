namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public static class GroupCriteria
    {
        public static readonly OperandValue NullValue;
        public static readonly OperandValue BlanksValue;

        static GroupCriteria();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static CriteriaOperator Branch<TValue>(string[] grouping, Type[] groupingTypes, object[] path, object[] values, int level, bool useInversion);
        public static CriteriaOperator Get(string[] grouping, CriteriaOperator criteria, CriteriaOperator nonColumnCriteria);
        private static CriteriaOperator GetEqual(OperandProperty prop, OperandValue value, bool useInversion = false);
        private static CriteriaOperator GetEquals(Type type, OperandProperty prop, object value, bool useInversion = false);
        private static CriteriaOperator GetIn(Type type, OperandProperty prop, object[] values, bool useInversion = false);
        private static CriteriaOperator GetIsNull(OperandProperty prop, bool isString, bool useInversion = false);
        private static CriteriaOperator GetIsSameDay(OperandProperty prop, OperandValue value, bool useInversion = false);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsNullValue(object value);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsNullValue(OperandValue value, out object nullValue);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static CriteriaOperator Parent<TValue>(string[] grouping, Type[] groupingTypes, object[] path);
        public static void Set<TOwner>(TOwner owner, string[] grouping, string origin, CriteriaOperator criteria, Action<TOwner, string, CriteriaOperator> setter);

        private sealed class GroupingPropertyEqualityComparer : IEqualityComparer<string>
        {
            private readonly HashSet<string> grouping;

            public GroupingPropertyEqualityComparer(string[] grouping);
            bool IEqualityComparer<string>.Equals(string x, string y);
            int IEqualityComparer<string>.GetHashCode(string obj);
        }
    }
}

