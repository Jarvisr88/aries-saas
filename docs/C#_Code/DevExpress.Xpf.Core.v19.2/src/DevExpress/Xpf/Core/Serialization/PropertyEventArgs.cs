namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class PropertyEventArgs : RoutedEventArgs
    {
        private DependencyPropertyDescriptor dependencyPropertyDescriptor;

        public PropertyEventArgs(PropertyDescriptor property, object source, RoutedEvent routedEvent) : base(routedEvent, source)
        {
            this.Property = property;
        }

        protected DependencyPropertyDescriptor GetDependencyPropertyDescriptor()
        {
            this.dependencyPropertyDescriptor ??= DependencyPropertyDescriptor.FromProperty(this.Property);
            return this.dependencyPropertyDescriptor;
        }

        public PropertyDescriptor Property { get; private set; }

        public System.Windows.DependencyProperty DependencyProperty
        {
            get
            {
                DependencyPropertyDescriptor dependencyPropertyDescriptor = this.GetDependencyPropertyDescriptor();
                return dependencyPropertyDescriptor?.DependencyProperty;
            }
        }
    }
}

