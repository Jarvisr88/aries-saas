namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SvgRenderedImageCache : Dictionary<SvgImageKey, Image>
    {
        public bool TryGetValue(string id, Size size, ISvgPaletteProvider provider, out Image image)
        {
            Func<ISvgPaletteProvider, int?> get = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<ISvgPaletteProvider, int?> local1 = <>c.<>9__0_0;
                get = <>c.<>9__0_0 = x => new int?(x.GetHashCode());
            }
            int? defaultValue = null;
            return this.TryGetValue(new SvgImageKey(id, size, provider.Get<ISvgPaletteProvider, int?>(get, defaultValue)), out image);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgRenderedImageCache.<>c <>9 = new SvgRenderedImageCache.<>c();
            public static Func<ISvgPaletteProvider, int?> <>9__0_0;

            internal int? <TryGetValue>b__0_0(ISvgPaletteProvider x) => 
                new int?(x.GetHashCode());
        }
    }
}

