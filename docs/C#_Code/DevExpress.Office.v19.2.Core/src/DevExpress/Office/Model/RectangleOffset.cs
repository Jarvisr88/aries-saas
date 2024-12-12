namespace DevExpress.Office.Model
{
    using DevExpress.Office.History;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleOffset
    {
        private int bottomOffset;
        private int leftOffset;
        private int rightOffset;
        private int topOffset;
        public static RectangleOffset Empty;
        public override bool Equals(object obj)
        {
            RectangleOffset offset = (RectangleOffset) obj;
            return ((this.BottomOffset == offset.BottomOffset) && ((this.LeftOffset == offset.LeftOffset) && ((this.RightOffset == offset.RightOffset) && (this.TopOffset == offset.TopOffset))));
        }

        public override int GetHashCode() => 
            (((this.GetHashCode() ^ this.BottomOffset) ^ this.LeftOffset) ^ this.RightOffset) ^ this.TopOffset;

        public RectangleOffset(int bottomOffset, int leftOffset, int rightOffset, int topOffset)
        {
            this.bottomOffset = bottomOffset;
            this.leftOffset = leftOffset;
            this.rightOffset = rightOffset;
            this.topOffset = topOffset;
        }

        public int BottomOffset =>
            this.bottomOffset;
        public int LeftOffset =>
            this.leftOffset;
        public int RightOffset =>
            this.rightOffset;
        public int TopOffset =>
            this.topOffset;
        internal void Write(IHistoryWriter writer)
        {
            writer.Write(this.bottomOffset);
            writer.Write(this.leftOffset);
            writer.Write(this.rightOffset);
            writer.Write(this.topOffset);
        }

        static RectangleOffset()
        {
        }
    }
}

