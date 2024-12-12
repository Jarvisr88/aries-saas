namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class ChangeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<Type, bool> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x.IsEnum;
            }
            if (this.TargetType.If<Type>(evaluator).ReturnSuccess<Type>())
            {
                Func<object, string> func2 = <>c.<>9__8_1;
                if (<>c.<>9__8_1 == null)
                {
                    Func<object, string> local2 = <>c.<>9__8_1;
                    func2 = <>c.<>9__8_1 = x => x.ToString();
                }
                return Enum.Parse(this.TargetType, value.With<object, string>(func2));
            }
            Type type1 = this.TargetType;
            Type conversionType = type1;
            if (type1 == null)
            {
                Type local3 = type1;
                conversionType = typeof(object);
            }
            return System.Convert.ChangeType(value, conversionType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<Type, bool> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.IsEnum;
            }
            if (this.SourceType.If<Type>(evaluator).ReturnSuccess<Type>())
            {
                Func<object, string> func2 = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Func<object, string> local2 = <>c.<>9__9_1;
                    func2 = <>c.<>9__9_1 = x => x.ToString();
                }
                return Enum.Parse(this.SourceType, value.With<object, string>(func2));
            }
            Type sourceType = this.SourceType;
            Type conversionType = sourceType;
            if (sourceType == null)
            {
                Type local3 = sourceType;
                conversionType = typeof(object);
            }
            return System.Convert.ChangeType(value, conversionType);
        }

        public Type TargetType { get; set; }

        public Type SourceType { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ChangeTypeConverter.<>c <>9 = new ChangeTypeConverter.<>c();
            public static Func<Type, bool> <>9__8_0;
            public static Func<object, string> <>9__8_1;
            public static Func<Type, bool> <>9__9_0;
            public static Func<object, string> <>9__9_1;

            internal bool <Convert>b__8_0(Type x) => 
                x.IsEnum;

            internal string <Convert>b__8_1(object x) => 
                x.ToString();

            internal bool <ConvertBack>b__9_0(Type x) => 
                x.IsEnum;

            internal string <ConvertBack>b__9_1(object x) => 
                x.ToString();
        }
    }
}

