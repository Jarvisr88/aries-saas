namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ConvertHelper
    {
        public static readonly ConvertHelper Instance;
        private static readonly Dictionary<Type, Func<object, object>> simpleConvertActions;

        static ConvertHelper();
        protected ConvertHelper();
        internal static object GetDefaultValue(Type type);
        public object GetNativeValue(string s);
        public static object SimpleConvertValue(Type toType, object value);
        internal static object ToCodeType(IConvertible obj, object defaultValue);

        protected virtual ConvertHelper.SimpleConverter Converter { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConvertHelper.<>c <>9;

            static <>c();
            internal object <.cctor>b__10_0(object value);
            internal object <.cctor>b__10_1(object value);
            internal object <.cctor>b__10_10(object value);
            internal object <.cctor>b__10_11(object value);
            internal object <.cctor>b__10_2(object value);
            internal object <.cctor>b__10_3(object value);
            internal object <.cctor>b__10_4(object value);
            internal object <.cctor>b__10_5(object value);
            internal object <.cctor>b__10_6(object value);
            internal object <.cctor>b__10_7(object value);
            internal object <.cctor>b__10_8(object value);
            internal object <.cctor>b__10_9(object value);
        }

        private class BooleanConverter : ConvertHelper.SimpleConverter
        {
            private static ConvertHelper.SimpleConverter instance;

            public override bool Convert(string s, out object result);

            public static ConvertHelper.SimpleConverter Instance { get; }
        }

        private class DoubleConverter : ConvertHelper.SimpleConverter
        {
            private static ConvertHelper.SimpleConverter instance;

            public override bool Convert(string s, out object result);

            public static ConvertHelper.SimpleConverter Instance { get; }
        }

        private class Int32Converter : ConvertHelper.SimpleConverter
        {
            private static ConvertHelper.SimpleConverter instance;

            public override bool Convert(string s, out object result);

            public static ConvertHelper.SimpleConverter Instance { get; }
        }

        protected class SimpleConverter
        {
            private static ConvertHelper.SimpleConverter instance;

            public virtual bool Convert(string s, out object result);

            public static ConvertHelper.SimpleConverter Instance { get; }
        }
    }
}

