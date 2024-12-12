namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public static class GridColumnTypeHelper
    {
        public static BaseEditSettings CreateEditSettings(Type type)
        {
            TypeCode typeCode = GetTypeCode(type);
            return ((typeCode == TypeCode.Boolean) ? ((BaseEditSettings) new CheckEditSettings()) : ((typeCode == TypeCode.DateTime) ? ((BaseEditSettings) new DateEditSettings()) : ((BaseEditSettings) new TextEditSettings())));
        }

        public static TypeCode GetTypeCode(Type type) => 
            Type.GetTypeCode(ResolveType(type));

        public static bool IsNumericType(Type type) => 
            IsNumericTypeCode(GetTypeCode(type));

        public static bool IsNumericTypeCode(TypeCode typeCode) => 
            (typeCode >= TypeCode.SByte) && (typeCode <= TypeCode.Decimal);

        private static Type ResolveType(Type type) => 
            (type != null) ? (Nullable.GetUnderlyingType(type) ?? type) : type;
    }
}

