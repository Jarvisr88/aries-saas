namespace DevExpress.Data.TreeList
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.TreeList.DataHelpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class TreeListFilterHelper
    {
        public TreeListFilterHelper(TreeListDataControllerBase controller);
        public virtual CriteriaOperator CalcColumnFilterCriteriaByValue(DataColumnInfo columnInfo, object columnValue, bool roundDateTime, bool useDisplayText);
        [IteratorStateMachine(typeof(TreeListFilterHelper.<GetColumnValues>d__12))]
        protected virtual IEnumerable<object> GetColumnValues(DataColumnInfo columnInfo, CriteriaOperator criteria, bool includeFilteredOut, bool roundDateTime, bool displayText, bool implyNullLikeEmptyString);
        public virtual object[] GetUniqueColumnValuesCore(DataColumnInfo column, bool includeFilteredOut, bool roundDataTime, bool useDisplayText, bool implyNullLikeEmptyString);
        public virtual object[] GetUniqueColumnValuesCore(DataColumnInfo column, CriteriaOperator criteria, bool includeFilteredOut, bool roundDataTime, bool useDisplayText, bool implyNullLikeEmptyString);

        protected TreeListDataControllerBase Controller { get; private set; }

        protected IDataProvider DataProvider { get; }

        protected TreeListDataHelperBase DataHelper { get; }

        [CompilerGenerated]
        private sealed class <GetColumnValues>d__12 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public TreeListFilterHelper <>4__this;
            private bool displayText;
            public bool <>3__displayText;
            private DataColumnInfo columnInfo;
            public DataColumnInfo <>3__columnInfo;
            private CriteriaOperator criteria;
            public CriteriaOperator <>3__criteria;
            private bool includeFilteredOut;
            public bool <>3__includeFilteredOut;
            private Func<object, bool> <filterFitPredicate>5__1;
            private bool implyNullLikeEmptyString;
            public bool <>3__implyNullLikeEmptyString;
            private bool <isDateTimeColumn>5__2;
            private bool roundDateTime;
            public bool <>3__roundDateTime;
            private IEnumerator<TreeListNodeBase> <>7__wrap1;

            [DebuggerHidden]
            public <GetColumnValues>d__12(int <>1__state);
            private void <>m__Finally1();
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

