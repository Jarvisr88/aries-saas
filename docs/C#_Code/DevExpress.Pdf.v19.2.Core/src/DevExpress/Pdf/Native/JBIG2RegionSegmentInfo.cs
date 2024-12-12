namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2RegionSegmentInfo
    {
        private readonly int width;
        private readonly int height;
        private readonly int x;
        private readonly int y;
        private readonly int composeOperator;

        public JBIG2RegionSegmentInfo(JBIG2StreamHelper helper)
        {
            this.width = helper.ReadInt32();
            this.height = helper.ReadInt32();
            this.x = helper.ReadInt32();
            this.y = helper.ReadInt32();
            byte num = helper.ReadByte();
            this.composeOperator = num & 7;
        }

        public JBIG2RegionSegmentInfo(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.x = 0;
            this.y = 0;
            this.composeOperator = 0;
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public int X =>
            this.x;

        public int Y =>
            this.y;

        public int ComposeOperator =>
            this.composeOperator;
    }
}

