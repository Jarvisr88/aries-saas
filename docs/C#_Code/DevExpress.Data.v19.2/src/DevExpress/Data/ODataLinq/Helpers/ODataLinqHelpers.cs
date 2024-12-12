namespace DevExpress.Data.ODataLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class ODataLinqHelpers
    {
        private const string SkipMethodName = "Skip";
        private const string TakeMethodName = "Take";
        private const string WhereMethodName = "Where";
        private const string OrderByMethodName = "OrderBy";
        private const string SelectVRMethodName = "SelectFieldValuesAndRows";
        private const string SelectVMethodName = "SelectFieldValues";
        private const string GroupByMethodName = "GroupBy";
        private const string MaxMethodName = "Max";
        private const string MinMethodName = "Min";
        private const string KeyPropertyName = "Key";
        private const string CountPropertyName = "Count";
        private const string SelectMethodName = "Select";
        private static readonly ReaderWriterLockSlim wrapperDataTypeLock;
        private static readonly Dictionary<Type, Type> wrapperDataTypeCache;
        private static readonly Dictionary<Type, ODataLinqHelpers.ListHelperBase> listHelperCache;
        private static readonly ReaderWriterLockSlim wrapperHelperLock;
        private static readonly Dictionary<Type, ODataLinqHelpers.WrapperHelper> wrapperHelperDict;
        private static readonly ReaderWriterLockSlim dataServiceQueryLock;
        private static readonly Dictionary<Type, ODataLinqHelpers.DataServiceQueryHelper> dataServiceQueryDict;
        private static readonly ElementDescriptorCache elementDescriptorCache;
        public static readonly object NotSummarySupported;

        static ODataLinqHelpers();
        public static IQueryable<T> AddQueryOption<T>(IQueryable<T> source, string name, object value);
        private static IQueryable CallMethodWithParamOfTypeCriteriaOperator(IQueryable source, CriteriaOperator criteriaOp, string methodName);
        private static IQueryable CallMethodWithParamOfTypeInt(IQueryable source, int count, string methodName);
        private static IQueryable CallMethodWithParamOfTypeString(IQueryable source, string str, string methodName);
        public static int CountOfData(IQueryable source);
        public static IEnumerable<T> ExecuteDataServiceQuery<T>(IQueryable source);
        private static ODataLinqHelpers.DataServiceQueryHelper GetDataServiceQueryHelper(Type dataServiceQueryType);
        public static ElementDescriptor GetElementDescriptor(Type type);
        private static ODataLinqHelpers.ListHelperBase GetListHelper(Type wrapperDataType);
        private static object GetNewWrapperData(IQueryable source, Type wrapperDataType);
        public static long GetTotalCount<T>(IQueryable<T> source);
        private static Type GetWrapperDataType(Type elementType);
        private static ODataLinqHelpers.WrapperHelper GetWrapperHelper(Type wrapperDataType);
        private static WrapperResult GetWrapperResult(IQueryable source, CriteriaOperator[] operands, string methodName);
        public static IQueryable GroupBy(this IQueryable source, CriteriaOperator groupCriteria, bool isDesc, int skip, int top);
        public static IQueryable OrderBy(this IQueryable source, ServerModeOrderDescriptor[] order);
        public static IQueryable SelectData(this IQueryable source, string selectExpression);
        public static IQueryable SelectFieldValues(this IQueryable source, CriteriaOperator fieldCriteria);
        public static WrapperResult SelectFieldValuesAndRows(this IQueryable source, CriteriaOperator[] fieldsCriteria);
        public static IEnumerable<object[]> SelectSummary(this IQueryable source, Type sourceType, ServerModeSummaryDescriptor[] summaries);
        public static IQueryable SkipData(this IQueryable source, int count);
        public static IQueryable TakeData(this IQueryable source, int count);
        public static IQueryable Where(this IQueryable source, CriteriaOperator filterCriteria);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ODataLinqHelpers.<>c <>9;
            public static Predicate<OperandProperty> <>9__35_0;

            static <>c();
            internal bool <OrderBy>b__35_0(OperandProperty sortProperty);
        }

        private class DataServiceQueryHelper
        {
            private readonly Type dataServiceQueryType;
            private readonly Func<object, string, object, object> addQueryOptionHandler;
            private readonly Func<object, IEnumerable> executeQueryHandler;
            private readonly Func<object, object> getContextHandler;
            private readonly Func<object, object> getContinuationTokenHandler;
            private readonly Func<object, object, IEnumerable> executeContextHandler;
            private readonly Func<object, long> getTotalCountHandler;
            private readonly Func<object, object> includeTotalCountHandler;

            public DataServiceQueryHelper(Type dataServiceQueryType);
            public IQueryable<T> AddQueryOption<T>(IQueryable<T> source, string name, object value);
            [IteratorStateMachine(typeof(ODataLinqHelpers.DataServiceQueryHelper.<ExecuteWithContinuation>d__12<><>))]
            public IEnumerable<T> ExecuteWithContinuation<T>(IQueryable dataServiceQuery);
            private static Expression GetResultSynchronously(Expression taskExpression);
            public long GetTotalCount(IQueryable dataServiceQuery);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ODataLinqHelpers.DataServiceQueryHelper.<>c <>9;
                public static Func<MethodInfo, bool> <>9__9_0;

                static <>c();
                internal bool <GetResultSynchronously>b__9_0(MethodInfo m);
            }

            [CompilerGenerated]
            private sealed class <ExecuteWithContinuation>d__12<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                private int <>l__initialThreadId;
                public ODataLinqHelpers.DataServiceQueryHelper <>4__this;
                private IQueryable dataServiceQuery;
                public IQueryable <>3__dataServiceQuery;
                private ODataLinqHelpers.DataServiceQueryHelper.<>c__DisplayClass12_0<T> <>8__1;
                private IEnumerable <lastResult>5__2;
                private IEnumerator <>7__wrap1;

                [DebuggerHidden]
                public <ExecuteWithContinuation>d__12(int <>1__state);
                private void <>m__Finally1();
                private bool MoveNext();
                [DebuggerHidden]
                IEnumerator<T> IEnumerable<T>.GetEnumerator();
                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator();
                [DebuggerHidden]
                void IEnumerator.Reset();
                [DebuggerHidden]
                void IDisposable.Dispose();

                T IEnumerator<T>.Current { [DebuggerHidden] get; }

                object IEnumerator.Current { [DebuggerHidden] get; }
            }
        }

        public class ListHelper<T> : ODataLinqHelpers.ListHelperBase
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

        private class WrapperHelper
        {
            private readonly Type wrapperDataType;
            private readonly Type elementType;
            private readonly ConstructorInfo constructor;
            private readonly ConstructorInfo constructorFromWrapper;
            private readonly Func<object, object> createHandler;
            private readonly Func<object, object> createFromWrapperHandler;
            private readonly Dictionary<string, Func<object, object, object>> methodsDict;
            private readonly Func<object, CriteriaOperator, bool, int, int, IQueryable> groupByHandler;
            private readonly Dictionary<string, Func<object, object>> getPropertyDict;

            public WrapperHelper(Type wrapperDataType, Type elementType);
            public object CallMethod(string methodName, object instance, object parameter);
            private Func<object, object, object> CompileMethodCall(Type wrapperDataType, ParameterExpression instanceParameter, ParameterExpression objParameter, string methodName, Type argumentType);
            private Func<object, object> CompilePropertyGet(Type wrapperDataType, ParameterExpression instanceParameter, string propertyName);
            public object Create(IQueryable source);
            public object CreateFromWrapper(object wrapper);
            public object GetPropertyValue(string propetyName, object instance);
            public IQueryable GroupBy(object instance, CriteriaOperator key, bool isDesc, int skip, int top);

            public Type WrapperDataType { get; }

            public Type ElementType { get; }
        }
    }
}

