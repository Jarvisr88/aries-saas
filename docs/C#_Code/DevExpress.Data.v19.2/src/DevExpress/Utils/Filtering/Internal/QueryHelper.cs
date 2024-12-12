namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class QueryHelper
    {
        private static Type UniqueValuesType;
        private static Type ArrayListType;
        private static Type DataRowCollectionType;
        private static Type DataTableType;
        private static IDictionary<Type, Func<object, object>> ofTypeCache;
        private static MethodInfo enumerableOfTypeInfo;
        private static MethodInfo queryableOfTypeInfo;
        private static IDictionary<Type, Func<object, Func<object, bool>, bool>> anyCache;
        private static MethodInfo enumerableAnyInfo;
        private static MethodInfo queryableAnyInfo;
        private static readonly Type UntypedPredicate;
        private static IDictionary<Type, Func<object, int, object>> takeCache;
        private static MethodInfo enumerableTakeInfo;
        private static MethodInfo queryableTakeInfo;
        private static IDictionary<Type, Func<object, int>> countCache;
        private static MethodInfo enumerableCountInfo;
        private static MethodInfo queryableCountInfo;
        private static IDictionary<Type, Func<object, object>> distinctCache;
        private static MethodInfo enumerableDistinctInfo;
        private static MethodInfo queryableDistinctInfo;
        private static IDictionary<Type, Func<object, object>> selectCache;
        private static MethodInfo enumerableSelectInfo;
        private static MethodInfo queryableSelectInfo;
        private static IDictionary<Type, Func<object, object>> materializationCache;
        private static MethodInfo enumerableToListInfo;
        private static MethodInfo enumerableToArrayInfo;

        static QueryHelper();
        internal static bool Any<T>(object dataSource, Func<object, bool> predicate);
        private static bool AnyCore(Array array, Func<object, bool> predicate);
        private static bool AnyCore(object dataSource, Func<object, bool> predicate);
        internal static int Count<T>(object dataSource);
        private static int Count(object dataSource, Type elementType = null);
        internal static int CountDistinct<T>(object dataSource);
        private static Expression CreatePredicate(Func<object, bool> predicate, Type elementType, ParameterExpression pPredicate);
        private static object Distinct(object dataSource, Type valueType);
        private static DataRowCollection GetDataRows(object dataSource);
        [IteratorStateMachine(typeof(QueryHelper.<GetDataRowValues>d__41))]
        private static IEnumerable<object> GetDataRowValues(object dataSource, string member);
        internal static IReadOnlyCollection<T> GetValues<T>(object dataSource, string valueMember);
        private static object Materialize(object dataSource, bool forceToArray = false);
        private static object Select(object dataSource, string valueMember, Type valueType);
        private static object Take(object dataSource, int? count);
        internal static object Take<T>(object dataSource, int? count);
        internal static object TakeDistinct<T>(object dataSource, int? count);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly QueryHelper.<>c <>9;
            public static Func<DataTable, DataRowCollection> <>9__40_0;

            static <>c();
            internal bool <.cctor>b__43_0(MethodInfo x);
            internal bool <.cctor>b__43_1(MethodInfo x);
            internal object <.cctor>b__43_10(object dataSource);
            internal bool <.cctor>b__43_11(MethodInfo x);
            internal bool <.cctor>b__43_12(MethodInfo x);
            internal object <.cctor>b__43_13(object dataSource);
            internal object <.cctor>b__43_14(object dataSource);
            internal object <.cctor>b__43_15(object dataSource);
            internal object <.cctor>b__43_16(object dataSource);
            internal bool <.cctor>b__43_17(MethodInfo x);
            internal bool <.cctor>b__43_18(MethodInfo x);
            internal int <.cctor>b__43_2(object dataSource);
            internal int <.cctor>b__43_3(object dataSource);
            internal int <.cctor>b__43_4(object dataSource);
            internal int <.cctor>b__43_5(object dataSource);
            internal bool <.cctor>b__43_6(MethodInfo x);
            internal bool <.cctor>b__43_7(MethodInfo x);
            internal object <.cctor>b__43_8(object dataSource);
            internal object <.cctor>b__43_9(object dataSource);
            internal DataRowCollection <GetDataRows>b__40_0(DataTable table);
        }

        [CompilerGenerated]
        private sealed class <GetDataRowValues>d__41 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private object dataSource;
            public object <>3__dataSource;
            private DataRowCollection <rows>5__1;
            private string member;
            public string <>3__member;
            private int <i>5__2;

            [DebuggerHidden]
            public <GetDataRowValues>d__41(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

