namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    public class CheckedListColumnFilterInfo : ListColumnFilterInfo
    {
        private bool isSelectionLocked;
        private List<object> SelectedItems;

        public CheckedListColumnFilterInfo(ColumnBase column) : base(column)
        {
            this.SelectedItems = new List<object>();
        }

        internal override void AddColumnMRUFilter(CustomComboBoxItem filter)
        {
        }

        private void AddSelectedItems(ComboBoxEdit comboBox, IEnumerable values)
        {
            if (values != null)
            {
                this.SelectComboBoxItems(comboBox, values);
                foreach (object obj2 in comboBox.SelectedItems)
                {
                    this.SelectedItems.Add(obj2);
                }
            }
        }

        protected override bool AllowColumnFilterValues() => 
            true;

        private void BeginSelection(ComboBoxEdit comboBox)
        {
            this.isSelectionLocked = true;
            comboBox.BeginInit();
        }

        public override bool CanShowFilterPopup() => 
            !this.GetCanShowUniqueValues() ? (this.ImplyNullLikeEmptyStringWhenFiltering && (base.Column.AllowFilter(AllowedUnaryFilters.IsNullOrEmpty) && this.GetIsNullableColumn())) : true;

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
            base.ClearPopupData(popup);
            ((ComboBoxEdit) popup).PopupContentSelectionChanged -= new SelectionChangedEventHandler(this.PopupListBoxSelectionChanged);
        }

        private void EndSelection(ComboBoxEdit comboBox)
        {
            comboBox.EndInit();
            this.isSelectionLocked = false;
        }

        protected object FindItem(IEnumerable source, object value)
        {
            object obj2;
            object obj4;
            if ((base.Column.ColumnFilterMode != ColumnFilterMode.DisplayText) && base.GetComboBoxItem(base.ComboBoxHelper, value, out obj2))
            {
                return obj2;
            }
            using (IEnumerator enumerator = source.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (!(current is CustomComboBoxItem) || !Equals((current as CustomComboBoxItem).EditValue, value))
                        {
                            continue;
                        }
                        obj4 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return obj4;
        }

        protected virtual bool GetCanShowUniqueValues() => 
            (!this.IsDateTimeColumn() || !this.RoundDateTimeValues) ? base.Column.AllowFilter(AllowedAnyOfFilters.AnyOf) : base.Column.AllowFilter(AllowedDateTimeFilters.MultipleDateRanges);

        protected override CriteriaOperator GetColumnCriteriaOperator()
        {
            if (this.GetCanShowUniqueValues())
            {
                return base.GetColumnCriteriaOperator();
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(base.Column.FieldName) };
            return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
        }

        protected override List<object> GetDefaultItems(bool addShowAllItem) => 
            new List<object>();

        protected internal override CriteriaOperator GetFilterCriteria()
        {
            IEnumerable<DateTime> dates = this.PrepareListOfDateTimeItems(this.SelectedItems);
            CriteriaOperator @operator = null;
            @operator = ((dates == null) || !base.Column.RoundDateTimeForColumnFilter) ? ColumnFilterInfoHelper.CreateCriteriaOperator(this.SelectedItems, base.Column.FieldName, base.View.DataControl.ImplyNullLikeEmptyStringWhenFiltering, new Func<object, object>(this.GetValue)) : MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.Column.FieldName, dates);
            return (@operator | ColumnFilterInfoHelper.GetIsNullOrEmptyCriteria(this.SelectedItems, base.Column.FieldName, base.View.DataControl.ImplyNullLikeEmptyStringWhenFiltering, new Func<object, object>(this.GetValue)));
        }

        protected override CriteriaOperator GetFilterCriteriaCore() => 
            base.View.GetCheckedFilterPopupFilterCriteria(base.Column, this.SelectedItems);

        protected override bool GetIncludeFilteredOut() => 
            base.Column.GetShowAllTableValuesInCheckedFilterPopup() || base.Column.IsFiltered;

        protected internal override object GetItem(ColumnFilterComboBoxEditHelper comboBoxHelper, object value, bool implyNullLikeEmptyStringWhenFiltering)
        {
            if (!implyNullLikeEmptyStringWhenFiltering || ((value != null) && (!ColumnFilterInfoHelper.IsNullOrEmptyString(value) && !(value is DBNull))))
            {
                return base.GetItem(comboBoxHelper, value, implyNullLikeEmptyStringWhenFiltering);
            }
            CustomComboBoxItem item1 = new CustomComboBoxItem();
            item1.EditValue = string.Empty;
            item1.DisplayValue = base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterBlanks);
            return item1;
        }

        private IEnumerable GetSelectedItems(ComboBoxEdit comboBox, CriteriaOperator op) => 
            base.View.GetCheckedFilterPopupSelectedItems(base.Column, comboBox, op);

        protected internal override IEnumerable GetSelectedItems(IEnumerable items, CriteriaOperator op)
        {
            IEnumerable<DateTime> dateTimeItems = this.PrepareListOfDateTimeItems(items);
            IEnumerable<object> selectedItems = null;
            if (dateTimeItems != null)
            {
                selectedItems = this.GetSelectedItemsForDateTime(op, dateTimeItems).Cast<object>();
            }
            else
            {
                List<CriteriaOperator> customOperators = null;
                selectedItems = ColumnFilterInfoHelper.GetSelectedItems(op, out customOperators).Do<List<object>>(delegate (List<object> list) {
                    if (customOperators != null)
                    {
                        list.AddRange(customOperators);
                    }
                });
            }
            return ColumnFilterInfoHelper.AddNullOrEmptyOperator(selectedItems, op);
        }

        internal IEnumerable GetSelectedItemsCore(ComboBoxEdit comboBox, CriteriaOperator op) => 
            this.GetSelectedItems((IEnumerable) comboBox.ItemsSource, op);

        internal IEnumerable<DateTime> GetSelectedItemsForDateTime(CriteriaOperator op, IEnumerable<DateTime> dateTimeItems) => 
            (dateTimeItems != null) ? MultiselectRoundedDateTimeFilterHelper.GetCheckedDates(op, base.Column.FieldName, dateTimeItems) : null;

        protected override bool IsDateTimeColumn() => 
            base.IsDateTimeColumn() && (this.RoundDateTimeValues && (base.Column.ColumnFilterMode == ColumnFilterMode.Value));

        protected override void OnPopupOpenedCore(PopupBaseEdit popup)
        {
            base.OnPopupOpenedCore(popup);
            ComboBoxEdit comboBox = (ComboBoxEdit) popup;
            this.RecreateSelectedItems(comboBox);
            comboBox.PopupContentSelectionChanged += new SelectionChangedEventHandler(this.PopupListBoxSelectionChanged);
        }

        private void PopupListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.isSelectionLocked)
            {
                foreach (object obj2 in e.AddedItems)
                {
                    DataProxy proxy = obj2 as DataProxy;
                    if (proxy != null)
                    {
                        this.SelectedItems.Add(proxy.f_component);
                        continue;
                    }
                    this.SelectedItems.Add(obj2);
                }
                foreach (object obj3 in e.RemovedItems)
                {
                    DataProxy proxy2 = obj3 as DataProxy;
                    if (proxy2 != null)
                    {
                        this.SelectedItems.Remove(proxy2.f_component);
                        continue;
                    }
                    this.SelectedItems.Remove(obj3);
                }
                this.UpdateColumnFilterIfNeeded(new Func<CriteriaOperator>(this.GetFilterCriteriaCore));
            }
        }

        private IEnumerable<DateTime> PrepareListOfDateTimeItems(IEnumerable items)
        {
            IEnumerable<DateTime> enumerable;
            IList<DateTime> list = new List<DateTime>();
            using (IEnumerator enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        CustomComboBoxItem item = current as CustomComboBoxItem;
                        if (current is DateTime)
                        {
                            list.Add((DateTime) current);
                            continue;
                        }
                        if ((item != null) && (item.EditValue is DateTime))
                        {
                            list.Add((DateTime) item.EditValue);
                            continue;
                        }
                        if ((item != null) && (base.View.DataControl.ImplyNullLikeEmptyStringWhenFiltering && ColumnFilterInfoHelper.IsNullOrEmptyString(item.EditValue)))
                        {
                            continue;
                        }
                        enumerable = null;
                    }
                    else
                    {
                        return ((list.Count == 0) ? null : list);
                    }
                    break;
                }
            }
            return enumerable;
        }

        private void RecreateSelectedItems(ComboBoxEdit comboBox)
        {
            comboBox.SelectedItems.Clear();
            this.SelectedItems.Clear();
            CriteriaOperator columnFilterCriteria = base.View.DataControl.GetColumnFilterCriteria(base.Column);
            if (columnFilterCriteria != null)
            {
                this.AddSelectedItems(comboBox, this.GetSelectedItems(comboBox, columnFilterCriteria));
            }
        }

        private void SelectComboBoxItems(ComboBoxEdit comboBox, IEnumerable values)
        {
            this.BeginSelection(comboBox);
            foreach (object obj2 in values)
            {
                object obj3 = obj2;
                if (base.View.DataControl.ImplyNullLikeEmptyStringWhenFiltering && (obj2 == null))
                {
                    obj3 = string.Empty;
                }
                object item = this.FindItem((IEnumerable) comboBox.ItemsSource, obj3);
                if (item != null)
                {
                    comboBox.SelectedItems.Add(item);
                }
            }
            this.EndSelection(comboBox);
        }

        protected override void SetupComboBox(ComboBoxEdit comboBoxEdit)
        {
            comboBoxEdit.StyleSettings = new CheckedComboBoxStyleSettings();
            comboBoxEdit.ShowNullText = false;
            comboBoxEdit.IsTextEditable = false;
            comboBoxEdit.AutoComplete = true;
            comboBoxEdit.FocusPopupOnOpen = true;
        }

        protected override PopupFooterButtons ShowButtons() => 
            !this.ImmediateUpdateFilter ? PopupFooterButtons.OkCancel : PopupFooterButtons.None;

        protected override void UpdatePopupData(PopupBaseEdit popup, object[] values)
        {
            base.UpdatePopupData(popup, values);
            ComboBoxEdit comboBox = (ComboBoxEdit) popup;
            this.RecreateSelectedItems(comboBox);
        }

        protected override bool ImmediateUpdateFilter =>
            base.Column.ImmediateUpdateColumnFilter;
    }
}

