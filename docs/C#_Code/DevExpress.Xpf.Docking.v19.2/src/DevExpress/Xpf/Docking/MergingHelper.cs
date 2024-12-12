namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.Generic;

    internal static class MergingHelper
    {
        private static readonly object syncObj = new object();
        [ThreadStatic]
        private static IList<WeakReference> clients;

        public static void AddMergingClient(IMergingClient client)
        {
            object syncObj = MergingHelper.syncObj;
            lock (syncObj)
            {
                if (Array.IndexOf<object>(Purge(), client) == -1)
                {
                    int count = Clients.Count;
                    Clients.Add(new WeakReference(client));
                }
            }
        }

        public static void DoMerging()
        {
            object syncObj = MergingHelper.syncObj;
            lock (syncObj)
            {
                object[] objArray = Purge();
                for (int i = 0; i < objArray.Length; i++)
                {
                    IMergingClient client = objArray[i] as IMergingClient;
                    if (client != null)
                    {
                        client.Merge();
                        RemoveClient(client);
                    }
                }
            }
        }

        private static object[] Purge()
        {
            List<object> list = new List<object>();
            WeakReference[] array = new WeakReference[Clients.Count];
            Clients.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                WeakReference item = array[i];
                object target = item.Target;
                if (target == null)
                {
                    Clients.Remove(item);
                }
                else
                {
                    list.Add(target);
                }
            }
            return list.ToArray();
        }

        public static void RemoveClient(IMergingClient client)
        {
            object syncObj = MergingHelper.syncObj;
            lock (syncObj)
            {
                WeakReference[] array = new WeakReference[Clients.Count];
                Clients.CopyTo(array, 0);
                for (int i = 0; i < array.Length; i++)
                {
                    WeakReference item = array[i];
                    object target = item.Target;
                    if ((target == null) || (target == client))
                    {
                        Clients.Remove(item);
                    }
                }
            }
        }

        private static IList<WeakReference> Clients =>
            clients ??= new List<WeakReference>();
    }
}

