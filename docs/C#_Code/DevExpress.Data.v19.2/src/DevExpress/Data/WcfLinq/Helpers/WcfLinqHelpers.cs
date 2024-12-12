namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class WcfLinqHelpers
    {
        private static readonly Dictionary<Type, Type> wrapperDataTypeCache;
        private static readonly Dictionary<Type, WcfLinqHelpers.ListHelperBase> listHelperCache;
        private static readonly ElementDescriptorCache elementDescriptorCache;
        public static readonly object NotSummarySupported;

        static WcfLinqHelpers();
        private static void AddMethodResultValue(this List<object> aggregateFunclist, Type wrapperDataType, object reflectWrapperData, CriteriaOperator summary, string methodName);
        private static void AddPropertyValue(this List<object> aggregateFunclist, Type wrapperDataType, object reflectWrapperData, string propertyName);
        public static int CountOfData(this IQueryable source);
        public static ElementDescriptor GetElementDescriptor(Type type);
        private static WcfLinqHelpers.ListHelperBase GetListHelper(Type wrapperDataType);
        private static object GetNewWrapperData(IQueryable source, Type wrapperDataType);
        private static Type GetWrapperDataType(IQueryable source);
        private static WrapperResult GetWrapperResult(IQueryable source, CriteriaOperator criteriaOp, Type wrapperDataType, string methodName);
        public static IQueryable GroupBy(this IQueryable source, CriteriaOperator groupCriteria, bool isDesc, int skip, int top);
        private static IQueryable InvokeMethodWithParamOfTypeCriteriaOperator(IQueryable source, CriteriaOperator criteriaOp, Type wrapperDataType, string methodName);
        private static IQueryable InvokeMethodWithParamOfTypeInt(IQueryable source, int count, Type wrapperDataType, string methodName);
        public static IQueryable OrderBy(this IQueryable source, IEnumerable<ServerModeOrderDescriptor> order);
        public static IQueryable SelectFieldValues(this IQueryable source, CriteriaOperator fieldCriteria);
        public static WrapperResult SelectFieldValuesAndRows(this IQueryable source, CriteriaOperator fieldCriteria);
        public static IEnumerable<object[]> SelectSummary(this IQueryable source, Type sourceType, ServerModeSummaryDescriptor[] summaries);
        public static IQueryable SkipData(this IQueryable source, int count);
        public static IQueryable TakeData(this IQueryable source, int count);
        public static IQueryable Where(this IQueryable source, CriteriaOperator filterCriteria);
        public static IQueryable WhereEq(this IQueryable source, CriteriaOperator fieldNameCriteria, List<CriteriaOperator> criteriaList);

        public class ListHelper<T> : WcfLinqHelpers.ListHelperBase
        {
            public override void Add(object list, object obj);
            public override void AddRange(object list, object range);
            public override IQueryable AsQueryable(object list);
            public override object CreateNewList();
            private IQueryable CreateNewQueryable();
            public override IQueryable Distinct(object list);
            public override IQueryable OrderByKey(ICriteriaToExpressionConverter converter, object list, ServerModeOrderDescriptor descriptor);
        }

        public abstract class ListHelperBase
        {
            protected ListHelperBase();
            public abstract void Add(object list, object obj);
            public abstract void AddRange(object list, object range);
            public abstract IQueryable AsQueryable(object list);
            public abstract object CreateNewList();
            public abstract IQueryable Distinct(object list);
            public abstract IQueryable OrderByKey(ICriteriaToExpressionConverter converter, object list, ServerModeOrderDescriptor descriptor);
        }
    }
}

