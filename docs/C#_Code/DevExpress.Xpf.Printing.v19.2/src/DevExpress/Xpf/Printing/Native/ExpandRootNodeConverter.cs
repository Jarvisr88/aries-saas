namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Threading;

    public class ExpandRootNodeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            TreeView view = parameter as TreeView;
            if (view != null)
            {
                object[] args = new object[] { view };
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action<TreeView>(this.ExpandRootNodes), DispatcherPriority.Loaded, args);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void ExpandRootNodes(TreeView treeView)
        {
            foreach (object obj2 in (IEnumerable) treeView.Items)
            {
                TreeViewItem item = treeView.ItemContainerGenerator.ContainerFromItem(obj2) as TreeViewItem;
                if (item != null)
                {
                    item.IsExpanded = true;
                }
            }
        }
    }
}

