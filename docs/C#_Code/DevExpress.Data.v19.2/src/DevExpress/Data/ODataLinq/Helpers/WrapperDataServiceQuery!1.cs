namespace DevExpress.Data.ODataLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class WrapperDataServiceQuery<TElement> : IQueryable, IEnumerable
    {
        private readonly object rootGroupKeyValue;
        private readonly IQueryable<TElement> rootSource;
        private readonly CriteriaOperator rootKeyExpression;
        private readonly int rootLevel;
        private readonly int rootTakeCount;
        private readonly int rootSkipCount;
        private readonly ElementDescriptor elementDescriptor;
        private readonly string rootSelectExpression;

        public WrapperDataServiceQuery(WrapperDataServiceQuery<TElement> source);
        public WrapperDataServiceQuery(object rootGroupKeyValue, IQueryable<TElement> rootSource, CriteriaOperator rootKeyExpression, int rootLevel, int rootTakeCount, int rootSkipCount, string selectExpression);
        public WrapperDataServiceQuery(object rootGroupKeyValue, IQueryable<TElement> rootSource, CriteriaOperator rootKeyExpression, int rootLevel, int rootTakeCount, int rootSkipCount, string selectExpression, ElementDescriptor elementDescriptor);
        private bool AddToList(List<WrapperDataServiceQuery<TElement>> list, WrapperDataServiceQuery<TElement> value, int skip, int top, ref int skipCounter);
        private IQueryable<TElement> AppendKeyFilter(IQueryable query, CriteriaOperator keyCriteria, object keyValue, BinaryOperatorType opType);
        public IEnumerator<TElement> GetEnumerator();
        private static ODataLinqHelpers.ListHelperBase GetListHelper(Type type);
        private IQueryable<TElement> GetSourceForExecute(IQueryable<TElement> source, int? skip = new int?(), int? take = new int?());
        public IQueryable GroupBy(CriteriaOperator key, bool isDesc, int skip, int top);
        private bool IsKeyProperty(CriteriaOperator[] criteria);
        private static bool IsKeyProperty(CriteriaOperator criteria);
        public object Max(CriteriaOperator fieldCriteria);
        private object MaxOrMin(CriteriaOperator fieldCriteria, bool isMax);
        public object Min(CriteriaOperator fieldCriteria);
        public IQueryable OrderBy(ServerModeOrderDescriptor[] order);
        public IQueryable Select(string selectExpression);
        public IQueryable SelectFieldValues(CriteriaOperator criteria);
        public WrapperResult SelectFieldValuesAndRows(CriteriaOperator[] operands);
        private WrapperResult SelectFieldValuesAsList(IQueryable<TElement> source, CriteriaOperator[] keysCriteria, int skipCount, int takeCount);
        private IQueryable SelectUniqueFieldValues(IQueryable<TElement> source, CriteriaOperator criteria, int skipCount, int takeCount);
        public IQueryable Skip(int count);
        IEnumerator IEnumerable.GetEnumerator();
        public IQueryable Take(int count);
        private IQueryable<TElement> TryAddSelectExpression(IQueryable<TElement> query);
        public IQueryable Where(CriteriaOperator filterCriteria);

        public int Count { get; }

        public object Key { get; }

        public IQueryable<TElement> Rows { get; }

        public Type ElementType { get; }

        public System.Linq.Expressions.Expression Expression { get; }

        public IQueryProvider Provider { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WrapperDataServiceQuery<TElement>.<>c <>9;
            public static Predicate<OperandValue> <>9__20_0;
            public static Predicate<OperandProperty> <>9__23_0;
            public static Func<object[], ServerModeCompoundKey> <>9__27_0;

            static <>c();
            internal bool <GroupBy>b__20_0(OperandValue keyOperandValue);
            internal bool <IsKeyProperty>b__23_0(OperandProperty property);
            internal ServerModeCompoundKey <SelectFieldValuesAsList>b__27_0(object[] oa);
        }
    }
}

