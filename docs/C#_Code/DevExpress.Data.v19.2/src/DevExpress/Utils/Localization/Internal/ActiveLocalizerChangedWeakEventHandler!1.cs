namespace DevExpress.Utils.Localization.Internal
{
    using DevExpress.Utils;
    using DevExpress.Utils.Localization;
    using System;
    using System.Threading;

    internal sealed class ActiveLocalizerChangedWeakEventHandler<T> : WeakEventHandler<EventArgs, EventHandler> where T: struct
    {
        [ThreadStatic]
        private static ActiveLocalizerChangedWeakEventHandler<T> instanceCore;

        private ActiveLocalizerChangedWeakEventHandler()
        {
        }

        internal static void AddHandler(EventHandler value)
        {
            ActiveLocalizerChangedWeakEventHandler<T>.Instance.Purge();
            ActiveLocalizerChangedWeakEventHandler<T>.Instance.Add(value);
        }

        internal static void RaiseChanged(XtraLocalizer<T> localizer)
        {
            ActiveLocalizerChangedWeakEventHandler<T>.Instance.Raise(localizer, EventArgs.Empty);
        }

        internal static void RemoveHandler(EventHandler value)
        {
            ActiveLocalizerChangedWeakEventHandler<T>.Instance.Remove(value);
        }

        private static ActiveLocalizerChangedWeakEventHandler<T> Instance
        {
            get
            {
                if (ActiveLocalizerChangedWeakEventHandler<T>.instanceCore == null)
                {
                    Interlocked.CompareExchange<ActiveLocalizerChangedWeakEventHandler<T>>(ref ActiveLocalizerChangedWeakEventHandler<T>.instanceCore, new ActiveLocalizerChangedWeakEventHandler<T>(), null);
                }
                return ActiveLocalizerChangedWeakEventHandler<T>.instanceCore;
            }
        }
    }
}

