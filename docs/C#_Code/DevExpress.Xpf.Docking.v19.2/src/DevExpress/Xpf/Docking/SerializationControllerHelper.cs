namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public static class SerializationControllerHelper
    {
        public static void ClearGroupsCollection<T>(ObservableCollection<T> collection) where T: LayoutGroup
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            collection.Accept<T>(group => group.Accept(new VisitDelegate<BaseLayoutItem>(items.Add)));
            collection.Clear();
            Decompose(items);
        }

        public static void ClearHiddenItemsCollection(HiddenItemsCollection collection)
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            collection.Accept<BaseLayoutItem>(item => item.Accept(new VisitDelegate<BaseLayoutItem>(items.Add)));
            collection.Clear();
            Decompose(items);
        }

        public static void ClearItemsCollection<T>(ObservableCollection<T> collection) where T: BaseLayoutItem
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            collection.Accept<T>(new VisitDelegate<T>(items.Add));
            collection.Clear();
            Decompose(items);
        }

        private static void ClearLayoutGroup(LayoutGroup group)
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            group.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
            Decompose(items);
        }

        public static void ClearLayoutRoot(DockLayoutManager container)
        {
            List<BaseLayoutItem> items = new List<BaseLayoutItem>();
            container.LayoutRoot.Accept(new VisitDelegate<BaseLayoutItem>(items.Add));
            Decompose(items);
        }

        public static T CreateCommand<T>(DockLayoutManager container, object path) where T: SerializationControllerCommand, new()
        {
            ISerializationController serializationController = GetSerializationController(container);
            if (serializationController != null)
            {
                return serializationController.CreateCommand<T>(path);
            }
            return default(T);
        }

        private static void Decompose(List<BaseLayoutItem> items)
        {
            foreach (BaseLayoutItem item in items)
            {
                if (item.Parent != null)
                {
                    item.Parent.Remove(item);
                }
                LayoutPanel panel = item as LayoutPanel;
                if ((panel != null) && (panel.Layout != null))
                {
                    ClearLayoutGroup(panel.Layout);
                }
            }
        }

        private static DockLayoutManager GetContainer(DependencyObject dObj)
        {
            DockLayoutManager manager = dObj as DockLayoutManager;
            if (manager == null)
            {
                if (dObj is BaseLayoutItem)
                {
                    manager = ((BaseLayoutItem) dObj).FindDockLayoutManager();
                }
                manager ??= ((dObj != null) ? DockLayoutManager.GetDockLayoutManager(dObj) : null);
            }
            return manager;
        }

        private static ISerializationController GetSerializationController(BaseLayoutItem item) => 
            (item != null) ? ((item.AttachedSerializationController != null) ? (item.AttachedSerializationController.Target as ISerializationController) : null) : null;

        private static ISerializationController GetSerializationController(DockLayoutManager container) => 
            container?.SerializationController;

        public static ISerializationController GetSerializationController(DependencyObject dObj)
        {
            ISerializationController serializationController = GetSerializationController(dObj as BaseLayoutItem);
            return ((serializationController == null) ? GetSerializationController(GetContainer(dObj)) : serializationController);
        }
    }
}

