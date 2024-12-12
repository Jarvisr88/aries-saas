namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class ExcelDateTimeColumnFilterInfo : ExcelColumnFilterInfoBase
    {
        internal const string SubItemsPropertyName = "Items";
        private ExcelDateColumnFilterScope _SearchScope;

        public event EventHandler BeginUpdateExpandState;

        public event EventHandler EndUpdateExpandState;

        public ExcelDateTimeColumnFilterInfo(ColumnBase column) : base(column)
        {
            this._SearchScope = ExcelDateColumnFilterScope.All;
        }

        protected override bool CanShowValuesCore() => 
            base.Column.AllowFilter(AllowedDateTimeFilters.MultipleDateRanges);

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
            base.ClearPopupData(popup);
            this._SearchScope = ExcelDateColumnFilterScope.All;
        }

        protected override ExcelColumnFilterValuesListBase CreateList() => 
            new ExcelDateColumnFilterList();

        private void EnumerateItems(ExcelDateColumnFilterList items)
        {
            foreach (ExcelColumnFilterValue value2 in items.GetFilterableValues())
            {
                if (value2.IsChecked == null)
                {
                    value2.IsExpanded = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(base.SearchText))
                    {
                        value2.IsExpanded = false;
                        continue;
                    }
                    switch (this.SearchScope)
                    {
                        case ExcelDateColumnFilterScope.Day:
                            if ((items.FilterScope == ExcelDateColumnFilterScope.Year) || (items.FilterScope == ExcelDateColumnFilterScope.Month))
                            {
                                value2.IsExpanded = true;
                            }
                            break;

                        case ExcelDateColumnFilterScope.Month:
                            value2.IsExpanded = items.FilterScope == ExcelDateColumnFilterScope.Year;
                            break;

                        case ExcelDateColumnFilterScope.Year:
                        {
                            value2.IsExpanded = false;
                            continue;
                        }
                        case ExcelDateColumnFilterScope.All:
                            value2.IsExpanded = true;
                            break;

                        default:
                            break;
                    }
                }
                ExcelDateColumnFilterValue value3 = value2 as ExcelDateColumnFilterValue;
                if (value3 != null)
                {
                    this.EnumerateItems(value3.Items);
                }
            }
        }

        private IEnumerable<ExcelColumnFilterValue> EnumerateItemsCore(Func<ExcelColumnFilterValuesListBase, IEnumerable<ExcelColumnFilterValue>> getSubItems)
        {
            List<ExcelColumnFilterValue> list = new List<ExcelColumnFilterValue>();
            foreach (ExcelColumnFilterValue value2 in getSubItems(base.FilterItems))
            {
                ExcelDateColumnFilterValue item = value2 as ExcelDateColumnFilterValue;
                if (item == null)
                {
                    if (!ColumnFilterInfoHelper.IsNullOrEmptyString(value2.EditValue) || ((value2.IsChecked == null) || !value2.IsChecked.Value))
                    {
                        continue;
                    }
                    list.Add(value2);
                    continue;
                }
                if (item.Items.IsAllItemsChecked && (item.Items.AllItemsVisible || base.FilterItems.SelectedItemsAddedToFilter))
                {
                    list.Add(item);
                    continue;
                }
                foreach (ExcelDateColumnFilterValue value4 in getSubItems(item.Items))
                {
                    if (value4.Items.IsAllItemsChecked && (value4.Items.AllItemsVisible || base.FilterItems.SelectedItemsAddedToFilter))
                    {
                        list.Add(value4);
                        continue;
                    }
                    foreach (ExcelDateDayColumnFilterValue value5 in getSubItems(value4.Items))
                    {
                        bool? isChecked = value5.IsChecked;
                        if ((isChecked != null) && value5.IsChecked.Value)
                        {
                            list.Add(value5);
                        }
                    }
                }
            }
            return list;
        }

        protected override void FillList(ExcelColumnFilterValuesListBase list, IList<object> columnFilterValues, out bool shouldCreateBlanks)
        {
            ExcelDateColumnFilterList list2 = (ExcelDateColumnFilterList) list;
            int num = -1;
            ExcelDateColumnFilterValue item = null;
            int month = -1;
            ExcelDateColumnFilterValue value3 = null;
            foreach (DateTime time in this.GetDateTimeColumnFilterValues(columnFilterValues, out shouldCreateBlanks))
            {
                int year = time.Year;
                if (num != year)
                {
                    month = -1;
                    num = year;
                    item = new ExcelDateColumnFilterValue(year, ExcelDateColumnFilterScope.Month);
                    list2.AddItem(item);
                }
                int num4 = time.Month;
                if (month != num4)
                {
                    month = num4;
                    value3 = new ExcelDateColumnFilterValue(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month), ExcelDateColumnFilterScope.Day);
                    item.Items.AddItem(value3);
                }
                ExcelDateDayColumnFilterValue value4 = new ExcelDateDayColumnFilterValue(time.Day, time);
                value3.Items.AddItem(value4);
            }
        }

        protected override string GetActualSearchText() => 
            !string.IsNullOrWhiteSpace(base.SearchText) ? (base.SearchText + " " + this.SearchScope) : base.SearchText;

        protected override List<DateTime> GetAllDates(ExcelColumnFilterValuesListBase items, out bool hasBlankItem)
        {
            hasBlankItem = false;
            List<DateTime> list = new List<DateTime>();
            foreach (ExcelColumnFilterValue value2 in items.GetFilterableValues())
            {
                ExcelDateColumnFilterValue value3 = value2 as ExcelDateColumnFilterValue;
                if (value3 == null)
                {
                    if (!ReferenceEquals(value2, items.ServiceItem_SelectBlanks))
                    {
                        continue;
                    }
                    hasBlankItem = true;
                    continue;
                }
                foreach (ExcelDateColumnFilterValue value4 in value3.Items.GetFilterableValues())
                {
                    foreach (ExcelDateDayColumnFilterValue value5 in value4.Items.GetFilterableValues())
                    {
                        list.Add(value5.Date);
                    }
                }
            }
            return list;
        }

        private DateTime GetDateFromMonth(ExcelDateColumnFilterValue month) => 
            ((ExcelDateDayColumnFilterValue) month.Items[1]).Date;

        private DateTime GetDateFromYear(ExcelDateColumnFilterValue year) => 
            this.GetDateFromMonth((ExcelDateColumnFilterValue) year.Items[1]);

        private List<DateTime> GetDateTimeColumnFilterValues(IList<object> columnFilterValues, out bool shouldCreateBlanks)
        {
            shouldCreateBlanks = false;
            List<DateTime> list = new List<DateTime>();
            foreach (object obj2 in columnFilterValues)
            {
                if ((obj2 == null) || (obj2 is DBNull))
                {
                    shouldCreateBlanks = true;
                    continue;
                }
                if (obj2 is DateTime)
                {
                    list.Add((DateTime) obj2);
                    continue;
                }
                if (obj2 is DateTime?)
                {
                    DateTime? nullable = obj2 as DateTime?;
                    list.Add(nullable.Value);
                }
            }
            list.Sort();
            return list;
        }

        protected internal override string GetDisplayText(object value, string originalDisplayText) => 
            originalDisplayText;

        protected override CriteriaOperator GetFilterCriteria(List<ExcelColumnFilterValue> selectedItems)
        {
            if (selectedItems == null)
            {
                return null;
            }
            ExcelDateTimeColumnFilterValues selectedDates = this.GetSelectedDates(selectedItems);
            CriteriaOperator @operator = null;
            foreach (Tuple<int, int> tuple in selectedDates.GetYearRanges())
            {
                @operator |= ColumnFilterInfoHelper.IncludeYears(base.Column.FieldName, tuple);
            }
            foreach (Tuple<int, int, int> tuple2 in selectedDates.GetMonthRanges())
            {
                @operator |= ColumnFilterInfoHelper.IncludeMonths(base.Column.FieldName, tuple2);
            }
            if (selectedDates.Dates.Count > 0)
            {
                @operator |= MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.Column.FieldName, selectedDates.Dates);
            }
            if (selectedDates.IncludeBlanks)
            {
                @operator |= ColumnFilterInfoHelper.CreateIsNullOrEmptyCriteria(base.Column.FieldName);
            }
            return @operator;
        }

        private ExcelDateTimeColumnFilterValues GetSelectedDates(List<ExcelColumnFilterValue> selectedItems)
        {
            ExcelDateTimeColumnFilterValues values = new ExcelDateTimeColumnFilterValues();
            foreach (ExcelColumnFilterValue value2 in selectedItems)
            {
                ExcelDateColumnFilterValue month = value2 as ExcelDateColumnFilterValue;
                if (month != null)
                {
                    ExcelDateColumnFilterScope filterScope = month.Items.FilterScope;
                    if (filterScope == ExcelDateColumnFilterScope.Day)
                    {
                        values.Months.Add(this.GetDateFromMonth(month));
                        continue;
                    }
                    if (filterScope != ExcelDateColumnFilterScope.Month)
                    {
                        continue;
                    }
                    values.Years.Add(this.GetDateFromYear(month));
                    continue;
                }
                ExcelDateDayColumnFilterValue value4 = value2 as ExcelDateDayColumnFilterValue;
                if (value4 != null)
                {
                    values.Dates.Add(value4.Date);
                    continue;
                }
                if (ColumnFilterInfoHelper.IsNullOrEmptyString(value2.EditValue))
                {
                    values.IncludeBlanks = true;
                }
            }
            return values;
        }

        protected override IEnumerable<ExcelColumnFilterValue> GetSelectedItemsCore() => 
            this.EnumerateItemsCore(items => this.GetSelectedItemsCore(items));

        private IEnumerable<ExcelColumnFilterValue> GetSelectedItemsCore(ExcelColumnFilterValuesListBase list) => 
            list.GetFilterableValues();

        protected override BaseEditSettings GetValueEditSettings() => 
            null;

        protected override IEnumerable<ExcelColumnFilterValue> GetVisibleSelectedItemsCore() => 
            this.EnumerateItemsCore(items => this.GetVisibleSelectedItemsCore(items));

        private IEnumerable<ExcelColumnFilterValue> GetVisibleSelectedItemsCore(ExcelColumnFilterValuesListBase list)
        {
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__29_0;
                predicate = <>c.<>9__29_0 = item => item.IsVisible;
            }
            return list.GetFilterableValues().Where<ExcelColumnFilterValue>(predicate);
        }

        protected override void UpdateExpandState()
        {
            if (this.BeginUpdateExpandState != null)
            {
                this.BeginUpdateExpandState(this, EventArgs.Empty);
            }
            this.EnumerateItems((ExcelDateColumnFilterList) base.FilterItems);
            if (this.EndUpdateExpandState != null)
            {
                this.EndUpdateExpandState(this, EventArgs.Empty);
            }
        }

        protected override void UpdateFilter(string oldFilter, string newFilter)
        {
            ((ExcelDateColumnFilterList) base.FilterItems).UpdateFilter(this.SearchScope, oldFilter, newFilter);
        }

        protected override bool RoundDateTimeValues =>
            true;

        protected override bool ImplyNullLikeEmptyStringWhenFiltering =>
            true;

        public override bool ShowSearchPanelScopeSelector =>
            true;

        public override ExcelDateColumnFilterScope SearchScope
        {
            get => 
                this._SearchScope;
            set
            {
                if (this._SearchScope != value)
                {
                    this._SearchScope = value;
                    base.OnPropertyChanged("SearchScope");
                    base.FilterItems.LockCheckedChanged = true;
                    this.UpdateFilter(base.SearchText, base.SearchText);
                    base.FilterItems.LockCheckedChanged = false;
                    base.OnPropertyChanged("ActualSearchText");
                    this.UpdateExpandState();
                }
            }
        }

        public override bool IsHierarchicalView =>
            true;

        public override string ChildPropertyName =>
            "Items";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelDateTimeColumnFilterInfo.<>c <>9 = new ExcelDateTimeColumnFilterInfo.<>c();
            public static Func<ExcelColumnFilterValue, bool> <>9__29_0;

            internal bool <GetVisibleSelectedItemsCore>b__29_0(ExcelColumnFilterValue item) => 
                item.IsVisible;
        }
    }
}

