namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal abstract class OperatorMenuBuilderBase<TItem, TID> where TItem: class where TID: class
    {
        private FilterSelector<TItem> selectorCore;

        public OperatorMenuBuilderBase(OperatorMenuItemFactory<TItem, TID> builder, FilterSelectorBuildInfo info, OperatorMenuItemIdentityFactoryBase<TID> identityFactory)
        {
            Guard.ArgumentNotNull(builder, "builder");
            Guard.ArgumentNotNull(info, "info");
            Guard.ArgumentNotNull(identityFactory, "identityFactory");
            this.<Builder>k__BackingField = builder;
            this.<Info>k__BackingField = info;
            this.<IdentityFactory>k__BackingField = identityFactory;
        }

        [IteratorStateMachine(typeof(<AddStringContentOperators>d__24))]
        private IEnumerable<Tree<TID, string>> AddStringContentOperators()
        {
            if (this.Restrictions.Allow(FunctionOperatorType.Contains))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateFunction(FunctionOperatorType.Contains));
            }
            if (this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.DoesNotContain))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateDoesNotContain());
            }
            if (this.Restrictions.Allow(FunctionOperatorType.StartsWith))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateFunction(FunctionOperatorType.StartsWith));
            }
            if (this.Restrictions.Allow(FunctionOperatorType.EndsWith))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateFunction(FunctionOperatorType.EndsWith));
            }
            if (this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.Like))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateLike());
            }
            while (true)
            {
                if (this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.NotLike))
                {
                    yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateNotLike());
                }
            }
        }

        protected static Tree<TID, string> CreateGroup(string groupName, TID[] ids)
        {
            if ((ids == null) || (ids.Length == 0))
            {
                return null;
            }
            if (ids.Length == 1)
            {
                return OperatorMenuBuilderBase<TItem, TID>.CreateID(ids[0]);
            }
            Tree<TID, string>[] children = ids.Select<TID, Tree<TID, string>>(new Func<TID, Tree<TID, string>>(Tree<TID, string>.CreateLeaf)).ToArray<Tree<TID, string>>();
            return Tree<TID, string>.CreateGroup(groupName, children);
        }

        protected static Tree<TID, string> CreateID(TID id) => 
            Tree<TID, string>.CreateLeaf(id);

        [IteratorStateMachine(typeof(<GetAnyOfOperators>d__22))]
        protected virtual IEnumerable<Tree<TID, string>> GetAnyOfOperators() => 
            new <GetAnyOfOperators>d__22<TItem, TID>(-2);

        private Tree<TID, string>[] GetAvailableIds() => 
            this.GetAvailableIdsInternal().Concat<Tree<TID, string>>(this.GetPredefinedFormatCondition()).Concat<Tree<TID, string>>(this.GetFormatConditions()).Concat<Tree<TID, string>>(this.GetPredefined()).ToArray<Tree<TID, string>>();

        private IEnumerable<Tree<TID, string>> GetAvailableIdsInternal()
        {
            switch (this.Info.Category)
            {
                case OperatorMenuCategory.Selector:
                    return this.GetEqualityOperators().Concat<Tree<TID, string>>(this.GetNullOperators()).Concat<Tree<TID, string>>(this.GetAnyOfOperators());

                case OperatorMenuCategory.Object:
                    return this.GetNullOperators();

                case OperatorMenuCategory.String:
                    return this.GetEqualityOperators().Concat<Tree<TID, string>>(this.GetComparisonOperators()).Concat<Tree<TID, string>>(this.GetRangeOperators()).Concat<Tree<TID, string>>(this.AddStringContentOperators()).Concat<Tree<TID, string>>(this.GetAnyOfOperators()).Concat<Tree<TID, string>>(this.GetBlankOperators());

                case OperatorMenuCategory.DateTime:
                {
                    Func<IEnumerable<Tree<TID, string>>, IEnumerable<Tree<TID, string>>> selector = <>c<TItem, TID>.<>9__18_0;
                    if (<>c<TItem, TID>.<>9__18_0 == null)
                    {
                        Func<IEnumerable<Tree<TID, string>>, IEnumerable<Tree<TID, string>>> local1 = <>c<TItem, TID>.<>9__18_0;
                        selector = <>c<TItem, TID>.<>9__18_0 = x => x;
                    }
                    return this.GetDateComparisonOperators(false).SelectMany<IEnumerable<Tree<TID, string>>, Tree<TID, string>>(selector).Concat<Tree<TID, string>>(this.GetComparisonOperators()).Concat<Tree<TID, string>>(this.GetDateComparisonOperators(true).SelectMany<IEnumerable<Tree<TID, string>>, Tree<TID, string>>((<>c<TItem, TID>.<>9__18_1 ??= x => x))).Concat<Tree<TID, string>>(this.GetNullOperators()).Concat<Tree<TID, string>>(this.GetOutlookIntervalOperators());
                }
                case OperatorMenuCategory.Numeric:
                    return this.GetEqualityOperators().Concat<Tree<TID, string>>(this.GetComparisonOperators()).Concat<Tree<TID, string>>(this.GetRangeOperators()).Concat<Tree<TID, string>>(this.GetNullOperators()).Concat<Tree<TID, string>>(this.GetAnyOfOperators());

                case OperatorMenuCategory.Boolean:
                    return this.GetEqualityOperators().Concat<Tree<TID, string>>(this.GetNullOperators());
            }
            return Enumerable.Empty<Tree<TID, string>>();
        }

        [IteratorStateMachine(typeof(<GetBlankOperators>d__25))]
        private IEnumerable<Tree<TID, string>> GetBlankOperators()
        {
            if (this.Restrictions.Allow(FunctionOperatorType.IsNullOrEmpty))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateFunction(FunctionOperatorType.IsNullOrEmpty));
            }
            if (this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNotNullOrEmpty))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateIsNotNullOrEmpty());
            }
        }

        private IEnumerable<Tree<TID, string>> GetComparisonOperators()
        {
            BinaryOperatorType[] typeArray1 = new BinaryOperatorType[] { BinaryOperatorType.Greater, BinaryOperatorType.GreaterOrEqual, BinaryOperatorType.Less, BinaryOperatorType.LessOrEqual };
            return (from type in typeArray1
                where base.Restrictions.Allow(type)
                select OperatorMenuBuilderBase<TItem, TID>.CreateID(base.IdentityFactory.CreateBinary(type)));
        }

        [IteratorStateMachine(typeof(<GetDateComparisonOperators>d__26))]
        private IEnumerable<IEnumerable<Tree<TID, string>>> GetDateComparisonOperators(bool isRange)
        {
            if (!this.Info.RoundDateTime)
            {
                yield return (isRange ? this.GetRangeOperators() : this.GetEqualityOperators());
            }
            else
            {
                if (!this.Restrictions.AllowCustomDateFilters)
                {
                    yield break;
                }
                if (isRange)
                {
                    yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateBetweenDates()).Yield<Tree<TID, string>>();
                    yield return this.GetIsOnDates();
                }
                else
                {
                    yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateIsOnDate()).Yield<Tree<TID, string>>();
                    yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateIsNotOnDate()).Yield<Tree<TID, string>>();
                }
            }
        }

        private IEnumerable<Tree<TID, string>> GetEqualityOperators()
        {
            BinaryOperatorType[] typeArray1 = new BinaryOperatorType[2];
            typeArray1[1] = BinaryOperatorType.NotEqual;
            return (from type in typeArray1
                where base.Restrictions.Allow(type)
                select OperatorMenuBuilderBase<TItem, TID>.CreateID(base.IdentityFactory.CreateBinary(type)));
        }

        protected abstract IEnumerable<Tree<TID, string>> GetFormatConditions();
        [IteratorStateMachine(typeof(<GetIsOnDates>d__27))]
        protected virtual IEnumerable<Tree<TID, string>> GetIsOnDates() => 
            new <GetIsOnDates>d__27<TItem, TID>(-2);

        [IteratorStateMachine(typeof(<GetNullOperators>d__23))]
        private IEnumerable<Tree<TID, string>> GetNullOperators()
        {
            if (!this.Info.IsNullable)
            {
            }
            if (this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNull))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateIsNull());
            }
            if (this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNotNull))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateIsNotNull());
            }
        }

        protected abstract IEnumerable<Tree<TID, string>> GetOutlookIntervalOperators();
        protected abstract IEnumerable<Tree<TID, string>> GetPredefined();
        [IteratorStateMachine(typeof(<GetPredefinedFormatCondition>d__31))]
        private IEnumerable<Tree<TID, string>> GetPredefinedFormatCondition()
        {
            switch (this.Info.Category)
            {
                case OperatorMenuCategory.Selector:
                {
                }
                case OperatorMenuCategory.Object:
                case OperatorMenuCategory.String:
                case OperatorMenuCategory.Boolean:
                    break;

                case OperatorMenuCategory.DateTime:
                case OperatorMenuCategory.Numeric:
                    if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.Top))
                    {
                        yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Top));
                    }
                    if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.Bottom))
                    {
                        yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Bottom));
                    }
                    goto TR_000D;

                default:
                {
                }
            }
        TR_0007:
            if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.Unique))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Unique));
            }
            while (true)
            {
                if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.Duplicate))
                {
                    yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Duplicate));
                }
            }
        TR_000D:
            if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.AboveAverage))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.AboveAverage));
            }
            if (this.Info.Restrictions.Allow(PredefinedFormatConditionType.BelowAverage))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.BelowAverage));
            }
            goto TR_0007;
        }

        [IteratorStateMachine(typeof(<GetRangeOperators>d__21))]
        private IEnumerable<Tree<TID, string>> GetRangeOperators()
        {
            if (this.Restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.Between))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateBetween());
            }
            if (this.Restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.NotBetween))
            {
                yield return OperatorMenuBuilderBase<TItem, TID>.CreateID(this.IdentityFactory.CreateNotBetween());
            }
        }

        protected static string GetString(EditorStringId id) => 
            EditorLocalizer.GetString(id);

        protected virtual TID SelectDefaultID(Tree<TID, string>[] available)
        {
            TID local = default(TID);
            switch (this.Info.Category)
            {
                case OperatorMenuCategory.Selector:
                case OperatorMenuCategory.Numeric:
                    local = this.IdentityFactory.CreateBinary(BinaryOperatorType.Equal);
                    break;

                case OperatorMenuCategory.Object:
                    local = this.IdentityFactory.CreateIsNull();
                    break;

                case OperatorMenuCategory.String:
                    local = this.IdentityFactory.CreateFunction(FunctionOperatorType.StartsWith);
                    break;

                case OperatorMenuCategory.DateTime:
                    local = this.Info.RoundDateTime ? this.IdentityFactory.CreateIsOnDate() : this.IdentityFactory.CreateIsNotOnDate();
                    break;

                case OperatorMenuCategory.Boolean:
                    local = this.IdentityFactory.CreateBinary(BinaryOperatorType.Equal);
                    break;

                default:
                    return default(TID);
            }
            IEnumerable<TID> source = available.FlattenLeaves<TID, string>();
            return (((local == null) || !source.Contains<TID>(local)) ? source.FirstOrDefault<TID>() : local);
        }

        protected abstract TID SelectFormatConditionID(FormatConditionFilter[] formatConditionFilters);
        protected virtual TID SelectID(CriteriaOperator filter, Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>[] available, TID defaultID)
        {
            if (this.Info.PredefinedFilters.Any<PredefinedFilter>())
            {
                Func<string, bool> predicate = <>c<TItem, TID>.<>9__36_0;
                if (<>c<TItem, TID>.<>9__36_0 == null)
                {
                    Func<string, bool> local1 = <>c<TItem, TID>.<>9__36_0;
                    predicate = <>c<TItem, TID>.<>9__36_0 = x => x != null;
                }
                string[] predefinedNames = PredefinedFiltersModel.GetPredefinedFilters(filter).Where<string>(predicate).ToArray<string>();
                TID local = this.SelectPredefinedID(predefinedNames);
                if (local != null)
                {
                    return local;
                }
            }
            if (this.Info.FormatConditionFilters.Any<FormatConditionFilter>())
            {
                Func<FormatConditionFilter, bool> formatConditionFilterChecker = FormatConditionFilterModel.GetFormatConditionFilterChecker(filter);
                TID local2 = this.SelectFormatConditionID(this.Info.FormatConditionFilters.Where<FormatConditionFilter>(formatConditionFilterChecker).ToArray<FormatConditionFilter>());
                if (local2 != null)
                {
                    return local2;
                }
            }
            if (filter is FunctionOperator)
            {
                FormatConditionFilterInfo topBottomFilterInfo = FormatConditionFiltersHelper.GetTopBottomFilterInfo((FunctionOperator) filter);
                if ((topBottomFilterInfo != null) && topBottomFilterInfo.Type.IsPredefinedFormatCondition())
                {
                    return this.SelectPredefinedFormatConditionID(topBottomFilterInfo.Type.ToPredefinedFormatConditionType());
                }
            }
            FallbackMapper<TID> fallback = <>c<TItem, TID>.<>9__36_6;
            if (<>c<TItem, TID>.<>9__36_6 == null)
            {
                FallbackMapper<TID> local3 = <>c<TItem, TID>.<>9__36_6;
                fallback = <>c<TItem, TID>.<>9__36_6 = _ => default(TID);
            }
            return filter.MapExtended<TID>((_, valueData, type) => base.IdentityFactory.CreateBinary(type).ToOption<TID>(), delegate (string _, UnaryOperatorType type) {
                if (type == UnaryOperatorType.IsNull)
                {
                    return base.IdentityFactory.CreateIsNull();
                }
                return default(TID);
            }, (_, __) => this.SelectIn().ToOption<TID>(), (_, __, ___) => base.IdentityFactory.CreateBetween().ToOption<TID>(), delegate (string propertyName, ValueData[] valuesData, FunctionOperatorType type) {
                object obj1;
                if (BetweenDatesHelper.IsBetweenDatesFunction(valuesData, type))
                {
                    return base.IdentityFactory.CreateBetweenDates().ToOption<TID>();
                }
                if (BetweenDatesHelper.IsIsOnDatesFunction(valuesData, type))
                {
                    return ((valuesData.Skip<ValueData>(1).Count<ValueData>() != 1) ? this.SelectIsOnDates().ToOption<TID>() : base.IdentityFactory.CreateIsOnDate().ToOption<TID>());
                }
                if (type != FunctionOperatorType.Custom)
                {
                    return base.IdentityFactory.CreateFunction(type).ToOption<TID>();
                }
                ValueData local1 = valuesData.FirstOrDefault<ValueData>();
                if (local1 != null)
                {
                    obj1 = local1.ToValue();
                }
                else
                {
                    ValueData local2 = local1;
                    obj1 = null;
                }
                string customFunctionName = obj1 as string;
                return (!CustomFunctionHelper.IsLikeCustomFunction(customFunctionName) ? ((string.IsNullOrEmpty(customFunctionName) || (CustomFunctionHelper.GetCustomFunctionOperatorBrowsable(customFunctionName) == null)) ? Option<TID>.Empty : base.IdentityFactory.CreateCustom(customFunctionName).ToOption<TID>()) : base.IdentityFactory.CreateLike().ToOption<TID>());
            }, null, null, new NotOperatorMapper<TID>(this.SelectNot), fallback, () => defaultID);
        }

        protected virtual TID SelectIn() => 
            default(TID);

        protected virtual TID SelectIsOnDates() => 
            default(TID);

        private TItem SelectItem(CriteriaOperator filter, Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>[] available, TID defaultID)
        {
            TItem local5;
            TID id = this.SelectID(filter, available, defaultID);
            if (id == null)
            {
                return default(TItem);
            }
            IdentifiedOperatorMenuItem<TID, TItem> local1 = available.FlattenLeaves<IdentifiedOperatorMenuItem<TID, TItem>, string>().FirstOrDefault<IdentifiedOperatorMenuItem<TID, TItem>>(x => x.ID.Equals(id));
            if (local1 != null)
            {
                local5 = local1.Value;
            }
            else
            {
                IdentifiedOperatorMenuItem<TID, TItem> local2 = local1;
                local5 = default(TItem);
            }
            TItem local3 = local5;
            TItem local6 = local3;
            if (local3 == null)
            {
                TItem local4 = local3;
                local6 = this.Builder.CreateNonAvailableItem(id);
            }
            return local6;
        }

        protected virtual TID SelectNot(TID identity)
        {
            if (identity != null)
            {
                if (identity.Equals(this.IdentityFactory.CreateBetween()))
                {
                    return this.IdentityFactory.CreateNotBetween();
                }
                if (identity.Equals(this.IdentityFactory.CreateFunction(FunctionOperatorType.Contains)))
                {
                    return this.IdentityFactory.CreateDoesNotContain();
                }
                if (identity.Equals(this.IdentityFactory.CreateIsNull()))
                {
                    return this.IdentityFactory.CreateIsNotNull();
                }
                if (identity.Equals(this.IdentityFactory.CreateIsOnDate()))
                {
                    return this.IdentityFactory.CreateIsNotOnDate();
                }
                if (identity.Equals(this.IdentityFactory.CreateFunction(FunctionOperatorType.IsNullOrEmpty)))
                {
                    return this.IdentityFactory.CreateIsNotNullOrEmpty();
                }
            }
            return default(TID);
        }

        private TID SelectPredefinedFormatConditionID(PredefinedFormatConditionType type) => 
            this.IdentityFactory.CreatePredefinedFormatCondition(type);

        protected abstract TID SelectPredefinedID(string[] predefinedNames);

        private OperatorMenuItemFactory<TItem, TID> Builder { get; }

        protected FilterSelectorBuildInfo Info { get; }

        private OperatorMenuItemIdentityFactoryBase<TID> IdentityFactory { get; }

        public FilterSelector<TItem> Selector
        {
            get
            {
                if (this.selectorCore == null)
                {
                    Tree<TID, string>[] availableIds = this.GetAvailableIds();
                    AvailableMenuItems<TItem, TID> availableItemsInfo = this.Builder.CreateAvailableItems(new AvailableMenuItemIdentities<TID>(availableIds, this.SelectDefaultID(availableIds)));
                    Func<Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>, Tree<TItem, string>> selector = <>c<TItem, TID>.<>9__12_0;
                    if (<>c<TItem, TID>.<>9__12_0 == null)
                    {
                        Func<Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>, Tree<TItem, string>> local1 = <>c<TItem, TID>.<>9__12_0;
                        selector = <>c<TItem, TID>.<>9__12_0 = delegate (Tree<IdentifiedOperatorMenuItem<TID, TItem>, string> x) {
                            Func<IdentifiedOperatorMenuItem<TID, TItem>, TItem> transformLeaf = <>c<TItem, TID>.<>9__12_1;
                            if (<>c<TItem, TID>.<>9__12_1 == null)
                            {
                                Func<IdentifiedOperatorMenuItem<TID, TItem>, TItem> local1 = <>c<TItem, TID>.<>9__12_1;
                                transformLeaf = <>c<TItem, TID>.<>9__12_1 = leaf => leaf.Value;
                            }
                            return x.Transform<IdentifiedOperatorMenuItem<TID, TItem>, string, TItem>(transformLeaf);
                        };
                    }
                    Tree<TItem, string>[] available = availableItemsInfo.Items.Select<Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>, Tree<TItem, string>>(selector).ToArray<Tree<TItem, string>>();
                    this.selectorCore = new FilterSelector<TItem>(available, filter => ((OperatorMenuBuilderBase<TItem, TID>) this).SelectItem(filter, availableItemsInfo.Items, availableItemsInfo.DefaultID));
                }
                return this.selectorCore;
            }
        }

        protected FilterRestrictions Restrictions =>
            this.Info.Restrictions;

        protected string FieldName =>
            this.Info.FieldName;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OperatorMenuBuilderBase<TItem, TID>.<>c <>9;
            public static Func<IdentifiedOperatorMenuItem<TID, TItem>, TItem> <>9__12_1;
            public static Func<Tree<IdentifiedOperatorMenuItem<TID, TItem>, string>, Tree<TItem, string>> <>9__12_0;
            public static Func<IEnumerable<Tree<TID, string>>, IEnumerable<Tree<TID, string>>> <>9__18_0;
            public static Func<IEnumerable<Tree<TID, string>>, IEnumerable<Tree<TID, string>>> <>9__18_1;
            public static Func<string, bool> <>9__36_0;
            public static FallbackMapper<TID> <>9__36_6;

            static <>c()
            {
                OperatorMenuBuilderBase<TItem, TID>.<>c.<>9 = new OperatorMenuBuilderBase<TItem, TID>.<>c();
            }

            internal Tree<TItem, string> <get_Selector>b__12_0(Tree<IdentifiedOperatorMenuItem<TID, TItem>, string> x)
            {
                Func<IdentifiedOperatorMenuItem<TID, TItem>, TItem> transformLeaf = OperatorMenuBuilderBase<TItem, TID>.<>c.<>9__12_1;
                if (OperatorMenuBuilderBase<TItem, TID>.<>c.<>9__12_1 == null)
                {
                    Func<IdentifiedOperatorMenuItem<TID, TItem>, TItem> local1 = OperatorMenuBuilderBase<TItem, TID>.<>c.<>9__12_1;
                    transformLeaf = OperatorMenuBuilderBase<TItem, TID>.<>c.<>9__12_1 = leaf => leaf.Value;
                }
                return x.Transform<IdentifiedOperatorMenuItem<TID, TItem>, string, TItem>(transformLeaf);
            }

            internal TItem <get_Selector>b__12_1(IdentifiedOperatorMenuItem<TID, TItem> leaf) => 
                leaf.Value;

            internal IEnumerable<Tree<TID, string>> <GetAvailableIdsInternal>b__18_0(IEnumerable<Tree<TID, string>> x) => 
                x;

            internal IEnumerable<Tree<TID, string>> <GetAvailableIdsInternal>b__18_1(IEnumerable<Tree<TID, string>> x) => 
                x;

            internal bool <SelectID>b__36_0(string x) => 
                x != null;

            internal TID <SelectID>b__36_6(CriteriaOperator _) => 
                default(TID);
        }

        [CompilerGenerated]
        private sealed class <AddStringContentOperators>d__24 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;

            [DebuggerHidden]
            public <AddStringContentOperators>d__24(int <>1__state)
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
                        if (!this.<>4__this.Restrictions.Allow(FunctionOperatorType.Contains))
                        {
                            break;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateFunction(FunctionOperatorType.Contains));
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_000B;

                    case 3:
                        this.<>1__state = -1;
                        goto TR_000A;

                    case 4:
                        this.<>1__state = -1;
                        goto TR_0009;

                    case 5:
                        this.<>1__state = -1;
                        goto TR_0008;

                    case 6:
                        this.<>1__state = -1;
                        goto TR_0007;

                    default:
                        return false;
                }
                if (this.<>4__this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.DoesNotContain))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateDoesNotContain());
                    this.<>1__state = 2;
                    return true;
                }
                goto TR_000B;
            TR_0007:
                return false;
            TR_0008:
                if (this.<>4__this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.NotLike))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateNotLike());
                    this.<>1__state = 6;
                    return true;
                }
                goto TR_0007;
            TR_0009:
                if (this.<>4__this.Restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.Like))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateLike());
                    this.<>1__state = 5;
                    return true;
                }
                goto TR_0008;
            TR_000A:
                if (this.<>4__this.Restrictions.Allow(FunctionOperatorType.EndsWith))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateFunction(FunctionOperatorType.EndsWith));
                    this.<>1__state = 4;
                    return true;
                }
                goto TR_0009;
            TR_000B:
                if (this.<>4__this.Restrictions.Allow(FunctionOperatorType.StartsWith))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateFunction(FunctionOperatorType.StartsWith));
                    this.<>1__state = 3;
                    return true;
                }
                goto TR_000A;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<AddStringContentOperators>d__24 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<AddStringContentOperators>d__24) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<AddStringContentOperators>d__24(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetAnyOfOperators>d__22 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetAnyOfOperators>d__22(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetAnyOfOperators>d__22 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetAnyOfOperators>d__22(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetAnyOfOperators>d__22) this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetBlankOperators>d__25 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;

            [DebuggerHidden]
            public <GetBlankOperators>d__25(int <>1__state)
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
                        if (!this.<>4__this.Restrictions.Allow(FunctionOperatorType.IsNullOrEmpty))
                        {
                            break;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateFunction(FunctionOperatorType.IsNullOrEmpty));
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
                if (this.<>4__this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNotNullOrEmpty))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateIsNotNullOrEmpty());
                    this.<>1__state = 2;
                    return true;
                }
            TR_0003:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetBlankOperators>d__25 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetBlankOperators>d__25) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetBlankOperators>d__25(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetDateComparisonOperators>d__26 : IEnumerable<IEnumerable<Tree<TID, string>>>, IEnumerable, IEnumerator<IEnumerable<Tree<TID, string>>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IEnumerable<Tree<TID, string>> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;
            private bool isRange;
            public bool <>3__isRange;

            [DebuggerHidden]
            public <GetDateComparisonOperators>d__26(int <>1__state)
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
                        if (!this.<>4__this.Info.RoundDateTime)
                        {
                            this.<>2__current = this.isRange ? this.<>4__this.GetRangeOperators() : this.<>4__this.GetEqualityOperators();
                            this.<>1__state = 5;
                            return true;
                        }
                        if (!this.<>4__this.Restrictions.AllowCustomDateFilters)
                        {
                            break;
                        }
                        if (this.isRange)
                        {
                            this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateBetweenDates()).Yield<Tree<TID, string>>();
                            this.<>1__state = 1;
                            return true;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateIsOnDate()).Yield<Tree<TID, string>>();
                        this.<>1__state = 3;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        this.<>2__current = this.<>4__this.GetIsOnDates();
                        this.<>1__state = 2;
                        return true;

                    case 2:
                        this.<>1__state = -1;
                        break;

                    case 3:
                        this.<>1__state = -1;
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateIsNotOnDate()).Yield<Tree<TID, string>>();
                        this.<>1__state = 4;
                        return true;

                    case 4:
                        this.<>1__state = -1;
                        break;

                    case 5:
                        this.<>1__state = -1;
                        break;

                    default:
                        return false;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IEnumerable<Tree<TID, string>>> IEnumerable<IEnumerable<Tree<TID, string>>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetDateComparisonOperators>d__26 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetDateComparisonOperators>d__26) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetDateComparisonOperators>d__26(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.isRange = this.<>3__isRange;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IEnumerable<Tree<TID, string>> IEnumerator<IEnumerable<Tree<TID, string>>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetIsOnDates>d__27 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetIsOnDates>d__27(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetIsOnDates>d__27 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetIsOnDates>d__27(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetIsOnDates>d__27) this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetNullOperators>d__23 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;

            [DebuggerHidden]
            public <GetNullOperators>d__23(int <>1__state)
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
                        if (!this.<>4__this.Info.IsNullable)
                        {
                            return false;
                        }
                        if (!this.<>4__this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNull))
                        {
                            break;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateIsNull());
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        break;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_0004;

                    default:
                        return false;
                }
                if (this.<>4__this.Restrictions.AllowedUnaryFilters.HasFlag(AllowedUnaryFilters.IsNotNull))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateIsNotNull());
                    this.<>1__state = 2;
                    return true;
                }
            TR_0004:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetNullOperators>d__23 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetNullOperators>d__23) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetNullOperators>d__23(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetPredefinedFormatCondition>d__31 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;

            [DebuggerHidden]
            public <GetPredefinedFormatCondition>d__31(int <>1__state)
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
                        switch (this.<>4__this.Info.Category)
                        {
                            case OperatorMenuCategory.Selector:
                                return false;

                            case OperatorMenuCategory.Object:
                            case OperatorMenuCategory.String:
                            case OperatorMenuCategory.Boolean:
                                break;

                            case OperatorMenuCategory.DateTime:
                            case OperatorMenuCategory.Numeric:
                                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.Top))
                                {
                                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Top));
                                    this.<>1__state = 1;
                                    return true;
                                }
                                goto TR_000E;

                            default:
                                return false;
                        }
                        break;

                    case 1:
                        this.<>1__state = -1;
                        goto TR_000E;

                    case 2:
                        this.<>1__state = -1;
                        goto TR_000D;

                    case 3:
                        this.<>1__state = -1;
                        goto TR_000C;

                    case 4:
                        this.<>1__state = -1;
                        break;

                    case 5:
                        this.<>1__state = -1;
                        goto TR_0006;

                    case 6:
                        this.<>1__state = -1;
                        goto TR_0005;

                    default:
                        return false;
                }
                goto TR_0007;
            TR_0005:
                return false;
            TR_0006:
                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.Duplicate))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Duplicate));
                    this.<>1__state = 6;
                    return true;
                }
                goto TR_0005;
            TR_0007:
                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.Unique))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Unique));
                    this.<>1__state = 5;
                    return true;
                }
                goto TR_0006;
            TR_000C:
                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.BelowAverage))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.BelowAverage));
                    this.<>1__state = 4;
                    return true;
                }
                goto TR_0007;
            TR_000D:
                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.AboveAverage))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.AboveAverage));
                    this.<>1__state = 3;
                    return true;
                }
                goto TR_000C;
            TR_000E:
                if (this.<>4__this.Info.Restrictions.Allow(PredefinedFormatConditionType.Bottom))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreatePredefinedFormatCondition(PredefinedFormatConditionType.Bottom));
                    this.<>1__state = 2;
                    return true;
                }
                goto TR_000D;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetPredefinedFormatCondition>d__31 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetPredefinedFormatCondition>d__31) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetPredefinedFormatCondition>d__31(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <GetRangeOperators>d__21 : IEnumerable<Tree<TID, string>>, IEnumerable, IEnumerator<Tree<TID, string>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private Tree<TID, string> <>2__current;
            private int <>l__initialThreadId;
            public OperatorMenuBuilderBase<TItem, TID> <>4__this;

            [DebuggerHidden]
            public <GetRangeOperators>d__21(int <>1__state)
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
                        if (!this.<>4__this.Restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.Between))
                        {
                            break;
                        }
                        this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateBetween());
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
                if (this.<>4__this.Restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.NotBetween))
                {
                    this.<>2__current = OperatorMenuBuilderBase<TItem, TID>.CreateID(this.<>4__this.IdentityFactory.CreateNotBetween());
                    this.<>1__state = 2;
                    return true;
                }
            TR_0003:
                return false;
            }

            [DebuggerHidden]
            IEnumerator<Tree<TID, string>> IEnumerable<Tree<TID, string>>.GetEnumerator()
            {
                OperatorMenuBuilderBase<TItem, TID>.<GetRangeOperators>d__21 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (OperatorMenuBuilderBase<TItem, TID>.<GetRangeOperators>d__21) this;
                }
                else
                {
                    d__ = new OperatorMenuBuilderBase<TItem, TID>.<GetRangeOperators>d__21(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.FilteringUI.Tree<TID,System.String>>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            Tree<TID, string> IEnumerator<Tree<TID, string>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

