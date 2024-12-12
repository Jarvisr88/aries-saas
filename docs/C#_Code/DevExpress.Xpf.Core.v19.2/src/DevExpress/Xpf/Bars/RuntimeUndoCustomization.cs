namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RuntimeUndoCustomization : RuntimeCustomization
    {
        public RuntimeUndoCustomization();
        public RuntimeUndoCustomization(string name);
        protected override bool ApplyOverride(bool silent);
        [IteratorStateMachine(typeof(RuntimeUndoCustomization.<GetTargets>d__5))]
        protected IEnumerable<RuntimeCustomization> GetTargets();
        protected override bool TryOverwriteOverride(RuntimeCustomization second);
        protected override bool UndoOverride();

        public override bool IsInformativeCustomization { get; }

        [CompilerGenerated]
        private sealed class <GetTargets>d__5 : IEnumerable<RuntimeCustomization>, IEnumerable, IEnumerator<RuntimeCustomization>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private RuntimeCustomization <>2__current;
            private int <>l__initialThreadId;
            public RuntimeUndoCustomization <>4__this;
            private bool <removeAll>5__1;
            private int <i>5__2;

            [DebuggerHidden]
            public <GetTargets>d__5(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<RuntimeCustomization> IEnumerable<RuntimeCustomization>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            RuntimeCustomization IEnumerator<RuntimeCustomization>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

