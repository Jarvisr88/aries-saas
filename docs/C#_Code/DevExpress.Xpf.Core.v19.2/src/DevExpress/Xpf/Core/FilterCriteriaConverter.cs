namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class FilterCriteriaConverter : TypeConverter
    {
        [DebuggerHidden, CompilerGenerated]
        private object <>n__0(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            base.ConvertFrom(context, culture, value);

        [DebuggerHidden, CompilerGenerated]
        private object <>n__1(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            base.ConvertTo(context, culture, value, destinationType);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Func<string, object> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string, object> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => CriteriaOperator.Parse(x, new object[0]);
            }
            return (value as string).Return<string, object>(evaluator, () => this.<>n__0(context, culture, value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Func<CriteriaOperator, object> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<CriteriaOperator, object> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.ToString();
            }
            return (value as CriteriaOperator).Return<CriteriaOperator, object>(evaluator, () => this.<>n__1(context, culture, value, destinationType));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterCriteriaConverter.<>c <>9 = new FilterCriteriaConverter.<>c();
            public static Func<string, object> <>9__2_0;
            public static Func<CriteriaOperator, object> <>9__3_0;

            internal object <ConvertFrom>b__2_0(string x) => 
                CriteriaOperator.Parse(x, new object[0]);

            internal object <ConvertTo>b__3_0(CriteriaOperator x) => 
                x.ToString();
        }
    }
}

