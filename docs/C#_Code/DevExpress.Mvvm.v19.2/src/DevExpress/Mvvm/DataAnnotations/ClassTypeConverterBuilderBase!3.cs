namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class ClassTypeConverterBuilderBase<T, TParentBuilder, TSelf> : NestedBuilderBase<TypeConverterWrapperAttribute, TSelf, TParentBuilder> where TParentBuilder: IAttributeBuilderInternal where TSelf: ClassTypeConverterBuilderBase<T, TParentBuilder, TSelf>
    {
        internal ClassTypeConverterBuilderBase(TParentBuilder parent) : base(parent)
        {
        }

        public TSelf ConvertFromNullRule(Func<T> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<CultureInfo, object, object> <>9__1;
                Func<CultureInfo, object, object> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<CultureInfo, object, object> local1 = <>9__1;
                    func2 = <>9__1 = (culture, context) => convertRule();
                }
                x.ConvertFromNullRule = func2;
            });

        public TSelf ConvertFromNullRule(Func<CultureInfo, T> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<CultureInfo, object, object> <>9__1;
                Func<CultureInfo, object, object> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<CultureInfo, object, object> local1 = <>9__1;
                    func2 = <>9__1 = (culture, context) => convertRule(culture);
                }
                x.ConvertFromNullRule = func2;
            });

        public TSelf ConvertFromRule<TSource>(Func<TSource, T> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<TSource, CultureInfo, object, T> <>9__1;
                Func<TSource, CultureInfo, object, T> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<TSource, CultureInfo, object, T> local1 = <>9__1;
                    func2 = <>9__1 = (value, cultureInfo, context) => convertRule(value);
                }
                x.AddConvertFromRule<object, T, TSource>(func2);
            });

        public TSelf ConvertFromRule<TSource>(Func<TSource, CultureInfo, T> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<TSource, CultureInfo, object, T> <>9__1;
                Func<TSource, CultureInfo, object, T> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<TSource, CultureInfo, object, T> local1 = <>9__1;
                    func2 = <>9__1 = (value, cultureInfo, context) => convertRule(value, cultureInfo);
                }
                x.AddConvertFromRule<object, T, TSource>(func2);
            });

        public TSelf ConvertToRule<TDestination>(Func<T, TDestination> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<T, CultureInfo, object, TDestination> <>9__1;
                Func<T, CultureInfo, object, TDestination> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<T, CultureInfo, object, TDestination> local1 = <>9__1;
                    func2 = <>9__1 = (Func<T, CultureInfo, object, TDestination>) ((value, cultureInfo, context) => convertRule(value));
                }
                x.AddConvertToRule<object, T, TDestination>(func2);
            });

        public TSelf ConvertToRule<TDestination>(Func<T, CultureInfo, TDestination> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<T, CultureInfo, object, TDestination> <>9__1;
                Func<T, CultureInfo, object, TDestination> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<T, CultureInfo, object, TDestination> local1 = <>9__1;
                    func2 = <>9__1 = (Func<T, CultureInfo, object, TDestination>) ((value, cultureInfo, context) => convertRule(value, cultureInfo));
                }
                x.AddConvertToRule<object, T, TDestination>(func2);
            });

        public TParentBuilder EndTypeConverter() => 
            base.EndCore();

        public TSelf PropertiesProvider(Func<IEnumerable<PropertyDescriptor>> provider) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                x.PropertiesProvider = provider;
            });

        public TSelf StandardValuesProvider(Func<IEnumerable<T>> provider, bool? standardValuesExclusive = new bool?()) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<object, IEnumerable<object>> <>9__1;
                Func<object, IEnumerable<object>> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<object, IEnumerable<object>> local1 = <>9__1;
                    func2 = <>9__1 = context => provider().Cast<object>();
                }
                x.StandardValuesProvider = func2;
                x.StandardValuesExclusive = standardValuesExclusive;
            });
    }
}

