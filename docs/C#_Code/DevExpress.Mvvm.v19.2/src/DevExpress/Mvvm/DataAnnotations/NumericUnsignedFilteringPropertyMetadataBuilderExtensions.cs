namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [CLSCompliant(false)]
    public static class NumericUnsignedFilteringPropertyMetadataBuilderExtensions
    {
        public static FilteringPropertyMetadataBuilder<T, ushort?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, ushort?> builder) => 
            builder.CurrencyDataTypeCore<T, ushort?>();

        public static FilteringPropertyMetadataBuilder<T, uint?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, uint?> builder) => 
            builder.CurrencyDataTypeCore<T, uint?>();

        public static FilteringPropertyMetadataBuilder<T, ulong?> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, ulong?> builder) => 
            builder.CurrencyDataTypeCore<T, ulong?>();

        public static FilteringPropertyMetadataBuilder<T, ushort> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, ushort> builder) => 
            builder.CurrencyDataTypeCore<T, ushort>();

        public static FilteringPropertyMetadataBuilder<T, uint> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, uint> builder) => 
            builder.CurrencyDataTypeCore<T, uint>();

        public static FilteringPropertyMetadataBuilder<T, ulong> CurrencyDataType<T>(this FilteringPropertyMetadataBuilder<T, ulong> builder) => 
            builder.CurrencyDataTypeCore<T, ulong>();

        public static FilteringPropertyMetadataBuilder<T, ushort?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, ushort?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ushort?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, uint?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, uint?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, uint?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, ulong?> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, ulong?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ulong?>(enumType);

        public static FilteringPropertyMetadataBuilder<T, ushort> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, ushort> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ushort>(enumType);

        public static FilteringPropertyMetadataBuilder<T, uint> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, uint> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, uint>(enumType);

        public static FilteringPropertyMetadataBuilder<T, ulong> EnumDataType<T>(this FilteringPropertyMetadataBuilder<T, ulong> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ulong>(enumType);

        public static NumericMaskBuilder<T, ushort?, FilteringPropertyMetadataBuilder<T, ushort?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, ushort?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ushort?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, uint?, FilteringPropertyMetadataBuilder<T, uint?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, uint?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, uint?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ulong?, FilteringPropertyMetadataBuilder<T, ulong?>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, ulong?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ulong?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ushort, FilteringPropertyMetadataBuilder<T, ushort>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, ushort> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ushort>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, uint, FilteringPropertyMetadataBuilder<T, uint>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, uint> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, uint>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ulong, FilteringPropertyMetadataBuilder<T, ulong>> NumericMask<T>(this FilteringPropertyMetadataBuilder<T, ulong> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ulong>(mask, useMaskAsDisplayFormat);
    }
}

