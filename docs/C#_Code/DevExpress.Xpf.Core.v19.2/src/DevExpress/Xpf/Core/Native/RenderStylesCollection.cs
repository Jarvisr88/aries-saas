namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RenderStylesCollection : ObservableCollection<RenderStyle>
    {
        private static readonly object Locker;
        private bool isSealed;
        private RenderStylesCollection basedOn;
        private RenderTriggersCollection triggers;
        private readonly Dictionary<RenderStyleTarget, RenderStyleSetter[]> cache;

        static RenderStylesCollection();
        public RenderStylesCollection();
        public void Apply(IFrameworkRenderElement renderTree);
        [IteratorStateMachine(typeof(RenderStylesCollection.<EnumerateTriggers>d__17))]
        public IEnumerable<RenderTriggerBase> EnumerateTriggers();
        [IteratorStateMachine(typeof(RenderStylesCollection.<FindSetters>d__15))]
        private IEnumerable<RenderStyleSetter> FindSetters(RenderStyleTarget key);
        [IteratorStateMachine(typeof(RenderStylesCollection.<FindStyles>d__16))]
        private IEnumerable<Tuple<RenderStyle, uint>> FindStyles(RenderStyleTarget key, byte index);
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
        public void Seal();

        public RenderStylesCollection BasedOn { get; set; }

        public RenderTriggersCollection Triggers { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderStylesCollection.<>c <>9;
            public static Func<Tuple<RenderStyle, uint>, uint> <>9__15_0;
            public static Func<Tuple<RenderStyle, uint>, RenderStyle> <>9__15_1;

            static <>c();
            internal uint <FindSetters>b__15_0(Tuple<RenderStyle, uint> x);
            internal RenderStyle <FindSetters>b__15_1(Tuple<RenderStyle, uint> x);
        }


        [CompilerGenerated]
        private sealed class <FindSetters>d__15 : IEnumerable<RenderStyleSetter>, IEnumerable, IEnumerator<RenderStyleSetter>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RenderStyleSetter <>2__current;
            private int <>l__initialThreadId;
            public RenderStylesCollection <>4__this;
            private RenderStyleTarget key;
            public RenderStyleTarget <>3__key;
            private HashSet<string> <foundSetters>5__1;
            private IEnumerator<RenderStyle> <>7__wrap1;
            private IEnumerator<RenderStyleSetter> <>7__wrap2;

            [DebuggerHidden]
            public <FindSetters>d__15(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<RenderStyleSetter> IEnumerable<RenderStyleSetter>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            RenderStyleSetter IEnumerator<RenderStyleSetter>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

    }
}

