namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Themes;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public class IconSetExtension : MarkupExtension
    {
        public static readonly DependencyProperty IconNameProperty = DependencyProperty.RegisterAttached("IconName", typeof(string), typeof(IconSetExtension), new PropertyMetadata(null));

        public static string GetIconName(DependencyObject obj) => 
            (string) obj.GetValue(IconNameProperty);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return null;
            }
            bool? isSvg = null;
            Uri uriSource = QuickIconSetFormatExtension.GetUri(this.Name, isSvg);
            if (uriSource == null)
            {
                return null;
            }
            if (!ApplicationThemeHelper.UseDefaultSvgImages)
            {
                BitmapImage image = new BitmapImage(uriSource);
                SetIconName(image, this.Name);
                return image;
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = uriSource;
            extension1.ActionBeforeFreeze = delegate (DrawingImage img) {
                SetIconName(img, this.Name);
            };
            return extension1.ProvideValue(null);
        }

        public static void SetIconName(DependencyObject obj, string value)
        {
            obj.SetValue(IconNameProperty, value);
        }

        public string Name { get; set; }
    }
}

