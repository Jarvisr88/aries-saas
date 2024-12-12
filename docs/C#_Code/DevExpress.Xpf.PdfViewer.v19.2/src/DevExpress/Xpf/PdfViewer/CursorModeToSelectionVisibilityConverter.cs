namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class CursorModeToSelectionVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((CursorModeType) value) == CursorModeType.MarqueeZoom) ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            (((Visibility) value) == Visibility.Hidden) ? CursorModeType.SelectTool : CursorModeType.MarqueeZoom;
    }
}

