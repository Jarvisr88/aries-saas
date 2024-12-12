namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public class PageMarginSettings
    {
        public PageMarginSettings(MarginSide side, double size)
        {
            Func<MarginSide, bool> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<MarginSide, bool> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = x => (x != MarginSide.All) && (x != MarginSide.None);
            }
            Guard.ArgumentMatch<MarginSide>(side, "side", predicate);
            this.Side = side;
            this.Size = size;
        }

        public MarginSide Side { get; private set; }

        public double Size { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageMarginSettings.<>c <>9 = new PageMarginSettings.<>c();
            public static Func<MarginSide, bool> <>9__8_0;

            internal bool <.ctor>b__8_0(MarginSide x) => 
                (x != MarginSide.All) && (x != MarginSide.None);
        }
    }
}

