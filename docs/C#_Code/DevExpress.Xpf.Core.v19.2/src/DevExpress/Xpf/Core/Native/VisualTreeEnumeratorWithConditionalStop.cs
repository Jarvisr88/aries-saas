namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;

    public class VisualTreeEnumeratorWithConditionalStop : IEnumerator<DependencyObject>, IDisposable, IEnumerator
    {
        private DependencyObject Root;
        private Stack<DependencyObject> Stack;
        private Predicate<DependencyObject> TreeStop;
        private DependencyObject current;

        public VisualTreeEnumeratorWithConditionalStop(DependencyObject root, Predicate<DependencyObject> treeStop);
        public void Dispose();
        public bool MoveNext();
        public void Reset();

        object IEnumerator.Current { get; }

        public DependencyObject Current { get; }
    }
}

