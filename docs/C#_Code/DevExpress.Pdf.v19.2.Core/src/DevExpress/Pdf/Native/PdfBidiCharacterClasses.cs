namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public static class PdfBidiCharacterClasses
    {
        private const string resourcePath = "Text.bidiData.bin";
        private const int charCount = 0x10000;
        private static readonly PdfBidiCharacterClass[] characterClasses = new PdfBidiCharacterClass[0x10000];

        static PdfBidiCharacterClasses()
        {
            using (Stream stream = PdfEmbeddedResourceProvider.GetEmbeddedResourceStream("Text.bidiData.bin"))
            {
                byte[] buffer = new byte[0x10000];
                stream.Read(buffer, 0, 0x10000);
                for (int i = 0; i < 0x10000; i++)
                {
                    characterClasses[i] = (PdfBidiCharacterClass) buffer[i];
                }
            }
        }

        public static PdfBidiCharacterClass GetCharacterClass(char ch) => 
            characterClasses[ch];
    }
}

