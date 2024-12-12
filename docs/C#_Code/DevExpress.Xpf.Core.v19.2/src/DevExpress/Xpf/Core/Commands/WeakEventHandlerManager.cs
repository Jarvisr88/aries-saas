namespace DevExpress.Xpf.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Threading;

    [Obsolete]
    internal static class WeakEventHandlerManager
    {
        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
        {
            handlers ??= ((defaultListSize > 0) ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            handlers.Add(new WeakReference(handler));
        }

        private static void CallHandler(object sender, EventHandler eventHandler)
        {
            DispatcherProxy proxy = CreateDispatcherProxy(sender);
            if (eventHandler != null)
            {
                if ((proxy != null) && !proxy.CheckAccess())
                {
                    object[] args = new object[] { sender, eventHandler };
                    proxy.BeginInvoke(new Action<object, EventHandler>(WeakEventHandlerManager.CallHandler), args);
                }
                else
                {
                    eventHandler(sender, EventArgs.Empty);
                }
            }
        }

        public static void CallWeakReferenceHandlers(object sender, List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                EventHandler[] callees = new EventHandler[handlers.Count];
                int num = CleanupOldHandlers(handlers, callees, 0);
                for (int i = 0; i < num; i++)
                {
                    CallHandler(sender, callees[i]);
                }
            }
        }

        private static int CleanupOldHandlers(List<WeakReference> handlers, EventHandler[] callees, int count)
        {
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                WeakReference reference = handlers[i];
                EventHandler target = reference.Target as EventHandler;
                if (target == null)
                {
                    handlers.RemoveAt(i);
                }
                else
                {
                    callees[count] = target;
                    count++;
                }
            }
            return count;
        }

        private static DispatcherProxy CreateDispatcherProxy(object obj)
        {
            IDispatcherInfo info = obj as IDispatcherInfo;
            return ((info == null) ? DispatcherProxy.CreateDispatcher() : DispatcherProxy.CreateDispatcher(info.Dispatcher));
        }

        public static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers != null)
            {
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler target = reference.Target as EventHandler;
                    if ((target == null) || (target == handler))
                    {
                        handlers.RemoveAt(i);
                    }
                }
            }
        }

        private class DispatcherProxy
        {
            private Dispatcher innerDispatcher;

            private DispatcherProxy(Dispatcher dispatcher)
            {
                this.innerDispatcher = dispatcher;
            }

            public DispatcherOperation BeginInvoke(Delegate method, params object[] args) => 
                this.innerDispatcher.BeginInvoke(method, DispatcherPriority.Normal, args);

            public bool CheckAccess() => 
                this.innerDispatcher.CheckAccess();

            public static WeakEventHandlerManager.DispatcherProxy CreateDispatcher() => 
                CreateDispatcher(Dispatcher.FromThread(Thread.CurrentThread));

            public static WeakEventHandlerManager.DispatcherProxy CreateDispatcher(Dispatcher innerDispatcher) => 
                (innerDispatcher != null) ? new WeakEventHandlerManager.DispatcherProxy(innerDispatcher) : null;
        }
    }
}

