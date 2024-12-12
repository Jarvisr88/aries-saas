namespace DevExpress.Office.Utils
{
    using System;

    public static class OfficeArtExceptions
    {
        public static void ThrowInvalidContent()
        {
            throw new ArgumentException("Invalid OfficeArt content!");
        }
    }
}

