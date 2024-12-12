namespace DevExpress.Data.Mask
{
    using System;

    public abstract class MaskManagerCommon<TState> : MaskManagerStated<TState> where TState: MaskManagerPlainTextState
    {
        protected MaskManagerCommon(TState initialState);
        protected abstract TState CreateStateForApply(string editText, int cursorPosition, int selectionAnchor);
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        protected override object GetEditValue(TState state);
        protected abstract TState GetEmptyState();
        protected virtual bool IsValidCursorPosition(int testedPositionInEditText);
        protected bool MoveCursorTo(int newPosition, bool forceSelection);
        protected bool MoveCursorTo(int newPosition, bool forceSelection, bool isNeededKeyCheck);
        public override void SetInitialEditText(string initialEditText);
        public override void SetInitialEditValue(object initialEditValue);
    }
}

