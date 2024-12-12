namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class SeparableBrick : VisualBrick
    {
        public SeparableBrick()
        {
        }

        public SeparableBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        protected internal override float ValidatePageBottomInternal(float pageBottom, RectangleF rect, IPrintingSystemContext context) => 
            pageBottom;
    }
}

