namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public interface ISelectorBase
    {
        event NotifyCollectionChangedEventHandler ItemsChanged;

        event EventHandler SelectionChanged;

        ContentControl GetContainer(int index);
        ContentControl GetContainer(object item);

        int SelectedIndex { get; set; }

        object SelectedItem { get; set; }
    }
}

