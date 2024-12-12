namespace DevExpress.Data.Mask
{
    using System;

    public abstract class MaskManagerFormattingBase<TState> : MaskManagerCommon<TState> where TState: MaskManagerPlainTextState
    {
        protected MaskManagerFormattingBase(TState initialState);
        protected abstract string CreateFormattedText(string editText);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        protected override int GetCursorPosition(TState state);
        protected override string GetDisplayText(TState state);
        protected virtual int GetEditPosition(string editText, int formattedPosition);
        protected abstract int GetFormattedPosition(string editText, int editPosition);
        protected override int GetSelectionAnchor(TState state);
    }
}

