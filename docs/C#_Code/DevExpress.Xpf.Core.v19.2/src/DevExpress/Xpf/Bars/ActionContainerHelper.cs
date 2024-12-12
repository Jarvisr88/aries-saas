namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ActionContainerHelper
    {
        public static BarManager GetBarManager(this IActionContainer container);
        public static IActionContainer GetRootContainer(this IControllerAction action);
    }
}

