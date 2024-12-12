namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RenderTriggersCollection : ObservableCollection<RenderTriggerBase>
    {
        private ObservableCollection<RenderTriggersCollection> mergedTriggers;
        private RenderTriggersCollection source;

        public RenderTriggersCollection();
        [IteratorStateMachine(typeof(RenderTriggersCollection.<EnumerateItems>d__13))]
        public virtual IEnumerable<RenderTriggerBase> EnumerateItems();
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        protected virtual void OnMergedTriggersChanged(ObservableCollection<RenderTriggersCollection> oldValue, ObservableCollection<RenderTriggersCollection> newValue);
        protected virtual void OnSourceChanged(RenderTriggersCollection oldValue, RenderTriggersCollection newValue);
        protected virtual void ValidateSource();

        public ObservableCollection<RenderTriggersCollection> MergedTriggers { get; set; }

        public RenderTriggersCollection Source { get; set; }

        [CompilerGenerated]
        private sealed class <EnumerateItems>d__13 : IEnumerable<RenderTriggerBase>, IEnumerable, IEnumerator<RenderTriggerBase>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RenderTriggerBase <>2__current;
            private int <>l__initialThreadId;
            public RenderTriggersCollection <>4__this;
            private IEnumerator<RenderTriggerBase> <>7__wrap1;
            private IEnumerator<RenderTriggersCollection> <>7__wrap2;

            [DebuggerHidden]
            public <EnumerateItems>d__13(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private void <>m__Finally3();
            private void <>m__Finally4();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<RenderTriggerBase> IEnumerable<RenderTriggerBase>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            RenderTriggerBase IEnumerator<RenderTriggerBase>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

