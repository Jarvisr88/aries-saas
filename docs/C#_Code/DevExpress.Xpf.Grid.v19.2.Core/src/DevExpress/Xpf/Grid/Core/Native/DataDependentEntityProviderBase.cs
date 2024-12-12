namespace DevExpress.Xpf.Grid.Core.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal abstract class DataDependentEntityProviderBase
    {
        private readonly DataViewBase view;

        public DataDependentEntityProviderBase(DataViewBase view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
        }

        private static IEnumerable<string> GetColumnPath(ColumnBase column, BindingParseResult parsedBinding) => 
            (column.Binding == null) ? GetColumnPathCore(column.IsUnbound, column.UnboundExpression, column.FieldName) : ((IEnumerable<string>) parsedBinding.Properties);

        private static IEnumerable<string> GetColumnPathCore(bool isUnbound, string unboundExpression, string fieldName)
        {
            if (isUnbound)
            {
                string[] source = CriteriaPropertyNameParser.Parse(unboundExpression);
                if (source.Any<string>())
                {
                    return source;
                }
            }
            return (string.IsNullOrEmpty(fieldName) ? Enumerable.Empty<string>() : fieldName.Yield<string>());
        }

        [IteratorStateMachine(typeof(<GetDataDependentEntityInfo>d__3))]
        public virtual IEnumerable<DataDependentEntityInfo> GetDataDependentEntityInfo()
        {
            <GetDataDependentEntityInfo>d__3 d__1 = new <GetDataDependentEntityInfo>d__3(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private static DataDependentEntity GetFormatConditionDataDependentEntity(FormatConditionBase condition, IColumnCollection columns)
        {
            if (condition.IsValid)
            {
                if (string.IsNullOrEmpty(condition.FieldName) || condition.ApplyToRow)
                {
                    return new DataDependentEntity().AddRowConditions();
                }
                ColumnBase column = columns[condition.FieldName];
                if (column != null)
                {
                    return new DataDependentEntity().AddColumn(column);
                }
            }
            return new DataDependentEntity();
        }

        private static IEnumerable<string> GetSortInfoPath(DataColumnSortInfo sortInfo)
        {
            DataMergedColumnSortInfo info = sortInfo as DataMergedColumnSortInfo;
            if (info == null)
            {
                string name = sortInfo.ColumnInfo.Name;
                return (string.IsNullOrEmpty(name) ? Enumerable.Empty<string>() : name.Yield<string>());
            }
            Func<DataColumnSortInfo, IEnumerable<string>> selector = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<DataColumnSortInfo, IEnumerable<string>> local1 = <>c.<>9__7_0;
                selector = <>c.<>9__7_0 = childInfo => GetSortInfoPath(childInfo);
            }
            return info.Infos.SelectMany<DataColumnSortInfo, string>(selector);
        }

        [IteratorStateMachine(typeof(<GetSortingDataDependentEntityInfo>d__11))]
        protected static IEnumerable<DataDependentEntityInfo> GetSortingDataDependentEntityInfo(IList<DataColumnSortInfo> sortInfo, int groupCount = 0)
        {
            <GetSortingDataDependentEntityInfo>d__11 d__1 = new <GetSortingDataDependentEntityInfo>d__11(-2);
            d__1.<>3__sortInfo = sortInfo;
            d__1.<>3__groupCount = groupCount;
            return d__1;
        }

        protected static IEnumerable<DataDependentEntityInfo> GetSummaryDataDependentEntityInfo(IEnumerable<SummaryItem> summary, Func<SummaryItem, DataDependentEntity> createDataDependentEntity) => 
            from summaryItem in summary select new DataDependentEntityInfo(GetSummaryPath(summaryItem), createDataDependentEntity(summaryItem), summaryItem.IsCustomSummary);

        private static IEnumerable<string> GetSummaryPath(SummaryItem summaryItem)
        {
            DataColumnInfo columnInfo = summaryItem.ColumnInfo;
            return ((columnInfo != null) ? GetColumnPathCore(columnInfo.Unbound, columnInfo.UnboundExpression, columnInfo.Name) : Enumerable.Empty<string>());
        }

        private static bool IsAffectedByCustomUnboundColumnDataEvent(ColumnBase column, bool hasCustomUnboundColumnDataSubscription) => 
            hasCustomUnboundColumnDataSubscription && ((column.UnboundType != UnboundColumnType.Bound) && ReferenceEquals(column.DisplayMemberBindingCalculator, null));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataDependentEntityProviderBase.<>c <>9 = new DataDependentEntityProviderBase.<>c();
            public static Func<DataColumnSortInfo, IEnumerable<string>> <>9__7_0;

            internal IEnumerable<string> <GetSortInfoPath>b__7_0(DataColumnSortInfo childInfo) => 
                DataDependentEntityProviderBase.GetSortInfoPath(childInfo);
        }

        [CompilerGenerated]
        private sealed class <GetDataDependentEntityInfo>d__3 : IEnumerable<DataDependentEntityInfo>, IEnumerable, IEnumerator<DataDependentEntityInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DataDependentEntityInfo <>2__current;
            private int <>l__initialThreadId;
            public DataDependentEntityProviderBase <>4__this;
            private DataControlBase <grid>5__1;
            private IEnumerator <>7__wrap1;
            private IEnumerator<FormatConditionBase> <>7__wrap2;

            [DebuggerHidden]
            public <GetDataDependentEntityInfo>d__3(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                IDisposable disposable = this.<>7__wrap1 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private void <>m__Finally2()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap2 != null)
                {
                    this.<>7__wrap2.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<grid>5__1 = this.<>4__this.view.DataControl;
                            if (this.<grid>5__1 != null)
                            {
                                this.<>7__wrap1 = this.<grid>5__1.ColumnsCore.GetEnumerator();
                                this.<>1__state = -3;
                            }
                            else
                            {
                                return false;
                            }
                            goto TR_0006;

                        case 1:
                            this.<>1__state = -3;
                            goto TR_0006;

                        case 2:
                            this.<>1__state = -1;
                            this.<>7__wrap2 = this.<>4__this.view.GetFormatConditions().GetEnumerator();
                            this.<>1__state = -4;
                            break;

                        case 3:
                            this.<>1__state = -4;
                            break;

                        default:
                            return false;
                    }
                    if (!this.<>7__wrap2.MoveNext())
                    {
                        this.<>m__Finally2();
                        this.<>7__wrap2 = null;
                        flag = false;
                    }
                    else
                    {
                        FormatConditionBase current = this.<>7__wrap2.Current;
                        IEnumerable<string> path = DataDependentEntityProviderBase.GetColumnPathCore(true, current.Expression, current.FieldName);
                        this.<>2__current = new DataDependentEntityInfo(path, DataDependentEntityProviderBase.GetFormatConditionDataDependentEntity(current, this.<grid>5__1.ColumnsCore), false);
                        this.<>1__state = 3;
                        flag = true;
                    }
                    return flag;
                TR_0006:
                    if (!this.<>7__wrap1.MoveNext())
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        this.<>2__current = new DataDependentEntityInfo(DataDependentEntityProviderBase.CriteriaPropertyNameParser.Parse(this.<grid>5__1.FilterString), new DataDependentEntity().AddFilter(), false);
                        this.<>1__state = 2;
                        flag = true;
                    }
                    else
                    {
                        ColumnBase current = (ColumnBase) this.<>7__wrap1.Current;
                        BindingParseResult parsedBinding = BindingParser.Parse(current.Binding);
                        DataDependentEntity dataDependentEntity = new DataDependentEntity().AddColumn(current);
                        this.<>2__current = new DataDependentEntityInfo(DataDependentEntityProviderBase.GetColumnPath(current, parsedBinding), dataDependentEntity, parsedBinding.Row || DataDependentEntityProviderBase.IsAffectedByCustomUnboundColumnDataEvent(current, this.<grid>5__1.HasCustomUnboundColumnDataSubscription));
                        this.<>1__state = 1;
                        flag = true;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<DataDependentEntityInfo> IEnumerable<DataDependentEntityInfo>.GetEnumerator()
            {
                DataDependentEntityProviderBase.<GetDataDependentEntityInfo>d__3 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new DataDependentEntityProviderBase.<GetDataDependentEntityInfo>d__3(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Grid.Core.Native.DataDependentEntityInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (num > -3)
                {
                    if (num == 1)
                    {
                        goto TR_0003;
                    }
                    else if (num != 3)
                    {
                        return;
                    }
                }
                else if (num != -4)
                {
                    if (num != -3)
                    {
                        return;
                    }
                    goto TR_0003;
                }
                try
                {
                }
                finally
                {
                    this.<>m__Finally2();
                }
                return;
            TR_0003:
                try
                {
                }
                finally
                {
                    this.<>m__Finally1();
                }
            }

            DataDependentEntityInfo IEnumerator<DataDependentEntityInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetSortingDataDependentEntityInfo>d__11 : IEnumerable<DataDependentEntityInfo>, IEnumerable, IEnumerator<DataDependentEntityInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DataDependentEntityInfo <>2__current;
            private int <>l__initialThreadId;
            private int groupCount;
            public int <>3__groupCount;
            private IList<DataColumnSortInfo> sortInfo;
            public IList<DataColumnSortInfo> <>3__sortInfo;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetSortingDataDependentEntityInfo>d__11(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.sortInfo.Count)
                {
                    return false;
                }
                DataDependentEntity dataDependentEntity = (this.<i>5__1 < this.groupCount) ? new DataDependentEntity().AddGrouping() : new DataDependentEntity().AddSorting();
                this.<>2__current = new DataDependentEntityInfo(DataDependentEntityProviderBase.GetSortInfoPath(this.sortInfo[this.<i>5__1]), dataDependentEntity, false);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<DataDependentEntityInfo> IEnumerable<DataDependentEntityInfo>.GetEnumerator()
            {
                DataDependentEntityProviderBase.<GetSortingDataDependentEntityInfo>d__11 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DataDependentEntityProviderBase.<GetSortingDataDependentEntityInfo>d__11(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.sortInfo = this.<>3__sortInfo;
                d__.groupCount = this.<>3__groupCount;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Grid.Core.Native.DataDependentEntityInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            DataDependentEntityInfo IEnumerator<DataDependentEntityInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private sealed class CriteriaPropertyNameParser : ClientCriteriaVisitorBase
        {
            private List<string> descriptorPaths = new List<string>();

            private CriteriaPropertyNameParser()
            {
            }

            public static string[] Parse(CriteriaOperator op)
            {
                DataDependentEntityProviderBase.CriteriaPropertyNameParser parser = new DataDependentEntityProviderBase.CriteriaPropertyNameParser();
                parser.Process(op);
                return parser.descriptorPaths.ToArray();
            }

            public static string[] Parse(string opString) => 
                !string.IsNullOrEmpty(opString) ? Parse(CriteriaOperator.TryParse(opString, new object[0])) : new string[0];

            protected override CriteriaOperator Visit(OperandProperty theOperand)
            {
                this.descriptorPaths.Add(theOperand.PropertyName);
                return base.Visit(theOperand);
            }
        }
    }
}

