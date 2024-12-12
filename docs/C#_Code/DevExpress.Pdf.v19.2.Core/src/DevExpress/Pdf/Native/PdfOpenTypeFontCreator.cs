namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public static class PdfOpenTypeFontCreator
    {
        public static byte[] Create(PdfFont font)
        {
            PdfCFFFontProgramFacade fontProgramFacade = font.FontProgramFacade as PdfCFFFontProgramFacade;
            if (fontProgramFacade == null)
            {
                return null;
            }
            using (PdfFontFile file = new PdfFontFile(fontProgramFacade, font))
            {
                return file.GetData();
            }
        }
    }
}

