namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal class ElementRegistratorService : IElementRegistratorService
    {
        private BarNameScope scope;

        internal ElementRegistratorService(BarNameScope scope);
        void IElementRegistratorService.Changed(IBarNameScopeSupport element, object registratorKey);
        IEnumerable<TRegistratorKey> IElementRegistratorService.GetElements<TRegistratorKey>();
        IEnumerable<TRegistratorKey> IElementRegistratorService.GetElements<TRegistratorKey>(ScopeSearchSettings searchMode);
        IEnumerable<TRegistratorKey> IElementRegistratorService.GetElements<TRegistratorKey>(object name);
        IEnumerable<TRegistratorKey> IElementRegistratorService.GetElements<TRegistratorKey>(object name, ScopeSearchSettings searchMode);
        void IElementRegistratorService.NameChanged(IBarNameScopeSupport element, object registratorKey, object oldName, object newName, bool skipNameEqualityCheck);
        [IteratorStateMachine(typeof(ElementRegistratorService.<ParentsAndSelf>d__8))]
        public static IEnumerable<BarNameScope> ParentsAndSelf(BarNameScope scope);

        [Serializable, CompilerGenerated]
        private sealed class <>c__7<TRegistratorKey>
        {
            public static readonly ElementRegistratorService.<>c__7<TRegistratorKey> <>9;
            public static Func<BarNameScope, IEnumerable<IBarNameScopeSupport>> <>9__7_0;

            static <>c__7();
            internal IEnumerable<IBarNameScopeSupport> <DevExpress.Xpf.Bars.IElementRegistratorService.GetElements>b__7_0(BarNameScope x);
        }

        [CompilerGenerated]
        private sealed class <ParentsAndSelf>d__8 : IEnumerable<BarNameScope>, IEnumerable, IEnumerator<BarNameScope>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private BarNameScope <>2__current;
            private int <>l__initialThreadId;
            private BarNameScope scope;
            public BarNameScope <>3__scope;

            [DebuggerHidden]
            public <ParentsAndSelf>d__8(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<BarNameScope> IEnumerable<BarNameScope>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            BarNameScope IEnumerator<BarNameScope>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

