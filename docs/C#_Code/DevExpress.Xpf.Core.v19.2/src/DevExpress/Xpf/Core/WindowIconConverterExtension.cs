namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Security;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class WindowIconConverterExtension : MarkupExtension, IMultiValueConverter
    {
        private static WindowIconConverterExtension converter;

        [SecuritySafeCritical]
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource[] sourceArray;
            ImageSource[] sourceArray2;
            object obj2 = value[0];
            DevExpress.Xpf.Core.Native.DpiScale dpi = WindowUtility.GetDpi(TreeHelper.GetParent<Window>((FrameworkElement) value[1], null, true, true));
            System.Windows.Size iconSize = new System.Windows.Size(((double) NativeWindowMethods.GetSystemMetrics(11)) / dpi.DpiScaleX, ((double) NativeWindowMethods.GetSystemMetrics(12)) / dpi.DpiScaleY);
            System.Windows.Size size = new System.Windows.Size(((double) NativeWindowMethods.GetSystemMetrics(50)) / dpi.DpiScaleX, ((double) NativeWindowMethods.GetSystemMetrics(50)) / dpi.DpiScaleY);
            if (obj2 is BitmapFrame)
            {
                BitmapFrame frame = obj2 as BitmapFrame;
                if (frame.Decoder != null)
                {
                    return (frame.Decoder.Frames.FirstOrDefault<BitmapFrame>(x => ((x.Height == iconSize.Height) && (x.Width == iconSize.Width))) ?? frame);
                }
            }
            if ((obj2 != null) && (obj2 != DependencyProperty.UnsetValue))
            {
                return (ImageSource) obj2;
            }
            List<ImageSource> list = new List<ImageSource>();
            WindowUtility.GetIcons(out sourceArray, out sourceArray2);
            ImageSource source = Get16x16Icon(sourceArray2, size);
            return ((source == null) ? (!sourceArray.Any<ImageSource>() ? SystemIcons.Application.ToImageSource() : sourceArray.First<ImageSource>()) : source);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static ImageSource Get16x16Icon(ImageSource[] icons, System.Windows.Size iconSize) => 
            icons.FirstOrDefault<ImageSource>(icon => (icon.Height == iconSize.Height) && (icon.Width == iconSize.Width));

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            converter ??= new WindowIconConverterExtension();
    }
}

