namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class JPXResolutionLevel : JPXArea
    {
        private readonly int levelNumber;
        private readonly int precinctWidth;
        private readonly int precinctHeight;
        private readonly int codeBlockWidth;
        private readonly int codeBlockHeight;
        private readonly int numPrecinctsWide;
        private readonly int numPrecinctsHigh;
        private readonly int precinctCount;
        private readonly List<JPXPrecinct> precincts;

        protected JPXResolutionLevel(JPXTileComponent component, int levelNumber, JPXCodingStyleComponent codingStyle)
        {
            this.levelNumber = levelNumber;
            long num = 1L << ((codingStyle.DecompositionLevelCount - levelNumber) & 0x3f);
            int num2 = (int) Math.Ceiling((double) (((float) component.X0) / ((float) num)));
            base.X0 = num2;
            int num3 = (int) Math.Ceiling((double) (((float) component.Y0) / ((float) num)));
            base.Y0 = num3;
            int num4 = (int) Math.Ceiling((double) (((float) component.X1) / ((float) num)));
            base.X1 = num4;
            int num5 = (int) Math.Ceiling((double) (((float) component.Y1) / ((float) num)));
            base.Y1 = num5;
            JPXPrecinctSize size = codingStyle.PrecinctSizes[levelNumber];
            int widthExponent = size.WidthExponent;
            int heightExponent = size.HeightExponent;
            this.precinctWidth = 1 << (widthExponent & 0x1f);
            this.precinctHeight = 1 << (heightExponent & 0x1f);
            int num8 = (levelNumber == 0) ? 0 : 1;
            this.codeBlockWidth = 1 << (Math.Min(codingStyle.CodeBlockWidthExponent, widthExponent - num8) & 0x1f);
            this.codeBlockHeight = 1 << (Math.Min(codingStyle.CodeBlockHeightExponent, heightExponent - num8) & 0x1f);
            this.numPrecinctsWide = ((int) Math.Ceiling((double) (((float) num4) / ((float) this.precinctWidth)))) - (num2 / this.precinctWidth);
            this.numPrecinctsHigh = ((int) Math.Ceiling((double) (((float) num5) / ((float) this.precinctHeight)))) - (num3 / this.precinctHeight);
            this.precinctCount = this.numPrecinctsWide * this.numPrecinctsHigh;
            this.precincts = new List<JPXPrecinct>(this.precinctCount);
        }

        protected void AppendPrecincts(JPXSubBand subBand)
        {
            int num = 0;
            int y = 0;
            while (y < this.numPrecinctsHigh)
            {
                int x = 0;
                while (true)
                {
                    if (x >= this.numPrecinctsWide)
                    {
                        y++;
                        break;
                    }
                    JPXPrecinct item = new JPXPrecinct(subBand, this.CodeBlockWidth, this.CodeBlockHeight, x, y, this.precinctWidth, this.precinctHeight, num++);
                    JPXCodeBlock[] codeBlocks = subBand.CodeBlocks;
                    int index = 0;
                    while (true)
                    {
                        if (index >= codeBlocks.Length)
                        {
                            this.precincts.Add(item);
                            x++;
                            break;
                        }
                        JPXCodeBlock block = codeBlocks[index];
                        if ((block.X0 >= item.X0) && ((block.X1 <= item.X1) && ((block.Y0 >= item.Y0) && (block.Y1 <= item.Y1))))
                        {
                            item.CodeBlocks.Add(block);
                        }
                        index++;
                    }
                }
            }
        }

        public int LevelNumber =>
            this.levelNumber;

        public int CodeBlockWidth =>
            this.codeBlockWidth;

        public int CodeBlockHeight =>
            this.codeBlockHeight;

        public int NumPrecinctsWide =>
            this.numPrecinctsWide;

        public int NumPrecinctsHigh =>
            this.numPrecinctsHigh;

        public int PrecinctCount =>
            this.precinctCount;

        public IList<JPXPrecinct> Precincts =>
            this.precincts;
    }
}

