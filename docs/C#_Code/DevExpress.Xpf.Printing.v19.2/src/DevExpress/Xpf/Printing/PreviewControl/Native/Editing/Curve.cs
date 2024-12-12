namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class Curve
    {
        public Curve(System.Drawing.Color color, float thickness)
        {
            this.Color = color;
            this.Thickness = thickness;
            this.Points = new List<PointF>();
        }

        public System.Drawing.Color Color { get; private set; }

        public float Thickness { get; private set; }

        public List<PointF> Points { get; private set; }
    }
}

