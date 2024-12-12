namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;

    public class VisualTreeEnumerable : IEnumerable
    {
        private IEnumerator enumerator;

        public VisualTreeEnumerable(DependencyObject dObject)
        {
            this.enumerator = new VisualTreeEnumerator(dObject);
        }

        public IEnumerator GetEnumerator() => 
            this.enumerator;
    }
}

