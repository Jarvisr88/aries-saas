namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ColumnFilteringUIDataContext : ImmutableObject
    {
        internal ColumnFilteringUIDataContext(FilteringUIContext context, string propertyName, DataTemplate template, bool applyFilterImmediately, bool includeFilteredOut, string clearFilterButtonCaption, ICommand clearFilterCommand, Func<UniqueValues, UniqueValues> getUniqueValuesForValuesTab)
        {
            this.<Context>k__BackingField = context;
            this.<PropertyName>k__BackingField = propertyName;
            this.<Template>k__BackingField = template;
            this.<ApplyFilterImmediately>k__BackingField = applyFilterImmediately;
            this.<IncludeFilteredOut>k__BackingField = includeFilteredOut;
            this.<ClearFilterButtonCaption>k__BackingField = clearFilterButtonCaption;
            this.<ClearFilterCommand>k__BackingField = clearFilterCommand;
            this.<GetUniqueValuesForValuesTab>k__BackingField = getUniqueValuesForValuesTab;
        }

        public FilteringUIContext Context { get; }

        public string PropertyName { get; }

        public DataTemplate Template { get; }

        public bool ApplyFilterImmediately { get; }

        public bool IncludeFilteredOut { get; }

        public string ClearFilterButtonCaption { get; }

        public ICommand ClearFilterCommand { get; }

        internal Func<UniqueValues, UniqueValues> GetUniqueValuesForValuesTab { get; }
    }
}

