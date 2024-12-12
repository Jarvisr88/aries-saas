namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DataControlFilteredComponent : IFilteredComponent, IFilteredComponentBase
    {
        private DataControlBase dataControl;
        private EventHandler propertiesChanged;
        private EventHandler rowFilterChanged;
        private bool useFilterClauseHelper = true;

        event EventHandler IFilteredComponentBase.PropertiesChanged
        {
            add
            {
                this.propertiesChanged += value;
            }
            remove
            {
                this.propertiesChanged -= value;
            }
        }

        event EventHandler IFilteredComponentBase.RowFilterChanged
        {
            add
            {
                this.rowFilterChanged += value;
            }
            remove
            {
                this.rowFilterChanged -= value;
            }
        }

        public DataControlFilteredComponent(DataControlBase dataControl)
        {
            this.dataControl = dataControl;
        }

        public IEnumerable<FilterColumn> CreateFilterColumnCollection()
        {
            List<FilterColumn> list = new List<FilterColumn>();
            foreach (ColumnBase base2 in this.dataControl.ColumnsCore)
            {
                if ((this.UseFilterClauseHelper ? base2.ActualAllowFilterEditor : base2.GetAllowConditionFormatingFilterEditor()) || (base2.IsFiltered || this.dataControl.DesignTimeAdorner.ForceAllowUseColumnInFilterControl))
                {
                    list.Add(this.dataControl.GetFilterColumnFromGridColumn(base2, true, this.UseFilterClauseHelper, false));
                }
            }
            return list;
        }

        internal void RaiseFilterColumnsChanged()
        {
            if (this.propertiesChanged != null)
            {
                this.propertiesChanged(this, new EventArgs());
            }
        }

        CriteriaOperator IFilteredComponentBase.RowCriteria { get; set; }

        internal bool UseFilterClauseHelper
        {
            get => 
                this.useFilterClauseHelper;
            set => 
                this.useFilterClauseHelper = value;
        }
    }
}

