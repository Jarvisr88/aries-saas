namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class DXImageConverter : TypeConverter
    {
        private static readonly Lazy<Dictionary<string, DXImageInfo>> ImagesColored;
        protected static readonly Lazy<Dictionary<string, DXImageInfo>> ImagesGrayscale;
        protected static readonly Lazy<Dictionary<string, DXImageInfo>> ImagesOffice2013;
        protected static readonly Lazy<Dictionary<string, DXImageInfo>> ImagesSvg;

        static DXImageConverter();
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public static IEnumerable<IDXImageInfo> GetDistinctImages(IDXImagesProvider imagesProvider);
        private static Lazy<Dictionary<string, DXImageInfo>> GetImages(ImageType imageType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context);

        protected virtual Lazy<Dictionary<string, DXImageInfo>> Images { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXImageConverter.<>c <>9;
            public static Func<IDXImageInfo, bool> <>9__0_0;
            public static Func<IDXImageInfo, DXImageInfo> <>9__5_2;
            public static Func<DXImageInfo, string> <>9__5_3;

            static <>c();
            internal bool <GetDistinctImages>b__0_0(IDXImageInfo x);
            internal DXImageInfo <GetImages>b__5_2(IDXImageInfo x);
            internal string <GetImages>b__5_3(DXImageInfo x);
        }
    }
}

