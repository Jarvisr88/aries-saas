namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
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
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            bool flag = ((PrinterType) value).HasFlag(PrinterType.Default);
            return (new PrinterTypeToImageUriConverter().Convert(value, null, parameter, culture) as Uri).With<Uri, object>(x => new UriToBitmapImageConverterExtension().Convert(x, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

