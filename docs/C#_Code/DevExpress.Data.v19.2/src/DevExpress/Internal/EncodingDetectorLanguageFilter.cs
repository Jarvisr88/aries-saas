namespace DevExpress.Internal
{
    using System;

    [Flags]
    public enum EncodingDetectorLanguageFilter
    {
        ChineseSimplified = 1,
        ChineseTraditional = 2,
        Japanese = 4,
        Korean = 8,
        NonCjk = 0x10,
        All = 0x1f,
        Chinese = 3,
        Cjk = 15
    }
}

