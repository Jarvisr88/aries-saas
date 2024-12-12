namespace DevExpress.Mvvm.UI
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    internal static class ConverterHelper
    {
        public static Visibility BooleanToVisibility(bool booleanValue, bool hiddenInsteadOfCollapsed) => 
            booleanValue ? Visibility.Visible : (hiddenInsteadOfCollapsed ? Visibility.Hidden : Visibility.Collapsed);

        private static bool CorrectBoolean(bool value, bool inverse) => 
            value ^ inverse;

        public static bool GetBooleanParameter(string[] parameters, string name)
        {
            foreach (string str in parameters)
            {
                if (string.Equals(str, name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetBooleanValue(object value)
        {
            if (value as bool)
            {
                return (bool) value;
            }
            if (!(value is bool?))
            {
                return ((value is DefaultBoolean) && (((DefaultBoolean) value) == DefaultBoolean.True));
            }
            bool? nullable = (bool?) value;
            return ((nullable != null) ? nullable.Value : false);
        }

        public static bool? GetNullableBooleanValue(object value)
        {
            if (value as bool)
            {
                return new bool?((bool) value);
            }
            if (value is bool?)
            {
                return (bool?) value;
            }
            if (value is DefaultBoolean)
            {
                DefaultBoolean flag = (DefaultBoolean) value;
                if (flag != DefaultBoolean.Default)
                {
                    return new bool?(flag == DefaultBoolean.True);
                }
            }
            return null;
        }

        public static string[] GetParameters(object parameter)
        {
            string str = parameter as string;
            if (string.IsNullOrEmpty(str))
            {
                return new string[0];
            }
            char[] separator = new char[] { ';' };
            return str.Split(separator);
        }

        public static bool NumericToBoolean(object value, bool inverse)
        {
            bool flag;
            if (value == null)
            {
                return CorrectBoolean(false, inverse);
            }
            try
            {
                flag = CorrectBoolean(!(((double) Convert.ChangeType(value, typeof(double), null)) == 0.0), inverse);
            }
            catch (Exception)
            {
                return CorrectBoolean(false, inverse);
            }
            return flag;
        }

        public static bool StringToBoolean(object value, bool inverse) => 
            (value is string) ? CorrectBoolean(!string.IsNullOrEmpty((string) value), inverse) : CorrectBoolean(false, inverse);

        public static DefaultBoolean ToDefaultBoolean(bool? booleanValue) => 
            (booleanValue == null) ? DefaultBoolean.Default : (booleanValue.Value ? DefaultBoolean.True : DefaultBoolean.False);
    }
}

