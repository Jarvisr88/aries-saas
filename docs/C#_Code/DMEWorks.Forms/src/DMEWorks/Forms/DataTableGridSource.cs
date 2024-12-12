namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    public class DataTableGridSource : DataView, IGridSource, IBindingList, IList, ICollection, IEnumerable
    {
        internal DataTableGridSource(DataTable table) : base(table)
        {
        }

        void IGridSource.ApplyFilterText(string text)
        {
            base.RowFilter = DropDownSupport.BuildFilter(base.Table, text, DropDownSupport.StringFilterType.Contains);
        }

        public override string RowFilter
        {
            get => 
                base.RowFilter;
            set
            {
            }
        }
    }
}

