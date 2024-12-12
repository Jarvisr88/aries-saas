namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BrickIterator : IEnumerator
    {
        private BrickBase container;
        private IIndexedEnumerator bricks;
        private PointF innerOffset;
        private PointF outerOffset;

        public BrickIterator(BrickBase container, IList bricks, PointF offset, RectangleF clipRect);
        public BrickIterator(BrickBase container, IIndexedEnumerator bricks, PointF innerOffset, PointF outerOffset, RectangleF clipRect);
        public bool MoveNext();
        private bool Predicate(RectangleF rect);
        public void Reset();

        public int Index { get; }

        public PointF Offset { get; private set; }

        public BrickBase CurrentBrick { get; }

        public virtual RectangleF CurrentBrickRectangle { get; }

        public RectangleF CurrentBrickClientRectangle { get; }

        public RectangleF CurrentClipRectangle { get; private set; }

        public object Current { get; }
    }
}

