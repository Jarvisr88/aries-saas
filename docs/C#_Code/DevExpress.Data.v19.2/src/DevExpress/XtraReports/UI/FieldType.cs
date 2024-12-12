namespace DevExpress.XtraReports.UI
{
    using DevExpress.Data;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum FieldType
    {
        public const FieldType None = FieldType.None;,
        public const FieldType String = FieldType.String;,
        public const FieldType DateTime = FieldType.DateTime;,
        public const FieldType TimeSpan = FieldType.TimeSpan;,
        public const FieldType Byte = FieldType.Byte;,
        public const FieldType Int16 = FieldType.Int16;,
        public const FieldType Int32 = FieldType.Int32;,
        public const FieldType Float = FieldType.Float;,
        public const FieldType Double = FieldType.Double;,
        public const FieldType Decimal = FieldType.Decimal;,
        public const FieldType Boolean = FieldType.Boolean;,
        public const FieldType Guid = FieldType.Guid;
    }
}

