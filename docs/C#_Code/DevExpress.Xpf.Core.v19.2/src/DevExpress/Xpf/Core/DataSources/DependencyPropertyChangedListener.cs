namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    public class DependencyPropertyChangedListener : FrameworkElement
    {
        private bool shouldRaiseEvent;
        private readonly Dictionary<DependencyObject, List<DependencyProperty>> propsStorage = new Dictionary<DependencyObject, List<DependencyProperty>>();
        private readonly Dictionary<DependencyProperty, DependencyObject> storage = new Dictionary<DependencyProperty, DependencyObject>();

        public event EventHandler<ListenerPropertyChangedEventArgs> DependencyPropertyChanged;

        private void ClearProperties(DependencyObject source)
        {
            foreach (DependencyProperty property in this.propsStorage[source])
            {
                base.ClearValue(property);
                this.storage.Remove(property);
            }
        }

        public bool IsRegistered(DependencyObject source) => 
            this.propsStorage.ContainsKey(source);

        private void OnDependencyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.shouldRaiseEvent)
            {
                this.shouldRaiseEvent = false;
                this.RaiseDependencyPropertyChanged(new ListenerPropertyChangedEventArgs(this.storage[e.Property], base.GetBindingExpression(e.Property).ParentBinding.Path.Path));
                this.shouldRaiseEvent = true;
            }
        }

        private void RaiseDependencyPropertyChanged(ListenerPropertyChangedEventArgs args)
        {
            if (this.DependencyPropertyChanged != null)
            {
                this.DependencyPropertyChanged(this, args);
            }
        }

        public void Register(DependencyObject source, Binding binding)
        {
            PropertyChangedCallback propertyChangedCallback = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                PropertyChangedCallback local1 = <>c.<>9__6_0;
                propertyChangedCallback = <>c.<>9__6_0 = (d, e) => ((DependencyPropertyChangedListener) d).OnDependencyPropertyChanged(e);
            }
            DependencyProperty key = DependencyPropertyManager.Register(Guid.NewGuid().ToString(), typeof(object), typeof(DependencyPropertyChangedListener), new FrameworkPropertyMetadata(null, propertyChangedCallback));
            if (!this.propsStorage.ContainsKey(source))
            {
                this.propsStorage[source] = new List<DependencyProperty>();
            }
            this.storage.Add(key, source);
            this.propsStorage[source].Add(key);
            this.shouldRaiseEvent = false;
            BindingOperations.SetBinding(this, key, binding);
            this.shouldRaiseEvent = true;
        }

        public void Unregister(DependencyObject source)
        {
            if (this.propsStorage.ContainsKey(source))
            {
                this.ClearProperties(source);
                this.propsStorage.Remove(source);
            }
        }

        public void UnregisterAll()
        {
            foreach (DependencyObject obj2 in this.propsStorage.Keys)
            {
                this.ClearProperties(obj2);
            }
            this.propsStorage.Clear();
            this.storage.Clear();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DependencyPropertyChangedListener.<>c <>9 = new DependencyPropertyChangedListener.<>c();
            public static PropertyChangedCallback <>9__6_0;

            internal void <Register>b__6_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DependencyPropertyChangedListener) d).OnDependencyPropertyChanged(e);
            }
        }
    }
}

