namespace DevExpress.Mvvm.DataAnnotations
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class FilteringPropertyMetadataBuilderExtensions
    {
        public static FilteringPropertyMetadataBuilder<T, string> CreditCardDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<CreditCardAttribute>(new CreditCardAttribute(errorMessageAccessor));

        public static FilteringPropertyMetadataBuilder<T, byte> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, byte> builder) => 
            builder.CurrencyDataTypeCore<T, byte>();

        public static FilteringPropertyMetadataBuilder<T, decimal> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, decimal> builder) => 
            builder.CurrencyDataTypeCore<T, decimal>();

        public static FilteringPropertyMetadataBuilder<T, double> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, double> builder) => 
            builder.CurrencyDataTypeCore<T, double>();

        public static FilteringPropertyMetadataBuilder<T, short> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, short> builder) => 
            builder.CurrencyDataTypeCore<T, short>();

        public static FilteringPropertyMetadataBuilder<T, int> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, int> builder) => 
            builder.CurrencyDataTypeCore<T, int>();

        public static FilteringPropertyMetadataBuilder<T, long> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, long> builder) => 
            builder.CurrencyDataTypeCore<T, long>();

        public static FilteringPropertyMetadataBuilder<T, decimal?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, decimal?> builder) => 
            builder.CurrencyDataTypeCore<T, decimal?>();

        public static FilteringPropertyMetadataBuilder<T, double?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, double?> builder) => 
            builder.CurrencyDataTypeCore<T, double?>();

        public static FilteringPropertyMetadataBuilder<T, short?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, short?> builder) => 
            builder.CurrencyDataTypeCore<T, short?>();

        public static FilteringPropertyMetadataBuilder<T, int?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, int?> builder) => 
            builder.CurrencyDataTypeCore<T, int?>();

        public static FilteringPropertyMetadataBuilder<T, long?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, long?> builder) => 
            builder.CurrencyDataTypeCore<T, long?>();

        public static FilteringPropertyMetadataBuilder<T, float?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, float?> builder) => 
            builder.CurrencyDataTypeCore<T, float?>();

        public static FilteringPropertyMetadataBuilder<T, float> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, float> builder) => 
            builder.CurrencyDataTypeCore<T, float>();

        internal static FilteringPropertyMetadataBuilder<T, TProperty> CurrencyDataTypeCore<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder) => 
            builder.SetDataTypeCore<T, TProperty>(PropertyDataType.Currency);

        public static FilteringPropertyMetadataBuilder<T, System.DateTime> DateTimeDataType<T>(this FilteringPropertyMetadataBuilder<T, System.DateTime> builder, DateTimeDisplayMode displayMode = 0) => 
            builder.SetDataTypeCore<T, System.DateTime>(GetDataTypeByDateTimeDisplayMode(displayMode));

        public static FilteringPropertyMetadataBuilder<T, System.DateTime?> DateTimeDataType<T>(this FilteringPropertyMetadataBuilder<T, System.DateTime?> builder, DateTimeDisplayMode displayMode = 0) => 
            builder.SetDataTypeCore<T, System.DateTime?>(GetDataTypeByDateTimeDisplayMode(displayMode));

        public static DateTimeMaskBuilder<T, FilteringPropertyMetadataBuilder<T, System.DateTime>> DateTimeMask<T>(this FilteringPropertyMetadataBuilder<T, System.DateTime> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            new DateTimeMaskBuilder<T, FilteringPropertyMetadataBuilder<T, System.DateTime>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static FilteringPropertyMetadataBuilder<T, string> EmailAddressDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<EmailAddressAttribute>(new EmailAddressAttribute(errorMessageAccessor));

        public static FilteringPropertyMetadataBuilder<T, byte> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, byte> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, byte>(enumType);

        public static FilteringPropertyMetadataBuilder<T, short> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, short> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, short>(enumType);

        public static FilteringPropertyMetadataBuilder<T, int> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, int> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, int>(enumType);

        public static FilteringPropertyMetadataBuilder<T, long> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, long> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, long>(enumType);

        public static FilteringPropertyMetadataBuilder<T, byte?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, byte?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, byte?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, short?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, short?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, short?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, int?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, int?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, int?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, long?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, long?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, long?>(enumType);

        internal static FilteringPropertyMetadataBuilder<T, TProperty> EnumDataTypeCore<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder, Type enumType) => 
            DataAnnotationsAttributeHelper.SetEnumDataTypeCore<FilteringPropertyMetadataBuilder<T, TProperty>>(builder, enumType);

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

        public static FilteringPropertyMetadataBuilder<T, string> ImageUrlDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.ImageUrl);

        public static FilteringPropertyMetadataBuilder<T, TProperty> InRange<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder, TProperty minimum, TProperty maximum, Func<string> errorMessageAccessor = null) where TProperty: IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor(errorMessageAccessor)));

        public static FilteringPropertyMetadataBuilder<T, TProperty?> InRange<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty?> builder, TProperty? minimum, TProperty? maximum, Func<string> errorMessageAccessor = null) where TProperty: struct, IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor(errorMessageAccessor)));

        public static FilteringPropertyMetadataBuilder<T, TProperty> InRange<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder, TProperty minimum, TProperty maximum, Func<TProperty, string> errorMessageAccessor) where TProperty: IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor)));

        public static FilteringPropertyMetadataBuilder<T, TProperty?> InRange<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty?> builder, TProperty? minimum, TProperty? maximum, Func<TProperty, string> errorMessageAccessor) where TProperty: struct, IComparable => 
            builder.AddOrReplaceAttribute<RangeAttribute>(new RangeAttribute(minimum, maximum, DXValidationAttribute.ErrorMessageAccessor<TProperty>(errorMessageAccessor)));

        public static FilteringPropertyMetadataBuilder<T, string> MultilineTextDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.MultilineText);

        public static FilteringPropertyMetadataBuilder<T, ICollection<TItem>> NewItemInitializer<T, TItem, TValue>(this FilteringPropertyMetadataBuilder<T, ICollection<TItem>> builder, Func<TValue> createDelegate, string name = null)
        {
            Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> attributeFactory = <>c__57<T, TItem, TValue>.<>9__57_0;
            if (<>c__57<T, TItem, TValue>.<>9__57_0 == null)
            {
                Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> local1 = <>c__57<T, TItem, TValue>.<>9__57_0;
                attributeFactory = <>c__57<T, TItem, TValue>.<>9__57_0 = (t, n, c) => new NewItemInstanceInitializerAttribute(t, n, c);
            }
            return builder.InitializerCore<TValue, NewItemInstanceInitializerAttribute>(createDelegate, name, attributeFactory);
        }

        public static NumericMaskBuilder<T, byte, FilteringPropertyMetadataBuilder<T, byte>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, byte> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, byte>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, decimal, FilteringPropertyMetadataBuilder<T, decimal>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, decimal> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, decimal>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, double, FilteringPropertyMetadataBuilder<T, double>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, double> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, double>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, short, FilteringPropertyMetadataBuilder<T, short>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, short> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, short>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, int, FilteringPropertyMetadataBuilder<T, int>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, int> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, int>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, long, FilteringPropertyMetadataBuilder<T, long>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, long> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, long>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, decimal?, FilteringPropertyMetadataBuilder<T, decimal?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, decimal?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, decimal?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, double?, FilteringPropertyMetadataBuilder<T, double?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, double?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, double?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, short?, FilteringPropertyMetadataBuilder<T, short?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, short?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, short?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, int?, FilteringPropertyMetadataBuilder<T, int?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, int?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, int?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, long?, FilteringPropertyMetadataBuilder<T, long?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, long?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, long?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, float?, FilteringPropertyMetadataBuilder<T, float?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, float?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, float?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, float, FilteringPropertyMetadataBuilder<T, float>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, float> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, float>(mask, useMaskAsDisplayFormat);

        internal static NumericMaskBuilder<T, TProperty, FilteringPropertyMetadataBuilder<T, TProperty>> NumericMaskOptionsCore<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder, string mask, bool useMaskAsDisplayFormat) => 
            new NumericMaskBuilder<T, TProperty, FilteringPropertyMetadataBuilder<T, TProperty>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static FilteringPropertyMetadataBuilder<T, string> PasswordDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder) => 
            builder.SetDataTypeCore<T, string>(PropertyDataType.Password);

        public static FilteringPropertyMetadataBuilder<T, string> PhoneNumberDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<PhoneAttribute>(new PhoneAttribute(errorMessageAccessor));

        public static RegExMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>> RegExMask<T>(this FilteringPropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new RegExMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static RegularMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>> RegularMask<T>(this FilteringPropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new RegularMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static FilteringPropertyMetadataBuilder<T, IEnumerable<TProperty>> ScaffoldDetailCollection<T, TProperty>(this FilteringPropertyMetadataBuilder<T, IEnumerable<TProperty>> builder) => 
            builder.AddOrReplaceAttribute<ScaffoldDetailCollectionAttribute>(new ScaffoldDetailCollectionAttribute());

        private static FilteringPropertyMetadataBuilder<T, TProperty> SetDataTypeCore<T, TProperty>(this FilteringPropertyMetadataBuilder<T, TProperty> builder, PropertyDataType dataType) => 
            DataAnnotationsAttributeHelper.SetDataTypeCore<FilteringPropertyMetadataBuilder<T, TProperty>>(builder, dataType);

        public static SimpleMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>> SimpleMask<T>(this FilteringPropertyMetadataBuilder<T, string> builder, string mask, bool useMaskAsDisplayFormat = false) => 
            new SimpleMaskBuilder<T, string, FilteringPropertyMetadataBuilder<T, string>>(builder).MaskCore(mask, useMaskAsDisplayFormat);

        public static FilteringPropertyMetadataBuilder<T, string> UrlDataType<T>(this FilteringPropertyMetadataBuilder<T, string> builder, Func<string> errorMessageAccessor = null) => 
            builder.AddOrReplaceAttribute<UrlAttribute>(new UrlAttribute(errorMessageAccessor));

        [Serializable, CompilerGenerated]
        private sealed class <>c__57<T, TItem, TValue>
        {
            public static readonly FilteringPropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue> <>9;
            public static Func<Type, string, Func<object>, NewItemInstanceInitializerAttribute> <>9__57_0;

            static <>c__57()
            {
                FilteringPropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue>.<>9 = new FilteringPropertyMetadataBuilderExtensions.<>c__57<T, TItem, TValue>();
            }

            internal NewItemInstanceInitializerAttribute <NewItemInitializer>b__57_0(Type t, string n, Func<object> c) => 
                new NewItemInstanceInitializerAttribute(t, n, c);
        }
    }
}

