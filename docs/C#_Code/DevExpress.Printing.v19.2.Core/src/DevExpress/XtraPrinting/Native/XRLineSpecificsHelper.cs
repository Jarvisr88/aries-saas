namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Drawing.Drawing2D;

    public class XRLineSpecificsHelper
    {
        private const char Separator = ';';

        public static string Combine(LineDirection lineDirection, DashStyle dashStyle, int lineWidth, int colorARGB);
        public static LineSpecifics Split(string combinedLineSpecifics);
    }
}

