namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.XtraPrinting;
    using System;

    public static class RtfExportBorderHelper
    {
        private const int maxSingleBorderWidth = 0x4b;

        private static string GetBorderStyle(BorderDashStyle style, int borderWidth)
        {
            switch (style)
            {
                case BorderDashStyle.Dash:
                    return RtfTags.DashBorderStyle;

                case BorderDashStyle.Dot:
                    return RtfTags.DotBorderStyle;

                case BorderDashStyle.DashDot:
                    return RtfTags.DashDotBorderStyle;

                case BorderDashStyle.DashDotDot:
                    return RtfTags.DashDotDotBorderStyle;

                case BorderDashStyle.Double:
                    return RtfTags.DoubleBorderStyle;
            }
            return ((borderWidth <= 0x4b) ? RtfTags.SingleBorderWidth : RtfTags.DoubleBorderWidth);
        }

        private static int GetBorderWidth(BorderDashStyle style, int borderWidth) => 
            (style == BorderDashStyle.Double) ? (borderWidth / 3) : borderWidth;

        public static string GetFullBorderStyle(BorderDashStyle style, int borderWidth, int borderColorIndex) => 
            GetBorderStyle(style, borderWidth) + string.Format(RtfTags.BorderWidth, GetBorderWidth(style, borderWidth)) + string.Format(RtfTags.BorderColor, borderColorIndex);

        public static string GetFullBorderStyle(BorderDashStyle style, int borderWidth, int borderColorIndex, int borderSpace)
        {
            string str = GetFullBorderStyle(style, borderWidth, borderColorIndex);
            if (borderSpace > 0)
            {
                str = str + string.Format(RtfTags.BorderSpace, borderSpace);
            }
            return str;
        }
    }
}

