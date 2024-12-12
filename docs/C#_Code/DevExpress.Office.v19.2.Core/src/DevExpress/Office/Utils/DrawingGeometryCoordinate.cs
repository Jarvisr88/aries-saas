namespace DevExpress.Office.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class DrawingGeometryCoordinate
    {
        private const uint MinGuideIndex = 0x80000000;
        private const uint MaxGuideIndex = 0x8000007f;

        public DrawingGeometryCoordinate()
        {
            this.GuideIndex = -1;
        }

        public static DrawingGeometryCoordinate FromBytes(byte[] bytes, int offset, int size)
        {
            DrawingGeometryCoordinate coordinate = new DrawingGeometryCoordinate();
            if (size == 2)
            {
                coordinate.Value = BitConverter.ToInt16(bytes, offset);
            }
            else if (size == 4)
            {
                uint num = BitConverter.ToUInt32(bytes, offset);
                if ((num >= 0x80000000) && (num <= 0x8000007f))
                {
                    coordinate.GuideIndex = ((int) num) - -2147483648;
                }
                else
                {
                    coordinate.Value = BitConverter.ToInt32(bytes, offset);
                }
            }
            return coordinate;
        }

        public byte[] GetBytes() => 
            this.IsConstant ? BitConverter.GetBytes(this.Value) : BitConverter.GetBytes((uint) (0x80000000UL + this.GuideIndex));

        public int Value { get; set; }

        public int GuideIndex { get; set; }

        public bool IsConstant =>
            this.GuideIndex < 0;
    }
}

