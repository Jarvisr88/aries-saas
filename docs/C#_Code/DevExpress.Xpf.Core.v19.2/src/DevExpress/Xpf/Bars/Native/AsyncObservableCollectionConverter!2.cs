namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public class AsyncObservableCollectionConverter<TResult> : ObservableCollectionConverter<TSource, TResult>
    {
        private readonly List<AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData> bridgeList;
        private DispatcherTimer addTimer;
        private WeakEventHandler<AsyncObservableCollectionConverter<TSource, TResult>, EventArgs, EventHandler> addTimerTickHandler;
        private AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData forceAddItem;

        public AsyncObservableCollectionConverter();
        private void CheckStartItemAddition();
        public void ForceAdd(TSource sourceObject);
        public void ForceAddNextItem();
        private void InsertResultElementForData(AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData elementData);
        protected internal override void OnAdd(int p, IList list);
        protected internal override void OnRemove(int p, IList list);
        protected internal override void OnReset();
        private void PerformAddItemAction();
        public void Recreate();
        public void Recreate(Predicate<TSource> predicate);

        public System.Windows.Threading.Dispatcher Dispatcher { get; set; }

        public int SleepTime { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncObservableCollectionConverter<TSource, TResult>.<>c <>9;
            public static Func<DispatcherTimer, bool> <>9__16_0;
            public static Func<bool> <>9__16_1;
            public static Func<AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData, bool> <>9__16_2;
            public static Action<AsyncObservableCollectionConverter<TSource, TResult>, object, EventArgs> <>9__16_3;
            public static Action<WeakEventHandler<AsyncObservableCollectionConverter<TSource, TResult>, EventArgs, EventHandler>, object> <>9__16_4;
            public static Func<WeakEventHandler<AsyncObservableCollectionConverter<TSource, TResult>, EventArgs, EventHandler>, EventHandler> <>9__16_5;
            public static Func<AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData, bool> <>9__17_0;
            public static Func<AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData, bool> <>9__17_1;
            public static Func<AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData, bool> <>9__17_2;

            static <>c();
            internal bool <CheckStartItemAddition>b__16_0(DispatcherTimer x);
            internal bool <CheckStartItemAddition>b__16_1();
            internal bool <CheckStartItemAddition>b__16_2(AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData dt);
            internal void <CheckStartItemAddition>b__16_3(AsyncObservableCollectionConverter<TSource, TResult> t, object o, EventArgs a);
            internal void <CheckStartItemAddition>b__16_4(WeakEventHandler<AsyncObservableCollectionConverter<TSource, TResult>, EventArgs, EventHandler> h, object o);
            internal EventHandler <CheckStartItemAddition>b__16_5(WeakEventHandler<AsyncObservableCollectionConverter<TSource, TResult>, EventArgs, EventHandler> h);
            internal bool <PerformAddItemAction>b__17_0(AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData dt);
            internal bool <PerformAddItemAction>b__17_1(AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData x);
            internal bool <PerformAddItemAction>b__17_2(AsyncObservableCollectionConverter<TSource, TResult>.AOCCElementData dt);
        }

        internal class AOCCElementData
        {
            public bool Exists { get; set; }

            public TSource SourceElement { get; set; }

            public TResult ResultElement { get; set; }

            public int InSourceCollectionIndex { get; set; }

            public int InResultCollectionIndex { get; set; }
        }
    }
}

