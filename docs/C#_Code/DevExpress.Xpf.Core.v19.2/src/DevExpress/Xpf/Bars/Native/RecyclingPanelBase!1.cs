namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Threading;

    public abstract class RecyclingPanelBase : VirtualizingPanel, IRecyclingPanel
    {
        private int maxItemsCount;
        private static readonly Action<UIElementCollection, UIElement> RemoveNoVerify;
        private static readonly Action<UIElementCollection, UIElement> ClearLogicalParent;
        private static readonly Action<UIElementCollection, int, UIElement> InsertInternal;
        private List<RecyclingPanelBase<TElement>.RealizedItemData> realizedItems;
        private List<RecyclingPanelBase<TElement>.RecycledItemData> recycledItems;
        private Queue<DependencyObject> recyclableContainers;
        private DispatcherTimer cleanupTimer;
        private static readonly Func<ItemContainerGenerator, IList> getItemsInternal;
        private static readonly Func<ItemContainerGenerator, Queue<DependencyObject>> getRecyclableContainers;
        private readonly Locker recyclableLocker;
        private DependencyObject[] tempRecyclableContainers;

        static RecyclingPanelBase();
        public RecyclingPanelBase();
        protected void AddRecycledContainersToGenerator(IEnumerable<TElement> enumerable);
        protected virtual void AddRecycledItem(TElement element);
        protected virtual bool AllowRecycledContainerFor(object item);
        public void BeforeAddItem(int index, object item);
        public void BeforeRemoveItem(int index, object item);
        protected virtual bool CanRecycle(TElement container);
        private void CheckRecyclableContainers(TElement curr);
        protected void CleanUpRecycledContainers(bool force);
        public void ClearCaches();
        [IteratorStateMachine(typeof(RecyclingPanelBase<>.<EnumerateChildren>d__40))]
        protected IEnumerable<TElement> EnumerateChildren();
        private bool GenerateContainerImpl(RecyclingPanelBase<TElement>.RealizedItemData itemData, int index);
        private TElement GenerateNext();
        private TElement GenerateNext(out bool isNewlyRealized);
        private GeneratorPosition GeneratorPositionFromIndex(int itemIndex);
        private ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel);
        private int IndexFromGeneratorPosition(GeneratorPosition position);
        private void OnCleanupTimerTick(object sender, EventArgs e);
        protected override void OnClearChildren();
        private void OnItemsAdded(int newIndex, int cout);
        protected sealed override void OnItemsChanged(object sender, ItemsChangedEventArgs args);
        private void OnItemsRemoved(int oldIndex, int count, bool itemsGenerated);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnRecycled(TElement container);
        protected virtual void OnReused(TElement container, object item);
        protected virtual void OnUnloaded(object sender, RoutedEventArgs e);
        private IDisposable PatchRecyclableContainers(int position, out object item);
        private void PrepareContainerImpl(RecyclingPanelBase<TElement>.RealizedItemData current);
        private void PrepareItemContainer(UIElement container);
        private void ReattachToGenerator();
        private void RecyclableLocker_Unlocked(object sender, EventArgs e);
        private void Recycle(GeneratorPosition position, int count);
        private void Remove(GeneratorPosition position, int count);
        private void RemoveAll();
        protected void RemoveRecycledContainersFromGenerator(IEnumerable<TElement> enumerable);
        protected virtual bool RemoveRecycledItem(TElement element);
        public virtual TElement SelectContainerForReuse(object item);
        private IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction);
        private IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem);

        private Queue<DependencyObject> RecyclableContainers { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RecyclingPanelBase<TElement>.<>c <>9;
            public static Func<RecyclingPanelBase<TElement>.RecycledItemData, bool> <>9__8_0;
            public static Func<RecyclingPanelBase<TElement>.RecycledItemData, int> <>9__8_1;
            public static Action<RecyclingPanelBase<TElement>.RecycledItemData> <>9__8_2;
            public static Func<RecyclingPanelBase<TElement>.RecycledItemData, TElement> <>9__8_3;
            public static Func<<>f__AnonymousType5<int, TElement>, int> <>9__10_1;
            public static Func<<>f__AnonymousType5<int, TElement>, bool> <>9__10_2;
            public static Func<<>f__AnonymousType5<int, TElement>, TElement> <>9__10_3;

            static <>c();
            internal void <.cctor>b__1_0(UIElementCollection a, UIElement b);
            internal void <.cctor>b__1_1(UIElementCollection a, UIElement b);
            internal IList <.cctor>b__1_2(ItemContainerGenerator g);
            internal Queue<DependencyObject> <.cctor>b__1_3(ItemContainerGenerator x);
            internal bool <CleanUpRecycledContainers>b__8_0(RecyclingPanelBase<TElement>.RecycledItemData x);
            internal int <CleanUpRecycledContainers>b__8_1(RecyclingPanelBase<TElement>.RecycledItemData x);
            internal void <CleanUpRecycledContainers>b__8_2(RecyclingPanelBase<TElement>.RecycledItemData x);
            internal TElement <CleanUpRecycledContainers>b__8_3(RecyclingPanelBase<TElement>.RecycledItemData x);
            internal int <SelectContainerForReuse>b__10_1(<>f__AnonymousType5<int, TElement> x);
            internal bool <SelectContainerForReuse>b__10_2(<>f__AnonymousType5<int, TElement> x);
            internal TElement <SelectContainerForReuse>b__10_3(<>f__AnonymousType5<int, TElement> x);
        }

        [CompilerGenerated]
        private sealed class <EnumerateChildren>d__40 : IEnumerable<TElement>, IEnumerable, IEnumerator<TElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private TElement <>2__current;
            private int <>l__initialThreadId;
            public RecyclingPanelBase<TElement> <>4__this;
            private int <index>5__1;
            private RecyclingPanelBase<TElement>.RealizedItemData <data>5__2;
            private int <i>5__3;
            private IDisposable <>7__wrap1;

            [DebuggerHidden]
            public <EnumerateChildren>d__40(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            TElement IEnumerator<TElement>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        private class RealizedItemData
        {
            public RealizedItemData();

            public TElement Container { get; set; }

            public bool Dirty { get; set; }
        }

        private class RecycledItemData
        {
            public RecycledItemData(TElement container);

            public TElement Container { get; set; }

            public int LifeTime { get; set; }
        }
    }
}

