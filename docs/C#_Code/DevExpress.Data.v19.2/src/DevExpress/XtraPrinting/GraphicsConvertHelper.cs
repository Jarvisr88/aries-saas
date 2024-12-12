namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public class GraphicsConvertHelper
    {
        public static TextAlignment ChangeVertTextAlignment(TextAlignment textAlignment, StringAlignment vertAlignment)
        {
            switch (vertAlignment)
            {
                case StringAlignment.Near:
                    return ChangeVertTextAlignmentToNear(textAlignment);

                case StringAlignment.Center:
                    return ChangeVertTextAlignmentToCenter(textAlignment);

                case StringAlignment.Far:
                    return ChangeVertTextAlignmentToFar(textAlignment);
            }
            throw new ArgumentException("textAlignment");
        }

        private static TextAlignment ChangeVertTextAlignmentToCenter(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.BottomLeft)
            {
                if (textAlignment > TextAlignment.BottomRight)
                {
                    if ((textAlignment == TextAlignment.TopJustify) || ((textAlignment == TextAlignment.MiddleJustify) || (textAlignment == TextAlignment.BottomJustify)))
                    {
                        return TextAlignment.MiddleJustify;
                    }
                }
                else if (textAlignment == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (textAlignment > TextAlignment.MiddleLeft)
            {
                if (textAlignment == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment == TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
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
                        if (textAlignment != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new ArgumentException("textAlignment");
        TR_0001:
            return TextAlignment.MiddleLeft;
        TR_0003:
            return TextAlignment.MiddleCenter;
        TR_0004:
            return TextAlignment.MiddleRight;
        }

        private static TextAlignment ChangeVertTextAlignmentToFar(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.BottomLeft)
            {
                if (textAlignment > TextAlignment.BottomRight)
                {
                    if ((textAlignment == TextAlignment.TopJustify) || ((textAlignment == TextAlignment.MiddleJustify) || (textAlignment == TextAlignment.BottomJustify)))
                    {
                        return TextAlignment.BottomJustify;
                    }
                }
                else if (textAlignment == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (textAlignment > TextAlignment.MiddleLeft)
            {
                if (textAlignment == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment == TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
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
                        if (textAlignment != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new ArgumentException("textAlignment");
        TR_0001:
            return TextAlignment.BottomLeft;
        TR_0003:
            return TextAlignment.BottomCenter;
        TR_0004:
            return TextAlignment.BottomRight;
        }

        private static TextAlignment ChangeVertTextAlignmentToNear(TextAlignment textAlignment)
        {
            if (textAlignment > TextAlignment.BottomLeft)
            {
                if (textAlignment > TextAlignment.BottomRight)
                {
                    if ((textAlignment == TextAlignment.TopJustify) || ((textAlignment == TextAlignment.MiddleJustify) || (textAlignment == TextAlignment.BottomJustify)))
                    {
                        return TextAlignment.TopJustify;
                    }
                }
                else if (textAlignment == TextAlignment.BottomCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.BottomRight)
                {
                    goto TR_0004;
                }
                goto TR_0000;
            }
            else if (textAlignment > TextAlignment.MiddleLeft)
            {
                if (textAlignment == TextAlignment.MiddleCenter)
                {
                    goto TR_0003;
                }
                else if (textAlignment == TextAlignment.MiddleRight)
                {
                    goto TR_0004;
                }
                else if (textAlignment == TextAlignment.BottomLeft)
                {
                    goto TR_0001;
                }
                goto TR_0000;
            }
            else
            {
                switch (textAlignment)
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
                        if (textAlignment != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0004;
        TR_0000:
            throw new ArgumentException("textAlignment");
        TR_0001:
            return TextAlignment.TopLeft;
        TR_0003:
            return TextAlignment.TopCenter;
        TR_0004:
            return TextAlignment.TopRight;
        }

        public static ContentAlignment RTLContentAlignment(ContentAlignment contentAlignment)
        {
            if (contentAlignment > ContentAlignment.MiddleCenter)
            {
                if (contentAlignment > ContentAlignment.BottomLeft)
                {
                    if (contentAlignment == ContentAlignment.BottomCenter)
                    {
                        return contentAlignment;
                    }
                    else if (contentAlignment == ContentAlignment.BottomRight)
                    {
                        return ContentAlignment.BottomLeft;
                    }
                }
                else
                {
                    if (contentAlignment == ContentAlignment.MiddleRight)
                    {
                        return ContentAlignment.MiddleLeft;
                    }
                    if (contentAlignment == ContentAlignment.BottomLeft)
                    {
                        return ContentAlignment.BottomRight;
                    }
                }
            }
            else
            {
                switch (contentAlignment)
                {
                    case ContentAlignment.TopLeft:
                        return ContentAlignment.TopRight;

                    case ContentAlignment.TopCenter:
                        break;

                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        goto TR_0000;

                    case ContentAlignment.TopRight:
                        return ContentAlignment.TopLeft;

                    default:
                        if (contentAlignment == ContentAlignment.MiddleLeft)
                        {
                            return ContentAlignment.MiddleRight;
                        }
                        if (contentAlignment == ContentAlignment.MiddleCenter)
                        {
                            break;
                        }
                        goto TR_0000;
                }
                return contentAlignment;
            }
        TR_0000:
            throw new ArgumentException("contentAlignment");
        }

        public static StringAlignment RTLStringAlignment(StringAlignment align) => 
            (align == StringAlignment.Near) ? StringAlignment.Far : ((align == StringAlignment.Far) ? StringAlignment.Near : align);

        public static BrickAlignment ToHorzBrickAlignment(ImageAlignment align, ImageSizeMode sizeMode)
        {
            switch (align)
            {
                case ImageAlignment.Default:
                    return (((sizeMode == ImageSizeMode.CenterImage) || ((sizeMode == ImageSizeMode.Squeeze) || (sizeMode == ImageSizeMode.ZoomImage))) ? BrickAlignment.Center : BrickAlignment.Near);

                case ImageAlignment.TopLeft:
                case ImageAlignment.MiddleLeft:
                case ImageAlignment.BottomLeft:
                    return BrickAlignment.Near;

                case ImageAlignment.TopCenter:
                case ImageAlignment.MiddleCenter:
                case ImageAlignment.BottomCenter:
                    return BrickAlignment.Center;

                case ImageAlignment.TopRight:
                case ImageAlignment.MiddleRight:
                case ImageAlignment.BottomRight:
                    return BrickAlignment.Far;
            }
            throw new ArgumentException("align");
        }

        public static StringAlignment ToHorzStringAlignment(TextAlignment align) => 
            ToHorzStringAlignment(align, false);

        public static StringAlignment ToHorzStringAlignment(TextAlignment align, bool rightToLeft)
        {
            if (align > TextAlignment.BottomLeft)
            {
                if (align > TextAlignment.BottomRight)
                {
                    if ((align == TextAlignment.TopJustify) || ((align == TextAlignment.MiddleJustify) || (align == TextAlignment.BottomJustify)))
                    {
                        goto TR_0001;
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
                else if (align != TextAlignment.BottomLeft)
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            else
            {
                switch (align)
                {
                    case TextAlignment.TopLeft:
                        break;

                    case TextAlignment.TopCenter:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        goto TR_0000;

                    case TextAlignment.TopRight:
                        goto TR_0004;

                    default:
                        if (align == TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0000;
                }
                goto TR_0001;
            }
        TR_0000:
            throw new ArgumentException("align");
        TR_0001:
            return (rightToLeft ? StringAlignment.Far : StringAlignment.Near);
        TR_0003:
            return StringAlignment.Center;
        TR_0004:
            return (rightToLeft ? StringAlignment.Near : StringAlignment.Far);
        }

        public static BrickAlignment ToVertBrickAlignment(ImageAlignment align, ImageSizeMode sizeMode)
        {
            switch (align)
            {
                case ImageAlignment.Default:
                    return (((sizeMode == ImageSizeMode.CenterImage) || ((sizeMode == ImageSizeMode.Squeeze) || (sizeMode == ImageSizeMode.ZoomImage))) ? BrickAlignment.Center : BrickAlignment.Near);

                case ImageAlignment.TopLeft:
                case ImageAlignment.TopCenter:
                case ImageAlignment.TopRight:
                    return BrickAlignment.Near;

                case ImageAlignment.MiddleLeft:
                case ImageAlignment.MiddleCenter:
                case ImageAlignment.MiddleRight:
                    return BrickAlignment.Center;

                case ImageAlignment.BottomLeft:
                case ImageAlignment.BottomCenter:
                case ImageAlignment.BottomRight:
                    return BrickAlignment.Far;
            }
            throw new ArgumentException("align");
        }

        public static StringAlignment ToVertStringAlignment(TextAlignment align)
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
                        goto TR_0005;
                    }
                }
                else if ((align == TextAlignment.BottomCenter) || (align == TextAlignment.BottomRight))
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else if (align > TextAlignment.MiddleLeft)
            {
                if ((align == TextAlignment.MiddleCenter) || (align == TextAlignment.MiddleRight))
                {
                    goto TR_0001;
                }
                else if (align == TextAlignment.BottomLeft)
                {
                    goto TR_0005;
                }
                goto TR_0000;
            }
            else
            {
                switch (align)
                {
                    case TextAlignment.TopLeft:
                    case TextAlignment.TopCenter:
                    case TextAlignment.TopRight:
                        goto TR_0003;

                    case (TextAlignment.TopCenter | TextAlignment.TopLeft):
                        break;

                    default:
                        if (align != TextAlignment.MiddleLeft)
                        {
                            break;
                        }
                        goto TR_0001;
                }
                goto TR_0000;
            }
            goto TR_0005;
        TR_0000:
            throw new ArgumentException("align");
        TR_0001:
            return StringAlignment.Center;
        TR_0003:
            return StringAlignment.Near;
        TR_0005:
            return StringAlignment.Far;
        }
    }
}

