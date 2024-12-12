namespace DevExpress.XtraEditors
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class RangeControlHitInfo
    {
        public static Point InvalidPoint;
        public static RangeControlHitInfo Empty;

        static RangeControlHitInfo();
        public RangeControlHitInfo();
        public RangeControlHitInfo(Point pt);
        public RangeControlHitInfo(Point pt, bool allowSelection);
        public bool ContainsSet(Rectangle rect, RangeControlHitTest hitTest);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public bool AllowSelection { get; set; }

        public Rectangle ObjectBounds { get; set; }

        public Point HitPoint { get; set; }

        public RangeControlHitTest HitTest { get; set; }

        public object ClientHitTest { get; set; }

        public object HitObject { get; set; }
    }
}

