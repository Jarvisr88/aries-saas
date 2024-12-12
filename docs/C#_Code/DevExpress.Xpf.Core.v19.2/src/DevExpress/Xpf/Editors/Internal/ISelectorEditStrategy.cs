namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows;

    public interface ISelectorEditStrategy : IEditStrategy
    {
        void BringToView();
        object GetCurrentDataViewHandle();
        object GetCurrentEditableValue();
        IEnumerable GetInnerEditorCustomItemsSource();
        object GetInnerEditorItemsSource();
        IEnumerable GetInnerEditorMRUItemsSource();
        object GetNextValueFromSearchText(int startIndex);
        object GetPrevValueFromSearchText(int startIndex);
        object GetSelectedItems(object editValue);
        bool IsNullValue(object value);
        bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource);
        void SyncWithValue(DependencyProperty dp, object oldValue, object newValue);

        ISelectorEdit Editor { get; }

        RoutedEvent SelectedIndexChangedEvent { get; }

        IItemsProvider2 ItemsProvider { get; }

        bool IsSingleSelection { get; }

        bool IsTokenMode { get; }

        object CurrentDataViewHandle { get; }

        object TokenDataViewHandle { get; }

        int EditableTokenIndex { get; }

        bool IsInProcessNewValue { get; }

        bool IsInLookUpMode { get; }

        object EditValue { get; set; }

        string SearchText { get; }

        DevExpress.Xpf.Editors.Native.TextSearchEngine TextSearchEngine { get; }
    }
}

