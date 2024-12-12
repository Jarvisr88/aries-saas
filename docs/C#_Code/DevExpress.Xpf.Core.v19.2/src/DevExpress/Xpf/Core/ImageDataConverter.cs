namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class ImageDataConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(ImageSource)))
            {
                throw new NotSupportedException();
            }
            if (value == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream((byte[]) value))
            {
                return CreateImageFromStream(stream);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        internal static object CreateImageFromStream(Stream stream) => 
            ImageHelper.CreateImageFromStream(stream);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

