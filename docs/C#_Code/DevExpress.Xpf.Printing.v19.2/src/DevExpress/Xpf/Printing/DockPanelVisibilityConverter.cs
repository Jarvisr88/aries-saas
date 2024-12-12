namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class DockPanelVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2)
            {
                Func<object, bool> predicate = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<object, bool> local1 = <>c.<>9__0_0;
                    predicate = <>c.<>9__0_0 = x => x == DependencyProperty.UnsetValue;
                }
                if (!values.Any<object>(predicate))
                {
                    bool local6;
                    bool? nullable = values[0] as bool?;
                    Func<bool?, bool> evaluator = <>c.<>9__0_1;
                    if (<>c.<>9__0_1 == null)
                    {
                        Func<bool?, bool> local2 = <>c.<>9__0_1;
                        evaluator = <>c.<>9__0_1 = x => x.Value;
                    }
                    bool flag = (values[1] as bool?).Return<bool, bool>(evaluator, <>c.<>9__0_2 ??= () => false);
                    if (values.Length != 3)
                    {
                        local6 = true;
                    }
                    else
                    {
                        Func<bool?, bool> func3 = <>c.<>9__0_3;
                        if (<>c.<>9__0_3 == null)
                        {
                            Func<bool?, bool> local4 = <>c.<>9__0_3;
                            func3 = <>c.<>9__0_3 = x => x.Value;
                        }
                        local6 = (values[2] as bool?).Return<bool, bool>(func3, <>c.<>9__0_4 ??= () => true);
                    }
                    bool flag2 = local6;
                    return (((nullable != null) ? (!flag ? false : nullable.Value) : (flag2 & flag)) ? Visibility.Visible : Visibility.Collapsed);
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            bool flag = ((Visibility) value) == Visibility.Visible;
            return new object[] { flag };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockPanelVisibilityConverter.<>c <>9 = new DockPanelVisibilityConverter.<>c();
            public static Func<object, bool> <>9__0_0;
            public static Func<bool?, bool> <>9__0_1;
            public static Func<bool> <>9__0_2;
            public static Func<bool?, bool> <>9__0_3;
            public static Func<bool> <>9__0_4;

            internal bool <Convert>b__0_0(object x) => 
                x == DependencyProperty.UnsetValue;

            internal bool <Convert>b__0_1(bool? x) => 
                x.Value;

            internal bool <Convert>b__0_2() => 
                false;

            internal bool <Convert>b__0_3(bool? x) => 
                x.Value;

            internal bool <Convert>b__0_4() => 
                true;
        }
    }
}

