namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native.Navigation;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class SearchDataToLocationTextConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SearchData data = value as SearchData;
            if (data == null)
            {
                return null;
            }
            Func<BookmarkNode, string> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<BookmarkNode, string> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.Text;
            }
            return string.Format(PrintingLocalizer.GetString(PrintingStringId.NavigationPane_SearchResultHint), data.PageIndex + 1, data.BookmarkNode.Return<BookmarkNode, string>(evaluator, <>c.<>9__1_1 ??= () => ""));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchDataToLocationTextConverter.<>c <>9 = new SearchDataToLocationTextConverter.<>c();
            public static Func<BookmarkNode, string> <>9__1_0;
            public static Func<string> <>9__1_1;

            internal string <System.Windows.Data.IValueConverter.Convert>b__1_0(BookmarkNode x) => 
                x.Text;

            internal string <System.Windows.Data.IValueConverter.Convert>b__1_1() => 
                "";
        }
    }
}

