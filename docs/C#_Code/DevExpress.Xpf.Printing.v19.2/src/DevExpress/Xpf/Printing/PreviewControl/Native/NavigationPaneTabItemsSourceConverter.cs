namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class NavigationPaneTabItemsSourceConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                Func<object, bool> predicate = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<object, bool> local1 = <>c.<>9__1_0;
                    predicate = <>c.<>9__1_0 = x => (x == DependencyProperty.UnsetValue) || !(x is bool);
                }
                if (!values.Any<object>(predicate))
                {
                    bool flag = (bool) values[0];
                    List<NavigationPaneTabType> list1 = new List<NavigationPaneTabType>();
                    list1.Add(NavigationPaneTabType.SearchResults);
                    List<NavigationPaneTabType> list = list1;
                    if ((bool) values[1])
                    {
                        list.Insert(0, NavigationPaneTabType.Pages);
                    }
                    if (flag)
                    {
                        list.Insert(0, NavigationPaneTabType.DocumentMap);
                    }
                    return list;
                }
            }
            return NavigationPaneTabType.SearchResults.Yield<NavigationPaneTabType>();
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneTabItemsSourceConverter.<>c <>9 = new NavigationPaneTabItemsSourceConverter.<>c();
            public static Func<object, bool> <>9__1_0;

            internal bool <System.Windows.Data.IMultiValueConverter.Convert>b__1_0(object x) => 
                (x == DependencyProperty.UnsetValue) || !(x is bool);
        }
    }
}

