namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Data;

    public class PrinterTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PrinterType type = (PrinterType) value;
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            bool flag = type.HasFlag(PrinterType.Default);
            return ((!type.HasFlag(PrinterType.Fax) || !type.HasFlag(PrinterType.Network)) ? (!type.HasFlag(PrinterType.Fax) ? ((!type.HasFlag(PrinterType.Printer) || !type.HasFlag(PrinterType.Network)) ? (!type.HasFlag(PrinterType.Printer) ? null : (flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\DefaultPrinter_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\Printer_16x16.png"))) : (flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\DefaultPrinterNetwork_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\PrinterNetwork_16x16.png"))) : (flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\DefaultFax_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\Fax_16x16.png"))) : (flag ? DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\DefaultFaxNetwork_16x16.png") : DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(name, @"\Images\Print\FaxNetwork_16x16.png")));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

