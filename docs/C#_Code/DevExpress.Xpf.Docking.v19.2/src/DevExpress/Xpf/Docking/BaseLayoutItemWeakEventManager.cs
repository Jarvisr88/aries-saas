namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    internal class BaseLayoutItemWeakEventManager : BaseWeakEventManager<BaseLayoutItemWeakEventManager>
    {
        public static void AddListener(UIElement root, IWeakEventListener listener)
        {
            GetManager().ProtectedAddListener(root, listener);
        }

        public static void RemoveListener(UIElement root, IWeakEventListener listener)
        {
            GetManager().ProtectedRemoveListener(root, listener);
        }

        protected override void StartListening(object source)
        {
            BaseLayoutItem item = source as BaseLayoutItem;
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.DeliverEvent);
            }
        }

        protected override void StopListening(object source)
        {
            BaseLayoutItem item = source as BaseLayoutItem;
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.DeliverEvent);
            }
        }
    }
}

