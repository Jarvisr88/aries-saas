namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DragHeaderConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<IList<object>, int> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IList<object>, int> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Count;
            }
            int num = (value as IList<object>).Return<IList<object>, int>(evaluator, <>c.<>9__0_1 ??= () => 0);
            return ((num == 1) ? GetLocalizedString(EditorStringId.DragDropOneRecord) : string.Format(GetLocalizedString(EditorStringId.DragDropMultipleRecords), num));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string GetLocalizedString(EditorStringId id) => 
            EditorLocalizer.GetString(id);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragHeaderConverter.<>c <>9 = new DragHeaderConverter.<>c();
            public static Func<IList<object>, int> <>9__0_0;
            public static Func<int> <>9__0_1;

            internal int <Convert>b__0_0(IList<object> x) => 
                x.Count;

            internal int <Convert>b__0_1() => 
                0;
        }
    }
}

