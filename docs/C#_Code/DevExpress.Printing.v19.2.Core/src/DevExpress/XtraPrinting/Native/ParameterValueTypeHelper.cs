namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ParameterValueTypeHelper
    {
        private static Type FindRegisteredContract(string fullTypeName);
        private static Type GetGenericTypeForMultivalueParameter(IEnumerable value);
        private static Type GetType<T>(string fullTypeName);
        private static Type GetTypeByName(string fullTypeName);
        public static Type GetValueType(object value, bool multiValue, string fullTypeName = null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ParameterValueTypeHelper.<>c <>9;
            public static Func<object, Type> <>9__1_0;
            public static Func<Type, bool> <>9__1_1;
            public static Func<Type, bool> <>9__1_2;
            public static Func<Type, Type> <>9__1_3;

            static <>c();
            internal Type <GetGenericTypeForMultivalueParameter>b__1_0(object x);
            internal bool <GetGenericTypeForMultivalueParameter>b__1_1(Type x);
            internal bool <GetGenericTypeForMultivalueParameter>b__1_2(Type x);
            internal Type <GetGenericTypeForMultivalueParameter>b__1_3(Type x);
        }
    }
}

