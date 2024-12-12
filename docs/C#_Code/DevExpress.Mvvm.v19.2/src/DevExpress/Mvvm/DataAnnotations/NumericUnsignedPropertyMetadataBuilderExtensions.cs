namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [CLSCompliant(false)]
    public static class NumericUnsignedPropertyMetadataBuilderExtensions
    {
        public static PropertyMetadataBuilder<T, ushort?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, ushort?> builder) => 
            builder.CurrencyDataTypeCore<T, ushort?>();

        public static PropertyMetadataBuilder<T, uint?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, uint?> builder) => 
            builder.CurrencyDataTypeCore<T, uint?>();

        public static PropertyMetadataBuilder<T, ulong?> CurrencyDataType<T>(this PropertyMetadataBuilder<T, ulong?> builder) => 
            builder.CurrencyDataTypeCore<T, ulong?>();

        public static PropertyMetadataBuilder<T, ushort> CurrencyDataType<T>(this PropertyMetadataBuilder<T, ushort> builder) => 
            builder.CurrencyDataTypeCore<T, ushort>();

        public static PropertyMetadataBuilder<T, uint> CurrencyDataType<T>(this PropertyMetadataBuilder<T, uint> builder) => 
            builder.CurrencyDataTypeCore<T, uint>();

        public static PropertyMetadataBuilder<T, ulong> CurrencyDataType<T>(this PropertyMetadataBuilder<T, ulong> builder) => 
            builder.CurrencyDataTypeCore<T, ulong>();

        public static PropertyMetadataBuilder<T, ushort?> EnumDataType<T>(this PropertyMetadataBuilder<T, ushort?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ushort?>(enumType);

        public static PropertyMetadataBuilder<T, uint?> EnumDataType<T>(this PropertyMetadataBuilder<T, uint?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, uint?>(enumType);

        public static PropertyMetadataBuilder<T, ulong?> EnumDataType<T>(this PropertyMetadataBuilder<T, ulong?> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ulong?>(enumType);

        public static PropertyMetadataBuilder<T, ushort> EnumDataType<T>(this PropertyMetadataBuilder<T, ushort> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ushort>(enumType);

        public static PropertyMetadataBuilder<T, uint> EnumDataType<T>(this PropertyMetadataBuilder<T, uint> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, uint>(enumType);

        public static PropertyMetadataBuilder<T, ulong> EnumDataType<T>(this PropertyMetadataBuilder<T, ulong> builder, Type enumType) => 
            builder.EnumDataTypeCore<T, ulong>(enumType);

        public static NumericMaskBuilder<T, ushort?, PropertyMetadataBuilder<T, ushort?>> NumericMask<T>(this PropertyMetadataBuilder<T, ushort?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ushort?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, uint?, PropertyMetadataBuilder<T, uint?>> NumericMask<T>(this PropertyMetadataBuilder<T, uint?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, uint?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ulong?, PropertyMetadataBuilder<T, ulong?>> NumericMask<T>(this PropertyMetadataBuilder<T, ulong?> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ulong?>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ushort, PropertyMetadataBuilder<T, ushort>> NumericMask<T>(this PropertyMetadataBuilder<T, ushort> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ushort>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, uint, PropertyMetadataBuilder<T, uint>> NumericMask<T>(this PropertyMetadataBuilder<T, uint> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, uint>(mask, useMaskAsDisplayFormat);

        public static NumericMaskBuilder<T, ulong, PropertyMetadataBuilder<T, ulong>> NumericMask<T>(this PropertyMetadataBuilder<T, ulong> builder, string mask, bool useMaskAsDisplayFormat = true) => 
            builder.NumericMaskOptionsCore<T, ulong>(mask, useMaskAsDisplayFormat);
    }
}

