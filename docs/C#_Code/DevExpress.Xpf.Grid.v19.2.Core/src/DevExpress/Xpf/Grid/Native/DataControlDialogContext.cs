namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DataControlDialogContext : IDialogContext
    {
        private ColumnBase column;
        private DataControlBase dataControl;
        private ITableView view;
        private IDesignTimeAdornerBase adorner;
        private DevExpress.Xpf.Editors.Filtering.FilterColumn filterColumn;
        private IDataColumnInfo columnInfoWrapper;
        private IConditionModelItemsBuilder builder;
        private bool allowAnimations;
        private IFilteredComponent filteredComponent;
        private DefaultAnimationSettings? settings;

        public DataControlDialogContext(ColumnBase column) : this(column, column.OwnerControl)
        {
        }

        public DataControlDialogContext(ColumnBase column, DataControlBase dataControl)
        {
            column = ((column == null) || (column.OwnerControl == null)) ? null : column;
            this.column = column;
            this.filterColumn = dataControl.GetFilterColumnFromGridColumn(column, true, false, false);
            this.dataControl = dataControl;
            this.view = (ITableView) dataControl.viewCore;
            if (this.view != null)
            {
                this.allowAnimations = this.view.AllowDataUpdateFormatConditionMenu;
            }
            this.IsVirtualSource = dataControl.DataProviderBase.IsVirtualSource;
            DataControlFilteredComponent component1 = new DataControlFilteredComponent(dataControl);
            component1.UseFilterClauseHelper = false;
            this.filteredComponent = component1;
            this.settings = DataUpdateAnimationProvider.CreateDefaultAnimationSettings(this.view);
            this.adorner = dataControl.DesignTimeAdorner;
            this.builder = new ConditionModelItemsBuilder(this.EditingContext);
            List<IDataColumnInfo> columns = this.GetColumns();
            if (column != null)
            {
                this.columnInfoWrapper = new DataColumnInfoWrapper(column, columns);
            }
            else
            {
                this.columnInfoWrapper = new EmptyDataColumnInfoWrapper(dataControl.DataProviderBase.DataController, columns);
            }
        }

        public IModelItem CreateModelItem(object obj) => 
            this.adorner.CreateModelItem(obj, this.adorner.DataControlModelItem);

        string IDialogContext.GetFilterOperatorCustomText(CriteriaOperator filterCriteria) => 
            this.view.ViewBase.GetFilterOperatorCustomText(filterCriteria);

        IModelItem IDialogContext.GetRootModelItem() => 
            this.adorner.DataControlModelItem;

        public IDialogContext Find(string name)
        {
            ColumnBase column = this.dataControl.ColumnsCore[name];
            return ((column == null) ? null : new DataControlDialogContext(column));
        }

        private List<IDataColumnInfo> GetColumns()
        {
            List<IDataColumnInfo> list = new List<IDataColumnInfo>();
            foreach (IDataColumnInfo info in this.dataControl.ColumnsCore)
            {
                list.Add(info);
            }
            return list;
        }

        private IModelProperty GetConditions() => 
            this.adorner.DataControlModelItem.Properties["View"].Value.Properties["FormatConditions"];

        internal bool IsVirtualSource { get; set; }

        public IDataColumnInfo ColumnInfo =>
            this.columnInfoWrapper;

        public IEditingContext EditingContext =>
            this.adorner.DataControlModelItem.Context;

        public DevExpress.Xpf.Editors.Filtering.FilterColumn FilterColumn =>
            this.filterColumn;

        public IFilteredComponent FilteredComponent =>
            this.filteredComponent;

        public IFormatsOwner PredefinedFormatsOwner =>
            this.view;

        public bool IsDesignTime =>
            this.adorner.IsDesignTime;

        public IConditionModelItemsBuilder Builder =>
            this.builder;

        public IModelProperty Conditions =>
            this.GetConditions();

        bool IDialogContext.IsPivot =>
            false;

        bool IDialogContext.AllowAnimations =>
            this.allowAnimations;

        bool IDialogContext.IsVirtualSource =>
            this.IsVirtualSource;

        string IDialogContext.ApplyToFieldNameCaption =>
            ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_Manager_AppliesTo);

        string IDialogContext.ApplyToPivotColumnCaption =>
            null;

        string IDialogContext.ApplyToPivotRowCaption =>
            null;

        IEnumerable<FieldNameWrapper> IDialogContext.PivotSpecialFieldNames =>
            null;

        DefaultAnimationSettings? IDialogContext.DefaultAnimationSettings =>
            this.settings;

        private class DataColumnInfoWrapper : IDataColumnInfo
        {
            private readonly IDataColumnInfo column;
            private List<IDataColumnInfo> columns;

            public DataColumnInfoWrapper(IDataColumnInfo column, List<IDataColumnInfo> columns)
            {
                this.column = column;
                this.columns = columns;
            }

            string IDataColumnInfo.Caption =>
                this.column.Caption;

            List<IDataColumnInfo> IDataColumnInfo.Columns =>
                this.columns;

            DataControllerBase IDataColumnInfo.Controller =>
                this.column.Controller;

            string IDataColumnInfo.FieldName =>
                this.column.FieldName;

            Type IDataColumnInfo.FieldType =>
                this.column.FieldType;

            string IDataColumnInfo.Name =>
                this.column.Name;

            string IDataColumnInfo.UnboundExpression =>
                this.column.UnboundExpression;
        }

        private class EmptyDataColumnInfoWrapper : IDataColumnInfo
        {
            private readonly List<IDataColumnInfo> columnsCore;
            private DataControllerBase dataControllerBase;

            public EmptyDataColumnInfoWrapper(DataControllerBase dataControllerBase, List<IDataColumnInfo> columns)
            {
                this.dataControllerBase = dataControllerBase;
                this.columnsCore = columns;
            }

            string IDataColumnInfo.Caption =>
                string.Empty;

            List<IDataColumnInfo> IDataColumnInfo.Columns =>
                this.columnsCore;

            DataControllerBase IDataColumnInfo.Controller =>
                this.dataControllerBase;

            string IDataColumnInfo.FieldName =>
                string.Empty;

            Type IDataColumnInfo.FieldType =>
                typeof(object);

            string IDataColumnInfo.Name =>
                string.Empty;

            string IDataColumnInfo.UnboundExpression =>
                string.Empty;
        }
    }
}

