namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public sealed class FilteringUIContext
    {
        private readonly FilteringUIContextClient client;
        private CriteriaOperator lastFitler;
        private IDictionary<string, CriteriaOperator> split;
        private CriteriaOperator complexFilter;
        private List<FilteringContextListener> listeners = new List<FilteringContextListener>();
        private bool notifyListChangedScheduled;

        internal FilteringUIContext(FilteringUIContextClient client)
        {
            this.client = client;
            this.SplitFilter();
        }

        internal void ApplyFilter(string propertyName, CriteriaOperator filter)
        {
            this.ApplyFilters(new KeyValuePair<string, CriteriaOperator>(propertyName, filter).Yield<KeyValuePair<string, CriteriaOperator>>());
        }

        internal void ApplyFilters(IEnumerable<KeyValuePair<string, CriteriaOperator>> filters)
        {
            foreach (KeyValuePair<string, CriteriaOperator> pair in filters)
            {
                FilteringColumn column = this.GetColumn(pair.Key);
                string rootFilterValuesPropertyName = column?.RootFilterValuesPropertyName;
                string key = rootFilterValuesPropertyName;
                if (rootFilterValuesPropertyName == null)
                {
                    string local2 = rootFilterValuesPropertyName;
                    key = pair.Key;
                }
                this.split.Remove(key);
                string[] groupFields = column?.GroupFields;
                if (groupFields != null)
                {
                    foreach (string str in groupFields)
                    {
                        this.split.Remove(str);
                    }
                }
                if (pair.Value != null)
                {
                    this.split[pair.Key] = pair.Value;
                }
            }
            this.client.Filter = this.BuildFilter(new string[0]);
        }

        private CriteriaOperator BuildExceptGroupFieldsFilter(string propertyName)
        {
            FilteringColumn column = this.GetColumn(propertyName);
            string[] groupFields = column?.GroupFields;
            if ((groupFields != null) && groupFields.Any<string>())
            {
                return this.BuildFilter(column.RootFilterValuesPropertyName.Yield<string>().Concat<string>(groupFields).ToArray<string>());
            }
            string[] exceptPropertyNames = new string[] { propertyName };
            return this.BuildFilter(exceptPropertyNames);
        }

        private CriteriaOperator BuildFilter(params string[] exceptPropertyNames)
        {
            IEnumerable<KeyValuePair<string, CriteriaOperator>> split = this.split;
            if (exceptPropertyNames.Any<string>())
            {
                split = from x in split
                    where !exceptPropertyNames.Contains<string>(x.Key)
                    select x;
            }
            Func<KeyValuePair<string, CriteriaOperator>, CriteriaOperator> selector = <>c.<>9__39_1;
            if (<>c.<>9__39_1 == null)
            {
                Func<KeyValuePair<string, CriteriaOperator>, CriteriaOperator> local1 = <>c.<>9__39_1;
                selector = <>c.<>9__39_1 = x => x.Value;
            }
            return CriteriaOperator.And(split.Select<KeyValuePair<string, CriteriaOperator>, CriteriaOperator>(selector).Concat<CriteriaOperator>(this.complexFilter.Yield<CriteriaOperator>()));
        }

        internal void DeleteColumnFilter(string propertyName)
        {
            this.ApplyFilter(propertyName, null);
        }

        internal static List<string> ExtractProperties(CriteriaOperator filter)
        {
            ExtractPropertiesVisitor visitor = new ExtractPropertiesVisitor();
            if (filter != null)
            {
                filter.Accept<CriteriaOperator>(visitor);
            }
            return visitor.Properties.Distinct<string>().ToList<string>();
        }

        internal DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters GetAllowedGroupFilters() => 
            this.client.GetAllowedGroupFilters();

        internal FilteringColumn GetColumn(string propertyName) => 
            this.client.GetColumn(propertyName);

        internal IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>> GetColumnForest(ColumnForestFilterMode filterMode = 1) => 
            this.client.GetColumnForest(filterMode);

        internal int[] GetCounts(string propertyName, IEnumerable<CriteriaOperator> filters)
        {
            string[] exceptPropertyNames = new string[] { propertyName };
            CriteriaOperator filter = this.BuildFilter(exceptPropertyNames);
            SummaryFilterInfo[] summaries = (from x in filters select new SummaryFilterInfo(SummaryItemType.Count, CriteriaOperator.And(filter, x))).ToArray<SummaryFilterInfo>();
            object[] summaryValues = this.GetSummaryValues(propertyName, summaries);
            if (summaryValues == null)
            {
                object[] local1 = summaryValues;
                return null;
            }
            IEnumerable<int> source = summaryValues.Cast<int>();
            if (source != null)
            {
                return source.ToArray<int>();
            }
            IEnumerable<int> local2 = source;
            return null;
        }

        internal CriteriaOperator GetFilter(string fieldName) => 
            this.GetFilter(this.client.GetColumn(fieldName), fieldName);

        internal CriteriaOperator GetFilter(FilteringColumn column, string propertyName = null)
        {
            if ((column == null) && (propertyName == null))
            {
                return null;
            }
            string rootFilterValuesPropertyName = column?.RootFilterValuesPropertyName;
            string key = rootFilterValuesPropertyName;
            if (rootFilterValuesPropertyName == null)
            {
                string local2 = rootFilterValuesPropertyName;
                key = propertyName;
            }
            CriteriaOperator valueOrDefault = this.split.GetValueOrDefault<string, CriteriaOperator>(key);
            string[] groupFields = column?.GroupFields;
            if (groupFields != null)
            {
                foreach (string str in groupFields)
                {
                    valueOrDefault &= this.split.GetValueOrDefault<string, CriteriaOperator>(str);
                }
            }
            return valueOrDefault;
        }

        internal IEnumerable<FormatConditionFilter> GetFormatConditionFilters(string propertyName) => 
            from x in this.client.GetFormatConditionFilters()
                where (x.Info.PropertyName == propertyName) || x.ApplyToRow
                select x;

        internal Task<UniqueValues> GetGroupUniqueValues(string valuesPropertyName, bool includeCounts, CriteriaOperator filter, string filterPropertyName) => 
            this.GetUniqueValuesCore(valuesPropertyName, this.BuildExceptGroupFieldsFilter(filterPropertyName), filter, true, includeCounts, FilterValuesThrottleMode.Forbid);

        internal HeaderAppearance GetHeader(string name)
        {
            VisualFilteringColumn local1 = this.GetColumnForest(ColumnForestFilterMode.FilterEditorVisibleOnly).FlattenLeaves<VisualFilteringColumn, HeaderAppearanceAccessor>().FirstOrDefault<VisualFilteringColumn>(x => x.Name == name);
            if (local1 != null)
            {
                return local1.GetHeaderAppearance();
            }
            VisualFilteringColumn local2 = local1;
            return null;
        }

        internal Tuple<object, object> GetMinMaxRange(string propertyName, bool includeFilteredOut)
        {
            bool flag1;
            SummaryItemType[] summaryTypes = new SummaryItemType[] { SummaryItemType.Min, SummaryItemType.Max };
            object[] source = this.GetSummaryValues(propertyName, summaryTypes, includeFilteredOut);
            if (source == null)
            {
                flag1 = true;
            }
            else
            {
                Func<object, bool> predicate = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<object, bool> local1 = <>c.<>9__16_0;
                    predicate = <>c.<>9__16_0 = x => x != null;
                }
                flag1 = !source.All<object>(predicate);
            }
            return (!flag1 ? Tuple.Create<object, object>(source[0], source[1]) : null);
        }

        internal object[] GetSummaryValues(string propertyName, SummaryFilterInfo[] summaries) => 
            this.client.GetColumn(propertyName).GetSummaryValues(summaries);

        private object[] GetSummaryValues(string propertyName, SummaryItemType[] summaryTypes, bool includeFilteredOut)
        {
            object obj1;
            if (includeFilteredOut)
            {
                obj1 = null;
            }
            else
            {
                string[] exceptPropertyNames = new string[] { propertyName };
                obj1 = this.BuildFilter(exceptPropertyNames);
            }
            CriteriaOperator filter = (CriteriaOperator) obj1;
            return this.GetSummaryValues(propertyName, (from x in summaryTypes select new SummaryFilterInfo(x, filter)).ToArray<SummaryFilterInfo>());
        }

        internal Task<UniqueValues> GetUniqueValues(string propertyName, bool includeCounts) => 
            this.GetUniqueValuesCore(propertyName, this.BuildExceptGroupFieldsFilter(propertyName), null, false, includeCounts, FilterValuesThrottleMode.Forbid);

        internal Task<UniqueValues> GetUniqueValues(string propertyName, CriteriaOperator filter, bool includeCounts, FilterValuesThrottleMode throttleMode = 0) => 
            this.GetUniqueValuesCore(propertyName, filter, null, true, includeCounts, throttleMode);

        private Task<UniqueValues> GetUniqueValuesCore(string propertyName, CriteriaOperator filter, CriteriaOperator fixedFilter, bool includeFilteredOut, bool includeCounts, FilterValuesThrottleMode throttleMode)
        {
            FilteringColumn column = this.client.GetColumn(propertyName);
            if (column == null)
            {
                return UniqueValues.Empty;
            }
            bool flag = column.ShowAllValuesInFilter();
            return column.GetUniqueValues(flag ? fixedFilter : (filter & fixedFilter), (flag && fixedFilter.ReferenceEqualsNull()) | includeFilteredOut, includeCounts ? CountsIncludeMode.Include : CountsIncludeMode.Exclude, throttleMode);
        }

        internal void NotifyColumnAddedRemoved(string fieldName)
        {
            this.UpdateAllListeners(x => x.ColumnAddedRemoved(fieldName));
        }

        internal void NotifyColumnsReset()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__49_0;
                updateAction = <>c.<>9__49_0 = x => x.ColumnsReset();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyColumnUnboundChanged()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__47_0;
                updateAction = <>c.<>9__47_0 = x => x.ColumnUnboundChanged();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyEditSettingsChanged(string fieldName)
        {
            this.UpdateAllListeners(x => x.EditSettingsChanged(fieldName));
        }

        internal void NotifyEndDataUpdate()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__41_0;
                updateAction = <>c.<>9__41_0 = x => x.EndDataUpdate();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyFilterChanged()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__44_0;
                updateAction = <>c.<>9__44_0 = x => x.FilterChanged();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyFormatConditionsChanged()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__53_0;
            if (<>c.<>9__53_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__53_0;
                updateAction = <>c.<>9__53_0 = x => x.FormatConditionsChanged();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyGroupFieldsChanged(string fieldName)
        {
            this.UpdateAllListeners(x => x.GroupFieldsChanged(fieldName));
        }

        internal void NotifyItemsSourceChanged()
        {
            Action<FilteringContextListener> updateAction = <>c.<>9__46_0;
            if (<>c.<>9__46_0 == null)
            {
                Action<FilteringContextListener> local1 = <>c.<>9__46_0;
                updateAction = <>c.<>9__46_0 = x => x.DataSourceChanged();
            }
            this.UpdateAllListeners(updateAction);
        }

        internal void NotifyListChanged(ListChangedEventArgs e)
        {
            if (!this.notifyListChangedScheduled)
            {
                this.notifyListChangedScheduled = true;
                Dispatcher.CurrentDispatcher.BeginInvoke(delegate {
                    Action<FilteringContextListener> updateAction = <>c.<>9__42_1;
                    if (<>c.<>9__42_1 == null)
                    {
                        Action<FilteringContextListener> local1 = <>c.<>9__42_1;
                        updateAction = <>c.<>9__42_1 = x => x.ListChanged();
                    }
                    this.UpdateAllListeners(updateAction);
                    this.notifyListChangedScheduled = false;
                }, DispatcherPriority.Normal, new object[0]);
            }
        }

        internal void NotifyRoundDateChanged(string fieldName)
        {
            this.UpdateAllListeners(x => x.RoundDateChanged(fieldName));
        }

        internal UnsubscribeAction RegisterListener(FilteringContextListener listener)
        {
            this.listeners.Add(listener);
            listener.FilterChanged();
            bool removed = false;
            return delegate {
                if (removed)
                {
                    throw new InvalidOperationException("Listener already removed");
                }
                removed = true;
                this.listeners.Remove(listener);
            };
        }

        internal void SetFilter()
        {
            if ((this.split == null) || !ReferenceEquals(this.lastFitler, this.client.Filter))
            {
                this.SplitFilter();
            }
        }

        internal void SetFilterAndNotifyFilterChanged()
        {
            this.SetFilter();
            this.NotifyFilterChanged();
        }

        private void SplitFilter()
        {
            this.lastFitler = this.client.Filter;
            Tuple<CriteriaOperator, IDictionary<string, CriteriaOperator>> tuple = CriteriaColumnAffinityResolver.SplitByColumnNames(this.client.Filter, null);
            this.split = tuple.Item2;
            Predicate<GroupOperator> condition = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Predicate<GroupOperator> local1 = <>c.<>9__27_0;
                condition = <>c.<>9__27_0 = x => x.OperatorType == GroupOperatorType.And;
            }
            ICollection<CriteriaOperator> source = tuple.Item1.Transform<GroupOperator, ICollection<CriteriaOperator>>(condition, <>c.<>9__27_1 ??= x => x.Operands.ToList<CriteriaOperator>(), <>c.<>9__27_2 ??= delegate (CriteriaOperator x) {
                List<CriteriaOperator> list1 = new List<CriteriaOperator>();
                list1.Add(x);
                return list1;
            });
            foreach (CriteriaOperator @operator in source.ToList<CriteriaOperator>())
            {
                List<string> first = ExtractProperties(@operator);
                if (first.Count >= 2)
                {
                    foreach (string str in first.Concat<string>(this.split.Keys))
                    {
                        FilteringColumn column = this.GetColumn(str);
                        string[] groupFields = column?.GroupFields;
                        if ((groupFields != null) && !first.Except<string>(column.RootFilterValuesPropertyName.Yield<string>().Concat<string>(groupFields)).Any<string>())
                        {
                            this.split[str] = this.split.GetValueOrDefault<string, CriteriaOperator>(str) & @operator;
                            source.Remove(@operator);
                            break;
                        }
                    }
                }
            }
            this.complexFilter = CriteriaOperator.And(source);
            Func<CriteriaOperator, CriteriaOperator> mapper = <>c.<>9__27_3;
            if (<>c.<>9__27_3 == null)
            {
                Func<CriteriaOperator, CriteriaOperator> local4 = <>c.<>9__27_3;
                mapper = <>c.<>9__27_3 = x => x;
            }
            List<CriteriaOperator> list = FormatConditionFiltersHelper.ParseGroupOrOperator<CriteriaOperator>(this.complexFilter, mapper).ToList<CriteriaOperator>();
            foreach (FunctionOperator operator2 in list.OfType<FunctionOperator>().ToList<FunctionOperator>())
            {
                AppliedFormatConditionFilterInfo appliedFormatConditionFilterInfo = FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo(operator2);
                if (appliedFormatConditionFilterInfo != null)
                {
                    this.split[appliedFormatConditionFilterInfo.PropertyName] = this.split.GetValueOrDefault<string, CriteriaOperator>(appliedFormatConditionFilterInfo.PropertyName) | operator2;
                    list.Remove(operator2);
                }
            }
            this.complexFilter = CriteriaOperator.Or(list);
        }

        internal CriteriaOperator SubstitutePredefinedFilters(CriteriaOperator filter)
        {
            if (filter.ReferenceEqualsNull())
            {
                return null;
            }
            Func<FilteringColumn, bool> predicate = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<FilteringColumn, bool> local1 = <>c.<>9__24_1;
                predicate = <>c.<>9__24_1 = x => x != null;
            }
            Func<FilteringColumn, IEnumerable<Attributed<IPredefinedFilter>>> selector = <>c.<>9__24_2;
            if (<>c.<>9__24_2 == null)
            {
                Func<FilteringColumn, IEnumerable<Attributed<IPredefinedFilter>>> local2 = <>c.<>9__24_2;
                selector = <>c.<>9__24_2 = x => from y in x.GetSustitutedPredefinedFilters() select Attributed.Attribute<IPredefinedFilter>(y, x.Name);
            }
            List<Attributed<IPredefinedFilter>> predefinedFilters = (from x in this.GetColumnForest(ColumnForestFilterMode.All).FlattenLeaves<VisualFilteringColumn, HeaderAppearanceAccessor>() select this.GetColumn(x.Name)).Where<FilteringColumn>(predicate).SelectMany<FilteringColumn, Attributed<IPredefinedFilter>>(selector).ToList<Attributed<IPredefinedFilter>>();
            return PredefinedFiltersSubstituteHelper.Substitute(filter, predefinedFilters, () => this.client.GetFormatConditionFilters().ToList<FormatConditionFilter>(), this.client.SubstituteTopBottomFilter);
        }

        internal CriteriaOperator SubstituteWholeFilter(CriteriaOperator filter) => 
            this.client.SubstituteFilter(filter);

        private void UpdateAllListeners(Action<FilteringContextListener> updateAction)
        {
            foreach (FilteringContextListener listener in this.listeners.ToList<FilteringContextListener>())
            {
                updateAction(listener);
            }
        }

        internal CriteriaOperator Filter
        {
            get => 
                this.client.Filter;
            set => 
                this.client.Filter = value;
        }

        internal DevExpress.Xpf.Core.FilteringUI.AllowedGroupFilters AllowedGroupFilters { get; }

        internal bool AllowCustomExpressionInFilterEditor =>
            this.client.GetAllowCustomExpressionInFilterEditor();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringUIContext.<>c <>9 = new FilteringUIContext.<>c();
            public static Func<object, bool> <>9__16_0;
            public static Func<FilteringColumn, bool> <>9__24_1;
            public static Func<FilteringColumn, IEnumerable<Attributed<IPredefinedFilter>>> <>9__24_2;
            public static Predicate<GroupOperator> <>9__27_0;
            public static Func<GroupOperator, ICollection<CriteriaOperator>> <>9__27_1;
            public static Func<CriteriaOperator, ICollection<CriteriaOperator>> <>9__27_2;
            public static Func<CriteriaOperator, CriteriaOperator> <>9__27_3;
            public static Func<KeyValuePair<string, CriteriaOperator>, CriteriaOperator> <>9__39_1;
            public static Action<FilteringContextListener> <>9__41_0;
            public static Action<FilteringContextListener> <>9__42_1;
            public static Action<FilteringContextListener> <>9__44_0;
            public static Action<FilteringContextListener> <>9__46_0;
            public static Action<FilteringContextListener> <>9__47_0;
            public static Action<FilteringContextListener> <>9__49_0;
            public static Action<FilteringContextListener> <>9__53_0;

            internal CriteriaOperator <BuildFilter>b__39_1(KeyValuePair<string, CriteriaOperator> x) => 
                x.Value;

            internal bool <GetMinMaxRange>b__16_0(object x) => 
                x != null;

            internal void <NotifyColumnsReset>b__49_0(FilteringContextListener x)
            {
                x.ColumnsReset();
            }

            internal void <NotifyColumnUnboundChanged>b__47_0(FilteringContextListener x)
            {
                x.ColumnUnboundChanged();
            }

            internal void <NotifyEndDataUpdate>b__41_0(FilteringContextListener x)
            {
                x.EndDataUpdate();
            }

            internal void <NotifyFilterChanged>b__44_0(FilteringContextListener x)
            {
                x.FilterChanged();
            }

            internal void <NotifyFormatConditionsChanged>b__53_0(FilteringContextListener x)
            {
                x.FormatConditionsChanged();
            }

            internal void <NotifyItemsSourceChanged>b__46_0(FilteringContextListener x)
            {
                x.DataSourceChanged();
            }

            internal void <NotifyListChanged>b__42_1(FilteringContextListener x)
            {
                x.ListChanged();
            }

            internal bool <SplitFilter>b__27_0(GroupOperator x) => 
                x.OperatorType == GroupOperatorType.And;

            internal ICollection<CriteriaOperator> <SplitFilter>b__27_1(GroupOperator x) => 
                x.Operands.ToList<CriteriaOperator>();

            internal ICollection<CriteriaOperator> <SplitFilter>b__27_2(CriteriaOperator x)
            {
                List<CriteriaOperator> list1 = new List<CriteriaOperator>();
                list1.Add(x);
                return list1;
            }

            internal CriteriaOperator <SplitFilter>b__27_3(CriteriaOperator x) => 
                x;

            internal bool <SubstitutePredefinedFilters>b__24_1(FilteringColumn x) => 
                x != null;

            internal IEnumerable<Attributed<IPredefinedFilter>> <SubstitutePredefinedFilters>b__24_2(FilteringColumn x) => 
                from y in x.GetSustitutedPredefinedFilters() select Attributed.Attribute<IPredefinedFilter>(y, x.Name);
        }

        private class ExtractPropertiesVisitor : ClientCriteriaVisitorBase
        {
            public List<string> Properties = new List<string>();

            protected override CriteriaOperator Visit(OperandProperty theOperand)
            {
                this.Properties.Add(theOperand.PropertyName);
                return base.Visit(theOperand);
            }
        }
    }
}

