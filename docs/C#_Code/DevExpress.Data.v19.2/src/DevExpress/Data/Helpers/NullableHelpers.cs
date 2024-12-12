namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Concurrent;

    public static class NullableHelpers
    {
        private static ConcurrentDictionary<Type, Type> _NullablesDict;

        static NullableHelpers();
        public static bool CanAcceptNull(Type t);
        public static Type GetBoxedType(Type t);
        public static Type GetUnBoxedType(Type t);
        public static Type GetUnderlyingType(Type maybeNullableType);
        public static bool IsNullableValueType(Type maybeNullableType);
        public static Type MakeNullableType(Type unnullableStructType);
        private static Type MakeNullableTypeCore(Type unnullableStructType);
    }
}

