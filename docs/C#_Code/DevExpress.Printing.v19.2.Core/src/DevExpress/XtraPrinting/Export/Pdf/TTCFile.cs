namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTCFile
    {
        public TTFFile Read(byte[] data, string fontName, int fontCodePage)
        {
            TTFStream ttfStream = new TTFStreamAsByteArray(data);
            TTCHeaderBase base2 = TTCHeaderBase.CreateTCCHeader(ttfStream);
            base2.Read(ttfStream);
            for (int i = 0; i < base2.NumFonts; i++)
            {
                TTFFile file = new TTFFile(base2.OffsetTable[i]);
                file.ReadNameTable(ttfStream);
                if (file.Name.FamilyName == fontName)
                {
                    file.Read(data);
                    file.FontCodePage = fontCodePage;
                    return file;
                }
            }
            return null;
        }
    }
}

