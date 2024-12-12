namespace DevExpress.Data
{
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(UnboundColumnTypeConverter)), ResourceFinder(typeof(ResFinder))]
    public enum UnboundColumnType
    {
        public const UnboundColumnType Bound = UnboundColumnType.Bound;,
        public const UnboundColumnType Integer = UnboundColumnType.Integer;,
        public const UnboundColumnType Decimal = UnboundColumnType.Decimal;,
        public const UnboundColumnType DateTime = UnboundColumnType.DateTime;,
        public const UnboundColumnType String = UnboundColumnType.String;,
        public const UnboundColumnType Boolean = UnboundColumnType.Boolean;,
        public const UnboundColumnType Object = UnboundColumnType.Object;
    }
}

