namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class MayBe
    {
        [DebuggerStepThrough, DebuggerHidden]
        internal static void Do<T>(this T @this, Action<T> @do)
        {
            if (@this != null)
            {
                @do(@this);
            }
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static TResult Get<T, TResult>(this T @this, Func<T, TResult> get, TResult defaultValue = null) => 
            (@this != null) ? get(@this) : defaultValue;
    }
}

