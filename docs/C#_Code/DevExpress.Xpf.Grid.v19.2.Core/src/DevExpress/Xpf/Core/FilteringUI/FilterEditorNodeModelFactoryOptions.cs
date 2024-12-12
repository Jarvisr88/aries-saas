namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class FilterEditorNodeModelFactoryOptions
    {
        public FilterEditorNodeModelFactoryOptions(FilteringUIContext context, string propertyName, bool? showCounts, DevExpress.Xpf.Core.FilteringUI.PropertySelectorMode propertySelectorMode, Func<IList<FieldItem>, CriteriaOperator, NodeClientColumnsInfo> getColumns, Func<CriteriaOperator, IReadOnlyCollection<string>> getParameters, Func<CriteriaOperator, AllowedOperandTypes> getAllowedOperandTypes, Func<Lazy<CriteriaOperator>, string, bool> canRemove, Func<Lazy<CriteriaOperator>, AllowedGroupFilters> getAllowedGroupFilters, Func<Lazy<CriteriaOperator>, GroupNodeModelChildMenuOptions> getChildMenuOptions, DevExpress.Xpf.Core.FilteringUI.SubstituteOperatorMenuItems substituteOperatorMenuItems, Func<FilterModelBase, DataTemplate> selectTemplate)
        {
            this.<Context>k__BackingField = context;
            this.<PropertyName>k__BackingField = propertyName;
            this.<ShowCounts>k__BackingField = showCounts;
            this.<PropertySelectorMode>k__BackingField = propertySelectorMode;
            this.<GetColumns>k__BackingField = getColumns;
            this.<GetParameters>k__BackingField = getParameters;
            this.<GetAllowedOperandTypes>k__BackingField = getAllowedOperandTypes;
            this.<CanRemove>k__BackingField = canRemove;
            this.<GetAllowedGroupFilters>k__BackingField = getAllowedGroupFilters;
            this.<GetChildMenuOptions>k__BackingField = getChildMenuOptions;
            this.<SubstituteOperatorMenuItems>k__BackingField = substituteOperatorMenuItems;
            this.<SelectTemplate>k__BackingField = selectTemplate;
        }

        public FilteringUIContext Context { get; }

        public string PropertyName { get; }

        public bool? ShowCounts { get; }

        public DevExpress.Xpf.Core.FilteringUI.PropertySelectorMode PropertySelectorMode { get; }

        public Func<IList<FieldItem>, CriteriaOperator, NodeClientColumnsInfo> GetColumns { get; }

        public Func<CriteriaOperator, IReadOnlyCollection<string>> GetParameters { get; }

        public Func<CriteriaOperator, AllowedOperandTypes> GetAllowedOperandTypes { get; }

        public Func<Lazy<CriteriaOperator>, string, bool> CanRemove { get; }

        public Func<Lazy<CriteriaOperator>, AllowedGroupFilters> GetAllowedGroupFilters { get; }

        public Func<Lazy<CriteriaOperator>, GroupNodeModelChildMenuOptions> GetChildMenuOptions { get; }

        public DevExpress.Xpf.Core.FilteringUI.SubstituteOperatorMenuItems SubstituteOperatorMenuItems { get; }

        public Func<FilterModelBase, DataTemplate> SelectTemplate { get; }
    }
}

