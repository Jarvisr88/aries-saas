namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodingStyleComponent
    {
        private readonly int decompositionLevelCount;
        private readonly int codeBlockWidthExponent;
        private readonly int codeBlockHeightExponent;
        private readonly JPXCodeBlockCodingStyle codeBlockCodingStyle;
        private readonly bool useWaveletTransformation;
        private readonly JPXPrecinctSize[] precinctSizes;

        public JPXCodingStyleComponent(PdfBigEndianStreamReader reader, bool usePrecincts)
        {
            this.decompositionLevelCount = reader.ReadByte();
            this.codeBlockWidthExponent = (reader.ReadByte() & 15) + 2;
            this.codeBlockHeightExponent = (reader.ReadByte() & 15) + 2;
            this.codeBlockCodingStyle = (JPXCodeBlockCodingStyle) reader.ReadByte();
            this.useWaveletTransformation = reader.ReadByte() > 0;
            this.precinctSizes = new JPXPrecinctSize[this.decompositionLevelCount + 1];
            for (int i = 0; i < (this.decompositionLevelCount + 1); i++)
            {
                this.precinctSizes[i] = new JPXPrecinctSize(usePrecincts ? reader.ReadByte() : 0xff);
            }
        }

        public int DecompositionLevelCount =>
            this.decompositionLevelCount;

        public int CodeBlockWidthExponent =>
            this.codeBlockWidthExponent;

        public int CodeBlockHeightExponent =>
            this.codeBlockHeightExponent;

        public JPXCodeBlockCodingStyle CodeBlockCodingStyle =>
            this.codeBlockCodingStyle;

        public bool UseWaveletTransformation =>
            this.useWaveletTransformation;

        public JPXPrecinctSize[] PrecinctSizes =>
            this.precinctSizes;
    }
}

