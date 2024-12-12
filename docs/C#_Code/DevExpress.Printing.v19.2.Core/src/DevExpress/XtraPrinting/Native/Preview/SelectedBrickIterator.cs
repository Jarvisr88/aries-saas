namespace DevExpress.XtraPrinting.Native.Preview
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SelectedBrickIterator
    {
        private List<BrickIterator> iterators;
        private Page page;
        private RectangleF pageRect;
        private RectangleF selectionRect;

        private SelectedBrickIterator(Page page, RectangleF pageRect, RectangleF selectionRect);
        private RectangleF GetCurrentClipRect();
        public static List<Tuple<Brick, RectangleF>> GetSelectedBricks(Page page, RectangleF pageRect, RectangleF selectionRect);
        private bool MoveNext();

        private BrickIterator CurrentIterator { get; }

        private BrickBase CurrentBrick { get; }

        private RectangleF CurrentBrickRectangle { get; }

        private RectangleF CurrentClipRectangle { get; }
    }
}

