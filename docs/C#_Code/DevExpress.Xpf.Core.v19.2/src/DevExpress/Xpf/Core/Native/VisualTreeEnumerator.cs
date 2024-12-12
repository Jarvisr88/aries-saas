namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class VisualTreeEnumerator : NestedObjectEnumeratorBase, IDisposable
    {
        private EnumeratorDirection direction;

        public VisualTreeEnumerator(DependencyObject dObject);
        protected VisualTreeEnumerator(DependencyObject dObject, EnumeratorDirection direction);
        public void Dispose();
        [IteratorStateMachine(typeof(VisualTreeEnumerator.<GetDependencyObjectEnumerator>d__0))]
        private static IEnumerator<object> GetDependencyObjectEnumerator(DependencyObject dObject, int startIndex, int endIndex, int step);
        protected override IEnumerator GetNestedObjects(object obj);
        public IEnumerable<DependencyObject> GetVisualParents();
        protected virtual bool IsObjectVisual(DependencyObject d);

        public DependencyObject Current { get; }

        [CompilerGenerated]
        private sealed class <GetDependencyObjectEnumerator>d__0 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public int startIndex;
            public DependencyObject dObject;
            private int <i>5__1;
            public int step;
            public int endIndex;

            [DebuggerHidden]
            public <GetDependencyObjectEnumerator>d__0(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

