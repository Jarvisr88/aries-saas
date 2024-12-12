namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal class BooleanAndConverter : IMultiValueConverter
    {
        public BooleanAndConverter()
        {
            this.UseNullableValues = false;
            this.NullValue = false;
        }

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object obj2 in values)
            {
                if (obj2 == DependencyProperty.UnsetValue)
                {
                    return DependencyProperty.UnsetValue;
                }
                if (!(!this.UseNullableValues ? ((bool) obj2) : ((((bool?) obj2) != null) ? ((bool?) obj2).Value : this.NullValue)))
                {
                    return false;
                }
            }
            return true;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool UseNullableValues { get; set; }

        public bool NullValue { get; set; }
    }
}

