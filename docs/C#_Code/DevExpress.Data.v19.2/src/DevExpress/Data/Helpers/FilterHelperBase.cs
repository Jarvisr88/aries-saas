namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FilterHelperBase
    {
        private DataControllerBase controller;
        private VisibleListSourceRowCollection visibleListSourceRows;
        public static bool? AvoidInvalidColumnFilterCriteriaBasedOnFilterValueType;

        public FilterHelperBase(DataControllerBase controller, VisibleListSourceRowCollection visibleListSourceRows);
        public virtual CriteriaOperator CalcColumnFilterCriteriaByValue(DataColumnInfo columnInfo, object columnValue, bool equal, bool roundDateTime, IFormatProvider provider);
        public static CriteriaOperator CalcColumnFilterCriteriaByValue(string columnName, Type columnFilteredType, object value, bool roundDateTime, IFormatProvider provider);
        public static CriteriaOperator CalcColumnFilterCriteriaByValue(string columnName, Type columnFilteredType, object value, bool roundDateTime, IFormatProvider provider, bool isServerMode);
        public string CalcColumnFilterStringByValue(DataColumnInfo columnInfo, object columnValue, bool equal, bool roundDateTime, IFormatProvider provider);
        public static string ConvertDateToString(object val, IFormatProvider provider);
        public static DateTime ConvertToDate(object val, IFormatProvider provider);
        public static object CorrectFilterValueType(Type columnFilteredType, object filteredValue);
        public static object CorrectFilterValueType(Type columnType, object filteredValue, IFormatProvider provider);
        [IteratorStateMachine(typeof(FilterHelperBase.<GetColumnValues>d__12))]
        protected virtual IEnumerable<object> GetColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, bool displayText);
        protected IEnumerable<object> GetColumnValues(DataColumnInfo columnInfo, int maxCount, bool ignoreAppliedFilter, bool roundDateTime, bool displayText);
        protected IEnumerable<object> GetColumnValues(DataColumnInfo columnInfo, int maxCount, bool ignoreAppliedFilter, bool roundDateTime, bool displayText, bool implyNullLikeEmptyStringWhenFiltering);
        protected virtual object[] GetFilteredColumnValues(int column, ColumnValuesArguments args, bool displayText);
        protected virtual IEnumerable<int> GetRowIndices(CriteriaOperator filter, bool ignoreAppliedFilter);
        public virtual object[] GetUniqueColumnValues(DataColumnInfo columnInfo, ColumnValuesArguments args, OperationCompleted completed);
        public object[] GetUniqueColumnValues(DataColumnInfo columnInfo, int maxCount, bool ignoreAppliedFilter, bool roundDataTime, OperationCompleted completed);
        public object[] GetUniqueColumnValues(DataColumnInfo columnInfo, int maxCount, bool ignoreAppliedFilter, bool roundDataTime, OperationCompleted completed, bool implyNullLikeEmptyStringWhenFiltering);
        public object[] GetUniqueColumnValues(DataColumnInfo columnInfo, int maxCount, CriteriaOperator filter, bool ignoreAppliedFilter, bool roundDataTime, OperationCompleted completed, bool implyNullLikeEmptyStringWhenFiltering);
        protected bool IsRequiredDisplayText(DataColumnInfo column);
        protected bool IsRequiredDisplayText(int column);
        public static bool TryCorrectFilterValueType(Type columnType, object filteredValue, IFormatProvider provider, out object value);
        protected virtual bool YieldColumnValue(bool implyNullLikeEmptyString, bool useDateTimeColumnRounding, ref object value);

        public DataControllerBase Controller { get; }

        public virtual VisibleListSourceRowCollection VisibleListSourceRows { get; }

        [CompilerGenerated]
        private sealed class <GetColumnValues>d__12 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            public FilterHelperBase <>4__this;
            private ColumnValuesArguments args;
            public ColumnValuesArguments <>3__args;
            private DataColumnInfo columnInfo;
            public DataColumnInfo <>3__columnInfo;
            private bool displayText;
            public bool <>3__displayText;
            private bool <implyNullLikeEmptyString>5__1;
            private bool <useDateTimeColumnRounding>5__2;
            private IEnumerator<int> <>7__wrap1;

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

