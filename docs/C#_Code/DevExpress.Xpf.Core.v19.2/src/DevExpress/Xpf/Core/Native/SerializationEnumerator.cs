namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;

    public class SerializationEnumerator : LogicalTreeEnumerator
    {
        private Func<DependencyObject, bool> nestedChildrenPredicate;

        public SerializationEnumerator(DependencyObject dObject, Func<DependencyObject, bool> nestedChildrenPredicate);
        protected override IEnumerator GetNestedObjects(object obj);

        protected override bool DependencyObjectsOnly { get; }
    }
}

