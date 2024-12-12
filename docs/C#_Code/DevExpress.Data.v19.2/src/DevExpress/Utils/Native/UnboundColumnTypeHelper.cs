namespace DevExpress.Utils.Native
{
    using DevExpress.Data;
    using System;

    public static class UnboundColumnTypeHelper
    {
        public static UnboundColumnType TypeToUnboundColumnType(Type columnType)
        {
            if ((columnType == null) || !columnType.IsEnum)
            {
                switch (Type.GetTypeCode(columnType))
                {
                    case TypeCode.Empty:
                    case TypeCode.Object:
                    case TypeCode.DBNull:
                        return UnboundColumnType.Object;

                    case TypeCode.Boolean:
                        return UnboundColumnType.Boolean;

                    case TypeCode.Char:
                    case TypeCode.String:
                        return UnboundColumnType.String;

                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        return UnboundColumnType.Integer;

                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        return UnboundColumnType.Decimal;

                    case TypeCode.DateTime:
                        return UnboundColumnType.DateTime;
                }
            }
            return UnboundColumnType.Object;
        }
    }
}

