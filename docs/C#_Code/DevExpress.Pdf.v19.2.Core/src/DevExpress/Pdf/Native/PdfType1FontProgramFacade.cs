namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class PdfType1FontProgramFacade : PdfFontProgramFacade
    {
        private PdfType1FontProgramFacade(PdfRectangle fontBBox, IPdfCodePointMapping mapping) : base(fontBBox, nullable, nullable, mapping)
        {
            double? nullable = null;
            nullable = null;
        }

        public static PdfType1FontProgramFacade Create(PdfSimpleFont font, PdfType1FontFileData fontFileData) => 
            Create(font, fontFileData, program => program.GetSimpleMapping(font.Encoding));

        public static PdfType1FontProgramFacade Create(PdfType0Font font, PdfType1FontFileData fontFileData) => 
            Create(font, fontFileData, program => program.GetCompositeMapping(font.CidToGidMap));

        private static PdfType1FontProgramFacade Create(PdfFont font, PdfType1FontFileData fontFileData, Func<PdfType1FontClassicFontProgram, IPdfCodePointMapping> createMapping)
        {
            PdfType1FontFileData data;
            byte[] sourceArray = fontFileData.Data;
            int plainTextLength = fontFileData.PlainTextLength;
            byte[] destinationArray = sourceArray;
            int length = plainTextLength;
            PdfType1FontClassicFontProgram arg = PdfType1FontClassicFontProgram.Create(font.BaseFont, fontFileData);
            IPdfCodePointMapping mapping = null;
            PdfRectangle fontBBox = null;
            if (arg == null)
            {
                data = fontFileData;
            }
            else
            {
                arg.Validate(font);
                byte[] bytes = Encoding.UTF8.GetBytes(arg.ToPostScript());
                length = bytes.Length;
                int cipherTextLength = fontFileData.CipherTextLength;
                int nullSegmentLength = fontFileData.NullSegmentLength;
                int num5 = cipherTextLength + nullSegmentLength;
                destinationArray = new byte[length + num5];
                Array.Copy(bytes, destinationArray, length);
                Array.Copy(sourceArray, plainTextLength, destinationArray, length, num5);
                data = new PdfType1FontFileData(destinationArray, length, cipherTextLength, nullSegmentLength);
                fontBBox = arg.FontBBox;
                mapping = createMapping(arg);
            }
            return new PdfType1FontProgramFacade(fontBBox, mapping) { FontFile = data };
        }

        public PdfType1FontFileData FontFile { get; private set; }
    }
}

