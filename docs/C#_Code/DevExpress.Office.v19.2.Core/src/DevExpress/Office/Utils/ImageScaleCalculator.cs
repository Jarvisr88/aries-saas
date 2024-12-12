namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public static class ImageScaleCalculator
    {
        public static Size GetDesiredImageSizeInModelUnits(Size desiredSizeInPixels, DocumentModelUnitConverter unitConverter) => 
            unitConverter.PixelsToModelUnits(desiredSizeInPixels);

        public static Size GetDesiredImageSizeInModelUnits(int widthInPixels, int heightInPixels) => 
            Units.PixelsToTwips(new Size(widthInPixels, heightInPixels), DocumentModelBase<int>.DpiX, DocumentModelBase<int>.DpiY);

        public static SizeF GetScale(Size desiredSize, Size originalSize, float defaultScale) => 
            new SizeF(GetScale(desiredSize.Width, originalSize.Width, defaultScale), GetScale(desiredSize.Height, originalSize.Height, defaultScale));

        public static float GetScale(int desired, int originalInModelUnits, float defaultScale)
        {
            float num = defaultScale;
            if (desired > 0)
            {
                num = Math.Max((float) 1f, (float) ((100f * desired) / ((float) Math.Max(1, originalInModelUnits))));
            }
            return Math.Max(1f, num);
        }
    }
}

