namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class NotificationBatch : IDisposable
    {
        private object Source;
        private static Dictionary<object, int> lockHash = new Dictionary<object, int>();
        private static object syncObj = new object();

        public NotificationBatch(object source)
        {
            this.Source = source;
            Updating(this.Source);
        }

        public static void Action(object source, object target = null, DependencyProperty property = null)
        {
            NotificationEventArgs action = new NotificationEventArgs(Notification.Action);
            action.ActionTarget = target;
            action.Property = property;
            NotificationHelper.Notify(source, action);
        }

        public void Dispose()
        {
            Updated(this.Source);
        }

        public static void Updated(object source)
        {
            if (source != null)
            {
                object syncObj = NotificationBatch.syncObj;
                lock (syncObj)
                {
                    object obj3 = source;
                    int num = lockHash[obj3] - 1;
                    lockHash[obj3] = num;
                    if (num == 0)
                    {
                        NotificationHelper.Notify(source, new NotificationEventArgs(Notification.Updated));
                        lockHash.Remove(source);
                    }
                }
            }
        }

        public static void Updating(object source)
        {
            if (source != null)
            {
                object syncObj = NotificationBatch.syncObj;
                lock (syncObj)
                {
                    int num;
                    if (!lockHash.TryGetValue(source, out num))
                    {
                        lockHash.Add(source, 0);
                        NotificationHelper.Notify(source, new NotificationEventArgs(Notification.Updating));
                    }
                    object obj3 = source;
                    lockHash[obj3] += 1;
                }
            }
        }
    }
}

