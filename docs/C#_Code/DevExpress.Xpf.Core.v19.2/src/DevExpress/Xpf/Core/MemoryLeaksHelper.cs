namespace DevExpress.Xpf.Core
{
    using DevExpress;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class MemoryLeaksHelper
    {
        public static void CollectOptional(params WeakReference[] references)
        {
            GCTestHelper.CollectOptional(references);
        }

        public static List<WeakReference> CollectReferences(FrameworkElement rootElement)
        {
            List<WeakReference> referencesList = new List<WeakReference>();
            CollectReferences(rootElement, referencesList);
            return referencesList;
        }

        public static void CollectReferences(FrameworkElement rootElement, IList<WeakReference> referencesList)
        {
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(rootElement);
            while (enumerator.MoveNext())
            {
                referencesList.Add(new WeakReference(enumerator.Current));
            }
        }

        public static void EnsureCollected(IEnumerable<WeakReference> references)
        {
            GCTestHelper.EnsureCollected(references);
        }

        public static void EnsureCollected(params WeakReference[] references)
        {
            GCTestHelper.EnsureCollected(references);
        }

        public static void GarbageCollect()
        {
            DispatcherHelper.DoEvents(1);
            GC.Collect();
            GC.GetTotalMemory(true);
        }
    }
}

