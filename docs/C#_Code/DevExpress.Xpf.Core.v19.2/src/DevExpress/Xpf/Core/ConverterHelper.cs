namespace DevExpress.Xpf.Core
{
    using System;
    using System.Globalization;
    using System.Windows;

    internal static class ConverterHelper
    {
        private static ThicknessConverter thicknessConverter = new ThicknessConverter();
        private static CornerRadiusConverter cornerRadiusConverter = new CornerRadiusConverter();

        public static object Convert(object value, Type targetType) => 
            (value is string) ? (!(targetType == typeof(Thickness)) ? (!(targetType == typeof(CornerRadius)) ? value : cornerRadiusConverter.ConvertFrom(null, CultureInfo.InvariantCulture, value)) : thicknessConverter.ConvertFrom(null, CultureInfo.InvariantCulture, value)) : value;
    }
}

