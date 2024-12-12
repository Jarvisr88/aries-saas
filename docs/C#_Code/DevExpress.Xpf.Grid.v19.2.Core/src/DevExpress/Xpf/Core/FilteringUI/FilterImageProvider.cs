namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows.Media;

    internal static class FilterImageProvider
    {
        private const string imageFolderPath = "pack://application:,,,/DevExpress.Xpf.Grid.v19.2.Core;component/FilteringUI/FilterEditor/Images/";
        private const string imageExtension = ".svg";

        public static ImageSource GetImage(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            SvgImageSourceExtension extension1 = new SvgImageSourceExtension();
            extension1.Uri = new Uri("pack://application:,,,/DevExpress.Xpf.Grid.v19.2.Core;component/FilteringUI/FilterEditor/Images/" + name + ".svg");
            return (ImageSource) extension1.ProvideValue(null);
        }
    }
}

