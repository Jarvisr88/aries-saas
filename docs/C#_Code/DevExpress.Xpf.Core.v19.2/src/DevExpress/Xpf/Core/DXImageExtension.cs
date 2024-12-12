namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class DXImageExtension : DXImageExtensionBase
    {
        public DXImageExtension()
        {
        }

        public DXImageExtension(DXImagePathInfo pathInfo)
        {
            this.ImagePath = pathInfo;
        }

        public DXImageExtension(string imageString)
        {
            this.ImagePath = DXImagePathConverter.GetDXPictureInfo(imageString);
        }

        protected override ImageSource GetImageSource()
        {
            if (this.ImagePath == null)
            {
                Func<DXImageInfo, ImageSource> evaluator = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<DXImageInfo, ImageSource> local1 = <>c.<>9__11_0;
                    evaluator = <>c.<>9__11_0 = x => ImageSourceHelper.GetImageSource(x.MakeUri());
                }
                return this.Image.With<DXImageInfo, ImageSource>(evaluator);
            }
            Uri uri = this.ImagePath.MakeUri();
            if (!uri.OriginalString.EndsWith(".svg"))
            {
                return ImageSourceHelper.GetImageSource(uri);
            }
            Size? targetSize = null;
            bool? autoSize = null;
            return WpfSvgRenderer.CreateImageSource(uri, null, targetSize, null, null, autoSize);
        }

        [ConstructorArgument("pathInfo"), TypeConverter(typeof(DXImagePathConverter))]
        public DXImagePathInfo ImagePath { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), TypeConverter(typeof(DXImageConverter))]
        public DXImageInfo Image { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXImageExtension.<>c <>9 = new DXImageExtension.<>c();
            public static Func<DXImageInfo, ImageSource> <>9__11_0;

            internal ImageSource <GetImageSource>b__11_0(DXImageInfo x) => 
                ImageSourceHelper.GetImageSource(x.MakeUri());
        }
    }
}

