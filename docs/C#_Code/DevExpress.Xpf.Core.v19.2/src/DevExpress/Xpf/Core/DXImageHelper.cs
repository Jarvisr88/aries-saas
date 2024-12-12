namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public static class DXImageHelper
    {
        private static IDXImagesProvider imageProvider;

        static DXImageHelper()
        {
            ImageSourceHelper.RegisterPackScheme();
        }

        private static IDXImagesProvider CreateImageProvider() => 
            CreateImageProviderInstance();

        private static IDXImagesProvider CreateImageProviderInstance() => 
            (IDXImagesProvider) Activator.CreateInstance(Type.GetType("DevExpress.Images.DXImageServicesImp, DevExpress.Images.v19.2" + AssemblyHelper.GetFullNameAppendix()));

        internal static string GetFile(string name, ImageSize imageSize, ImageType imageType) => 
            ImagesProvider.GetFile(name, imageSize, imageType);

        public static ImageSource GetImageSource(string path) => 
            ImageSourceHelper.GetImageSource(GetImageUri(path));

        public static ImageSource GetImageSource(string id, ImageSize imageSize, ImageType imageType = 1) => 
            ImageSourceHelper.GetImageSource(GetImageUri(id, imageSize, imageType));

        public static Uri GetImageUri(string path) => 
            new Uri("pack://application:,,,/DevExpress.Images.v19.2;component/" + path, UriKind.RelativeOrAbsolute);

        public static Uri GetImageUri(string id, ImageSize imageSize, ImageType imageType = 1) => 
            new Uri(GetFile(id, imageSize, imageType), UriKind.RelativeOrAbsolute);

        internal static IDXImagesProvider ImagesProvider =>
            imageProvider ??= CreateImageProvider();
    }
}

