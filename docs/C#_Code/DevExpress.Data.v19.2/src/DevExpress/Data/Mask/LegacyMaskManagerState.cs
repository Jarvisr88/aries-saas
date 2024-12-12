namespace DevExpress.Data.Mask
{
    using System;

    public sealed class LegacyMaskManagerState : MaskManagerState
    {
        public string[] Elements;
        public int CursorPositionElement;
        public int CursorPositionInsideElement;
        public int SelectionAnchorElement;
        public int SelectionAnchorInsideElement;
        public LegacyMaskInfo Info;

        public LegacyMaskManagerState(LegacyMaskInfo info);
        private LegacyMaskManagerState(LegacyMaskManagerState source);
        public LegacyMaskManagerState(LegacyMaskInfo info, string[] elements, int cursorPositionElement, int cursorPositionInsideElement, int selectionAnchorElement, int selectionAnchorInsideElement);
        public bool Backspace();
        public LegacyMaskManagerState Clone();
        public bool CursorEnd(bool forceSelection);
        public bool CursorHome(bool forceSelection);
        public bool CursorLeft();
        public bool CursorRight();
        public bool CursorTo(int newPosition, bool forceSelection);
        public bool Delete();
        private bool Erase();
        public string GetDisplayText(char blank);
        public string GetEditText(char blank, bool saveLiteral);
        private bool Insert(char inp);
        public bool Insert(string insertion);
        public bool IsEmpty();
        public bool IsFinal(char blank);
        public bool IsMatch(char blank);
        public override bool IsSame(MaskManagerState comparedState);
        public void SelectAll();
        private bool SetCaretTo(int element, int insideElement);
        private void SetPositions(int element, int insideElement);

        public int DisplayCursorPosition { get; }

        public int DisplaySelectionAnchor { get; }
    }
}

