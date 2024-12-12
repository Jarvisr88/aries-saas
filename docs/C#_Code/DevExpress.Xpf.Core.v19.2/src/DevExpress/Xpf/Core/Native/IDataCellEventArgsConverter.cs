namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm;
    using System.Windows;

    public interface IDataCellEventArgsConverter
    {
        CellValue GetDataCell(RoutedEventArgs e);
    }
}

