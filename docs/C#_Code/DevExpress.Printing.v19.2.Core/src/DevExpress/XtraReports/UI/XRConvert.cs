namespace DevExpress.XtraReports.UI
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Text;
    using System.Text.RegularExpressions;

    public class XRConvert : GraphicsUnitConverter
    {
        private static object[] psBorderSides = new object[] { BorderSide.None, BorderSide.Left, BorderSide.Top, BorderSide.Right, BorderSide.Bottom };
        private static object[] xrBorderSides = new object[] { XRBorderSide_Utils.None, XRBorderSide_Utils.Left, XRBorderSide_Utils.Top, XRBorderSide_Utils.Right, XRBorderSide_Utils.Bottom };
        private float dpi;

        [Obsolete("This member has become obsolete. Use another constructor of the XRConvert class instead."), DXHelpExclude(true)]
        public XRConvert(ReportUnit_Utils unit)
        {
            this.dpi = 100f;
            this.dpi = UnitToDpi(unit);
        }

        public XRConvert(float dpi)
        {
            this.dpi = 100f;
            this.dpi = dpi;
        }

        public Point ConvertFrom(Point val, float dpi) => 
            Point.Round((PointF) Convert(val, dpi, this.Dpi));

        public Rectangle ConvertFrom(Rectangle val, float dpi) => 
            Convert(val, dpi, this.Dpi);

        public Size ConvertFrom(Size val, float dpi) => 
            Size.Round((SizeF) Convert(val, dpi, this.Dpi));

        public int ConvertFrom(int val, float dpi) => 
            Convert.ToInt32(Convert(val, dpi, this.Dpi));

        public static Margins ConvertMargins(Margins val, float fromDpi, float toDpi)
        {
            Rectangle rectangle = Convert(Rectangle.FromLTRB(val.Left, val.Top, val.Right, val.Bottom), fromDpi, toDpi);
            return new Margins(rectangle.Left, rectangle.Right, rectangle.Top, rectangle.Bottom);
        }

        public Point ConvertTo(Point val, float dpi) => 
            Convert(val, this.Dpi, dpi);

        public Rectangle ConvertTo(Rectangle val, float dpi) => 
            Convert(val, this.Dpi, dpi);

        public Size ConvertTo(Size val, float dpi) => 
            Convert(val, this.Dpi, dpi);

        public int ConvertTo(int val, float dpi) => 
            Convert(val, this.Dpi, dpi);

        public static string StringArrayToString(string[] array)
        {
            if ((array == null) || (array.Length == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder(array[0]);
            int length = array.Length;
            for (int i = 1; i < length; i++)
            {
                builder.Append("\r\n");
                builder.Append(array[i]);
            }
            return builder.ToString();
        }

        public static string[] StringToStringArray(string str) => 
            (str != null) ? Regex.Split(str, "\r\n") : new string[0];

        [Obsolete("This member has become obsolete. Use other members of the XRConvert class instead."), DXHelpExclude(true)]
        public static BorderSide ToBorderSide(XRBorderSide_Utils val)
        {
            BorderSide none = BorderSide.None;
            for (int i = 0; i < xrBorderSides.Length; i++)
            {
                if ((val & ((XRBorderSide_Utils) xrBorderSides[i])) != XRBorderSide_Utils.None)
                {
                    none |= (BorderSide) psBorderSides[i];
                }
            }
            return none;
        }

        public static ContentAlignment ToContentAlignment(TextAlignment align) => 
            (align < TextAlignment.TopJustify) ? ((ContentAlignment) align) : ((align != TextAlignment.TopJustify) ? ((align != TextAlignment.MiddleJustify) ? ContentAlignment.BottomLeft : ContentAlignment.MiddleLeft) : ContentAlignment.TopLeft);

        public static TextAlignment ToTextAlignment(ContentAlignment align) => 
            (TextAlignment) align;

        [Obsolete("This member has become obsolete. Use other members of the XRConvert class instead."), DXHelpExclude(true)]
        public static XRBorderSide_Utils ToXRBorderSide(BorderSide val)
        {
            XRBorderSide_Utils none = XRBorderSide_Utils.None;
            for (int i = 0; i < psBorderSides.Length; i++)
            {
                if ((val & ((BorderSide) psBorderSides[i])) != BorderSide.None)
                {
                    none |= (XRBorderSide_Utils) xrBorderSides[i];
                }
            }
            return none;
        }

        [Obsolete("This member has become obsolete. Use other members of the XRConvert class instead."), DXHelpExclude(true)]
        public static float UnitToDpi(ReportUnit_Utils reportUnit)
        {
            switch (reportUnit)
            {
                case ReportUnit_Utils.HundredthsOfAnInch:
                    return 100f;

                case ReportUnit_Utils.TenthsOfAMillimeter:
                    return 254f;

                case ReportUnit_Utils.Pixels:
                    return 96f;
            }
            throw new NotSupportedException();
        }

        public virtual float Dpi =>
            this.dpi;
    }
}

