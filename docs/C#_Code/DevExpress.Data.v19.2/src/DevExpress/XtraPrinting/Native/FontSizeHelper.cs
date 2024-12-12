namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public static class FontSizeHelper
    {
        private static float DocumentsToPointsF(float val) => 
            (val * 6f) / 25f;

        public static float GetSizeInPoints(Font font)
        {
            if (GraphicsHelper.CanUseFontSizeInPoints)
            {
                return font.SizeInPoints;
            }
            switch (font.Unit)
            {
                case GraphicsUnit.Point:
                    return font.Size;

                case GraphicsUnit.Inch:
                    return InchesToPointsF(font.Size);

                case GraphicsUnit.Document:
                    return DocumentsToPointsF(font.Size);

                case GraphicsUnit.Millimeter:
                    return MillimetersToPointsF(font.Size);
            }
            return font.Size;
        }

        private static float InchesToPointsF(float val) => 
            val * 72f;

        private static float MillimetersToPointsF(float val) => 
            (72f * val) / 25.4f;
    }
}

