namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    internal class IndependentRoutedEventArgs : IRoutedEventArgs
    {
        private readonly RoutedEventArgs component;

        public IndependentRoutedEventArgs(RoutedEventArgs arg)
        {
            Guard.ArgumentNotNull(arg, "arg");
            this.component = arg;
        }

        bool IRoutedEventArgs.Handled
        {
            get => 
                this.component.Handled;
            set => 
                this.component.Handled = value;
        }

        object IRoutedEventArgs.OriginalSource =>
            this.component.OriginalSource;
    }
}

