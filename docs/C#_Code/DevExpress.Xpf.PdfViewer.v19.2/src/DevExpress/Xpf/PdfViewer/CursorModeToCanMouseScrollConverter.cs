namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CursorModeToCanMouseScrollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((CursorModeType) value) == CursorModeType.HandTool;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ((bool) value) ? CursorModeType.HandTool : CursorModeType.SelectTool;
    }
}

