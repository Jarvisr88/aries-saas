namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class TextAlignmentConverter
    {
        public static StringAlignment ToAlignment(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.MiddleLeft)
            {
                if (textAlignment != TextAlignment.MiddleRight)
                {
                    if (textAlignment == TextAlignment.BottomLeft)
                    {
                        goto TR_0001;
                    }
                    else if (textAlignment != TextAlignment.BottomRight)
                    {
                        goto TR_0000;
                    }
                }
            }
            else
            {
                if (textAlignment != TextAlignment.TopLeft)
                {
                    if (textAlignment == TextAlignment.TopRight)
                    {
                        goto TR_0003;
                    }
                    else if (textAlignment != TextAlignment.MiddleLeft)
                    {
                        goto TR_0000;
                    }
                }
                goto TR_0001;
            }
            goto TR_0003;
        TR_0000:
            return StringAlignment.Center;
        TR_0001:
            return StringAlignment.Near;
        TR_0003:
            return StringAlignment.Far;
        }

        public static StringAlignment ToLineAlignment(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.MiddleCenter)
            {
                if (textAlignment == TextAlignment.MiddleRight)
                {
                    goto TR_0001;
                }
                else if (textAlignment != TextAlignment.TopJustify)
                {
                    if (textAlignment != TextAlignment.MiddleJustify)
                    {
                        goto TR_0000;
                    }
                    goto TR_0001;
                }
                goto TR_0003;
            }
            else
            {
                switch (textAlignment)
                {
                    case TextAlignment.TopLeft:
                    case TextAlignment.TopCenter:
                    case TextAlignment.TopRight:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        goto TR_0000;

                    default:
                        if ((textAlignment == TextAlignment.MiddleLeft) || (textAlignment == TextAlignment.MiddleCenter))
                        {
                            break;
                        }
                        goto TR_0000;
                }
            }
            goto TR_0001;
        TR_0000:
            return StringAlignment.Far;
        TR_0001:
            return StringAlignment.Center;
        TR_0003:
            return StringAlignment.Near;
        }

        public static TextAlignment ToTextAlignment(HorzAlignment alignment, VertAlignment lineAlignment) => 
            ToTextAlignment(AlignmentConverter.HorzAlignmentToStringAlignment(alignment), AlignmentConverter.VertAlignmentToStringAlignment(lineAlignment));

        public static TextAlignment ToTextAlignment(StringAlignment alignment, StringAlignment lineAlignment) => 
            (TextAlignment) ((1 << (alignment & 0x1f)) << ((4 * lineAlignment) & 0x1f));
    }
}

