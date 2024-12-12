namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JPXPrecinct : JPXArea
    {
        private readonly List<JPXCodeBlock> codeBlocks;
        private readonly JPXTagTree inclusionTree;
        private readonly JPXTagTree zeroBitPlaneTree;
        private readonly int codeBlockHorizontalCount;
        private readonly int codeBlockVerticalCount;
        private readonly int number;

        public JPXPrecinct(JPXSubBand subBand, int codeBlockWidth, int codeBlockHeight, int x, int y, int width, int height, int number)
        {
            this.number = number;
            x += subBand.X0 / width;
            y += subBand.Y0 / height;
            base.X0 = Math.Max(subBand.X0, x * width);
            base.Y0 = Math.Max(subBand.Y0, y * height);
            base.X1 = Math.Min(subBand.X1, (x + 1) * width);
            base.Y1 = Math.Min(subBand.Y1, (y + 1) * height);
            this.codeBlockHorizontalCount = (int) Math.Ceiling((double) (((float) base.Width) / ((float) codeBlockWidth)));
            this.codeBlockVerticalCount = (int) Math.Ceiling((double) (((float) base.Height) / ((float) codeBlockHeight)));
            if ((this.codeBlockHorizontalCount * this.codeBlockVerticalCount) != 0)
            {
                this.inclusionTree = new JPXTagTree(this.codeBlockHorizontalCount, this.codeBlockVerticalCount);
                this.zeroBitPlaneTree = new JPXTagTree(this.codeBlockHorizontalCount, this.codeBlockVerticalCount);
            }
            this.codeBlocks = new List<JPXCodeBlock>();
        }

        public int CodeBlockHorizontalCount =>
            this.codeBlockHorizontalCount;

        public int CodeBlockVerticalCount =>
            this.codeBlockVerticalCount;

        public List<JPXCodeBlock> CodeBlocks =>
            this.codeBlocks;

        public JPXTagTree InclusionTree =>
            this.inclusionTree;

        public JPXTagTree ZeroBitPlaneTree =>
            this.zeroBitPlaneTree;

        public int Number =>
            this.number;
    }
}

