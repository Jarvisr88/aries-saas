namespace DevExpress.Data.Mask
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class MaskManager
    {
        public event EventHandler EditTextChanged;

        public event MaskChangingEventHandler EditTextChanging;

        public event CancelEventHandler LocalEditAction;

        protected MaskManager();
        public abstract bool Backspace();
        public abstract bool CursorEnd(bool forceSelection);
        public abstract bool CursorHome(bool forceSelection);
        public bool CursorLeft(bool forceSelection);
        public abstract bool CursorLeft(bool forceSelection, bool isNeededKeyCheck);
        public bool CursorRight(bool forceSelection);
        public abstract bool CursorRight(bool forceSelection, bool isNeededKeyCheck);
        public abstract bool CursorToDisplayPosition(int newPosition, bool forceSelection);
        public abstract bool Delete();
        public virtual bool FlushPendingEditActions();
        public abstract string GetCurrentEditText();
        public abstract object GetCurrentEditValue();
        public abstract bool Insert(string insertion);
        protected void RaiseEditTextChanged();
        protected virtual bool RaiseEditTextChanging(object newEditValue);
        protected bool RaiseModifyWithoutEditValueChange();
        public abstract void SelectAll();
        public abstract void SetInitialEditText(string initialEditText);
        public abstract void SetInitialEditValue(object initialEditValue);
        public abstract bool SpinDown();
        public abstract bool SpinUp();
        public abstract bool Undo();

        public virtual bool IsEditValueAssignedAsFormattedText { get; }

        public virtual bool IsMatch { get; }

        public virtual bool IsFinal { get; }

        public bool IsSelection { get; }

        public abstract string DisplayText { get; }

        public abstract int DisplayCursorPosition { get; }

        public abstract int DisplaySelectionAnchor { get; }

        public int DisplaySelectionStart { get; }

        public int DisplaySelectionEnd { get; }

        public int DisplaySelectionLength { get; }

        public abstract bool CanUndo { get; }
    }
}

