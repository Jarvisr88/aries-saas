namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;

    public class SingleObjectEnumerator : VisualTreeEnumerator
    {
        public SingleObjectEnumerator(DependencyObject dObject);
        protected override IEnumerator GetNestedObjects(object obj);
    }
}

