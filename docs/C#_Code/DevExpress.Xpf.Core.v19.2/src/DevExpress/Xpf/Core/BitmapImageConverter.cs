namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal class BitmapImageConverter : IOneTypeObjectConverter
    {
        public object FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = PatchingImageSourceConverter.PatchImagePathWithCurrentAssemblyVersion(str);
            return new ImageSourceConverter().ConvertFromInvariantString(str);
        }

        public string ToString(object obj) => 
            PatchingImageSourceConverter.ToStringBase(obj);

        public System.Type Type =>
            typeof(BitmapImage);
    }
}

