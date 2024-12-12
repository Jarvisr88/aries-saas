namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ContentTemplateConverter : IValueConverter
    {
        private static ContentTemplateConverter instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            RenderTriggerHelper.GetConvertedValue(targetType, value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static ContentTemplateConverter Instance
        {
            get
            {
                instance ??= new ContentTemplateConverter();
                return instance;
            }
        }
    }
}

