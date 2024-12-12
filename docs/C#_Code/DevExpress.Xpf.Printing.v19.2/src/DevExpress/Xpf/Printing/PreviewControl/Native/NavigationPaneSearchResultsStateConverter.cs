namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NavigationPaneSearchResultsStateConverter : MarkupExtension, IMultiValueConverter
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
                return null;
            }
            SearchState state = (SearchState) values[0];
            int num = (int) values[1];
            string str = (string) values[2];
            return (((state != SearchState.None) || !string.IsNullOrEmpty(str)) ? (((state != SearchState.Finished) || (num != 0)) ? null : PrintingLocalizer.GetString(PrintingStringId.NavigationPane_NoMatchesFound)) : PrintingLocalizer.GetString(PrintingStringId.NavigationPane_EnterTextToSearch));
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneSearchResultsStateConverter.<>c <>9 = new NavigationPaneSearchResultsStateConverter.<>c();
            public static Func<object, bool> <>9__1_0;

            internal bool <System.Windows.Data.IMultiValueConverter.Convert>b__1_0(object x) => 
                x == DependencyProperty.UnsetValue;
        }
    }
}

