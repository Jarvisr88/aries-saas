namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;

    public interface IBrickGraphics
    {
        IBrick DrawBrick(IBrick brick, RectangleF rect);
        void RaiseModifierChanged();

        BrickStyle DefaultBrickStyle { get; set; }
    }
}

