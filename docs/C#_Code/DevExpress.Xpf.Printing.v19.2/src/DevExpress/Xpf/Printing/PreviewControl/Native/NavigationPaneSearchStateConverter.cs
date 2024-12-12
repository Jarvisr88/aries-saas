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

    public class NavigationPaneSearchStateConverter : MarkupExtension, IMultiValueConverter
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
            int num2 = (int) values[2];
            return ((state != SearchState.None) ? (((state != SearchState.InProgress) || (num != 0)) ? (((state != SearchState.Finished) || (num != 0)) ? ((num2 != -1) ? string.Format(PrintingLocalizer.GetString(PrintingStringId.NavigationPane_ResultIndex), num2 + 1, num) : string.Format(PrintingLocalizer.GetString(PrintingStringId.NavigationPane_ResultCount), num)) : PrintingLocalizer.GetString(PrintingStringId.NavigationPane_NoMatches)) : PrintingLocalizer.GetString(PrintingStringId.NavigationPane_Searching)) : null);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneSearchStateConverter.<>c <>9 = new NavigationPaneSearchStateConverter.<>c();
            public static Func<object, bool> <>9__1_0;

            internal bool <System.Windows.Data.IMultiValueConverter.Convert>b__1_0(object x) => 
                x == DependencyProperty.UnsetValue;
        }
    }
}

