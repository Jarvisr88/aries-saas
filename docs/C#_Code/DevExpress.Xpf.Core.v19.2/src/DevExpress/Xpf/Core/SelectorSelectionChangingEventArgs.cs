namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectorSelectionChangingEventArgs : CancelRoutedEventArgs
    {
        public SelectorSelectionChangingEventArgs(IEnumerable<object> removedItems, IEnumerable<int> removedIndexes, IEnumerable<object> addedItems, IEnumerable<int> addedIndexes)
        {
            this.Init(removedItems, removedIndexes, addedItems, addedIndexes);
        }

        public SelectorSelectionChangingEventArgs(RoutedEvent routedEvent, IEnumerable<object> removedItems, IEnumerable<int> removedIndexes, IEnumerable<object> addedItems, IEnumerable<int> addedIndexes) : base(routedEvent)
        {
            this.Init(removedItems, removedIndexes, addedItems, addedIndexes);
        }

        public SelectorSelectionChangingEventArgs(RoutedEvent routedEvent, object source, IEnumerable<object> removedItems, IEnumerable<int> removedIndexes, IEnumerable<object> addedItems, IEnumerable<int> addedIndexes) : base(routedEvent, source)
        {
            this.Init(removedItems, removedIndexes, addedItems, addedIndexes);
        }

        private void Init(IEnumerable<object> removedItems, IEnumerable<int> removedIndexes, IEnumerable<object> addedItems, IEnumerable<int> addedIndexes)
        {
            this.RemovedItems = removedItems;
            this.RemovedIndexes = removedIndexes;
            this.AddedItems = addedItems;
            this.AddedIndexes = addedIndexes;
        }

        public IEnumerable<object> RemovedItems { get; private set; }

        public IEnumerable<int> RemovedIndexes { get; private set; }

        public IEnumerable<object> AddedItems { get; private set; }

        public IEnumerable<int> AddedIndexes { get; private set; }

        public object RemovedItem
        {
            get
            {
                Func<IEnumerable<object>, object> evaluator = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Func<IEnumerable<object>, object> local1 = <>c.<>9__17_0;
                    evaluator = <>c.<>9__17_0 = x => x.FirstOrDefault<object>();
                }
                return this.RemovedItems.With<IEnumerable<object>, object>(evaluator);
            }
        }

        public object AddedItem
        {
            get
            {
                Func<IEnumerable<object>, object> evaluator = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<IEnumerable<object>, object> local1 = <>c.<>9__19_0;
                    evaluator = <>c.<>9__19_0 = x => x.FirstOrDefault<object>();
                }
                return this.AddedItems.With<IEnumerable<object>, object>(evaluator);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectorSelectionChangingEventArgs.<>c <>9 = new SelectorSelectionChangingEventArgs.<>c();
            public static Func<IEnumerable<object>, object> <>9__17_0;
            public static Func<IEnumerable<object>, object> <>9__19_0;

            internal object <get_AddedItem>b__19_0(IEnumerable<object> x) => 
                x.FirstOrDefault<object>();

            internal object <get_RemovedItem>b__17_0(IEnumerable<object> x) => 
                x.FirstOrDefault<object>();
        }
    }
}

