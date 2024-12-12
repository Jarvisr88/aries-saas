namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class InstantFeedbackColumnFilterList : List<object>, IListServer, IList, ICollection, IEnumerable, IDXCloneable, ITypedList
    {
        private IItemsProvider2 itemsProvider;
        private DataViewBase view;
        private ColumnBase column;

        public event EventHandler<ServerModeExceptionThrownEventArgs> ExceptionThrown;

        public event EventHandler<ServerModeInconsistencyDetectedEventArgs> InconsistencyDetected;

        public InstantFeedbackColumnFilterList(IItemsProvider2 itemsProvider, DataViewBase view, ColumnBase column)
        {
            this.itemsProvider = itemsProvider;
            this.itemsProvider.ItemsProviderChanged += new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
            this.view = view;
            this.column = column;
        }

        public void Apply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
        }

        public void Destroy()
        {
            this.itemsProvider.ItemsProviderChanged -= new ItemsProviderChangedEventHandler(this.itemsProvider_ItemsProviderChanged);
            this.itemsProvider = null;
            this.view = null;
            this.column = null;
        }

        public object DXClone() => 
            this;

        public int FindIncremental(CriteriaOperator expression, string value, int startIndex, bool searchUp, bool ignoreStartRow, bool allowLoop) => 
            -1;

        public IList GetAllFilteredAndSortedRows()
        {
            throw new NotImplementedException();
        }

        public List<ListSourceGroupInfo> GetGroupInfo(ListSourceGroupInfo parentGroup)
        {
            throw new NotImplementedException();
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) => 
            TypeDescriptor.GetProperties(typeof(CustomComboBoxItem));

        public string GetListName(PropertyDescriptor[] listAccessors) => 
            string.Empty;

        public int GetRowIndexByKey(object key)
        {
            throw new NotImplementedException();
        }

        public object GetRowKey(int index) => 
            ((ICustomItem) base[index]).EditValue;

        public List<object> GetTotalSummary()
        {
            throw new NotImplementedException();
        }

        public object[] GetUniqueColumnValues(CriteriaOperator expression, int maxCount, CriteriaOperator filterExpression, bool includeFilteredOut)
        {
            throw new NotImplementedException();
        }

        private void itemsProvider_ItemsProviderChanged(object sender, ItemsProviderChangedEventArgs e)
        {
            ItemsProviderRowLoadedEventArgs args = e as ItemsProviderRowLoadedEventArgs;
            if (args != null)
            {
                using (List<object>.Enumerator enumerator = base.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        CustomComboBoxItem current = (CustomComboBoxItem) enumerator.Current;
                        if (Equals(current.EditValue, args.Value))
                        {
                            int? rowHandle = null;
                            current.DisplayValue = this.view.GetColumnDisplayText(current.EditValue, this.column, rowHandle);
                            return;
                        }
                    }
                }
            }
            ItemsProviderOnBusyChangedEventArgs args2 = e as ItemsProviderOnBusyChangedEventArgs;
            if ((args2 != null) && !args2.IsBusy)
            {
                this.InconsistencyDetected(this, new ServerModeInconsistencyDetectedEventArgs());
            }
        }

        public int LocateByExpression(CriteriaOperator expression, int startIndex, bool searchUp)
        {
            throw new NotImplementedException();
        }

        public int LocateByValue(CriteriaOperator expression, object value, int startIndex, bool searchUp)
        {
            for (int i = 0; i < base.Count; i++)
            {
                CustomComboBoxItem item = base[i] as CustomComboBoxItem;
                if ((item != null) && Equals(item.EditValue, value))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool PrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
        }

        public object this[int index]
        {
            get
            {
                CustomComboBoxItem item = (CustomComboBoxItem) base[index];
                if ((this.column != null) && ((this.column.ColumnFilterMode == ColumnFilterMode.Value) && !(item.EditValue is CustomComboBoxItem)))
                {
                    int? rowHandle = null;
                    item.DisplayValue = this.view.GetColumnDisplayText(item.EditValue, this.column, rowHandle);
                }
                return item;
            }
            set => 
                base[index] = value;
        }
    }
}

