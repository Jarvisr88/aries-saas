namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Globalization;

    public static class TypeCastHelper
    {
        public static object TryCast(object value, Type targetType)
        {
            Type enumType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (enumType.IsEnum && (value is string))
            {
                value = Enum.Parse(enumType, (string) value, false);
            }
            else if ((value is IConvertible) && !targetType.IsAssignableFrom(value.GetType()))
            {
                value = Convert.ChangeType(value, enumType, CultureInfo.InvariantCulture);
            }
            if ((value == null) && targetType.IsValueType)
            {
                value = Activator.CreateInstance(targetType);
            }
            return value;
        }
    }
}

