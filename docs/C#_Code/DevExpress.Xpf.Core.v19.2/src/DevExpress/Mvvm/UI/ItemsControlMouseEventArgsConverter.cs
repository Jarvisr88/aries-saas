namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class ItemsControlMouseEventArgsConverter : EventArgsConverterBase<MouseEventArgs>
    {
        protected static Dictionary<Type, Type> itemsTypes;
        public static readonly DependencyProperty ItemTypeProperty;

        static ItemsControlMouseEventArgsConverter()
        {
            Dictionary<Type, Type> dictionary = new Dictionary<Type, Type> {
                { 
                    typeof(ListBox),
                    typeof(ListBoxItem)
                },
                { 
                    typeof(ListView),
                    typeof(ListViewItem)
                },
                { 
                    typeof(TreeView),
                    typeof(TreeViewItem)
                },
                { 
                    typeof(TreeViewItem),
                    typeof(TreeViewItem)
                },
                { 
                    typeof(TabControl),
                    typeof(TabItem)
                },
                { 
                    typeof(StatusBar),
                    typeof(StatusBarItem)
                },
                { 
                    typeof(Menu),
                    typeof(MenuItem)
                }
            };
            itemsTypes = dictionary;
            ItemTypeProperty = DependencyProperty.Register("ItemType", typeof(Type), typeof(ItemsControlMouseEventArgsConverter), new PropertyMetadata(null));
        }

        protected override object Convert(object sender, MouseEventArgs args) => 
            this.ConvertCore(sender, (DependencyObject) args.OriginalSource);

        protected object ConvertCore(object sender, DependencyObject originalSource)
        {
            FrameworkElement element = this.FindParent(sender, originalSource);
            return element?.DataContext;
        }

        protected virtual FrameworkElement FindParent(object sender, DependencyObject originalSource) => 
            (from d in LayoutTreeHelper.GetVisualParents(originalSource, (DependencyObject) sender)
                where d.GetType() == this.GetItemType(sender)
                select d).FirstOrDefault<DependencyObject>() as FrameworkElement;

        public static object GetDataRow(ItemsControl sender, MouseEventArgs args) => 
            new ItemsControlMouseEventArgsConverter().Convert(sender, args);

        protected virtual Type GetItemType(object sender)
        {
            Type itemType = this.ItemType;
            if ((itemType == typeof(FrameworkElement)) || ((this.ItemType != null) && itemType.IsSubclassOf(typeof(FrameworkElement))))
            {
                return itemType;
            }
            Type type2 = null;
            itemsTypes.TryGetValue(sender.GetType(), out type2);
            return type2;
        }

        public Type ItemType
        {
            get => 
                (Type) base.GetValue(ItemTypeProperty);
            set => 
                base.SetValue(ItemTypeProperty, value);
        }
    }
}

