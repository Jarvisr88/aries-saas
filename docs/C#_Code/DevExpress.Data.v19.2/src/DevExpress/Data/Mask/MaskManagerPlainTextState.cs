namespace DevExpress.Data.Mask
{
    using System;

    public class MaskManagerPlainTextState : MaskManagerState
    {
        private string editText;
        private int cursorPosition;
        private int selectionAnchor;
        public static MaskManagerPlainTextState Empty;

        static MaskManagerPlainTextState();
        public MaskManagerPlainTextState(string editText, int cursorPosition, int selectionAnchor);
        public override bool IsSame(MaskManagerState comparedState);

        public string EditText { get; }

        public int CursorPosition { get; }

        public int SelectionAnchor { get; }
    }
}

