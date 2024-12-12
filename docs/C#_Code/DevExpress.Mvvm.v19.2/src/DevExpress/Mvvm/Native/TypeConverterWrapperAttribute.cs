namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class TypeConverterWrapperAttribute : Attribute
    {
        private readonly Dictionary<Type, Func<object, CultureInfo, object, object>> convertToRules = new Dictionary<Type, Func<object, CultureInfo, object, object>>();
        private readonly Dictionary<Type, Func<object, CultureInfo, object, object>> convertFromRules = new Dictionary<Type, Func<object, CultureInfo, object, object>>();

        public void AddConvertFromRule<T, TProperty, TSource>(Func<TSource, CultureInfo, T, TProperty> convertRule)
        {
            this.convertFromRules[typeof(TSource)] = (value, culture, context) => convertRule((TSource) value, culture, (T) context);
        }

        public void AddConvertToRule<T, TProperty, TDestination>(Func<TProperty, CultureInfo, T, TDestination> convertRule)
        {
            this.convertToRules[typeof(TDestination)] = (value, culture, context) => convertRule((TProperty) value, culture, (T) context);
        }

        public Func<object, CultureInfo, object, object> GetConvertFromRule(Type sourceType) => 
            this.convertFromRules.GetValueOrDefault<Type, Func<object, CultureInfo, object, object>>(sourceType);

        public Func<object, CultureInfo, object, object> GetConvertToRule(Type destinationType) => 
            this.convertToRules.GetValueOrDefault<Type, Func<object, CultureInfo, object, object>>(destinationType);

        public Func<CultureInfo, object, object> ConvertFromNullRule { get; set; }

        public Func<object, IEnumerable<object>> StandardValuesProvider { get; set; }

        public Func<IEnumerable<PropertyDescriptor>> PropertiesProvider { get; set; }

        public bool? StandardValuesExclusive { get; set; }

        public Type BaseConverterType { get; set; }
    }
}

