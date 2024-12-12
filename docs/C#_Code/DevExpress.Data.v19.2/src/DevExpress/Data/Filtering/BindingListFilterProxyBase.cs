namespace DevExpress.Data.Filtering
{
    using System;
    using System.ComponentModel;

    public class BindingListFilterProxyBase : IFilteredComponentBase
    {
        private IBindingList dataSource;
        private EventHandler filterChanged;
        private EventHandler propertiesChanged;

        event EventHandler IFilteredComponentBase.PropertiesChanged;

        event EventHandler IFilteredComponentBase.RowFilterChanged;

        public BindingListFilterProxyBase(IBindingList dataSource);
        private void AfterRemoveEvent();
        private void BeforeAddEvent();
        private void DS_ListChanged(object sender, ListChangedEventArgs e);
        private void OnFilterChanged();
        private void OnPropertiesChanged();

        public IBindingList DataSource { get; }

        CriteriaOperator IFilteredComponentBase.RowCriteria { get; set; }
    }
}

