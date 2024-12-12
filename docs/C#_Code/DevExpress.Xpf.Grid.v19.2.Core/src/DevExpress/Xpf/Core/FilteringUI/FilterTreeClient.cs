namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;

    internal class FilterTreeClient
    {
        public readonly string PropertyName;
        public readonly Func<string, FilteringColumn> GetColumn;
        public readonly Lazy<BaseEditSettings> RootEditSettingsLazy;
        public readonly Func<string, BlankOperations> GetBlankOperations;
        public readonly Action ApplyFilter;
        public readonly Action BeginUpdate;
        public readonly Action EndUpdate;
        public readonly Func<NodeBase<NodeValueInfo>, bool> GetIsNodeVisible;
        public readonly DevExpress.Xpf.Core.FilteringUI.FilterRestrictions FilterRestrictions;
        public readonly string[] GroupFields;
        public readonly Func<string, CriteriaOperator, string, Task<IList<FilterValueInfo>>> GetGroupFilterValues;

        public FilterTreeClient(string propertyName, Func<string, FilteringColumn> getColumn, Lazy<BaseEditSettings> rootEditSettingsLazy, Func<string, BlankOperations> getBlankOperations, Action applyFilter, Action beginUpdate, Action endUpdate, Func<NodeBase<NodeValueInfo>, bool> getIsNodeVisible, DevExpress.Xpf.Core.FilteringUI.FilterRestrictions filterRestrictions, string[] groupFields, Func<string, CriteriaOperator, string, Task<IList<FilterValueInfo>>> getGroupFilterValues)
        {
            Guard.ArgumentNotNull(propertyName, "propertyName");
            this.PropertyName = propertyName;
            this.GetColumn = getColumn;
            this.RootEditSettingsLazy = rootEditSettingsLazy;
            this.GetBlankOperations = getBlankOperations;
            this.ApplyFilter = applyFilter;
            this.BeginUpdate = beginUpdate;
            this.EndUpdate = endUpdate;
            this.GetIsNodeVisible = getIsNodeVisible;
            this.FilterRestrictions = filterRestrictions;
            this.GroupFields = groupFields;
            this.GetGroupFilterValues = getGroupFilterValues;
        }
    }
}

