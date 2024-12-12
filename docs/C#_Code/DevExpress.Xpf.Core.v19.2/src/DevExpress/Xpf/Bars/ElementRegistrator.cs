namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ElementRegistrator
    {
        private readonly object key;

        public event ElementRegistratorChangedEventHandler Changed;

        public ElementRegistrator(object key, bool uniqueElements);
        public void Add(IBarNameScopeSupport element);
        public void Detach();
        public object GetName(IBarNameScopeSupport element);
        [IteratorStateMachine(typeof(ElementRegistrator.<GetValue>d__39))]
        private IEnumerable<IBarNameScopeSupport> GetValue(object name);
        private IEnumerable<IBarNameScopeSupport> GetValues();
        protected bool IsValidName(IBarNameScopeSupport element);
        protected bool IsValidName(object name);
        public void NameChanged(IBarNameScopeSupport element, object oldValue, object newValue);
        protected void RaiseChanged(IBarNameScopeSupport element, ElementRegistratorChangeType change, object oldName, object currentName);
        public void Remove(IBarNameScopeSupport element);
        private IEnumerable<IBarNameScopeSupport> Wrap(IEnumerable<IBarNameScopeSupport> source);

        protected MultiDictionary<object, IBarNameScopeSupport> Elements { get; private set; }

        protected Dictionary<IBarNameScopeSupport, object> ElementNames { get; private set; }

        public bool Unique { get; private set; }

        public static bool GlobalSkipUniquenessCheck { get; set; }

        public bool SkipUniquenessCheck { get; set; }

        private bool ActualSkipUniquenessCheck { get; }

        public object Key { get; }

        public Func<object, bool> ValidateNamePredicate { get; set; }

        public IEnumerable<IBarNameScopeSupport> this[object name] { get; }

        public IEnumerable<IBarNameScopeSupport> Values { get; }

        [CompilerGenerated]
        private sealed class <GetValue>d__39 : IEnumerable<IBarNameScopeSupport>, IEnumerable, IEnumerator<IBarNameScopeSupport>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IBarNameScopeSupport <>2__current;
            private int <>l__initialThreadId;
            private object name;
            public object <>3__name;
            public ElementRegistrator <>4__this;
            private IEnumerator<IBarNameScopeSupport> <>7__wrap1;

            [DebuggerHidden]
            public <GetValue>d__39(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<IBarNameScopeSupport> IEnumerable<IBarNameScopeSupport>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            IBarNameScopeSupport IEnumerator<IBarNameScopeSupport>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

