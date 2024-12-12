namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    internal static class FormattedTextExportHelper
    {
        private static Color GetActualColor(Color mainColor, Color secondaryColor)
        {
            if (!IsTransparentOrSemitransparentColor(mainColor))
            {
                return mainColor;
            }
            Color backgroundColor = DXColor.Blend(secondaryColor, Color.White);
            return DXColor.Blend(mainColor, backgroundColor);
        }

        public static Color GetBackColor(Color mainColor, Color secondaryColor) => 
            (DXColor.IsTransparentColor(mainColor) || !DXColor.IsSemitransparentColor(mainColor)) ? mainColor : GetActualColor(mainColor, secondaryColor);

        public static Color GetBorderColor(Color mainColor, Color secondaryColor) => 
            IsTransparentOrSemitransparentColor(mainColor) ? GetActualColor(mainColor, secondaryColor) : mainColor;

        public static Color GetForeColor(Color mainColor, Color secondaryColor, Color thirdColor)
        {
            if (!IsTransparentOrSemitransparentColor(mainColor))
            {
                return mainColor;
            }
            Color actualColor = GetActualColor(secondaryColor, thirdColor);
            return DXColor.Blend(mainColor, actualColor);
        }

        private static bool IsTransparentOrSemitransparentColor(Color color) => 
            DXColor.IsTransparentColor(color) || DXColor.IsSemitransparentColor(color);
    }
}

