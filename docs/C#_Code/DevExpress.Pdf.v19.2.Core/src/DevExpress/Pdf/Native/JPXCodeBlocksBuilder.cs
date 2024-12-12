namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXCodeBlocksBuilder
    {
        private readonly int x0;
        private readonly int x1;
        private readonly int y0;
        private readonly int y1;
        private readonly int codeBlockWidth;
        private readonly int codeBlockHeight;
        private readonly int rightCodeBlockBand;
        private readonly int bottomCodeBlockBand;
        private readonly int startInnerHorizontalOffset;
        private readonly int actualLeftBlockWidth;
        private readonly JPXCodeBlock[] codeBlocks;
        private int codeBlockIndex;

        private JPXCodeBlocksBuilder(JPXSubBand subBand, int codeBlockWidth, int codeBlockHeight)
        {
            this.x0 = subBand.X0;
            this.y0 = subBand.Y0;
            this.x1 = subBand.X1;
            this.y1 = subBand.Y1;
            this.codeBlockWidth = codeBlockWidth;
            this.codeBlockHeight = codeBlockHeight;
            int codeBlocksWide = subBand.CodeBlocksWide;
            int codeBlocksHigh = subBand.CodeBlocksHigh;
            this.rightCodeBlockBand = codeBlocksWide - 1;
            this.bottomCodeBlockBand = codeBlocksHigh - 1;
            this.codeBlocks = new JPXCodeBlock[codeBlocksWide * codeBlocksHigh];
            this.startInnerHorizontalOffset = ((this.x0 / codeBlockWidth) + 1) * codeBlockWidth;
            this.actualLeftBlockWidth = Math.Min((int) (this.startInnerHorizontalOffset - this.x0), (int) (this.x1 - this.x0));
        }

        private void AddRow(int top, int codeBlockHeight)
        {
            int codeBlockIndex = this.codeBlockIndex;
            this.codeBlockIndex = codeBlockIndex + 1;
            this.codeBlocks[codeBlockIndex] = new JPXCodeBlock(this.x0, top, this.actualLeftBlockWidth, codeBlockHeight);
            int startInnerHorizontalOffset = this.startInnerHorizontalOffset;
            int num3 = 1;
            while (num3 < this.rightCodeBlockBand)
            {
                codeBlockIndex = this.codeBlockIndex;
                this.codeBlockIndex = codeBlockIndex + 1;
                this.codeBlocks[codeBlockIndex] = new JPXCodeBlock(startInnerHorizontalOffset, top, this.codeBlockWidth, codeBlockHeight);
                num3++;
                startInnerHorizontalOffset += this.codeBlockWidth;
            }
            if (this.rightCodeBlockBand > 0)
            {
                codeBlockIndex = this.codeBlockIndex;
                this.codeBlockIndex = codeBlockIndex + 1;
                this.codeBlocks[codeBlockIndex] = new JPXCodeBlock(startInnerHorizontalOffset, top, this.x1 - startInnerHorizontalOffset, codeBlockHeight);
            }
        }

        private JPXCodeBlock[] Build()
        {
            if (this.codeBlocks.Length != 0)
            {
                this.AddRow(this.y0, Math.Min((int) ((((this.y0 / this.codeBlockHeight) + 1) * this.codeBlockHeight) - this.y0), (int) (this.y1 - this.y0)));
                int top = ((this.y0 / this.codeBlockHeight) + 1) * this.codeBlockHeight;
                int num2 = 1;
                while (true)
                {
                    if (num2 >= this.bottomCodeBlockBand)
                    {
                        if (this.bottomCodeBlockBand > 0)
                        {
                            this.AddRow(top, this.y1 - top);
                        }
                        break;
                    }
                    this.AddRow(top, this.codeBlockHeight);
                    num2++;
                    top += this.codeBlockHeight;
                }
            }
            return this.codeBlocks;
        }

        public static JPXCodeBlock[] Build(JPXSubBand subBand, int codeBlockWidth, int codeBlockHeight) => 
            new JPXCodeBlocksBuilder(subBand, codeBlockWidth, codeBlockHeight).Build();
    }
}

