namespace DevExpress.XtraPrinting.Native.Navigation
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class TextBrickSelector : BrickSelector
    {
        private readonly string text;
        private readonly bool wholeWord;
        private readonly bool matchCase;
        private readonly IPrintingSystemContext context;

        public TextBrickSelector(string text, bool wholeWord, bool matchCase, IPrintingSystemContext context);
        public override bool CanSelect(Brick brick, RectangleF brickRect, RectangleF visibleRect);
        public string GetVisibleText(TextBrick textBrick, RectangleF brickRect, RectangleF visibleRect);
        private static string ReplaceNoBreakSpace(string text);
        private static bool StringContainsWord(string text, string searchingWord);
    }
}

