namespace DevExpress.Mvvm.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public static class TypeConverterWrapperAttributeExtensions
    {
        internal static object GetInstance(this ITypeDescriptorContext context)
        {
            Func<ITypeDescriptorContext, object> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<ITypeDescriptorContext, object> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.Instance;
            }
            return context.With<ITypeDescriptorContext, object>(evaluator);
        }

        public static TypeConverter WrapTypeConverter(this TypeConverterWrapperAttribute wrapper, TypeConverter baseConverter) => 
            wrapper.Return<TypeConverterWrapperAttribute, TypeConverter>(x => new TypeConverterWrapper(wrapper, (x.BaseConverterType == null) ? baseConverter : ((TypeConverter) Activator.CreateInstance(x.BaseConverterType))), () => baseConverter);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeConverterWrapperAttributeExtensions.<>c <>9 = new TypeConverterWrapperAttributeExtensions.<>c();
            public static Func<ITypeDescriptorContext, object> <>9__1_0;

            internal object <GetInstance>b__1_0(ITypeDescriptorContext x) => 
                x.Instance;
        }
    }
}

