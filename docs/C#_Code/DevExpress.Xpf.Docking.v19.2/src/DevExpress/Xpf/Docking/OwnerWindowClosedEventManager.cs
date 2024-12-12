namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    internal class OwnerWindowClosedEventManager : BaseWeakEventManager<OwnerWindowClosedEventManager>
    {
        public static void AddListener(Window w, IWeakEventListener listener)
        {
            GetManager().ProtectedAddListener(w, listener);
        }

        public static void RemoveListener(Window w, IWeakEventListener listener)
        {
            GetManager().ProtectedRemoveListener(w, listener);
        }

        protected override void StartListening(object source)
        {
            ((Window) source).Closed += new EventHandler(this.DeliverEvent);
        }

        protected override void StopListening(object source)
        {
            ((Window) source).Closed -= new EventHandler(this.DeliverEvent);
        }
    }
}

