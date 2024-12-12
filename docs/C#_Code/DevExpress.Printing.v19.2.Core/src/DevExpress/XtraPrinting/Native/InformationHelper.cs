namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    internal static class InformationHelper
    {
        public static System.Drawing.Font Font;
        public static System.Drawing.Color Color;

        static InformationHelper();
        public static SizeF CalcSize(string infoString, float dpi, Measurer measurer);
    }
}

