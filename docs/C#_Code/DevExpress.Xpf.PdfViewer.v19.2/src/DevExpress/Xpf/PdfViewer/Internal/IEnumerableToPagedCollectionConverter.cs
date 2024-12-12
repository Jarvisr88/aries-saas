namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;

    public class IEnumerableToPagedCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            new PagedCollectionView((IEnumerable<int>) value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

