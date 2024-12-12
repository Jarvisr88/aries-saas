namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;

    [BrickExporter(typeof(EmptyBrickExporter))]
    public class SpanBrick : Brick
    {
        public SpanBrick()
        {
            base.CanPublish = false;
            base.IsVisible = false;
        }

        public override float ValidatePageBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            pageBounds.Bottom;

        public override float ValidatePageRight(float pageRight, RectangleF rect) => 
            pageRight;

        public override string BrickType =>
            "Span";

        internal override bool IsServiceBrick =>
            true;
    }
}

