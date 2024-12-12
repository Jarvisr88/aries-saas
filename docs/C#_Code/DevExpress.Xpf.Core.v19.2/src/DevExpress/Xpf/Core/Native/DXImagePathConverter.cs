namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class DXImagePathConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        protected internal static DXImagePathInfo GetDXPictureInfo(string stringValue);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXImagePathConverter.<>c <>9;
            public static Func<IDXImageInfo, DXImagePathInfo> <>9__0_1;

            static <>c();
            internal DXImagePathInfo <GetDXPictureInfo>b__0_1(IDXImageInfo x);
        }
    }
}

