namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PredefinedFontSizeFCollection : List<int>
    {
        public PredefinedFontSizeFCollection()
        {
            Func<int, int> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<int, int> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = x => x;
            }
            this.AddRange(FontSizeManager.GetPredefinedFontSizes().Select<int, int>(selector));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PredefinedFontSizeFCollection.<>c <>9 = new PredefinedFontSizeFCollection.<>c();
            public static Func<int, int> <>9__0_0;

            internal int <.ctor>b__0_0(int x) => 
                x;
        }
    }
}

