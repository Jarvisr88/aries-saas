namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Windows;

    public class DocumentMapSelectionChangedEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object sender, object args) => 
            (args is RoutedPropertyChangedEventArgs<object>) ? ((RoutedPropertyChangedEventArgs<object>) args).NewValue : null;
    }
}

