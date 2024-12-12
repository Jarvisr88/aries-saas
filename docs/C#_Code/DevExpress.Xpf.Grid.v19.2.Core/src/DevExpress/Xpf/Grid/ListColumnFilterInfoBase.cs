namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ListColumnFilterInfoBase : ColumnFilterInfoBase
    {
        public ListColumnFilterInfoBase(ColumnBase column) : base(column)
        {
        }

        protected virtual void AddMRUItemsToList(IList list)
        {
        }

        protected abstract bool AllowColumnFilterValues();
        protected virtual bool AllowImplyNullLikeEmptyStringWhenFiltering(bool implyNullLikeEmptyStringWhenFiltering) => 
            implyNullLikeEmptyStringWhenFiltering;

        protected virtual IList CreateFilterList() => 
            new List<object>();

        protected virtual CriteriaOperator GetColumnCriteriaOperator()
        {
            if (!this.CalcColumnCriteriaOperator || base.Column.GetShowAllTableValuesInFilterPopup())
            {
                return null;
            }
            CriteriaOperator left = base.View.DataProviderBase.IsVirtualSource ? base.View.DataControl.ExtraFilter : null;
            foreach (KeyValuePair<OperandProperty, CriteriaOperator> pair in CriteriaColumnAffinityResolver.SplitByColumnsWithOptionalLegacyFallback(base.View.DataControl.GetActualFilterCriteria()))
            {
                string propertyName = pair.Key.PropertyName;
                ColumnBase objA = !string.IsNullOrEmpty(propertyName) ? base.View.DataControl.ColumnsCore[pair.Key.PropertyName] : null;
                if ((objA == null) || !ReferenceEquals(objA, base.Column))
                {
                    left = CriteriaOperator.And(left, pair.Value);
                }
            }
            return left;
        }

        protected internal IList GetColumnFilterItems(object[] values, Func<IList> createFilterList, Action<object[]> sortAction, bool includeFilteredOut, bool roundDateTimeValues, bool implyNullLikeEmptyStringWhenFiltering, bool addNullItem, out bool isColumnFilterItemsLoading)
        {
            isColumnFilterItemsLoading = false;
            IList list = createFilterList();
            this.AddMRUItemsToList(list);
            foreach (object obj2 in this.GetDefaultItems(base.Column.IsFiltered))
            {
                list.Add(obj2);
            }
            if (base.View == null)
            {
                return list;
            }
            values ??= this.GetColumnFilterValues(includeFilteredOut, roundDateTimeValues, implyNullLikeEmptyStringWhenFiltering);
            if (values == null)
            {
                return list;
            }
            if (this.IsWaitingAsyncData(values) && !base.Column.IsAsyncLookup)
            {
                isColumnFilterItemsLoading = true;
                return list;
            }
            if (sortAction != null)
            {
                sortAction(values);
            }
            int num = 0;
            object[] objArray = values;
            int index = 0;
            while (true)
            {
                if (index < objArray.Length)
                {
                    object item = objArray[index];
                    if (num != base.Column.ColumnFilterPopupMaxRecordsCount)
                    {
                        if (this.ShouldIncludeItem(item, implyNullLikeEmptyStringWhenFiltering, addNullItem))
                        {
                            list.Add(item);
                            num++;
                        }
                        index++;
                        continue;
                    }
                }
                return list;
            }
        }

        protected virtual object[] GetColumnFilterValues(bool includeFilteredOut, bool roundDateTimeValues, bool implyNullLikeEmptyStringWhenFiltering) => 
            this.AllowColumnFilterValues() ? base.View.DataControl.GetUniqueColumnValues(base.Column, includeFilteredOut, roundDateTimeValues, base.View.DataControl.GetColumnFilterCriteria(base.Column), new bool?(implyNullLikeEmptyStringWhenFiltering), this.GetColumnCriteriaOperator()) : null;

        protected bool GetComboBoxItem(ColumnFilterComboBoxEditHelper comboBoxHelper, object value, out object item)
        {
            item = null;
            if (comboBoxHelper.ComboBoxEditSettings != null)
            {
                if (!string.IsNullOrEmpty(comboBoxHelper.ComboBoxEditSettings.ValueMember) || EnumItemsSource.IsEnumItemsSource(comboBoxHelper.ComboBoxEditSettings.ItemsSource))
                {
                    item = comboBoxHelper.ComboBoxEditSettings.GetItemFromValue(value);
                    return true;
                }
                if (comboBoxHelper.ComboBoxEditSettings.ItemTemplate != null)
                {
                    item = value;
                    return true;
                }
            }
            return false;
        }

        private string GetComboBoxItemDisplayValue(object value)
        {
            if (base.Column.IsAsyncLookup)
            {
                return string.Empty;
            }
            if (this.IsDateTimeColumn() && (this.RoundDateTimeValues && !string.IsNullOrEmpty(base.Column.RoundDateDisplayFormat)))
            {
                return string.Format(FormatStringConverter.GetDisplayFormat(base.Column.RoundDateDisplayFormat), value);
            }
            int? rowHandle = null;
            return base.View.GetColumnDisplayText(value, base.Column, rowHandle);
        }

        protected virtual List<object> GetDefaultItems(bool addShowAllItem) => 
            new List<object>();

        protected abstract string GetEditValueString(object item);
        protected virtual bool GetIncludeFilteredOut() => 
            ColumnFilterInfoHelper.GetIncludeFilteredOut(base.Column);

        protected internal virtual object GetItem(ColumnFilterComboBoxEditHelper comboBoxHelper, object value, bool implyNullLikeEmptyStringWhenFiltering)
        {
            object obj2;
            if (implyNullLikeEmptyStringWhenFiltering && ((value == null) || (value is DBNull)))
            {
                value = string.Empty;
            }
            if (base.Column.ColumnFilterMode == ColumnFilterMode.DisplayText)
            {
                CustomComboBoxItem item1 = new CustomComboBoxItem();
                item1.EditValue = value;
                item1.DisplayValue = value;
                return item1;
            }
            if (this.GetComboBoxItem(comboBoxHelper, value, out obj2))
            {
                return obj2;
            }
            CustomComboBoxItem item2 = new CustomComboBoxItem();
            item2.EditValue = value;
            item2.DisplayValue = this.GetComboBoxItemDisplayValue(value);
            return item2;
        }

        protected bool IsWaitingAsyncData(object[] values) => 
            (values.Length != 0) && AsyncServerModeDataController.IsNoValue(values[0]);

        protected virtual bool ShouldIncludeItem(object item, bool implyNullLikeEmptyStringWhenFiltering, bool addNullItem)
        {
            if (item == null)
            {
                return addNullItem;
            }
            string editValueString = this.GetEditValueString(item);
            if (editValueString == null)
            {
                return true;
            }
            if (!implyNullLikeEmptyStringWhenFiltering || (editValueString != string.Empty))
            {
                return ((editValueString != string.Empty) || (base.Column.ColumnFilterMode != ColumnFilterMode.DisplayText));
            }
            if (this.EmptyIncluded)
            {
                return false;
            }
            this.EmptyIncluded = true;
            return base.Column.AllowFilter(AllowedUnaryFilters.IsNullOrEmpty);
        }

        protected abstract void ShowWaitIndicator(IList list);
        protected virtual void SortColumnFilterItems(object[] values)
        {
            this.EmptyIncluded = false;
        }

        protected bool EmptyIncluded { get; set; }

        protected bool IsColumnFilterItemsLoading { get; set; }

        protected abstract bool CalcColumnCriteriaOperator { get; }
    }
}

