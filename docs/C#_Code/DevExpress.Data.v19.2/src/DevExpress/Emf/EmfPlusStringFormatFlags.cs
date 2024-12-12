namespace DevExpress.Emf
{
    using System;

    [Flags]
    public enum EmfPlusStringFormatFlags : long
    {
        StringFormatDirectionRightToLeft = 1L,
        StringFormatDirectionVertical = 2L,
        StringFormatNoFitBlackBox = 4L,
        StringFormatDisplayFormatControl = 0x20L,
        StringFormatNoFontFallback = 0x400L,
        StringFormatMeasureTrailingSpaces = 0x800L,
        StringFormatNoWrap = 0x1000L,
        StringFormatLineLimit = 0x2000L,
        StringFormatNoClip = 0x4000L,
        StringFormatBypassGDI = 0x80000000L
    }
}

