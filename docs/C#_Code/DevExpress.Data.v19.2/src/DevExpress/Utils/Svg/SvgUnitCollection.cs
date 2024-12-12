namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SvgUnitCollection : List<SvgUnit>
    {
        public override string ToString()
        {
            Func<SvgUnit, string> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<SvgUnit, string> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = x => x.ToString();
            }
            return string.Join(" ", this.Select<SvgUnit, string>(selector));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgUnitCollection.<>c <>9 = new SvgUnitCollection.<>c();
            public static Func<SvgUnit, string> <>9__0_0;

            internal string <ToString>b__0_0(SvgUnit x) => 
                x.ToString();
        }
    }
}

