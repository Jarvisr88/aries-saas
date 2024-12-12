namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public static class RtfAlignmentConverter
    {
        public static string ToHorzRtfAlignment(TextAlignment align)
        {
            if (align > TextAlignment.BottomLeft)
            {
                if (align > TextAlignment.BottomRight)
                {
                    if ((align == TextAlignment.TopJustify) || ((align == TextAlignment.MiddleJustify) || (align == TextAlignment.BottomJustify)))
                    {
                        return RtfTags.Justified;
                    }
                }
                else if (align == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (align == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (align > TextAlignment.MiddleLeft)
            {
                if (align == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (align == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (align == TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (align)
                {
                    case TextAlignment.TopLeft:
                        goto TR_0001;

                    case TextAlignment.TopCenter:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        break;

                    case TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (align != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new ArgumentException("align");
        TR_0001:
            return RtfTags.LeftAligned;
        TR_0003:
            return RtfTags.Centered;
        TR_0004:
            return RtfTags.RightAligned;
        }

        public static string ToHorzRtfAlignment(StringAlignment align)
        {
            switch (align)
            {
                case StringAlignment.Near:
                    return RtfTags.LeftAligned;

                case StringAlignment.Center:
                    return RtfTags.Centered;

                case StringAlignment.Far:
                    return RtfTags.RightAligned;
            }
            throw new ArgumentException("align");
        }

        public static string ToVertRtfAlignment(TextAlignment align)
        {
            if (align > TextAlignment.BottomLeft)
            {
                if (align > TextAlignment.BottomRight)
                {
                    if (align == TextAlignment.TopJustify)
                    {
                        goto TR_0003;
                    }
                    else if (align == TextAlignment.MiddleJustify)
                    {
                        goto TR_0001;
                    }
                    else if (align == TextAlignment.BottomJustify)
                    {
                        goto TR_0006;
                    }
                }
                else if (align == TextAlignment.BottomCenter)
                {
                    goto TR_0006;
                }
                else if (align == TextAlignment.BottomRight)
                {
                    return (RtfTags.BottomAlignedInCell + " ");
                }
                goto TR_0000;
            }
            else if (align > TextAlignment.MiddleLeft)
            {
                if (align == TextAlignment.MiddleCenter)
                {
                    goto TR_0001;
                }
                else
                {
                    if (align == TextAlignment.MiddleRight)
                    {
                        return (RtfTags.VerticalCenteredInCell + " ");
                    }
                    if (align == TextAlignment.BottomLeft)
                    {
                        goto TR_0006;
                    }
                }
                goto TR_0000;
            }
            else
            {
                switch (align)
                {
                    case TextAlignment.TopLeft:
                    case TextAlignment.TopCenter:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        break;

                    case TextAlignment.TopRight:
                        return (RtfTags.TopAlignedInCell + " ");

                    default:
                        if (align != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0006;
        TR_0000:
            throw new ArgumentException("align");
        TR_0001:
            return RtfTags.VerticalCenteredInCell;
        TR_0003:
            return RtfTags.TopAlignedInCell;
        TR_0006:
            return RtfTags.BottomAlignedInCell;
        }
    }
}

