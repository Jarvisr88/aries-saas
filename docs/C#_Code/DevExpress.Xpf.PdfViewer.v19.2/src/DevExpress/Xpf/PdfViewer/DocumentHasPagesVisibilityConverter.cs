namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class DocumentHasPagesVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<IEnumerable<PdfPageViewModel>, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IEnumerable<PdfPageViewModel>, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => !x.Any<PdfPageViewModel>();
            }
            return (((IEnumerable<PdfPageViewModel>) evaluator).Return<IEnumerable<PdfPageViewModel>, bool>(evaluator, (<>c.<>9__0_1 ??= () => false)) ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentHasPagesVisibilityConverter.<>c <>9 = new DocumentHasPagesVisibilityConverter.<>c();
            public static Func<IEnumerable<PdfPageViewModel>, bool> <>9__0_0;
            public static Func<bool> <>9__0_1;

            internal bool <Convert>b__0_0(IEnumerable<PdfPageViewModel> x) => 
                !x.Any<PdfPageViewModel>();

            internal bool <Convert>b__0_1() => 
                false;
        }
    }
}

