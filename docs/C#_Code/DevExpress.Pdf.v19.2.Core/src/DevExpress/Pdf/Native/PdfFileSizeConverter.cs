namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.Localization;
    using System;

    public static class PdfFileSizeConverter
    {
        private static readonly PdfCoreStringId[] units = new PdfCoreStringId[] { PdfCoreStringId.UnitKiloBytes, PdfCoreStringId.UnitMegaBytes, PdfCoreStringId.UnitGigaBytes, PdfCoreStringId.UnitTeraBytes, PdfCoreStringId.UnitPetaBytes, PdfCoreStringId.UnitExaBytes, PdfCoreStringId.UnitZettaBytes };

        public static string ToString(string pattern, double fileSize)
        {
            string str = fileSize.ToString("N0");
            if (fileSize < 1024.0)
            {
                return string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.FileSizeInBytes), str);
            }
            int index = 0;
            while (true)
            {
                double num1 = fileSize / 1024.0;
                if ((fileSize = num1) < 1024.0)
                {
                    return string.Format(pattern, fileSize, PdfCoreLocalizer.GetString(units[index]), str);
                }
                index++;
            }
        }
    }
}

