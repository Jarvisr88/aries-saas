namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public sealed class ThemeResourceConverter : IMultiValueConverter
    {
        [ThreadStatic]
        private static DevExpress.Xpf.Core.Internal.ReflectionHelper reflectionHelper;

        static ThemeResourceConverter();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
        private bool IsFrameworkElement(object obj);
        private object TryFindResource(object obj, object resourceKey);

        private static DevExpress.Xpf.Core.Internal.ReflectionHelper ReflectionHelper { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemeResourceConverter.<>c <>9;
            public static Func<object, bool> <>9__3_0;

            static <>c();
            internal bool <Convert>b__3_0(object val);
        }
    }
}

