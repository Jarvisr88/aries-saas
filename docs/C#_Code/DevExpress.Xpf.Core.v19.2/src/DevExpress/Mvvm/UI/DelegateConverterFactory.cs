namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public static class DelegateConverterFactory
    {
        public static IMultiValueConverter CreateMultiValueConverter(Func<object[], object> convert, Func<object, object[]> convertBack = null) => 
            new DelegateMultiValueConverter(convert, convertBack, -1, true);

        public static IMultiValueConverter CreateMultiValueConverter<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> convert, Func<TOut, Tuple<TIn1, TIn2>> convertBack = null) => 
            new DelegateMultiValueConverter(x => convert((TIn1) x[0], (TIn2) x[1]), x => ToArray<TIn1, TIn2>(convertBack((TOut) x)), 2, false);

        public static IMultiValueConverter CreateMultiValueConverter<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> convert, Func<TOut, Tuple<TIn1, TIn2, TIn3>> convertBack = null) => 
            new DelegateMultiValueConverter(x => convert((TIn1) x[0], (TIn2) x[1], (TIn3) x[2]), x => ToArray<TIn1, TIn2, TIn3>(convertBack((TOut) x)), 3, false);

        public static IMultiValueConverter CreateMultiValueConverter<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> convert, Func<TOut, Tuple<TIn1, TIn2, TIn3, TIn4>> convertBack = null) => 
            new DelegateMultiValueConverter(x => convert((TIn1) x[0], (TIn2) x[1], (TIn3) x[2], (TIn4) x[3]), x => ToArray<TIn1, TIn2, TIn3, TIn4>(convertBack((TOut) x)), 4, false);

        public static IMultiValueConverter CreateMultiValueConverter(Func<object[], Type, object, CultureInfo, object> convert, Func<object, Type[], object, CultureInfo, object[]> convertBack = null) => 
            new DelegateMultiValueConverter(convert, convertBack, -1, true);

        public static IMultiValueConverter CreateMultiValueConverter<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> convert, Func<TOut, Tuple<TIn1, TIn2, TIn3, TIn4, TIn5>> convertBack = null) => 
            new DelegateMultiValueConverter(x => convert((TIn1) x[0], (TIn2) x[1], (TIn3) x[2], (TIn4) x[3], (TIn5) x[4]), x => ToArray<TIn1, TIn2, TIn3, TIn4, TIn5>(convertBack((TOut) x)), 5, false);

        public static IMultiValueConverter CreateMultiValueConverter<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> convert, Func<TOut, Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>> convertBack = null) => 
            new DelegateMultiValueConverter(x => convert((TIn1) x[0], (TIn2) x[1], (TIn3) x[2], (TIn4) x[3], (TIn5) x[4], (TIn6) x[5]), x => ToArray<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(convertBack((TOut) x)), 6, false);

        public static IValueConverter CreateValueConverter(Func<object, object> convert, Func<object, object> convertBack = null) => 
            new DelegateValueConverter<object, object>(convert, convertBack, true);

        public static IValueConverter CreateValueConverter<TIn, TOut>(Func<TIn, TOut> convert, Func<TOut, TIn> convertBack = null) => 
            new DelegateValueConverter<TIn, TOut>(convert, convertBack, false);

        public static IValueConverter CreateValueConverter<TIn, TOut>(Func<TIn, object, CultureInfo, TOut> convert, Func<TOut, object, CultureInfo, TIn> convertBack = null) => 
            new DelegateValueConverter<TIn, TOut>(convert, convertBack, false);

        public static IValueConverter CreateValueConverter(Func<object, Type, object, CultureInfo, object> convert, Func<object, Type, object, CultureInfo, object> convertBack = null) => 
            new DelegateValueConverter<object, object>(convert, convertBack, true);

        private static object[] ToArray<T1, T2>(Tuple<T1, T2> tuple) => 
            new object[] { tuple.Item1, tuple.Item2 };

        private static object[] ToArray<T1, T2, T3>(Tuple<T1, T2, T3> tuple) => 
            new object[] { tuple.Item1, tuple.Item2, tuple.Item3 };

        private static object[] ToArray<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> tuple) => 
            new object[] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };

        private static object[] ToArray<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> tuple) => 
            new object[] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5 };

        private static object[] ToArray<T1, T2, T3, T4, T5, T6>(Tuple<T1, T2, T3, T4, T5, T6> tuple) => 
            new object[] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6 };

        private class DelegateMultiValueConverter : IMultiValueConverter
        {
            private readonly Func<object[], object> convert1;
            private readonly Func<object, object[]> convertBack1;
            private readonly Func<object[], Type, object, CultureInfo, object> convert2;
            private readonly Func<object, Type[], object, CultureInfo, object[]> convertBack2;
            private readonly int valuesCount;
            private readonly bool allowUnsetValue;

            public DelegateMultiValueConverter(Func<object[], object> convert, Func<object, object[]> convertBack, int valuesCount, bool allowUnsetValue)
            {
                this.convert1 = convert;
                this.convertBack1 = convertBack;
                this.valuesCount = valuesCount;
                this.allowUnsetValue = allowUnsetValue;
            }

            public DelegateMultiValueConverter(Func<object[], Type, object, CultureInfo, object> convert, Func<object, Type[], object, CultureInfo, object[]> convertBack, int valuesCount, bool allowUnsetValue)
            {
                this.convert2 = convert;
                this.convertBack2 = convertBack;
                this.valuesCount = valuesCount;
                this.allowUnsetValue = allowUnsetValue;
            }

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if ((this.valuesCount > 0) && (values.Count<object>() != this.valuesCount))
                {
                    throw new TargetParameterCountException();
                }
                if (!this.allowUnsetValue)
                {
                    Func<object, bool> predicate = <>c.<>9__8_0;
                    if (<>c.<>9__8_0 == null)
                    {
                        Func<object, bool> local1 = <>c.<>9__8_0;
                        predicate = <>c.<>9__8_0 = x => x == DependencyProperty.UnsetValue;
                    }
                    if (values.Any<object>(predicate))
                    {
                        return Binding.DoNothing;
                    }
                }
                if (this.convert1 != null)
                {
                    return this.convert1(values);
                }
                if (this.convert2 == null)
                {
                    throw new InvalidOperationException();
                }
                return this.convert2(values, targetType, parameter, culture);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                if (this.convertBack1 != null)
                {
                    return this.convertBack1(value);
                }
                if (this.convertBack2 == null)
                {
                    throw new InvalidOperationException();
                }
                return this.convertBack2(value, targetTypes, parameter, culture);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DelegateConverterFactory.DelegateMultiValueConverter.<>c <>9 = new DelegateConverterFactory.DelegateMultiValueConverter.<>c();
                public static Func<object, bool> <>9__8_0;

                internal bool <Convert>b__8_0(object x) => 
                    x == DependencyProperty.UnsetValue;
            }
        }

        private class DelegateValueConverter<TIn, TOut> : IValueConverter
        {
            private readonly Func<TIn, TOut> convert1;
            private readonly Func<TOut, TIn> convertBack1;
            private readonly Func<TIn, object, CultureInfo, TOut> convert2;
            private readonly Func<TOut, object, CultureInfo, TIn> convertBack2;
            private readonly Func<TIn, Type, object, CultureInfo, TOut> convert3;
            private readonly Func<TOut, Type, object, CultureInfo, TIn> convertBack3;
            private readonly bool allowUnsetValue;

            public DelegateValueConverter(Func<TIn, TOut> convert, Func<TOut, TIn> convertBack, bool allowUnsetValue)
            {
                this.convert1 = convert;
                this.convertBack1 = convertBack;
                this.allowUnsetValue = allowUnsetValue;
            }

            public DelegateValueConverter(Func<TIn, object, CultureInfo, TOut> convert, Func<TOut, object, CultureInfo, TIn> convertBack, bool allowUnsetValue)
            {
                this.convert2 = convert;
                this.convertBack2 = convertBack;
                this.allowUnsetValue = allowUnsetValue;
            }

            public DelegateValueConverter(Func<TIn, Type, object, CultureInfo, TOut> convert, Func<TOut, Type, object, CultureInfo, TIn> convertBack, bool allowUnsetValue)
            {
                this.convert3 = convert;
                this.convertBack3 = convertBack;
                this.allowUnsetValue = allowUnsetValue;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (!this.allowUnsetValue && (value == DependencyProperty.UnsetValue))
                {
                    return Binding.DoNothing;
                }
                if (this.convert1 != null)
                {
                    return this.convert1((TIn) value);
                }
                if (this.convert2 != null)
                {
                    return this.convert2((TIn) value, parameter, culture);
                }
                if (this.convert3 == null)
                {
                    throw new InvalidOperationException();
                }
                return this.convert3((TIn) value, targetType, parameter, culture);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (this.convertBack1 != null)
                {
                    return this.convertBack1((TOut) value);
                }
                if (this.convertBack2 != null)
                {
                    return this.convertBack2((TOut) value, parameter, culture);
                }
                if (this.convertBack3 == null)
                {
                    throw new InvalidOperationException();
                }
                return this.convertBack3((TOut) value, targetType, parameter, culture);
            }
        }
    }
}

