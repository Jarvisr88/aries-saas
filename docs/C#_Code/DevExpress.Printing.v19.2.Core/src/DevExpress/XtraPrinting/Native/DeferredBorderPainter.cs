namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public class DeferredBorderPainter : IDeferredBorderPainter
    {
        private Stack<Dictionary<VisualBrick, Action<BrickStyle>>> stack;
        private Dictionary<VisualBrick, Action<BrickStyle>> currentDictionary;

        public DeferredBorderPainter();
        public void AddDeferredBrick(VisualBrick brick);
        public void DrawBorders();
        public void StartNewSection();
        public bool TryDrawBorder(VisualBrick brick, Action<BrickStyle> drawBorder);

        public bool AnySectionStarted { get; }
    }
}

