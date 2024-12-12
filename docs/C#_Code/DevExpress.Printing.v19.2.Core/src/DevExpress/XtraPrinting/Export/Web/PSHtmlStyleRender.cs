namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    public static class PSHtmlStyleRender
    {
        private static void AppendBorderSide(StringBuilder sb, Color borderColor, PaddingInfo borders, BorderDashStyle borderStyle, BorderSide side, bool rtl)
        {
            int borderWidth = GetBorderWidth(borders, side);
            string str = rtl ? GetRtlHtmlBorderSide(side) : GetHtmlBorderSide(side);
            if (borderWidth == 0)
            {
                sb.AppendFormat("border-{0}-style: none;", str);
            }
            else
            {
                object[] args = new object[] { str, HtmlConvert.ToHtml(borderColor), borderWidth, GetHtmlBorderStyle(borderStyle) };
                sb.AppendFormat("border-{0}:{1} {2}px {3};", args);
            }
        }

        public static string GetBorderHtml(Color borderColor, Color backColor, BorderSide sides, int borderWidth, bool rtl) => 
            GetBorderHtml(borderColor, backColor, GetBorders(sides, borderWidth), BorderDashStyle.Solid, rtl);

        public static string GetBorderHtml(Color borderColor, Color backColor, PaddingInfo borders, BorderDashStyle borderStyle, bool rtl)
        {
            StringBuilder sb = new StringBuilder();
            AppendBorderSide(sb, borderColor, borders, borderStyle, BorderSide.Left, rtl);
            AppendBorderSide(sb, borderColor, borders, borderStyle, BorderSide.Top, rtl);
            AppendBorderSide(sb, borderColor, borders, borderStyle, BorderSide.Right, rtl);
            AppendBorderSide(sb, borderColor, borders, borderStyle, BorderSide.Bottom, rtl);
            return sb.ToString();
        }

        public static PaddingInfo GetBorders(BrickStyle style) => 
            GetBorders(style.Sides, (int) Math.Round((double) style.BorderWidth));

        public static PaddingInfo GetBorders(BorderSide sides, int borderWidth) => 
            new PaddingInfo(((sides & BorderSide.Left) != BorderSide.None) ? borderWidth : 0, ((sides & BorderSide.Right) != BorderSide.None) ? borderWidth : 0, ((sides & BorderSide.Top) != BorderSide.None) ? borderWidth : 0, ((sides & BorderSide.Bottom) != BorderSide.None) ? borderWidth : 0);

        private static int GetBorderWidth(PaddingInfo borders, BorderSide side)
        {
            switch (side)
            {
                case BorderSide.Left:
                    return borders.Left;

                case BorderSide.Top:
                    return borders.Top;

                case (BorderSide.Top | BorderSide.Left):
                    break;

                case BorderSide.Right:
                    return borders.Right;

                default:
                    if (side != BorderSide.Bottom)
                    {
                        break;
                    }
                    return borders.Bottom;
            }
            return 0;
        }

        private static string GetHtmlBorderSide(BorderSide side)
        {
            switch (side)
            {
                case BorderSide.Left:
                    return "left";

                case BorderSide.Top:
                    return "top";

                case (BorderSide.Top | BorderSide.Left):
                    break;

                case BorderSide.Right:
                    return "right";

                default:
                    if (side != BorderSide.Bottom)
                    {
                        break;
                    }
                    return "bottom";
            }
            throw new NotImplementedException();
        }

        private static string GetHtmlBorderStyle(BorderDashStyle borderStyle)
        {
            switch (borderStyle)
            {
                case BorderDashStyle.Dash:
                case BorderDashStyle.DashDot:
                case BorderDashStyle.DashDotDot:
                    return "dashed";

                case BorderDashStyle.Dot:
                    return "dotted";

                case BorderDashStyle.Double:
                    return "double";
            }
            return "solid";
        }

        private static string GetHtmlImagePosition(ContentAlignment alignment)
        {
            if (alignment > ContentAlignment.MiddleCenter)
            {
                if (alignment <= ContentAlignment.BottomLeft)
                {
                    if (alignment == ContentAlignment.MiddleRight)
                    {
                        return "right center";
                    }
                    if (alignment == ContentAlignment.BottomLeft)
                    {
                        return "left bottom";
                    }
                }
                else
                {
                    if (alignment == ContentAlignment.BottomCenter)
                    {
                        return "center bottom";
                    }
                    if (alignment == ContentAlignment.BottomRight)
                    {
                        return "right bottom";
                    }
                }
            }
            else
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        break;

                    case ContentAlignment.TopCenter:
                        return "center top";

                    case ContentAlignment.TopRight:
                        return "right top";

                    default:
                        if (alignment == ContentAlignment.MiddleLeft)
                        {
                            return "left center";
                        }
                        if (alignment != ContentAlignment.MiddleCenter)
                        {
                            break;
                        }
                        return "center center";
                }
            }
            return "left top";
        }

        private static object GetHtmlPadding(PaddingInfo padding)
        {
            PaddingInfo info = new PaddingInfo(padding, 96f);
            string str = string.Empty;
            if (info.Top > 0)
            {
                object[] objArray1 = new object[] { str, "padding-top:", info.Top, "px;" };
                str = string.Concat(objArray1);
            }
            if (info.Left > 0)
            {
                object[] objArray2 = new object[] { str, "padding-left:", info.Left, "px;" };
                str = string.Concat(objArray2);
            }
            if (info.Right > 0)
            {
                object[] objArray3 = new object[] { str, "padding-right:", info.Right, "px;" };
                str = string.Concat(objArray3);
            }
            if (info.Bottom > 0)
            {
                object[] objArray4 = new object[] { str, "padding-bottom:", info.Bottom, "px;" };
                str = string.Concat(objArray4);
            }
            return str;
        }

        public static string GetHtmlStyle(Font font, Color foreColor, Color backColor, Color borderColor, PaddingInfo borders, PaddingInfo padding, BorderDashStyle borderStyle, bool rtl) => 
            $"color:{HtmlConvert.ToHtml(foreColor)};background-color:{HtmlConvert.ToHtml(backColor)};{GetBorderHtml(borderColor, backColor, borders, borderStyle, rtl)}{HtmlStyleRender.GetFontHtmlInPixels(font)}{GetHtmlPadding(padding)}";

        public static int GetHtmlTextDirection(DirectionMode textDirection)
        {
            switch (textDirection)
            {
                case DirectionMode.ForwardDiagonal:
                    return -50;

                case DirectionMode.BackwardDiagonal:
                    return 50;

                case DirectionMode.Vertical:
                    return -90;
            }
            return 0;
        }

        public static string GetHtmlWatermarkImageStyle(Size pageSize, Point offset, bool needClipMargins, string imageSrc, PageWatermark pageWatermark)
        {
            string str = $"width:{pageSize.Width}px;height:{pageSize.Height}px;position:absolute;";
            if (needClipMargins)
            {
                str = str + $"margin-top:{-offset.Y}px;margin-left:{-offset.X}px;";
            }
            if (!string.IsNullOrEmpty(imageSrc))
            {
                str = str + $"background-image:url('{imageSrc}');";
                ImageSource imageSource = pageWatermark.ImageSource;
                bool imageTiling = pageWatermark.ImageTiling;
                ContentAlignment imageAlign = pageWatermark.ImageAlign;
                bool flag2 = false;
                SizeF empty = (SizeF) Size.Empty;
                string htmlImagePosition = "left top";
                switch (pageWatermark.ImageViewMode)
                {
                    case ImageViewMode.Clip:
                        if (imageTiling)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            htmlImagePosition = GetHtmlImagePosition(imageAlign);
                        }
                        empty = imageSource.GetImageSize(true);
                        break;

                    case ImageViewMode.Stretch:
                        empty = (SizeF) pageSize;
                        break;

                    case ImageViewMode.Zoom:
                    {
                        SizeF imageSize = imageSource.GetImageSize(false);
                        float adjustedScale = WatermarkHelper.GetAdjustedScale((SizeF) pageSize, imageSize);
                        empty = (SizeF) new Size((int) (imageSize.Width * adjustedScale), (int) (imageSize.Height * adjustedScale));
                        if (imageTiling)
                        {
                            flag2 = true;
                        }
                        else
                        {
                            ContentAlignment alignment2 = WatermarkHelper.GetAdjustedAlignment((SizeF) pageSize, imageSource.GetImageSize(false), imageAlign);
                            htmlImagePosition = GetHtmlImagePosition(imageAlign);
                        }
                        break;
                    }
                    default:
                        break;
                }
                str = str + $"background-size:{((int) Math.Round((double) empty.Width)).ToString()}px {((int) Math.Round((double) empty.Height)).ToString()}px;background-position:{htmlImagePosition};background-repeat:{(flag2 ? "repeat" : "no-repeat")};" + GetOpacityStyle(pageWatermark.ImageTransparency);
            }
            return str;
        }

        public static string GetHtmlWatermarkTextStyle(Size pageSize, Point offset, bool needClipMargins, Size textSize, PageWatermark pageWatermark)
        {
            string str = string.Format("width:{0}px;height:{1}px;position:absolute;line-height:{1}px;{2}", textSize.Width, textSize.Height, HtmlStyleRender.GetFontHtmlInPixels(pageWatermark.Font)) + $"text-align:center;color:{HtmlConvert.ToHtml(pageWatermark.ForeColor)};";
            int num = (int) (((float) (pageSize.Height - textSize.Height)) / 2f);
            int num2 = (int) (((float) (pageSize.Width - textSize.Width)) / 2f);
            if (needClipMargins)
            {
                num -= offset.Y;
                num2 -= offset.X;
            }
            return ((str + $"margin-top:{num}px;margin-left:{num2}px;") + string.Format("-webkit-transform:rotate({0}deg);transform:rotate({0}deg);", GetHtmlTextDirection(pageWatermark.TextDirection)) + GetOpacityStyle(pageWatermark.TextTransparency));
        }

        private static string GetOpacityStyle(int transparency)
        {
            float num = (255f - transparency) / 255f;
            return $"opacity:{num.ToString("F3", CultureInfo.InvariantCulture)};filter:alpha(opacity={(num * 100f).ToString("F1", CultureInfo.InvariantCulture)});";
        }

        private static string GetRtlHtmlBorderSide(BorderSide side)
        {
            switch (side)
            {
                case BorderSide.Left:
                    return "right";

                case BorderSide.Top:
                    return "top";

                case (BorderSide.Top | BorderSide.Left):
                    break;

                case BorderSide.Right:
                    return "left";

                default:
                    if (side != BorderSide.Bottom)
                    {
                        break;
                    }
                    return "bottom";
            }
            throw new NotImplementedException();
        }
    }
}

