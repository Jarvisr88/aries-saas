namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LogicalTreeEnumerator : VisualTreeEnumerator
    {
        private Hashtable acceptedVisuals;

        public LogicalTreeEnumerator(DependencyObject dObject);
        protected override IEnumerator GetNestedObjects(object obj);
        [IteratorStateMachine(typeof(LogicalTreeEnumerator.<GetVisualAndLogicalChildren>d__1))]
        private static IEnumerator GetVisualAndLogicalChildren(object obj, IEnumerator visualChildren, bool dependencyObjectsOnly, Hashtable acceptedVisuals);
        public override void Reset();

        protected virtual bool DependencyObjectsOnly { get; }

    }
}

