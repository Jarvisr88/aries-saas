namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class ImageAlignmentToImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageAlignment? nullable = value as ImageAlignment?;
            if (nullable == null)
            {
                throw new InvalidOperationException();
            }
            string format = "pack://application:,,,/{0};component/Images/ImageEditItems/Alignment{1}.svg";
            string str2 = null;
            switch (nullable.Value)
            {
                case ImageAlignment.TopLeft:
                    str2 = "TL";
                    break;

                case ImageAlignment.TopCenter:
                    str2 = "TC";
                    break;

                case ImageAlignment.TopRight:
                    str2 = "TR";
                    break;

                case ImageAlignment.MiddleLeft:
                    str2 = "ML";
                    break;

                case ImageAlignment.MiddleCenter:
                    str2 = "MC";
                    break;

                case ImageAlignment.MiddleRight:
                    str2 = "MR";
                    break;

                case ImageAlignment.BottomLeft:
                    str2 = "BL";
                    break;

                case ImageAlignment.BottomCenter:
                    str2 = "BC";
                    break;

                case ImageAlignment.BottomRight:
                    str2 = "BR";
                    break;

                default:
                    throw new InvalidOperationException();
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri(string.Format(format, "DevExpress.Xpf.Printing.v19.2", str2));
            return extension1.ProvideValue(null);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

