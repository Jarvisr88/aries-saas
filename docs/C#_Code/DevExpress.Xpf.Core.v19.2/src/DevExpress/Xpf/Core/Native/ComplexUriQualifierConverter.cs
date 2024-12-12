namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal class ComplexUriQualifierConverter : IValueConverter, IMultiValueConverter
    {
        private Uri uri;
        private Func<ICollection<UriInfo>> uriCandidates;

        public ComplexUriQualifierConverter(Func<ICollection<UriInfo>> uriCandidates, Uri uri);
        private object Convert(DependencyObject instance, object value, Type targetType, object parameter, CultureInfo culture);
        private double GetScore(DependencyObject dObj, UriInfo qualifierInfo, Dictionary<IBaseUriQualifier, List<string>> qualifiersAndValues);
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture);
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ComplexUriQualifierConverter.<>c <>9;
            public static Func<QualifierListener, DependencyObject> <>9__3_0;
            public static Func<Tuple<double, Uri>, bool> <>9__3_2;
            public static Func<Tuple<double, Uri>, double> <>9__3_3;
            public static Func<Tuple<double, Uri>, Uri> <>9__3_4;
            public static Func<Tuple<int, int>, int> <>9__4_1;

            static <>c();
            internal DependencyObject <Convert>b__3_0(QualifierListener x);
            internal bool <Convert>b__3_2(Tuple<double, Uri> x);
            internal double <Convert>b__3_3(Tuple<double, Uri> x);
            internal Uri <Convert>b__3_4(Tuple<double, Uri> x);
            internal int <GetScore>b__4_1(Tuple<int, int> x);
        }
    }
}

