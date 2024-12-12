namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScrollBarAnnotationsCreatingEventArgs : RoutedEventArgs
    {
        public ScrollBarAnnotationsCreatingEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public ICollection<ScrollBarAnnotationRowInfo> CustomScrollBarAnnotations { get; set; }
    }
}

