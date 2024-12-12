namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class TypeConverterBuilderBase<T, TProperty, TParentBuilder, TSelf> : NestedBuilderBase<TypeConverterWrapperAttribute, TSelf, TParentBuilder> where TParentBuilder: IAttributeBuilderInternal where TSelf: TypeConverterBuilderBase<T, TProperty, TParentBuilder, TSelf>
    {
        internal TypeConverterBuilderBase(TParentBuilder parent) : base(parent)
        {
        }

        public TSelf ConvertFromNullRule(Func<TProperty> convertRule) => 
            this.ConvertFromNullRule((culture, context) => convertRule());

        public TSelf ConvertFromNullRule(Func<CultureInfo, TProperty> convertRule) => 
            this.ConvertFromNullRule((culture, context) => convertRule(culture));

        public TSelf ConvertFromNullRule(Func<CultureInfo, T, TProperty> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<CultureInfo, object, object> <>9__1;
                Func<CultureInfo, object, object> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<CultureInfo, object, object> local1 = <>9__1;
                    func2 = <>9__1 = (culture, context) => convertRule(culture, (T) context);
                }
                x.ConvertFromNullRule = func2;
            });

        public TSelf ConvertFromRule<TSource>(Func<TSource, TProperty> convertRule) => 
            this.ConvertFromRule<TSource>((Func<TSource, CultureInfo, T, TProperty>) ((value, culture, context) => convertRule(value)));

        public TSelf ConvertFromRule<TSource>(Func<TSource, CultureInfo, TProperty> convertRule) => 
            this.ConvertFromRule<TSource>((Func<TSource, CultureInfo, T, TProperty>) ((value, culture, context) => convertRule(value, culture)));

        public TSelf ConvertFromRule<TSource>(Func<TSource, CultureInfo, T, TProperty> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                x.AddConvertFromRule<T, TProperty, TSource>((Func<TSource, CultureInfo, T, TProperty>) convertRule);
            });

        public TSelf ConvertToRule<TDestination>(Func<TProperty, TDestination> convertRule) => 
            this.ConvertToRule<TDestination>((Func<TProperty, CultureInfo, T, TDestination>) ((value, culture, context) => convertRule(value)));

        public TSelf ConvertToRule<TDestination>(Func<TProperty, CultureInfo, TDestination> convertRule) => 
            this.ConvertToRule<TDestination>((Func<TProperty, CultureInfo, T, TDestination>) ((value, culture, context) => convertRule(value, culture)));

        public TSelf ConvertToRule<TDestination>(Func<TProperty, CultureInfo, T, TDestination> convertRule) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                x.AddConvertToRule<T, TProperty, TDestination>((Func<TProperty, CultureInfo, T, TDestination>) convertRule);
            });

        public TParentBuilder EndTypeConverter() => 
            base.EndCore();

        public TSelf PropertiesProvider(Func<IEnumerable<PropertyDescriptor>> provider) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                x.PropertiesProvider = provider;
            });

        public TSelf StandardValuesProvider(Func<IEnumerable<TProperty>> provider, bool? standardValuesExclusive = new bool?()) => 
            this.StandardValuesProvider(context => provider(), standardValuesExclusive);

        public TSelf StandardValuesProvider(Func<T, IEnumerable<TProperty>> provider, bool? standardValuesExclusive = new bool?()) => 
            base.ChangeAttribute(delegate (TypeConverterWrapperAttribute x) {
                Func<object, IEnumerable<object>> <>9__1;
                Func<object, IEnumerable<object>> func2 = <>9__1;
                if (<>9__1 == null)
                {
                    Func<object, IEnumerable<object>> local1 = <>9__1;
                    func2 = <>9__1 = context => provider((T) context).Cast<object>();
                }
                x.StandardValuesProvider = func2;
                x.StandardValuesExclusive = standardValuesExclusive;
            });
    }
}

