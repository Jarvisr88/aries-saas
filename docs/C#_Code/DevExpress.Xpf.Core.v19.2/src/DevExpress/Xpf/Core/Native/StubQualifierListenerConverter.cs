namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    internal class StubQualifierListenerConverter : IValueConverter
    {
        private readonly Uri uri;
        private readonly Func<ICollection<UriInfo>> uriCandidates;
        private readonly DependencyProperty targetProperty;

        public StubQualifierListenerConverter(Uri uri, Func<ICollection<UriInfo>> uriCandidates, DependencyProperty targetProperty);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}

