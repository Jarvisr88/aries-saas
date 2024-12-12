namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;

    public class SerializationVisualEnumerator : VisualTreeEnumerator
    {
        private Func<DependencyObject, bool> nestedChildrenPredicate;

        public SerializationVisualEnumerator(DependencyObject dObject, Func<DependencyObject, bool> nestedChildrenPredicate);
        protected override IEnumerator GetNestedObjects(object obj);
    }
}

