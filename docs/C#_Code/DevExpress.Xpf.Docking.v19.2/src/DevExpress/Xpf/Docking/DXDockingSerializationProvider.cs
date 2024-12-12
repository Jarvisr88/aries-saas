namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Windows;

    public class DXDockingSerializationProvider : SerializationProvider
    {
        private static ISerializationController GetController(object obj) => 
            SerializationControllerHelper.GetSerializationController(obj as DependencyObject);

        protected override bool OnAllowProperty(AllowPropertyEventArgs e)
        {
            BaseLayoutItem source = e.Source as BaseLayoutItem;
            if (source != null)
            {
                if (e.IsSerializing)
                {
                    e.Allow = source.AllowSerializeProperty(e);
                }
                this.RaiseEvent(e, e.Source);
                return e.Allow;
            }
            if (!(e.Source is BaseLayoutItemSerializationInfo))
            {
                return base.OnAllowProperty(e);
            }
            this.RaiseEvent(e, ((BaseLayoutItemSerializationInfo) e.Source).Owner);
            return e.Allow;
        }

        protected override void OnClearCollection(XtraItemRoutedEventArgs e)
        {
            GetController(e.Source).OnClearCollection(e);
        }

        protected override object OnCreateCollectionItem(XtraCreateCollectionItemEventArgs e) => 
            GetController(e.Source).OnCreateCollectionItem(e);

        protected override void OnCustomGetSerializableChildren(DependencyObject dObj, CustomGetSerializableChildrenEventArgs e)
        {
            DockLayoutManager container = dObj as DockLayoutManager;
            if (container != null)
            {
                foreach (BaseLayoutItem item in container.GetItems())
                {
                    ILayoutContent content = item as ILayoutContent;
                    if (content != null)
                    {
                        UIElement element = content.Control ?? content.ContentPresenter;
                        if (element != null)
                        {
                            e.Children.Add(element);
                        }
                    }
                }
            }
        }

        protected override object OnFindCollectionItem(XtraFindCollectionItemEventArgs e) => 
            GetController(e.Source).OnFindCollectionItem(e);

        private void RaiseEvent(RoutedEventArgs e, object source)
        {
            ISerializationController controller = GetController(source);
            if (controller != null)
            {
                controller.Container.RaiseEvent(e);
            }
        }
    }
}

