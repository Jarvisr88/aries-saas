namespace DevExpress.Office.Utils.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    public class InternalOfficeImageHelper
    {
        public static Size CalculateImageSizeInModelUnits(OfficeImage instance, DocumentModelUnitConverter unitConverter) => 
            instance.CalculateImageSizeInModelUnits(unitConverter);

        public static Graphics CreateEnhancedGraphics(Image img)
        {
            Graphics graphics = Graphics.FromImage(img);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return graphics;
        }

        public static void EnsureLoadComplete(OfficeImage instance)
        {
            instance.EnsureLoadComplete();
        }

        public static Stream GetImageBytesStream(OfficeImage instance, OfficeImageFormat imageFormat) => 
            instance.GetImageBytesStreamSafe(imageFormat);

        public static bool GetSuppressStore(OfficeImage instance) => 
            instance.SuppressStore;

        public static void SetSuppressStore(OfficeImage instance, bool value)
        {
            instance.SuppressStore = value;
        }
    }
}

