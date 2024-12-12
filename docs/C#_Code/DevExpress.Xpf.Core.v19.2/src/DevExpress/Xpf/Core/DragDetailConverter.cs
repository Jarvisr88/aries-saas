namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DragDetailConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<IList<object>, object> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<IList<object>, object> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => (x.Count == 1) ? x.First<object>() : null;
            }
            return (value as IList<object>).With<IList<object>, object>(evaluator);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragDetailConverter.<>c <>9 = new DragDetailConverter.<>c();
            public static Func<IList<object>, object> <>9__0_0;

            internal object <Convert>b__0_0(IList<object> x) => 
                (x.Count == 1) ? x.First<object>() : null;
        }
    }
}

