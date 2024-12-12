namespace DevExpress.Data.WcfLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class WcfServerModeCore : ServerModeCoreExtendable, IFilteredDataSource
    {
        private static readonly ConcurrentDictionary<int, bool> subscribedContextDictionary;
        private readonly IQueryable source;
        private CriteriaOperator _FixedCriteria;
        private static readonly ICriteriaToExpressionConverter converter;
        private static readonly ICriteriaToExpressionConverter converterForInstance;

        static WcfServerModeCore();
        public WcfServerModeCore(IQueryable source, CriteriaOperator keyExpression, CriteriaOperator fixedFilterCriteria, ServerModeCoreExtender extender);
        public WcfServerModeCore(IQueryable source, string keyExpression, CriteriaOperator fixedFilterCriteria, ServerModeCoreExtender extender);
        protected override ServerModeKeyedCacheExtendable CreateCacheCoreExtendable();
        protected override ServerModeCore DXCloneCreate();
        public override IList GetAllFilteredAndSortedRows();
        protected override object[] GetUniqueValuesInternal(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        public static string GuessKeyExpression(Type objectType);
        private static string ResolveVersionByUri(Uri uri, string oldVersion);
        public virtual void SetFixedCriteria(CriteriaOperator op);

        public CriteriaOperator FixedCriteria { get; }

        CriteriaOperator IFilteredDataSource.Filter { get; set; }

        public static ICriteriaToExpressionConverter Converter { get; }

        public static ICriteriaToExpressionConverter ConverterForInstance { get; }

        private CriteriaOperator SingleKeyCriteria { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WcfServerModeCore.<>c <>9;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__22_0;

            static <>c();
            internal IEnumerable<ServerModeOrderDescriptor> <GetAllFilteredAndSortedRows>b__22_0(ServerModeOrderDescriptor[] ords);
        }
    }
}

