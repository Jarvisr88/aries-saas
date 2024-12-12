namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodeBlockHeaderData
    {
        private readonly JPXCodeBlock codeBlock;
        private readonly byte codingPasses;
        private readonly int chunkLength;

        public JPXCodeBlockHeaderData(JPXCodeBlock codeBlock, byte codingPasses, int chunkLength)
        {
            this.codeBlock = codeBlock;
            this.codingPasses = codingPasses;
            this.chunkLength = chunkLength;
        }

        public JPXCodeBlock CodeBlock =>
            this.codeBlock;

        public byte CodingPasses =>
            this.codingPasses;

        public int ChunkLength =>
            this.chunkLength;
    }
}

