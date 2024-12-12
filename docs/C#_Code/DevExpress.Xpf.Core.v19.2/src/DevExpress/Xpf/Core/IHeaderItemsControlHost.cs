namespace DevExpress.Xpf.Core
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;

    public interface IHeaderItemsControlHost
    {
        Style HeaderItemContainerStyle { get; }

        StyleSelector HeaderItemContainerStyleSelector { get; }

        ObservableCollection<object> HeaderItems { get; }

        IEnumerable HeaderItemsSource { get; }

        DataTemplate HeaderItemTemplate { get; }

        DataTemplateSelector HeaderItemTemplateSelector { get; }
    }
}

