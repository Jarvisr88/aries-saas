namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal static class StyleHelper
    {
        public static string GetBorderStyle(HtmlCellBorderStyle style)
        {
            switch (style)
            {
                case HtmlCellBorderStyle.None:
                    return "none";

                case HtmlCellBorderStyle.Dashed:
                case HtmlCellBorderStyle.MediumDashed:
                case HtmlCellBorderStyle.DashDot:
                case HtmlCellBorderStyle.MediumDashDot:
                case HtmlCellBorderStyle.DashDotDot:
                case HtmlCellBorderStyle.MediumDashDotDot:
                    return "dashed";

                case HtmlCellBorderStyle.Dotted:
                case HtmlCellBorderStyle.Hair:
                    return "dotted";

                case HtmlCellBorderStyle.Double:
                    return "double";
            }
            return "solid";
        }

        public static string GetBorderWidth(HtmlCellBorderStyle style, float width)
        {
            switch (style)
            {
                case HtmlCellBorderStyle.None:
                    return string.Empty;

                case HtmlCellBorderStyle.Thin:
                    return $"{width:F}px";

                case HtmlCellBorderStyle.Dashed:
                case HtmlCellBorderStyle.MediumDashed:
                case HtmlCellBorderStyle.DashDot:
                case HtmlCellBorderStyle.MediumDashDot:
                    return string.Format("{0:}px", width);

                case HtmlCellBorderStyle.Dotted:
                case HtmlCellBorderStyle.Hair:
                case HtmlCellBorderStyle.DashDotDot:
                case HtmlCellBorderStyle.MediumDashDotDot:
                    return $"{width:F}px";

                case HtmlCellBorderStyle.Thick:
                    return $"{width:F}px";

                case HtmlCellBorderStyle.Double:
                    return $"{width:F}px";
            }
            return $"{width:F}px";
        }

        public static string ToHex(this Color color) => 
            "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

        public static string ToHtmlValue(this HtmlCellHAlignment alignmnent)
        {
            switch (alignmnent)
            {
                case HtmlCellHAlignment.Center:
                    return "center";

                case HtmlCellHAlignment.Right:
                    return "right";

                case HtmlCellHAlignment.Justify:
                    return "justify";
            }
            return "left";
        }

        public static string ToHtmlValue(this HtmlCellVAlignment alignmnent)
        {
            switch (alignmnent)
            {
                case HtmlCellVAlignment.Center:
                    return "center";

                case HtmlCellVAlignment.Bottom:
                    return "bottom";
            }
            return "top";
        }
    }
}

