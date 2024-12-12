namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public static class TreeViewBehavior
    {
        public static readonly DependencyProperty ExpandRootNodeProperty = DependencyPropertyManager.RegisterAttached("ExpandRootNode", typeof(bool?), typeof(TreeViewBehavior), new PropertyMetadata(new PropertyChangedCallback(TreeViewBehavior.OnExpandRootNodeChanged)));
        public static readonly DependencyProperty SubscribeProperty = DependencyPropertyManager.RegisterAttached("Subscribe", typeof(bool), typeof(TreeViewBehavior), new PropertyMetadata(false, new PropertyChangedCallback(TreeViewBehavior.OnSubscribeChanged)));
        public static readonly DependencyProperty SelectedItemProperty = DependencyPropertyManager.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewBehavior), new PropertyMetadata(null, new PropertyChangedCallback(TreeViewBehavior.OnSelectedItemChanged)));

        private static void ExpandRootNodes(TreeView treeView)
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

        private static void ExpandTreeViewItem(TreeViewItem item)
        {
            if (!item.IsExpanded)
            {
                item.IsExpanded = true;
                item.UpdateLayout();
            }
        }

        private static TreeViewItem FindTreeViewItem(TreeView treeView, object item)
        {
            INode node = item as INode;
            if ((node != null) && !string.IsNullOrEmpty(node.DataMember))
            {
                return FindTreeViewItemByNodeRecursively(treeView, node);
            }
            List<object> scannedItems = new List<object>();
            return FindTreeViewItemRecursive(treeView.ItemContainerGenerator, treeView.Items, item, scannedItems);
        }

        private static TreeViewItem FindTreeViewItemByNode(ItemContainerGenerator generator, IEnumerable<INode> nodes, string dataMember)
        {
            TreeViewItem item;
            using (IEnumerator<INode> enumerator = nodes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        INode current = enumerator.Current;
                        if (current.DataMember != dataMember)
                        {
                            continue;
                        }
                        item = (TreeViewItem) generator.ContainerFromItem(current);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item;
        }

        private static TreeViewItem FindTreeViewItemByNodeRecursively(TreeView treeView, INode node)
        {
            TreeViewItem item3;
            using (IEnumerator enumerator = ((IEnumerable) treeView.Items).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        INode current = (INode) enumerator.Current;
                        TreeViewItem item = (TreeViewItem) treeView.ItemContainerGenerator.ContainerFromItem(current);
                        ExpandTreeViewItem(item);
                        TreeViewItem item2 = FindTreeViewItemByNodeRecursivelyCore(item.ItemContainerGenerator, current.ChildNodes.Cast<INode>(), node);
                        if (item2 == null)
                        {
                            continue;
                        }
                        ExpandTreeViewItem(item);
                        item3 = item2;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item3;
        }

        private static TreeViewItem FindTreeViewItemByNodeRecursivelyCore(ItemContainerGenerator generator, IEnumerable<INode> nodes, INode node)
        {
            char[] separator = new char[] { '.' };
            string[] strArray = node.DataMember.Split(separator);
            TreeViewItem item = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                string dataMember = string.Join(".", strArray, 0, i + 1);
                item = FindTreeViewItemByNode(generator, nodes, dataMember);
                if (item == null)
                {
                    return null;
                }
                if (i < (strArray.Length - 1))
                {
                    ExpandTreeViewItem(item);
                    generator = item.ItemContainerGenerator;
                    nodes = item.Items.Cast<INode>();
                }
            }
            return item;
        }

        private static TreeViewItem FindTreeViewItemRecursive(ItemContainerGenerator generator, ItemCollection itemCollection, object item, List<object> scannedItems)
        {
            TreeViewItem item4;
            DependencyObject obj2 = generator.ContainerFromItem(item);
            if (obj2 != null)
            {
                return (TreeViewItem) obj2;
            }
            using (IEnumerator enumerator = ((IEnumerable) itemCollection).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if (scannedItems.Contains(current))
                        {
                            continue;
                        }
                        TreeViewItem item2 = (TreeViewItem) generator.ContainerFromItem(current);
                        if (item2 == null)
                        {
                            continue;
                        }
                        bool flag = !item2.IsExpanded;
                        if (flag)
                        {
                            ExpandTreeViewItem(item2);
                        }
                        scannedItems.Add(current);
                        TreeViewItem item3 = FindTreeViewItemRecursive(item2.ItemContainerGenerator, item2.Items, item, scannedItems);
                        if (item3 != null)
                        {
                            item4 = item3;
                            break;
                        }
                        if (flag)
                        {
                            item2.IsExpanded = false;
                        }
                        continue;
                    }
                    return null;
                }
            }
            return item4;
        }

        public static bool? GetExpandRootNode(DependencyObject obj) => 
            (bool?) obj.GetValue(ExpandRootNodeProperty);

        public static object GetSelectedItem(DependencyObject obj) => 
            obj.GetValue(SelectedItemProperty);

        public static bool GetSubscribe(DependencyObject obj) => 
            (bool) obj.GetValue(SelectedItemProperty);

        private static void OnExpandRootNodeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            TreeView view = sender as TreeView;
            if (view == null)
            {
                throw new NotSupportedException("The ExpandRootNode behavior can be attached to a TreeView class instance only.");
            }
            if ((bool) e.NewValue)
            {
                object[] args = new object[] { view };
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action<TreeView>(TreeViewBehavior.ExpandRootNodes), DispatcherPriority.Loaded, args);
            }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TreeView treeView = (TreeView) d;
                treeView.Dispatcher.BeginInvoke(delegate {
                    TreeViewItem item = FindTreeViewItem(treeView, e.NewValue);
                    if (item != null)
                    {
                        item.IsSelected = true;
                    }
                }, new object[0]);
            }
        }

        private static void OnSubscribeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeView view = (TreeView) d;
            if ((bool) e.NewValue)
            {
                view.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(TreeViewBehavior.treeView_SelectedItemChanged);
            }
            else
            {
                view.SelectedItemChanged -= new RoutedPropertyChangedEventHandler<object>(TreeViewBehavior.treeView_SelectedItemChanged);
            }
        }

        public static void SetExpandRootNode(DependencyObject obj, bool? value)
        {
            obj.SetValue(ExpandRootNodeProperty, value);
        }

        public static void SetSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedItemProperty, value);
        }

        public static void SetSubscribe(DependencyObject obj, bool value)
        {
            obj.SetValue(SubscribeProperty, value);
        }

        private static void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((TreeView) sender).SetValue(SelectedItemProperty, e.NewValue);
        }
    }
}

