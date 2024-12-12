namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Popups;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public interface ISelectorEdit : IBaseEdit, IInputElement
    {
        void BeginDataUpdate();
        void EndDataUpdate();
        object GetCurrentSelectedItem();
        IEnumerable GetCurrentSelectedItems();
        IEnumerable GetPopupContentCustomItemsSource();
        object GetPopupContentItemsSource();
        IEnumerable GetPopupContentMRUItemsSource();

        ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle { get; }

        EditStrategyBase EditStrategy { get; }

        ICommand SelectAllItemsCommand { get; }

        System.Windows.Controls.SelectionMode SelectionMode { get; }

        IItemsProvider2 ItemsProvider { get; }

        string DisplayMember { get; set; }

        string ValueMember { get; set; }

        DataTemplate ItemTemplate { get; }

        ListItemCollection Items { get; }

        ObservableCollection<object> SelectedItems { get; }

        object SelectedItem { get; set; }

        int SelectedIndex { get; set; }

        object ItemsSource { get; set; }

        bool SelectItemWithNullValue { get; }

        IListNotificationOwner ListNotificationOwner { get; }

        ISelectionProvider SelectionProvider { get; }

        bool AllowCollectionView { get; }

        bool UseCustomItems { get; }

        CriteriaOperator FilterCriteria { get; set; }

        bool IsSynchronizedWithCurrentItem { get; }

        DevExpress.Xpf.Editors.Popups.SelectionEventMode SelectionEventMode { get; }

        bool AllowRejectUnknownValues { get; }

        bool IncrementalSearch { get; }
    }
}

