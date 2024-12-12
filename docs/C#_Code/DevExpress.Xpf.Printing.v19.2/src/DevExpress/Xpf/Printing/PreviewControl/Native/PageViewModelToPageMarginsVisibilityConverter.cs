namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class PageViewModelToPageMarginsVisibilityConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Func<object, bool> predicate = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__1_0;
                predicate = <>c.<>9__1_0 = x => x == DependencyProperty.UnsetValue;
            }
            if (values.Any<object>(predicate))
            {
                return Visibility.Collapsed;
            }
            int num = Guard.ArgumentMatchType<int>(values[1], "values[1]");
            return ((Guard.ArgumentMatchType<PageViewModel>(values[0], "values[0]").PageIndex == (num - 1)) ? Visibility.Visible : Visibility.Collapsed);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageViewModelToPageMarginsVisibilityConverter.<>c <>9 = new PageViewModelToPageMarginsVisibilityConverter.<>c();
            public static Func<object, bool> <>9__1_0;

            internal bool <System.Windows.Data.IMultiValueConverter.Convert>b__1_0(object x) => 
                x == DependencyProperty.UnsetValue;
        }
    }
}

