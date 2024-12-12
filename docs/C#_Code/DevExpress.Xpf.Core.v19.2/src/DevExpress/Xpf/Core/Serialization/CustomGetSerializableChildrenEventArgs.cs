namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomGetSerializableChildrenEventArgs : RoutedEventArgs
    {
        internal CustomGetSerializableChildrenEventArgs(object source) : base(DXSerializer.CustomGetSerializableChildrenEvent, source)
        {
            this.Children = new List<DependencyObject>();
        }

        public IList<DependencyObject> Children { get; private set; }
    }
}

