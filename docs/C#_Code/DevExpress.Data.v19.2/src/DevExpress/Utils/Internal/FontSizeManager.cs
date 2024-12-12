namespace DevExpress.Utils.Internal
{
    using System;
    using System.Collections.Generic;

    public class FontSizeManager
    {
        public static IEnumerable<int> GetPredefinedFontSizes() => 
            new List<int> { 
                8,
                9,
                10,
                11,
                12,
                14,
                0x10,
                0x12,
                20,
                0x16,
                0x18,
                0x1a,
                0x1c,
                0x24,
                0x30,
                0x48
            };
    }
}

