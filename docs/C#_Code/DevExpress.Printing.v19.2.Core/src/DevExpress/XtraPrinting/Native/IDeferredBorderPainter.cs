namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public interface IDeferredBorderPainter
    {
        void AddDeferredBrick(VisualBrick brick);
        void DrawBorders();
        void StartNewSection();
        bool TryDrawBorder(VisualBrick brick, Action<BrickStyle> drawBorder);

        bool AnySectionStarted { get; }
    }
}

