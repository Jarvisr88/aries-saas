namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public static class BarItemLinkControlChecker
    {
        public static bool Is<TBarItemLinkControl>(this IBarItemLinkControl instance) where TBarItemLinkControl: IBarItemLinkControl;
    }
}

