namespace DevExpress.Data.Mask
{
    using System;

    public class MaskManagerPlainText : MaskManagerCommon<MaskManagerPlainTextState>
    {
        public MaskManagerPlainText();
        protected bool Apply(string editText, int cursorPosition, int selectionAnchor, MaskManagerStated<MaskManagerPlainTextState>.StateChangeType changeType);
        public override bool Backspace();
        protected override MaskManagerPlainTextState CreateStateForApply(string editText, int cursorPosition, int selectionAnchor);
        public override bool Delete();
        protected override int GetCursorPosition(MaskManagerPlainTextState state);
        protected override string GetDisplayText(MaskManagerPlainTextState state);
        protected override string GetEditText(MaskManagerPlainTextState state);
        protected override MaskManagerPlainTextState GetEmptyState();
        protected override int GetSelectionAnchor(MaskManagerPlainTextState state);
        public override bool Insert(string insertion);
        public override void SelectAll();

        public override bool IsEditValueAssignedAsFormattedText { get; }
    }
}

