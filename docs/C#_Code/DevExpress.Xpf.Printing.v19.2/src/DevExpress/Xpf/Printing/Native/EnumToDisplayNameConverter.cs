namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class EnumToDisplayNameConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if ((value == DependencyProperty.UnsetValue) || (value == null))
            {
                return null;
            }
            Func<string> fallback = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<string> local1 = <>c.<>9__1_0;
                fallback = <>c.<>9__1_0 = () => string.Empty;
            }
            string str = (value as Enum).Return<Enum, string>(new Func<Enum, string>(DisplayTypeNameHelper.GetDisplayTypeName), fallback);
            return (string.IsNullOrEmpty(str) ? value.ToString() : str);
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumToDisplayNameConverter.<>c <>9 = new EnumToDisplayNameConverter.<>c();
            public static Func<string> <>9__1_0;

            internal string <System.Windows.Data.IValueConverter.Convert>b__1_0() => 
                string.Empty;
        }
    }
}

