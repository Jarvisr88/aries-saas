namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BrickVisibleArea
    {
        public BrickVisibleArea(RectangleF rectangle);

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Bottom { get; }

        public float ContentOffsetY { get; set; }

        public float ContentOffsetX { get; set; }

        public PointF ContentOffset { get; }

        public RectangleF Rectangle { get; }
    }
}

