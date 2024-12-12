namespace DevExpress.Data.WcfLinq.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class WrapperDataServiceQuery<TElement> : IQueryable, IEnumerable
    {
        private readonly object rootGroupKeyValue;
        private readonly IQueryable<TElement> rootSource;
        private readonly CriteriaOperator rootKeyExpression;
        private readonly string rootOrderByQuery;
        private readonly string rootFilterQuery;
        private readonly int rootLevel;
        private readonly int rootTakeCount;
        private readonly int rootSkipCount;
        private readonly ElementDescriptor elementDescriptor;

        public WrapperDataServiceQuery(WrapperDataServiceQuery<TElement> source);
        public WrapperDataServiceQuery(object rootGroupKeyValue, IQueryable<TElement> rootSource, string rootOrderByQuery, string rootFilterQuery, CriteriaOperator rootKeyExpression, int rootLevel, int rootTakeCount, int rootSkipCount);
        public WrapperDataServiceQuery(object rootGroupKeyValue, IQueryable<TElement> rootSource, string rootOrderByQuery, string rootFilterQuery, CriteriaOperator rootKeyExpression, int rootLevel, int rootTakeCount, int rootSkipCount, ElementDescriptor elementDescriptor);
        private bool AddToList(List<WrapperDataServiceQuery<TElement>> list, WrapperDataServiceQuery<TElement> value, int skip, int top, ref int skipCounter);
        public EnumeratorForWrapperDataServiceQuery<TElement> GetEnumerator();
        private string GetFilterQuery(CriteriaOperator keyCriteria, object keyValue, BinaryOperatorType opType);
        private static WcfLinqHelpers.ListHelperBase GetListHelper(Type type);
        private static WcfLinqHelpers.ListHelperBase GetNewListHelper(string fieldName);
        private static WcfLinqHelpers.ListHelperBase GetNewListHelper(Type fieldType);
        private string GetOrderByQuery(string orderbyQuery, string fieldName, bool desc);
        public IQueryable GroupBy(CriteriaOperator key, bool isDesc, int skip, int top);
        public object Max(CriteriaOperator fieldCriteria);
        private object MaxOrMin(CriteriaOperator fieldCriteria, bool isMax);
        public object Min(CriteriaOperator fieldCriteria);
        public IQueryable OrderBy(IEnumerable<ServerModeOrderDescriptor> order);
        public IQueryable SelectFieldValues(CriteriaOperator criteria);
        public WrapperResult SelectFieldValuesAndRows(CriteriaOperator criteria);
        private WrapperResult SelectFieldValuesAsList(IQueryable<TElement> source, CriteriaOperator criteria, string orderByQuery, string filterQuery, int skipCount, int takeCount);
        private IQueryable SelectUniqueFieldValues(IQueryable<TElement> source, CriteriaOperator criteria, string orderByQuery, string filterQuery, int skipCount, int takeCount);
        public IQueryable Skip(int count);
        IEnumerator IEnumerable.GetEnumerator();
        public IQueryable Take(int count);
        public IQueryable Where(CriteriaOperator filterCriteria);

        public int Count { get; }

        public object Key { get; }

        public IQueryable<TElement> Rows { get; }

        public Type ElementType { get; }

        public System.Linq.Expressions.Expression Expression { get; }

        public IQueryProvider Provider { get; }
    }
}

