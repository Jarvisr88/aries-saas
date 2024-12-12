namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public interface IBrickPaintService
    {
        bool TryDrawBackground(Action drawBackground, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect);
        bool TryDrawContent(Action<IGraphics, RectangleF> drawContent, VisualBrick brick, BrickPaintBase brickPaint, IGraphics gr, RectangleF rect);

        bool EditingFieldsHighlighted { get; set; }
    }
}

