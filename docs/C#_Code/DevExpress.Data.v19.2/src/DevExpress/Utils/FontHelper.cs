namespace DevExpress.Utils
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class FontHelper
    {
        private static GetNativeFamily getNativeFamily;
        private static readonly CultureInfo englishNeutralCulture = CultureInfo.CreateSpecificCulture("en");

        public static float GetCellAscent(Font font) => 
            (font.FontFamily.GetCellAscent(font.Style) * font.Size) / ((float) font.FontFamily.GetEmHeight(font.Style));

        public static float GetCellDescent(Font font) => 
            (font.FontFamily.GetCellDescent(font.Style) * font.Size) / ((float) font.FontFamily.GetEmHeight(font.Style));

        public static string GetEnglishFamilyName(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            return GetEnglishFamilyName(font.FontFamily);
        }

        public static string GetEnglishFamilyName(FontFamily family)
        {
            if (family == null)
            {
                throw new ArgumentNullException("family");
            }
            return family.GetName(englishNeutralCulture.LCID);
        }

        public static string GetFamilyName(Font font)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            return GetFamilyName(font.FontFamily);
        }

        public static string GetFamilyName(FontFamily family)
        {
            if (family == null)
            {
                throw new ArgumentNullException("family");
            }
            return family.Name;
        }

        public static long GetFontHashCode(Font font)
        {
            long num = ((long) (font.Size * 100f)) << 0x20;
            return (GetFontHashCodeCore(font) ^ num);
        }

        private static long GetFontHashCodeCore(Font font)
        {
            long num = NativeFamily(font.FontFamily).ToInt64();
            int num2 = ((int) (num >> 0x20)) ^ ((int) (((ulong) num) & 0xffffffffUL));
            int style = (int) font.Style;
            int unit = (int) font.Unit;
            uint size = (uint) font.Size;
            return ((((num2 ^ ((style << 13) | (style >> 0x13))) ^ ((unit << 0x1a) | (unit >> 6))) ^ ((size << 7) | (size >> 0x19))) + (num2 << 40));
        }

        public static float GetLineSpacing(Font font) => 
            (font.FontFamily.GetLineSpacing(font.Style) * font.Size) / ((float) font.FontFamily.GetEmHeight(font.Style));

        private static GetNativeFamily NativeFamily
        {
            get
            {
                if (getNativeFamily == null)
                {
                    try
                    {
                        getNativeFamily = (GetNativeFamily) Delegate.CreateDelegate(typeof(GetNativeFamily), null, typeof(FontFamily).GetProperty("NativeFamily", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true));
                        getNativeFamily(FontFamily.GenericSansSerif);
                    }
                    catch
                    {
                        GetNativeFamily family1 = <>c.<>9__4_0;
                        if (<>c.<>9__4_0 == null)
                        {
                            GetNativeFamily local2 = <>c.<>9__4_0;
                            family1 = <>c.<>9__4_0 = f => new IntPtr(f.GetHashCode());
                        }
                        getNativeFamily = family1;
                    }
                }
                return getNativeFamily;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FontHelper.<>c <>9 = new FontHelper.<>c();
            public static FontHelper.GetNativeFamily <>9__4_0;

            internal IntPtr <get_NativeFamily>b__4_0(FontFamily f) => 
                new IntPtr(f.GetHashCode());
        }

        private delegate IntPtr GetNativeFamily(FontFamily family);
    }
}

