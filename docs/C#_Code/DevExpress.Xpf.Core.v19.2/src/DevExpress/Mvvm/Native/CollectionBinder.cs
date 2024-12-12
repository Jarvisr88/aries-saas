namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class CollectionBinder
    {
        public static IDisposable BindOneWay<TSource, TTarget>(IEnumerable<TSource> source, IList<TTarget> target, Func<TSource, TTarget> convert, bool resetTarget = true, bool weakSource = false)
        {
            if (resetTarget)
            {
                Reset<TSource, TTarget>(source, target, convert);
            }
            return BindOneWayCore(source, target, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync<TSource, TTarget>((IEnumerable<TSource>) from, (IList<TTarget>) to, convert, e);
            }, weakSource, false);
        }

        public static IDisposable BindOneWay(IEnumerable source, IList target, Func<object, object> convert, bool resetTarget = true, bool weakSource = false)
        {
            if (resetTarget)
            {
                Reset(source, target, convert);
            }
            return BindOneWayCore(source, target, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync((IEnumerable) from, (IList) to, convert, e);
            }, weakSource, false);
        }

        private static IDisposable BindOneWayCore(object source, object target, SyncDelegate fromSourceToTarget, bool weakSource, bool weakTarget) => 
            new CollectionBinderOneWay(source, target, fromSourceToTarget, weakSource, weakTarget);

        public static IDisposable BindTwoWay<TSource, TTarget>(IList<TSource> source, IList<TTarget> target, Func<TSource, TTarget> convert, Func<TTarget, TSource> convertBack, bool resetTarget = true, bool weakSource = false)
        {
            if (resetTarget)
            {
                Reset<TSource, TTarget>(source, target, convert);
            }
            return BindTwoWayCore(source, target, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync<TSource, TTarget>((IList<TSource>) from, (IList<TTarget>) to, convert, e);
            }, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync<TTarget, TSource>((IList<TTarget>) from, (IList<TSource>) to, convertBack, e);
            }, weakSource, false);
        }

        public static IDisposable BindTwoWay(IList source, IList target, Func<object, object> convert, Func<object, object> convertBack, bool resetTarget = true, bool weakSource = false)
        {
            if (resetTarget)
            {
                Reset(source, target, convert);
            }
            return BindTwoWayCore(source, target, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync((IList) from, (IList) to, convert, e);
            }, delegate (object from, object to, NotifyCollectionChangedEventArgs e) {
                IEnumerableToIListHelper.Sync((IList) from, (IList) to, convertBack, e);
            }, weakSource, false);
        }

        private static IDisposable BindTwoWayCore(object source, object target, SyncDelegate fromSourceToTarget, SyncDelegate fromTargetToSource, bool weakSource, bool weakTarget) => 
            new CollectionBinderTwoWay(source, target, fromSourceToTarget, fromTargetToSource, weakSource, weakTarget);

        private static void Reset<TSource, TTarget>(IEnumerable<TSource> source, IList<TTarget> target, Func<TSource, TTarget> convert)
        {
            IEnumerableToIListHelper.Reset<TSource, TTarget>(source, target, convert);
        }

        private static void Reset(IEnumerable source, IList target, Func<object, object> convert)
        {
            IEnumerableToIListHelper.Reset(source, target, convert);
        }

        public delegate void SyncDelegate(object from, object to, NotifyCollectionChangedEventArgs e);
    }
}

