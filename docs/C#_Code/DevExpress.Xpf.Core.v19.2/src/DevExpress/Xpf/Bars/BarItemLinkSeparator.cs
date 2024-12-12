namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarItemLinkSeparator : BarItemLink
    {
        static BarItemLinkSeparator();
        public BarItemLinkSeparator();
        protected override bool GetActualIsVisible();
        protected internal override void Initialize();
        protected internal override void UpdateProperties();

        protected internal override bool NeedsItem { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkSeparator.<>c <>9;
            public static LinkControlAction<IBarItemLinkControl> <>9__6_0;

            static <>c();
            internal void <UpdateProperties>b__6_0(IBarItemLinkControl lc);
        }
    }
}

