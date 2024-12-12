namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ParameterHelper
    {
        public const string ParametersPrefix = "Parameters";

        public static object ConvertFrom(object value, Type type, object defaultValue);
        public static object ConvertFrom(object value, Type type, object defaultValue, CultureInfo culture);
        public static string ConvertValueToString(object value);
        private static object CreateDefaultValue(Type type);
        public static IEnumerable EnsureEnumerable(object value);
        public static IEnumerable<T> GetActualParameters<T>(this IEnumerable<T> rootParameters) where T: IParameter;
        public static IEnumerable<T> GetAllParameters<T>(this IEnumerable<T> rootParameters) where T: IParameter;
        public static IParameter GetByName(this IEnumerable<IParameter> parameters, string parameterName);
        public static T GetByName<T>(this IEnumerable<T> parameters, string parameterName) where T: class, IFilterParameter;
        public static object GetDefaultValue(Type type);
        [IteratorStateMachine(typeof(ParameterHelper.<GetParameters>d__17<>))]
        private static IEnumerable<T> GetParameters<T>(IEnumerable<T> rootParameters, bool includeRangeRoot) where T: IParameter;
        public static bool ShouldConvertValue(object value, Type type);
        public static bool TryConvertEnumerable(IEnumerable value, out object result, Type type, CultureInfo culture);
        private static bool TryConvertFrom(object value, out object result, Type type, CultureInfo culture);
        private static bool TryConvertTo(object value, out object result, Type type);
        public static bool TryConvertValue(object value, out object result, Type type, CultureInfo culture);
        public static bool TryGetParameterName(string path, out string parameterName);

        [CompilerGenerated]
        private sealed class <GetParameters>d__17<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T: IParameter
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> rootParameters;
            public IEnumerable<T> <>3__rootParameters;
            private T <param>5__1;
            private bool includeRangeRoot;
            public bool <>3__includeRangeRoot;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <GetParameters>d__17(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

