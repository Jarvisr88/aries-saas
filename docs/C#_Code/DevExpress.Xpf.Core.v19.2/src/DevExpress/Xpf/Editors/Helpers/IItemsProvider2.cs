namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IItemsProvider2 : IItemsProviderCollectionViewSupport
    {
        event ItemsProviderChangedEventHandler ItemsProviderChanged;

        void BeginInit();
        void CancelAsyncOperations(object handle);
        CriteriaOperator CreateDisplayFilterCriteria(string searchText, FilterCondition condition);
        void DestroyVisibleListSource(object handle);
        void DoRefresh();
        void EndInit();
        int FindItemIndexByText(string text, bool isCaseSensitiveSearch, bool allowTextInputSuggestions, object handle, int startIndex = -1, bool searchNext = true, bool ignoreStartIndex = false);
        int GetControllerIndexByIndex(int selectedIndex, object handle);
        int GetCount(object handle);
        string GetDisplayPropertyName(object handle);
        object GetDisplayValueByEditValue(object editValue, object handle);
        object GetDisplayValueByIndex(int index, object handle);
        int GetIndexByControllerIndex(int newControllerIndex, object handle);
        int GetIndexByItem(object item, object handle);
        object GetItem(object value, object handle);
        object GetItemByControllerIndex(int controllerIndex, object handle);
        object GetValueByIndex(int index, object handle);
        object GetValueByRowKey(object rowKey, object handle);
        object GetValueFromItem(object item, object handle);
        string GetValuePropertyName(object handle);
        IEnumerable GetVisibleListSource(object handle);
        int IndexOfValue(object value, object handle);
        void ProcessCollectionChanged(NotifyItemsProviderChangedEventArgs e);
        void ProcessSelectionChanged(NotifyItemsProviderSelectionChangedEventArgs e);
        void RegisterSnapshot(object handle);
        void ReleaseSnapshot(object handle);
        void Reset();
        void ResetDisplayTextCache();
        void ResetVisibleList(object handle);
        void SetDisplayFilterCriteria(CriteriaOperator criteria, object handle);
        void SetFilterCriteria(CriteriaOperator criteria, object handle);
        void UpdateDisplayMember();
        void UpdateFilterCriteria();
        void UpdateIsCaseSensitiveFilter();
        void UpdateItemsSource();
        void UpdateValueMember();

        object CurrentDataViewHandle { get; }

        IEnumerable<string> AvailableColumns { get; }

        CriteriaOperator ActualFilterCriteria { get; }

        bool IsAsyncServerMode { get; }

        bool IsBusy { get; }

        bool IsSyncServerMode { get; }

        bool IsServerMode { get; }

        bool IsInLookUpMode { get; }

        bool HasValueMember { get; }

        IEnumerable VisibleListSource { get; }

        object this[int index] { get; }

        bool NeedsRefresh { get; }
    }
}

