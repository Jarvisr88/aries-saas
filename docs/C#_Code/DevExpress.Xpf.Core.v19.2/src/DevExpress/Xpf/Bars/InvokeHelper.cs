namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class InvokeHelper
    {
        public static void InvokeIfRequired(this BarManager manager, Action operation);
    }
}

