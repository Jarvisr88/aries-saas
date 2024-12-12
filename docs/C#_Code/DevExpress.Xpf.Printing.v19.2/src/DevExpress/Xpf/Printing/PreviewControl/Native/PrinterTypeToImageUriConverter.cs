namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Data;

    public class PrinterTypeToImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            PrinterType type = (PrinterType) value;
            bool flag = type.HasFlag(PrinterType.Default);
            Uri uri = null;
            if (type.HasFlag(PrinterType.Fax) && type.HasFlag(PrinterType.Network))
            {
                uri = flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\DefaultFaxNetwork_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\FaxNetwork_16x16.png");
            }
            else if (type.HasFlag(PrinterType.Fax))
            {
                uri = flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\DefaultFax_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\Fax_16x16.png");
            }
            else if (type.HasFlag(PrinterType.Printer) && type.HasFlag(PrinterType.Network))
            {
                uri = flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\DefaultPrinterNetwork_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\PrinterNetwork_16x16.png");
            }
            else if (type.HasFlag(PrinterType.Printer))
            {
                uri = flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\DefaultPrinter_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Printers\Printer_16x16.png");
            }
            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

