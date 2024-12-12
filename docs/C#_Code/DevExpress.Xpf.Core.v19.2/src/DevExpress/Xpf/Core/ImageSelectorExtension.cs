namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Markup;
    using System.Windows.Media.Imaging;

    public class ImageSelectorExtension : MarkupExtension
    {
        private static readonly TypeConverter UriTypeConverter = TypeDescriptor.GetConverter(typeof(Uri));

        public ImageSelectorExtension() : this(null)
        {
        }

        public ImageSelectorExtension(object source)
        {
            this.Source = source;
        }

        private static bool CheckResourceExists(ImageKind imageKind, Uri uri, out Uri resourceUri)
        {
            bool flag;
            resourceUri = null;
            try
            {
                if (imageKind != ImageKind.Svg)
                {
                    SvgImageHelper.CreateStream(uri);
                }
                else if (SvgImageHelper.GetOrCreate(uri, new Func<Uri, SvgImage>(SvgImageHelper.CreateImage)) == null)
                {
                    return false;
                }
                resourceUri = uri;
                return true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private static bool GetAbsoluteUri(IServiceProvider serviceProvider, Uri uri, out Uri absoluteUri)
        {
            Uri uri2;
            return UriQualifierHelper.MakeAbsoluteUri(serviceProvider, uri, out absoluteUri, out uri2);
        }

        private static string GetImageFileName(string fileName)
        {
            Func<string, string> evaluator = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<string, string> local1 = <>c.<>9__19_0;
                evaluator = <>c.<>9__19_0 = x => x;
            }
            string local2 = Regex.Match(fileName, @".+(?=_\d+x\d+$)").Value.WithString<string>(evaluator);
            string local4 = local2;
            if (local2 == null)
            {
                string local3 = local2;
                local4 = fileName;
            }
            return local4;
        }

        private static void GetImageFileNameParts(Uri uri, out string fileName, out ImageKind? kind)
        {
            fileName = uri.OriginalString;
            kind = 0;
            foreach (ImageKind kind2 in Enum.GetValues(typeof(ImageKind)))
            {
                string str = "." + kind2.ToString();
                if (fileName.EndsWith(str, StringComparison.InvariantCultureIgnoreCase))
                {
                    fileName = fileName.Substring(0, fileName.Length - str.Length);
                    kind = new ImageKind?(kind2);
                    break;
                }
            }
        }

        private static object GetImageObject(ImageKind imageKind, IServiceProvider serviceProvider, Uri imageUri)
        {
            if (imageKind == ImageKind.Png)
            {
                return new BitmapImage(imageUri);
            }
            if (imageKind != ImageKind.Svg)
            {
                throw new NotImplementedException();
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = imageUri;
            return extension1.ProvideValue(serviceProvider);
        }

        public static object GetImageObject(IServiceProvider serviceProvider, object source, object svgSource)
        {
            object obj2;
            if (ApplicationThemeHelper.UseDefaultSvgImages)
            {
                object obj1 = svgSource;
                if (svgSource == null)
                {
                    object local1 = svgSource;
                    obj1 = source;
                }
                if (GetImageObject(serviceProvider, obj1, ImageKind.Svg, out obj2))
                {
                    return obj2;
                }
            }
            object obj3 = source;
            if (source == null)
            {
                object local2 = source;
                obj3 = svgSource;
            }
            return (!GetImageObject(serviceProvider, obj3, ImageKind.Png, out obj2) ? null : obj2);
        }

        private static bool GetImageObject(IServiceProvider serviceProvider, object source, ImageKind imageKind, out object image)
        {
            object obj2;
            image = null;
            if (source == null)
            {
                return false;
            }
            if (UriTypeConverter.CanConvertFrom(source.GetType()))
            {
                Uri uri2;
                Uri uri = (Uri) UriTypeConverter.ConvertFrom(source);
                if (GetImageUri(serviceProvider, uri, imageKind, out uri2))
                {
                    image = GetImageObject(imageKind, serviceProvider, uri2);
                    return true;
                }
            }
            image = obj2 = (source as MarkupExtension).With<MarkupExtension, object>(x => x.ProvideValue(serviceProvider));
            return (obj2 != null);
        }

        private static bool GetImageUri(IServiceProvider serviceProvider, Uri uri, ImageKind imageKind, out Uri imageUri)
        {
            Uri uri2;
            string imageFileName;
            ImageKind? nullable;
            Guard.ArgumentNotNull(uri, "uri");
            imageUri = null;
            if (!GetAbsoluteUri(serviceProvider, uri, out uri2))
            {
                return false;
            }
            GetImageFileNameParts(uri2, out imageFileName, out nullable);
            if (imageKind == ImageKind.Svg)
            {
                ImageKind? nullable2 = nullable;
                ImageKind svg = ImageKind.Svg;
                if ((((ImageKind) nullable2.GetValueOrDefault()) == svg) ? (nullable2 == null) : true)
                {
                    imageFileName = GetImageFileName(imageFileName);
                }
            }
            string uriString = $"{imageFileName}.{imageKind}";
            return CheckResourceExists(imageKind, new Uri(uriString), out imageUri);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            GetImageObject(serviceProvider, this.Source, this.SvgSource);

        public object Source { get; set; }

        public object SvgSource { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageSelectorExtension.<>c <>9 = new ImageSelectorExtension.<>c();
            public static Func<string, string> <>9__19_0;

            internal string <GetImageFileName>b__19_0(string x) => 
                x;
        }

        private enum ImageKind
        {
            Png,
            Svg
        }
    }
}

