namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public static class Units
    {
        public static float CentimetersToDocumentsF(float value) => 
            MulDivF(value, 15000f, 127f);

        public static float CentimetersToTwipsF(float value) => 
            MulDivF(value, 1440f, 2.54f);

        public static float DocumentsToCentimetersF(float value) => 
            MulDivF(value, 127f, 15000f);

        public static int DocumentsToEmu(int val) => 
            MulDiv(val, 0x23b8, 3);

        public static double DocumentsToEmuD(float val) => 
            (double) MulDivF(val, 9144f, 3f);

        public static int DocumentsToEmuF(float val) => 
            (int) MulDivF(val, 9144f, 3f);

        public static long DocumentsToEmuL(long val) => 
            MulDivL(val, 0x23b8L, (long) 3L);

        public static Size DocumentsToHundredthsOfInch(Size val) => 
            new Size(DocumentsToHundredthsOfInch(val.Width), DocumentsToHundredthsOfInch(val.Height));

        public static int DocumentsToHundredthsOfInch(int val) => 
            val / 3;

        public static Size DocumentsToHundredthsOfMillimeter(Size val) => 
            new Size(DocumentsToHundredthsOfMillimeter(val.Width), DocumentsToHundredthsOfMillimeter(val.Height));

        public static int DocumentsToHundredthsOfMillimeter(int val) => 
            MulDiv(val, 0x7f, 15);

        public static float DocumentsToInchesF(float value) => 
            value / 300f;

        public static float DocumentsToMillimetersF(float value) => 
            MulDivF(value, 127f, 1500f);

        public static float DocumentsToPicasF(float value) => 
            value / 50f;

        public static int DocumentsToPixels(int val, float dpi) => 
            (val < 0) ? ((int) (((dpi * val) / 300f) - 0.99)) : ((int) (((dpi * val) / 300f) + 0.99));

        public static Point DocumentsToPixels(Point val, float dpiX, float dpiY) => 
            new Point(DocumentsToPixels(val.X, dpiX), DocumentsToPixels(val.Y, dpiY));

        public static Rectangle DocumentsToPixels(Rectangle val, float dpiX, float dpiY) => 
            new Rectangle(DocumentsToPixels(val.X, dpiX), DocumentsToPixels(val.Y, dpiY), DocumentsToPixels(val.Width, dpiX), DocumentsToPixels(val.Height, dpiY));

        public static RectangleF DocumentsToPixels(RectangleF val, float dpiX, float dpiY) => 
            new RectangleF(DocumentsToPixelsF(val.X, dpiX), DocumentsToPixelsF(val.Y, dpiY), DocumentsToPixelsF(val.Width, dpiX), DocumentsToPixelsF(val.Height, dpiY));

        public static Size DocumentsToPixels(Size val, float dpiX, float dpiY) => 
            new Size(DocumentsToPixels(val.Width, dpiX), DocumentsToPixels(val.Height, dpiY));

        public static float DocumentsToPixelsF(float val, float dpi) => 
            MulDivF(val, dpi, 300f);

        public static int DocumentsToPoints(int val) => 
            MulDiv(val, 6, 0x19);

        public static float DocumentsToPointsF(float val) => 
            MulDivF(val, 6f, 25f);

        public static float DocumentsToPointsFRound(float val) => 
            (float) Math.Round((double) DocumentsToPointsF(val));

        public static Rectangle DocumentsToTwips(Rectangle val) => 
            Rectangle.FromLTRB(DocumentsToTwips(val.Left), DocumentsToTwips(val.Top), DocumentsToTwips(val.Right), DocumentsToTwips(val.Bottom));

        public static RectangleF DocumentsToTwips(RectangleF val) => 
            RectangleF.FromLTRB(DocumentsToTwipsF(val.Left), DocumentsToTwipsF(val.Top), DocumentsToTwipsF(val.Right), DocumentsToTwipsF(val.Bottom));

        public static Size DocumentsToTwips(Size val) => 
            new Size(DocumentsToTwips(val.Width), DocumentsToTwips(val.Height));

        public static int DocumentsToTwips(int val) => 
            MulDiv(val, 0x18, 5);

        public static float DocumentsToTwipsF(float val) => 
            MulDivF(val, 24f, 5f);

        public static long DocumentsToTwipsL(long val) => 
            MulDivL(val, (long) 0x18, (long) 5L);

        public static int EmuToDocuments(int val) => 
            MulDiv(val, 3, 0x23b8);

        public static float EmuToDocumentsD(double val) => 
            (float) MulDivD(val, 3.0, 9144.0);

        public static float EmuToDocumentsF(int val) => 
            MulDivF((float) val, 3f, 9144f);

        public static long EmuToDocumentsL(long val) => 
            MulDivL(val, 3L, (long) 0x23b8L);

        public static int EmuToTwips(int val) => 
            val / 0x27b;

        public static float EmuToTwipsD(double val) => 
            (float) (val / 635.0);

        public static float EmuToTwipsF(int val) => 
            ((float) val) / 635f;

        public static long EmuToTwipsL(long val) => 
            val / 0x27bL;

        public static Size HundredthsOfInchToDocuments(Size val) => 
            new Size(HundredthsOfInchToDocuments(val.Width), HundredthsOfInchToDocuments(val.Height));

        public static int HundredthsOfInchToDocuments(int val) => 
            val * 3;

        public static Size HundredthsOfInchToTwips(Size val) => 
            new Size(HundredthsOfInchToTwips(val.Width), HundredthsOfInchToTwips(val.Height));

        public static int HundredthsOfInchToTwips(int val) => 
            MulDiv(val, 0x48, 5);

        public static Size HundredthsOfMillimeterToDocuments(Size val) => 
            new Size(HundredthsOfMillimeterToDocuments(val.Width), HundredthsOfMillimeterToDocuments(val.Height));

        public static int HundredthsOfMillimeterToDocuments(int val) => 
            MulDiv(val, 15, 0x7f);

        public static Size HundredthsOfMillimeterToDocumentsRound(Size val) => 
            new Size(HundredthsOfMillimeterToDocumentsRound(val.Width), HundredthsOfMillimeterToDocumentsRound(val.Height));

        private static int HundredthsOfMillimeterToDocumentsRound(int val) => 
            (int) Math.Round((double) MulDivF((float) val, 15f, 127f));

        public static int HundredthsOfMillimeterToPixels(int val, float dpi) => 
            (int) Math.Round((double) MulDivF((float) val, dpi, 2540f));

        public static Size HundredthsOfMillimeterToPixels(Size val, float dpiX, float dpiY) => 
            new Size(HundredthsOfMillimeterToPixels(val.Width, dpiX), HundredthsOfMillimeterToPixels(val.Height, dpiY));

        public static Size HundredthsOfMillimeterToTwips(Size val) => 
            new Size(HundredthsOfMillimeterToTwips(val.Width), HundredthsOfMillimeterToTwips(val.Height));

        public static int HundredthsOfMillimeterToTwips(int val) => 
            MulDiv(val, 0x48, 0x7f);

        public static Size HundredthsOfMillimeterToTwipsRound(Size val) => 
            new Size(HundredthsOfMillimeterToTwipsRound(val.Width), HundredthsOfMillimeterToTwipsRound(val.Height));

        private static int HundredthsOfMillimeterToTwipsRound(int val) => 
            (int) Math.Round((double) MulDivF((float) val, 72f, 127f));

        public static float InchesToDocumentsF(float value) => 
            300f * value;

        public static float InchesToPointsF(float value) => 
            72f * value;

        public static float InchesToTwipsF(float value) => 
            1440f * value;

        public static float MillimetersToDocumentsF(float value) => 
            MulDivF(value, 1500f, 127f);

        public static int MillimetersToPoints(int value) => 
            MulDiv(value, 360, 0x7f);

        public static float MillimetersToPointsF(float value) => 
            MulDivF(value, 72f, 25.4f);

        public static float MillimetersToTwipsF(float value) => 
            MulDivF(value, 1440f, 25.4f);

        private static int MulDiv(int value, int mul, int div) => 
            (mul * value) / div;

        public static int MulDiv(int value, int mul, float div) => 
            (int) (((float) (mul * value)) / div);

        public static int MulDiv(int value, float mul, int div) => 
            (int) ((mul * value) / ((float) div));

        public static double MulDivD(double value, double mul, double div) => 
            (mul * value) / div;

        public static float MulDivF(float value, float mul, float div) => 
            (mul * value) / div;

        private static long MulDivL(long value, long mul, long div) => 
            (mul * value) / div;

        private static long MulDivL(long value, long mul, float div) => 
            (long) (((float) (mul * value)) / div);

        public static float PicasToDocumentsF(float value) => 
            50f * value;

        public static float PicasToTwipsF(float value) => 
            240f * value;

        public static int PixelsToDocuments(double val, float dpi) => 
            (dpi != 0f) ? PixelsToDocumentsCore(val, dpi) : 0;

        public static int PixelsToDocuments(int val, float dpi) => 
            (dpi != 0f) ? PixelsToDocumentsCore(val, dpi) : 0;

        public static Point PixelsToDocuments(Point point, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new Point(PixelsToDocumentsCore(point.X, dpiX), PixelsToDocumentsCore(point.Y, dpiY));
        }

        public static Rectangle PixelsToDocuments(Rectangle rect, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new Rectangle(PixelsToDocumentsCore(rect.X, dpiX), PixelsToDocumentsCore(rect.Y, dpiY), PixelsToDocumentsCore(rect.Width, dpiX), PixelsToDocumentsCore(rect.Height, dpiY));
        }

        public static RectangleF PixelsToDocuments(RectangleF rect, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new RectangleF(PixelsToDocumentsCoreF(rect.X, dpiX), PixelsToDocumentsCoreF(rect.Y, dpiY), PixelsToDocumentsCoreF(rect.Width, dpiX), PixelsToDocumentsCoreF(rect.Height, dpiY));
        }

        public static Size PixelsToDocuments(Size size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new Size(PixelsToDocumentsCore(size.Width, dpiX), PixelsToDocumentsCore(size.Height, dpiY));
        }

        internal static int PixelsToDocumentsCore(double val, float dpi) => 
            (int) Math.Round((double) ((300.0 * val) / ((double) dpi)));

        internal static int PixelsToDocumentsCore(int val, float dpi) => 
            MulDiv(val, 300, dpi);

        private static float PixelsToDocumentsCoreF(float val, float dpi) => 
            MulDivF(val, 300f, dpi);

        internal static int PixelsToDocumentsCoreRound(int val, float dpi) => 
            (int) Math.Round((double) MulDivF((float) val, 300f, dpi));

        public static float PixelsToDocumentsF(float val, float dpi) => 
            (dpi != 0f) ? MulDivF(val, 300f, dpi) : 0f;

        public static SizeF PixelsToDocumentsF(SizeF size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new SizeF(PixelsToDocumentsCoreF(size.Width, dpiX), PixelsToDocumentsCoreF(size.Height, dpiY));
        }

        public static Size PixelsToDocumentsRound(Size size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 300f;
            }
            if (dpiY == 0f)
            {
                dpiY = 300f;
            }
            return new Size(PixelsToDocumentsCoreRound(size.Width, dpiX), PixelsToDocumentsCoreRound(size.Height, dpiY));
        }

        public static Size PixelsToHundredthsOfInch(Size val, float dpi) => 
            new Size(PixelsToHundredthsOfInch(val.Width, dpi), PixelsToHundredthsOfInch(val.Height, dpi));

        public static int PixelsToHundredthsOfInch(int val, float dpi) => 
            MulDiv(val, 100, dpi);

        public static int PixelsToHundredthsOfMillimeter(int val, float dpi) => 
            (int) Math.Round((double) (((float) (0x9ec * val)) / dpi));

        public static Size PixelsToHundredthsOfMillimeter(Size val, float dpiX, float dpiY) => 
            new Size(PixelsToHundredthsOfMillimeter(val.Width, dpiX), PixelsToHundredthsOfMillimeter(val.Height, dpiY));

        public static int PixelsToPoints(int val, float dpi) => 
            (dpi != 0f) ? PixelsToPointsCore(val, dpi) : 0;

        internal static int PixelsToPointsCore(int val, float dpi) => 
            MulDiv(val, 0x48, dpi);

        internal static float PixelsToPointsCore(float val, float dpi) => 
            MulDivF(val, 72f, dpi);

        public static float PixelsToPointsF(float val, float dpi) => 
            (dpi != 0f) ? PixelsToPointsCore(val, dpi) : 0f;

        public static int PixelsToTwips(int val, float dpi) => 
            (dpi != 0f) ? PixelsToTwipsCore(val, dpi) : 0;

        public static Rectangle PixelsToTwips(Rectangle rect, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 1440f;
            }
            if (dpiY == 0f)
            {
                dpiY = 1440f;
            }
            return new Rectangle(PixelsToTwipsCore(rect.X, dpiX), PixelsToTwipsCore(rect.Y, dpiY), PixelsToTwipsCore(rect.Width, dpiX), PixelsToTwipsCore(rect.Height, dpiY));
        }

        public static Size PixelsToTwips(Size size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 1440f;
            }
            if (dpiY == 0f)
            {
                dpiY = 1440f;
            }
            return new Size(PixelsToTwipsCore(size.Width, dpiX), PixelsToTwipsCore(size.Height, dpiY));
        }

        internal static int PixelsToTwipsCore(int val, float dpi) => 
            MulDiv(val, 0x5a0, dpi);

        internal static long PixelsToTwipsCore(long val, float dpi) => 
            MulDivL(val, 0x5a0L, dpi);

        internal static float PixelsToTwipsCore(float val, float dpi) => 
            MulDivF(val, 1440f, dpi);

        internal static int PixelsToTwipsCoreRound(int val, float dpi) => 
            (int) Math.Round((double) MulDivF((float) val, 1440f, dpi));

        public static float PixelsToTwipsF(float val, float dpi) => 
            (dpi != 0f) ? PixelsToTwipsCore(val, dpi) : 0f;

        public static SizeF PixelsToTwipsF(SizeF size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 1440f;
            }
            if (dpiY == 0f)
            {
                dpiY = 1440f;
            }
            return new SizeF(PixelsToTwipsCore(size.Width, dpiX), PixelsToTwipsCore(size.Height, dpiY));
        }

        public static long PixelsToTwipsL(long val, float dpi) => 
            (dpi != 0f) ? PixelsToTwipsCore(val, dpi) : 0L;

        public static Size PixelsToTwipsRound(Size size, float dpiX, float dpiY)
        {
            if (dpiX == 0f)
            {
                dpiX = 1440f;
            }
            if (dpiY == 0f)
            {
                dpiY = 1440f;
            }
            return new Size(PixelsToTwipsCoreRound(size.Width, dpiX), PixelsToTwipsCoreRound(size.Height, dpiY));
        }

        public static int PointsToDocuments(int val) => 
            MulDiv(val, 0x19, 6);

        public static float PointsToDocumentsF(float value) => 
            MulDivF(value, 25f, 6f);

        public static int PointsToPixels(int val, float dpi) => 
            (val < 0) ? ((int) (((dpi * val) / 72f) - 0.99)) : ((int) (((dpi * val) / 72f) + 0.99));

        public static float PointsToPixelsF(float val, float dpi) => 
            MulDivF(val, dpi, 72f);

        public static int PointsToTwips(int value) => 
            value * 20;

        public static float PointsToTwipsF(float value) => 
            value * 20f;

        internal static int RoundDocumentsToPixels(int val, float dpi) => 
            (int) (((dpi * val) / 300f) + 0.5);

        internal static int RoundTwipsToPixels(int val, float dpi) => 
            (int) (((dpi * val) / 1440f) + 0.5);

        public static float TwipsToCentimetersF(float value) => 
            MulDivF(value, 2.54f, 1440f);

        public static Rectangle TwipsToDocuments(Rectangle val) => 
            Rectangle.FromLTRB(TwipsToDocuments(val.Left), TwipsToDocuments(val.Top), TwipsToDocuments(val.Right), TwipsToDocuments(val.Bottom));

        public static RectangleF TwipsToDocuments(RectangleF val) => 
            RectangleF.FromLTRB(TwipsToDocumentsF(val.Left), TwipsToDocumentsF(val.Top), TwipsToDocumentsF(val.Right), TwipsToDocumentsF(val.Bottom));

        public static Size TwipsToDocuments(Size val) => 
            new Size(TwipsToDocuments(val.Width), TwipsToDocuments(val.Height));

        public static int TwipsToDocuments(int val) => 
            MulDiv(val, 5, 0x18);

        public static float TwipsToDocumentsF(float value) => 
            MulDivF(value, 5f, 24f);

        public static long TwipsToDocumentsL(long val) => 
            MulDivL(val, 5L, (long) 0x18);

        public static int TwipsToEmu(int val) => 
            val * 0x27b;

        public static double TwipsToEmuD(float val) => 
            val * 635.0;

        public static int TwipsToEmuF(float val) => 
            (int) (val * 635.0);

        public static long TwipsToEmuL(long val) => 
            val * 0x27bL;

        public static Size TwipsToHundredthsOfInch(Size val) => 
            new Size(TwipsToHundredthsOfInch(val.Width), TwipsToHundredthsOfInch(val.Height));

        public static int TwipsToHundredthsOfInch(int val) => 
            MulDiv(val, 5, 0x48);

        public static Size TwipsToHundredthsOfMillimeter(Size val) => 
            new Size(MulDiv(val.Width, 0x7f, 0x48), MulDiv(val.Height, 0x7f, 0x48));

        public static float TwipsToInchesF(float value) => 
            value / 1440f;

        public static float TwipsToMillimetersF(float value) => 
            MulDivF(value, 25.4f, 1440f);

        public static int TwipsToPixels(int val, float dpi) => 
            (int) (((dpi * val) / 1440f) + 0.99);

        public static Point TwipsToPixels(Point val, float dpiX, float dpiY) => 
            new Point(TwipsToPixels(val.X, dpiX), TwipsToPixels(val.Y, dpiY));

        public static Rectangle TwipsToPixels(Rectangle val, float dpiX, float dpiY) => 
            new Rectangle(TwipsToPixels(val.X, dpiX), TwipsToPixels(val.Y, dpiY), TwipsToPixels(val.Width, dpiX), TwipsToPixels(val.Height, dpiY));

        public static Size TwipsToPixels(Size val, float dpiX, float dpiY) => 
            new Size(TwipsToPixels(val.Width, dpiX), TwipsToPixels(val.Height, dpiY));

        public static float TwipsToPixelsF(float val, float dpi) => 
            MulDivF(val, dpi, 1440f);

        public static long TwipsToPixelsL(long val, float dpi) => 
            (long) (((dpi * val) / 1440f) + 0.99);

        public static float TwipsToPointsF(float val) => 
            val / 20f;

        public static float TwipsToPointsFRound(float val) => 
            (float) Math.Round((double) TwipsToPointsF(val));
    }
}

