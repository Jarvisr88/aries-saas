namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Collections.Generic;

    public interface IDialogContext
    {
        IModelItem CreateModelItem(object obj);
        IDialogContext Find(string name);
        string GetFilterOperatorCustomText(CriteriaOperator filterCriteria);
        IModelItem GetRootModelItem();

        IDataColumnInfo ColumnInfo { get; }

        DevExpress.Xpf.Editors.Filtering.FilterColumn FilterColumn { get; }

        IFilteredComponent FilteredComponent { get; }

        IFormatsOwner PredefinedFormatsOwner { get; }

        IModelProperty Conditions { get; }

        IConditionModelItemsBuilder Builder { get; }

        IEditingContext EditingContext { get; }

        bool IsDesignTime { get; }

        bool IsPivot { get; }

        bool AllowAnimations { get; }

        bool IsVirtualSource { get; }

        string ApplyToFieldNameCaption { get; }

        string ApplyToPivotRowCaption { get; }

        string ApplyToPivotColumnCaption { get; }

        IEnumerable<FieldNameWrapper> PivotSpecialFieldNames { get; }

        DevExpress.Xpf.Core.ConditionalFormatting.Native.DefaultAnimationSettings? DefaultAnimationSettings { get; }
    }
}

