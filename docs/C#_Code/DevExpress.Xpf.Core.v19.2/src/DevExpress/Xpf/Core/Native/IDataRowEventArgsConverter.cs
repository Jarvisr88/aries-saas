namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface IDataRowEventArgsConverter
    {
        object GetDataRow(RoutedEventArgs e);
    }
}

