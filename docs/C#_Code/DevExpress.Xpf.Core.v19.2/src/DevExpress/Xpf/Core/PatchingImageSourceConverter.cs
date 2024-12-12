namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Media;

    internal class PatchingImageSourceConverter : IOneTypeObjectConverter
    {
        private ImageSourceConverter adaptee = new ImageSourceConverter();
        private const string imagePathPattern = @"(^pack:\/\/application:,,,\/DevExpress\.)([a-zA-Z\.]+)(\.v\d{2}\.\d{1})(;component\/\S+$)";
        private static Regex imagePathRegex = new Regex(@"(^pack:\/\/application:,,,\/DevExpress\.)([a-zA-Z\.]+)(\.v\d{2}\.\d{1})(;component\/\S+$)");

        public object FromString(string str) => 
            !string.IsNullOrEmpty(str) ? this.GetImageSource(PatchImagePathWithCurrentAssemblyVersion(str)) : str;

        private object GetImageSource(string str)
        {
            Uri uri;
            if (Uri.TryCreate(str, UriKind.Absolute, out uri) && (uri != null))
            {
                return this.adaptee.ConvertFromInvariantString(str);
            }
            IconSetExtension extension1 = new IconSetExtension();
            extension1.Name = str;
            return extension1.ProvideValue(null);
        }

        internal static string PatchImagePathWithCurrentAssemblyVersion(string str)
        {
            MatchEvaluator evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                MatchEvaluator local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = m => m.Groups[1].Value + m.Groups[2].Value + ".v19.2" + m.Groups[4].Value;
            }
            return imagePathRegex.Replace(str, evaluator);
        }

        public string ToString(object obj) => 
            this.adaptee.ConvertToInvariantString(obj);

        internal static string ToStringBase(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj is DependencyObject)
            {
                string iconName = IconSetExtension.GetIconName(obj as DependencyObject);
                if (!string.IsNullOrEmpty(iconName))
                {
                    return iconName;
                }
            }
            return new ImageSourceConverter().ConvertToInvariantString(obj);
        }

        public System.Type Type =>
            typeof(ImageSource);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PatchingImageSourceConverter.<>c <>9 = new PatchingImageSourceConverter.<>c();
            public static MatchEvaluator <>9__3_0;

            internal string <PatchImagePathWithCurrentAssemblyVersion>b__3_0(Match m) => 
                m.Groups[1].Value + m.Groups[2].Value + ".v19.2" + m.Groups[4].Value;
        }
    }
}

