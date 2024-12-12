namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    internal class BindingListFilterProxy : BindingListFilterProxyBase, IFilteredComponent, IFilteredComponentBase
    {
        public BindingListFilterProxy(IBindingList dataSource) : base(dataSource)
        {
        }

        IEnumerable<FilterColumn> IFilteredComponent.CreateFilterColumnCollection()
        {
            List<FilterColumn> list = new List<FilterColumn>();
            DataColumnInfo[] infoArray = new MasterDetailHelper().GetDataColumnInfo(null, base.DataSource, null);
            if (infoArray != null)
            {
                foreach (DataColumnInfo info in infoArray)
                {
                    list.Add(new DataColumnInfoFilterColumn(info));
                }
            }
            return list;
        }
    }
}

