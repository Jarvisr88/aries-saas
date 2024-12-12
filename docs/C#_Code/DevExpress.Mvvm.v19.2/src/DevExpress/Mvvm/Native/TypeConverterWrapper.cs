namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TypeConverterWrapper : TypeConverter
    {
        private readonly TypeConverterWrapperAttribute wrapper;
        private readonly TypeConverter baseConverter;

        public TypeConverterWrapper(TypeConverterWrapperAttribute wrapper, TypeConverter baseConverter)
        {
            this.wrapper = wrapper;
            TypeConverter converter1 = baseConverter;
            if (baseConverter == null)
            {
                TypeConverter local1 = baseConverter;
                converter1 = new TypeConverter();
            }
            this.baseConverter = converter1;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (this.wrapper.GetConvertFromRule(sourceType) != null) || this.baseConverter.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (this.wrapper.GetConvertToRule(destinationType) != null) || this.baseConverter.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                if (this.wrapper.ConvertFromNullRule != null)
                {
                    return this.wrapper.ConvertFromNullRule(culture, context.GetInstance());
                }
            }
            else
            {
                Func<object, CultureInfo, object, object> convertFromRule = this.wrapper.GetConvertFromRule(value.GetType());
                if (convertFromRule != null)
                {
                    return convertFromRule(value, culture, context.GetInstance());
                }
            }
            return this.baseConverter.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            this.wrapper.GetConvertToRule(destinationType).Return<Func<object, CultureInfo, object, object>, object>(x => x(value, culture, context.GetInstance()), () => this.baseConverter.ConvertTo(context, culture, value, destinationType));

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) => 
            this.baseConverter.CreateInstance(context, propertyValues);

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => 
            this.baseConverter.GetCreateInstanceSupported(context);

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            Func<Func<IEnumerable<PropertyDescriptor>>, PropertyDescriptorCollection> evaluator = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<Func<IEnumerable<PropertyDescriptor>>, PropertyDescriptorCollection> local1 = <>c.<>9__12_0;
                evaluator = <>c.<>9__12_0 = x => new PropertyDescriptorCollection(x().ToArray<PropertyDescriptor>());
            }
            return this.wrapper.PropertiesProvider.Return<Func<IEnumerable<PropertyDescriptor>>, PropertyDescriptorCollection>(evaluator, () => this.baseConverter.GetProperties(context, value, attributes));
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
            (this.wrapper.PropertiesProvider != null) || this.baseConverter.GetPropertiesSupported(context);

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => 
            this.wrapper.StandardValuesProvider.Return<Func<object, IEnumerable<object>>, TypeConverter.StandardValuesCollection>(x => new TypeConverter.StandardValuesCollection(x(context.GetInstance()).ToArray<object>()), () => this.baseConverter.GetStandardValues(context));

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => 
            this.wrapper.StandardValuesExclusive.GetValueOrDefault(this.baseConverter.GetStandardValuesExclusive(context));

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            (this.wrapper.StandardValuesProvider != null) || this.baseConverter.GetStandardValuesSupported(context);

        public override bool IsValid(ITypeDescriptorContext context, object value) => 
            this.baseConverter.IsValid(context, value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeConverterWrapper.<>c <>9 = new TypeConverterWrapper.<>c();
            public static Func<Func<IEnumerable<PropertyDescriptor>>, PropertyDescriptorCollection> <>9__12_0;

            internal PropertyDescriptorCollection <GetProperties>b__12_0(Func<IEnumerable<PropertyDescriptor>> x) => 
                new PropertyDescriptorCollection(x().ToArray<PropertyDescriptor>());
        }
    }
}

