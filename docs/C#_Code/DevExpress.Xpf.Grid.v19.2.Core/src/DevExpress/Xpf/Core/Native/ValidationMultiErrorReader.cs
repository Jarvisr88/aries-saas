namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class ValidationMultiErrorReader
    {
        private static T MatchErrorContent<T>(object obj, Func<object, T> fallback, Func<string, T> stringMatcher = null, Func<ErrorInfo, T> errorInfoMatcher = null)
        {
            if (stringMatcher != null)
            {
                string arg = obj as string;
                if (arg != null)
                {
                    return stringMatcher(arg);
                }
            }
            if (errorInfoMatcher != null)
            {
                ErrorInfo arg = obj as ErrorInfo;
                if (arg != null)
                {
                    return errorInfoMatcher(arg);
                }
            }
            return fallback(obj);
        }

        public static MultiErrorInfo Read<T>(T[] errorContainers) where T: class
        {
            Func<T, object> containerExtractor = <>c__0<T>.<>9__0_0;
            if (<>c__0<T>.<>9__0_0 == null)
            {
                Func<T, object> local1 = <>c__0<T>.<>9__0_0;
                containerExtractor = <>c__0<T>.<>9__0_0 = x => x;
            }
            return Read<T>(errorContainers, containerExtractor);
        }

        public static MultiErrorInfo Read<T>(T[] errorContainers, Func<T, object> containerExtractor) where T: class
        {
            // Unresolved stack state at '000000C2'
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T> where T: class
        {
            public static readonly ValidationMultiErrorReader.<>c__0<T> <>9;
            public static Func<T, object> <>9__0_0;

            static <>c__0()
            {
                ValidationMultiErrorReader.<>c__0<T>.<>9 = new ValidationMultiErrorReader.<>c__0<T>();
            }

            internal object <Read>b__0_0(T x) => 
                x;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<T> where T: class
        {
            public static readonly ValidationMultiErrorReader.<>c__1<T> <>9;
            public static Func<object, string> <>9__1_0;
            public static Func<string, string> <>9__1_1;
            public static Func<ErrorInfo, string> <>9__1_2;
            public static Func<object, ErrorType> <>9__1_3;
            public static Func<ErrorInfo, ErrorType> <>9__1_4;

            static <>c__1()
            {
                ValidationMultiErrorReader.<>c__1<T>.<>9 = new ValidationMultiErrorReader.<>c__1<T>();
            }

            internal string <Read>b__1_0(object obj) => 
                (obj != null) ? obj.ToString() : string.Empty;

            internal string <Read>b__1_1(string str) => 
                str;

            internal string <Read>b__1_2(ErrorInfo info) => 
                info.ErrorText;

            internal ErrorType <Read>b__1_3(object _) => 
                ErrorType.Default;

            internal ErrorType <Read>b__1_4(ErrorInfo info) => 
                info.ErrorType;
        }
    }
}

