namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class MultiElementOperatorMenuBuilder<T> : OperatorMenuBuilderBase<T, IExcelStyleOperatorMenuItemIdentity> where T: class
    {
        public MultiElementOperatorMenuBuilder(OperatorMenuItemFactory<T, IExcelStyleOperatorMenuItemIdentity> builder, FilterSelectorBuildInfo info, ExcelStyleOperatorMenuItemIdentityFactory identityFactory) : base(builder, info, identityFactory)
        {
            this.<IdentityFactory>k__BackingField = identityFactory;
        }

        [IteratorStateMachine(typeof(<GetFormatConditions>d__8))]
        protected override IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>> GetFormatConditions()
        {
            <GetFormatConditions>d__8<T> d__1 = new <GetFormatConditions>d__8<T>(-2);
            d__1.<>4__this = (MultiElementOperatorMenuBuilder<T>) this;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetOutlookIntervalOperators>d__13))]
        protected override IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>> GetOutlookIntervalOperators()
        {
            <GetOutlookIntervalOperators>d__13<T> d__1 = new <GetOutlookIntervalOperators>d__13<T>(-2);
            d__1.<>4__this = (MultiElementOperatorMenuBuilder<T>) this;
            return d__1;
        }

        [IteratorStateMachine(typeof(<GetPredefined>d__9))]
        protected override IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>> GetPredefined()
        {
            <GetPredefined>d__9<T> d__1 = new <GetPredefined>d__9<T>(-2);
            d__1.<>4__this = (MultiElementOperatorMenuBuilder<T>) this;
            return d__1;
        }

        private static bool IsDateTimeOperatorsFilter(CriteriaOperator filter, FilterRestrictions restrictions, string fieldName)
        {
            FilterDateType[] filters = DatePeriodsFilterModel.GetFilters(restrictions.AllowedDateTimeFilters);
            return ((filters.Length != 0) ? filter.ToFilters(fieldName, true).Intersect<FilterDateType>(filters).Any<FilterDateType>() : false);
        }

        protected override IExcelStyleOperatorMenuItemIdentity SelectDefaultID(Tree<IExcelStyleOperatorMenuItemIdentity, string>[] available) => 
            !this.HasPredefinedFilter ? base.SelectDefaultID(available) : this.IdentityFactory.CreatePredefined();

        protected override IExcelStyleOperatorMenuItemIdentity SelectFormatConditionID(FormatConditionFilter[] formatConditionFilters) => 
            formatConditionFilters.Any<FormatConditionFilter>() ? this.IdentityFactory.CreateFormatConditions() : null;

        protected override IExcelStyleOperatorMenuItemIdentity SelectID(CriteriaOperator filter, Tree<IdentifiedOperatorMenuItem<IExcelStyleOperatorMenuItemIdentity, T>, string>[] available, IExcelStyleOperatorMenuItemIdentity defaultID) => 
            !MultiElementOperatorMenuBuilder<T>.IsDateTimeOperatorsFilter(filter, base.Restrictions, base.FieldName) ? base.SelectID(filter, available, defaultID) : this.IdentityFactory.CreateDateOperators();

        protected override IExcelStyleOperatorMenuItemIdentity SelectPredefinedID(string[] predefinedNames) => 
            predefinedNames.Any<string>() ? this.IdentityFactory.CreatePredefined() : null;

        private ExcelStyleOperatorMenuItemIdentityFactory IdentityFactory { get; }

        private bool HasPredefinedFilter =>
            base.Info.PredefinedFilters.Any<PredefinedFilter>();

        private bool HasFormatConditions =>
            base.Info.FormatConditionFilters.Any<FormatConditionFilter>();

        [CompilerGenerated]
        private sealed class <GetFormatConditions>d__8 : IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IEnumerable, IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<IExcelStyleOperatorMenuItemIdentity, string> <>2__current;
            private int <>l__initialThreadId;
            public MultiElementOperatorMenuBuilder<T> <>4__this;

            [DebuggerHidden]
            public <GetFormatConditions>d__8(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.<>4__this.HasFormatConditions)
                    {
                        this.<>2__current = OperatorMenuBuilderBase<T, IExcelStyleOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreateFormatConditions());
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>> IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.GetEnumerator()
            {
                MultiElementOperatorMenuBuilder<T>.<GetFormatConditions>d__8 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (MultiElementOperatorMenuBuilder<T>.<GetFormatConditions>d__8) this;
                }
                else
                {
                    d__ = new MultiElementOperatorMenuBuilder<T>.<GetFormatConditions>d__8(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<DevExpress.Xpf.Core.FilteringUI.IExcelStyleOperatorMenuItemIdentity,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<IExcelStyleOperatorMenuItemIdentity, string> IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetOutlookIntervalOperators>d__13 : IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IEnumerable, IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<IExcelStyleOperatorMenuItemIdentity, string> <>2__current;
            private int <>l__initialThreadId;
            public MultiElementOperatorMenuBuilder<T> <>4__this;

            [DebuggerHidden]
            public <GetOutlookIntervalOperators>d__13(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.<>4__this.Restrictions.AllowedDateTimeFilters != AllowedDateTimeFilters.None)
                    {
                        this.<>2__current = OperatorMenuBuilderBase<T, IExcelStyleOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreateDateOperators());
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>> IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.GetEnumerator()
            {
                MultiElementOperatorMenuBuilder<T>.<GetOutlookIntervalOperators>d__13 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (MultiElementOperatorMenuBuilder<T>.<GetOutlookIntervalOperators>d__13) this;
                }
                else
                {
                    d__ = new MultiElementOperatorMenuBuilder<T>.<GetOutlookIntervalOperators>d__13(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<DevExpress.Xpf.Core.FilteringUI.IExcelStyleOperatorMenuItemIdentity,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<IExcelStyleOperatorMenuItemIdentity, string> IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetPredefined>d__9 : IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IEnumerable, IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<IExcelStyleOperatorMenuItemIdentity, string> <>2__current;
            private int <>l__initialThreadId;
            public MultiElementOperatorMenuBuilder<T> <>4__this;

            [DebuggerHidden]
            public <GetPredefined>d__9(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.<>4__this.HasPredefinedFilter)
                    {
                        this.<>2__current = OperatorMenuBuilderBase<T, IExcelStyleOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreatePredefined());
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>> IEnumerable<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.GetEnumerator()
            {
                MultiElementOperatorMenuBuilder<T>.<GetPredefined>d__9 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (MultiElementOperatorMenuBuilder<T>.<GetPredefined>d__9) this;
                }
                else
                {
                    d__ = new MultiElementOperatorMenuBuilder<T>.<GetPredefined>d__9(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<DevExpress.Xpf.Core.FilteringUI.IExcelStyleOperatorMenuItemIdentity,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<IExcelStyleOperatorMenuItemIdentity, string> IEnumerator<Tree<IExcelStyleOperatorMenuItemIdentity, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

