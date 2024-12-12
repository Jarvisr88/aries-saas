namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public static class ValueDataExtensions
    {
        public static bool IsCompatibleResult<T>(this ValueData valueData) where T: struct;
        public static bool IsLocalDateTimeFunction(this FunctionOperatorType functionOperatorType);
        public static bool IsValueOfType<T>(this ValueData valueData, bool fallbackValue);
        public static CriteriaOperator ToCriteria(this ValueData valueData);
        public static object ToValue(this ValueData valueData);
        public static T? TryCast<T>(object value) where T: struct;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ValueDataExtensions.<>c <>9;
            public static Func<object, CriteriaOperator> <>9__0_0;
            public static Func<object, object> <>9__2_0;
            public static Func<object> <>9__2_1;

            static <>c();
            internal CriteriaOperator <ToCriteria>b__0_0(object value);
            internal object <ToValue>b__2_0(object value);
            internal object <ToValue>b__2_1();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T>
        {
            public static readonly ValueDataExtensions.<>c__1<T> <>9;
            public static Func<object, bool> <>9__1_0;

            static <>c__1();
            internal bool <IsValueOfType>b__1_0(object value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<T> where T: struct
        {
            public static readonly ValueDataExtensions.<>c__4<T> <>9;
            public static Func<object, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;

            static <>c__4();
            internal bool <IsCompatibleResult>b__4_0(object value);
            internal bool <IsCompatibleResult>b__4_1();
        }
    }
}

