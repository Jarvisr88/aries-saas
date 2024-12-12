namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Windows.Media;

    internal class DrawingImageConverter : IOneTypeObjectConverter
    {
        public object FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = PatchingImageSourceConverter.PatchImagePathWithCurrentAssemblyVersion(str);
            IconSetExtension extension1 = new IconSetExtension();
            extension1.Name = str;
            return extension1.ProvideValue(null);
        }

        public string ToString(object obj) => 
            PatchingImageSourceConverter.ToStringBase(obj);

        public System.Type Type =>
            typeof(DrawingImage);
    }
}

