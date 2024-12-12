namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Resources;
    using System.Runtime.CompilerServices;

    internal class PackResourceCollection : ResourceCollection
    {
        private ResourceSet set;

        public PackResourceCollection(ResourceSet set);
        [IteratorStateMachine(typeof(PackResourceCollection.<EnumerateResourceKeys>d__2))]
        protected override IEnumerable<string> EnumerateResourceKeys();

        [CompilerGenerated]
        private sealed class <EnumerateResourceKeys>d__2 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private string <>2__current;
            private int <>l__initialThreadId;
            public PackResourceCollection <>4__this;
            private IDictionaryEnumerator <en>5__1;

            [DebuggerHidden]
            public <EnumerateResourceKeys>d__2(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            string IEnumerator<string>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

