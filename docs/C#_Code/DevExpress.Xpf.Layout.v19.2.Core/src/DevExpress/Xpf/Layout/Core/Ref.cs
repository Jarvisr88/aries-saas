namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;

    public static class Ref
    {
        public static void Clear<T>(ref ICollection<T> refToClear)
        {
            ClearCore<T>(refToClear);
            DisposeCore(refToClear as IDisposable);
            refToClear = null;
        }

        public static void Clear<TKey, TValue>(ref IDictionary<TKey, TValue> refToClear)
        {
            ClearCore<KeyValuePair<TKey, TValue>>(refToClear);
            DisposeCore(refToClear as IDisposable);
            refToClear = null;
        }

        public static void Clear<T>(ref IList<T> refToClear)
        {
            ClearCore<T>(refToClear);
            DisposeCore(refToClear as IDisposable);
            refToClear = null;
        }

        private static void ClearCore<T>(ICollection<T> refToClear)
        {
            if (refToClear != null)
            {
                refToClear.Clear();
            }
        }

        public static void Dispose<T>(ref T refToDispose) where T: class, IDisposable
        {
            DisposeCore((T) refToDispose);
            refToDispose = default(T);
        }

        private static void DisposeCore(IDisposable refToDispose)
        {
            if (refToDispose != null)
            {
                refToDispose.Dispose();
            }
        }
    }
}

