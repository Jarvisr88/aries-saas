namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;

    public interface ISelectorEditInnerListBox
    {
        ICustomItem GetCustomItem(Func<object, bool> getNeedItem);
        bool IsCustomItem(ICustomItem item);
        void ScrollIntoView(object item);
        void SelectAll();
        void UnselectAll();

        Style ItemContainerStyle { get; }

        ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle { get; }

        DataTemplateSelector ItemTemplateSelector { get; set; }

        Locker SelectAllLocker { get; }

        ItemsPanelTemplate ItemsPanel { get; }

        ISelectorEdit OwnerEdit { get; }

        IEnumerable ItemsSource { get; set; }

        IList SelectedItems { get; }

        object SelectedItem { get; set; }

        int SelectedIndex { get; set; }

        bool? IsSelectAll { get; }

        System.Windows.Controls.ItemContainerGenerator ItemContainerGenerator { get; }

        DevExpress.Xpf.Editors.ScrollUnit ScrollUnit2 { get; }
    }
}

