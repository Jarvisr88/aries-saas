namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class NestedBrickIterator : IEnumerator
    {
        private List<BrickIterator> iterators;
        private bool reversed;
        private bool forceBrickMap;
        private IIndexedEnumerator enumerator;
        private BrickBase brickContainer;

        public NestedBrickIterator(BrickBase brickContainer);
        public NestedBrickIterator(IList bricks);
        protected NestedBrickIterator(bool reversed, bool forceBrickMap);
        public NestedBrickIterator(BrickBase brick, bool reversed, bool forceBrickMap);
        private IIndexedEnumerator CreateEnumerator(BrickBase brick);
        internal static IIndexedEnumerator CreateIndexedEnumerator(BrickBase brick);
        private static IIndexedEnumerator CreateIndexedEnumerator(BrickBase brick, bool forceBrickMap, bool reversed);
        public int[] GetCurrentBrickIndices();
        private RectangleF GetCurrentClipRect();
        public bool MoveNext();
        public void Reset();

        public PointF Offset { get; set; }

        public RectangleF ClipRect { get; set; }

        public BrickBase CurrentBrick { get; }

        public BrickBase CurrentBrickOwner { get; }

        public RectangleF CurrentBrickRectangle { get; }

        public RectangleF CurrentClipRectangle { get; }

        private BrickIterator CurrentIterator { get; }

        object IEnumerator.Current { get; }
    }
}

