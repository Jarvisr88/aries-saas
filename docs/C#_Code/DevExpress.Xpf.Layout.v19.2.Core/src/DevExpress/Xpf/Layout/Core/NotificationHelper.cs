namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;

    public static class NotificationHelper
    {
        internal static Dictionary<object, Tracker<NotificationEventArgs>> trackers = new Dictionary<object, Tracker<NotificationEventArgs>>();

        private static void CollectUnusedTrackers(IDictionary<object, Tracker<NotificationEventArgs>> trackers)
        {
            ICollection<object> is2 = new List<object>();
            foreach (KeyValuePair<object, Tracker<NotificationEventArgs>> pair in trackers)
            {
                if (!pair.Value.InUse)
                {
                    is2.Add(pair.Key);
                }
            }
            foreach (object obj2 in is2)
            {
                trackers.Remove(obj2);
            }
        }

        public static void EndNotification(object source)
        {
            Dictionary<object, Tracker<NotificationEventArgs>> trackers = NotificationHelper.trackers;
            lock (trackers)
            {
                Tracker<NotificationEventArgs> tracker;
                if (NotificationHelper.trackers.TryGetValue(source, out tracker))
                {
                    tracker.EndNotification();
                }
                NotificationHelper.trackers.Remove(source);
            }
        }

        public static void Notify(object source, NotificationEventArgs action)
        {
            Dictionary<object, Tracker<NotificationEventArgs>> trackers = NotificationHelper.trackers;
            lock (trackers)
            {
                Tracker<NotificationEventArgs> tracker;
                if (NotificationHelper.trackers.TryGetValue(source, out tracker))
                {
                    if (tracker.InUse)
                    {
                        tracker.Notify(action);
                    }
                    else
                    {
                        NotificationHelper.trackers.Remove(source);
                    }
                }
            }
        }

        public static IDisposable Subscribe(object source, IObserver<NotificationEventArgs> observer)
        {
            if ((observer == null) || (source == null))
            {
                return null;
            }
            Dictionary<object, Tracker<NotificationEventArgs>> trackers = NotificationHelper.trackers;
            lock (trackers)
            {
                Tracker<NotificationEventArgs> tracker;
                if (!NotificationHelper.trackers.TryGetValue(source, out tracker))
                {
                    CollectUnusedTrackers(NotificationHelper.trackers);
                    tracker = new Tracker<NotificationEventArgs>();
                    NotificationHelper.trackers.Add(source, tracker);
                }
                return tracker.Subscribe(observer);
            }
        }
    }
}

