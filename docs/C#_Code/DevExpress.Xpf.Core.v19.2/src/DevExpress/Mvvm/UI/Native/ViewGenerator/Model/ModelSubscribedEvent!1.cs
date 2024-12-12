namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ModelSubscribedEvent<TEventHandler> : IModelSubscribedEvent
    {
        private readonly TEventHandler handler;
        public static IModelSubscribedEvent Subscribe(TEventHandler handler, Action<TEventHandler> subscribeAction)
        {
            ModelSubscribedEvent<TEventHandler> event2 = new ModelSubscribedEvent<TEventHandler>(handler);
            subscribeAction(event2.handler);
            return event2;
        }

        public static void Unsubscribe(IModelSubscribedEvent e, Action<TEventHandler> unsubscribeAction)
        {
            if (e != null)
            {
                ModelSubscribedEvent<TEventHandler> event2 = (ModelSubscribedEvent<TEventHandler>) e;
                unsubscribeAction(event2.handler);
            }
        }

        private ModelSubscribedEvent(TEventHandler handler)
        {
            this.handler = handler;
        }
    }
}

