namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraPrinting;
    using System;

    internal static class XlGraphicUnitConverter
    {
        public static float GetDpi(IXlExport exporter)
        {
            if (exporter != null)
            {
                IXlDocumentOptionsEx documentOptions = exporter.DocumentOptions as IXlDocumentOptionsEx;
                if ((documentOptions != null) && documentOptions.UseDeviceIndependentPixels)
                {
                    return 96f;
                }
            }
            return GraphicsDpi.Pixel;
        }

        private static float MulDivF(float value, float mul, float div) => 
            (mul * value) / div;

        public static float PixelsToPointsF(float val, float dpi) => 
            MulDivF(val, 72f, dpi);

        public static int PixelsToTwips(int val, float dpi) => 
            (int) (((float) (val * 0x5a0)) / dpi);

        public static float PointsToPixelsF(float val, float dpi) => 
            MulDivF(val, dpi, 72f);
    }
}

