namespace DevExpress.Data.Mask
{
    using System;
    using System.ComponentModel;

    public abstract class MaskManagerSelectAllEnhancer<TNestedMaskManager> : MaskManager where TNestedMaskManager: MaskManager
    {
        public readonly TNestedMaskManager Nested;
        private bool _isForcedSelectAll;

        protected MaskManagerSelectAllEnhancer(TNestedMaskManager nested);
        public override bool Backspace();
        protected void ClearSelectAllFlag();
        public override bool CursorEnd(bool forceSelection);
        public override bool CursorHome(bool forceSelection);
        public override bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public override bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public override bool Delete();
        public override bool FlushPendingEditActions();
        public override string GetCurrentEditText();
        public override object GetCurrentEditValue();
        public override bool Insert(string insertion);
        protected virtual bool MakeChange(Func<bool> changeWithTrueWhenSuccessfull);
        protected virtual bool MakeCursorOp(Func<bool> cursorOpWithTrueWhenSuccessfull);
        protected virtual bool MakeSpinOrUndoOp(Func<bool> spinOrUndoOpWithTrueWhenSuccessfull);
        private void Nested_EditTextChanged(object sender, EventArgs e);
        private void Nested_EditTextChanging(object sender, MaskChangingEventArgs e);
        private void Nested_LocalEditAction(object sender, CancelEventArgs e);
        public override void SelectAll();
        public override void SetInitialEditText(string initialEditText);
        public override void SetInitialEditValue(object initialEditValue);
        public override bool SpinDown();
        public override bool SpinUp();
        public override bool Undo();

        protected bool IsSelectAllEnforced { get; }

        protected virtual bool IsNestedCanSelectAll { get; }

        public override bool IsEditValueAssignedAsFormattedText { get; }

        public override string DisplayText { get; }

        public override int DisplayCursorPosition { get; }

        public override int DisplaySelectionAnchor { get; }

        public override bool CanUndo { get; }

        public override bool IsFinal { get; }

        public override bool IsMatch { get; }
    }
}

