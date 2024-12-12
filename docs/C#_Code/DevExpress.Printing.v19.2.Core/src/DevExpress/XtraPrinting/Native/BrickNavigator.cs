namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BrickNavigator
    {
        private BrickBase brick;
        private bool reversed;
        private bool forceBrickMap;

        public BrickNavigator(BrickBase brick, bool reversed, bool forceBrickMap);
        public Tuple<Brick, RectangleF> FindBrick(PointF pt);
        public virtual void IterateBricks(Func<Brick, RectangleF, RectangleF, bool> predicate);

        public PointF BrickPosition { get; set; }

        public RectangleF ClipRect { get; set; }
    }
}

