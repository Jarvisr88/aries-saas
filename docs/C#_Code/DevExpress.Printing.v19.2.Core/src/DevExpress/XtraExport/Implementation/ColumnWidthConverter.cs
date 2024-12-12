namespace DevExpress.XtraExport.Implementation
{
    using System;

    internal static class ColumnWidthConverter
    {
        public static int CharactersWidthToPixels(float widthInCharacters, float maxDigitWidthInPixels)
        {
            double num = (widthInCharacters >= 1f) ? ((double) 5f) : ((double) (5f * widthInCharacters));
            return (int) Math.Truncate((double) ((((256.0 * (Math.Truncate((double) ((((widthInCharacters * maxDigitWidthInPixels) + num) / ((double) maxDigitWidthInPixels)) * 256.0)) / 256.0)) + Math.Truncate((double) (128f / maxDigitWidthInPixels))) / 256.0) * maxDigitWidthInPixels));
        }

        public static float PixelsToCharactersWidth(float pixels, float maxDigitWidthInPixels) => 
            Math.Min(255f, Math.Max((float) 0f, (float) (((float) ((int) (((pixels / maxDigitWidthInPixels) * 100f) + 0.5f))) / 100f)));
    }
}

