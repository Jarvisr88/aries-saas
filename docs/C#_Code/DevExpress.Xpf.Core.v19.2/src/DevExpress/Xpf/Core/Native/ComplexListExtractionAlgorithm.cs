namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Data;

    public class ComplexListExtractionAlgorithm : ListExtractionAlgorithmBase
    {
        private readonly ItemPropertyNotificationMode itemPropertyNotificationMode;
        private readonly bool wrapDataView;
        private readonly bool wrapRealList;
        private readonly bool listenToComplexProperties;
        private readonly bool useSlidingSubscription;

        public ComplexListExtractionAlgorithm(ItemPropertyNotificationMode itemPropertyNotificationMode = 1, bool wrapDataView = false, bool wrapRealList = false, bool listenToComplexProperties = false, bool useSlidingSubscription = false);
        public override IList Extract(object dataSource);
        protected virtual object GetDataSourceFromProvider(DataSourceProvider provider);
        protected virtual IList GetListFromDictionary(IList list, IEnumerable enumerableDataSource);
        protected virtual IList GetListFromICollectionView(ICollectionView collectionView);
        protected virtual IList GetListFromIEnumerable(IEnumerable enumerable);
        protected virtual object GetListFromListSource(IListSource listSource);
        [IteratorStateMachine(typeof(ComplexListExtractionAlgorithm.<PreloadEFCoreSource>d__11))]
        private IEnumerable PreloadEFCoreSource(IEnumerable listSource);
        private bool UseWrapRealList(IList list);
        private IList ValidateList(IList list);

        [CompilerGenerated]
        private sealed class <PreloadEFCoreSource>d__11 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable listSource;
            public IEnumerable <>3__listSource;
            private IEnumerator <enumerator>5__1;

            [DebuggerHidden]
            public <PreloadEFCoreSource>d__11(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

