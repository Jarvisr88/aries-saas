namespace DevExpress.Data.ODataLinq
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ODataServerModeCore : ServerModeCoreExtendable, IFilteredDataSource
    {
        private IQueryable source;
        private CriteriaOperator _FixedCriteria;
        private string _Properties;
        private string _SelectExpression;
        private static readonly ICriteriaToExpressionConverter converter;
        private static readonly ICriteriaToExpressionConverter converterForInstance;

        static ODataServerModeCore();
        public ODataServerModeCore(IQueryable source, CriteriaOperator[] keys, CriteriaOperator fixedFilterCriteria, string properties, ServerModeCoreExtender extender);
        public ODataServerModeCore(IQueryable source, string[] keys, CriteriaOperator fixedFilterCriteria, string properties, ServerModeCoreExtender extender);
        protected override ServerModeKeyedCacheExtendable CreateCacheCoreExtendable();
        protected override ServerModeCore DXCloneCreate();
        public override IList GetAllFilteredAndSortedRows();
        private string GetSelectExpression();
        protected override object[] GetUniqueValuesInternal(CriteriaOperator expression, int maxCount, CriteriaOperator filter);
        public static string[] GuessKeyExpression(Type objectType);
        public virtual void SetFixedCriteria(CriteriaOperator op);

        public CriteriaOperator FixedCriteria { get; }

        CriteriaOperator IFilteredDataSource.Filter { get; set; }

        public string Properties { get; }

        public static ICriteriaToExpressionConverter Converter { get; }

        public static ICriteriaToExpressionConverter ConverterForInstance { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ODataServerModeCore.<>c <>9;
            public static Func<string, CriteriaOperator> <>9__1_0;
            public static Func<ServerModeOrderDescriptor[], IEnumerable<ServerModeOrderDescriptor>> <>9__23_0;
            public static Func<object, bool> <>9__26_0;
            public static Func<PropertyInfo, string> <>9__26_1;

            static <>c();
            internal CriteriaOperator <.ctor>b__1_0(string k);
            internal IEnumerable<ServerModeOrderDescriptor> <GetAllFilteredAndSortedRows>b__23_0(ServerModeOrderDescriptor[] sis);
            internal bool <GuessKeyExpression>b__26_0(object a);
            internal string <GuessKeyExpression>b__26_1(PropertyInfo p);
        }
    }
}

