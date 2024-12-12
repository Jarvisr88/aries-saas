namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal class VisualRootPreviewKeyUpEventManager : BaseWeakEventManager<VisualRootPreviewKeyUpEventManager>
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
            ((UIElement) source).PreviewKeyUp += new KeyEventHandler(this.DeliverEvent);
        }

        protected override void StopListening(object source)
        {
            ((UIElement) source).PreviewKeyUp -= new KeyEventHandler(this.DeliverEvent);
        }
    }
}

