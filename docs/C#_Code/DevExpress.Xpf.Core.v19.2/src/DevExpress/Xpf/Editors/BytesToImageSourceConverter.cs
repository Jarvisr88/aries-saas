namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;

    public class BytesToImageSourceConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj2;
            byte[] buffer = value as byte[];
            if (buffer == null)
            {
                return null;
            }
            try
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    obj2 = ImageHelper.CreateImageFromStream(stream);
                }
            }
            catch
            {
                obj2 = null;
            }
            return obj2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

