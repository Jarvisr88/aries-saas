namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class ColumnChooserCaptionLocalizationStringConvertor : IValueConverter
    {
        private readonly DataViewBase view;

        public ColumnChooserCaptionLocalizationStringConvertor(DataViewBase view)
        {
            this.view = view;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LocalizationDescriptor descriptor = value as LocalizationDescriptor;
            return ((descriptor != null) ? (!this.view.DataControl.AllowBandChooser ? ((this.view.DetailHeaderContent == null) ? descriptor.GetValue(0x22.ToString()) : string.Format(descriptor.GetValue(0x24.ToString()), this.view.DetailHeaderContent)) : descriptor.GetValue(0x23.ToString())) : null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

