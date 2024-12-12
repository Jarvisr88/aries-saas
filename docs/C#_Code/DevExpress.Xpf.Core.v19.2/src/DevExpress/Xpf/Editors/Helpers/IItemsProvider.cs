namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IItemsProvider : IItemsProviderCollectionViewSupport
    {
        event ItemsProviderChangedEventHandler ItemsProviderChanged;

        CriteriaOperator CreateDisplayFilterCriteria(string searchText, FilterCondition condition);
        void DoRefresh();
        int FindItemIndexByText(string text, bool isCaseSensitiveSearch, bool allowTextInputSuggestions, object handle, int startControllerIndex = -1);
        int GetControllerIndexByIndex(int selectedIndex, object handle);
        int GetCount(object handle);
        string GetDisplayPropertyName(object handle);
        object GetDisplayValueByEditValue(object editValue, object handle);
        object GetDisplayValueByIndex(int index, object handle);
        int GetIndexByControllerIndex(int newControllerIndex, object handle);
        int GetIndexByItem(object item, object handle);
        object GetItem(object value, object handle);
        object GetItemByControllerIndex(int controllerIndex, object handle);
        ServerModeDataControllerBase GetServerModeDataController();
        object GetValueByIndex(int index, object handle);
        object GetValueFromItem(object item, object handle);
        IEnumerable GetVisibleListSource(object handle);
        int IndexOfValue(object value, object handle);
        void ProcessCollectionChanged(NotifyItemsProviderChangedEventArgs e);
        void ProcessSelectionChanged(NotifyItemsProviderSelectionChangedEventArgs e);
        void RegisterSnapshot(object handle);
        void ReleaseSnapshot(object handle);
        void Reset();
        void ResetDisplayTextCache();
        void SetDisplayFilterCriteria(CriteriaOperator criteria, object handle);
        void UpdateDisplayMember();
        void UpdateFilterCriteria();
        void UpdateItemsSource();
        void UpdateValueMember();

        object CurrentDataViewHandle { get; }

        int Count { get; }

        IEnumerable<string> AvailableColumns { get; }

        CriteriaOperator ActualFilterCriteria { get; }

        bool IsAsyncServerMode { get; }

        bool IsInLookUpMode { get; }

        CriteriaOperator DisplayFilterCriteria { get; set; }

        IEnumerable VisibleListSource { get; }

        object this[int index] { get; }
    }
}

