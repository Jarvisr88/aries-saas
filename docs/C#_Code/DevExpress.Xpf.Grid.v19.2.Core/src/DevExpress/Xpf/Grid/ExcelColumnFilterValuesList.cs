namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Linq;

    public class ExcelColumnFilterValuesList : ExcelColumnFilterValuesListBase
    {
        public override ExcelColumnFilterValue FindItem(object value, ColumnFilterMode columnFilterMode)
        {
            if (columnFilterMode == ColumnFilterMode.DisplayText)
            {
                ExcelColumnFilterValue value2 = this.FirstOrDefault<ExcelColumnFilterValue>(item => !(item is ExcelColumnFilterServiceValueBase) && (item.GetComputedValue() == value));
                if (value2 != null)
                {
                    return value2;
                }
            }
            return this.FirstOrDefault<ExcelColumnFilterValue>(item => Equals(item.GetComputedValue(), value));
        }

        public override int FilterValuesCount =>
            (base.Count - ((base.ServiceItem_SelectAll != null) ? 1 : 0)) - ((base.ServiceItem_AddFilter != null) ? 1 : 0);

        internal override bool AllItemsVisible =>
            base.VisibleItemsCount == this.FilterValuesCount;

        internal override bool ForceCalcVisibility =>
            true;

        public override bool IsAllItemsChecked =>
            base.CheckedItemsCount == this.FilterValuesCount;
    }
}

