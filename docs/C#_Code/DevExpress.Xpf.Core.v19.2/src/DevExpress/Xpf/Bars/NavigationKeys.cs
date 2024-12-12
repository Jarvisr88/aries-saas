namespace DevExpress.Xpf.Bars
{
    using System;

    [Flags]
    public enum NavigationKeys
    {
        public const NavigationKeys None = NavigationKeys.None;,
        public const NavigationKeys Arrows = NavigationKeys.Arrows;,
        public const NavigationKeys Tab = NavigationKeys.Tab;,
        public const NavigationKeys HomeEnd = NavigationKeys.HomeEnd;,
        public const NavigationKeys CtrlTab = NavigationKeys.CtrlTab;,
        public const NavigationKeys All = NavigationKeys.All;
    }
}

