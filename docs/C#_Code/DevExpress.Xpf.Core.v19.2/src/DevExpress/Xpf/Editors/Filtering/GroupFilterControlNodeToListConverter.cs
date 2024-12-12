namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Data;

    public class GroupFilterControlNodeToListConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<GroupNode> collection1 = new ObservableCollection<GroupNode>();
            collection1.Add((GroupNode) value);
            return collection1;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

