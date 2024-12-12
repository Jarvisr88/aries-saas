namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class PropertyMetadataBuilderExtensions
    {
        public static PropertyMetadataBuilder<T, string> CreditCardDataType<T>(this PropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<CreditCardAttribute>(new CreditCardAttribute(errorMessageAccessor));

        public static PropertyMetadataBuilder<T, byte> CurrencyDataType<T>(this PropertyMetadataBuilder<T, byte> builder) => 
            builder.CurrencyDataTypeCore<T, byte>();

        public static PropertyMetadataBuilder<T, decimal> CurrencyDataType<T>(this PropertyMetadataBuilder<T, decimal> builder) => 
            builder.CurrencyDataTypeCore<T, decimal>();

        public static PropertyMetadataBuilder<T, double> CurrencyDataType<T>(this PropertyMetadataBuilder<T, double> builder) => 
            builder.CurrencyDataTypeCore<T, double>();

        public static PropertyMetadataBuilder<T, short> CurrencyDataType<T>(this PropertyMetadataBuilder<T, short> builder) => 
            builder.CurrencyDataTypeCore<T, short>();

        public static PropertyMetadataBuilder<T, int> CurrencyDataType<T>(this PropertyMetadataBuilder<T, int> builder) => 
            builder.CurrencyDataTypeCore<T, int>();

        public static PropertyMetadataBuilder<T, long> CurrencyDataType<T>(this PropertyMetadataBuilder<T, long> builder) => 
            builder.CurrencyDataTypeCore<T, long>();

        public static PropertyMetadataBuilder<T, decimal?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, decimal?> builder) => 
            builder.CurrencyDataTypeCore<T, decimal?>();

        public static PropertyMetadataBuilder<T, double?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, double?> builder) => 
            builder.CurrencyDataTypeCore<T, double?>();

        public static PropertyMetadataBuilder<T, short?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, short?> builder) => 
            builder.CurrencyDataTypeCore<T, short?>();

        public static PropertyMetadataBuilder<T, int?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, int?> builder) => 
            builder.CurrencyDataTypeCore<T, int?>();

        public static PropertyMetadataBuilder<T, long?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, long?> builder) => 
            builder.CurrencyDataTypeCore<T, long?>();

        public static PropertyMetadataBuilder<T, float?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, float?> builder) => 
            builder.CurrencyDataTypeCore<T, float?>();

        public static PropertyMetadataBuilder<T, float> CurrencyDataType<T>(this PropertyMetadataBuilder<T, float> builder) => 
            builder.CurrencyDataTypeCore<T, float>();

        internal static PropertyMetadataBuilder<T, TProperty> CurrencyDataTypeCore<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder) => 
            builder.SetDataTypeCore<T, TProperty>(PropertyDataType.Currency);

        public static PropertyMetadataBuilder<T, System.DateTime> DateTimeDataType<T>(this PropertyMetadataBuilder<T, System.DateTime> builder, DateTimeDisplayMode displayMode = 0) => 
            builder.SetDataTypeCore<T, System.DateTime>(GetDataTypeByDateTimeDisplayMode(displayMode));

        public static PropertyMetadataBuilder<T, System.DateTime?> DateTimeDataType<T>(this PropertyMetadataBuilder<T, System.DateTime?> builder, DateTimeDisplayMode displayMode = 0) => 
            builder.SetDataTypeCore<T, System.DateTime?>(GetDataTypeByDateTimeDisplayMode(displayMode));

        public static DateTimeMaskBuilder<T, PropertyMetadataBuilder<T, System.DateTime>> DateTimeMask<T>(this PropertyMetadataBuilder<T, System.DateTime> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            new DateTimeMaskBuilder<T, PropertyMetadataBuilder<T, System.DateTime>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static PropertyMetadataBuilder<T, string> EmailAddressDataType<T>(this PropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<EmailAddressAttribute>(new EmailAddressAttribute(errorMessageAccessor));

        public static PropertyMetadataBuilder<T, byte> EnumDataType<T>(this PropertyMetadataBuilder<T, byte> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, byte>(enumType);

        public static PropertyMetadataBuilder<T, short> EnumDataType<T>(this PropertyMetadataBuilder<T, short> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, short>(enumType);

        public static PropertyMetadataBuilder<T, int> EnumDataType<T>(this PropertyMetadataBuilder<T, int> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, int>(enumType);

        public static PropertyMetadataBuilder<T, long> EnumDataType<T>(this PropertyMetadataBuilder<T, long> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, long>(enumType);

        public static PropertyMetadataBuilder<T, byte?> EnumDataType<T>(this PropertyMetadataBuilder<T, byte?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, byte?>(enumType);

        public static PropertyMetadataBuilder<T, short?> EnumDataType<T>(this PropertyMetadataBuilder<T, short?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, short?>(enumType);

        public static PropertyMetadataBuilder<T, int?> EnumDataType<T>(this PropertyMetadataBuilder<T, int?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, int?>(enumType);

        public static PropertyMetadataBuilder<T, long?> EnumDataType<T>(this PropertyMetadataBuilder<T, long?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, long?>(enumType);

        internal static PropertyMetadataBuilder<T, TProperty> EnumDataTypeCore<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder, Type enumType) => 
            DataAnnotationsAttributeHelper.SetEnumDataTypeCore<PropertyMetadataBuilder<T, TProperty>>(builder, enumType);

        private static PropertyDataType GetDataTypeByDateTimeDisplayMode(DateTimeDisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DateTimeDisplayMode.Date:
                    return PropertyDataType.Date;

                case DateTimeDisplayMode.Time:
                    return PropertyDataType.Time;

                case DateTimeDisplayMode.DateTime:
                    return PropertyDataType.DateTime;
            }
            throw new NotSupportedException();
        }

        public static PropertyMetadataBuilder<T, string> ImageUrlDataType<T>(this PropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.ImageUrl);

        public static PropertyMetadataBuilder<T, TProperty> InRange<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder, TProperty minimum, TProperty maximum, Func<string> errorMessageAccessor = null) where TProperty: IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor(errorMessageAccessor)));

        public static PropertyMetadataBuilder<T, TProperty?> InRange<T, TProperty>(this PropertyMetadataBuilder<T, TProperty?> builder, TProperty? minimum, TProperty? maximum, Func<string> errorMessageAccessor = null) where TProperty: struct, IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor(errorMessageAccessor)));

        public static PropertyMetadataBuilder<T, TProperty> InRange<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder, TProperty minimum, TProperty maximum, Func<TProperty, string> errorMessageAccessor) where TProperty: IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor)));

        public static PropertyMetadataBuilder<T, TProperty?> InRange<T, TProperty>(this PropertyMetadataBuilder<T, TProperty?> builder, TProperty? minimum, TProperty? maximum, Func<TProperty, string> errorMessageAccessor) where TProperty: struct, IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor)));

        public static PropertyMetadataBuilder<T, string> MultilineTextDataType<T>(this PropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.MultilineText);

        public static PropertyMetadataBuilder<T, ICollection<TItem>> NewItemInitializer<T, TItem, TValue>(this PropertyMetadataBuilder<T, ICollection<TItem>> builder, Func<TValue> createDelegate, string name = null)
        {
            Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> attributeFactory = <>c__57<T, TItem, TValue>.<>9__57_0;
            if (<>c__57<T, TItem, TValue>.<>9__57_0 == null)
            {
                Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> local1 = <>c__57<T, TItem, TValue>.<>9__57_0;
                attributeFactory = <>c__57<T, TItem, TValue>.<>9__57_0 = (t, n, c) => new NewItemInstanceInitializerAttribute(t, n, c);
            }
            return builder.InitializerCore<TValue, NewItemInstanceInitializerAttribute>(createDelegate, name, attributeFactory);
        }

        public static PropertyMetadataBuilder<T, IDictionary<TKey, TValue>> NewItemInitializer<T, TKey, TValue, TNewItem>(this PropertyMetadataBuilder<T, IDictionary<TKey, TValue>> builder, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TNewItem>> createDelegate, string name = null) where TNewItem: TValue
        {
            Func<Type, string, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, NewItemInstanceInitializerAttribute> attributeFactory = <>c__58<T, TKey, TValue, TNewItem>.<>9__58_0;
            if (<>c__58<T, TKey, TValue, TNewItem>.<>9__58_0 == null)
            {
                Func<Type, string, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, NewItemInstanceInitializerAttribute> local1 = <>c__58<T, TKey, TValue, TNewItem>.<>9__58_0;
                attributeFactory = <>c__58<T, TKey, TValue, TNewItem>.<>9__58_0 = (t, n, c) => new NewItemInstanceInitializerAttribute(t, n, delegate (ITypeDescriptorContext context, IEnumerable dictionary) {
                    KeyValuePair<TKey, TValue> pair = c(context, (IDictionary<TKey, TValue>) dictionary);
                    return new KeyValuePair<object, object>(pair.Key, pair.Value);
                });
            }
            return builder.InitializerCore<TKey, TValue, TNewItem, NewItemInstanceInitializerAttribute>(createDelegate, name, attributeFactory);
        }

        public static PropertyMetadataBuilder<T, IDictionary> NewItemInitializer<T, TNewItem>(this PropertyMetadataBuilder<T, IDictionary> builder, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, TNewItem>> createDelegate, string name = null)
        {
            Func<Type, string, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, object>>, NewItemInstanceInitializerAttribute> attributeFactory = <>c__59<T, TNewItem>.<>9__59_0;
            if (<>c__59<T, TNewItem>.<>9__59_0 == null)
            {
                Func<Type, string, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, object>>, NewItemInstanceInitializerAttribute> local1 = <>c__59<T, TNewItem>.<>9__59_0;
                attributeFactory = <>c__59<T, TNewItem>.<>9__59_0 = (t, n, c) => new NewItemInstanceInitializerAttribute(t, n, delegate (ITypeDescriptorContext context, IEnumerable dictionary) {
                    KeyValuePair<object, object> pair = c(context, (IDictionary) dictionary);
                    return new KeyValuePair<object, object>(pair.Key, pair.Value);
                });
            }
            return builder.InitializerCore<TNewItem, NewItemInstanceInitializerAttribute>(createDelegate, name, attributeFactory);
        }

        public static NumericMaskBuilder<T, byte, PropertyMetadataBuilder<T, byte>> NumericMask<T>(this PropertyMetadataBuilder<T, byte> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, byte>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, decimal, PropertyMetadataBuilder<T, decimal>> NumericMask<T>(this PropertyMetadataBuilder<T, decimal> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, decimal>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, double, PropertyMetadataBuilder<T, double>> NumericMask<T>(this PropertyMetadataBuilder<T, double> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, double>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, short, PropertyMetadataBuilder<T, short>> NumericMask<T>(this PropertyMetadataBuilder<T, short> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, short>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, int, PropertyMetadataBuilder<T, int>> NumericMask<T>(this PropertyMetadataBuilder<T, int> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, int>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, long, PropertyMetadataBuilder<T, long>> NumericMask<T>(this PropertyMetadataBuilder<T, long> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, long>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, decimal?, PropertyMetadataBuilder<T, decimal?>> NumericMask<T>(this PropertyMetadataBuilder<T, decimal?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, decimal?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, double?, PropertyMetadataBuilder<T, double?>> NumericMask<T>(this PropertyMetadataBuilder<T, double?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, double?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, short?, PropertyMetadataBuilder<T, short?>> NumericMask<T>(this PropertyMetadataBuilder<T, short?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, short?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, int?, PropertyMetadataBuilder<T, int?>> NumericMask<T>(this PropertyMetadataBuilder<T, int?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, int?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, long?, PropertyMetadataBuilder<T, long?>> NumericMask<T>(this PropertyMetadataBuilder<T, long?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, long?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, float?, PropertyMetadataBuilder<T, float?>> NumericMask<T>(this PropertyMetadataBuilder<T, float?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, float?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, float, PropertyMetadataBuilder<T, float>> NumericMask<T>(this PropertyMetadataBuilder<T, float> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, float>(mask, useMaskAsDisplayFormat);

        internal static NumericMaskBuilder<T, TProperty, PropertyMetadataBuilder<T, TProperty>> NumericMaskOptionsCore<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder, string mask, bool useMaskAsDisplayFormat) => 
            new NumericMaskBuilder<T, TProperty, PropertyMetadataBuilder<T, TProperty>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static PropertyMetadataBuilder<T, string> PasswordDataType<T>(this PropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.Password);

        public static PropertyMetadataBuilder<T, string> PhoneNumberDataType<T>(this PropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<PhoneAttribute>(new PhoneAttribute(errorMessageAccessor));

        public static RegExMaskBuilder<T, string, PropertyMetadataBuilder<T, string>> RegExMask<T>(this PropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new RegExMaskBuilder<T, string, PropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static RegularMaskBuilder<T, string, PropertyMetadataBuilder<T, string>> RegularMask<T>(this PropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new RegularMaskBuilder<T, string, PropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static PropertyMetadataBuilder<T, IEnumerable<TProperty>> ScaffoldDetailCollection<T, TProperty>(this PropertyMetadataBuilder<T, IEnumerable<TProperty>> builder) => 
            builder.AddOrReplaceAttribute<ScaffoldDetailCollectionAttribute>(new ScaffoldDetailCollectionAttribute());

        private static PropertyMetadataBuilder<T, TProperty> SetDataTypeCore<T, TProperty>(this PropertyMetadataBuilder<T, TProperty> builder, PropertyDataType dataType) => 
            DataAnnotationsAttributeHelper.SetDataTypeCore<PropertyMetadataBuilder<T, TProperty>>(builder, dataType);

        public static SimpleMaskBuilder<T, string, PropertyMetadataBuilder<T, string>> SimpleMask<T>(this PropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new SimpleMaskBuilder<T, string, PropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static PropertyMetadataBuilder<T, string> UrlDataType<T>(this PropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<UrlAttribute>(new UrlAttribute(errorMessageAccessor));

        [Serializable, CompilerGenerated]
        private sealed class <>c__57<T, TItem, TValue>
        {
            public static readonly PropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue> <>9;
            public static Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> <>9__57_0;

            static <>c__57()
            {
                PropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue>.<>9 = new PropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue>();
            }

            internal NewItemInstanceInitializerAttribute <NewItemInitializer>b__57_0(Type t, string n, Func<object> c) => 
                new NewItemInstanceInitializerAttribute(t, n, c);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__58<T, TKey, TValue, TNewItem> where TNewItem: TValue
        {
            public static readonly PropertyMetadataBuilderExtensions.<>c__58<T, TKey, TValue, TNewItem> <>9;
            public static Func<Type, string, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, NewItemInstanceInitializerAttribute> <>9__58_0;

            static <>c__58()
            {
                PropertyMetadataBuilderExtensions.<>c__58<T, TKey, TValue, TNewItem>.<>9 = new PropertyMetadataBuilderExtensions.<>c__58<T, TKey, TValue, TNewItem>();
            }

            internal NewItemInstanceInitializerAttribute <NewItemInitializer>b__58_0(Type t, string n, Func<ITypeDescriptorContext, IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>> c) => 
                new NewItemInstanceInitializerAttribute(t, n, delegate (ITypeDescriptorContext context, IEnumerable dictionary) {
                    KeyValuePair<TKey, TValue> pair = c(context, (IDictionary<TKey, TValue>) dictionary);
                    return new KeyValuePair<object, object>(pair.Key, pair.Value);
                });
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__59<T, TNewItem>
        {
            public static readonly PropertyMetadataBuilderExtensions.<>c__59<T, TNewItem> <>9;
            public static Func<Type, string, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, object>>, NewItemInstanceInitializerAttribute> <>9__59_0;

            static <>c__59()
            {
                PropertyMetadataBuilderExtensions.<>c__59<T, TNewItem>.<>9 = new PropertyMetadataBuilderExtensions.<>c__59<T, TNewItem>();
            }

            internal NewItemInstanceInitializerAttribute <NewItemInitializer>b__59_0(Type t, string n, Func<ITypeDescriptorContext, IDictionary, KeyValuePair<object, object>> c) => 
                new NewItemInstanceInitializerAttribute(t, n, delegate (ITypeDescriptorContext context, IEnumerable dictionary) {
                    KeyValuePair<object, object> pair = c(context, (IDictionary) dictionary);
                    return new KeyValuePair<object, object>(pair.Key, pair.Value);
                });
        }
    }
}

