namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class DockLayoutManagerBehavior
    {
        public static readonly DependencyProperty HideItemsOnClosingProperty = DependencyPropertyManager.RegisterAttached("HideItemsOnClosing", typeof(bool?), typeof(DockLayoutManagerBehavior), new PropertyMetadata(new PropertyChangedCallback(DockLayoutManagerBehavior.OnHideItemsOnClosingChanged)));
        private static string itemName = string.Empty;
        public static readonly DependencyProperty ForbidDockingWithItemsProperty = DependencyPropertyManager.RegisterAttached("ForbidDockingWithItems", typeof(string), typeof(DockLayoutManagerBehavior), new PropertyMetadata(new PropertyChangedCallback(DockLayoutManagerBehavior.OnForbidDockingWithItemsChanged)));

        public static string GetForbidDockingWithItems(DependencyObject obj) => 
            (string) obj.GetValue(ForbidDockingWithItemsProperty);

        public static bool? GetHideItemsOnClosing(DependencyObject obj) => 
            (bool?) obj.GetValue(HideItemsOnClosingProperty);

        private static void manager_DockItemClosing(object sender, ItemCancelEventArgs e)
        {
            e.Item.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }

        private static void manager_ShowingDockHints(object sender, ShowingDockHintsEventArgs e)
        {
            char[] separator = new char[] { ',' };
            List<string> list = new List<string>(itemName.Split(separator));
            if ((e.DraggingTarget != null) && list.Contains(e.DraggingTarget.Name))
            {
                e.Hide(DockGuide.Center);
            }
        }

        private static void OnForbidDockingWithItemsChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            DockLayoutManager manager = sender as DockLayoutManager;
            if (manager == null)
            {
                throw new NotSupportedException("It is impossible to attach ForbidDockingWithItems behavior to non-DockLayoutManager elements");
            }
            itemName = e.NewValue as string;
            if (string.IsNullOrEmpty(itemName))
            {
                throw new ArgumentException("Cannot assign an empty or null value to the string property");
            }
            if (e.OldValue == null)
            {
                manager.ShowingDockHints += new ShowingDockHintsEventHandler(DockLayoutManagerBehavior.manager_ShowingDockHints);
            }
            else
            {
                manager.ShowingDockHints -= new ShowingDockHintsEventHandler(DockLayoutManagerBehavior.manager_ShowingDockHints);
                manager.ShowingDockHints += new ShowingDockHintsEventHandler(DockLayoutManagerBehavior.manager_ShowingDockHints);
            }
        }

        private static void OnHideItemsOnClosingChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }
            DockLayoutManager manager = sender as DockLayoutManager;
            if (manager == null)
            {
                throw new NotSupportedException("It is impossible to attach HideItemsOnClosing behavior to non-DockLayoutManager elements");
            }
            bool? newValue = (bool?) e.NewValue;
            bool flag = true;
            if ((newValue.GetValueOrDefault() == flag) ? (newValue != null) : false)
            {
                manager.DockItemClosing += new DockItemCancelEventHandler(DockLayoutManagerBehavior.manager_DockItemClosing);
            }
            newValue = (bool?) e.NewValue;
            flag = false;
            if ((newValue.GetValueOrDefault() == flag) ? (newValue != null) : false)
            {
                manager.DockItemClosing -= new DockItemCancelEventHandler(DockLayoutManagerBehavior.manager_DockItemClosing);
            }
        }

        public static void SetForbidDockingWithItems(DependencyObject obj, string value)
        {
            obj.SetValue(ForbidDockingWithItemsProperty, value);
        }

        public static void SetHideItemsOnClosing(DependencyObject obj, bool? value)
        {
            obj.SetValue(HideItemsOnClosingProperty, value);
        }
    }
}

