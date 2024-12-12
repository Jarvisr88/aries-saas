namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public sealed class MultiFilterModelItem : BindableBase
    {
        internal MultiFilterModelItem(string displayName, OperandRestoreAdapterFactory factory, ImageSource icon, FormatConditionBase formatCondition, Func<FilterModelBase, DataTemplate> selectTemplate)
        {
            Guard.ArgumentNotNull(factory, "factory");
            this.<DisplayName>k__BackingField = displayName;
            this.<Factory>k__BackingField = factory;
            this.<Icon>k__BackingField = icon;
            this.<FormatCondition>k__BackingField = formatCondition;
            this.<SelectTemplate>k__BackingField = selectTemplate;
        }

        public string DisplayName { get; }

        public ImageSource Icon { get; }

        internal OperandRestoreAdapterFactory Factory { get; }

        public FormatConditionBase FormatCondition { get; }

        internal Func<FilterModelBase, DataTemplate> SelectTemplate { get; }
    }
}

