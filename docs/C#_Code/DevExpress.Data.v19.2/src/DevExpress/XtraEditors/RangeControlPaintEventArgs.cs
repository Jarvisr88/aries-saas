namespace DevExpress.XtraEditors
{
    using DevExpress.Utils.Drawing;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class RangeControlPaintEventArgs : EventArgs
    {
        public int CalcX(double normalizedValue);

        public IRangeControl RangeControl { get; set; }

        public IGraphicsCache Cache { get; set; }

        public virtual System.Drawing.Graphics Graphics { get; }

        public Rectangle Bounds { get; set; }

        public Rectangle ContentBounds { get; set; }

        public Rectangle RulerBounds { get; set; }

        public Color BorderColor { get; }

        public Color RulerColor { get; }

        public Color LabelColor { get; }

        public Matrix NormalTransform { get; }

        public RangeControlHitInfo HotInfo { get; set; }

        public RangeControlHitInfo PressedInfo { get; set; }

        public double ActualRangeMinimum { get; set; }

        public double ActualRangeMaximum { get; set; }
    }
}

