namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public sealed class TextUtils
    {
        private const char charTab = '\t';

        private TextUtils()
        {
        }

        public static string[] GetTabbedPieces(string source)
        {
            char[] separator = new char[] { '\t' };
            return source.Split(separator);
        }
    }
}

