namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraPrinting;
    using System;

    public static class ExcelHelper
    {
        public static XlBorderLineStyle ConvertBorderStyle(float borderWidth, BorderDashStyle borderStyle)
        {
            if (Math.Round((double) borderWidth) <= 1.0)
            {
                switch (borderStyle)
                {
                    case BorderDashStyle.Solid:
                        return XlBorderLineStyle.Thin;

                    case BorderDashStyle.Dash:
                        return XlBorderLineStyle.Dashed;

                    case BorderDashStyle.DashDot:
                        return XlBorderLineStyle.DashDot;

                    case BorderDashStyle.DashDotDot:
                        return XlBorderLineStyle.DashDotDot;

                    default:
                        break;
                }
            }
            switch (borderStyle)
            {
                case BorderDashStyle.Solid:
                    return ((Math.Round((double) borderWidth) >= 3.0) ? XlBorderLineStyle.Thick : XlBorderLineStyle.Medium);

                case BorderDashStyle.Dash:
                    return XlBorderLineStyle.MediumDashed;

                case BorderDashStyle.Dot:
                    return XlBorderLineStyle.Dotted;

                case BorderDashStyle.DashDot:
                    return XlBorderLineStyle.MediumDashDot;

                case BorderDashStyle.DashDotDot:
                    return XlBorderLineStyle.MediumDashDotDot;

                case BorderDashStyle.Double:
                    return XlBorderLineStyle.Double;
            }
            return XlBorderLineStyle.Thin;
        }
    }
}

