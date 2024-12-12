namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ExcelColumnFilterInfo : ExcelColumnFilterInfoBase
    {
        internal static readonly Type[] NotSupportEditSettings;
        private UnsubscribeAction unsubscribeAsyncLookupColumn;

        static ExcelColumnFilterInfo()
        {
            Type[] typeArray1 = new Type[9];
            typeArray1[0] = typeof(ColorEditSettings);
            typeArray1[1] = typeof(MemoEditSettings);
            typeArray1[2] = typeof(TrackBarEditSettings);
            typeArray1[3] = typeof(PopupImageEditSettings);
            typeArray1[4] = typeof(CheckEditSettings);
            typeArray1[5] = typeof(SparklineEditSettings);
            typeArray1[6] = typeof(BarCodeEditSettings);
            typeArray1[7] = typeof(HyperlinkEditSettings);
            typeArray1[8] = typeof(ToggleSwitchEditSettings);
            NotSupportEditSettings = typeArray1;
        }

        public ExcelColumnFilterInfo(ColumnBase column) : base(column)
        {
        }

        protected override bool CanShowValuesCore() => 
            base.Column.AllowFilter(DevExpress.Xpf.Grid.AllowedAnyOfFilters.AnyOf);

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
            base.ClearPopupData(popup);
            if (this.unsubscribeAsyncLookupColumn == null)
            {
                UnsubscribeAction unsubscribeAsyncLookupColumn = this.unsubscribeAsyncLookupColumn;
            }
            else
            {
                this.unsubscribeAsyncLookupColumn();
            }
            this.unsubscribeAsyncLookupColumn = null;
        }

        protected override ExcelColumnFilterValuesListBase CreateList() => 
            new ExcelColumnFilterValuesList();

        private List<DateTime> ExtractDates(IEnumerable<ExcelColumnFilterValue> selectedItems, out bool hasBlankItem)
        {
            hasBlankItem = false;
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__25_0;
                predicate = <>c.<>9__25_0 = item => item.EditValue is DateTime;
            }
            Func<ExcelColumnFilterValue, DateTime> selector = <>c.<>9__25_1;
            if (<>c.<>9__25_1 == null)
            {
                Func<ExcelColumnFilterValue, DateTime> local2 = <>c.<>9__25_1;
                selector = <>c.<>9__25_1 = item => (DateTime) item.EditValue;
            }
            return selectedItems.Where<ExcelColumnFilterValue>(predicate).Select<ExcelColumnFilterValue, DateTime>(selector).ToList<DateTime>();
        }

        protected override void FillList(ExcelColumnFilterValuesListBase items, IList<object> columnFilterValues, out bool shouldCreateBlanks)
        {
            shouldCreateBlanks = false;
            foreach (object obj2 in columnFilterValues)
            {
                string text1;
                if ((obj2 == null) || (ColumnFilterInfoHelper.IsNullOrEmptyString(obj2) || (obj2 is DBNull)))
                {
                    shouldCreateBlanks = true;
                    continue;
                }
                ICustomItem item = obj2 as ICustomItem;
                if (item != null)
                {
                    items.AddItem(new ExcelColumnFilterValue(item.EditValue, item.DisplayValue));
                    continue;
                }
                if (!this.IsAsyncLookupColumn)
                {
                    text1 = null;
                }
                else
                {
                    int? rowHandle = null;
                    text1 = base.View.GetColumnDisplayText(obj2, base.Column, rowHandle);
                }
                items.AddItem(new ExcelColumnFilterValue(obj2, text1));
            }
            if (this.IsAsyncLookupColumn)
            {
                this.SubscribeAsyncLookupColumn();
            }
        }

        protected override string GetActualSearchText() => 
            base.SearchText;

        protected override List<DateTime> GetAllDates(ExcelColumnFilterValuesListBase items, out bool hasBlankItem) => 
            this.ExtractDates(items, out hasBlankItem);

        protected internal override string GetDisplayText(object value, string originalDisplayText)
        {
            if ((base.Column.ColumnFilterMode == ColumnFilterMode.DisplayText) || (base.View == null))
            {
                return originalDisplayText;
            }
            int? rowHandle = null;
            return base.View.GetColumnDisplayText(value, base.Column, rowHandle);
        }

        protected override CriteriaOperator GetFilterCriteria(List<ExcelColumnFilterValue> selectedItems)
        {
            CriteriaOperator @operator;
            if (selectedItems == null)
            {
                return null;
            }
            bool hasBlankItem = false;
            List<DateTime> dates = this.ExtractDates(selectedItems, out hasBlankItem);
            if (!this.RoundDateTimeValues || (dates.Count <= 0))
            {
                @operator = ColumnFilterInfoHelper.CreateCriteriaOperator(selectedItems, base.Column.FieldName, this.ImplyNullLikeEmptyStringWhenFiltering, new Func<object, object>(this.GetValue)) | ColumnFilterInfoHelper.GetIsNullOrEmptyCriteria(selectedItems, base.Column.FieldName, this.ImplyNullLikeEmptyStringWhenFiltering, new Func<object, object>(this.GetValue));
            }
            else
            {
                @operator = MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.Column.FieldName, dates);
                if (hasBlankItem)
                {
                    @operator |= ColumnFilterInfoHelper.CreateIsNullOrEmptyCriteria(base.Column.FieldName);
                }
            }
            return @operator;
        }

        protected IEnumerable<ExcelColumnFilterValue> GetSelectedItems()
        {
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__23_0;
                predicate = <>c.<>9__23_0 = item => (item.IsChecked == null) ? false : item.IsChecked.Value;
            }
            return base.FilterItems.GetFilterableValues().Where<ExcelColumnFilterValue>(predicate);
        }

        protected override IEnumerable<ExcelColumnFilterValue> GetSelectedItemsCore() => 
            this.GetSelectedItems();

        protected override BaseEditSettings GetValueEditSettings() => 
            ColumnFilterInfoHelper.CanUseEditSettingsInExcelColumnFilter(base.Column) ? base.Column.EditSettings : null;

        protected IEnumerable<ExcelColumnFilterValue> GetVisibleSelectedItems()
        {
            Func<ExcelColumnFilterValue, bool> predicate = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<ExcelColumnFilterValue, bool> local1 = <>c.<>9__24_0;
                predicate = <>c.<>9__24_0 = item => (item.IsChecked != null) && (item.IsChecked.Value && item.IsVisible);
            }
            return base.FilterItems.GetFilterableValues().Where<ExcelColumnFilterValue>(predicate);
        }

        protected override IEnumerable<ExcelColumnFilterValue> GetVisibleSelectedItemsCore() => 
            this.GetVisibleSelectedItems();

        private void SubscribeAsyncLookupColumn()
        {
            this.unsubscribeAsyncLookupColumn = AsyncLookupColumnListener.Subscribe(base.Column.ItemsProvider, new Action<object>(this.UpdateValueFromAsyncLookupColumn));
        }

        protected override void UpdateFilter(string oldFilter, string newFilter)
        {
            base.FilterItems.UpdateFilter(oldFilter, newFilter);
        }

        private void UpdateValueFromAsyncLookupColumn(object value)
        {
            foreach (ExcelColumnFilterValue value2 in base.FilterItems)
            {
                bool flag1;
                object editValue = value2.EditValue;
                if (editValue != null)
                {
                    flag1 = editValue.Equals(value);
                }
                else
                {
                    object local1 = editValue;
                    flag1 = false;
                }
                if (flag1)
                {
                    int? rowHandle = null;
                    value2.DisplayValue = base.View.GetColumnDisplayText(value, base.Column, rowHandle);
                    break;
                }
            }
        }

        protected override bool ImplyNullLikeEmptyStringWhenFiltering =>
            true;

        public override bool ShowSearchPanelScopeSelector =>
            false;

        public override bool IsHierarchicalView =>
            false;

        public override string ChildPropertyName =>
            null;

        public override ExcelDateColumnFilterScope SearchScope { get; set; }

        private bool IsAsyncLookupColumn =>
            base.Column.IsAsyncLookup && (base.Column.ColumnFilterMode == ColumnFilterMode.Value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelColumnFilterInfo.<>c <>9 = new ExcelColumnFilterInfo.<>c();
            public static Func<ExcelColumnFilterValue, bool> <>9__23_0;
            public static Func<ExcelColumnFilterValue, bool> <>9__24_0;
            public static Func<ExcelColumnFilterValue, bool> <>9__25_0;
            public static Func<ExcelColumnFilterValue, DateTime> <>9__25_1;

            internal bool <ExtractDates>b__25_0(ExcelColumnFilterValue item) => 
                item.EditValue is DateTime;

            internal DateTime <ExtractDates>b__25_1(ExcelColumnFilterValue item) => 
                (DateTime) item.EditValue;

            internal bool <GetSelectedItems>b__23_0(ExcelColumnFilterValue item) => 
                (item.IsChecked == null) ? false : item.IsChecked.Value;

            internal bool <GetVisibleSelectedItems>b__24_0(ExcelColumnFilterValue item) => 
                (item.IsChecked != null) && (item.IsChecked.Value && item.IsVisible);
        }
    }
}

