namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ListColumnFilterInfo : ListColumnFilterInfoBase
    {
        public ListColumnFilterInfo(ColumnBase column) : base(column)
        {
            this.ColumnMRUFilters = new List<CustomComboBoxItem>();
        }

        internal virtual void AddColumnMRUFilter(CustomComboBoxItem filter)
        {
            if (filter != null)
            {
                for (int i = this.ColumnMRUFilters.Count - 1; i > -1; i--)
                {
                    if (this.AreCustomItemsEquals(this.ColumnMRUFilters[i], filter))
                    {
                        this.ColumnMRUFilters.Remove(this.ColumnMRUFilters[i]);
                    }
                }
                this.ColumnMRUFilters.Insert(0, filter);
                if (this.ColumnMRUFilters.Count > (base.View.DataControl.MRUColumnFilterListCount + 1))
                {
                    this.ColumnMRUFilters.RemoveAt(base.View.DataControl.MRUColumnFilterListCount + 1);
                }
            }
        }

        protected override void AddMRUItemsToList(IList list)
        {
            if (base.View.DataControl.AllowColumnMRUFilterList && !base.Column.IsAsyncLookup)
            {
                this.TruncateColumnMRUFilterToMaxCount();
                CriteriaOperator columnFilterCriteria = base.View.DataControl.GetColumnFilterCriteria(base.Column);
                foreach (CustomComboBoxItem item in this.ColumnMRUFilters)
                {
                    CriteriaOperator filterCriteria = this.GetFilterCriteria(item);
                    if (!Equals(filterCriteria, columnFilterCriteria) && (list.Count < base.View.DataControl.MRUColumnFilterListCount))
                    {
                        list.Add(item);
                    }
                }
                if (list.Count != 0)
                {
                    list.Add(this.CreateSeparator());
                }
            }
        }

        protected override bool AllowColumnFilterValues() => 
            base.Column.AllowFilter(AllowedBinaryFilters.Equals);

        protected bool AreCustomItemsEquals(object obj1, object obj2)
        {
            CustomComboBoxItem item = obj1 as CustomComboBoxItem;
            CustomComboBoxItem item2 = obj2 as CustomComboBoxItem;
            return ((item != null) && ((item2 != null) && Equals(item.DisplayValue, item2.DisplayValue)));
        }

        public override bool CanShowFilterPopup() => 
            base.Column.AllowFilter(AllowedUnaryFilters.IsNull) || (base.Column.AllowFilter(AllowedUnaryFilters.IsNotNull) || base.Column.AllowFilter(AllowedBinaryFilters.Equals));

        protected bool CheckIsItemDefault(CustomComboBoxItem item)
        {
            bool flag;
            using (List<object>.Enumerator enumerator = this.GetDefaultItems(true).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (!this.AreCustomItemsEquals(current, item))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private void ClearColumnMRUFilter()
        {
            this.ColumnMRUFilters.Clear();
        }

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
            if (base.Column.IsAsyncLookup)
            {
                ((InstantFeedbackColumnFilterList) this.ComboBoxHelper.ComboBoxEdit.ItemsSource).Destroy();
            }
            this.ComboBoxHelper.ComboBoxEdit.ItemsSource = null;
        }

        internal override PopupBaseEdit CreateColumnFilterPopup()
        {
            this.ComboBoxHelper = new ColumnFilterComboBoxEditHelper(base.Column);
            this.ComboBoxHelper.CreateComboBoxEdit(new Action<ComboBoxEdit>(this.SetupComboBox));
            return this.ComboBoxHelper.ComboBoxEdit;
        }

        private CriteriaOperator CreateEqualsOperator(object value) => 
            base.View.DataControl.CalcColumnFilterCriteriaByValue(base.Column, value);

        protected override IList CreateFilterList() => 
            ColumnFilterInfoHelper.CreateFilterList(base.View, base.Column);

        private object CreateSeparator()
        {
            CustomItem item = new CustomItem {
                ItemContainerStyle = new Style(typeof(ComboBoxEditItem))
            };
            item.ItemContainerStyle.Setters.Add(new Setter(UIElement.IsEnabledProperty, false));
            item.ItemContainerStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            item.EditValue = new object();
            item.ItemTemplate = XamlHelper.GetTemplate("<Separator />");
            return item;
        }

        private CriteriaOperator GetCriteriaOperator(bool isBlanks)
        {
            if (!this.IsStringColumn())
            {
                return (!isBlanks ? new OperandProperty(base.Column.FieldName).IsNotNull() : new OperandProperty(base.Column.FieldName).IsNull());
            }
            if (isBlanks)
            {
                CriteriaOperator[] operatorArray1 = new CriteriaOperator[] { new OperandProperty(base.Column.FieldName) };
                return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operatorArray1);
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(base.Column.FieldName) };
            return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands).Not();
        }

        protected override List<object> GetDefaultItems(bool addShowAllItem)
        {
            List<object> list = new List<object>();
            if (addShowAllItem)
            {
                CustomComboBoxItem item = new CustomComboBoxItem();
                item.DisplayValue = base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterAll);
                item.EditValue = new CustomComboBoxItem();
                list.Add(item);
            }
            if (base.Column.AllowFilter(AllowedUnaryFilters.IsNull))
            {
                CustomComboBoxItem item = new CustomComboBoxItem();
                item.DisplayValue = base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterBlanks);
                CustomComboBoxItem item3 = new CustomComboBoxItem();
                item3.EditValue = this.GetCriteriaOperator(true);
                item.EditValue = item3;
                list.Add(item);
            }
            if (base.Column.AllowFilter(AllowedUnaryFilters.IsNotNull))
            {
                CustomComboBoxItem item = new CustomComboBoxItem();
                item.DisplayValue = base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterNonBlanks);
                CustomComboBoxItem item5 = new CustomComboBoxItem();
                item5.EditValue = this.GetCriteriaOperator(false);
                item.EditValue = item5;
                list.Add(item);
            }
            return list;
        }

        protected override string GetEditValueString(object item)
        {
            CustomComboBoxItem item2 = item as CustomComboBoxItem;
            return ((item2 == null) ? null : (item2.EditValue as string));
        }

        protected internal override CriteriaOperator GetFilterCriteria()
        {
            object selectedItem = this.ComboBoxHelper.ComboBoxEdit.SelectedItem;
            if (selectedItem == null)
            {
                return null;
            }
            CustomComboBoxItem customItem = selectedItem as CustomComboBoxItem;
            return ((customItem != null) ? this.GetFilterCriteria(customItem) : this.CreateEqualsOperator(this.GetValue(selectedItem)));
        }

        protected CriteriaOperator GetFilterCriteria(CustomComboBoxItem customItem) => 
            (customItem != null) ? (!(customItem.EditValue is CustomComboBoxItem) ? (!(customItem.EditValue is CriteriaOperator) ? this.CreateEqualsOperator(customItem.EditValue) : (customItem.EditValue as CriteriaOperator)) : ((customItem.EditValue as CustomComboBoxItem).EditValue as CriteriaOperator)) : null;

        protected object GetValue(object item)
        {
            DataProxy proxy = item as DataProxy;
            if (proxy != null)
            {
                item = proxy.f_component;
            }
            return (!(item is CustomComboBoxItem) ? ((this.ComboBoxHelper.ComboBoxEditSettings == null) ? item : this.ComboBoxHelper.ComboBoxEditSettings.GetValueFromItem(item)) : (item as CustomComboBoxItem).EditValue);
        }

        protected bool IsStringColumn()
        {
            DataColumnInfo info = base.View.DataProviderBase.Columns[base.Column.FieldName];
            return (((base.Column.ColumnFilterMode == ColumnFilterMode.DisplayText) || ((info == null) || info.Type.Equals(typeof(string)))) ? !(base.Column.EditSettings is CheckEditSettings) : false);
        }

        internal override void OnPopupClosed(PopupBaseEdit popup, bool applyFilter)
        {
            if (applyFilter && !this.ImmediateUpdateFilter)
            {
                CustomComboBoxItem selectedItem = ((ComboBoxEdit) popup).SelectedItem as CustomComboBoxItem;
                if (!this.CheckIsItemDefault(selectedItem))
                {
                    this.AddColumnMRUFilter(selectedItem);
                }
            }
            base.OnPopupClosed(popup, applyFilter);
        }

        protected virtual void SetupComboBox(ComboBoxEdit comboBoxEdit)
        {
            comboBoxEdit.StyleSettings = new ComboBoxStyleSettings();
            comboBoxEdit.ShowNullText = false;
            comboBoxEdit.IsTextEditable = false;
            comboBoxEdit.AutoComplete = true;
            comboBoxEdit.FocusPopupOnOpen = false;
        }

        protected override PopupFooterButtons ShowButtons() => 
            PopupFooterButtons.None;

        protected override void ShowWaitIndicator(IList list)
        {
            WaitIndicatorItem item1 = new WaitIndicatorItem();
            item1.IsHitTestVisible = false;
            WaitIndicatorItem item = item1;
            list.Add(item);
        }

        protected override void SortColumnFilterItems(object[] values)
        {
            ColumnFilterInfoHelper.SortComboBoxItems(values, base.Column, this.ComboBoxHelper.ComboBoxEditSettings, item => this.GetItem(this.ComboBoxHelper, item, this.ImplyNullLikeEmptyStringWhenFiltering));
            base.EmptyIncluded = false;
        }

        private void TruncateColumnMRUFilterToMaxCount()
        {
            for (int i = base.View.DataControl.MRUColumnFilterListCount + 1; i < this.ColumnMRUFilters.Count; i++)
            {
                this.ColumnMRUFilters.RemoveAt(i);
            }
        }

        protected override void UpdatePopupData(PopupBaseEdit popup)
        {
            this.UpdatePopupData(popup, null);
        }

        protected override void UpdatePopupData(PopupBaseEdit popup, object[] values)
        {
            bool isColumnFilterItemsLoading = false;
            IList list = base.GetColumnFilterItems(values, new Func<IList>(this.CreateFilterList), new Action<object[]>(this.SortColumnFilterItems), this.GetIncludeFilteredOut(), this.RoundDateTimeValues, this.ImplyNullLikeEmptyStringWhenFiltering, this.AddNullItem, out isColumnFilterItemsLoading);
            base.IsColumnFilterItemsLoading = isColumnFilterItemsLoading;
            if (base.IsColumnFilterItemsLoading)
            {
                this.ShowWaitIndicator(list);
            }
            if (!ReferenceEquals(this.ComboBoxHelper.ComboBoxEdit, popup))
            {
                this.ComboBoxHelper.ComboBoxEdit = popup as ComboBoxEdit;
            }
            this.ComboBoxHelper.UpdateItems(base.View, base.IsColumnFilterItemsLoading, list);
        }

        protected override bool ImmediateUpdateFilter =>
            false;

        protected List<CustomComboBoxItem> ColumnMRUFilters { get; set; }

        protected ColumnFilterComboBoxEditHelper ComboBoxHelper { get; private set; }

        protected override bool CalcColumnCriteriaOperator =>
            base.View.DataProviderBase.IsVirtualSource;
    }
}

