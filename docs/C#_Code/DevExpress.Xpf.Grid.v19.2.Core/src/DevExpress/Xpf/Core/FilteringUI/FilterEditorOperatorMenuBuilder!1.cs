namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class FilterEditorOperatorMenuBuilder<T> : OperatorMenuBuilderBase<T, IFilterEditorOperatorMenuItemIdentity> where T: class
    {
        public FilterEditorOperatorMenuBuilder(OperatorMenuItemFactory<T, IFilterEditorOperatorMenuItemIdentity> builder, FilterSelectorBuildInfo info, FilterEditorOperatorMenuItemIdentityFactory identityFactory) : base(builder, info, identityFactory)
        {
            this.<IdentityFactory>k__BackingField = identityFactory;
        }

        [IteratorStateMachine(typeof(<GetAnyOfOperators>d__9))]
        protected override IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> GetAnyOfOperators()
        {
            if (this.Restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.AnyOf))
            {
                yield return CreateID(this.IdentityFactory.CreateAnyOf());
            }
            if (this.Restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.NoneOf))
            {
                yield return CreateID(this.IdentityFactory.CreateNoneOf());
            }
        }

        protected override IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> GetFormatConditions()
        {
            Func<FormatConditionFilter, FormatConditionFilterEditorOperatorMenuItemIdentity> selector = <>c<T>.<>9__4_0;
            if (<>c<T>.<>9__4_0 == null)
            {
                Func<FormatConditionFilter, FormatConditionFilterEditorOperatorMenuItemIdentity> local1 = <>c<T>.<>9__4_0;
                selector = <>c<T>.<>9__4_0 = x => new FormatConditionFilterEditorOperatorMenuItemIdentity(x);
            }
            FormatConditionFilterEditorOperatorMenuItemIdentity[] ids = base.Info.FormatConditionFilters.Select<FormatConditionFilter, FormatConditionFilterEditorOperatorMenuItemIdentity>(selector).ToArray<FormatConditionFilterEditorOperatorMenuItemIdentity>();
            Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> evaluator = <>c<T>.<>9__4_1;
            if (<>c<T>.<>9__4_1 == null)
            {
                Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> local2 = <>c<T>.<>9__4_1;
                evaluator = <>c<T>.<>9__4_1 = x => x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();
            }
            return CreateGroup(GetString(EditorStringId.FormatConditionFilters), ids).Return<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>>(evaluator, (<>c<T>.<>9__4_2 ??= () => Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>()));
        }

        [IteratorStateMachine(typeof(<GetIsOnDates>d__10))]
        protected override IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> GetIsOnDates()
        {
            <GetIsOnDates>d__10<T> d__1 = new <GetIsOnDates>d__10<T>(-2);
            d__1.<>4__this = (FilterEditorOperatorMenuBuilder<T>) this;
            return d__1;
        }

        protected override IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> GetOutlookIntervalOperators()
        {
            FunctionOperatorType[] typeArray1 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalBeyondThisYear, FunctionOperatorType.IsOutlookIntervalLaterThisYear, FunctionOperatorType.IsOutlookIntervalLaterThisMonth, FunctionOperatorType.IsOutlookIntervalNextWeek, FunctionOperatorType.IsOutlookIntervalLaterThisWeek, FunctionOperatorType.IsOutlookIntervalTomorrow, FunctionOperatorType.IsOutlookIntervalToday, FunctionOperatorType.IsOutlookIntervalYesterday, FunctionOperatorType.IsOutlookIntervalEarlierThisWeek, FunctionOperatorType.IsOutlookIntervalLastWeek, FunctionOperatorType.IsOutlookIntervalEarlierThisMonth, FunctionOperatorType.IsOutlookIntervalEarlierThisYear, FunctionOperatorType.IsOutlookIntervalPriorThisYear };
            IFilterEditorOperatorMenuItemIdentity[] ids = (from type in typeArray1
                where base.Restrictions.Allow(type)
                select base.IdentityFactory.CreateFunction(type)).ToArray<IFilterEditorOperatorMenuItemIdentity>();
            Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> evaluator = <>c<T>.<>9__8_2;
            if (<>c<T>.<>9__8_2 == null)
            {
                Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> local1 = <>c<T>.<>9__8_2;
                evaluator = <>c<T>.<>9__8_2 = x => x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();
            }
            return CreateGroup(GetString(EditorStringId.DateIntervalsMenuCaption), ids).Return<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>>(evaluator, (<>c<T>.<>9__8_3 ??= () => Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>()));
        }

        protected override IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> GetPredefined()
        {
            Func<PredefinedFilter, PredefinedFilterEditorOperatorMenuItemIdentity> selector = <>c<T>.<>9__5_0;
            if (<>c<T>.<>9__5_0 == null)
            {
                Func<PredefinedFilter, PredefinedFilterEditorOperatorMenuItemIdentity> local1 = <>c<T>.<>9__5_0;
                selector = <>c<T>.<>9__5_0 = x => new PredefinedFilterEditorOperatorMenuItemIdentity(x.Name);
            }
            PredefinedFilterEditorOperatorMenuItemIdentity[] ids = base.Info.PredefinedFilters.Select<PredefinedFilter, PredefinedFilterEditorOperatorMenuItemIdentity>(selector).ToArray<PredefinedFilterEditorOperatorMenuItemIdentity>();
            Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> evaluator = <>c<T>.<>9__5_1;
            if (<>c<T>.<>9__5_1 == null)
            {
                Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> local2 = <>c<T>.<>9__5_1;
                evaluator = <>c<T>.<>9__5_1 = x => x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();
            }
            return CreateGroup(GetString(EditorStringId.PredefinedFilters), ids).Return<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>>(evaluator, (<>c<T>.<>9__5_2 ??= () => Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>()));
        }

        protected override IFilterEditorOperatorMenuItemIdentity SelectFormatConditionID(FormatConditionFilter[] formatConditionFilters) => 
            (formatConditionFilters.Length == 1) ? new FormatConditionFilterEditorOperatorMenuItemIdentity(formatConditionFilters[0]) : null;

        protected override IFilterEditorOperatorMenuItemIdentity SelectIn() => 
            this.IdentityFactory.CreateAnyOf();

        protected override IFilterEditorOperatorMenuItemIdentity SelectIsOnDates() => 
            this.IdentityFactory.CreateIsOnDates();

        protected override IFilterEditorOperatorMenuItemIdentity SelectNot(IFilterEditorOperatorMenuItemIdentity identity) => 
            ((identity == null) || !identity.Equals(this.IdentityFactory.CreateAnyOf())) ? base.SelectNot(identity) : this.IdentityFactory.CreateNoneOf();

        protected override IFilterEditorOperatorMenuItemIdentity SelectPredefinedID(string[] predefinedNames) => 
            (predefinedNames.Length == 1) ? new PredefinedFilterEditorOperatorMenuItemIdentity(predefinedNames[0]) : null;

        private FilterEditorOperatorMenuItemIdentityFactory IdentityFactory { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterEditorOperatorMenuBuilder<T>.<>c <>9;
            public static Func<FormatConditionFilter, FormatConditionFilterEditorOperatorMenuItemIdentity> <>9__4_0;
            public static Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__4_1;
            public static Func<IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__4_2;
            public static Func<PredefinedFilter, PredefinedFilterEditorOperatorMenuItemIdentity> <>9__5_0;
            public static Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__5_1;
            public static Func<IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__5_2;
            public static Func<Tree<IFilterEditorOperatorMenuItemIdentity, string>, IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__8_2;
            public static Func<IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>> <>9__8_3;

            static <>c()
            {
                FilterEditorOperatorMenuBuilder<T>.<>c.<>9 = new FilterEditorOperatorMenuBuilder<T>.<>c();
            }

            internal FormatConditionFilterEditorOperatorMenuItemIdentity <GetFormatConditions>b__4_0(FormatConditionFilter x) => 
                new FormatConditionFilterEditorOperatorMenuItemIdentity(x);

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetFormatConditions>b__4_1(Tree<IFilterEditorOperatorMenuItemIdentity, string> x) => 
                x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetFormatConditions>b__4_2() => 
                Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetOutlookIntervalOperators>b__8_2(Tree<IFilterEditorOperatorMenuItemIdentity, string> x) => 
                x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetOutlookIntervalOperators>b__8_3() => 
                Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();

            internal PredefinedFilterEditorOperatorMenuItemIdentity <GetPredefined>b__5_0(PredefinedFilter x) => 
                new PredefinedFilterEditorOperatorMenuItemIdentity(x.Name);

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetPredefined>b__5_1(Tree<IFilterEditorOperatorMenuItemIdentity, string> x) => 
                x.Yield<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();

            internal IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>> <GetPredefined>b__5_2() => 
                Enumerable.Empty<Tree<IFilterEditorOperatorMenuItemIdentity, string>>();
        }

        [CompilerGenerated]
        private sealed class <GetAnyOfOperators>d__9 : IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>, IEnumerable, IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<IFilterEditorOperatorMenuItemIdentity, string> <>2__current;
            private int <>l__initialThreadId;
            public FilterEditorOperatorMenuBuilder<T> <>4__this;

            [DebuggerHidden]
            public <GetAnyOfOperators>d__9(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if (!this.<>4__this.Restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.AnyOf))
                        {
                            break;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<T, IFilterEditorOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreateAnyOf());
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_0003;

                    default:
                        return false;
                }
                if (this.<>4__this.Restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.NoneOf))
                {
                    this.<>2__current = OperatorMenuBuilderBase<T, IFilterEditorOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreateNoneOf());
                    this.<>1__state = 2;
                    return true;
                }
            TR_0003:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>> IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>.GetEnumerator()
            {
                FilterEditorOperatorMenuBuilder<T>.<GetAnyOfOperators>d__9 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (FilterEditorOperatorMenuBuilder<T>.<GetAnyOfOperators>d__9) this;
                }
                else
                {
                    d__ = new FilterEditorOperatorMenuBuilder<T>.<GetAnyOfOperators>d__9(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<DevExpress.Xpf.Core.FilteringUI.IFilterEditorOperatorMenuItemIdentity,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<IFilterEditorOperatorMenuItemIdentity, string> IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetIsOnDates>d__10 : IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>, IEnumerable, IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<IFilterEditorOperatorMenuItemIdentity, string> <>2__current;
            private int <>l__initialThreadId;
            public FilterEditorOperatorMenuBuilder<T> <>4__this;

            [DebuggerHidden]
            public <GetIsOnDates>d__10(int <>1__state)
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
                    this.<>2__current = OperatorMenuBuilderBase<T, IFilterEditorOperatorMenuItemIdentity>.CreateID(this.<>4__this.IdentityFactory.CreateIsOnDates());
                    this.<>1__state = 1;
                    return true;
                }
                if (num == 1)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>> IEnumerable<Tree<IFilterEditorOperatorMenuItemIdentity, string>>.GetEnumerator()
            {
                FilterEditorOperatorMenuBuilder<T>.<GetIsOnDates>d__10 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (FilterEditorOperatorMenuBuilder<T>.<GetIsOnDates>d__10) this;
                }
                else
                {
                    d__ = new FilterEditorOperatorMenuBuilder<T>.<GetIsOnDates>d__10(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<DevExpress.Xpf.Core.FilteringUI.IFilterEditorOperatorMenuItemIdentity,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<IFilterEditorOperatorMenuItemIdentity, string> IEnumerator<Tree<IFilterEditorOperatorMenuItemIdentity, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

