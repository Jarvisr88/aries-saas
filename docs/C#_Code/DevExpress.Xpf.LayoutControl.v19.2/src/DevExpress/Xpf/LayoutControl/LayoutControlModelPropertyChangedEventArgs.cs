namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutControlModelPropertyChangedEventArgs : LayoutControlModelChangedEventArgs
    {
        public LayoutControlModelPropertyChangedEventArgs(DependencyObject obj, string propertyName, DependencyProperty property)
        {
            this.Object = obj;
            this.PropertyName = propertyName;
            this.Property = property;
            base.ChangeDescription = "Change " + this.PropertyName;
        }

        public DependencyObject Object { get; private set; }

        public DependencyProperty Property { get; private set; }

        public string PropertyName { get; private set; }
    }
}

