namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class LinqServerModeCore : ServerModeCore
    {
        protected readonly IQueryable Q;
        private ICriteriaToExpressionConverter converter;

        public LinqServerModeCore(IQueryable queryable, CriteriaOperator[] keys);
        protected override ServerModeCache CreateCacheCore();
        protected override ServerModeCore DXCloneCreate();
        public override IList GetAllFilteredAndSortedRows();
        protected override object[] GetUniqueValues(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        internal static object[] GetUniqueValuesStatic(IQueryable q, ICriteriaToExpressionConverter converter, CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        public static string GuessKeyExpression(Type objectType);
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string GuessKeyExpressionInternal(Type objectType);

        protected virtual ICriteriaToExpressionConverter Converter { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinqServerModeCore.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__11_0;

            static <>c();
            internal IEnumerable<ServerModeOrderDescriptor> <GetAllFilteredAndSortedRows>b__11_0(ServerModeOrderDescriptor[] ords);
        }
    }
}

