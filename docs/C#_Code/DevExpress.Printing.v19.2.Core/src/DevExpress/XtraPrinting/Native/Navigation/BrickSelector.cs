namespace DevExpress.XtraPrinting.Native.Navigation
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class BrickSelector
    {
        protected BrickSelector();
        public abstract bool CanSelect(Brick brick, RectangleF brickRect, RectangleF visibleRect);
    }
}

