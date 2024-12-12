namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JPXCodeBlock : JPXArea
    {
        private readonly List<byte> data;
        private int zeroBitPlanes;
        private byte codingPassCount;
        private int lBlock = 3;
        private bool isFirstTimeInclusion = true;

        public JPXCodeBlock(int x, int y, int width, int height)
        {
            base.X0 = x;
            base.Y0 = y;
            base.X1 = x + width;
            base.Y1 = y + height;
            this.data = new List<byte>();
        }

        public void AddChunk(JPXCodeBlockChunk chunk)
        {
            this.data.AddRange(chunk.Data);
            this.codingPassCount = (byte) (this.codingPassCount + chunk.CodingPassCount);
        }

        public int ZeroBitPlanes
        {
            get => 
                this.zeroBitPlanes;
            set => 
                this.zeroBitPlanes = value;
        }

        public int LBlock
        {
            get => 
                this.lBlock;
            set => 
                this.lBlock = value;
        }

        public byte CodingPassCount =>
            this.codingPassCount;

        public byte[] EncodedData =>
            this.data.ToArray();

        public bool IsFirstTimeInclusion
        {
            get => 
                this.isFirstTimeInclusion;
            set => 
                this.isFirstTimeInclusion = value;
        }
    }
}

